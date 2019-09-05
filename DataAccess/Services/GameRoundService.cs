using BonusRacing.DataDomain.Entities;
using BonusRacing.DataDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusRacing.DataAccess.Services
{
    public class GameRoundService
    {
        private readonly IGameRoundRepository _gameRoundRepository;
        private readonly IArchivalGameRoundRepository _archivalGameRoundRepository;
        private readonly IGameDataRepository _gameDataRepository;

        public GameRoundService(
            IGameRoundRepository gameRoundRepository,
            IArchivalGameRoundRepository archivalGameRoundRepository,
            IGameDataRepository gameDataRepository)
        {
            _gameRoundRepository = gameRoundRepository;
            _archivalGameRoundRepository = archivalGameRoundRepository;
            _gameDataRepository = gameDataRepository;
        }

        public async Task<GameRound> Get(Guid id)
        {
            return await _gameRoundRepository.Get(id);
        }

        public async Task<IEnumerable<GameRound>> Get()
        {
            return await _gameRoundRepository.GetAll();
        }

        public async Task<GameRound> GetActive()
        {
            return await _gameRoundRepository.GetActive();
        }

        public async Task Create(string name, int duration, Guid? copyBy)
        {
            GameRound round = new GameRound
            {
                Id = Guid.NewGuid(),
                Name = name,
                Created = DateTime.UtcNow,
                Players = new List<Player>(),
                Duration = duration,
                State = GameRoundState.Draft,
            };
            if (copyBy.HasValue)
            {
                var copyByRound = await _gameRoundRepository.Get(copyBy.Value);
                if (copyByRound != null && copyByRound.Players != null)
                {
                    round.Players = copyByRound.Players;
                    round.Duration = copyByRound.Duration;
                }
            }
            await _gameRoundRepository.Add(round);
        }

        public async Task Update(Guid id, string name, int duration)
        {
            var round = await _gameRoundRepository.Get(id);
            round.Name = name;
            round.Duration = duration;
            await _gameRoundRepository.Update(round);
        }

        public async Task AddPlayer(Guid roundId, Player player)
        {
            var round = await _gameRoundRepository.Get(roundId);
            if (round == null)
            {
                throw new Exception("Раунд не найден");
            }
            if (!round.Players.Any(p => p.PlayerId == player.PlayerId))
            {
                var players = round.Players.ToList();
                players.Add(player);
                round.Players = players;
                await _gameRoundRepository.Update(round);
            }
        }

        public async Task RemovePlayer(Guid roundId, Player player)
        {
            var round = await _gameRoundRepository.Get(roundId);
            if (round == null)
            {
                throw new Exception("Раунд не найден");
            }
            if (round.State == GameRoundState.Active && round.Start.HasValue)
            {
                var rating = await _gameDataRepository.GetRating();
                if (rating.Any(r => r.PlayerId == player.PlayerId && r.Points > 0))
                {
                    throw new Exception("Нельзя удалить игрока из запущенного раунда, если по нему пришли данные!");
                }
            }
            if (round.Players.Any(p => p.PlayerId == player.PlayerId))
            {
                var players = round.Players.ToList();
                var playerInList = players.Find(p => p.PlayerId == player.PlayerId);
                players.Remove(playerInList);
                round.Players = players;
                await _gameRoundRepository.Update(round);
            }
        }

        public async Task SetActiveRound(Guid roundId)
        {
            var activeRound = await _gameRoundRepository.GetActive();
            if (activeRound != null)
            {
                if (activeRound.State != GameRoundState.Archived)
                {
                    throw new Exception("Уже имеется активный раунд");
                }
                await _gameRoundRepository.Delete(activeRound.Id);
                await _gameDataRepository.Clear();
            }
            var round = await _gameRoundRepository.Get(roundId);
            round.State = GameRoundState.Active;
            await _gameRoundRepository.Update(round);
        }

        public async Task SetDraftRound()
        {
            var activeRound = await _gameRoundRepository.GetActive();
            if (activeRound == null)
                throw new Exception("Нет активного раунда");
            activeRound.State = GameRoundState.Draft;
            await _gameRoundRepository.Update(activeRound);
        }

        public async Task StartActiveRound()
        {
            var active = await _gameRoundRepository.GetActive();
            if (active == null)
                throw new Exception("Нет активного раунда");
            if (active.Start.HasValue)
                throw new Exception("Раунд уже был запущен");

            var now = DateTime.UtcNow;
            active.Start = now;
            active.End = now.Add(TimeSpan.FromMinutes(active.Duration));
            await _gameRoundRepository.Update(active);
        }

        public async Task CompleteActiveRound()
        {
            var active = await _gameRoundRepository.GetActive();
            if (active == null)
                throw new Exception("Нет активного раунда");
            if (!active.Start.HasValue)
                throw new Exception("Раунд еще не был запущен");
            active.End = DateTime.UtcNow;
            active.State = GameRoundState.Archived;
            active.Pause = null;
            await _gameRoundRepository.Update(active);

            var gameData = await _gameDataRepository.GetAll();
            var archival = new ArchivalGameRound<EncodedGameData>
            {
                Id = active.Id,
                Name = active.Name,
                Players = active.Players,
                Created = active.Created,
                Start = active.Start.Value,
                End = active.End.Value,
                Duration = active.Duration,
                GameData = gameData,
            };
            await _archivalGameRoundRepository.Add(archival);
        }

        public async Task Pause()
        {
            var active = await _gameRoundRepository.GetActive();
            if (active == null)
                throw new Exception("Нет активного раунда");
            if (!active.Start.HasValue)
                throw new Exception("Раунд еще не запущен");
            if (active.End.HasValue && active.End < DateTime.UtcNow)
                throw new Exception("Раунд уже завершен");
            if (active.Pause.HasValue)
                throw new Exception("Раунд уже на паузе");
            active.Pause = DateTime.Now;
            await _gameRoundRepository.Update(active);
        }

        public async Task Continue()
        {
            var active = await _gameRoundRepository.GetActive();
            if (active == null) {
                throw new Exception("Нет активного раунда");
            }
            if (!active.Pause.HasValue) {
                throw new Exception("Раунд не на паузе");
            }

            var remainingTime = DateTime.UtcNow - active.Pause.Value;
            active.End = active.End.Value.Add(remainingTime);
            active.Pause = null;

            await _gameRoundRepository.Update(active);
        }

        public async Task<IEnumerable<RatingItem>> GetRating()
        {
            var active = await _gameRoundRepository.GetActive();
            if (active == null)
            {
                throw new Exception("Нет активного раунда");
            }
            var ratings = await _gameDataRepository.GetRating();
            var ratingList = ratings.ToList();
            foreach (var player in active.Players)
            {
                if (!ratings.Any(r => r.PlayerId == player.PlayerId))
                {
                    ratingList.Add(new RatingItem { PlayerId = player.PlayerId, Points = 0 });
                }
            }
            return ratingList;
        }
    }
}
