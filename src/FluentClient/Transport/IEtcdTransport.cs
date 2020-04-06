namespace FluentClient.Transport
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
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
    }
}