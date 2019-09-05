using BonusRacing.DataDomain.Common;
using BonusRacing.DataDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace BonusRacing.DataAccess.Repositories
{
    public class SettingsRepository : BaseRepository, ISettingsRepository
    {
        public SettingsRepository(MongoDatabaseProvider databaseProvider)
            : base(databaseProvider)
        { }

        public async Task<T> Get<T>() where T : ISettings, new()
        {
            var collection = GetCollection(Collections.Settings);
            var type = typeof(T);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", type.Name);
            var document = await collection.Find(filter).FirstOrDefaultAsync();
            return document != null
                ? BsonSerializer.Deserialize<T>(document.GetValue("value").AsBsonDocument)
                : default(T);
        }

        public async Task Update<T>(T entity) where T : ISettings, new()
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var collection = GetCollection(Collections.Settings);
            var type = typeof(T);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", type.Name);
            var update = Builders<BsonDocument>.Update.Set("value", entity.ToBsonDocument());
            await collection.UpdateOneAsync(filter, update);
        }
    }
}
