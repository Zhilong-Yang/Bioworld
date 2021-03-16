namespace Bioworld.MessageBrokers.RabbitMQ
{
    using System;

    public interface IConventions
    {
        Type Type { get; }
        string RoutingKey { get; }
        string Exchange { get; }
        string Queue { get; }
    }
}