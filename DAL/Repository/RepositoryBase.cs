using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace WarehouseProProject.DAL.Repository
{
    public class RepositoryBase<T> where T : class
    {
        protected readonly WarehouseContext _context;
        private DbSet<T> _dbSet;

        public RepositoryBase(WarehouseContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }
        public void DeleteAll<T>() where T : class
        {
            var dbSet = _context.Set<T>();
            dbSet.RemoveRange(dbSet.ToList());
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            T entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}