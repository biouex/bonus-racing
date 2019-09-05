using BonusRacing.DataDomain;
using BonusRacing.DataDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BonusRacing.DataAccess.Services
{
    public class SystemUserService
    {
        private const string passwordSecret = "BHTD002MONBXm5MNxPqW";

        private readonly ISystemUserRepository _systemUserRepository;

        public SystemUserService(ISystemUserRepository systemUserRepository)
        {
            _systemUserRepository = systemUserRepository;
        }

        public async Task Add(SystemUser systemUser)
        {
            await _systemUserRepository.Add(systemUser);
        }

        public async Task<SystemUser> Get(Guid id)
        {
            return await _systemUserRepository.Get(id);
        }

        public async Task<IEnumerable<SystemUser>> GetList()
        {
            return await _systemUserRepository.GetList(user => user.Status == DataDomain.Common.EntityStatus.Active);
        }

        public async Task Update(SystemUser systemUser)
        {
            await _systemUserRepository.Update(systemUser);
        }

        public async Task Delete(Guid id)
        {
            var user = await _systemUserRepository.Get(id);
            if (user != null)
            {
                user.Status = DataDomain.Common.EntityStatus.Deleted;
                await _systemUserRepository.Update(user);
            }
        }

        public async Task ChangePassword(Guid id, string password)
        {
            var user = await _systemUserRepository.Get(id);
            user.PasswordHash = CalculatePasswordHash(password);
            await _systemUserRepository.Update(user);
        }

        public static string CalculatePasswordHash(string content)
        {
            using (var hasher = new SHA256Managed())
            {
                var bytes = Encoding.UTF8.GetBytes(content + passwordSecret);
                var actualHashBytes = hasher.ComputeHash(bytes);
                var actualHashBuilder = new StringBuilder();
                foreach (var theByte in actualHashBytes)
                {
                    actualHashBuilder.Append(theByte.ToString("x2"));
                }
                return actualHashBuilder.ToString();
            }
        }
    }
}
