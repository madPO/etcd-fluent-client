namespace FluentClient.Request
{
    using System.Threading;
    using System.Threading.Tasks;
    using Client;
    using Transport;

    public class EtcdPutRequest : IPutRequest
    {
        private readonly IEtcdTransport _transport;

        public EtcdPutRequest(IEtcdTransport transport)
        {
            _transport = transport;
        }
        
        public string Host { get; set; }
        public int Port { get; set; }
        public string Key { get; set; }
        public byte[] Value { get; set; }
        public ILease Lease { get; private set; }
        
        public async Task WithLeaseAsync(ILease lease, CancellationToken cancellationToken = default)
        {
            Lease = lease;

            await ExecuteAsync(cancellationToken);
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            await _transport.ExecutePutAsync(this, cancellationToken);
        }
    }
}