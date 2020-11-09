using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Services;

namespace SportsStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IStoreService _storeService;

        public NavigationMenuViewComponent(IStoreService StoreService)
        {
            _storeService = StoreService;
        }
        
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(_storeService.GetAllProducts()
                        .Select(x => x.Category)
                        .Distinct()
                        .OrderBy(x => x));
        }
    }
}
