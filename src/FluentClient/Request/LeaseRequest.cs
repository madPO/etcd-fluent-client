namespace FluentClient.Request
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Client;

    public class CreateLeaseRequest : ICreateLeaseRequest
    {
        private readonly Func<ICreateLeaseRequest, CancellationToken, Task<EtcdLease>> _execute;

        public CreateLeaseRequest(Func<ICreateLeaseRequest, CancellationToken, Task<EtcdLease>> execute)
        {
            _execute = execute;
        }

        public string Host { get; set; }
        
        public int Port { get; set; }
        
        public string Key { get; set; }
        
        public long Ttl { get; set; }
        
        //todo: create abstract class with this method
        public Task<EtcdLease> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            return _execute(this, cancellationToken);
        }
    }
    
    public class GetLeaseRequest : IGetLeaseRequest
    {
        private readonly Func<IGetLeaseRequest, CancellationToken, Task<EtcdLease>> _execute;

        public GetLeaseRequest(Func<IGetLeaseRequest, CancellationToken, Task<EtcdLease>> execute)
        {
            _execute = execute;
        }

        public string Host { get; set; }
        
        public int Port { get; set; }
        
        public string Key { get; set; }
        
        public long Id { get; set; }
        
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
            _execute = execute;
        }

        public string Host { get; set; }
        
        public int Port { get; set; }
        
        public string Key { get; set; }
        
        public EtcdLease EtcdLease { get; set; }
        
        public Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            return _execute(this, cancellationToken);
        }
    }  
    
    public class TimeToLiveLeaseRequest : ITimeToLiveLeaseRequest
    {
        private readonly Func<ITimeToLiveLeaseRequest, CancellationToken, Task<long>> _execute;

        public TimeToLiveLeaseRequest(Func<ITimeToLiveLeaseRequest, CancellationToken, Task<long>> execute)
        {
            _execute = execute;
        }

        public string Host { get; set; }
        
        public int Port { get; set; }
        
        public string Key { get; set; }
        
        public EtcdLease EtcdLease { get; set; }
        
        public Task<long> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            return _execute(this, cancellationToken);
        }
    }
}