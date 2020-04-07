namespace FluentClient.Request
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Client;

    public interface IGetRequest : IRequest
    {
        /// <summary>
        /// From <see cref="Key"/> to <see cref="key"/>
        /// </summary>
        IGetRequest ToKey(EtcdKey key);

        /// <summary>
        /// Where key name contains <see cref="key"/>
        /// </summary>
        IGetRequest Contains(EtcdKey key);

        /// <summary>
        /// Limit
        /// </summary>
        IGetRequest Limit(int limit);

        /// <summary>
        /// Key value version
        /// </summary>
        IGetRequest Revision(int version);

        /// <summary>
        /// Execute request
        /// </summary>
        Task<IReadOnlyCollection<byte[]>> ExecuteAsync(CancellationToken cancellationToken = default);
    }
}