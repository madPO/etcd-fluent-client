namespace FluentClient.Extensions
{
    using Client;
    using Request;

    public static class DeleteRequestExtension
    {
        public static IDeleteRequest ToKey(this IDeleteRequest request, EtcdKey key)
        {
            request.ToKey = key;

            return request;
        }

        public static IDeleteRequest Contains(this IDeleteRequest request, EtcdKey key)
        {
            request.ContainsKey = key;

            return request;
        }
    }
}