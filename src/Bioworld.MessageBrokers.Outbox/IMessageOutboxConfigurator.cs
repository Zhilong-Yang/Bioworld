namespace Bioworld.MessageBrokers.Outbox
{
    public interface IMessageOutboxConfigurator
    {
        IBioWorldBuilder Builder { get; }
        OutboxOptions Options { get; }
    }
}