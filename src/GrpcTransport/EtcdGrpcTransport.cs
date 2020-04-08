namespace GrpcTransport
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Etcdserverpb;
    using FluentClient.Client;
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

            if (request.EtcdLease != null)
            {
                put.Lease = request.EtcdLease.Id;
            }

            var client = new KV.KVClient(new Channel(request.Host, request.Port, ChannelCredentials.Insecure));
            var response = await client.PutAsync(put, cancellationToken: cancellationToken);
        }

        public Task ExecuteDeleteAsync(IDeleteRequest deleteRequest, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyCollection<byte[]>> ExecuteGetAsync(IGetRequest getRequest, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<EtcdLease> ExecuteGrantLeaseAsync(ICreateLeaseRequest createLeaseRequest, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<EtcdLease> ExecuteGetLeaseAsync(IGetLeaseRequest getLeaseRequest, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task ExecuteRevokeLeaseAsync(IRevokeLeaseRequest revokeLeaseRequest, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<long> ExecuteTimeToLiveLeaseAsync(ITimeToLiveLeaseRequest timeToLiveLeaseRequest, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}