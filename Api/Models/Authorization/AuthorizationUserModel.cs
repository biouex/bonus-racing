﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BonusRacing.Api.Models.Authorization
{
    public class AuthorizationUserModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}