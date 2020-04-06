namespace UnitTest.Client
{
    using System.Threading;
    using Xunit;

    public class ClientTest
    {
        [Fact]
        public void Create()
        {
            var client = new EtcdClient("loclahost:2379");
            using (client)
            {
                Thread.Sleep(10);
            }
        }
    }
}