using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using SportsStoreOnCore2.Models;
using System.Linq;
using SportsStoreOnCore2.Components;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace SportsStoreOnCore2.Tests
{
    class NavigationMenuViewComponentTests
    {
        [Fact]
        public void Can_Select_Categories()
        {
            // Arrange 
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product { ProductId = 1, Name = "P1", Category = "Apples" },
                new Product { ProductId = 2, Name = "P2", Category = "Apples"},
                new Product { ProductId = 3, Name = "P3", Category = "Plubs"},
                new Product { ProductId = 4, Name = "P4", Category = "Oranges"},
                }).AsQueryable<Product>());

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);

            // Act
            //string[] result = (IEnumerable<string>)target.Invoke() as ViewViewComponentResult)
        }

    }
}
