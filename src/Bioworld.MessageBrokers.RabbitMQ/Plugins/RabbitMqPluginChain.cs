namespace Bioworld.MessageBrokers.RabbitMQ.Plugins
{
    using System;

    public class RabbitMqPluginChain
    {
        public Type PluginType { get; set; }
        public IRabbitMqPlugin Plugin { get; set; }
    }
}