namespace Bioworld.Discovery.Consul.Services
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Discovery.Consul.Models;

    internal sealed class ConsulService : IConsulService
    {
        private static readonly StringContent EmptyRequest = GetPayload(new { });
        private const string Version = "v1";
        private readonly HttpClient _client;

        public ConsulService(HttpClient client)
        {
            _client = client;
        }

        public Task<HttpResponseMessage> RegisterServiceAsync(ServiceRegistration registration) => 
            _client.PutAsync(GetEndpoint("agent/service/register"), GetPayload(registration));

        public Task<HttpResponseMessage> DeregisterServiceAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IDictionary<string, ServiceAgent>> GetServiceAgentsAsync(string service = null)
        {
            throw new System.NotImplementedException();
        }

        private static StringContent GetPayload(object request)
            => new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

        private static string GetEndpoint(string endpoint) => $"{Version}/{endpoint}";
    }
}