using System.Collections.Generic;
using SportsStore.Models;
using SportsStore.Models.Pages;

namespace SportsStore.Data
{
    public interface IProductRepository : IRepository
    {
        PagedList<Product> GetProducts(QueryOptions options, long category = 0);
        IEnumerable<Product> Products { get; }
        void AddProduct(Product product);
        Product GetProduct(long key);
        void UpdateProduct(Product product);
        void UpdateAll(Product[] products);
        void Delete(Product product);
    }
}
