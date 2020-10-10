namespace EtcdClient.Core.Operations
{
    /// <summary>
    /// Etcd put operation
    /// </summary>
    public interface IEtcdPutOperation : IEtcdOperation
    {
        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }
        
        /// <summary>
        /// Value
        /// </summary>
        public byte[] Value { get; set; }
    }
}