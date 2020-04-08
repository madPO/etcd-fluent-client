namespace FluentClient.Extensions
{
    using System.Threading;
    using System.Threading.Tasks;
    using Client;
    using Request;

    public static class PutRequestExtension
    {
        public static async Task WithLeaseAsync(this IPutRequest request, EtcdLease etcdLease, CancellationToken cancellationToken = default)
        {
            request.EtcdLease = etcdLease;

            await request.ExecuteAsync(cancellationToken);
        }
        
    }
}