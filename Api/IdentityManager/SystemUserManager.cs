using BonusRacing.DataDomain;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace BonusRacing.Api.IdentityManager
{
    public class SystemUserManager : UserManager<SystemUser>
    {
        public SystemUserManager(SystemUserStore store) : base(store)
        {
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(SystemUser user, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var identity = await CreateIdentityAsync(user, authenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Email, user.UserName));

            foreach (var role in user.Roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return identity;
        }

        public async Task<SystemUser> AuthorizeOperator(string login, string passwordHash)
        {
            var user = await Store.FindByNameAsync(login);
            if (user != null)
            {
                if (user.PasswordHash == passwordHash)
                    return user;
            }
            return null;
        }
    }
}