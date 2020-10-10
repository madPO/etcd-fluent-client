namespace EtcdClient.Core
{
    /// <summary>
    /// Value from etcd
    /// </summary>
    public interface IEtcdValue
    {
        /// <summary>
        /// Key
        /// </summary>
        string Key { get; }
        
        /// <summary>
        /// Value
        /// </summary>
        byte[] Value { get; }
    }
}