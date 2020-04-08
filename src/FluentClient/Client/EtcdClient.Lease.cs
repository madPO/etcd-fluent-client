namespace FluentClient.Client
{
    using System.Threading;
    using System.Threading.Tasks;
    using Request;

    public partial class EtcdClient
    {
        public Task<EtcdLease> GrantLeaseAsync(long second, CancellationToken cancellationToken = default)
        {
            //todo: fill host -> extrnal method
            var host = Gateway.GetHost();
            var request = new CreateLeaseRequest((r, t) => Transport.ExecuteGrantLeaseAsync(r, t))
            {
                Host = host.Item1,
                Port = host.Item2,
                Ttl = second
            };

            return request.ExecuteAsync(cancellationToken);
        }

        public Task<EtcdLease> GetLeaseAsync(long id, CancellationToken cancellationToken = default)
        {
            var host = Gateway.GetHost();
            var request = new GetLeaseRequest((r, t) => Transport.ExecuteGetLeaseAsync(r, t))
            {
                Host = host.Item1,
                Port = host.Item2,
                Id = id
            };

            return request.ExecuteAsync(cancellationToken);
        }

        public Task RevokeLeaseAsync(EtcdLease lease, CancellationToken cancellationToken = default)
        {
            var host = Gateway.GetHost();
            var request = new RevokeLeaseRequest((r, t) => Transport.ExecuteRevokeLeaseAsync(r, t))
            {
                Host = host.Item1,
                Port = host.Item2,
                EtcdLease = lease
            };

            return request.ExecuteAsync(cancellationToken);
        }

        public Task<long> TimeToLiveLeaseAsync(EtcdLease lease, CancellationToken cancellationToken = default)
        {
            var host = Gateway.GetHost();
            var request = new TimeToLiveLeaseRequest((r, t) => Transport.ExecuteTimeToLiveLeaseAsync(r, t))
            {
                Host = host.Item1,
                Port = host.Item2,
                EtcdLease = lease
            };

            return request.ExecuteAsync(cancellationToken);
        }
    }
}