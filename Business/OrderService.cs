using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class OrderService : IService<Order>
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IRepository<Order> _repository;

        public OrderService(IRepository<Order> repository, IMemoryCache memoryCache)
        {
            _repository = repository;
            _memoryCache = memoryCache;
        }

        public void Add(Order model)
        {
            _memoryCache.Remove("orders");
            _repository.Add(model);
            _repository.Save();
        }

        public void Delete(int id)
        {
            _memoryCache.Remove("orders");
            _repository.Delete(id);
            _repository.Save();
        }

        public IEnumerable<Order> GetAll()
        {
            var cacheData = _memoryCache.Get<IEnumerable<Order>>("orders");
            if (cacheData != null)
            {
                return cacheData;
            }
            var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
            var orders = _repository.GetAll(null,
            r => r.Product, r=>r.Customer);
            cacheData = orders;
            _memoryCache.Set("orders", cacheData, expirationTime);
            return orders;
        }

        public Order GetById(int id)
        {
            var cacheData = _memoryCache.Get<IEnumerable<Order>>("orders").Where(x => x.Id == id).FirstOrDefault();
            if (cacheData != null)
            {
                return cacheData;
            }
            return _repository.GetById(id);
        }

        public void Update(Order model)
        {
            _memoryCache.Remove("orders");
            _repository.Update(model);
            _repository.Save();
        }
    }
}
