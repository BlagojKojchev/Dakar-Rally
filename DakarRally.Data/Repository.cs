using DakarRally.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DakarRally.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DakarRallyContext context;
        private readonly DbSet<T> dbSet;

        public Repository(IDakarRallyContext context)
        {
            this.context = (DakarRallyContext)context;
            this.dbSet = this.context.Set<T>();
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return this.dbSet.AsNoTracking().Where(predicate).ToList();
        }


        public IEnumerable<T> FindByTracking(Expression<Func<T, bool>> predicate)
        {
            return this.dbSet.Where(predicate).ToList();
        }

        public IEnumerable<T> GetAll()
        {
            return this.dbSet.AsNoTracking().ToList();
        }

        public T FindByKey<TKey>(TKey code)
        {
            return this.dbSet.Find(code);
        }

        public IEnumerable<T> AllInclude(params Expression<Func<T, object>>[] includeProperties)
        {
            return GetAllIncluding(includeProperties).ToList();
        }

        public IEnumerable<T> FindByInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return GetAllIncluding(includeProperties).Where(predicate).ToList();
        }

        private IQueryable<T> GetAllIncluding(Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = this.dbSet;

            return includeProperties.Aggregate(
                query,
                (current, includeProperty) => current.Include(includeProperty)
            );
        }

        public void Insert(T entity)
        {
            this.dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            this.dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
                this.dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete<TKey>(TKey code)
        {
            var entity = FindByKey(code);
            if (entity != null)
                Delete(entity);
        }
    }
}
