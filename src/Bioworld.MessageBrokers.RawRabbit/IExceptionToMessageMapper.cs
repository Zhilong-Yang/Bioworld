using System;

namespace Bioworld.MessageBrokers.RawRabbit
{
    public interface IExceptionToMessageMapper
    {
        object Map(Exception exception, object message);
    }
}