namespace FluentClient.Request
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Client;
    using Transport;

    public class GetRequest : IGetRequest
    {
        private readonly IEtcdTransport _transport;

        public GetRequest(IEtcdTransport transport)
        {
            _transport = transport;
        }
        
        public string Host { get; set; }
        
        public int Port { get; set; }
        
        public string Key { get; set; }

        private EtcdKey _toKey;

        private EtcdKey _containsKey;

        private int? _limit;

        private int? _version;
        
        public IGetRequest ToKey(EtcdKey key)
        {
            _toKey = key;

            return this;
        }

        public IGetRequest Contains(EtcdKey key)
        {
            _containsKey = key;

            return this;
        }

        public IGetRequest Limit(int limit)
        {
            _limit = limit;

            return this;
        }

        public IGetRequest Revision(int version)
        {
            _version = version;

            return this;
        }

        public Task<IReadOnlyCollection<byte[]>> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            return _transport.ExecuteGetAsync(this, cancellationToken);
        }
    }
}