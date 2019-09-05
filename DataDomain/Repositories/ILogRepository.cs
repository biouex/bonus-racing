using BonusRacing.DataDomain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusRacing.DataDomain.Repositories
{
    public interface ILogRepository : IRepository<LogRequest, Guid>
    {
    }
}
