using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BonusRacing.DataDomain.Repositories
{
    public interface ISystemUserRepository : IRepository<SystemUser, Guid>
    {
    }
}
