namespace FluentClient.Extensions
{
    using Auth;
    using Client;
    using Gateway;
    using Transport;

    public static class EtcdClientExtension
    {
        public static IEtcdClient UseAuth(this IEtcdClient client, IEtcdAuthMethod authMethod)
       {
           client.AuthMethod = authMethod;
           
           return client;
       }

       public static IEtcdClient UseTransport(this IEtcdClient client, IEtcdTransport transport)
       {
           client.Transport = transport;

           return client;
       }

       public static IEtcdClient UseGateway(this IEtcdClient client, IEtcdGateway gateway)
       {
           client.Gateway = gateway;

           return client;
       }
    }
}