﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NotableApi.Models;

namespace NotableApi.Services
{
    public class NotesService
    {
        private readonly IMongoCollection<Note> _notesCollection;

        public NotesService(
            IOptions<NotableDatabaseSettings> notableDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                notableDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                notableDatabaseSettings.Value.DatabaseName);

            _notesCollection = mongoDatabase.GetCollection<Note>(
                notableDatabaseSettings.Value.NotesCollectionName);
        }

        public async Task<List<Note>> GetAsync() =>
            await _notesCollection.Find(_ => true).ToListAsync();

        public async Task<Note?> GetAsync(string id) =>
            await _notesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Note newNote) =>
            await _notesCollection.InsertOneAsync(newNote);

        public async Task UpdateAsync(string id, Note updatedNote) =>
            await _notesCollection.ReplaceOneAsync(x => x.Id == id, updatedNote);

        public async Task RemoveAsync(string id) =>
            await _notesCollection.DeleteOneAsync(x => x.Id == id);
    }
}
