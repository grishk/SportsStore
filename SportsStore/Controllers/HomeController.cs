using Microsoft.AspNetCore.Mvc;
using SportsStore.Data;
using SportsStore.Models;
using SportsStore.Models.Pages;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index(QueryOptions options)
        {
            return View(_productRepository.GetProducts(options));
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _productRepository.AddProduct(product);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateProduct(long key)
        {
            ViewBag.Categories = _categoryRepository.Categories;
            return View(key == 0 ? new Product() : _productRepository.GetProduct(key));
        }
        
        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            if (product.Id == 0)
            {
                _productRepository.AddProduct(product);
            }
            else
            {
                _productRepository.UpdateProduct(product);
            }
            return RedirectToAction(nameof(Index));
        }

        //public IActionResult UpdateAll()
        //{
        //    ViewBag.UpdateAll = true;
        //    return View(nameof(Index), _productRepository.GetProducts());
        //}

        [HttpPost]
        public IActionResult UpdateAll(Product[] products)
        {
            _productRepository.UpdateAll(products);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            _productRepository.Delete(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
