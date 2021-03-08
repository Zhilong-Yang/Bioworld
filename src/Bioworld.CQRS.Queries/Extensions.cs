namespace Bioworld.CQRS.Queries
{
    using System;
    using CQRS.Queries.Dispatchers;
    using Types;
    using Microsoft.Extensions.DependencyInjection;


    public static class Extensions
    {
        public static IBioWorldBuilder AddQueryHandler(this IBioWorldBuilder builder)
        {
            builder.Services.Scan(s =>
                s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c =>
                        c.AssignableTo(typeof(IQueryHandler<,>))
                            .WithoutAttribute(typeof(DecoratorAttribute)))
                    .AsImplementedInterfaces().WithTransientLifetime());
            return builder;
        }

        public static IBioWorldBuilder AddInMemoryQueryDispatcher(this IBioWorldBuilder builder)
        {
            builder.Services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
            return builder;
        }
    }
}