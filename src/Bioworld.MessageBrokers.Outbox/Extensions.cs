namespace Bioworld.MessageBrokers.Outbox
{
    using System;
    using Configurators;
    using Outbox;
    using Processors;
    using Microsoft.Extensions.DependencyInjection;

    public static class Extensions
    {
        private const string SectionName = "outbox";
        private const string RegistryName = "messageBrokers.outbox";

        public static IBioWorldBuilder AddMessageOutbox(this IBioWorldBuilder builder,
            Action<IMessageOutboxConfigurator> configure = null, string sectionName = SectionName)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
            {
                sectionName = SectionName;
            }

            if (!builder.TryRegister(RegistryName))
            {
                return builder;
            }

            var options = builder.GetOptions<OutboxOptions>(sectionName);
            builder.Services.AddSingleton(options);
            var configurator = new MessageOutboxConfigurator(builder, options);

            if (configure is null)
            {
                configurator.AddInMemory();
            }
            else
            {
                configure(configurator);
            }

            if (!options.Enabled)
            {
                return builder;
            }

            builder.Services.AddHostedService<OutboxProcessor>();

            return builder;
        }

        public static IMessageOutboxConfigurator AddInMemory(this IMessageOutboxConfigurator configurator,
            string mongoSectionName = null)
        {
            configurator.Builder.Services.AddTransient<IMessageOutbox, InMemoryMessageOutbox>();

            return configurator;
        }
    }
}