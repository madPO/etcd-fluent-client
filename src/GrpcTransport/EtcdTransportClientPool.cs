namespace GrpcTransport
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using Dawn;
    using Grpc.Core;

    public class EtcdTransportClientPool<TType> where TType : class
    {
        private ConcurrentDictionary<string, TType> _pool;

        public EtcdTransportClientPool()
        {
            _pool = new ConcurrentDictionary<string, TType>();
        }

        public TType Get(Channel channel, Func<TType> creator)
        {
            Guard.Argument(channel)
                .NotNull()
                .Member(x => x.Target, x => x.NotNull().NotEmpty());
            Guard.Argument(creator).NotNull();
            
            if (!_pool.ContainsKey(channel.Target))
            {
                _pool.TryAdd(channel.Target, creator());
            }

            return _pool[channel.Target];
        }

        public IReadOnlyCollection<TType> All => _pool.Values.ToArray();
    }
}