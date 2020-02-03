using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models
{
    public class Cart
    {
        private readonly List<OrderLine> _selections = new List<OrderLine>();
        public Cart AddItem(Product p, int quantity)
        {
            OrderLine line = _selections.FirstOrDefault(l => l.ProductId == p.Id);
            if (line != null)
            {
                line.Quantity += quantity;
            }
            else
            {
                _selections.Add(new OrderLine
                {
                    ProductId = p.Id,
                    Product = p,
                    Quantity = quantity
                });
            }
            return this;
        }
        public Cart RemoveItem(long productId)
        {
            _selections.RemoveAll(l => l.ProductId == productId);
            return this;
        }
        public void Clear() => _selections.Clear();
        public IEnumerable<OrderLine> Selections { get => _selections; }
    }
}
