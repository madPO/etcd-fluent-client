namespace EtcdClient.Core
{
    using Gateway;

    /// <summary>
    /// Etcd client factory
    /// </summary>
    public interface IEtcdClientBuilder
    {
        /// <summary>
        /// Create new <see cref="IEtcdClient"/>
        /// </summary>
        IEtcdClient Build();

        /// <summary>
        /// Register gateway method 
        /// </summary>
        void RegisterGateway(IEtcdHostResolver gateway);
        
        /// <summary>
        /// Register transport method
        /// </summary>
        void RegisterTransport(IEtcdTransport transport);
    }
}