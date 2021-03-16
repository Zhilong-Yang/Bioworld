using System.Text;

namespace Bioworld.MessageBrokers.RabbitMQ.Contexts
{
    using System.Collections.Generic;

    internal sealed class ContextProvider : IContextProvider
    {
        private readonly IRabbitMqSerializer _serializer;
        public string HeaderName { get; }

        public ContextProvider(IRabbitMqSerializer serializer, RabbitMqOptions options)
        {
            _serializer = serializer;
            HeaderName = string.IsNullOrWhiteSpace(options.Context?.Header)
                ? "message_context"
                : options.Context.Header;
        }


        public object Get(IDictionary<string, object> headers)
        {
            if (headers is null)
            {
                return null;
            }

            if (!headers.TryGetValue(HeaderName, out var context))
            {
                return null;
            }

            if (!(context is byte[] bytes))
            {
                return null;
            }

            return _serializer.Deserialize(Encoding.UTF8.GetString(bytes));
        }
    }
}