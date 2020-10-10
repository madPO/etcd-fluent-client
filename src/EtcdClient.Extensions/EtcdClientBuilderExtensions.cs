namespace EtcdClient.Extensions
{
    using Core;
    using Dawn;

    /// <summary>
    /// Extensions for <see cref="IEtcdClientBuilder"/>
    /// </summary>
    public static class EtcdClientBuilderExtensions
    {
        public static IEtcdClientBuilder WithRoundRobinGateway(this IEtcdClientBuilder builder, params string[] servers)
        {
            Guard.Argument(builder).NotNull();

            builder.RegisterGateway();
            
            return builder;
        }
        
    }
}