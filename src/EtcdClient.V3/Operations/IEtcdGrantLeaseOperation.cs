namespace EtcdClient.V3.Operations
{
    using Core.Operations;

    /// <summary>
    /// Grant lease operation
    /// </summary>
    public interface IEtcdGrantLeaseOperation: IEtcdOperation<IEtcdLease>
    {
        /// <summary>
        /// Ttl
        /// </summary>
        int Ttl { get; set; }
    }
}