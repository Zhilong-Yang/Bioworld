using MongoDB.Driver;

namespace Bioworld.Persistence.MongoDB
{
    using System.Threading.Tasks;

    public interface IMongoSessionFactory
    {
        Task<IClientSessionHandle> CreateAsync();
    }
}