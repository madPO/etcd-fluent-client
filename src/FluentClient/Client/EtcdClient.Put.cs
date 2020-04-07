namespace FluentClient.Client
{
    using System.Threading;
    using System.Threading.Tasks;
    using Request;

    public partial class EtcdClient
    {
        public IPutRequest Put(EtcdKey key, byte[] value)
        {
            var host = _gateway.GetHost();
            var request = new EtcdPutRequest(_transport)
            {
                Host = host.Item1,
                Key = key.Name,
                Port = host.Item2,
                Value = value
            };

            return request;
        }
        
        public Task PutAsync(EtcdKey key, byte[] value, CancellationToken cancellationToken = default)
        {
            var request = Put(key, value);
            return request.ExecuteAsync(cancellationToken);
        }
    }
}