namespace EtcdClient.V3
{
    using Core;
    using Operations;

    /// <summary>
    /// Etcd client v3
    /// </summary>
    public interface IEtcdClientV3 : IEtcdClient
    {
        /// <summary>
        /// Get lease for key
        /// </summary>
        IEtcdGrantLeaseOperation GrantLease(int ttl);
    }
}