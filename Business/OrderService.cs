using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace Business
{
    public class OrderService : IService<Order>
    {
        private readonly IRepository<Order> _repository;

        public OrderService(IRepository<Order> repository, IMemoryCache memoryCache)
        {
            _repository = repository;
        }

        public void Add(Order model)
        {
            _repository.Add(model);
            _repository.Save();
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
            _repository.Save();
        }

        public IEnumerable<Order> GetAll()
        {
            var orders = _repository.GetAll(null,
            r => r.Product, r=>r.Customer);
            return orders;
        }

        public Order GetById(int id)
        {
            return _repository.GetById(id,null);
        }

        public void Update(Order model)
        {
            _repository.Update(model);
            _repository.Save();
        }
    }
}
