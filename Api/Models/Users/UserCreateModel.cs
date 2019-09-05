using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BonusRacing.Api.Models.Users
{
    public class UserCreateModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public IReadOnlyList<string> Roles { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}