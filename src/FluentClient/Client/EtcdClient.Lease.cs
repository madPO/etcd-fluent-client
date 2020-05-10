namespace FluentClient.Client
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Dawn;
    using Request;

    public partial class EtcdClient
    {
        public Task<EtcdLease> GrantLeaseAsync(long second, CancellationToken cancellationToken = default)
        {
            Guard.Argument(second).Positive();
            Guard.Argument(Transport).NotNull();

            Func<ICreateLeaseRequest, CancellationToken, Task<EtcdLease>> fn = (r, t) =>
                Transport.ExecuteGrantLeaseAsync(r, t);
            var request = new CreateLeaseRequest(fn)
            {
                Ttl = second
            };
            
            FillHost(request);

            return request.ExecuteAsync(cancellationToken);
        }

        public Task RevokeLeaseAsync(EtcdLease lease, CancellationToken cancellationToken = default)
        {
            Guard.Argument(lease).NotNull();
            Guard.Argument(Transport).NotNull();    
            
            Func<IRevokeLeaseRequest, CancellationToken, Task> fn = (r, t) => Transport.ExecuteRevokeLeaseAsync(r, t);
            var request = new RevokeLeaseRequest(fn)
            {
                EtcdLease = lease
            };
            
            FillHost(request);

            return request.ExecuteAsync(cancellationToken);
        }

        public Task<EtcdLease> TimeToLiveLeaseAsync(EtcdLease lease, CancellationToken cancellationToken = default)
        {
            Guard.Argument(lease).NotNull();
            Guard.Argument(Transport).NotNull();

            Func<ITimeToLiveLeaseRequest, CancellationToken, Task<EtcdLease>> fn = (r, t) =>
                Transport.ExecuteTimeToLiveLeaseAsync(r, t);
            var request = new TimeToLiveLeaseRequest(fn)
            {
                EtcdLease = lease
            };

            FillHost(request);

            return request.ExecuteAsync(cancellationToken);
        }
    }
}