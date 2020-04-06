namespace UnitTest.Client
{
    using System.Threading;
    using FluentClient.Auth;
    using FluentClient.Client;
    using GrpcTransport;
    using GrpcTransport.Implementation;
    using Xunit;

    public class ClientTest
    {
        [Fact]
        public void Create()
        {
            var client = new EtcdClient();
            
            using (client)
            {
                Thread.Sleep(10);
            }
        }

        [Fact]
        public void UseUserAuth()
        {
            var client = new EtcdClient()
                .UseAuth(new UserLoginAuth("root", "root"));
            
            using (client)
            {
                Thread.Sleep(10);
            }
        }
        
        [Fact]
        public void UseGrpcTransport()
        {
            var client = new EtcdClient()
                .UseTransport(new EtcdGrpcTransport());
            
            using (client)
            {
                Thread.Sleep(10);
            }
        }
    }
}