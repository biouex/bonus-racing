using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusRacing.DataAccess
{
    public class MongoDatabaseProvider
    {
        private readonly IMongoDatabase _database;

        public MongoDatabaseProvider(string connectionString, string dbName)
        {
            var client =  new MongoClient(connectionString);
            _database = client.GetDatabase(dbName);
        }

        public IMongoDatabase Database
        {
            get
            {
                return _database;
            }
        }
    }
}
