using System.Linq;
using SportsStoreOnCore2.Models;
using Xunit;

namespace SportsStoreOnCore2.Tests
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_lines()
        {
            // Arrange - create come products
            Product p1 = new Product { ProductId = 1, Name = "P1" };
            Product p2 = new Product { ProductId = 2, Name = "P2" };

            // Arrange - create a new cart
            Cart cart = new Cart();

            // Act 
            cart.AddItem(p1, 2);
            cart.AddItem(p2, 5);
            CartLine[] result = cart.Lines.ToArray();


            // Assert 
            Assert.Equal(2, result.Length);
            Assert.Equal(p1, result[0].Product);
            Assert.Equal(p2, result[1].Product);
        }

        [Fact]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            // Arrange - create come products
            Product p1 = new Product { ProductId = 1, Name = "P1" };
            Product p2 = new Product { ProductId = 2, Name = "P2" };

            // Arrange - create a new cart
            Cart cart = new Cart();

            // Act 
            cart.AddItem(p1, 2);
            cart.AddItem(p2, 3);
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 5);

            CartLine[] result = cart.Lines.OrderBy(p => p.Product.ProductId).ToArray();

            //Assert 
            Assert.Equal(2, result.Length);
            Assert.Equal(3, result[0].Quantity);
            Assert.Equal(8, result[1].Quantity);
        }

        [Fact]
        public void Can_Remove_Line()
        {
            // Arrange - create come products
            Product p1 = new Product { ProductId = 1, Name = "P1" };
            Product p2 = new Product { ProductId = 2, Name = "P2" };
            Product p3 = new Product { ProductId = 3, Name = "P3" };

            // Arrange - create a new cart
            Cart cart = new Cart();
            cart.AddItem(p1, 2);
            cart.AddItem(p2, 3);
            cart.AddItem(p3, 1);
            cart.AddItem(p2, 5);

            // Act 
            cart.RemoveLine(p2);

            // Assert
            Assert.Empty(cart.Lines.Where(p => p.Product == p2));
            Assert.Equal(2, cart.Lines.Count());
        }

        [Fact]
        public void Calculate_Cart_Total()
        {
            // Arrange
            Product p1 = new Product { ProductId = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductId = 2, Name = "P2", Price = 50M };

            Cart cart = new Cart();
            cart.AddItem(p1, 2);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 5);
            cart.AddItem(p2, 2);

            // Act 
            decimal result = cart.ComputeTotalValue();

            // Assert
            Assert.Equal(850, result);
        }

        [Fact]
        public void Can_Clear_Contents()
        {
            // Arrange
            Product p1 = new Product { ProductId = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductId = 2, Name = "P2", Price = 50M };

            Cart cart = new Cart();
            cart.AddItem(p1, 2);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 5);

            // Act 
            cart.Clear();
            Assert.Empty(cart.Lines);
        }
    }
}
