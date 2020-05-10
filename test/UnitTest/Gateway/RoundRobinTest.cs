namespace UnitTest.Gateway
{
    using System;
    using FluentClient.Gateway;
    using Xunit;

    public class RoundRobinTest
    {
        [Fact]
        public void CreateGatewayTest()
        {
            const string uri = "172.0.0.1:2379";
            
            var gateway = new RoundRobinGateway(new [] { uri });
        }

        [Fact]
        public void CreateGatewayWithNullTest()
        {
           Assert.Throws<ArgumentNullException>(() => new RoundRobinGateway(null));
        }
        
        [Fact]
        public void CreateGatewayWithNullElementTest()
        {
            Assert.Throws<ArgumentNullException>(() => new RoundRobinGateway(new string[]
            {
                "172.0.0.1:2379",
                null
            }));
        }

        [Fact]
        public void GetHostTest()
        {
            const string uri = "172.0.0.1:2379";
            
            var gateway = new RoundRobinGateway(new [] { uri });
            
            Assert.NotNull(gateway.GetHost());
        }

        [Fact]
        public void UriNotParsed()
        {
            const string uri = "http://172.0.0.1:2379";
            
            Assert.Throws<Exception>(() => new RoundRobinGateway(new [] { uri }));
        }
        
        [Fact]
        public void UriNotParsed2()
        {
            const string uri = "172.0.0.1";
            
            Assert.Throws<Exception>(() => new RoundRobinGateway(new [] { uri }));
        }

        [Fact]
        public void RoundRobinWorkTest()
        {
            var gateway = new RoundRobinGateway(new []
            {
                "192.168.0.2:2379",
                "192.168.0.3:2379",
            });

            var host1 = gateway.GetHost();
            var host2 = gateway.GetHost();
            
            Assert.NotEqual(host1, host2);
        }
        
        [Fact]
        public void RoundRobinCycleTest()
        {
            var gateway = new RoundRobinGateway(new []
            {
                "192.168.0.2:2379",
                "192.168.0.3:2379",
            });

            var host1 = gateway.GetHost();
            var host2 = gateway.GetHost();
            var host3 = gateway.GetHost();
            
            Assert.NotEqual(host1, host2);
            Assert.Equal(host1, host3);
        }
    }
}