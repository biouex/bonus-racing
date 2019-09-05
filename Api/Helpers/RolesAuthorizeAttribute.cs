using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace BonusRacing.Api.Helpers
{
    public class RolesAuthorizeAttribute : AuthorizeAttribute
    {
        public RolesAuthorizeAttribute(params string[] roles)
        {
            Roles = string.Join(",", roles);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;
            if (principal == null)
            {
                base.HandleUnauthorizedRequest(actionContext);
                return;
            }

            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
        }
    }
}