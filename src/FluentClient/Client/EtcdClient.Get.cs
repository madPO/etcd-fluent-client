namespace FluentClient.Client
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Request;

    public partial class EtcdClient
    {
        //todo: mb create IQueryable realization?
        public Task<IReadOnlyCollection<byte[]>> GetAsync(EtcdKey key, CancellationToken cancellationToken = default)
        {
            var request = Get(key);

            return request.ExecuteAsync(cancellationToken);
        }

        public IGetRequest Get(EtcdKey key)
        {
            var host = _gateway.GetHost();
            var request = new GetRequest(_transport)
            {
                Key = key.Name,
                Host = host.Item1,
                Port = host.Item2
            };

            return request;
        }
    }
}