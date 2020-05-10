namespace FluentClient.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Auth;
    using Gateway;
    using Request;
    using Transport;

    /// <summary>
    /// Etcd fluent client
    /// </summary>
    public interface IEtcdClient : IDisposable
    {
        IEtcdAuthMethod AuthMethod { get; set; }
        
        IEtcdTransport Transport { get; set;  }
        
        IEtcdGateway Gateway { get; set; }
        
        bool IsActive { get; }
        
        /// <summary>
        /// Put value
        /// </summary>
        Task PutAsync(EtcdKey key, byte[] value, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create put request
        /// </summary>
        IPutRequest Put(EtcdKey key, byte[] value);

        /// <summary>
        /// Get value by key
        /// </summary>
        Task<IReadOnlyCollection<byte[]>> GetAsync(EtcdKey key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create get request
        /// </summary>
        IGetRequest Get(EtcdKey key = null);

        /// <summary>
        /// Delete key
        /// </summary>
        Task DeleteAsync(EtcdKey key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create delete request
        /// </summary>
        IDeleteRequest Delete(EtcdKey key = null);

        /// <summary>
        /// Create lease
        /// </summary>
        Task<EtcdLease> GrantLeaseAsync(long second, CancellationToken cancellationToken = default);

        Task RevokeLeaseAsync(EtcdLease lease, CancellationToken cancellationToken = default);
        
        Task<EtcdLease> TimeToLiveLeaseAsync(EtcdLease lease, CancellationToken cancellationToken = default);
    }
}