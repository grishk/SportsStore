using Microsoft.AspNetCore.Mvc;
using SportsStore.Data;
using SportsStore.Models.Pages;

namespace SportsStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public StoreController(IProductRepository prepo, ICategoryRepository catRepo)
        {
            _productRepository = prepo;
            _categoryRepository = catRepo;
        }
        public IActionResult Index([FromQuery(Name = "options")]
            QueryOptions productOptions,
            QueryOptions catOptions,
            long category)
        {
            ViewBag.Categories = _categoryRepository.GetCategories(catOptions);
            ViewBag.SelectedCategory = category;
            return View(_productRepository.GetProducts(productOptions, category));
        }
    }
}