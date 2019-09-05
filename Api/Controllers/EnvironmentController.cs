using BonusRacing.Api.Models.Environment;
using BonusRacing.DataAccess.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace BonusRacing.Api.Controllers
{
    [Authorize]
    [RoutePrefix("environment")]
    public class EnvironmentController : ApiController
    {
        private readonly SystemUserService _systemUserService;

        public EnvironmentController(
            SystemUserService systemUserService)
        {
            _systemUserService = systemUserService;
        }

        [Route("")]
        [HttpGet]
        public async Task<EnvironmentModel> Get()
        {
            var userId = ((ClaimsPrincipal)User).Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = await _systemUserService.Get(Guid.Parse(userId));
            return new EnvironmentModel
            {
                UserName = user.FirstName + " " + user.LastName,
                Roles = user.Roles
            };
        }
    }
}
