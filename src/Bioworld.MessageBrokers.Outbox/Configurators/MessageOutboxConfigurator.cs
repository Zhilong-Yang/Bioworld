namespace Bioworld.MessageBrokers.Outbox.Configurators
{
    public class MessageOutboxConfigurator : IMessageOutboxConfigurator
    {
        public IBioWorldBuilder Builder { get; }

        public OutboxOptions Options { get; }

        public MessageOutboxConfigurator(IBioWorldBuilder builder, OutboxOptions options)
        {
            Builder = builder;
            Options = options;
        }
    }
}