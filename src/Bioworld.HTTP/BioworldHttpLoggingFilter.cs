namespace Bioworld.HTTP
{
    using System;
    using Microsoft.Extensions.Http;
    using Microsoft.Extensions.Logging;

    internal sealed class BioworldHttpLoggingFilter: IHttpMessageHandlerBuilderFilter
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly HttpClientOptions _options;

        public BioworldHttpLoggingFilter(ILoggerFactory loggerFactory, HttpClientOptions options)
        {
            _loggerFactory = loggerFactory;
            _options = options;
        }

        public Action<HttpMessageHandlerBuilder> Configure(Action<HttpMessageHandlerBuilder> next)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            return builder =>
            {
                next(builder);
                var logger = _loggerFactory.CreateLogger($"System.Net.Http.HttpClient.{builder.Name}.LogicalHandler");
                builder.AdditionalHandlers.Insert(0, new BioworldLoggingScopeHttpMessageHandler(logger, _options));
            };
        }
    }
}