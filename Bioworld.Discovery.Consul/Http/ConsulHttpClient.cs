namespace Bioworld.Discovery.Consul.Http
{
    using System.Net.Http;
    using HTTP;

    internal sealed class ConsulHttpClient : BioworldHttpClient, IConsulHttpClient
    {
        public ConsulHttpClient(HttpClient client, HttpClientOptions options,
            ICorrelationContextFactory correlationContextFactory, ICorrelationIdFactory correlationIdFactory)
            : base(client, options, correlationContextFactory, correlationIdFactory)
        {
        }
    }
}