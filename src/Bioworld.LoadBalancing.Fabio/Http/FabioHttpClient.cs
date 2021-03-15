namespace Bioworld.LoadBalancing.Fabio.Http
{
    using HTTP;
    using System.Net.Http;

    internal sealed class FabioHttpClient : BioworldHttpClient, IFabioHttpClient
    {
        public FabioHttpClient(HttpClient client, HttpClientOptions options,
            ICorrelationContextFactory correlationContextFactory, ICorrelationIdFactory correlationIdFactory)
            : base(client, options, correlationContextFactory, correlationIdFactory)
        {
        }
    }
}