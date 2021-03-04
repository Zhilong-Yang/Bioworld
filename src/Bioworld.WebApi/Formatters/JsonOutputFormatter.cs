using Microsoft.AspNetCore.Http;
using Open.Serialization.Json;

namespace Bioworld.WebApi.Formatters
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Formatters;

    internal class JsonOutputFormatter:IOutputFormatter
    {
        private readonly IJsonSerializer _serializer;

        public JsonOutputFormatter(IJsonSerializer serialize)
        {
            _serializer = serialize;
        }

        public bool CanWriteResult(OutputFormatterCanWriteContext context) => true;

        public async Task WriteAsync(OutputFormatterWriteContext context)
        {
            if (context.Object is null)
            {
                return;
            }

            context.HttpContext.Response.ContentType = "application/json";

            if (context.Object is string json)
            {
                await context.HttpContext.Response.WriteAsync(json);
                return;
            }

            await _serializer.SerializeAsync(context.HttpContext.Response.Body, context.Object);
        }
    }
}