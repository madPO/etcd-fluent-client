namespace FluentClient.Client
{
    using System;
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
        /// Create put request, without execute
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
    }
}