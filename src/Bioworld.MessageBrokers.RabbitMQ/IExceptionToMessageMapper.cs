namespace Bioworld.MessageBrokers.RabbitMQ
{
    using System;

    public interface IExceptionToMessageMapper
    {
        object Map(Exception exception, object message);
    }
}