namespace FluentClient.Client
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface ILease
    {
        long Id { get; }

        Task RevokeAsync(CancellationToken cancellationToken = default);

        Task KeepAliveAsync(long seconds, CancellationToken cancellationToken = default);

        Task<long> TimeToLiveAsync(CancellationToken cancellationToken = default);
    }
}