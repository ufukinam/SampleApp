using Business;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            var customers =  _customerService.GetCustomers();
            return View(customers);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Customer model)
        {
            if (ModelState.IsValid)
            {
                _customerService.AddCustomer(model);
                return RedirectToAction("Index", "Customer");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Customer model = _customerService.GetCustomerById(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Update(Customer model)
        {
            if (ModelState.IsValid) { 
                _customerService.UpdateCustomer(model);
                return RedirectToAction("Index", "Customer");
            }
            return View();
        }
        [HttpGet]
        public ActionResult DeleteCustomer(int id)
        {
            Customer model = _customerService.GetCustomerById(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _customerService.DeleteCustomer(id);
            return RedirectToAction("Index", "Customer");
        }
    }
}
