namespace Bioworld.MessageBrokers.Outbox.Messages
{
    using System;
    using Types;

    public sealed class InboxMessage : IIdentifiable<string>
    {
        public string Id { get; set; }
        public DateTime ProcessedAt { get; set; }
    }
}