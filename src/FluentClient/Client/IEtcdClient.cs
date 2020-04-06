namespace FluentClient.Client
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Auth;
    using Request;

    /// <summary>
    /// Etcd fluent client
    /// </summary>
    public interface IEtcdClient : IDisposable
    {
        /// <summary>
        /// Put value
        /// </summary>
        Task PutAsync(IEtcdKey key, byte[] value, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Create put request, without execute
        /// </summary>
        IPutRequest Put(IEtcdKey key, byte[] value);

        /// <summary>
        /// Add auth
        /// </summary>
        IEtcdClient UseAuth(IEtcdAuthMethod authMethod);
    }
}