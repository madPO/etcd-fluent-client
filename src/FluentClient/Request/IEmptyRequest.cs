namespace FluentClient.Request
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IEmptyRequest
    {
        Task ExecuteAsync(CancellationToken cancellationToken = default);
    }
}