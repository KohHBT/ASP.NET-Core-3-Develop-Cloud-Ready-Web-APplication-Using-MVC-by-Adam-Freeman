﻿using SimpleApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SimpleApp.Tests
{
    public class ProductTests
    {
        [Fact]
        public void CanChangeProductName()
        {
            //Arrange
            var p = new Product { Name = "Test", Price = 100M };
            //Act
            p.Name = "New Name";
            //Asert
            Assert.Equal("New Name", p.Name);
        }
        [Fact]
        public void CanChangeProductPrice()
        {
            //Arrange
            var p = new Product { Name = "Test", Price = 100M };
            //Act
            p.Price = 200M;
            //Assert
            Assert.Equal(200M, p.Price);
        }
    }
}
