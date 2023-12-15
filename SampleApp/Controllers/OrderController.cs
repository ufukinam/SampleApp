using Business;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IService<Order> _orderService;

        public OrderController(IService<Order> orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var orders = _orderService.GetAll();
            return View(orders);
        }

        [HttpGet]
        public ActionResult Create()
        {
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
