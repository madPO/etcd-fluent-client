namespace EtcdClient.Core
{
    using Operations;

    /// <summary>
    /// Etcd client
    /// </summary>
    public interface IEtcdClient
    {
        /// <summary>
        /// Put key\value pair in to Etcd
        /// </summary>
        IEtcdPutOperation Put(string key, byte[] value);

        /// <summary>
        /// Get value from Etcd
        /// </summary>
        IEtcdGetOperation Get(string key);

        /// <summary>
        /// Remove key\value from Etcd
        /// </summary>
        IEtcdDeleteOperation Delete(string key);
    }
}