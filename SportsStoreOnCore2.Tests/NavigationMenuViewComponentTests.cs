using Moq;
using System.Collections.Generic;
using Xunit;
using SportsStoreOnCore2.Models;
using System.Linq;
using SportsStoreOnCore2.Components;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;

namespace SportsStoreOnCore2.Tests
{
    public class NavigationMenuViewComponentTests
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
            string[] result = ((IEnumerable<string>)(target.Invoke() as ViewViewComponentResult).ViewData.Model).ToArray();

            // Assert

            Assert.True(Enumerable.SequenceEqual(new string[] { "Apples", "Oranges", "Plubs" }, result));
        }

        [Fact]
        public void Indicates_Select_Category()
        {
            string categoryToSelect = "Apples";

            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductId = 1, Name = "P1", Category = "Apples"},
                new Product {ProductId = 4, Name = "P2", Category = "Oranges"}
            }).AsQueryable<Product>);
            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object)
            {
                ViewComponentContext = new ViewComponentContext
                {
                    ViewContext = new ViewContext
                    {
                        RouteData = new RouteData()
                    }
                }
            };
            target.RouteData.Values["category"] = categoryToSelect;

            // Act 

            string result = (string)(target.Invoke() as ViewViewComponentResult).ViewData["SelectedCategory"];

            // Assert
            Assert.Equal(result, categoryToSelect);
        }
    }
}
