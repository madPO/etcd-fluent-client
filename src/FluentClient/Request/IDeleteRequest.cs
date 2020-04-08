namespace FluentClient.Request
{
    using Client;

    public interface IDeleteRequest : IRequest, IEmptyRequest
    {
        EtcdKey ToKey { get; set; }

        EtcdKey ContainsKey { get; set; }
    }
}