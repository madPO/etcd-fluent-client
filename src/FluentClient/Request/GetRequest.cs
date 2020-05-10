namespace FluentClient.Request
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Client;
    using Dawn;

    public class GetRequest : IGetRequest
    {
        private readonly Func<IGetRequest, CancellationToken, Task<IReadOnlyCollection<byte[]>>> _execute;
        
        public GetRequest(Func<IGetRequest, CancellationToken, Task<IReadOnlyCollection<byte[]>>> execute)
        {
            Guard.Argument(execute).NotNull();
            
            _execute = execute;
        }

        public string Host { get; set; }
        
        public int Port { get; set; }
        
        public EtcdKey Key { get; set; }

        public EtcdKey ToKey { get; set; }

        public EtcdKey ContainsKey { get; set; }

        public int? Limit { get; set; }

        public int? Version { get; set; }

        public Task<IReadOnlyCollection<byte[]>> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            return _execute(this, cancellationToken);
        }
    }
}