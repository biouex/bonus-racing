using BonusRacing.DataDomain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusRacing.DataDomain.Entities
{
    public class ArchivalGameRound<T> : IEntity<Guid> where T:GameData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Player> Players { get; set; }
        public DateTime Created { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Duration { get; set; }
        public IEnumerable<T> GameData { get; set; }
    }
}
