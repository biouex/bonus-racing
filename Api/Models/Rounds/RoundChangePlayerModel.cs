using BonusRacing.DataDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BonusRacing.Api.Models.Rounds
{
    public class RoundChangePlayerModel
    {
        public Guid Id { get; set; }
        public Player Player { get; set; }
    }
}