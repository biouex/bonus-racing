using BonusRacing.DataDomain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusRacing.DataDomain.Entities
{
    public enum GameRoundState {
        Draft = 0,
        Active = 1,
        Archived = 2
    };

    public class GameRound : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Player> Players { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public GameRoundState State { get; set; }
        public DateTime? Pause { get; set; }
        public int Duration { get; set; }
    }
}
