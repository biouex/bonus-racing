using BonusRacing.DataDomain;
using BonusRacing.DataDomain.Common;
using BonusRacing.DataDomain.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusRacing.DataAccess.Repositories
{
    public class SystemUserRepository : Repository<SystemUser, Guid>, ISystemUserRepository
    {
        public SystemUserRepository(MongoDatabaseProvider databaseProvider) : base(databaseProvider, Collections.Users)
        {
        }

        public override async Task Delete(Guid id)
        {
            var collection = GetCollection<SystemUser>(Collections.Users);
            await collection.UpdateOneAsync(
                Builders<SystemUser>.Filter.Eq(u => u.Id, id),
                Builders<SystemUser>.Update.Set(u => u.Status, EntityStatus.Deleted)
                    .Set(u => u.Roles, new string[0]));
        }
    }
}
