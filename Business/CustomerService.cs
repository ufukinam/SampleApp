using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class CustomerService : ICustomerService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository, IMemoryCache memoryCache)
        {
            _customerRepository = customerRepository;
            _memoryCache = memoryCache;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            var cacheData = _memoryCache.Get<IEnumerable<Customer>>("customers");
            if (cacheData != null)
            {
                return cacheData;
            }
            var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
            var customers = _customerRepository.GetCustomers();
            cacheData = customers;
            _memoryCache.Set("customers", cacheData, expirationTime);
            return customers;
        }

        public Customer GetCustomerById(int id)
        {
            var cacheData = _memoryCache.Get<IEnumerable<Customer>>("customers").Where(x=>x.Id==id).FirstOrDefault();
            if (cacheData != null)
            {
                return cacheData;
            }
            return _customerRepository.GetCustomerById(id);
        }

        public void AddCustomer(Customer customer)
        {
            _memoryCache.Remove("customers");
            _customerRepository.AddCustomer(customer);
            _customerRepository.Save();
        }

        public void DeleteCustomer(int id)
        {
            _memoryCache.Remove("customers");
            _customerRepository.DeleteCustomer(id);
            _customerRepository.Save();
        }

        public void UpdateCustomer(Customer customer)
        {
            _memoryCache.Remove("customers");
            _customerRepository.UpdateCustomer(customer);
            _customerRepository.Save();
        }
    }
}
