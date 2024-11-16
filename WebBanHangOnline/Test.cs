
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Tests
{
    [TestFixture]
    public class ShoppingCartTests
    {
        private ShoppingCart _cart;
        private ShoppingCartItem _testItem1;
        private ShoppingCartItem _testItem2;

        [SetUp]
        public void Setup()
        {
            // Khởi tạo shopping cart mới cho mỗi test
            _cart = new ShoppingCart();
            _cart.Items = new List<ShoppingCartItem>();

            // Khởi tạo test items
            _testItem1 = new ShoppingCartItem
            {
                ProductId = 1,
                ProductName = "Test Product 1",
                CategoryName = "Test Category",
                Alias = "test-product-1",
                Quantity = 1,
                Price = 100000,
                TotalPrice = 100000
            };

            _testItem2 = new ShoppingCartItem
            {
                ProductId = 2,
                ProductName = "Test Product 2",
                CategoryName = "Test Category",
                Alias = "test-product-2",
                Quantity = 1,
                Price = 200000,
                TotalPrice = 200000
            };
        }

        [Test]
        public void AddToCart_NewItem_ShouldAddToItems()
        {
            // Arrange
            int quantity = 1;

            // Act
            _cart.AddToCart(_testItem1, quantity);

            // Assert
            Assert.That(_cart.Items.Count, Is.EqualTo(1));
            Assert.That(_cart.Items.First().ProductId, Is.EqualTo(_testItem1.ProductId));
            Assert.That(_cart.Items.First().Quantity, Is.EqualTo(quantity));
        }

        [Test]
        public void AddToCart_ExistingItem_ShouldUpdateQuantity()
        {
            // Arrange
            _cart.AddToCart(_testItem1, 1); // Add initial item
            int additionalQuantity = 1;
            int expectedQuantity = 2;

            // Act
            _cart.AddToCart(_testItem1, additionalQuantity);

            // Assert
            Assert.That(_cart.Items.Count, Is.EqualTo(1));
            Assert.That(_cart.Items.First().Quantity, Is.EqualTo(expectedQuantity));
        }

        [Test]
        public void AddToCart_ExistingItem_ShouldUpdateTotalPrice()
        {
            // Arrange
            _cart.AddToCart(_testItem1, 1); // Add initial item
            int additionalQuantity = 1;
            decimal expectedTotalPrice = _testItem1.Price * 2; // Price * total quantity (1 + 2)

            // Act
            _cart.AddToCart(_testItem1, additionalQuantity);

            // Assert
            Assert.That(_cart.Items.First().TotalPrice, Is.EqualTo(expectedTotalPrice));
        }

        [Test]
        public void AddToCart_MultipleItems_ShouldMaintainSeparateItems()
        {
            // Arrange
            int quantity1 = 1;
            int quantity2 = 2;

            // Act
            _cart.AddToCart(_testItem1, quantity1);
            _cart.AddToCart(_testItem2, quantity2);

            // Assert
            Assert.That(_cart.Items.Count, Is.EqualTo(2));
            Assert.That(_cart.Items.Any(x => x.ProductId == _testItem1.ProductId && x.Quantity == quantity1), Is.True);
            Assert.That(_cart.Items.Any(x => x.ProductId == _testItem2.ProductId && x.Quantity == quantity2), Is.True);
        }

        
        [Test]
        public void AddToCart_ZeroQuantity_ShouldAddItemWithZeroTotalPrice()
        {
            // Arrange
            int quantity = 0;

            // Act
            _cart.AddToCart(_testItem1, quantity);

            // Assert
            Assert.That(_cart.Items.Count, Is.EqualTo(1), "Item should be added to cart.");
            Assert.That(_cart.Items.First().Quantity, Is.EqualTo(quantity), "Quantity should be 0.");
            Assert.That(_cart.Items.First().TotalPrice, Is.EqualTo(0), "TotalPrice should be 0 when Quantity is 0.");
        }

        
    }
}


