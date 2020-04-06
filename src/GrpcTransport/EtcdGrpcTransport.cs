namespace GrpcTransport
{
    using System.Threading;
    using System.Threading.Tasks;
    using Etcdserverpb;
    using FluentClient.Request;
    using FluentClient.Transport;
    using Google.Protobuf;
    using Grpc.Core;

    public class EtcdGrpcTransport : IEtcdTransport
    {
        public async Task ExecutePutAsync(IPutRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var put = new PutRequest
            {
                //todo: encoding
                Key = ByteString.CopyFromUtf8(request.Key),
                Value = ByteString.CopyFrom(request.Value)
            };

            if (request.Lease != null)
            {
                put.Lease = request.Lease.Id;
            }

            var client = new KV.KVClient(new Channel(request.Host, request.Port, ChannelCredentials.Insecure));
            var response = await client.PutAsync(put, cancellationToken: cancellationToken);
        }

        public void Dispose()
        {
        }
    }
}