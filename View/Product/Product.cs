using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WarehouseManagement;
using WarehouseProProject.BLL;
using WarehouseProProject.DAL;

namespace GUI.View.Product
{
    public partial class Product : UserControl
    {
        private readonly ProductBLL _productBLL;
        public Product()
        {
            InitializeComponent();
            var context = new WarehouseContext();
            var unitOfWork = new UnitOfWork(context);
            _productBLL = new ProductBLL(unitOfWork);
            loadProduct();
            AddToCart();
        }

        private void loadProduct()
        {
            var listProduct = _productBLL.GetAllProducts();
            dataGridView1.DataSource = listProduct;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                // Get the values from the selected row
                string name = dataGridView1.Rows[e.RowIndex].Cells["ProductName"].Value.ToString();
                int productID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ProductID"].Value);
                int price = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Price"].Value);
                int quantity = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Quantity"].Value);

                // Add item to the cart
                if (quantity > 0)
                {
                    bool itemAdded = _productBLL.AddItemToCart(productID, name, price);
                    if (itemAdded)
                    {
                        MessageBox.Show("Sản phẩm đã được thêm vào danh sách xuất kho.");
                    }
                }
                else
                {
                    MessageBox.Show("Sản phẩm đã hết hàng.");
                }
            }
        }
        private void AddToCart()
        {
            DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn
            {
                Text = "Add",
                UseColumnTextForButtonValue = true
            };
            dataGridView1.Columns.Add(buttonColumn);
            dataGridView1.CellContentClick -= dataGridView1_CellContentClick;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var addNewProduct = new AddNewProduct();
            Main main = (Main)this.ParentForm;
            main.LoadUserControl(addNewProduct);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Products product = new Products();
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                product.ProductID = (int)row.Cells["ProductID"].Value;
                product.ProductName = row.Cells["ProductName"].Value.ToString();
                product.Price = (int)row.Cells["price"].Value;
                product.Quantity = (int)row.Cells["Quantity"].Value;
                product.Unit = 1;
                string categoryName = row.Cells["CategoryName"].Value.ToString();

                var updateProduct = new UpdateProduct(product, categoryName);
                Main main = (Main)this.ParentForm;
                main.LoadUserControl(updateProduct);
            }
                
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = (int)dataGridView1.SelectedRows[0].Cells["ProductID"].Value;

                if (MessageBox.Show("Bạn có chắc chắn muốn xóa mục này không?", "Lưu ý!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    _productBLL.DeleteProduct(id);
                    dataGridView1.DataSource = _productBLL.GetAllProducts();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xóa.", "Trống!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string name = dataGridView1.SelectedRows[0].Cells["ProductName"].Value.ToString();

                var addStock = new AddStock(name);
                Main main = (Main)this.ParentForm;
                main.LoadUserControl(addStock);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để nhập kho.", "Trống!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void button5_Click(object sender, EventArgs e)
        {
            var showHistory = new ShowHistory();
            Main main = (Main)this.ParentForm;
            main.LoadUserControl(showHistory);
        }

        private void PerformSearch()
        {
            DataTable dt = _productBLL.SearchProducts(textBox1.Text);
            dataGridView1.DataSource = dt;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            PerformSearch();
        }
    }
}
