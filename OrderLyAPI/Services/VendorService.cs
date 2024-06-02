using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OrderLy_API.Models;

namespace OrderLy_API.Services
{
    public class VendorService
    {
        private readonly IMongoCollection<Vendor> _vendorCollection;
        public VendorService(
            IOptions<OrderLyDatabaseSettings> orderlyDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                orderlyDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                orderlyDatabaseSettings.Value.DatabaseName);

            _vendorCollection = mongoDatabase.GetCollection<Vendor>(
                orderlyDatabaseSettings.Value.VendorsCollectionName);
        }

        public async Task<List<Vendor>> GetAsync() =>
            await _vendorCollection.Find(_ => true).ToListAsync();

        public async Task<Vendor?> GetAsync(string id) =>
            await _vendorCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Vendor newVendor) =>
            await _vendorCollection.InsertOneAsync(newVendor);

        public async Task UpdateAsync(string id, Vendor updatedVendor) =>
            await _vendorCollection.ReplaceOneAsync(x => x.Id == id, updatedVendor);

        public async Task RemoveAsync(string id) =>
            await _vendorCollection.DeleteOneAsync(x => x.Id == id);
    }
}
