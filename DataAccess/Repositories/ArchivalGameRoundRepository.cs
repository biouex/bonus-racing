using BonusRacing.DataDomain.Entities;
using BonusRacing.DataDomain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusRacing.DataAccess.Repositories
{
    public class ArchivalGameRoundRepository : Repository<ArchivalGameRound<EncodedGameData>, Guid>, IArchivalGameRoundRepository
    {
        public ArchivalGameRoundRepository(MongoDatabaseProvider provider)
            : base (provider, Collections.ArchivalRounds)
        { }

        //public async Task<IEnumerable<ArchivalGameRound>> Get(int page, int count)
        //{
        //    var collection = GetCollection<ArchivalGameRound>(Collections.ArchivalRounds);
        //    return await collection.AsQueryable().OrderByDescending(r => r.End).Skip(page * count).Take(count).ToListAsync();
        //}

        public async Task<IEnumerable<ArchivalGameRoundListItem>> GetList(int page, int count)
        {
            var collection = GetCollection<ArchivalGameRound<EncodedGameData>>(Collections.ArchivalRounds);
            return await collection.Find(Builders<ArchivalGameRound<EncodedGameData>>.Filter.Empty)
                .Project<ArchivalGameRoundListItem>(Builders<ArchivalGameRound<EncodedGameData>>.Projection
                    .Include(r => r.Id)
                    .Include(r => r.Name)
                    .Include(r => r.End)
                )
                .SortByDescending(r => r.End)
                .Skip(page * count)
                .Limit(count)
                .ToListAsync();
        }

        public async Task<long> GetCount()
        {
            var collection = GetCollection<ArchivalGameRound<EncodedGameData>>(Collections.ArchivalRounds);
            return await collection.CountAsync(Builders<ArchivalGameRound<EncodedGameData>>.Filter.Empty);
        }
    }
}
