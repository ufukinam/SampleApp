using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace Business
{
    public class ProductService : IService<Product>
    {
        private readonly IRepository<Product> _repository;

        public ProductService(IRepository<Product> repository, IMemoryCache memoryCache)
        {
            _repository = repository;
        }

        public void Add(Product model)
        {
            _repository.Add(model);
            _repository.Save();
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
            _repository.Save();
        }

        public IEnumerable<Product> GetAll()
        {
            var products =  _repository.GetAll(null,null);
            return products;
        }

        public Product GetById(int id)
        {
            return _repository.GetById(id,null);
        }

        public void Update(Product model)
        {
            _repository.Update(model);
            _repository.Save();
        }
    }
}
