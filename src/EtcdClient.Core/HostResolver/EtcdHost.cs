namespace EtcdClient.Core.HostResolver
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Host etcd server
    /// </summary>
    public class EtcdHost : IEquatable<EtcdHost>
    {
        /// <summary>
        /// Uri
        /// </summary>
        public string Uri { get; set; }
        
        /// <summary>
        /// Port
        /// </summary>
        public int Port { get; set; }
        
        public bool Equals([AllowNull] EtcdHost other)
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