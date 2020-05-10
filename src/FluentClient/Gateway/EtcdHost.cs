namespace FluentClient.Gateway
{
    using System;

    public class EtcdHost : IEquatable<EtcdHost>
    {
        public string Uri { get; set; }
        
        public int Port { get; set; }
        
        public bool Equals(EtcdHost other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (Uri != other.Uri)
                return false;

            if (Port != other.Port)
                return false;

            return true;
        }
    }
}