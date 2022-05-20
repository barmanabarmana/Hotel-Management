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
        void Clear();
        void Delete(int Id);
        void Delete(TEntity Entity);
        void Add(TEntity Entity);
        void Modify(int Id, TEntity NewItem);
        TEntity Get(int Id);
        TEntity GetByPosition(int Position);
        List<TEntity> GetAll();
        List<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties);
        List<TEntity> GetAll(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
