using Moq;
using SportsStoreOnCore2.Controllers;
using SportsStoreOnCore2.Models;
using SportsStoreOnCore2.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SportsStoreOnCore2.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Paginate()
        {
            // Arrange 
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"},
                new Product {ProductId = 3, Name = "P3"},
                new Product {ProductId = 4, Name = "P4"},
                new Product {ProductId = 5, Name = "P5"}
            }).AsQueryable());

            ProductController controller = new ProductController(mock.Object)
            {
                PageSize = 3
            };

            // Act 
            ProductsListViewModel result = controller.List(2).ViewData.Model as ProductsListViewModel;

            // Assert
            Product[] productArray = result.Products.ToArray();
            Assert.True(productArray.Length == 2);
            Assert.Equal("P4", productArray[0].Name);
            Assert.Equal("P5", productArray[1].Name);
        }
        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            // Arrange 
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"},
                new Product {ProductId = 3, Name = "P3"},
                new Product {ProductId = 4, Name = "P4"},
                new Product {ProductId = 5, Name = "P5"}
            }).AsQueryable());

            ProductController controller = new ProductController(mock.Object)
            {
                PageSize = 3
            };

            // Act 
            ProductsListViewModel result = controller.List(2).ViewData.Model as ProductsListViewModel;

            // Assert
            PagingInfo paging = result.PagingInfo;
            Assert.Equal(2, paging.CurrentPage);
            Assert.Equal(3, paging.ItemsPerPage);
            Assert.Equal(5, paging.TotalItems);
            Assert.Equal(2, paging.TotalPages);
        }
    }
}
