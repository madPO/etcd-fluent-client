namespace EtcdClient.Core.Gateway
{
    using System.Diagnostics.CodeAnalysis;
    using HostResolver;

    /// <summary>
    /// Host resolver for <see cref="IEtcdClient"/>
    /// </summary>
    public interface IEtcdHostResolver
    {
        /// <summary>
        /// Return a host
        /// </summary>
        [return: NotNull]
        EtcdHost GetHost();
    }
}