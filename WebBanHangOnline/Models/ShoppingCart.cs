using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models
{
    public class ShoppingCart
    {
        public List<ShoppingCartItem> Items { get; set; }
        public ShoppingCart()
        {
            this.Items = new List<ShoppingCartItem>();
        }

        public void AddToCart(ShoppingCartItem item, int Quantity)
        {
            var checkExits = Items.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (checkExits != null)
            {

                checkExits.Quantity += Quantity;

                checkExits.TotalPrice = checkExits.Price * checkExits.Quantity > 0 ? checkExits.Price = checkExits.Quantity : 0;
            }
            else
            {
                item.Quantity = Quantity;
                item.TotalPrice = Quantity > 0 ? item.Price = Quantity : 0;
                Items.Add(item);
            }
        }


        public void Remove(int id)
        {
            var checkExits = Items.SingleOrDefault(x=>x.ProductId==id);
            if (checkExits != null)
            {
                Items.Remove(checkExits);
            }
        }

        public void UpdateQuantity(int id,int quantity)
        {
            var checkExits = Items.SingleOrDefault(x => x.ProductId == id);
            if (checkExits != null)
            {
                checkExits.Quantity = quantity;
                checkExits.TotalPrice = checkExits.Price * checkExits.Quantity;
            }
        }

        public decimal GetTotalPrice()
        {
            return Items.Sum(x=>x.TotalPrice);
        }
        public int GetTotalQuantity()
        {
            return Items.Sum(x => x.Quantity);
        }
        public void ClearCart()
        {
            Items.Clear();
        }

    }

    /*public void AddToCart(ShoppingCartItem item, int Quantity)
    {
        var checkExits = Items.FirstOrDefault(x => x.ProductId == item.ProductId);
        if (checkExits != null)
        {
            // Cập nhật số lượng khi sản phẩm đã tồn tại trong giỏ hàng
            checkExits.Quantity += Quantity;

            // Kiểm tra nếu số lượng <= 0, đặt giá tổng là 0
            checkExits.TotalPrice = checkExits.Quantity > 0
                ? checkExits.Price * checkExits.Quantity
                : 0;
        }
        else
        {
            // Thiết lập giá tổng là 0 nếu số lượng <= 0 khi thêm sản phẩm mới
            item.Quantity = Quantity;
            item.TotalPrice = Quantity > 0
                ? item.Price * Quantity
                : 0;

            Items.Add(item);
        }
    }*/
    public class ShoppingCartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Alias { get; set; }
        public string CategoryName { get; set; }
        public string ProductImg { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}