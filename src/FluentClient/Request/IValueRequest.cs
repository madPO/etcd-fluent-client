namespace FluentClient.Request
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IValueRequest<TValue>
    {
        Task<TValue> ExecuteAsync(CancellationToken cancellationToken = default);
    }
}