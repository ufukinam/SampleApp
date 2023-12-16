using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public class Repository<T> : IRepository<T> 
        where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<T> _dbSet;
        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public void Add(T model)
        {
            _dbSet.Add(model);
        }

        public void Delete(int id)
        {
            T entityToDelete = _dbSet.Find(id);

            if (entityToDelete != null)
                _dbSet.Remove(entityToDelete);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] children)
        {
            //return _dbSet.ToList();
            if (children != null)
                children.ToList().ForEach(x => _dbSet.Include(x).Load());

            return _dbSet.AsEnumerable<T>();
        }

        public T GetById(int id, params Expression<Func<T, object>>[] children)
        {
            if (children != null)
                children.ToList().ForEach(x => _dbSet.Include(x).Load());
            return _dbSet.Find(id);
        }

        public void Update(T model)
        {
            _dbContext.Entry(model).State = EntityState.Modified;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
