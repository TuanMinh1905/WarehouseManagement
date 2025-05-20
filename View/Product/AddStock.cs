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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GUI.View.Product
{
    public partial class AddStock : UserControl
    {
        private readonly ProductBLL _productBLL;
        private readonly HistoryBLL _historyBLL;
        private string productName;
        public AddStock(string name)
        {
            InitializeComponent();
            var context = new WarehouseContext();
            var unitOfWork = new UnitOfWork(context);
            _productBLL = new ProductBLL(unitOfWork);
            _historyBLL = new HistoryBLL(unitOfWork);

            label3.Text = name;
            productName = name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Products product = new Products();

            if (!int.TryParse(textBox2.Text, out int addedstock) || addedstock < 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên không âm.");
                return;
            }

            int Id = _productBLL.FindIdByName(productName);
            product = _productBLL.GetProductById(Id);
            product.Quantity += addedstock;

            Histories history = new Histories();
            history.ProductID = product.ProductID;
            history.AddedStocks = addedstock;
            history.Date = DateTime.Now;

            _historyBLL.AddHistory(history);
            _productBLL.UpdateProduct(product);
            MessageBox.Show("Nhập kho thành công!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var product = new Product();
            Main main = (Main)this.ParentForm;
            main.LoadUserControl(product);
        }
    }
}
