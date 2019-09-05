using BonusRacing.DataDomain;
using BonusRacing.DataDomain.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BonusRacing.Api.IdentityManager
{
    public class SystemUserStore : IUserStore<SystemUser>
    {
        private readonly ISystemUserRepository _usersRepository;

        public SystemUserStore(ISystemUserRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }


        public async Task CreateAsync(SystemUser user)
        {
            await _usersRepository.Add(user);
        }

        public async Task UpdateAsync(SystemUser user)
        {
            await _usersRepository.Update(user);
        }

        public async Task DeleteAsync(SystemUser user)
        {
            await _usersRepository.Delete(user.Id);
        }

        public async Task<SystemUser> FindByIdAsync(string userId)
        {
            var id = Guid.Parse(userId);
            return await _usersRepository.Get(id);

        }

        public async Task<SystemUser> FindByNameAsync(string userName)
        {
            var user = await _usersRepository.GetSingle(u => u.UserName == userName);
            return user;

        }

        public void Dispose()
        {

        }
    }
}