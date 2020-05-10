namespace FluentClient.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Dawn;
    using Request;

    public partial class EtcdClient
    {
        //todo: mb create IQueryable realization?
        public Task<IReadOnlyCollection<byte[]>> GetAsync(EtcdKey key, CancellationToken cancellationToken = default)
        {
            Guard.Argument(key).NotNull();
            
            var request = Get(key);

            return request.ExecuteAsync(cancellationToken);
        }

        public IGetRequest Get(EtcdKey key)
        {
            Guard.Argument(key).NotNull();
            Guard.Argument(Transport).NotNull();
            
            Func<IGetRequest, CancellationToken, Task<IReadOnlyCollection<byte[]>>> fn = (r, t) => Transport.ExecuteGetAsync(r, t);
            
            var request = new GetRequest(fn)
            {
                Key = key?.Name
            };

            FillHost(request);

            return request;
        }
    }
}