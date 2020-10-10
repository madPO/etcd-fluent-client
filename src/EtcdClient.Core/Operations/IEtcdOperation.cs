namespace EtcdClient.Core.Operations
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Default operation builder
    /// </summary>
    public interface IEtcdOperation
    {
        Task ExecuteAsync(CancellationToken token = default);
    }
    
    /// <summary>
    /// Default operation builder
    /// </summary>
    public interface IEtcdOperation<T>
    {
        Task<T> ExecuteAsync(CancellationToken token = default);
    }
}