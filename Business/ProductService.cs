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
    public class ProductService : IService<Product>
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IRepository<Product> _repository;

        public ProductService(IRepository<Product> repository, IMemoryCache memoryCache)
        {
            _repository = repository;
            _memoryCache = memoryCache;
        }

        public void Add(Product model)
        {
            _memoryCache.Remove("products");
            _repository.Add(model);
            _repository.Save();
        }

        public void Delete(int id)
        {
            _memoryCache.Remove("products");
            _repository.Delete(id);
            _repository.Save();
        }

        public IEnumerable<Product> GetAll()
        {
            var cacheData = _memoryCache.Get<IEnumerable<Product>>("products");
            if (cacheData != null)
            {
                return cacheData;
            }
            var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
            var products = _repository.GetAll();
            cacheData = products;
            _memoryCache.Set("products", cacheData, expirationTime);
            return products;
        }

        public Product GetById(int id)
        {
            var cacheData = _memoryCache.Get<IEnumerable<Product>>("products").Where(x => x.Id == id).FirstOrDefault();
            if (cacheData != null)
            {
                return cacheData;
            }
            return _repository.GetById(id);
        }

        public void Update(Product model)
        {
            _memoryCache.Remove("products");
            _repository.Update(model);
            _repository.Save();
        }
    }
}
