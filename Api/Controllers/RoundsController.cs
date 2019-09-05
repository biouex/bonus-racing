using BonusRacing.Api.Helpers;
using BonusRacing.Api.Models;
using BonusRacing.Api.Models.Rounds;
using BonusRacing.DataAccess.Services;
using BonusRacing.DataDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
namespace BonusRacing.Api.Controllers
{
    [Authorize]
    [RoutePrefix("rounds")]
    public class RoundsController : ApiController
    {
        private readonly GameRoundService _gameRoundService;

        public RoundsController(GameRoundService gameRoundService)
        {
            _gameRoundService = gameRoundService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<GameRound>> GetRounds()
        {
            return await _gameRoundService.Get();
        }


        [HttpGet]
        [Route("{id:guid}")]
        public async Task<GameRound> Get(Guid id)
        {
            return await _gameRoundService.Get(id);
        }

        [HttpGet]
        [Route("active")]
        public async Task<GameRound> GetActive()
        {
            return await _gameRoundService.GetActive();
        }

        [RolesAuthorize(Roles.RoundEdit)]
        [HttpPost]
        [Route("")]
        public async Task Create(RoundCreateModel model)
        {
            await _gameRoundService.Create(model.Name, model.Duration, model.CopyBy);
        }

        [RolesAuthorize(Roles.RoundEdit)]
        [HttpPost]
        [Route("update")]
        public async Task Update(RoundUpdateModel model)
        {
            await _gameRoundService.Update(model.id, model.Name, model.Duration);
        }

        [RolesAuthorize(Roles.RoundEdit)]
        [HttpPost]
        [Route("addPlayer")]
        public async Task AddPlayer(RoundChangePlayerModel model)
        {
            await _gameRoundService.AddPlayer(model.Id, model.Player);
        }

        [RolesAuthorize(Roles.RoundEdit)]
        [HttpPost]
        [Route("deletePlayer")]
        public async Task DeletePlayer(RoundChangePlayerModel model)
        {
            await _gameRoundService.RemovePlayer(model.Id, model.Player);
        }

        [RolesAuthorize(Roles.RoundStart)]
        [HttpPost]
        [Route("setActive")]
        public async Task SetActiveRound(IdModel model)
        {
            await _gameRoundService.SetActiveRound(model.Id);
        }

        [RolesAuthorize(Roles.RoundStart)]
        [HttpPost]
        [Route("setDraft")]
        public async Task SetDraftRound()
        {
            await _gameRoundService.SetDraftRound();
        }

        [RolesAuthorize(Roles.RoundStart)]
        [HttpPost]
        [Route("start")]
        public async Task Start()
        {
            await _gameRoundService.StartActiveRound();
        }

        [RolesAuthorize(Roles.RoundStart)]
        [HttpPost]
        [Route("complete")]
        public async Task Complete()
        {
            await _gameRoundService.CompleteActiveRound();
        }

        [RolesAuthorize(Roles.RoundStart)]
        [HttpPost]
        [Route("pause")]
        public async Task Pause()
        {
            await _gameRoundService.Pause();
        }

        [RolesAuthorize(Roles.RoundStart)]
        [HttpPost]
        [Route("continue")]
        public async Task Continue()
        {
            await _gameRoundService.Continue();
        }

        [HttpGet]
        [Route("rating")]
        public async Task<IEnumerable<RatingItem>> GetRating()
        {
            return await _gameRoundService.GetRating();
        }

    }
}