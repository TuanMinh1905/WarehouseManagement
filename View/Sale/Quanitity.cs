using GUI.View.Category;
using GUI.View.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarehouseManagement;
using WarehouseProProject.BLL;
using WarehouseProProject.DAL;

namespace GUI.View.Sale
{
    public partial class Quanitity : UserControl
    {
        private readonly ProductBLL _productBLL;
        private readonly CartBLL _cartBLL;
        private Carts carts;
        public Quanitity(Carts cart)
        {
            InitializeComponent();
            WarehouseContext context = new WarehouseContext();
            UnitOfWork unitOfWork = new UnitOfWork(context);
            _cartBLL = new CartBLL(unitOfWork);
            _productBLL = new ProductBLL(unitOfWork);
            carts = cart;
            textBox2.Text = cart.Quantity.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DecrementQuantity();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            IncrementQuantity();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Carts cart = new Carts();
            cart = carts;
            cart.Quantity = int.Parse(textBox2.Text);
            _cartBLL.UpdateCart(cart);
            MessageBox.Show("Cập nhật số lượng thành công.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var sale = new Cart();
            Main main = (Main)this.ParentForm;
            main.LoadUserControl(sale);
        }

        private void DecrementQuantity()
        {
            if (int.TryParse(textBox2.Text, out int value))
            {
                if (value > 1)
                {
                    value--;
                    textBox2.Text = value.ToString();
                }
            }
        }

        private void IncrementQuantity()
        {
            int value = int.Parse(textBox2.Text);
            int stock = _productBLL.GetQuantityById(carts.ProductID);
            if (value < stock)
            {
                value++;
                textBox2.Text = value.ToString();
            }
            else
            {
                MessageBox.Show("Đã đạt đến giới hạn số lượng.");
            }
        }
    }
}
