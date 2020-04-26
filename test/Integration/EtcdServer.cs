namespace Integration
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    public class EtcdServer : IDisposable
    {
        private Process _process;
        
        private TaskCompletionSource<bool> _taskSourcing;

        public EtcdServer()
        {
            _process = new Process();
            _taskSourcing = new TaskCompletionSource<bool>();
        }
        
        public void Run(CancellationToken cancellationToken = default)
        {
            const string controlLine = "ready to serve client requests";
            cancellationToken.ThrowIfCancellationRequested();
            
            _process.StartInfo.FileName = "docker.exe";
            //todo: ALLOW_NONE_AUTHENTICATION in method arguments
            _process.StartInfo.Arguments = "run --rm --publish 2379:2379 --publish 2380:2380 --env ALLOW_NONE_AUTHENTICATION=yes bitnami/etcd:3";
            _process.StartInfo.UseShellExecute = false;
            _process.StartInfo.RedirectStandardOutput = true;
            _process.StartInfo.RedirectStandardError = true;
            _process.StartInfo.CreateNoWindow = true;
            _process.OutputDataReceived += (sender, args) =>
            {
                if(cancellationToken.IsCancellationRequested)
                    _taskSourcing.SetCanceled();

                var data = args.Data;
                
                if(data == null)
                    return;
                
                if (data.Contains(controlLine, StringComparison.InvariantCultureIgnoreCase))
                {
                    _taskSourcing.SetResult(true);
                }
            };
            _process.Start();
        }

        public Task WaitLaunchEnding(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            //todo: work it!
            _process.BeginOutputReadLine();
            
            Thread.Sleep(TimeSpan.FromMinutes(2));
            
            return Task.CompletedTask;
        }

        public void Stop()
        {
            _process?.Kill();
        }

        public void Dispose()
        {
            Stop();
            _process?.Dispose();
        }
    }
}