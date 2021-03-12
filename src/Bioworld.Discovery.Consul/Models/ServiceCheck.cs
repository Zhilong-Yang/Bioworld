namespace Bioworld.Discovery.Consul.Models
{
    using System.Collections.Generic;

    public class ServiceCheck
    {
        public string DeregisterCriticalServiceAfter { get; set; }
        public List<string> Args { get; set; }
        public string Http { get; set; }
        public string Interval { get; set; }
        public string Ttl { get; set; }
    }
}