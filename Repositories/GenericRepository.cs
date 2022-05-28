using DAL;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
        public async Task Add(TEntity Item)
        {
            DbSet.Add(Item);
            await Context.SaveChangesAsync();
        }
        public async Task ModifyAsync(int Id, TEntity Item)
        {
            Context.Entry(await Context.Set<TEntity>().FindAsync(Id)).CurrentValues.SetValues(Item);
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

        public IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DbSet;
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).ToList();
        }

        public IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = DbSet.Where(predicate).AsQueryable();
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).ToList();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
