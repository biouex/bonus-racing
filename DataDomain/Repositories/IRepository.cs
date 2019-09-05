using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BonusRacing.DataDomain.Repositories
{
    public interface IRepository<T, in TM>
    {
        Task Add(T entity);
        Task Delete(TM id);
        Task Update(T entity);
        Task<IReadOnlyList<T>> GetList(Expression<Func<T, bool>> predicate);
        Task<T> Get(TM id);
        Task<T> GetSingle(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetAll();
    }
}
