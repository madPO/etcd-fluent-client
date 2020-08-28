namespace FluentClient.Request
{
    using System.Threading;
    using System.Threading.Tasks;
    using Client;

    public interface IWatchRequest : IRequest
    {
        string Key { get; set; }

        Task<EtcdWatcher> ExecuteAsync(CancellationToken cancellationToken = default);
    }
}