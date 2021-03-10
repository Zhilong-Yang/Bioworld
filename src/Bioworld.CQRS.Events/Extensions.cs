using System;
using Bioworld.CQRS.Events.Dispatchers;
using Bioworld.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Bioworld.CQRS.Events
{
    public static class Extensions
    {
        public static IBioWorldBuilder AddEventHandlers(this IBioWorldBuilder builder)
        {
            builder.Services.Scan(s =>
            {
                s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c =>
                    {
                        c.AssignableTo(typeof(IEventHandler<>))
                            .WithoutAttribute(typeof(DecoratorAttribute));
                    })
                    .AsImplementedInterfaces()
                    .WithTransientLifetime();
            });
            return builder;
        }

        public static IBioWorldBuilder AddInMemoryEventDispatcher(this IBioWorldBuilder builder)
        {
            builder.Services.AddSingleton<IEventDispatcher, EventDispatcher>();
            return builder;
        }
    }
}