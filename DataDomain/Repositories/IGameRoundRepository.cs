using BonusRacing.DataDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusRacing.DataDomain.Repositories
{
    public interface IGameRoundRepository : IRepository<GameRound, Guid>
    {
        Task<GameRound> GetActive();
    }
}
