using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using SportsStore.Models;
using Castle.DynamicProxy.Contributors;

namespace SportsStore.Tests
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_Line()
        {
            //Arrange - Create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };

            //Arrange - create a new cart
            Cart target = new Cart();

            //Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            CartLine[] results = target.Lines.ToArray();

            //Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(p2, results[1].Product);
        }
        [Fact]
        public void Can_Increase_Quantity_For_Existing_Lines()
        {
            //Arrange - create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };

            Cart target = new Cart();

            //Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            target.AddItem(p2, 11);

            CartLine[] results = target.Lines.OrderBy(l => l.Product.ProductID).ToArray() ;

            //Assert

            Assert.Equal(2, results.Length);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(12, results[1].Quantity);
        }
        [Fact]
        public void Can_Remove_Line()
        {
            //Arrange - create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3 = new Product { ProductID = 3, Name = "P3" };

            Cart target = new Cart();

            //Arrange- add some products to the cart
            target.AddItem(p1, 2);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            //Act
            target.RemoveLine(p2);

            //Assert
            Assert.Empty(target.Lines.Where(l => l.Product == p2));
            Assert.Equal(2, target.Lines.Count());

        }

        [Fact]
        public void Can_Calculate_Total()
        {
            //Arrange - create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };

            Cart target = new Cart();

            //Arrange- add some products to the cart
            target.AddItem(p1, 2);
            target.AddItem(p2, 3);

            //Act
            decimal result = target.ComputeTotalValue();

            //Assert
            Assert.Equal(350M, result);
        }
        [Fact]
        public void Can_Clear_Contents()
        {
            // Arrange - create some test products
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };
            // Arrange - create a new cart
            Cart target = new Cart();
            // Arrange - add some items
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            // Act - reset the cart
            target.Clear();
            // Assert
            Assert.Empty(target.Lines);
        }
    }
}
