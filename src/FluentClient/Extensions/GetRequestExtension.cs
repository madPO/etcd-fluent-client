namespace FluentClient.Extensions
{
    using Client;
    using Request;

    public static class GetRequestExtension
    {
        public static IGetRequest ToKey(this IGetRequest request, EtcdKey key)
        {
            request.ToKey = key;

            return request;
        }

        public static IGetRequest Contains(this IGetRequest request, EtcdKey key)
        {
            request.ContainsKey = key;

            return request;
        }

        public static IGetRequest Limit(this IGetRequest request, int limit)
        {
            request.Limit = limit;

            return request;
        }

        //todo: у etcd ревизии не версии ключа, т.е. есть глобальная версия состояния - ревизия и можно запроашивать значения ключей, который были на ревизии
        public static IGetRequest Revision(this IGetRequest request, int version)
        {
            request.Version = version;

            return request;
        }
    }
}