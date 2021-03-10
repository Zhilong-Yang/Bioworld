namespace Bioworld.CQRS.Commands
{
    using System;
    using Dispatchers;
    using Types;
    using Microsoft.Extensions.DependencyInjection;

    public static class Extensions
    {
        public static IBioWorldBuilder AddCommandHandlers(this IBioWorldBuilder builder)
        {
            builder.Services.Scan(s =>
                s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>))
                        .WithoutAttribute(typeof(DecoratorAttribute)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            return builder;
        }

        public static IBioWorldBuilder AddInMemoryCommandDispatcher(this IBioWorldBuilder builder)
        {
            builder.Services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            return builder;
        }
    }
}