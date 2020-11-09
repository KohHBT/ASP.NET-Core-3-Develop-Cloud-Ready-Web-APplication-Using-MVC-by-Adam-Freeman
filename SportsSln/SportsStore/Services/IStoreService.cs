using SportsStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Services
{
    public interface IStoreService
    {
        IQueryable<Product> GetAllProducts();
        IEnumerable<Product> GetProductByName(string Name = null);
    }
}