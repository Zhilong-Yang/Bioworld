using MongoDB.Driver;

namespace Bioworld.Persistence.MongoDB.Factories
{
    using System.Threading.Tasks;

    internal sealed class MongoSessionFactory : IMongoSessionFactory
    {
        private readonly IMongoClient _client;

        public MongoSessionFactory(IMongoClient client)
        {
            _client = client;
        }

        public Task<IClientSessionHandle> CreateAsync()
            => _client.StartSessionAsync();
    }
}