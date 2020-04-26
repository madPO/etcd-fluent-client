namespace FluentClient.Gateway
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class RoundRobinGateway : IEtcdGateway
    {
        private readonly IEnumerator _host;

        public RoundRobinGateway(string[] host)
        {
            _host = host
                .Select(x => Endpoint.Parse(x))
                .ToArray()
                .GetEnumerator();
        }

        public EtcdHost GetHost()
        {
            var result = Next();

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

        internal class Endpoint
        {
            internal string Host { get; set; }
            
            internal int Port { get; set; }
            
            internal EndpointState State { get; set; }

            internal static Endpoint Parse(string host)
            {
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