namespace FluentClient.Request
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Client;

    public class EtcdPutRequest : IPutRequest
    {
        private readonly Func<IPutRequest, CancellationToken, Task> _execute;
        
        public EtcdPutRequest(Func<IPutRequest, CancellationToken, Task> execute)
        {
            _execute = execute;
        }
        
        public string Host { get; set; }
        
        public int Port { get; set; }
        
        public string Key { get; set; }
        
        public byte[] Value { get; set; }
        
        public EtcdLease EtcdLease { get; set; }
        
        public Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            return _execute(this, cancellationToken);
        }
    }
}