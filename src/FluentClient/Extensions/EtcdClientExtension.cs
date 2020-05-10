namespace FluentClient.Extensions
{
    using Auth;
    using Client;
    using Dawn;
    using Gateway;
    using Transport;

    public static class EtcdClientExtension
    {
        public static IEtcdClient UseAuth(this IEtcdClient client, IEtcdAuthMethod authMethod)
        {
            Guard.Argument(client).NotNull();
            Guard.Argument(authMethod).NotNull();
           
           client.AuthMethod = authMethod;
           
           return client;
       }

       public static IEtcdClient UseTransport(this IEtcdClient client, IEtcdTransport transport)
       {
           Guard.Argument(client).NotNull();
           Guard.Argument(transport).NotNull();
           
           client.Transport = transport;

           return client;
       }

       public static IEtcdClient UseGateway(this IEtcdClient client, IEtcdGateway gateway)
       {
           Guard.Argument(client).NotNull();
           Guard.Argument(gateway).NotNull();
           
           client.Gateway = gateway;

           return client;
       }
    }
}