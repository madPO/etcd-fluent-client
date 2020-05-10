namespace FluentClient.Client
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Dawn;
    using Request;

    public partial class EtcdClient
    {
        public Task DeleteAsync(EtcdKey key, CancellationToken cancellationToken = default)
        {
            Guard.Argument(key).NotNull();
            
            var request = Delete(key);

            return request.ExecuteAsync(cancellationToken);
        }

        public IDeleteRequest Delete(EtcdKey key = null)
        {
            Guard.Argument(Transport).NotNull();
            
            Func<IDeleteRequest, CancellationToken, Task> fn = (r, t) => Transport.ExecuteDeleteAsync(r, t);
            
            var request = new DeleteRequest(fn)
            {
                Key = key?.Name
            };

            FillHost(request);

            return request;
        }
    }
}