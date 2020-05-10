namespace FluentClient.Gateway
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Dawn;

    public class RoundRobinGateway : IEtcdGateway
    {
        private readonly IEnumerator _host;

        public RoundRobinGateway(string[] host)
        {
            Guard.Argument(host).NotNull().NotEmpty();
            foreach (var h in host)
            {
                Guard.Argument(h).NotNull().NotEmpty();
            }
            
            _host = host
                .Select(x => Endpoint.Parse(x))
                .Where(x => x != null)
                .ToArray()
                .GetEnumerator();
        }

        public EtcdHost GetHost()
        {
            var result = Next();

            Guard.Argument(result)
                .NotNull()
                .Member(x => x.Host, x => x.NotNull().NotEmpty())
                .Member(x => x.Port, x => x.Positive())
                .Member(x => x.State, x => x.Equal(EndpointState.Work));

            return new EtcdHost
            {
                Uri = result.Host,
                Port = result.Port
            };
        }

        private Endpoint Next(bool reseted = false)
        {
            while (_host.MoveNext())
            {
                var current = (Endpoint) _host.Current;
                if (current?.State == EndpointState.Work)
                    return current;
            }
            
            if(reseted)
                throw new Exception();
            
            _host.Reset();
            return Next(true);
        }

        internal class Endpoint : IEquatable<Endpoint>
        {
            internal string Host { get; set; }
            
            internal int Port { get; set; }
            
            internal EndpointState State { get; set; }

            internal static Endpoint Parse(string host)
            {
                Guard.Argument(host).NotNull().NotEmpty();
                
                var elements = host.Split(':');
                if(elements.Length != 2)
                    throw new Exception();

                //todo: regex
                if (int.TryParse(elements[1], out var p))
                {
                    return new Endpoint
                    {
                        Host = elements[0],
                        Port = p,
                        State = EndpointState.Work
                    };
                }
                
                throw new Exception();
            }

            public bool Equals(Endpoint other)
            {
                if (other == null)
                    return false;

                if (Host != other.Host)
                    return false;

                if (Port != other.Port)
                    return false;

                return true;
            }
        }
        
        internal enum EndpointState
        {
            Work,
            
            //todo: change state
            Timeout,
            
            Dead
        }
    }
}