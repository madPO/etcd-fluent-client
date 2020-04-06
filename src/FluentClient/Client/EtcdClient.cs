namespace FluentClient.Client
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Auth;
    using Request;

    public class EtcdClient : IEtcdClient
    {
        private readonly string[] _host;

        private IEtcdAuthMethod _authMethod;

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

        private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here
        }

        protected virtual void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing)
            {
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