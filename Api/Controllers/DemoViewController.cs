using BonusRacing.Api.Helpers;
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
    [RolesAuthorize(Roles.DemoAccess)]
    [RoutePrefix("demoview")]
    public class DemoViewController : ApiController
    {
        private readonly GameRoundService _gameRoundService;

        public DemoViewController(
            GameRoundService gameRoundService)
        {
            _gameRoundService = gameRoundService;
        }

        [HttpGet]
        [Route("")]
        public async Task<GameRound> Get()
        {
            return await _gameRoundService.GetActive();
        }

        [HttpGet]
        [Route("rating")]
        public async Task<IEnumerable<RatingItem>> GetRating()
        {
            return await _gameRoundService.GetRating();
        }
    }
}
