using System.Collections.Generic;
using SportsStore.Models;
using SportsStore.Models.Pages;

namespace SportsStore.Data
{
    public class CategoryRepository :ICategoryRepository
    {
        private readonly DataContext _context;
        public CategoryRepository(DataContext ctx) => _context = ctx;
        public IEnumerable<Category> Categories => _context.Categories;

        public PagedList<Category> GetCategories(QueryOptions options)
        {
            return new PagedList<Category>(_context.Categories, options);
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }
        public void DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}
