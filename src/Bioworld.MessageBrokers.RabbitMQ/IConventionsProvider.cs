namespace Bioworld.MessageBrokers.RabbitMQ
{
    using System;

    public interface IConventionsProvider
    {
        IConventions Get<T>();
        IConventions Get(Type type);
    }
}