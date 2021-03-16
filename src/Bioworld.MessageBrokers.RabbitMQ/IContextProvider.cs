namespace Bioworld.MessageBrokers.RabbitMQ
{
    using System.Collections.Generic;

    public interface IContextProvider
    {
        string HeaderName { get; }
        object Get(IDictionary<string, object> headers);
    }
}