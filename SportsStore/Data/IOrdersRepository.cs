using System.Collections.Generic;
using SportsStore.Models;

namespace SportsStore.Data
{
    public interface IOrdersRepository : IRepository
    {
        IEnumerable<Order> Orders { get; }
        Order GetOrder(long key);
        void AddOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
    }
}
