using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
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
    }
}
