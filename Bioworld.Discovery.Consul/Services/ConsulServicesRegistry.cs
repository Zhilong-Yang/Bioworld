namespace Bioworld.Discovery.Consul.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    internal sealed class ConsulServicesRegistry : IConsulServicesRegistry
    {
        private readonly Random _random = new Random();
        private readonly IConsulService _consulService;
        private readonly IDictionary<string, ISet<string>> _usedService = new Dictionary<string, ISet<string>>();

        public Task<ServiceAgent> GetAsync(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}