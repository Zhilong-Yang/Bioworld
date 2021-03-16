using System.Collections.Generic;

namespace Bioworld.MessageBrokers.RabbitMQ.Plugins
{
    public interface IRabbitMqPluginsRegistryAccessor
    {
        LinkedList<RabbitMqPluginChain> Get();
    }
}