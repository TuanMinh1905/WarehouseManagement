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
    public partial class UpdateCategory : UserControl
    {
        private readonly CategoryBLL _categoryBLL;
        private int categoryID;
        public UpdateCategory(Categories category)
        {
            InitializeComponent();
            var context = new WarehouseContext();
            var unitOfWork = new UnitOfWork(context);
            _categoryBLL = new CategoryBLL(unitOfWork);
            textBox2.Text = category.CategoryItem.ToString();
            categoryID = category.CategoryID;
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
            category.CategoryID = categoryID;

            _categoryBLL.UpdateCategory(category);
            MessageBox.Show("Cập nhật loại sản phẩm thành công!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var category = new Category();
            Main main = (Main)this.ParentForm;
            main.LoadUserControl(category);
        }
    }
}
