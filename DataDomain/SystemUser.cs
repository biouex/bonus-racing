using BonusRacing.DataDomain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace BonusRacing.DataDomain
{
    public class SystemUser : IUser, IEntity<Guid>
    {
        public SystemUser(Guid id)
        {
            Id = id;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EntityStatus Status { get; set; }
        public IReadOnlyList<string> Roles { get; set; }
        public Guid Id { get; set; }
        public string UserName { get; set; }
        string IUser<string>.Id => Id.ToString();
        public string PasswordHash { get; set; }
    }
}
