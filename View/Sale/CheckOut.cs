using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarehouseManagement;
using WarehouseProProject.BLL;
using WarehouseProProject.DAL;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GUI.View.Sale
{
    public partial class CheckOut : UserControl
    {
        private readonly CartBLL _cartBLL;
        private readonly DiscountBLL _discountBLL;
        private readonly TransactionBLL _transactionBLL;
        private readonly ProductBLL _productBLL;
        private readonly OrderBLL _orderBLL;
        public CheckOut()
        {
            InitializeComponent();

            WarehouseContext context = new WarehouseContext();
            UnitOfWork unitOfWork = new UnitOfWork(context);
            _discountBLL = new DiscountBLL(unitOfWork);
            _cartBLL = new CartBLL(unitOfWork);
            _transactionBLL = new TransactionBLL(unitOfWork);
            _productBLL = new ProductBLL(unitOfWork);
            _orderBLL = new OrderBLL(unitOfWork);

            LoadCartItemsToListBox();
            LoadComboBox();
            label3.Text = _cartBLL.GetTotalCartPrice().ToString();
            _cartBLL.CalculateDiscount(label3.Text, comboBox1.Text, out string discountText, out string totalAfterDiscountText);
            label7.Text = discountText;
            label8.Text = totalAfterDiscountText;
        }
        public void LoadCartItemsToListBox()
        {
            var cartItems = _cartBLL.GetAllCarts();
            foreach (var item in cartItems)
            {
                string display = $"{item.Quantity} x {item.Name} - ${item.Price}";
                listBox1.Items.Add(display);
            }
        }

        private void LoadComboBox()
        {
            comboBox1.DataSource = _discountBLL.GetAllDiscounts();
            comboBox1.DisplayMember = "Description"; 
            comboBox1.ValueMember = "Value";
            comboBox1.SelectedIndex = 0;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var transaction = new Transactions();
            string newTransactionID = _transactionBLL.GenerateTransactionId();
            transaction.TransactionId = newTransactionID;
            transaction.Subtotal = label3.Text;
            transaction.DiscountPercent = comboBox1.Text;
            transaction.DiscountAmount = label7.Text;
            transaction.Total = label8.Text;
            transaction.Change = label10.Text;
            transaction.Cash = textBox2.Text;
            transaction.Date = DateTime.Now;
            if (Convert.ToDecimal(transaction.Cash) < Convert.ToDecimal(transaction.Total))
            {
                MessageBox.Show("Tiền khách đưa không đủ!");
                return;
            }

            _transactionBLL.SaveTransaction(transaction);
            var cartItems = _cartBLL.GetAllCarts();
            foreach (var item in cartItems)
            {
                int Id = item.ProductID;
                int quantity = item.Quantity;
                _productBLL.UpdateProductQuantity(Id, quantity);
                Order order = new Order();
                order.TransactionId = newTransactionID;
                order.Name = item.Name;
                order.Price = (item.Price).ToString();
                order.Quantity = item.Quantity;
                _orderBLL.AddOrder(order);
            }
            _cartBLL.DeleteAllCarts();
            MessageBox.Show("Giao dịch thành công!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var sale = new Cart();
            Main main = (Main)this.ParentForm;
            main.LoadUserControl(sale);
        }
        public void ChangeEventHandler()
        {
            string totalText = label8.Text;
            string receiveText = textBox2.Text;
            if (decimal.TryParse(textBox2.Text, out _))
            {
                _cartBLL.CalculateChange(totalText, receiveText, out string changeText);
                label10.Text = changeText;
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ChangeEventHandler();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _cartBLL.CalculateDiscount(label3.Text, comboBox1.Text, out string discountText, out string totalAfterDiscountText);
            label7.Text = discountText;
            label8.Text = totalAfterDiscountText;
        }

        private void label8_TextChanged(object sender, EventArgs e)
        {
            ChangeEventHandler();
        }
    }
}
