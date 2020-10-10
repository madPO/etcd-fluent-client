namespace EtcdClient.Core
{
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
        void RegisterGateway(IEtcdGateway gateway);
        
        /// <summary>
        /// Register transport method
        /// </summary>
        void RegisterTransport(IEtcdTransport transport);
    }
}