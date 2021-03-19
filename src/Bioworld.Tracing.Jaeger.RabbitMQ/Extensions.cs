using Bioworld.MessageBrokers.RabbitMQ;
using Bioworld.Tracing.Jaeger.RabbitMQ.Plugins;

namespace Bioworld.Tracing.Jaeger.RabbitMQ
{
    public static class Extensions
    {
        public static IRabbitMqPluginsRegistry AddJaegerRabbitMqPlugin(this IRabbitMqPluginsRegistry registry)
        {
            registry.Add<JaegerPlugin>();
            return registry;
        }
    }
}
