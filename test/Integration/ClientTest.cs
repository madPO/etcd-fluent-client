namespace Integration
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoFixture.Xunit2;
    using Xunit;
    using FluentClient.Client;
    using FluentClient.Extensions;
    using FluentClient.Gateway;
    using GrpcTransport;

    public class ClientTest : IDisposable
    {
        private const string ServerUri = "127.0.0.1:2379";

        private readonly EtcdServer _server;

        public ClientTest()
        {
            _server = new EtcdServer();
            _server.Run();
        }
        
        [Fact]
        public async Task CreateConnectionTest()
        {
            await _server.WaitLaunchEnding();
            var client = new EtcdClient()
                .UseTransport(new EtcdGrpcTransport())
                .UseGateway(new RoundRobinGateway(new [] { ServerUri }));

            using (client)
            {
                
            }
        }
        
        [Theory, AutoData]
        public async Task PutRequestTest(string key, byte[] value)
        {
            await _server.WaitLaunchEnding();
            var client = new EtcdClient()
                .UseTransport(new EtcdGrpcTransport())
                .UseGateway(new RoundRobinGateway(new [] { ServerUri }));

            using (client)
            {
                await client.PutAsync(key, value);
            }
        }
        
        [Theory, AutoData]
        public async Task LeaseRequestTest(int ttl)
        {
            await _server.WaitLaunchEnding();
            var client = new EtcdClient()
                .UseTransport(new EtcdGrpcTransport())
                .UseGateway(new RoundRobinGateway(new [] { ServerUri }));

            using (client)
            {
                var lease = await client.GrantLeaseAsync(ttl);
                
                Assert.NotEqual(0, lease.Id);
                Assert.Equal(ttl, lease.RequestedTtl);
            }
        }
        
        [Theory, AutoData]
        public async Task GetRequestTest(string key, byte[] value)
        {
            await _server.WaitLaunchEnding();
            var client = new EtcdClient()
                .UseTransport(new EtcdGrpcTransport())
                .UseGateway(new RoundRobinGateway(new [] { ServerUri }));

            using (client)
            {
                await client.PutAsync(key, value);

                var result = await client.GetAsync(key);
                
                Assert.Equal(value, result.First());
            }
        }
        
        [Theory, AutoData]
        public async Task DeleteRequestTest(string key, byte[] value)
        {
            await _server.WaitLaunchEnding();
            var client = new EtcdClient()
                .UseTransport(new EtcdGrpcTransport())
                .UseGateway(new RoundRobinGateway(new [] { ServerUri }));

            using (client)
            {
                await client.PutAsync(key, value);

                await client.DeleteAsync(key);
            }
        }
        
        [Theory, AutoData]
        public async Task PutRequestWithLeaseRequestTest(string key, byte[] value, [Range(1, 20)] int ttl)
        {
            const int zero = 0;
            const int connectionTime = 5;

            await _server.WaitLaunchEnding();
            var client = new EtcdClient()
                .UseTransport(new EtcdGrpcTransport())
                .UseGateway(new RoundRobinGateway(new [] { ServerUri }));

            using (client)
            {
                var lease = await client.GrantLeaseAsync(ttl);
                
                await client.Put(key, value)
                    .WithLeaseAsync(lease);
                
                var result = await client.GetAsync(key);
                
                Assert.NotEqual(zero, result.Count);
                
                Thread.Sleep((ttl + connectionTime) * 1000);
                
                result = await client.GetAsync(key);
                
                Assert.Equal(zero, result.Count);
            }
        }
        
        [Theory, AutoData]
        public async Task GetToKeyRequestTest(byte[] value)
        {
            var keys = new[]
            {
                "foo",
                "foo1",
                "foo2",
                "foo3"
            };

            await _server.WaitLaunchEnding();
            var client = new EtcdClient()
                .UseTransport(new EtcdGrpcTransport())
                .UseGateway(new RoundRobinGateway(new [] { ServerUri }));

            using (client)
            {
                foreach (var key in keys)
                {
                    await client.PutAsync(key, value);
                }

                var result = await client.Get(keys.First())
                    .ToKey(keys.Last())
                    .ExecuteAsync();
                
                Assert.Equal(keys.Length - 1, result.Count);
            }
        }
        
        [Theory, AutoData]
        public async Task GetContainsRequestTest(byte[] value)
        {
            var keys = new[]
            {
                "foo",
                "foo1",
                "foo2",
                "foo3"
            };

            await _server.WaitLaunchEnding();
            var client = new EtcdClient()
                .UseTransport(new EtcdGrpcTransport())
                .UseGateway(new RoundRobinGateway(new [] { ServerUri }));

            using (client)
            {
                foreach (var key in keys)
                {
                    await client.PutAsync(key, value);
                }

                var result = await client.Get()
                    .Contains("foo")
                    .ExecuteAsync();
                
                Assert.Equal(keys.Length, result.Count);
            }
        }
        
        [Theory, AutoData]
        public async Task LimitGetRequestTest(byte[] value)
        {
            var keys = new[]
            {
                "foo",
                "foo1",
                "foo2",
                "foo3"
            };

            await _server.WaitLaunchEnding();
            var client = new EtcdClient()
                .UseTransport(new EtcdGrpcTransport())
                .UseGateway(new RoundRobinGateway(new [] { ServerUri }));

            using (client)
            {
                foreach (var key in keys)
                {
                    await client.PutAsync(key, value);
                }

                var result = await client.Get()
                    .Contains("foo")
                    .Limit(2)
                    .ExecuteAsync();
                
                Assert.Equal(2, result.Count);
            }
        }
        
        [Theory, AutoData]
        public async Task RevisionGetRequestTest(string key, byte[][] value)
        {
            var client = new EtcdClient()
                .UseTransport(new EtcdGrpcTransport())
                .UseGateway(new RoundRobinGateway(new [] { ServerUri }));

            await _server.WaitLaunchEnding();
            using (client)
            {
                foreach (var v in value)
                {
                    await client.PutAsync(key, v);
                }

                var result = await client.Get(key)
                    .Revision(1)
                    .ExecuteAsync();
                
                Assert.Equal(value[1], result.ToArray()[1]);
                
                result = await client.Get(key)
                    .Revision(value.Length - 1)
                    .ExecuteAsync();
                
                Assert.Equal(value[value.Length - 1], result.ToArray()[value.Length - 1]);
            }
        }
        
        [Theory, AutoData]
        public async Task DeleteToKeyRequestTest(byte[] value)
        {
            var keys = new[]
            {
                "foo",
                "foo1",
                "foo2",
                "foo3"
            };

            await _server.WaitLaunchEnding();
            var client = new EtcdClient()
                .UseTransport(new EtcdGrpcTransport())
                .UseGateway(new RoundRobinGateway(new [] { ServerUri }));

            using (client)
            {
                foreach (var key in keys)
                {
                    await client.PutAsync(key, value);
                }

                await client.Delete(keys.First())
                    .ToKey(keys.Last())
                    .ExecuteAsync();
            }
        }

        public void Dispose()
        {
            _server?.Dispose();
        }
    }
}