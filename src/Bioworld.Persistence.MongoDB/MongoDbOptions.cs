﻿namespace Bioworld.Persistence.MongoDB
{
    using System.ComponentModel;

    public class MongoDbOptions
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public bool Seed { get; set; }
        
        [Description("Might be helpful for the integration testing.")]
        public bool SetRandomDatabaseSuffix { get; set; }
    }
}