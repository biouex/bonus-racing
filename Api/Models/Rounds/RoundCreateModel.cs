using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BonusRacing.Api.Models.Rounds
{
    public class RoundCreateModel
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public Guid? CopyBy { get; set; }
    }
}