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

namespace GUI.View.Transaction
{
    public partial class Details : UserControl
    {
        private readonly OrderBLL _orderBLL;
        private string transactionId;
        public Details(string transactionID)
        {
            InitializeComponent();
            WarehouseContext context = new WarehouseContext();
            UnitOfWork unitOfWork = new UnitOfWork(context);
            _orderBLL = new OrderBLL(unitOfWork);
            transactionId = transactionID;
            LoadDetails();
        }

        private void LoadDetails()
        {
            var listOrder = _orderBLL.GetOrdersByTransactionId(transactionId);
            dataGridView1.DataSource = listOrder;
            dataGridView1.Columns["OrderID"].Visible = false;
            dataGridView1.Columns["TransactionId"].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var _transaction = new Transaction();
            Main main = (Main)this.ParentForm;
            main.LoadUserControl(_transaction);
        }
    }
}
