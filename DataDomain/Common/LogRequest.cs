using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusRacing.DataDomain.Common
{
    public class LogRequest : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Uri { get; set; }
        public string Method { get; set; }
        public string Headers { get; set; }
        public string Content { get; set; }
    }
}
