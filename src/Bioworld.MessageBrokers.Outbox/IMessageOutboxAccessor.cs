namespace Bioworld.MessageBrokers.Outbox
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Messages;

    public interface IMessageOutboxAccessor
    {
        Task<IReadOnlyList<OutboxMessage>> GetUnsentAsync();
        Task ProcessAsync(OutboxMessage message);
        Task ProcessAsync(IEnumerable<OutboxMessage> outboxMessages);
    }
}