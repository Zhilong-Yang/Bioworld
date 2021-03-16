using RabbitMQ.Client.Events;

namespace Bioworld.MessageBrokers.RabbitMQ
{
    using System.Threading.Tasks;

    public interface IRabbitMqPlugin
    {
        Task HandleAsync(object message, object correlationContext, BasicDeliverEventArgs args);
    }
}