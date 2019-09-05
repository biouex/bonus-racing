using BonusRacing.DataDomain.Common;
using BonusRacing.DataDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusRacing.DataAccess.Repositories
{
    public class LogRepository : Repository<LogRequest, Guid>, ILogRepository
    {
        public LogRepository(MongoDatabaseProvider databaseProvider)
            : base(databaseProvider, Collections.LogRequest)
        {
        }
    }
}
