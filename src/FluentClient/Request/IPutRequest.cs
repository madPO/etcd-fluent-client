namespace FluentClient.Request
{
    using System.Threading;
    using System.Threading.Tasks;
    using Client;

    /// <summary>
    /// Etcd put request
    /// </summary>
    public interface IPutRequest : IRequest
    {
        /// <summary>
        /// Value
        /// </summary>
        byte[] Value { get; }
        
        /// <summary>
        /// Lease
        /// </summary>
        ILease Lease { get; }
        
        /// <summary>
        /// Execute put request with lease
        /// </summary>
        Task WithLeaseAsync(ILease lease, CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute put request
        /// </summary>
        Task ExecuteAsync(CancellationToken cancellationToken = default);
    }
}