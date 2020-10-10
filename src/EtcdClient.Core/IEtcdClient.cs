namespace EtcdClient.Core
{
    /// <summary>
    /// Etcd client
    /// </summary>
    public interface IEtcdClient
    {
        /// <summary>
        /// Put key\value pair in to Etcd
        /// </summary>
        IEtcdPutBuilder Put(string key, byte[] value);

        /// <summary>
        /// Get value from Etcd
        /// </summary>
        IEtcdGetBuilder Get(string key);

        /// <summary>
        /// Remove key\value from Etcd
        /// </summary>
        IEtcdDeleteBuilder Delete(string key);

        /// <summary>
        /// Get lease for key
        /// </summary>
        IEtcdGrantLeaseBuilder GrantLease(int ttl);
    }
}