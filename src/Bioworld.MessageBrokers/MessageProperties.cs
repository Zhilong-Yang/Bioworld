namespace Bioworld.MessageBrokers
{
    using System.Collections.Generic;

    public class MessageProperties : IMessageProperties
    {
        public string MessageId { get; }
        public string CorrelationId { get; }
        public long Timestamp { get; }
        public IDictionary<string, object> Header { get; }
    }
}