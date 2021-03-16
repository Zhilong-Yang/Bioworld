using MongoDB.Driver;

namespace Bioworld.Persistence.MongoDB
{
    using System.Threading.Tasks;

    public interface IMongoDbSeeder
    {
        Task SeedAsync(IMongoDatabase database);
    }
}