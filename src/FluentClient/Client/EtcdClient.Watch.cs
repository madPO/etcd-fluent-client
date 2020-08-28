namespace FluentClient.Client
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Dawn;
    using Request;

    public partial class EtcdClient
    {
        public IWatchRequest Watch(EtcdKey key)
        {
            Guard.Argument(key).NotNull();
            Guard.Argument(Transport).NotNull();

            Func<IWatchRequest, CancellationToken, Task<EtcdWatcher>> fn = (r, c) => Transport.ExecuteWatchAsync(r, c);
            var request = new EtcdWatchRequest(fn)
            {
                Key = key.Name
            };
            
            FillHost(request);

            return request;
        }

        public Task<EtcdWatcher> WatchAsync(EtcdKey key, CancellationToken cancellationToken = default)
        {
            Guard.Argument(key).NotNull();
            
            cancellationToken.ThrowIfCancellationRequested();

            var request = Watch(key);

            return request.ExecuteAsync(cancellationToken);
        }
        
    }
}