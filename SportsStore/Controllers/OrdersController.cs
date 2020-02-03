using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Data;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrdersRepository _ordersRepository;
        public OrdersController(IProductRepository productRepo,
            IOrdersRepository orderRepo)
        {
            _productRepository = productRepo;
            _ordersRepository = orderRepo;
        }
        public IActionResult Index() => View(_ordersRepository.Orders);
        public IActionResult EditOrder(long id)
        {
            var products = _productRepository.Products;
            Order order = id == 0 ? new Order() : _ordersRepository.GetOrder(id);
            IDictionary<long, OrderLine> linesMap
                = order.Lines?.ToDictionary(l => l.ProductId)
                  ?? new Dictionary<long, OrderLine>();
            ViewBag.Lines = products.Select(p => linesMap.ContainsKey(p.Id)
                ? linesMap[p.Id]
                : new OrderLine { Product = p, ProductId = p.Id, Quantity = 0 });
            return View(order);
        }
        [HttpPost]
        public IActionResult AddOrUpdateOrder(Order order)
        {
            order.Lines = order.Lines
                .Where(l => l.Id > 0 || (l.Id == 0 && l.Quantity > 0)).ToArray();
            if (order.Id == 0)
            {
                _ordersRepository.AddOrder(order);
            }
            else
            {
                _ordersRepository.UpdateOrder(order);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult DeleteOrder(Order order)
        {
            _ordersRepository.DeleteOrder(order);
            return RedirectToAction(nameof(Index));
        }
    }
}