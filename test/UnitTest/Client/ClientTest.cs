namespace UnitTest.Client
{
    using System.Threading;
    using FluentClient.Auth;
    using FluentClient.Client;
    using Xunit;

    public class ClientTest
    {
        [Fact]
        public void Create()
        {
            var client = new EtcdClient(new [] { "localhost:2379" });
            
            using (client)
            {
                Thread.Sleep(10);
            }
        }

        [Fact]
        public void UseUserAuth()
        {
            var client = new EtcdClient(new [] { "localhost:2379" })
                .UseAuth(new UserLoginAuth("root", "root"));
            
            using (client)
            {
                Thread.Sleep(10);
            }
        }
    }
}