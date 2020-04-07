namespace FluentClient.Client
{
    using System.Threading;
    using System.Threading.Tasks;

    public partial class EtcdClient
    {
        public Task<ILease> GrantLeaseAsync(long second, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<ILease> GetLeaseAsync(long id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}