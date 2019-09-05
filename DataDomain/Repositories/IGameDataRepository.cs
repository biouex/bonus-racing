using BonusRacing.DataDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusRacing.DataDomain.Repositories
{
    public interface IGameDataRepository
    {
        Task Add(EncodedGameData gameData);
        Task<IEnumerable<EncodedGameData>> GetAll();
        Task Clear();
        Task<IEnumerable<RatingItem>> GetRating();
    }
}
