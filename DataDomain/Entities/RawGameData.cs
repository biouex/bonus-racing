﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusRacing.DataDomain.Entities
{
    public class RawGameData
    {
        public Guid Id { get; set; }
        public DateTime ReceivingDate { get; set; }
        public int SessionId { get; set; }
        public long CardNumber { get; set; }
        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int IdenCardId { get; set; }
        public int MachId { get; set; }
        public DateTime UpdateDate { get; set; }
        public double EarnedPoints { get; set; }
    }
}
