using BonusRacing.DataDomain.Common;
using BonusRacing.DataDomain.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;


namespace BonusRacing.DataAccess.Repositories
{
    public class Repository<T, TM> : BaseRepository, IRepository<T, TM> where T : IEntity<TM>
    {
        private readonly Collections _collectionName;

        public Repository(MongoDatabaseProvider databaseProvider, Collections collectionName) : base(databaseProvider)
        {
            _collectionName = collectionName;
        }

        private IMongoCollection<T> Entityes => GetCollection<T>(_collectionName);

        public virtual async Task Add(T item)
        {
            await Entityes.InsertOneAsync(item);
        }

        public virtual async Task Delete(TM id)
        {
            await Entityes.DeleteOneAsync(x => x.Id.Equals(id));
        }

        public virtual async Task Update(T item)
        {
            await Entityes.ReplaceOneAsync(x => x.Id.Equals(item.Id), item);
        }

        public virtual async Task<T> Get(TM id)
        {
            return await Entityes.AsQueryable().SingleOrDefaultAsync(x => x.Id.Equals(id));
        }

        public virtual async Task<IReadOnlyList<T>> GetList(Expression<Func<T, bool>> predicate)
        {
            return await Entityes.AsQueryable().Where(predicate).ToListAsync();
        }

        public virtual async Task<T> GetSingle(Expression<Func<T, bool>> predicate)
        {
            return await Entityes.AsQueryable().SingleOrDefaultAsync(predicate);
        }

        public virtual async Task<IReadOnlyList<T>> GetAll()
        {
            return await Entityes.AsQueryable().ToListAsync();
        }
    }
}
