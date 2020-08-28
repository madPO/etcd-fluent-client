namespace FluentClient.Request
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Client;

    public class EtcdWatchRequest : IWatchRequest
    {
        private readonly Func<IWatchRequest, CancellationToken, Task<EtcdWatcher>> _execute;

        public EtcdWatchRequest(Func<IWatchRequest, CancellationToken, Task<EtcdWatcher>> execute)
        {
            _execute = execute;
        }

        public string Host { get; set; }
        
        public int Port { get; set; }
        
        public string Key { get; set; }

        public Task<EtcdWatcher> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return _execute(this, cancellationToken);
        }
    }
}