using System.Collections.Generic;
using SportsStore.Models;
using SportsStore.Models.Pages;

namespace SportsStore.Data
{
    public interface ICategoryRepository : IRepository
    {
        IEnumerable<Category> Categories { get; }
        PagedList<Category> GetCategories(QueryOptions options);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}
