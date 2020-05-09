namespace Integration
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    public class EtcdServer : IDisposable
    {
        private readonly string _rootPassword;
        
        private readonly Process _process;
        
        private readonly TaskCompletionSource<bool> _taskSourcing;

        private ServerState _state;

        private readonly string _name;

        public EtcdServer(string rootPassword = null)
        {
            _rootPassword = rootPassword;
            _process = new Process();
            _taskSourcing = new TaskCompletionSource<bool>();
            _state = ServerState.Empty;
            _name = $"etcd_test_server_{Guid.NewGuid():N}";
        }
        
        public void Run(CancellationToken cancellationToken = default)
        {
            const string controlLine = "ready to serve client requests";
            cancellationToken.ThrowIfCancellationRequested();
            var cancelSource = new CancellationTokenSource(TimeSpan.FromMinutes(3));
            cancelSource.Token.Register(() =>
            {
                if(_state == ServerState.Starting)
                    _taskSourcing.SetCanceled();
            });
            cancellationToken.Register(() =>
            {
                if(_state == ServerState.Starting)
                    _taskSourcing.SetCanceled();
            });

            var arguments = "run --rm --publish 2379:2379";

            if (string.IsNullOrEmpty(_rootPassword))
            {
                arguments += " --env ALLOW_NONE_AUTHENTICATION=yes ";
            }
            else
            {
                arguments += $" --env ETCD_ROOT_PASSWORD={_rootPassword} ";
            }

            arguments += $"--name {_name}  bitnami/etcd:3";
            
            _process.StartInfo.FileName = "docker.exe";
            _process.StartInfo.Arguments = arguments;
            _process.StartInfo.UseShellExecute = false;
            _process.StartInfo.RedirectStandardOutput = true;
            _process.StartInfo.RedirectStandardError = true;
            _process.StartInfo.RedirectStandardInput = true;
            _process.StartInfo.CreateNoWindow = true;
            
            _process.ErrorDataReceived += (sender, args) =>
            {
                var data = args.Data;
                
                if(data == null)
                    return;
                
                if (data.Contains(controlLine, StringComparison.InvariantCultureIgnoreCase))
                {
                    _state = ServerState.Run;
                    _taskSourcing.SetResult(true);
                }

                if (data.Contains("error", StringComparison.InvariantCultureIgnoreCase))
                {
                    _state = ServerState.Empty;
                    _taskSourcing.SetException(new Exception(data));
                }
            };

            _state = ServerState.Starting;
            
            _process.Start();
            _process.BeginOutputReadLine();
            _process.BeginErrorReadLine();
        }

        public Task WaitLaunchEnding(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            return _taskSourcing.Task;
        }

        public void Stop()
        {
            const int waitTime = 60 * 1000;
            if(_state == ServerState.Stopping || _state == ServerState.Empty)
                return;

            _state = ServerState.Stopping;
            if (_process != null)
            {
                _process.Kill();
                var stopCmd = new Process();
                stopCmd.StartInfo.FileName = "docker.exe";
                stopCmd.StartInfo.Arguments = $"stop {_name}";
                stopCmd.Start();
                _process.WaitForExit(waitTime);
                stopCmd.WaitForExit(waitTime);
            }
            
            _state = ServerState.Empty;
        }

        public void Dispose()
        {
            Stop();
            _process?.Dispose();
        }
    }

    enum ServerState
    {
        Empty,
        
        Starting,
        
        Run,
        
        Stopping
    }
}