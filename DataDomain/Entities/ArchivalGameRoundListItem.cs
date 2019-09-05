using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusRacing.DataDomain.Entities
{
    public class ArchivalGameRoundListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime End { get; set; }
    }
}
