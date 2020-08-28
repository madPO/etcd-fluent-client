namespace FluentClient.Client.WatchEvent
{
    public class CreatedEvent : IEtcdWatchEvent
    {
        public CreatedEvent(long watchId)
        {
            WatchId = watchId;
        }
        
        public long WatchId { get; }
    }
}