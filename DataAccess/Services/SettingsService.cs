using BonusRacing.DataDomain.Common.Settings;
using BonusRacing.DataDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusRacing.DataAccess.Services
{
    public class SettingsService
    {
        private readonly ISettingsRepository _settingsRepository;

        public SettingsService(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        private static bool? LogEnable;

        public async Task<bool> GetLogEnable()
        {
            if (!LogEnable.HasValue)
            {
                var model = await _settingsRepository.Get<LogSettings>();
                if (model == null)
                {
                    LogEnable = false;
                }
                else
                {
                    LogEnable = model.RequestLogEnable;
                }
            }
            return LogEnable.Value;

        }
    }
}
