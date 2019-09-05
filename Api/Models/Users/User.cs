using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BonusRacing.Api.Models.Users
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public IReadOnlyList<string> Roles { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}