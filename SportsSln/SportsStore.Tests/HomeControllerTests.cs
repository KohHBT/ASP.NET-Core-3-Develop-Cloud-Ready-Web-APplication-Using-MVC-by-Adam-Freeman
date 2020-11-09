using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using SportsStore.Services;
using System;
using System.Collections.Generic;
using System.Composition.Convention;
using System.Linq;
using System.Text;
using Xunit;

namespace SportsStore.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Can_Use_Repository()
        {
            //Arrange
            Mock<IStoreService> mock = new Mock<IStoreService>();
            mock.Setup(m => m.GetAllProducts()).Returns((new Product[]
            {
                new Product
                {
                    ProductID = 1,
                    Name = "P1"
                },
                new Product
                {
                    ProductID = 2,
                    Name = "P2"
                }
            }).AsQueryable<Product>());

            HomeController controller = new HomeController(mock.Object);

            //Act
            ProductsListViewModel result =
                (controller.Index(null) as ViewResult).ViewData.Model as ProductsListViewModel;

            //Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P1", prodArray[0].Name);
            Assert.Equal("P2", prodArray[1].Name);
        }

        [Fact]
        public void Can_Paginate()
        {
            //Arrange
            Mock<IStoreService> mock = new Mock<IStoreService>();
            mock.Setup(m => m.GetAllProducts()).Returns((new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}
            }).AsQueryable<Product>());

            //Showing 3 product per page
            HomeController controller = new HomeController(mock.Object)
            {
                PageSize = 3
            };

            //Act: passing 2 as the product page so we can check if it's showing 2 product on page 2

            ProductsListViewModel result =
                (controller.Index(null, 2) as ViewResult).ViewData.Model as ProductsListViewModel;


            //Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P4", prodArray[0].Name);
            Assert.Equal("P5", prodArray[1].Name);

        }

        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            // Arrange
            Mock<IStoreService> mock = new Mock<IStoreService>();
            mock.Setup(m => m.GetAllProducts()).Returns((new Product[] {
            new Product {ProductID = 1, Name = "P1"},
            new Product {ProductID = 2, Name = "P2"},
            new Product {ProductID = 3, Name = "P3"},
            new Product {ProductID = 4, Name = "P4"},
            new Product {ProductID = 5, Name = "P5"}
            }).AsQueryable<Product>());
            // Arrange
            HomeController controller =
            new HomeController(mock.Object) { PageSize = 3 };
            // Act
            ProductsListViewModel result =
            (controller.Index(null, 2) as ViewResult).ViewData.Model as ProductsListViewModel;
            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }

        [Fact]
        public void Can_Filter_Product()
        {
            //Arrange
            //-create the mock service
            Mock<IStoreService> mock = new Mock<IStoreService>();
            mock.Setup(m => m.GetAllProducts()).Returns(
                (new Product[]
                {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductID = 2, Name = "P2", Category = "Cat2"},
                new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductID = 4, Name = "P4", Category = "Cat2"},
                new Product {ProductID = 5, Name = "P5", Category = "Cat3"}
                }).AsQueryable());
            HomeController controller = new HomeController(mock.Object)
            {
                PageSize = 3
            };

            //Action
            Product[] result =
            (((controller.Index("Cat2", 1) as ViewResult).ViewData.Model) as ProductsListViewModel).Products.ToArray();

            // Assert
            Assert.Equal(2, result.Length);
            Assert.True(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.True(result[1].Name == "P4" && result[1].Category == "Cat2");

        }

        [Fact]
        public void Generate_Category_Specific_Product_Count()
        {
            //Arrange

        }
    }
}
