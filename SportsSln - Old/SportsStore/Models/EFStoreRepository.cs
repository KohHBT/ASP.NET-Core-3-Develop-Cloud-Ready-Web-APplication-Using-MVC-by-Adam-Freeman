using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    //This is the implementation of IStoreRepository
    public class EFStoreRepository : IStoreRepository
    {
        //A property to get dbcontext
        private StoreDbContext context;
        //Constructor to initialize dbcontext
        public EFStoreRepository(StoreDbContext ctx)
        {
            context = ctx;
        }
        //Implement the Products method in IStoreRepository interface, which uses the dbcontext to query all the products
        public IQueryable<Product> Products => context.Products;
    }
}
