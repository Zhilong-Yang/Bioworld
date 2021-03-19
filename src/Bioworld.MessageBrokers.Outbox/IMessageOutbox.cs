namespace Bioworld.MessageBrokers.Outbox
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMessageOutbox
    {
        bool Enabled { get; }

        Task HandleAsync(string messageId, Func<Task> handler);

        Task SendAsync<T>(T message, string originatedMessageId = null, string messageId = null,
            string correlationId = null, string spanContext = null, object messageContext = null,
            IDictionary<string, object> headers = null) where T : class;
    }
}