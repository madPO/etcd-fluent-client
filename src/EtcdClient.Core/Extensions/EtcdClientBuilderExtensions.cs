namespace EtcdClient.Core.Extensions
{
    using Core;
    using Dawn;
    using HostResolver;

    /// <summary>
    /// Extensions for <see cref="IEtcdClientBuilder"/>
    /// </summary>
    public static class EtcdClientBuilderExtensions
    {
        public static IEtcdClientBuilder WithRoundRobinGateway(this IEtcdClientBuilder builder, params string[] host)
        {
            Guard.Argument(builder).NotNull();

            builder.RegisterGateway(new RoundRobinHostResolver(host));
            
            return builder;
        }
        
    }
}