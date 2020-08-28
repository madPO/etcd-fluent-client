namespace FluentClient.Client.WatchEvent
{
    public class PutEvent : IEtcdWatchEvent
    {
        public PutEvent(long watchId, byte[] value)
        {
            WatchId = watchId;
            Value = value;
        }

        public long WatchId { get; }
        
        public byte[] Value { get; }
    }
}