namespace FluentClient.Client
{
    using System;
    using Dawn;
    using WatchEvent;

    public class EtcdWatcher
    {
        public string Key { get; }
        
        public long WatchId { get; }

        public event Action<IEtcdWatchEvent> Actions;

        public void Register(Action<IEtcdWatchEvent> action)
        {
            Guard.Argument(action).NotNull();
            
            Actions += action;
        }
    }
}