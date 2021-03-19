namespace Bioworld.MessageBrokers.Outbox.Messages
{
    using System;
    using Types;

    internal sealed class InboxMessage : IIdentifiable<string>
    {
        public string Id { get; }
        public DateTime ProcessedAt { get; set; }
    }
}