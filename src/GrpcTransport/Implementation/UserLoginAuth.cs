namespace GrpcTransport.Implementation
{
    using System.Threading;
    using System.Threading.Tasks;
    using Dawn;
    using FluentClient.Auth;

    public class UserLoginAuth : IEtcdAuthMethod
    {
        private readonly string _login;
        
        private readonly string _password;

        public UserLoginAuth(string login, string password)
        {
            Guard.Argument(login).NotNull().NotEmpty();
            Guard.Argument(password).NotNull().NotEmpty();
            
            _login = login;
            _password = password;
        }

        public Task PrepareAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}