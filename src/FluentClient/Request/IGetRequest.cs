namespace FluentClient.Request
{
    using System.Collections.Generic;
    using Client;

    public interface IGetRequest : IRequest, IValueRequest<IReadOnlyCollection<byte[]>>
    {
        EtcdKey ToKey { get; set; }

        EtcdKey ContainsKey { get; set; }

        int? Limit { get; set; }

        int? Version { get; set; }
    }
}