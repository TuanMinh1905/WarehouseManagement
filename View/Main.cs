using GUI.View.Category;
using GUI.View.Dashboard;
using GUI.View.Product;
using GUI.View.Sale;
using GUI.View.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarehouseProProject.BLL;

namespace WarehouseManagement
{
    public partial class Main : Form
    {
        private readonly UserBLL _userBLL;
        public Main(UserBLL userBLL)
        {
            InitializeComponent();
            _userBLL = userBLL;
        }
        public void LoadUserControl(UserControl uc)
        {
            panel_control.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panel_control.Controls.Add(uc);
        }
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            var _dashboard = new Dashboard();
            LoadUserControl(_dashboard);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            var _product = new Product();
            LoadUserControl(_product);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            var _category = new Category();
            LoadUserControl(_category);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            var _cart = new Cart();
            LoadUserControl(_cart);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            var _transaction = new GUI.View.Transaction.Transaction();
            LoadUserControl(_transaction);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _userBLL.Logout();
            var user = new Users(_userBLL);
            this.Hide();
            user.Show();
        }
    }
}
