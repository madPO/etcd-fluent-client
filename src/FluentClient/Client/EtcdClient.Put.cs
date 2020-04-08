namespace FluentClient.Client
{
    using System.Threading;
    using System.Threading.Tasks;
    using Request;

    public partial class EtcdClient
    {
        public IPutRequest Put(EtcdKey key, byte[] value)
        {
            var request = new EtcdPutRequest((r, c) => Transport.ExecutePutAsync(r, c))
            {
                Key = key.Name,
                Value = value
            };
            
            FillHost(request);

            return request;
        }
        
        public Task PutAsync(EtcdKey key, byte[] value, CancellationToken cancellationToken = default)
        {
            var request = Put(key, value);
            return request.ExecuteAsync(cancellationToken);
        }
    }
}