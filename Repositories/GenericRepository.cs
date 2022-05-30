using Microsoft.EntityFrameworkCore;
using Repositories.Interface;
using System.Linq.Expressions;

namespace Repositories.Generic
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbContext Context;
        private DbSet<TEntity> DbSet;
        public GenericRepository(DbContext Context)
        {
            this.Context = Context;
            DbSet = Context.Set<TEntity>();
        }

        public async Task ClearAsync()
        {
            DbSet.RemoveRange(DbSet);
            await Context.SaveChangesAsync();

        }
        public async Task DeleteAsync(int Id)
        {
            DbSet.Remove(await DbSet.FindAsync(Id));
            await Context.SaveChangesAsync();
        }
        public async Task DeleteAsync(TEntity Item)
        {
            Context.Entry(Item).State = EntityState.Deleted;
            await Context.SaveChangesAsync();
        }
        public async Task AddAsync(TEntity Item)
        {
            DbSet.Add(Item);
            await Context.SaveChangesAsync();
        }
        public async Task ModifyAsync(int Id, TEntity Item)
        {
            Context.Entry<TEntity>(await Context.Set<TEntity>().FindAsync(Id)).CurrentValues.SetValues(Item);
            await Context.SaveChangesAsync();
        }
        public async Task<TEntity> GetAsync(int Id)
        {
            return await DbSet.FindAsync(Id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.Where(predicate).ToListAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
