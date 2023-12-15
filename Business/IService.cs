using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IService<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T model);
        void Update(T model);
        void Delete(int id);
    }
}
