namespace FluentClient.Request
{
    using System.Threading;
    using System.Threading.Tasks;
    using Client;

    public interface ILeaseRequest : IRequest
    {
        Task<ILease> GrantLeaseAsync(CancellationToken cancellationToken = default);

        Task RevokeAsync(CancellationToken cancellationToken = default);

        Task KeepAliveAsync(CancellationToken cancellationToken = default);
        
        Task<long> TimeToLiveAsync(CancellationToken cancellationToken = default);
    }
}