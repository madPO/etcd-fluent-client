namespace FluentClient.Client
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Dawn;
    using Request;

    public partial class EtcdClient
    {
        public IPutRequest Put(EtcdKey key, byte[] value)
        {
            Guard.Argument(key).NotNull();
            Guard.Argument(value).NotNull().NotEmpty();
            Guard.Argument(Transport).NotNull();

            Func<IPutRequest, CancellationToken, Task> fn = (r, c) => Transport.ExecutePutAsync(r, c);
            var request = new EtcdPutRequest(fn)
            {
                Key = key.Name,
                Value = value
            };
            
            FillHost(request);

            return request;
        }
        
        public Task PutAsync(EtcdKey key, byte[] value, CancellationToken cancellationToken = default)
        {
            Guard.Argument(key).NotNull();
            Guard.Argument(value).NotNull().NotEmpty();
            
            var request = Put(key, value);
            return request.ExecuteAsync(cancellationToken);
        }
    }
}