using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class CartLine
    {
        public int CartLineID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();
        public void AddItem(Product product, int quantity)
        {
            //Create a card line and check if the product is already in the Lines
            CartLine line = Lines
                .Where(p => p.Product.ProductID == product.ProductID)
                .FirstOrDefault();
            //if not, add it to Lines
            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Product=product,
                    Quantity = quantity
                });
            }
            //Otherwise
            else
            {
                line.Quantity += quantity;
            }
        }
        public void RemoveLine(Product product) =>
            Lines.RemoveAll(l => l.Product.ProductID == product.ProductID);
        public decimal ComputeTotalValue() =>
            Lines.Sum(e => e.Product.Price * e.Quantity);
        public void Clear() =>Lines.Clear();
    }

}
