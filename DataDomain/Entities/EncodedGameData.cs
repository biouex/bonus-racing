using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusRacing.DataDomain.Entities
{
    public class EncodedGameData : GameData
    {
        public byte[] FirstName { get; set; }
        public byte[] LastName { get; set; }
        public byte[] MiddleName { get; set; }
    }
}
