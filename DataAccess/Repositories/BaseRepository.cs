using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusRacing.DataAccess.Repositories
{
    public class BaseRepository
    {
        private MongoDatabaseProvider _mongoDatabaseProvider;

        public BaseRepository(MongoDatabaseProvider mongoDatabaseProvider)
        {
            _mongoDatabaseProvider = mongoDatabaseProvider;
        }
        

        protected IMongoCollection<T> GetCollection<T>(Collections collections)
        {
            var collectionName = DatabaseCollections.Get(collections);
            return _mongoDatabaseProvider.Database.GetCollection<T>(collectionName);
        }

        public IMongoCollection<BsonDocument> GetCollection(Collections collection)
        {
            var collectionName = DatabaseCollections.Get(collection);
            return _mongoDatabaseProvider.Database.GetCollection<BsonDocument>(collectionName);
        }
    }
}
