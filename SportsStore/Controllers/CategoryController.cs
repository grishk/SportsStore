using Microsoft.AspNetCore.Mvc;
using SportsStore.Data;
using SportsStore.Models;
using SportsStore.Models.Pages;

namespace SportsStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _repository;
        public CategoryController(ICategoryRepository repo) => _repository = repo;
        public IActionResult Index(QueryOptions options)
            => View(_repository.GetCategories(options));
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            _repository.AddCategory(category);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult EditCategory(long id)
        {
            ViewBag.EditId = id;
            return View("Index", _repository.Categories);
        }
        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            _repository.UpdateCategory(category);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult DeleteCategory(Category category)
        {
            _repository.DeleteCategory(category);
            return RedirectToAction(nameof(Index));
        }
    }
}