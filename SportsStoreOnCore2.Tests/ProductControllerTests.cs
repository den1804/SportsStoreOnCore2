using Moq;
using SportsStoreOnCore2.Controllers;
using SportsStoreOnCore2.Models;
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
            IEnumerable<Product> result = controller.List(2).ViewData.Model as IEnumerable<Product>;

            // Assert
            Product[] productArray = result.ToArray();
            Assert.True(productArray.Length == 2);
            Assert.Equal("P4", productArray[0].Name);
            Assert.Equal("P5", productArray[1].Name);
        }
    }
}
