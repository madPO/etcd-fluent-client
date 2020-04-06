namespace FluentClient.Client
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Auth;
    using Request;
    using Transport;

    public class EtcdClient : IEtcdClient
    {
        private readonly string[] _host;

        private IEtcdAuthMethod _authMethod;
        
        private IEtcdTransport _transport;

        public EtcdClient(string[] host)
        {
            _host = host;
        }
        
        public Task PutAsync(IEtcdKey key, byte[] value, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public IPutRequest Put(IEtcdKey key, byte[] value)
        {
            throw new System.NotImplementedException();
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