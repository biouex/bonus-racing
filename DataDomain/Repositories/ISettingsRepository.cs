using BonusRacing.DataDomain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusRacing.DataDomain.Repositories
{
    public interface ISettingsRepository
    {
        Task<T> Get<T>() where T : ISettings, new();
        Task Update<T>(T entity) where T : ISettings, new();
    }
}
