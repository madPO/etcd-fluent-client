namespace FluentClient.Client
{
    using System;
    using Auth;
    using Dawn;
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

        private void FillHost(IRequest request)
        {
            Guard.Argument(request)
                .NotNull();
            Guard.Argument(Gateway)
                .NotNull();
            
            var host = Gateway.GetHost();

            Guard.Argument(host)
                .NotNull()
                .Member(x => x.Port, x => x.Positive())
                .Member(x => x.Uri, x => x.NotNull().NotEmpty());

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