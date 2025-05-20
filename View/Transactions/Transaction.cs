using GUI.View.Sale;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace GUI.View.Transaction
{
    public partial class Transaction : UserControl
    {
        private readonly TransactionBLL _transactionBLL;
        public Transaction()
        {
            InitializeComponent();
            WarehouseContext context = new WarehouseContext();
            UnitOfWork unitOfWork = new UnitOfWork(context);
            _transactionBLL = new TransactionBLL(unitOfWork);
            LoadHistory();

        }
        private void LoadHistory()
        {
            var listHistories = _transactionBLL.GetAllTransaction();
            dataGridView1.DataSource = listHistories;
            dataGridView1.Columns["Id"].Visible = false;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string id = (string)dataGridView1.SelectedRows[0].Cells["TransactionId"].Value;
                var detail = new Details(id);
                Main main = (Main)this.ParentForm;
                main.LoadUserControl(detail);
            }
        }
    }
}
