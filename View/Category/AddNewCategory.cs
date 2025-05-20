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

namespace GUI.View.Category
{
    public partial class AddNewCategory : UserControl
    {
        private readonly CategoryBLL _categoryBLL;
        public AddNewCategory()
        {
            InitializeComponent();
            var context = new WarehouseContext();
            var unitOfWork = new UnitOfWork(context);
            _categoryBLL = new CategoryBLL(unitOfWork);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Categories category = new Categories();
            category.CategoryItem = textBox2.Text;
            if (_categoryBLL.IsCategoryItemExists(category.CategoryItem))
            {
                MessageBox.Show("Tên loại sản phẩm đã tồn tại. Vui lòng chọn tên khác.");
                return;
            }
            _categoryBLL.AddCategory(category);
            MessageBox.Show("Thêm loại sản phẩm thành công!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var category = new Category();
            Main main = (Main)this.ParentForm;
            main.LoadUserControl(category);
        }
    }
}
