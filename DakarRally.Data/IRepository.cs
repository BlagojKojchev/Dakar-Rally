using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DakarRally.Data
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> AllInclude(params Expression<Func<T, object>>[] includeProperties);

        IEnumerable<T> GetAll();

        IEnumerable<T> FindByTracking(Expression<Func<T, bool>> predicate);

        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

        IEnumerable<T> FindByInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        T FindByKey<TKey>(TKey code);

        void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete<TKey>(TKey code);
    }
}
