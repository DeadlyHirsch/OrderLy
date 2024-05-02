﻿namespace NotableApi.Models
{
    public class NotableDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string NotesCollectionName { get; set; } = null!;

        public string TagsCollectionName { get; set; } = null!;

        public string UserCollectionName { get; set; } = null!;
    }
}
