using BonusRacing.DataDomain.Entities;
using BonusRacing.DataDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusRacing.DataAccess.Services
{
    public class GameDataService
    {
        private readonly IGameDataRepository _gameDataRepository;
        private readonly IGameRoundRepository _gameRoundRepository;
	    private readonly GameDataMapper _gameDataMapper;

	    public GameDataService(
            IGameDataRepository gameDataRepository,
            IGameRoundRepository gameRoundRepository,
			GameDataMapper gameDataMapper)
        {
            _gameDataRepository = gameDataRepository;
            _gameRoundRepository = gameRoundRepository;
            _gameDataMapper = gameDataMapper;
        }

        public async Task ReceivingGameData(DecodedGameData decodedGameData)
        {
            var activeRound = await _gameRoundRepository.GetActive();
            if (activeRound == null || !activeRound.Start.HasValue)
                return;
            var nowTime = DateTime.UtcNow;
            if ((activeRound.Pause.HasValue) ||
                (activeRound.Start > nowTime) ||
                (activeRound.End < nowTime))
                return;
            var player = activeRound.Players.FirstOrDefault(p => p.PlayerId.Equals(decodedGameData.IdenCardId));
            if (player == null)
                return;
            var encodedGameData = _gameDataMapper.Map(decodedGameData);
            await _gameDataRepository.Add(encodedGameData);
        }
    }
}
