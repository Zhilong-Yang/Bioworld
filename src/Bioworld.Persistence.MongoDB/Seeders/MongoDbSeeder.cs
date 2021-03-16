using MongoDB.Driver;

namespace Bioworld.Persistence.MongoDB.Seeders
{
    using System.Linq;
    using System.Threading.Tasks;

    internal class MongoDbSeeder: IMongoDbSeeder
    {
        public async Task SeedAsync(IMongoDatabase database)
        {
            await CustomSeedAsync(database);
        }

        protected virtual async Task CustomSeedAsync(IMongoDatabase database)
        {
            var cursor = await database.ListCollectionNamesAsync();
            var collections = await cursor.ToListAsync();
            if (collections.Any())
            {
                return;
            }

            await Task.CompletedTask;
        }
    }
}