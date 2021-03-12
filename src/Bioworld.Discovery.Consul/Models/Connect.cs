namespace Bioworld.Discovery.Consul.Models
{
    using System.Text.Json.Serialization;

    public class Connect
    {
        [JsonPropertyName("sidecar_service")]
        public SidecarService SidecarService { get; set; }
    }
}