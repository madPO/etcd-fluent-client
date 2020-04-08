namespace FluentClient.Request
{
    using Client;

    /// <summary>
    /// Etcd put request
    /// </summary>
    public interface IPutRequest : IRequest, IEmptyRequest
    {
        /// <summary>
        /// Value
        /// </summary>
        byte[] Value { get; set; }
        
        /// <summary>
        /// Lease
        /// </summary>
        EtcdLease EtcdLease { get; set; }
    }
}