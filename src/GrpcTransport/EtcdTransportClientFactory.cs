namespace GrpcTransport
{
    using Dawn;
    using Etcdserverpb;
    using Grpc.Core;

    public class EtcdTransportClientFactory
    {
        private static EtcdTransportClientPool<KV.KVClient> _kvPool;
        
        private static EtcdTransportClientPool<Lease.LeaseClient> _leasePool;

        static EtcdTransportClientFactory()
        {
            _kvPool = new EtcdTransportClientPool<KV.KVClient>();
            _leasePool = new EtcdTransportClientPool<Lease.LeaseClient>();
        }
        
        public static KV.KVClient GetKvClient(string host, int port)
        {
            Guard.Argument(host).NotNull().NotEmpty();
            Guard.Argument(port).Positive();
            
            var channel = new Channel(host, port, ChannelCredentials.Insecure);
            return _kvPool.Get(channel, () => new KV.KVClient(channel));
        }

        public static Lease.LeaseClient GetLeaseClient(string host, int port)
        {
            Guard.Argument(host).NotNull().NotEmpty();
            Guard.Argument(port).Positive();
            
            var channel = new Channel(host, port, ChannelCredentials.Insecure);
            return _leasePool.Get(channel, () => new Lease.LeaseClient(channel));
        } 
        
    }
}