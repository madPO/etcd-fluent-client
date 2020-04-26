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
            var request = new GetRequest((r, t) => Transport.ExecuteGetAsync(r, t))
            {
                Key = key?.Name
            };

            FillHost(request);

            return request;
        }
    }
}