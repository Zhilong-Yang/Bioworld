using RabbitMQ.Client.Events;

namespace Bioworld.MessageBrokers.RabbitMQ.Plugins
{
    using System;
    using System.Threading.Tasks;

    internal  interface IRabbitMqPluginsExecutor
    {
        Task ExecuteAsync(Func<object, object, BasicDeliverEventArgs, Task> successor,
            object message, object correlationContext, BasicDeliverEventArgs args);
    }
}