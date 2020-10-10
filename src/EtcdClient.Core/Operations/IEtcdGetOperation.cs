namespace EtcdClient.Core.Operations
{
    /// <summary>
    /// Get operation
    /// </summary>
    public interface IEtcdGetOperation: IEtcdOperation<IEtcdValue[]>
    {
        /// <summary>
        /// Key
        /// </summary>
        string Key { get; }
    }
}