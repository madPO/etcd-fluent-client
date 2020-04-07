namespace FluentClient.Request
{
    using System.Threading;
    using System.Threading.Tasks;
    using Client;
    using Transport;

    public class DeleteRequest : IDeleteRequest
    {
        private readonly IEtcdTransport _transport;

        public DeleteRequest(IEtcdTransport transport)
        {
            _transport = transport;
        }
        
        public string Host { get; set; }
        public int Port { get; set; }
        public string Key { get; set; }

        private EtcdKey _toKey;

        private EtcdKey _containsKey;
        
        public IDeleteRequest ToKey(EtcdKey key)
        {
            _toKey = key;

            return this;
        }

        public IDeleteRequest Contains(EtcdKey key)
        {
            _containsKey = _containsKey;

            return this;
        }

        public Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            return _transport.ExecuteDeleteAsync(this, cancellationToken);
        }
    }
}