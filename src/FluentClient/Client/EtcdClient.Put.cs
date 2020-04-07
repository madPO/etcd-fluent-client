namespace FluentClient.Client
{
    using System.Threading;
    using System.Threading.Tasks;
    using Request;

    public partial class EtcdClient
    {
        public IPutRequest Put(IEtcdKey key, byte[] value)
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
        
        public Task PutAsync(IEtcdKey key, byte[] value, CancellationToken cancellationToken = default)
        {
            var request = Put(key, value);
            return request.ExecuteAsync(cancellationToken);
        }
    }
}