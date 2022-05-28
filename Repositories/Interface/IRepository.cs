using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IRepository<TEntity> : IDisposable
    {
        Task ClearAsync();
        Task DeleteAsync(int Id);
        Task DeleteAsync(TEntity Entity);
        Task Add(TEntity Entity);
        Task ModifyAsync(int Id, TEntity NewItem);
        Task<TEntity> GetAsync(int Id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
