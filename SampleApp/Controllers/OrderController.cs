using Business;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SampleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IService<Order> _orderService;
        private readonly IService<Product> _productService;
        private readonly IService<Customer> _customerService;

        public OrderController(IService<Order> orderService, IService<Product> productService, IService<Customer> customerService)
        {
            _orderService = orderService;
            _productService = productService;
            _customerService = customerService;
        }

        [Authorize]
        public IActionResult Index()
        {
            var orders =  _orderService.GetAll();
            return View(orders);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var products = _productService.GetAll();
            var customers = _customerService.GetAll();
            List<SelectListItem> selectProduct = products.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            List<SelectListItem> selectCustomer = customers.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            
            ViewData["Products"] = selectProduct;
            ViewData["Customers"] = selectCustomer;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Order model)
        {
            if (ModelState.IsValid)
            {
                _orderService.Add(model);
                return RedirectToAction("Index", "Order");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var products = _productService.GetAll();
            var customers = _customerService.GetAll();
            List<SelectListItem> selectProduct = products.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            List<SelectListItem> selectCustomer = customers.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();

            ViewData["Products"] = selectProduct;
            ViewData["Customers"] = selectCustomer;

            Order model = _orderService.GetById(id);


            return View(model);
        }
        [HttpPost]
        public IActionResult Update(Order model)
        {
            if (ModelState.IsValid)
            {
                _orderService.Update(model);
                return RedirectToAction("Index", "Order");
            }
            return View();
        }
        [HttpGet]
        public ActionResult DeleteOrder(int id)
        {
            Order model = _orderService.GetById(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            _orderService.Delete(id);
            return RedirectToAction("Index", "Order");
        }
    }
}
