namespace FluentClient.Auth
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Auth method
    /// </summary>
    public interface IEtcdAuthMethod
    {
        /// <summary>
        /// Prepare etcd request and add auth 
        /// </summary>
        Task PrepareAsync(CancellationToken cancellationToken = default);
    }
}