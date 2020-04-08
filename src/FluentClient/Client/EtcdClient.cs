namespace FluentClient.Client
{
    using System;
    using Auth;
    using Gateway;
    using Transport;

    public partial class EtcdClient : IEtcdClient
    {
        public IEtcdAuthMethod AuthMethod { get; set; }
        
        public IEtcdTransport Transport { get; set;  }
        
        public IEtcdGateway Gateway { get; set; }

        private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here
        }

        protected virtual void Dispose(bool disposing)
        {
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