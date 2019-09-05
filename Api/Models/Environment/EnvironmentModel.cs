using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BonusRacing.Api.Models.Environment
{
    public class EnvironmentModel
    {
        public string UserName { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}