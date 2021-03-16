using RabbitMQ.Client.Events;

namespace Bioworld.MessageBrokers.RabbitMQ
{
    using System;
    using System.Threading.Tasks;
    using Plugins;

    public abstract class RabbitMqPlugin : IRabbitMqPlugin, IRabbitMqPluginAccessor
    {
        private Func<object, object, BasicDeliverEventArgs, Task> _successor;

        public abstract Task HandleAsync(object message, object correlationContext,
            BasicDeliverEventArgs args);

        public Task Next(object message, object correlationContext, BasicDeliverEventArgs args)
            => _successor(message, correlationContext, args);

        void IRabbitMqPluginAccessor.SetSuccessor(Func<object, object, BasicDeliverEventArgs, Task> successor)
            => _successor = successor;
    }
}