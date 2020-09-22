using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LanguageFeatures.Models;


namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
       
        public ViewResult Index()
        {
            var products = new[]
            {
                   new{ Name="Kayak", Price = 275M},
                   new{ Name="Lifejacket", Price = 48.95M},
                   new{ Name="Soccer ball", Price = 19.50M},
                   new{ Name="Corner flag", Price = 34.95M},
               };
            return View(products.Select(p => 
            $"{nameof(p.Name)}:{p.Name},{nameof(p.Price)}: {p.Price}"));
            /*
            List<string> output = new List<string>();
            await foreach(long? len in MyAsyncMethods.GetPageLengths(output, "apress.com", "microsoft.com", "amazon.com"))
            {
                output.Add($"Page length:{len}");
            }
            return View(output);
            */


            //IProductSelection cart = new ShoppingCart (
            //    new Product { Name = "Kayak", Price = 275M },
            //    new Product { Name = "Lifejacket", Price = 48.95M},
            //    new Product { Name = "Soccer ball", Price = 19.50M},
            //    new Product { Name = "Corner flag", Price = 34.95M}
            //);
            //return View(cart.Names);
            
            //Product[] productArray =
            //{
            //    new Product{Name="Kayak", Price= 275M},
            //    new Product{Name="Lifejacket", Price=48.95M},
            //    new Product {Name = "Soccer ball", Price = 19.50M},
            //    new Product {Name = "Corner flag", Price = 34.95M}
            //};


            //decimal priceFilterTotal = productArray.Filter(p => (p?.Price ?? 0) >= 20).TotalPrices();
            //decimal nameFilterTotal = productArray.Filter(p => (p?.Name?[0] == 'S')).TotalPrices();
    

            //return View("Index", new string[] {
            //    $"Price Total: {priceFilterTotal:C2}",
            //    $"Name Total: {nameFilterTotal:C2}"});
            
        }
    }
}
