using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace Business
{
    public class CustomerService : IService<Customer>
    {
        private readonly IRepository<Customer> _repository;

        public CustomerService(IRepository<Customer> repository, IMemoryCache memoryCache)
        {
            _repository = repository;
        }

        public IEnumerable<Customer> GetAll()
        {
            var customers = _repository.GetAll(null,null);
            return customers;
        }

        public Customer GetById(int id)
        {
            return _repository.GetById(id,null);
        }

        public void Add(Customer customer)
        {
            _repository.Add(customer);
            _repository.Save();
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
            _repository.Save();
        }

        public void Update(Customer customer)
        {
            _repository.Update(customer);
            _repository.Save();
        }
    }
}
