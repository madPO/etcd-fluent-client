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
            var host = Gateway.GetHost();
            var request = new DeleteRequest((r, t) => Transport.ExecuteDeleteAsync(r, t))
            {
                //todo: change Item1 to Host, and Item2 to Port
                Host = host.Item1,
                Port = host.Item2,
                Key = key?.Name
            };

            return request;
        }
    }
}