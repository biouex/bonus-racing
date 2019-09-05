using BonusRacing.Api.Models.Users;
using BonusRacing.DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BonusRacing.Api.Mappers
{
    public class UserMapper
    {
        public User Map(SystemUser user)
        {
            return new User
            {
               Id = user.Id,
               UserName = user.UserName,
               FirstName = user.FirstName,
               LastName = user.LastName,
               Roles = user.Roles
            };
        }

        public SystemUser Map(UserCreateModel model, Func<string, string> passwordHashCalculator)
        {
            return new SystemUser(Guid.NewGuid())
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Roles = model.Roles,
                Status = DataDomain.Common.EntityStatus.Active,
                PasswordHash = passwordHashCalculator(model.Password)
            };
        }
    }
}