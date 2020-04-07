namespace FluentClient.Request
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IRequest
    {
        /// <summary>
        /// Etcd host
        /// </summary>
        string Host { get; }
        
        /// <summary>
        /// Etcd host port
        /// </summary>
        int Port { get; }
        
        /// <summary>
        /// Key
        /// </summary>
        string Key { get; }
    }
}