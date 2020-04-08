namespace FluentClient.Request
{
    using Client;

    public interface ICreateLeaseRequest : IRequest, IValueRequest<EtcdLease>
    {
        long Ttl { get; set; }
    }
    
    public interface IGetLeaseRequest : IRequest, IValueRequest<EtcdLease>
    {
        long Id { get; set; }
    }
    
    public interface IRevokeLeaseRequest : IRequest, IEmptyRequest
    {
        EtcdLease EtcdLease { get; set; }
    }
    
    public interface ITimeToLiveLeaseRequest : IRequest, IValueRequest<long>
    {
        EtcdLease EtcdLease { get; set; }
    }
}