namespace FluentClient.Client.WatchEvent
{
    public class CanceledEvent : IEtcdWatchEvent
    {
        public CanceledEvent(long watchId, string reason)
        {
            Reason = reason;
            WatchId = watchId;
        }

        public string Reason { get; }
        
        public long WatchId { get; }
    }
}