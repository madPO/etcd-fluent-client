namespace FluentClient.Request
{
    using Client;

    public interface IDeleteRequest : IRequest, IEmptyRequest
    {
        /// <summary>
        /// Key
        /// </summary>
        EtcdKey Key { get; set; }
        
        EtcdKey ToKey { get; set; }

        EtcdKey ContainsKey { get; set; }
    }
}