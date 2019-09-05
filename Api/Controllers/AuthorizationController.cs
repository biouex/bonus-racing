using BonusRacing.Api.App_Services;
using BonusRacing.Api.App_Start;
using BonusRacing.Api.Helpers;
using BonusRacing.Api.IdentityManager;
using BonusRacing.Api.Models.Authorization;
using BonusRacing.DataDomain;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BonusRacing.Api.Controllers
{
    [RoutePrefix("authorization")]
    public class AuthorizationController : ApiController
    {
        private const int TicketLifetimeHours = 24;
        private const int DemoAccessTokenLifetime = 24;
        private readonly SingleSignOnManager _singleSignOnManager;
        private readonly SystemUserManager _systemUserManager;

        public AuthorizationController(
            SystemUserManager systemUserManager,
            SingleSignOnManager singleSignOnManager)
        {
            _systemUserManager = systemUserManager;
            _singleSignOnManager = singleSignOnManager;
        }

        [Route("Authorize")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> AuthorizeEmployee(AuthorizationUserModel model)
        {
            var loginBytes = Convert.FromBase64String(model.Login);
            var login = System.Text.Encoding.ASCII.GetString(loginBytes);
            var passwordBytes = Convert.FromBase64String(model.Password);
            var password = System.Text.Encoding.ASCII.GetString(passwordBytes);

            var user = await _singleSignOnManager.Authorization(login, password);
            if (user == null)
                return NotFound();

            return await GenerateTokenResponse(user, TimeSpan.FromHours(TicketLifetimeHours));
        }

        [Route("DemoToken")]
        [HttpGet]
        [Authorize]
        [RolesAuthorize(Roles.RoundStart)]
        public async Task<IHttpActionResult> GetDemoToken()
        {
            var user = new SystemUser(Guid.Empty)
            {
                FirstName = "Demo",
                LastName = "",
                PasswordHash = "",
                UserName = "demo",
                Roles = new string[] { Roles.DemoAccess }
            };
            return await GenerateTokenResponse(user, TimeSpan.FromHours(DemoAccessTokenLifetime));
        }


        private async Task<IHttpActionResult> GenerateTokenResponse(SystemUser user, TimeSpan ticketLifetime)
        {
            var identity = await _systemUserManager.GenerateUserIdentityAsync(user, OAuthDefaults.AuthenticationType);

            var properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
            var ticket = new AuthenticationTicket(identity, properties);
            ticket.Properties.ExpiresUtc = DateTime.UtcNow.Add(ticketLifetime);
            var token = AuthConfig.OAuthOptions.AccessTokenFormat.Protect(ticket);

            return Json(new { token }, DefaultSerializerSettings.Instance);
        }
    }
}
