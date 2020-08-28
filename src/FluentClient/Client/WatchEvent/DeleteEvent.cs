namespace FluentClient.Client.WatchEvent
{
    public class DeleteEvent: IEtcdWatchEvent
    {
        public DeleteEvent(long watchId)
        {
            WatchId = watchId;
        }

        public long WatchId { get; }
    }
}