using BonusRacing.DataDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusRacing.DataDomain.Repositories
{
    public interface IArchivalGameRoundRepository : IRepository<ArchivalGameRound<EncodedGameData>, Guid>
    {
        Task<IEnumerable<ArchivalGameRoundListItem>> GetList(int page, int count);
        Task<long> GetCount();
    }
}
