namespace FluentClient.Client
{
    using Dawn;

    public class EtcdValue
    {
        public EtcdValue(string key, byte[] value)
        {
            Guard.Argument(key).NotNull().NotEmpty();
            Guard.Argument(value).NotNull().NotEmpty();
            
            Key = key;
            Value = value;
        }
        
        public EtcdKey Key { get; }
        
        public byte[] Value { get; }
        
        public long Version { get; }
        
        public long CreateRevision { get;  }
        
        public long ModRevision { get; }
        
        public long? LeaseId { get; }
    }
}