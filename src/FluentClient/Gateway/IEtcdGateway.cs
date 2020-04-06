namespace FluentClient.Gateway
{
    public interface IEtcdGateway
    {
        (string, int) GetHost();
    }
}