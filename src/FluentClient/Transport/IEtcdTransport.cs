namespace FluentClient.Transport
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Client;
    using Request;

    /// <summary>
    /// Transport layer
    /// </summary>
    public interface IEtcdTransport : IDisposable
    {
        /// <summary>
        /// Execute put request
        /// </summary>
        Task ExecutePutAsync(IPutRequest request, CancellationToken cancellationToken = default);

        Task ExecuteDeleteAsync(IDeleteRequest deleteRequest, CancellationToken cancellationToken);
        
        Task<IReadOnlyCollection<byte[]>> ExecuteGetAsync(IGetRequest getRequest, CancellationToken cancellationToken);
        
        Task<EtcdLease> ExecuteGrantLeaseAsync(ICreateLeaseRequest createLeaseRequest, CancellationToken cancellationToken);
        
        Task<EtcdLease> ExecuteGetLeaseAsync(IGetLeaseRequest getLeaseRequest, CancellationToken cancellationToken);
        
        Task ExecuteRevokeLeaseAsync(IRevokeLeaseRequest revokeLeaseRequest, CancellationToken cancellationToken);
        
        Task<long> ExecuteTimeToLiveLeaseAsync(ITimeToLiveLeaseRequest timeToLiveLeaseRequest, CancellationToken cancellationToken);
    }
}