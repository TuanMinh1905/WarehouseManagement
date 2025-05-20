using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarehouseProProject.DAL.Repository;

namespace GUI.View.Dashboard
{
    public partial class Dashboard : UserControl
    {
        private readonly DashboardBLL _dashboardBLL;
        public Dashboard()
        {
            InitializeComponent();
            var context = new WarehouseContext();
            var transactionRepo = new TransactionRepository(context);
            var productRepo = new ProductRepository(context);
            var categoryRepo = new CategoryRepository(context);
            _dashboardBLL = new DashboardBLL(transactionRepo, productRepo, categoryRepo);
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            var totalProducts = _dashboardBLL.GetTotalProducts();
            lblTotalProducts.Text = totalProducts.ToString();

            var totalRevenue = _dashboardBLL.GetTotalRevenue();
            lblTotalRevenue.Text = totalRevenue.ToString();

            var totalTransactions = _dashboardBLL.GetTotalTransactions();
            lblTotalTransactions.Text = totalTransactions.ToString();

            var totalCategories = _dashboardBLL.GetTotalCategories();
            lblTotalCategories.Text = totalCategories.ToString();
        }
    }
}
