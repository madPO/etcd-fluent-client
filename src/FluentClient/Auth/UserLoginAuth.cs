namespace FluentClient.Auth
{
    public class UserLoginAuth : IEtcdAuthMethod
    {
        private readonly string _login;
        
        private readonly string _password;

        public UserLoginAuth(string login, string password)
        {
            _login = login;
            _password = password;
        }
    }
}