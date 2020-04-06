namespace FluentClient.Client
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Auth;
    using Gateway;
    using Request;
    using Transport;

    public class EtcdClient : IEtcdClient
    {
        private IEtcdAuthMethod _authMethod;
        
        private IEtcdTransport _transport;
        
        private IEtcdGateway _gateway;

        public Task PutAsync(IEtcdKey key, byte[] value, CancellationToken cancellationToken = default)
        {
            var request = Put(key, value);
            return request.ExecuteAsync(cancellationToken);
        }

        public IPutRequest Put(IEtcdKey key, byte[] value)
        {
            var host = _gateway.GetHost();
            var request = new EtcdPutRequest(_transport)
            {
                Host = host.Item1,
                Key = key.Name,
                Port = host.Item2,
                Value = value
            };

            return request;
        }

        public IEtcdClient UseAuth(IEtcdAuthMethod authMethod)
        {
            _authMethod = authMethod;
            
            return this;
        }

        public IEtcdClient UseTransport(IEtcdTransport transport)
        {
            _transport = transport;

            return this;
        }

        public IEtcdClient UseGateway(IEtcdGateway gateway)
        {
            _gateway = gateway;

            return this;
        }

        private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here
        }

        protected virtual void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing)
            {
                _transport?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~EtcdClient()
        {
            Dispose(false);
        }
    }
}