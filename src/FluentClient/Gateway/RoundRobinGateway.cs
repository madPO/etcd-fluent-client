namespace FluentClient.Gateway
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RoundRobinGateway : IEtcdGateway
    {
        private readonly IEnumerator<Endpoint> _host;

        public RoundRobinGateway(string[] host)
        {
            _host = (IEnumerator<Endpoint>) host
                .Select(x => Endpoint.Parse(x))
                .ToArray()
                .GetEnumerator();
        }

        public (string, int) GetHost()
        {
            var result = Next();

            return (result.Host, result.Port);
        }

        private Endpoint Next(bool reseted = false)
        {
            while (_host.MoveNext())
            {
                if (_host.Current.State == EndpointState.Work)
                    return _host.Current;
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