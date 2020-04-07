namespace FluentClient.Request
{
    using System.Threading;
    using System.Threading.Tasks;
    using Client;

    public interface IDeleteRequest : IRequest
    {
        IDeleteRequest ToKey(EtcdKey key);

        IDeleteRequest Contains(EtcdKey key);

        Task ExecuteAsync(CancellationToken cancellationToken = default);
    }
}