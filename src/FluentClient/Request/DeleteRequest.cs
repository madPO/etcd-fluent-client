namespace FluentClient.Request
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Client;

    public class DeleteRequest : IDeleteRequest
    {
        private readonly Func<IDeleteRequest, CancellationToken, Task> _execute;

        public DeleteRequest(Func<IDeleteRequest, CancellationToken, Task> execute)
        {
            _execute = execute;
        }
        
        public string Host { get; set; }
        
        public int Port { get; set; }
        
        public EtcdKey Key { get; set; }

        public EtcdKey ToKey { get; set; }

        public EtcdKey ContainsKey { get; set; }

        public Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            return _execute(this, cancellationToken);
        }
    }
}