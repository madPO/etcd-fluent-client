namespace FluentClient.Client
{
    //todo: mb use proxy class like NHibernate and implement lazy property and self method
    public class EtcdLease
    {
        public EtcdLease(long id, long ttl = 0, long requestedTtl = 0)
        {
            Id = id;
            Ttl = ttl;
            RequestedTtl = requestedTtl;
        }

        public long Id { get; }
        
        public long Ttl { get; }
        
        public long RequestedTtl { get; }
    }
}