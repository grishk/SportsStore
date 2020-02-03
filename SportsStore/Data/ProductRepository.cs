using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;
using SportsStore.Models.Pages;

namespace SportsStore.Data 
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext ctx) => _context = ctx;

        public PagedList<Product> GetProducts(QueryOptions options,
            long category = 0)
        {
            IQueryable<Product> query = _context.Products.Include(p => p.Category);
            if (category != 0)
            {
                query = query.Where(p => p.CategoryId == category);
            }
            return new PagedList<Product>(query, options);
        }

        public IEnumerable<Product> Products => _context.Products;

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            Product p = GetProduct(product.Id);

            p.Name = product.Name;
            p.CategoryId = product.CategoryId;
            p.PurchasePrice = product.PurchasePrice;
            p.RetailPrice = product.RetailPrice;

            _context.SaveChanges();
        }

        public void UpdateAll(Product[] products)
        {
            IEnumerable<long> data = products.Select(p => p.Id);
            IEnumerable<Product> baseline = _context.Products.Where(p => data.Contains(p.Id));

            foreach (Product databaseProduct in baseline)
            {
                Product requestProduct = products.Single(x => x.Id == databaseProduct.Id);
                databaseProduct.Name = requestProduct.Name;
                databaseProduct.CategoryId = requestProduct.Category.Id;
                databaseProduct.PurchasePrice = requestProduct.PurchasePrice;
                databaseProduct.RetailPrice = requestProduct.RetailPrice;
            }
            _context.SaveChanges();
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public Product GetProduct(long key) => _context.Products
            .Include(p => p.Category).Single(p => p.Id == key);
    }
}
