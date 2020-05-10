namespace FluentClient.Request
{
    using Client;

    public interface ICreateLeaseRequest : IRequest, IValueRequest<EtcdLease>
    {
        long Ttl { get; set; }
    }

    public interface IRevokeLeaseRequest : IRequest, IEmptyRequest
    {
        EtcdLease EtcdLease { get; set; }
    }
    
    public interface ITimeToLiveLeaseRequest : IRequest, IValueRequest<EtcdLease>
    {
        EtcdLease EtcdLease { get; set; }
    }
}