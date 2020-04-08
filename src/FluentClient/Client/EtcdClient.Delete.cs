namespace FluentClient.Client
{
    using System.Threading;
    using System.Threading.Tasks;
    using Request;

    public partial class EtcdClient
    {
        public Task DeleteAsync(EtcdKey key, CancellationToken cancellationToken = default)
        {
            var request = Delete(key);

            return request.ExecuteAsync(cancellationToken);
        }

        public IDeleteRequest Delete(EtcdKey key = null)
        {
            var request = new DeleteRequest((r, t) => Transport.ExecuteDeleteAsync(r, t))
            {
                Key = key?.Name
            };

            FillHost(request);

            return request;
        }
    }
}