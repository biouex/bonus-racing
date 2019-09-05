using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BonusRacing.Api.Models.Users
{
    public class ChangePasswordModel
    {
        public Guid Id { get; set; }
        public string Password { get; set; }
    }
}