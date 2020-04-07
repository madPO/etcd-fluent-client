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
        /// <summary>
        /// Put value
        /// </summary>
        Task PutAsync(EtcdKey key, byte[] value, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Create put request
        /// </summary>
        IPutRequest Put(EtcdKey key, byte[] value);

        /// <summary>
        /// Add auth
        /// </summary>
        IEtcdClient UseAuth(IEtcdAuthMethod authMethod);

        /// <summary>
        /// Add transport
        /// </summary>
        IEtcdClient UseTransport(IEtcdTransport transport);
        
        /// <summary>
        /// Add gateway
        /// </summary>
        IEtcdClient UseGateway(IEtcdGateway gateway);

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
        Task<ILease> GrantLeaseAsync(long second, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get lease
        /// </summary>
        Task<ILease> GetLeaseAsync(long id, CancellationToken cancellationToken = default);
    }
}