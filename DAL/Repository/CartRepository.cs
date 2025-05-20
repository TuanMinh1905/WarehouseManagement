using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace WarehouseProProject.DAL.Repository
{
    public class CartReposotory : RepositoryBase<Carts>
    {
        public CartReposotory(WarehouseContext context) : base(context) { }

        public List<Carts> Items { get; set; } = new List<Carts>();
        public bool AddItem(int productID, string name, int price)
        {
            var existingItem = Items.FirstOrDefault(i => i.ProductID == productID);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                Items.Add(new Carts { ProductID = productID, Name = name, Price = price, Quantity = 1 });
                Carts newCart = new Carts
                {
                    Name = name,
                    Price = price,
                    Quantity = 1,
                    ProductID = productID,
                };

                _context.Carts.Add(newCart);
                _context.SaveChanges();
            }
            return true;
        }
        public decimal GetTotalCartPrice()
        {
            var totalPrice = _context.Carts
                .Select(c => (decimal)c.Price * c.Quantity)
                .DefaultIfEmpty(0m)
                .Sum();

            return totalPrice;
        }

        public void CalculateDiscount(string totalText, string discountName, out string discountText, out string totalAfterDiscountText)
        {
            decimal total = Convert.ToDecimal(totalText);
            decimal discountAmount = 0;

            var discount = _context.Discounts.FirstOrDefault(d => d.Description == discountName);
            if (discount != null)
            {
                decimal discountPercent = (decimal)discount.Value;
                discountAmount = total * (discountPercent / 100m);
            }

            decimal totalAfterDiscount = total - discountAmount;
            discountText = (-discountAmount).ToString();
            totalAfterDiscountText = totalAfterDiscount.ToString();
        }

        public void CalculateChange(string totalText, string receiveText, out string changeText)
        {
            changeText = "0";

            if (!decimal.TryParse(totalText, out decimal total) || !decimal.TryParse(receiveText, out decimal receive))
            {
                changeText = "Dữ liệu không hợp lệ";
                return;
            }
            decimal change = receive - total;
            if (change < 0)
            {
                changeText = $"Thiếu {Math.Abs(change).ToString()}";
                return;
            }

            changeText = change.ToString("N0");
        }

        public void ResetID()
        {
            string tableName = "Carts";
            string sql = $"DBCC CHECKIDENT ('{tableName}', RESEED, 0)";
            _context.Database.ExecuteSqlCommand(sql);
        }

        public Carts GetCartItemByProductId(int productId)
        {
            return _context.Carts.FirstOrDefault(c => c.ProductID == productId);
        }

        public bool AddItemToCart(int productID, string productName, int price)
        {
            var existingItem = GetCartItemByProductId(productID);

            if (existingItem != null)
            {
                existingItem.Quantity += 1;
                Update(existingItem);
            }
            else
            {
                var newItem = new Carts
                {
                    ProductID = productID,
                    Name = productName,
                    Price = price,
                    Quantity = 1
                };
                Add(newItem);
            }

            return true;
        }


    }
}