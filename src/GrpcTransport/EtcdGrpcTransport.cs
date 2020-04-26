namespace GrpcTransport
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Dawn;
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
            Guard.Argument(request)
                .NotNull()
                .Member(x => x.Key, x => x.NotNull().NotEmpty())
                .Member(x => x.Value, x => x.NotNull().NotEmpty())
                .Member(x => x.Host, x => x.NotNull().NotEmpty())
                .Member(x => x.Port, x => x.Positive());
            
            cancellationToken.ThrowIfCancellationRequested();

            var put = new PutRequest
            {
                Key = ByteString.CopyFromUtf8(request.Key),
                Value = ByteString.CopyFrom(request.Value)
            };

            if (request.EtcdLease != null)
            {
                Guard.Argument(request.EtcdLease.Id).Positive();
                
                put.Lease = request.EtcdLease.Id;
            }

            var client = EtcdTransportClientFactory.GetKvClient(request.Host, request.Port);

            Guard.Argument(client).NotNull();
            
            var response = await client.PutAsync(put, cancellationToken: cancellationToken);
        }

        public async Task ExecuteDeleteAsync(IDeleteRequest request, CancellationToken cancellationToken)
        {
            Guard.Argument(request)
                .NotNull()
                .Member(x => x.Key, x => x.NotNull()
                    .Member(s => s.Name, s => s.NotNull().NotEmpty()))
                .Member(x => x.Host, x => x.NotNull().NotEmpty())
                .Member(x => x.Port, x => x.Positive());
                
            
            var headers = new Metadata();
            var delete = new DeleteRangeRequest
            {
                Key = ByteString.CopyFromUtf8(request.Key.Name)
            };

            if (request.ToKey != null)
            {
                Guard.Argument(request.ToKey.Name).NotNull().NotEmpty();
                
                delete.RangeEnd = ByteString.CopyFromUtf8(request.ToKey.Name);
            }

            //todo: not work
            if (request.ContainsKey != null)
            {
                Guard.Argument(request.ContainsKey.Name).NotNull().NotEmpty();
                
                headers.Add("prev-kv", request.ContainsKey.Name);
            }
            
            var client = EtcdTransportClientFactory.GetKvClient(request.Host, request.Port);

            Guard.Argument(client).NotNull();
            
            var response = await client.DeleteRangeAsync(delete, headers, cancellationToken: cancellationToken);
        }

        public async Task<IReadOnlyCollection<byte[]>> ExecuteGetAsync(IGetRequest request, CancellationToken cancellationToken)
        {
            Guard.Argument(request)
                .NotNull()
                .Member(x => x.Host, x => x.NotNull().NotEmpty())
                .Member(x => x.Port, x => x.Positive());
                
                
            
            var headers = new Metadata();
            var get = new RangeRequest();

            if (request.Key?.Name != null)
                get.Key = ByteString.CopyFromUtf8(request.Key.Name);

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
                //todo: this not work
                // get all keys and filter
                // and then request on all key
                headers.Add("prev-kv", request.ContainsKey.Name);
            }

            if (request.ToKey != null)
            {
                Guard.Argument(request.ToKey.Name).NotNull().NotEmpty();
                
                get.RangeEnd = ByteString.CopyFromUtf8(request.ToKey.Name);
            }
            
            var client = EtcdTransportClientFactory.GetKvClient(request.Host, request.Port);

            Guard.Argument(client).NotNull();
            
            var response = await client.RangeAsync(get, headers, cancellationToken: cancellationToken);

            Guard.Argument(request).NotNull();

            //todo: return all result with key and version
            return response.Kvs.Select(x => x.Value.ToByteArray()).ToArray();
        }

        public async Task<EtcdLease> ExecuteGrantLeaseAsync(ICreateLeaseRequest request, CancellationToken cancellationToken)
        {
            Guard.Argument(request)
                .NotNull()
                .Member(x => x.Ttl, x => x.Positive())
                .Member(x => x.Host, x => x.NotNull().NotEmpty())
                .Member(x => x.Port, x => x.Positive());
            
            var grant = new LeaseGrantRequest
            {
                TTL = request.Ttl
            };
            
            var client = EtcdTransportClientFactory.GetLeaseClient(request.Host, request.Port);

            Guard.Argument(client).NotNull();
            
            var response = await client.LeaseGrantAsync(grant, cancellationToken: cancellationToken);

            Guard.Argument(response).NotNull();

            if (response.Error != null && !string.IsNullOrEmpty(response.Error))
            {
                throw new Exception(response.Error);
            }
            
            return new EtcdLease(response.ID, requestedTtl: response.TTL);
        }

        public async Task ExecuteRevokeLeaseAsync(IRevokeLeaseRequest request, CancellationToken cancellationToken)
        {
            Guard.Argument(request)
                .NotNull()
                .Member(x => x.EtcdLease, x => x.NotNull()
                    .Member(s => s.Id, s => s.Positive()))
                .Member(x => x.Host, x => x.NotNull().NotEmpty())
                .Member(x => x.Port, x => x.Positive());
            
            var revoke = new LeaseRevokeRequest
            {
                ID = request.EtcdLease.Id
            };
            
            var client = EtcdTransportClientFactory.GetLeaseClient(request.Host, request.Port);

            Guard.Argument(client).NotNull();
            
            var response = await client.LeaseRevokeAsync(revoke, cancellationToken:cancellationToken);
        }

        public async Task<EtcdLease> ExecuteTimeToLiveLeaseAsync(ITimeToLiveLeaseRequest request, CancellationToken cancellationToken)
        {
            Guard.Argument(request)
                .NotNull()
                .Member(x => x.EtcdLease, x => x.NotNull()
                    .Member(s => s.Id, s => s.Positive()))
                .Member(x => x.Host, x => x.NotNull().NotEmpty())
                .Member(x => x.Port, x => x.Positive());
            
            var timeToLive = new LeaseTimeToLiveRequest
            {
                ID = request.EtcdLease.Id
            };
            
            var client = EtcdTransportClientFactory.GetLeaseClient(request.Host, request.Port);

            Guard.Argument(client).NotNull();
            
            var response = await client.LeaseTimeToLiveAsync(timeToLive, cancellationToken:cancellationToken);

            Guard.Argument(response)
                .NotNull()
                .Member(x => x.ID, x => x.Positive())
                .Member(x => x.TTL, x => x.Positive())
                .Member(x => x.GrantedTTL, x => x.Positive());

            return new EtcdLease(response.ID, response.TTL, response.GrantedTTL);
        }

        public void Dispose()
        {
        }
    }
}