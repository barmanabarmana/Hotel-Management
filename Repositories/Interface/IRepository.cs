using System.Linq.Expressions;

namespace Repositories.Interface
{
    public interface IRepository<TEntity> : IDisposable
    {
        Task ClearAsync();
        Task DeleteAsync(int Id);
        Task DeleteAsync(TEntity Entity);
        Task AddAsync(TEntity Entity);
        Task ModifyAsync(int Id, TEntity NewItem);
        Task<TEntity> GetAsync(int Id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
