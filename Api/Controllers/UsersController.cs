using BonusRacing.Api.Helpers;
using BonusRacing.Api.Mappers;
using BonusRacing.Api.Models.Users;
using BonusRacing.DataAccess.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BonusRacing.Api.Controllers
{
    [RolesAuthorize(Roles.UserManagement)]
    [Authorize]
    [RoutePrefix("users")]
    public class UsersController : ApiController
    {
        private readonly SystemUserService _systemUserService;
        private readonly UserMapper _userMapper;

        public UsersController(SystemUserService systemUserService, UserMapper userMapper)
        {
            _systemUserService = systemUserService;
            _userMapper = userMapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<User>> GetList()
        {
            var systemUsers = await _systemUserService.GetList();
            return systemUsers.Select(systemUser => _userMapper.Map(systemUser));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<User> Get(Guid id)
        {
            var systemUser = await _systemUserService.Get(id);
            return _userMapper.Map(systemUser);
        }

        [HttpPost]
        [Route("")]
        public async Task Create(UserCreateModel model)
        {
            var systemUser = _userMapper.Map(model, SystemUserService.CalculatePasswordHash);
            await _systemUserService.Add(systemUser);
        }

        [HttpPost]
        [Route("update")]
        public async Task Update(User model)
        {
            var user = await _systemUserService.Get(model.Id);
            user.UserName = model.UserName;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Roles = model.Roles;
            await _systemUserService.Update(user);
        }

        [HttpPost]
        [Route("{id:guid}")]
        public async Task Delete([FromUri] Guid id)
        {
            await _systemUserService.Delete(id);
        }

        [HttpPost]
        [Route("changePassword")]
        public async Task ChangePassword(ChangePasswordModel model)
        {
            var passwordBytes = Convert.FromBase64String(model.Password);
            var password = System.Text.Encoding.ASCII.GetString(passwordBytes);
            await _systemUserService.ChangePassword(model.Id, password);
        }
    }
}
