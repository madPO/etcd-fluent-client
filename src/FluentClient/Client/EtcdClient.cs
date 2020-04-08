namespace FluentClient.Client
{
    using System;
    using Auth;
    using Gateway;
    using Request;
    using Transport;

    public partial class EtcdClient : IEtcdClient
    {
        public IEtcdAuthMethod AuthMethod { get; set; }
        
        public IEtcdTransport Transport { get; set;  }
        
        public IEtcdGateway Gateway { get; set; }

        public bool IsActive => _isActive;

        private bool _isActive;

        public EtcdClient()
        {
            _isActive = true;
        }

        protected void FillHost(IRequest request)
        {
            var host = Gateway.GetHost();
            request.Host = host.Uri;
            request.Port = host.Port;
        }
        

        private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isActive)
                _isActive = false;
            
            ReleaseUnmanagedResources();
            if (disposing)
            {
                Transport?.Dispose();
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