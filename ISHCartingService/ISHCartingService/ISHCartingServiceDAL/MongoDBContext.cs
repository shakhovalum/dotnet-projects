using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using ISHCartingService.ISHCartingServiceModels;
//using ISHCartingService.ISHCartingServiceDAL;
//using Microsoft.Extensions.Configuration;


namespace ISHCartingService.ISHCartingServiceDAL
{
    public class MongoDBContext
    {
        private IMongoDatabase _database;

        public MongoDBContext(string connectionString = "")
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _database = client.GetDatabase("CartingServiceDB");
        }

        public IMongoCollection<Cart> Carts => _database.GetCollection<Cart>("Carts");
    }
}
