namespace FluentClient.Client
{
    using System;
    using Auth;
    using Gateway;
    using Transport;

    public partial class EtcdClient : IEtcdClient
    {
        private IEtcdAuthMethod _authMethod;
        
        private IEtcdTransport _transport;
        
        private IEtcdGateway _gateway;

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