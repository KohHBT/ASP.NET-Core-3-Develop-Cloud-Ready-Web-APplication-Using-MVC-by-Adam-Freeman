using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore.Migrations;
using Moq;
using SportsStore.Components;
using SportsStore.Models;
using SportsStore.Services;
using Xunit;

namespace SportsStore.Tests
{
    public class NavigationMenuViewComponentTests
    {
        [Fact]
        public void Can_Select_Categories()
        {
            //Arrange: Create a mocj IstoreService 
            //using Setup, call the GetAllProduct() method but return the mock array of products as desired
            Mock<IStoreService> mock = new Mock<IStoreService>();
            mock.Setup(m => m.GetAllProducts()).Returns((new Product[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Apples"},
                new Product {ProductID = 2, Name = "P2", Category = "Apples"},
                new Product {ProductID = 3, Name = "P3", Category = "Plums"},
                new Product {ProductID = 4, Name = "P4", Category = "Oranges"},
            }).AsQueryable());

            //Create an instance of the view component so we can call the Invoke() method to test
            //pass the mock.Object method so the view component can get dependency injected
            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);

            //Act = get the set of categories
            //Call the invoke methodm the result will be a viewcomponentresult, cast it to ViewData.Model
            //Use explicit cast to cast the whole thing to IEnumerable<string> then convert it to an array
            string[] results = ((IEnumerable<string>)((target.Invoke() as ViewViewComponentResult)
                .ViewData.Model)).ToArray();

            //Assert
            Assert.True(Enumerable.SequenceEqual(new string[] { "Apples", "Oranges", "Plums" }, results));
        }

        [Fact]
        public void Indicates_Selected_Category()
        {
            //Arrange
            string categoryToSelect = "Apples";

            Mock<IStoreService> mock = new Mock<IStoreService>();
            mock.Setup(m => m.GetAllProducts()).Returns(
                (new Product[]
                    {
                        new Product { ProductID = 1, Name = "P1", Category = "Apples" },
                        new Product { ProductID = 4, Name = "P2", Category = "Oranges" },
                    }
                ).AsQueryable()
            );

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);
            target.ViewComponentContext = new ViewComponentContext
            {
                ViewContext = new ViewContext
                {
                    RouteData = new Microsoft.AspNetCore.Routing.RouteData()
                }
            };

            target.RouteData.Values["category"] = categoryToSelect;

            //ACt
            string result = (string)(target.Invoke() as ViewViewComponentResult).ViewData["SelectedCategory"];

            //Assert
            Assert.Equal(categoryToSelect, result);
        }
    }
}
