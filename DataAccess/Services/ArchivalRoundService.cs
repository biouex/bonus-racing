using BonusRacing.DataDomain.Entities;
using BonusRacing.DataDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusRacing.DataAccess.Services
{
    public class ArchivalRoundService
    {
        private readonly IArchivalGameRoundRepository _archivalGameRoundRepository;
	    private readonly GameDataMapper _gameDataMapper;

	    public ArchivalRoundService(
            IArchivalGameRoundRepository archivalGameRoundRepository,
			GameDataMapper gameDataMapper)
	    {
		    _archivalGameRoundRepository = archivalGameRoundRepository;
            _gameDataMapper = gameDataMapper;
	    }

        public async Task<IEnumerable<ArchivalGameRoundListItem>> GetList(int page, int count)
        {
			return await _archivalGameRoundRepository.GetList(page, count);
		}

        public async Task<ArchivalGameRound<DecodedGameData>> Get(Guid id)
        {
			var round = await _archivalGameRoundRepository.Get(id);
            var decodedRound = new ArchivalGameRound<DecodedGameData>
            {
                Id = round.Id,
                Created = round.Created,
                Duration = round.Duration,
                Start = round.Start,
                End = round.Start,
                Name = round.Name,
                Players = round.Players
            };
            decodedRound.GameData = round.GameData.Select(gd => _gameDataMapper.Map((EncodedGameData)gd));
	        return decodedRound;
        }

        public async Task<long> GetCount()
        {
            return await _archivalGameRoundRepository.GetCount();
        }
    }
}
