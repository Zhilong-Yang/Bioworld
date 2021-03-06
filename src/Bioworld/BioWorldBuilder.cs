namespace Bioworld
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using Types;
    using Microsoft.Extensions.DependencyInjection;

    public sealed class BioWorldBuilder : IBioWorldBuilder
    {
        private readonly ConcurrentDictionary<string, bool> _register = new ConcurrentDictionary<string, bool>();

        private readonly List<Action<IServiceProvider>> _buildActions;

        private readonly IServiceCollection _services;

        private BioWorldBuilder(IServiceCollection services)
        {
            _services = services;
            _buildActions = new List<Action<IServiceProvider>>();
            _services.AddSingleton<IStartupInitializer>(new StartupInitializer());
        }

        IServiceCollection IBioWorldBuilder.Services => _services;

        public static IBioWorldBuilder Create(IServiceCollection service) => new BioWorldBuilder(service);

        public bool TryRegister(string name) => _register.TryAdd(name, true);

        public void AddBuildAction(Action<IServiceProvider> execute) => _buildActions.Add(execute);

        public void AddInitializer(IInitializer initializer) =>
            AddBuildAction(sp =>
            {
                var startupInitializer = sp.GetService<IStartupInitializer>();
                startupInitializer?.AddInitializer(initializer);
            });

        public void AddInitializer<TInitializer>() where TInitializer : IInitializer =>
            AddBuildAction(sp =>
            {
                var initializer = sp.GetService<TInitializer>();
                var startupInitializer = sp.GetService<IStartupInitializer>();
                startupInitializer?.AddInitializer(initializer);
            });

        public IServiceProvider Build()
        {
            var serviceProvider = _services.BuildServiceProvider();
            _buildActions.ForEach(a => a(serviceProvider));
            return serviceProvider;
        }
    }
}