/*using NUnit.Framework;
using System.Collections.Generic;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Tests
{
    [TestFixture]
    public class ShoppingCartTests
    {
        private ShoppingCart cart;

        [SetUp]
        public void Setup()
        {
            cart = new ShoppingCart();
        }

        [Test]
        public void AddToCart_NewProduct_ShouldAddToItemsList()
        {
            // Arrange
            var item = new ShoppingCartItem
            {
                ProductId = 1,
                ProductName = "Test Product",
                Price = 100,
                Quantity = 1,
                TotalPrice = 100
            };

            // Act
            cart.AddToCart(item, 1);

            // Assert
            Assert.AreEqual(1, cart.Items.Count);
            Assert.AreEqual(item, cart.Items[0]);
        }

        [Test]
        public void AddToCart_ExistingProduct_ShouldUpdateQuantityAndTotalPrice()
        {
            // Arrange
            var item = new ShoppingCartItem
            {
                ProductId = 1,
                ProductName = "Test Product",
                Price = 100,
                Quantity = 1,
                TotalPrice = 100
            };
            cart.AddToCart(item, 1);

            // Act
            cart.AddToCart(item, 2);

            // Assert
            var updatedItem = cart.Items.Find(x => x.ProductId == 1);
            Assert.AreEqual(3, updatedItem.Quantity);
            Assert.AreEqual(300, updatedItem.TotalPrice);
        }

        [Test]
        public void AddToCart_TotalPriceCalculation_ShouldBeAccurate()
        {
            // Arrange
            var item1 = new ShoppingCartItem
            {
                ProductId = 1,
                ProductName = "Test Product 1",
                Price = 100,
                Quantity = 1,
                TotalPrice = 100
            };
            var item2 = new ShoppingCartItem
            {
                ProductId = 2,
                ProductName = "Test Product 2",
                Price = 200,
                Quantity = 1,
                TotalPrice = 200
            };

            // Act
            cart.AddToCart(item1, 1);
            cart.AddToCart(item2, 1);

            // Assert
            Assert.AreEqual(300, cart.GetTotalPrice());
        }
    }
}
*/

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
            int additionalQuantity = 2;
            int expectedQuantity = 3;

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
            int additionalQuantity = 2;
            decimal expectedTotalPrice = _testItem1.Price * 3; // Price * total quantity (1 + 2)

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
        public void AddToCart_ZeroQuantity_ShouldStillAddItem()
        {
            // Arrange
            int quantity = 0;

            // Act
            _cart.AddToCart(_testItem1, quantity);

            // Assert
            Assert.That(_cart.Items.Count, Is.EqualTo(1));
            Assert.That(_cart.Items.First().Quantity, Is.EqualTo(quantity));
        }

        [Test]
        public void AddToCart_NegativeQuantity_ShouldStillAddItem()
        {
            // Arrange
            int quantity = -1;

            // Act
            _cart.AddToCart(_testItem1, quantity);

            // Assert
            Assert.That(_cart.Items.Count, Is.EqualTo(1));
            Assert.That(_cart.Items.First().Quantity, Is.EqualTo(quantity));
        }
    }
}


