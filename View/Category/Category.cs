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
    public partial class Category : UserControl
    {
        private readonly CategoryBLL _categoryBLL;
        public Category()
        {
            InitializeComponent();
            var context = new WarehouseContext();
            var unitOfWork = new UnitOfWork(context);
            _categoryBLL = new CategoryBLL(unitOfWork);
            loadCategory();
        }
        private void loadCategory()
        {
            var listCategory = _categoryBLL.GetAllCategories();
            dataGridView1.DataSource = listCategory;
            dataGridView1.Columns["Products"].Visible = false;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var addNewCategory = new AddNewCategory();
            Main main = (Main)this.ParentForm;
            main.LoadUserControl(addNewCategory);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Categories category = new Categories();
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                category.CategoryID = (int)row.Cells["CategoryID"].Value;
                category.CategoryItem = row.Cells["CategoryItem"].Value.ToString();

                var updateCategory = new UpdateCategory(category);
                Main main = (Main)this.ParentForm;
                main.LoadUserControl(updateCategory);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = (int)dataGridView1.SelectedRows[0].Cells["CategoryID"].Value;

                if (MessageBox.Show("Bạn có chắc chắn muốn xóa mục này không?", "Lưu ý!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    _categoryBLL.DeleteCategory(id);
                    dataGridView1.DataSource = _categoryBLL.GetAllCategories();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xóa.", "Trống!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
