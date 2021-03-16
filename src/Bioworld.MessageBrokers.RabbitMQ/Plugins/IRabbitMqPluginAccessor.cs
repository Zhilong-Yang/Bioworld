using RabbitMQ.Client.Events;

namespace Bioworld.MessageBrokers.RabbitMQ.Plugins
{
    using System;
    using System.Threading.Tasks;

    internal interface IRabbitMqPluginAccessor
    {
        void SetSuccessor(Func<object, object, BasicDeliverEventArgs, Task> successor);
    }
}