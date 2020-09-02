using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace AbBus.DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AbBusEntities _context = null;
        private readonly DbSet<T> table = null;
        public GenericRepository()
        {
            this._context = new AbBusEntities();
            table = _context.Set<T>();
        }
        public GenericRepository(AbBusEntities _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public virtual IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = table;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        public T GetById(object id)
        {
            return table.Find(id);
        }
        public void Insert(T obj)
        {
            table.Add(obj);
            Save(); // reflects the changes in database
        }
        public void Update(T entityToUpdate)
        {
            table.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
            Save(); // reflects the changes in database
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
            Save(); // reflects the changes in database
        }

        public virtual void Delete(T entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                table.Attach(entityToDelete);
            }
            table.Remove(entityToDelete);
            Save(); // reflects the changes in database
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
