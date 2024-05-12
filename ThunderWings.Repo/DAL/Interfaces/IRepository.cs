using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ThunderWings.Repo.DAL.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> Add(T entity);
        Task Update(T entity);
        Task<T> Get(int id, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> All();
        Task Delete(T entity);
        Task SaveChanges();
    }
}
