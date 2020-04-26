namespace FluentClient.Extensions
{
    using Client;
    using Dawn;
    using Request;

    public static class DeleteRequestExtension
    {
        public static IDeleteRequest ToKey(this IDeleteRequest request, EtcdKey key)
        {
            Guard.Argument(request).NotNull();
            Guard.Argument(key).NotNull();
            
            request.ToKey = key;

            return request;
        }

        public static IDeleteRequest Contains(this IDeleteRequest request, EtcdKey key)
        {
            Guard.Argument(request).NotNull();
            Guard.Argument(key).NotNull();
            
            request.ContainsKey = key;

            return request;
        }
    }
}