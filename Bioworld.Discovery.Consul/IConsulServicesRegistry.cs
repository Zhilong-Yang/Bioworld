namespace Bioworld.Discovery.Consul
{
    using System.Threading.Tasks;
    using Models;

    public interface IConsulServicesRegistry
    {
        Task<ServiceAgent> GetAsync(string name);
    }
}