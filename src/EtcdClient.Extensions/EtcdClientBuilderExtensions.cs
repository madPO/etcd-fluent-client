namespace EtcdClient.Extensions
{
    using Core;
    using Core.HostResolver;
    using Dawn;

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