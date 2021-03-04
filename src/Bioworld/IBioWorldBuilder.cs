namespace Bioworld
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Types;

    public interface IBioWorldBuilder
    {
        IServiceCollection Services { get; }
        bool TryRegister(string name);
        void AddBuildAction(Action<IServiceProvider> execute);
        void AddInitializer(IInitializer initializer);
        void AddInitializer<TInitializer>() where TInitializer : IInitializer;
        IServiceProvider Build();
    }
}