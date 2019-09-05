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
    [RolesAuthorize(Roles.RoundEdit)]
    [Authorize]
    [RoutePrefix("archive")]
    public class ArchivalRoundsController : ApiController
    {
        private readonly ArchivalRoundService _archivalRoundService;

        public ArchivalRoundsController(
            ArchivalRoundService archivalRoundService)
        {
            _archivalRoundService = archivalRoundService;
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<ArchivalGameRound<DecodedGameData>> Get(Guid id)
        {
            return await _archivalRoundService.Get(id);
        }

        [HttpGet]
        [Route("list")]
        public async Task<IEnumerable<ArchivalGameRoundListItem>> GetList(int page, int count)
        {
            return await _archivalRoundService.GetList(page, count);
        }

        [HttpGet]
        [Route("count")]
        public async Task<long> GetCount()
        {
            return await _archivalRoundService.GetCount();
        }
    }
}
