using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

namespace SportsStore.Data
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly DataContext _context;
        public OrdersRepository(DataContext ctx) => _context = ctx;
        public IEnumerable<Order> Orders => _context.Orders
            .Include(o => o.Lines).ThenInclude(l => l.Product);
        public Order GetOrder(long key) => _context.Orders
            .Include(o => o.Lines).First(o => o.Id == key);
        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }
        public void DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }
    }
}
