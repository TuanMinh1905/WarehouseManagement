using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using WarehouseProProject.BLL;
using WarehouseProProject.DAL;
using WarehouseManagement;

namespace GUI.View.Product
{
    public partial class UpdateProduct : UserControl
    {
        private readonly CategoryBLL _categoryBLL;
        private readonly ProductBLL _productBLL;
        private int productID;
        public UpdateProduct(Products product, string categoryName)
        {
            InitializeComponent();
            var context = new WarehouseContext();
            var unitOfWork = new UnitOfWork(context);
            _categoryBLL = new CategoryBLL(unitOfWork);
            _productBLL = new ProductBLL(unitOfWork);

            textBox1.Text = product.ProductName;
            textBox2.Text = product.Price.ToString();
            textBox3.Text = product.Quantity.ToString();
            comboBox1.Text = categoryName.ToString();
            productID = product.ProductID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Products product = new Products();
            product.ProductName = textBox1.Text;

            if (!int.TryParse(textBox2.Text, out int price) || price <= 0)
            {
                MessageBox.Show("Giá sản phẩm phải là số nguyên dương.");
                return;
            }

            product.ProductID = productID;
            product.Price = price;
            product.Quantity = Convert.ToInt32(textBox3.Text);
            product.Unit = 1;
            product.CategoryID = _categoryBLL.FindIdByName(comboBox1.Text);

            _productBLL.UpdateProduct(product);
            MessageBox.Show("Cập nhật giá sản phẩm thành công!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var product = new Product();
            Main main = (Main)this.ParentForm;
            main.LoadUserControl(product);
        }
    }
}
