namespace FluentClient.Client
{
    using System.Threading;
    using System.Threading.Tasks;
    using Request;

    public partial class EtcdClient
    {
        public Task<EtcdLease> GrantLeaseAsync(long second, CancellationToken cancellationToken = default)
        {
            var request = new CreateLeaseRequest((r, t) => Transport.ExecuteGrantLeaseAsync(r, t))
            {
                Ttl = second
            };
            
            FillHost(request);

            return request.ExecuteAsync(cancellationToken);
        }

        public Task RevokeLeaseAsync(EtcdLease lease, CancellationToken cancellationToken = default)
        {
            var request = new RevokeLeaseRequest((r, t) => Transport.ExecuteRevokeLeaseAsync(r, t))
            {
                EtcdLease = lease
            };
            
            FillHost(request);

            return request.ExecuteAsync(cancellationToken);
        }

        public Task<EtcdLease> TimeToLiveLeaseAsync(EtcdLease lease, CancellationToken cancellationToken = default)
        {
            var request = new TimeToLiveLeaseRequest((r, t) => Transport.ExecuteTimeToLiveLeaseAsync(r, t))
            {
                EtcdLease = lease
            };

            FillHost(request);

            return request.ExecuteAsync(cancellationToken);
        }
    }
}