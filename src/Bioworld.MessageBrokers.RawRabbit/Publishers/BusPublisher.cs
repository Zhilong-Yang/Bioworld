﻿using RawRabbit;
using RawRabbit.Enrichers.MessageContext;

namespace Bioworld.MessageBrokers.RawRabbit.Publishers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class BusPublisher : IBusPublisher
    {
        private readonly IBusClient _busClient;

        public BusPublisher(IBusClient busClient)
        {
            _busClient = busClient;
        }

        public Task PublishAsync<T>(T message, string messageId = null, string correlationId = null,
            string spanContext = null,
            object messageContext = null, IDictionary<string, object> headers = null) where T : class
        {
            return _busClient.PublishAsync(message, ctx => ctx.UseMessageContext(messageContext));
        }
    }
}