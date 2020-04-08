namespace FluentClient.Client
{
    public class EtcdKey
    {
        public string Name { get; set; }

        public static implicit operator EtcdKey(string keyName)
        {
            return new EtcdKey
            {
                Name = keyName
            };
        } 
    }
}