namespace Bioworld.Discovery.Consul.Models
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class ServiceRegistration
    {
        [JsonPropertyName("ID")]
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Tags { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
        public IDictionary<string, string> Meta { get; set; }
        public bool EnableTagOverride { get; set; }
        public IEnumerable<ServiceCheck> Checks { get; set; }
        public Weights Weights { get; set; }
        public Connect Connect { get; set; }
    }
}