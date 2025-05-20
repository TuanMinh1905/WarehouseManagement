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

namespace GUI.View.Product
{
    public partial class ShowHistory : UserControl
    {
        private readonly HistoryBLL _historyBLL;
        public ShowHistory()
        {
            InitializeComponent();
            var context = new WarehouseContext();
            var unitOfWork = new UnitOfWork(context);
            _historyBLL = new HistoryBLL(unitOfWork);
            loadHistory();
        }
        private void loadHistory()
        {
            var listProduct = _historyBLL.GetAllHistory();
            dataGridView1.DataSource = listProduct;
            if (dataGridView1.Columns["Product"] != null)
            {
                dataGridView1.Columns["Product"].Visible = false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var product = new Product();
            Main main = (Main)this.ParentForm;
            main.LoadUserControl(product);
        }
    }
}
