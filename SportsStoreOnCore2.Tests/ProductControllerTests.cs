using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStoreOnCore2.Controllers;
using SportsStoreOnCore2.Models;
using SportsStoreOnCore2.Models.ViewModels;
using System;
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
            ProductsListViewModel result = controller.List(null, 2).ViewData.Model as ProductsListViewModel;

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
            ProductsListViewModel result = controller.List(null, 2).ViewData.Model as ProductsListViewModel;

            // Assert
            PagingInfo paging = result.PagingInfo;
            Assert.Equal(2, paging.CurrentPage);
            Assert.Equal(3, paging.ItemsPerPage);
            Assert.Equal(5, paging.TotalItems);
            Assert.Equal(2, paging.TotalPages);
        }

        [Fact]
        public void Can_Filter_Products()
        {
            // Arrange 
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductId = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductId = 2, Name = "P2", Category = "Cat2"},
                new Product {ProductId = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductId = 4, Name = "P4", Category = "Cat2"},
                new Product {ProductId = 5, Name = "P5", Category = "Cat3"}
            }).AsQueryable());

            ProductController controller = new ProductController(mock.Object)
            {
                PageSize = 3
            };

            // Act 
            ProductsListViewModel result = controller.List("Cat2").ViewData.Model as ProductsListViewModel;


            // Assert
            Product[] productArray = result.Products.ToArray();
            Assert.True(productArray.Length == 2);
            Assert.Equal("P2", productArray[0].Name);
            Assert.Equal("P4", productArray[1].Name);
        }

        [Fact]
        public void Generate_Category_Spetific_Product_Count()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductId = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductId = 2, Name = "P2", Category = "Cat2"},
                new Product {ProductId = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductId = 4, Name = "P4", Category = "Cat2"},
                new Product {ProductId = 5, Name = "P5", Category = "Cat3"},
            }).AsQueryable<Product>);

            ProductController taget = new ProductController(mock.Object)
            {
                PageSize = 3
            };

            Func<ViewResult, ProductsListViewModel> GetModel = result =>
                result?.ViewData?.Model as ProductsListViewModel;

            // Action
            int? res1 = GetModel(taget.List("Cat1"))?.PagingInfo.TotalItems;
            int? res2 = GetModel(taget.List("Cat2"))?.PagingInfo.TotalItems;
            int? res3 = GetModel(taget.List("Cat3"))?.PagingInfo.TotalItems;
            int? resAll = GetModel(taget.List(null))?.PagingInfo.TotalItems;

            //Assert
            Assert.Equal(2, res1);
            Assert.Equal(2, res2);
            Assert.Equal(1, res3);
            Assert.Equal(5, resAll);
        }
    }
}
