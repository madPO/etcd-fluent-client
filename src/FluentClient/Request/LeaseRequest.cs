namespace FluentClient.Request
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Client;
    using Dawn;

    public class CreateLeaseRequest : ICreateLeaseRequest
    {
        private readonly Func<ICreateLeaseRequest, CancellationToken, Task<EtcdLease>> _execute;

        public CreateLeaseRequest(Func<ICreateLeaseRequest, CancellationToken, Task<EtcdLease>> execute)
        {
            Guard.Argument(execute).NotNull();
            
            _execute = execute;
        }

        public string Host { get; set; }
        
        public int Port { get; set; }

        public long Ttl { get; set; }
        
        public Task<EtcdLease> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            return _execute(this, cancellationToken);
        }
    }

    public class RevokeLeaseRequest : IRevokeLeaseRequest
    {
        private readonly Func<IRevokeLeaseRequest, CancellationToken, Task> _execute;

        public RevokeLeaseRequest(Func<IRevokeLeaseRequest, CancellationToken, Task> execute)
        {
            Guard.Argument(execute).NotNull();
            
            _execute = execute;
        }

        public string Host { get; set; }
        
        public int Port { get; set; }
        
        public EtcdLease EtcdLease { get; set; }
        
        public Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            return _execute(this, cancellationToken);
        }
    }  
    
    public class TimeToLiveLeaseRequest : ITimeToLiveLeaseRequest
    {
        private readonly Func<ITimeToLiveLeaseRequest, CancellationToken, Task<EtcdLease>> _execute;

        public TimeToLiveLeaseRequest(Func<ITimeToLiveLeaseRequest, CancellationToken, Task<EtcdLease>> execute)
        {
            Guard.Argument(execute).NotNull();
            
            _execute = execute;
        }

        public string Host { get; set; }
        
        public int Port { get; set; }
        
        public EtcdLease EtcdLease { get; set; }
        
        public Task<EtcdLease> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            return _execute(this, cancellationToken);
        }
    }
}