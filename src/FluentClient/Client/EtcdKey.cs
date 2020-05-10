namespace FluentClient.Client
{
    using Dawn;

    public class EtcdKey
    {
        public string Name { get; set; }

        public static implicit operator EtcdKey(string keyName)
        {
            Guard.Argument(keyName).NotNull().NotEmpty();
            
            return new EtcdKey
            {
                Name = keyName
            };
        } 
    }
}