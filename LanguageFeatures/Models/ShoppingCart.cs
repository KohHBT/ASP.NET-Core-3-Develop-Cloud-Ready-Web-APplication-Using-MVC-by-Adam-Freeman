using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;

namespace LanguageFeatures.Models
{
    public class ShoppingCart : IProductSelection 
    {
        private List<Product> products = new List<Product>();
        public ShoppingCart(params Product[] prods)
        {
            products.AddRange(prods);
        }
        public IEnumerable<Product> Products { get => products; }
        //public IEnumerator<Product> GetEnumerator()
        //{
        //    return Products.GetEnumerator();
        //}
        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return GetEnumerator();
        //}

    }
}
