using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AbBus.DAL
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
         IEnumerable<T> Get(
             Expression<Func<T, bool>> filter = null,
             Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
             string includeProperties = "");
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Delete(T entityToDelete);
        void Save();
    }
}
