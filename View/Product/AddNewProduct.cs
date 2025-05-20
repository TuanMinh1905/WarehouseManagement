using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarehouseManagement;
using WarehouseProProject.BLL;
using WarehouseProProject.DAL;

namespace GUI.View.Product
{
    public partial class AddNewProduct : UserControl
    {
        private readonly CategoryBLL _categoryBLL;
        private readonly ProductBLL _productBLL;
        public AddNewProduct()
        {
            InitializeComponent();
            var context = new WarehouseContext();
            var unitOfWork = new UnitOfWork(context);
            _categoryBLL = new CategoryBLL(unitOfWork);
            _productBLL = new ProductBLL(unitOfWork);
            loadComboBox();
        }

        private void loadComboBox()
        {
            var categories = _categoryBLL.GetAllCategories();
            comboBox1.DataSource = categories;
            comboBox1.DisplayMember = "CategoryItem";
            comboBox1.ValueMember = "CategoryID";
            comboBox1.SelectedIndex = -1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Products product = new Products();
            product.ProductName = textBox1.Text;

            if (_productBLL.IsProductNameExists(product.ProductName))
            {
                MessageBox.Show("Tên sản phẩm đã tồn tại. Vui lòng chọn tên khác.");
                return;
            }
            if (string.IsNullOrEmpty(product.ProductName) ||
                string.IsNullOrEmpty(textBox2.Text.Trim()) ||
                string.IsNullOrEmpty(textBox3.Text.Trim()) ||
                comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin sản phẩm và chọn loại.");
                return;
            }
            if (!int.TryParse(textBox2.Text, out int price) || price <= 0)
            {
                MessageBox.Show("Giá sản phẩm phải là số nguyên dương.");
                return;
            }

            if (!int.TryParse(textBox3.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên không âm.");
                return;
            }

            product.Price = price;
            product.Quantity = quantity;
            product.Unit = 1;
            product.CategoryID = Convert.ToInt32(comboBox1.SelectedValue);

            _productBLL.AddProduct(product);
            MessageBox.Show("Thêm sản phẩm thành công!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var product = new Product();
            Main main = (Main)this.ParentForm;
            main.LoadUserControl(product);
        }
    }
}
