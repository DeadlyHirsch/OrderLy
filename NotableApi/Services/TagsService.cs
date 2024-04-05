using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NotableApi.Models;

namespace NotableApi.Services
{
    public class TagsService
    {
        private readonly IMongoCollection<NoteTag> _tagsCollection;

        public TagsService(
            IOptions<NotableDatabaseSettings> notableDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                notableDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                notableDatabaseSettings.Value.DatabaseName);

            _tagsCollection = mongoDatabase.GetCollection<NoteTag>(
                notableDatabaseSettings.Value.TagsCollectionName);
        }

        public async Task<List<NoteTag>> GetAsync() =>
            await _tagsCollection.Find(_ => true).ToListAsync();

        public async Task<NoteTag?> GetAsync(string id) =>
            await _tagsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(NoteTag newTag) =>
            await _tagsCollection.InsertOneAsync(newTag);

        public async Task UpdateAsync(string id, NoteTag updatedTag) =>
            await _tagsCollection.ReplaceOneAsync(x => x.Id == id, updatedTag);

        public async Task RemoveAsync(string id) =>
            await _tagsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
