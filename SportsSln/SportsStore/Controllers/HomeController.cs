using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore.Models.ViewModels;
using SportsStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStoreService _service;
        //Number of product per page
        public int PageSize = 4;

        public HomeController(IStoreService Service)
        {
            _service = Service;
        }
        //Original way
        //public ViewResult Index(string category, int productPage = 1)
        //{
        //    return View(new ProductsListViewModel
        //    {
        //        Products = _service.GetAllProducts()
        //        //Order by the product ID
        //        .OrderBy(p => p.ProductID)
        //        .Where(p => p.Category != null || p.Category == category)
        //        //Skip the number of product per page multiplies by the number of page has skipped
        //        //For example we're on page 2, we will skip 4 products on page 1
        //        .Skip((productPage - 1) * PageSize)
        //        //Take the number of product according to PageSize
        //        //for example page 2: it will be translate as: Skip 4 and take 4
        //        .Take(PageSize),

        //        PagingInfo = new PagingInfo
        //        {
        //            CurrentPage = productPage,
        //            ItemsPerPage = PageSize,
        //            TotalItems = _service.GetAllProducts().Count()
        //        },
        //        CurrentCategory = category

        //    });
        //}



            //Shorter way
            public ViewResult Index(string searchTerm, string category, int productPage = 1)
                => View(new ProductsListViewModel
                {
                    Products = _service.GetProductByName(searchTerm)
                    .OrderBy(p => p.ProductID)
                    .Where(p => p.Category == category || category == null)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),

                    PagingInfo = new PagingInfo
                    {
                        TotalItems = category == null
                            ? _service.GetAllProducts().Count()
                            : _service.GetAllProducts().Where( p => p.Category == category).Count(),
                        CurrentPage = productPage,
                        ItemsPerPage = PageSize
                    },
                    CurrentCategory = category
                });
    }
                        
}
