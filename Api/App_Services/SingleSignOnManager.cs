using BonusRacing.Api.IdentityManager;
using BonusRacing.DataAccess.Services;
using BonusRacing.DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BonusRacing.Api.Helpers;
using BonusRacing.DataDomain.Common;

namespace BonusRacing.Api.App_Services
{
    public class SingleSignOnManager
    {
        private readonly SystemUserManager _systemUserManager;

        public SingleSignOnManager(
            SystemUserManager systemUserManager)

        {
            _systemUserManager = systemUserManager;
        }

        public async Task<SystemUser> Authorization(string username, string password)
        {
            string passwordHash = SystemUserService.CalculatePasswordHash(password);
            return await _systemUserManager.AuthorizeOperator(username, passwordHash);
        }
    }
}