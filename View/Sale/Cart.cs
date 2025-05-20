using GUI.View.Category;
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
    public partial class Cart : UserControl
    {
        private readonly CartBLL _cartBLL;
        public Cart()
        {
            InitializeComponent();
            WarehouseContext context = new WarehouseContext();
            UnitOfWork unitOfWork = new UnitOfWork(context);
            _cartBLL = new CartBLL(unitOfWork);
            LoadCart();
        }
        private void LoadCart()
        {
            var listCart = _cartBLL.GetAllCarts();
            dataGridView1.DataSource = listCart;
            dataGridView1.Columns["Product"].Visible = false;
            dataGridView1.Columns["CartID"].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var checkOut = new CheckOut();
            Main main = (Main)this.ParentForm;
            main.LoadUserControl(checkOut);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Carts cart = new Carts();
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                cart.ProductID = (int)row.Cells["ProductID"].Value;
                cart.CartID = (int)row.Cells["CartID"].Value;
                cart.Quantity = (int)row.Cells["Quantity"].Value;
                cart.Price = (int)row.Cells["Price"].Value;
                cart.Name = row.Cells["Name"].Value.ToString();

                var quantity = new Quanitity(cart);
                Main main = (Main)this.ParentForm;
                main.LoadUserControl(quantity);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = (int)dataGridView1.SelectedRows[0].Cells["CartID"].Value;

                if (MessageBox.Show("Bạn có chắc chắn muốn xóa mục này không?", "Lưu ý!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    _cartBLL.DeleteCart(id);
                    dataGridView1.DataSource = _cartBLL.GetAllCarts();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xóa.", "Trống!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
