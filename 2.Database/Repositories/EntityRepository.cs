using Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class EntityRepository<T> : IEntityRepository<T>
        where T : class
    {
        private readonly IEntities _context;

        public EntityRepository(IEntities context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public void CommitChanges()
        {
            _context.SaveChanges();
        }

        public void DeleteOnCommit(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public T Get(int key)
        {
            return _context.Set<T>().Find(key);
        }

        public T InsertOnCommit(T entity)
        {
            _context.Set<T>().Add(entity);

            return entity;
        }

        public T Get(string key)
        {
            return _context.Set<T>().Find(key);
        }

        public T Reload(T entity)
        {
            _context.Entry(entity).Reload();
            return entity;
        }
    }
}
