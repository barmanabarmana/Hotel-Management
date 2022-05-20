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

        public void Clear()
        {
            DbSet.RemoveRange(DbSet);
            Context.SaveChanges();

        }
        public void Delete(int Id)
        {
            DbSet.Remove(DbSet.Find(Id));
            Context.SaveChanges();
        }
        public void Delete(TEntity Item)
        {
            Context.Entry(Item).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        public void Add(TEntity Item)
        {
            DbSet.Add(Item);
            Context.SaveChanges();
        }
        public void Modify(int Id, TEntity Item)
        {
            Context.Entry(Context.Set<TEntity>().Find(Id)).CurrentValues.SetValues(Item);
            Context.SaveChanges();
        }
        public TEntity Get(int Id)
        {
            return DbSet.Find(Id);
        }

        public TEntity GetByPosition(int Position)
        {
            return DbSet.ToList()[Position];
        }

        public List<TEntity> GetAll()
        {
            return DbSet.AsNoTracking().ToList();
        }

        public List<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DbSet;
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).ToList();
        }

        public List<TEntity> GetAll(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
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
