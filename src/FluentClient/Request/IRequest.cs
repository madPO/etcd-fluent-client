namespace FluentClient.Request
{
    public interface IRequest
    {
        /// <summary>
        /// Etcd host
        /// </summary>
        string Host { get; set; }
        
        /// <summary>
        /// Etcd host port
        /// </summary>
        int Port { get; set; }
        
        /// <summary>
        /// Key
        /// </summary>
        string Key { get; set; }
    }
}