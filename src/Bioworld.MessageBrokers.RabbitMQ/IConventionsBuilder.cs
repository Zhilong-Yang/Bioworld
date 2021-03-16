namespace Bioworld.MessageBrokers.RabbitMQ
{
    using System;

    public interface IConventionsBuilder
    {
        string GetRoutingKey(Type type);
        string GetExchange(Type type);
        string GetQueue(Type type);
    }
}