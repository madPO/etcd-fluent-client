namespace UnitTest.Client
{
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentClient.Client;
    using FluentClient.Gateway;
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

        [Fact]
        public async Task PutExample()
        {
            var client = new EtcdClient()
                .UseTransport(new EtcdGrpcTransport())
                .UseGateway(new RoundRobinGateway(new [] { "http://localhost:2379" }));
            
            using (client)
            {
                await client.PutAsync("test", Encoding.UTF8.GetBytes("test"));
            }
        }
    }
}