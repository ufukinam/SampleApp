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
        private readonly IService<Customer> _customerService;
        public CustomerController(IService<Customer> customerService)
        {
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            var customers =  _customerService.GetAll();
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
                _customerService.Add(model);
                return RedirectToAction("Index", "Customer");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Customer model = _customerService.GetById(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Update(Customer model)
        {
            if (ModelState.IsValid) { 
                _customerService.Update(model);
                return RedirectToAction("Index", "Customer");
            }
            return View();
        }
        [HttpGet]
        public ActionResult DeleteCustomer(int id)
        {
            Customer model = _customerService.GetById(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _customerService.Delete(id);
            return RedirectToAction("Index", "Customer");
        }
    }
}
