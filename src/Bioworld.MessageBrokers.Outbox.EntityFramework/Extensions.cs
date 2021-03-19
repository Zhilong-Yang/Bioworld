namespace Bioworld.MessageBrokers.Outbox.EntityFramework
{
    using Internals;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class Extensions
    {
        public static IMessageOutboxConfigurator AddEntityFramework<T>(this IMessageOutboxConfigurator configurator)
            where T : DbContext
        {
            var builder = configurator.Builder;

            builder.Services.AddDbContext<T>();
            builder.Services.AddTransient<IMessageOutbox, EntityFrameworkMessageOutbox<T>>();
            builder.Services.AddTransient<IMessageOutboxAccessor, EntityFrameworkMessageOutbox<T>>();

            return configurator;
        }
    }
}

