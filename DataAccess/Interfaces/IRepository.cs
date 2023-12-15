using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] children);
        T GetById(int id);
        void Add(T model);
        void Update(T model);
        void Delete(int id);
        void Save();
    }
}
