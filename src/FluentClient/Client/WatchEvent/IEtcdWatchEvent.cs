namespace FluentClient.Client.WatchEvent
{
    public interface IEtcdWatchEvent
    {
        long WatchId { get; }
    }
}