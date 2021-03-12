namespace Bioworld.Discovery.Consul.Models
{
    using System.Collections.Generic;

    public class Proxy
    {
        public List<Upstream> Upstreams { get; set; }
    }
}