﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Bioworld.Persistence.MongoDB
{
    using System;
    using Persistence.MongoDB.Builders;
    using Persistence.MongoDB.Factories;
    using Persistence.MongoDB.Initializers;
    using Persistence.MongoDB.Repositories;
    using Persistence.MongoDB.Seeders;
    using Types;
    using Microsoft.Extensions.DependencyInjection;

    public static class Extensions
    {
        // Helpful when dealing with integration testing
        private static bool _conventionsRegistered;
        private const string SectionName = "mongo";
        private const string RegistryName = "persistence.mongoDb";

        public static IBioWorldBuilder AddMongo(this IBioWorldBuilder builder, string sectionName = SectionName,
            Type seederType = null, bool registerConventions = true)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
            {
                sectionName = SectionName;
            }

            var mongoOptions = builder.GetOptions<MongoDbOptions>(sectionName);
            return builder.AddMongo(mongoOptions, seederType, registerConventions);
        }

        public static IBioWorldBuilder AddMongo(this IBioWorldBuilder builder, Func<IMongoDbOptionsBuilder,
            IMongoDbOptionsBuilder> buildOptions, Type seederType = null, bool registerConventions = true)
        {
            var mongoOptions = buildOptions(new MongoDbOptionsBuilder()).Build();
            return builder.AddMongo(mongoOptions, seederType, registerConventions);
        }

        public static IBioWorldBuilder AddMongo(this IBioWorldBuilder builder, MongoDbOptions mongoOptions,
            Type seederType = null, bool registerConventions = true)
        {
            if (!builder.TryRegister(RegistryName))
            {
                return builder;
            }

            if (mongoOptions.SetRandomDatabaseSuffix)
            {
                var suffix = $"{Guid.NewGuid():N}";
                Console.WriteLine($"Setting a random MongoDB database suffix: '{suffix}'.");
                mongoOptions.Database = $"{mongoOptions.Database}_{suffix}";
            }

            builder.Services.AddSingleton(mongoOptions);
            builder.Services.AddSingleton<IMongoClient>(sp =>
            {
                var options = sp.GetService<MongoDbOptions>();
                return new MongoClient(options.ConnectionString);
            });
            builder.Services.AddTransient(sp =>
            {
                var options = sp.GetService<MongoDbOptions>();
                var client = sp.GetService<IMongoClient>();
                return client.GetDatabase(options.Database);
            });
            builder.Services.AddTransient<IMongoDbInitializer, MongoDbInitializer>();
            builder.Services.AddTransient<IMongoSessionFactory, MongoSessionFactory>();

            if (seederType is null)
            {
                builder.Services.AddTransient<IMongoDbSeeder, MongoDbSeeder>();
            }
            else
            {
                builder.Services.AddTransient(typeof(IMongoDbSeeder), seederType);
            }

            builder.AddInitializer<IMongoDbInitializer>();
            if (registerConventions && !_conventionsRegistered)
            {
                RegisterConventions();
            }

            return builder;
        }

        private static void RegisterConventions()
        {
            _conventionsRegistered = true;
            BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
            BsonSerializer.RegisterSerializer(typeof(decimal?),
                new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));
            ConventionRegistry.Register("convey", new ConventionPack
            {
                new CamelCaseElementNameConvention(),
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(BsonType.String),
            }, _ => true);
        }

        public static IBioWorldBuilder AddMongoRepository<TEntity, TIdentifiable>(this IBioWorldBuilder builder,
            string collectionName)
            where TEntity : IIdentifiable<TIdentifiable>
        {
            builder.Services.AddTransient<IMongoRepository<TEntity, TIdentifiable>>(sp =>
            {
                var database = sp.GetService<IMongoDatabase>();
                return new MongoRepository<TEntity, TIdentifiable>(database, collectionName);
            });

            return builder;
        }
    }
}