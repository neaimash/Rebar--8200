﻿using MongoDB.Driver;
using Rebar.Model;
using Rebar.Models;

namespace Rebar.Data
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<MenuShake> MenuShakes => _database.GetCollection<MenuShake>("Menu");
        public IMongoCollection<Order> Orders => _database.GetCollection<Order>("Orders");
        public IMongoCollection<Account> Accounts => _database.GetCollection<Account>("Accounts");   

    }
}
