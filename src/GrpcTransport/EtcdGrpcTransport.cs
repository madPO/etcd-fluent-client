namespace GrpcTransport
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
                Key = ByteString.CopyFromUtf8(request.Key),
                Value = ByteString.CopyFrom(request.Value)
            };

            if (request.EtcdLease != null)
            {
                put.Lease = request.EtcdLease.Id;
            }

            var client = EtcdTransportClientFactory.GetKvClient(request.Host, request.Port);
            var response = await client.PutAsync(put, cancellationToken: cancellationToken);
        }

        public async Task ExecuteDeleteAsync(IDeleteRequest request, CancellationToken cancellationToken)
        {
            var headers = new Metadata();
            var delete = new DeleteRangeRequest
            {
                Key = ByteString.CopyFromUtf8(request.Key.Name)
            };

            if (request.ToKey != null)
            {
                delete.RangeEnd = ByteString.CopyFromUtf8(request.ToKey.Name);
            }

            if (request.ContainsKey != null)
            {
                headers.Add("prev-kv", request.ContainsKey.Name);
            }
            
            var client = EtcdTransportClientFactory.GetKvClient(request.Host, request.Port);;
            var response = await client.DeleteRangeAsync(delete, headers, cancellationToken: cancellationToken);
        }

        public async Task<IReadOnlyCollection<byte[]>> ExecuteGetAsync(IGetRequest request, CancellationToken cancellationToken)
        {
            var headers = new Metadata();
            var get = new RangeRequest
            {
                Key = ByteString.CopyFromUtf8(request.Key.Name)
            };

            if (request.Limit.HasValue)
            {
                get.Limit = request.Limit.Value;
            }

            if (request.Version.HasValue)
            {
                get.Revision = request.Version.Value;
            }

            if (request.ContainsKey != null)
            {
                headers.Add("prev-kv", request.ContainsKey.Name);
            }

            if (request.ToKey != null)
            {
                get.RangeEnd = ByteString.CopyFromUtf8(request.ToKey.Name);
            }
            
            
            var client = EtcdTransportClientFactory.GetKvClient(request.Host, request.Port);;
            var response = await client.RangeAsync(get, headers, cancellationToken: cancellationToken);

            return response.Kvs.Select(x => x.Value.ToByteArray()).ToArray();
        }

        public async Task<EtcdLease> ExecuteGrantLeaseAsync(ICreateLeaseRequest request, CancellationToken cancellationToken)
        {
            var grant = new LeaseGrantRequest
            {
                TTL = request.Ttl
            };
            
            var client = EtcdTransportClientFactory.GetLeaseClient(request.Host, request.Port);
            var response = await client.LeaseGrantAsync(grant, cancellationToken: cancellationToken);

            if (response.Error != null)
            {
                throw new Exception(response.Error);
            }
            
            return new EtcdLease(response.ID, requestedTtl: response.TTL);
        }

        public async Task ExecuteRevokeLeaseAsync(IRevokeLeaseRequest request, CancellationToken cancellationToken)
        {
            var revoke = new LeaseRevokeRequest
            {
                ID = request.EtcdLease.Id
            };
            
            var client = EtcdTransportClientFactory.GetLeaseClient(request.Host, request.Port);
            var response = await client.LeaseRevokeAsync(revoke, cancellationToken:cancellationToken);
        }

        public async Task<EtcdLease> ExecuteTimeToLiveLeaseAsync(ITimeToLiveLeaseRequest request, CancellationToken cancellationToken)
        {
            var timeToLive = new LeaseTimeToLiveRequest
            {
                ID = request.EtcdLease.Id
            };
            
            var client = EtcdTransportClientFactory.GetLeaseClient(request.Host, request.Port);
            var response = await client.LeaseTimeToLiveAsync(timeToLive, cancellationToken:cancellationToken);

            return new EtcdLease(response.ID, response.TTL, response.GrantedTTL);
        }

        public void Dispose()
        {
        }
    }
}