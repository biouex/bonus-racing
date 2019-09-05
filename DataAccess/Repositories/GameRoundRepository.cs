using BonusRacing.DataDomain.Entities;
using BonusRacing.DataDomain.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;

namespace BonusRacing.DataAccess.Repositories
{
    public class GameRoundRepository : Repository<GameRound, Guid>, IGameRoundRepository
    {
        public GameRoundRepository(MongoDatabaseProvider provider)
            : base (provider, Collections.Rounds)
        { }

        public override async Task<IReadOnlyList<GameRound>> GetAll()
        {
            var collection = GetCollection<GameRound>(Collections.Rounds);
            return await collection.AsQueryable().Where(r => r.State != GameRoundState.Archived).OrderByDescending(r => r.Created).ToListAsync();
        }

        public async Task<GameRound> GetActive()
        {
            var collection = GetCollection<GameRound>(Collections.Rounds);
            var batch = await collection.FindAsync(Builders<GameRound>.Filter.Ne(r => r.State, GameRoundState.Draft));
            return batch.SingleOrDefault();
        }
    }
}
