namespace EtcdClient.Core.Operations
{
    /// <summary>
    /// Remove key operation
    /// </summary>
    public interface IEtcdDeleteOperation: IEtcdOperation
    {
        /// <summary>
        /// Key
        /// </summary>
        string Key { get; }
    }
}