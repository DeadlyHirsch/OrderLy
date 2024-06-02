using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using OrderLy_API.Models;
using System.Text.Json;

namespace OrderLy_API.Services
{
    public class OrderService
    {
        private readonly IMongoCollection<Order> _orderCollection;
        public OrderService(
            IOptions<OrderLyDatabaseSettings> orderlyDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                orderlyDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                orderlyDatabaseSettings.Value.DatabaseName);

            _orderCollection = mongoDatabase.GetCollection<Order>(
                orderlyDatabaseSettings.Value.OrdersCollectionName);
        }

        public async Task<List<Order>> GetAsync() =>
            await _orderCollection.Find(_ => true).ToListAsync();

        public async Task<Order?> GetAsync(string id) =>
            await _orderCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Order newOrder) =>
            await _orderCollection.InsertOneAsync(newOrder);

        public async Task UpdateAsync(string id, Order updatedOrder) =>
            await _orderCollection.ReplaceOneAsync(x => x.Id == id, updatedOrder);

        public async Task RemoveAsync(string id) =>
            await _orderCollection.DeleteOneAsync(x => x.Id == id);

        public async Task<List<Week>> GetWeeklyAsync()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("OrderLy");
            var collection = db.GetCollection<BsonDocument>("Orders");

            var Result = await collection.Aggregate()
                .Match(new BsonDocument { { "createdAt",
                new BsonDocument("$gte", DateTime.Now.AddDays(-365)) } })
                .Group(new BsonDocument{
                    { "_id",
                        new BsonDocument("$dateToString",
                        new BsonDocument
                        {
                            { "format", "%V" },
                            { "date", "$createdAt" }
                        })
                    },
                { "count",
                    new BsonDocument("$sum", 1) }
                })
                .ToListAsync();
            string s = Result.ToJson();
            return JsonSerializer.Deserialize<List<Week>>(s)!;
        }
    }
}
