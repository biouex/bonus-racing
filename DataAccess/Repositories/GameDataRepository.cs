using BonusRacing.DataDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BonusRacing.DataDomain.Entities;
using MongoDB.Driver;

namespace BonusRacing.DataAccess.Repositories
{
    public class GameDataRepository : BaseRepository, IGameDataRepository
    {
        public GameDataRepository(MongoDatabaseProvider mongoDatabaseProvider)
            : base(mongoDatabaseProvider)
        { }

        public async Task Add(EncodedGameData gameData)
        {
            var collection = GetCollection<EncodedGameData>(Collections.EncodedGameData);
            await collection.InsertOneAsync(gameData);
        }

        public async Task<IEnumerable<EncodedGameData>> GetAll()
        {
            var collection = GetCollection<EncodedGameData>(Collections.EncodedGameData);
            return await collection.Find(Builders<EncodedGameData>.Filter.Empty).ToListAsync();
        }

        public async Task Clear()
        {
            var collection = GetCollection<EncodedGameData>(Collections.EncodedGameData);
            await collection.DeleteManyAsync(Builders<EncodedGameData>.Filter.Empty);
        }

        public async Task<IEnumerable<RatingItem>> GetRating()
        {
            var collection = GetCollection<EncodedGameData>(Collections.EncodedGameData);
            var batch = await collection.FindAsync(Builders<EncodedGameData>.Filter.Empty);
            var data = await batch.ToListAsync();
            return data.GroupBy(d => d.IdenCardId)
                .Select(group => {
                    return new RatingItem
                    {
                        PlayerId = group.Key,
                        Points = Math.Round(group.Sum(i => i.EarnedPoints), 2)
                    };
                })
                .OrderByDescending(i => i.Points);
        }
    }
}
