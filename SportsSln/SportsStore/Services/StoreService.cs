using SportsStore.Models;
using SportsStore.MyDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Services
{
    public class StoreService : IStoreService
    {
        private readonly StoreDbContext _context;

        public StoreService(StoreDbContext Context)
        {
            _context = Context;
        }
        //Original way
        //public IQueryable<Product> GetAllProducts()
        //{
        //    return _context.Tbl_Products;
        //}
        //Shorter way
        public IQueryable<Product> GetAllProducts() => _context.Tbl_Products;

        public IEnumerable<Product> GetProductByName(string Name = null)
        {
            return from p in _context.Tbl_Products
                   where string.IsNullOrEmpty(Name) || p.Name.StartsWith(Name)
                   orderby p.Name
                   select p;
        }
    }
}
