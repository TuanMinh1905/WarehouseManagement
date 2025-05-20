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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WarehouseManagement
{
    public partial class Users : Form
    {
        private bool isRegisterMode = false;

        private readonly UserBLL _userBLL;
        public Users(UserBLL userBLL)
        {
            _userBLL = userBLL;
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;

            if (string.IsNullOrEmpty(username))
            {
                ePUsername.SetError(textBoxUsername, "Vui lòng nhập tên tài khoản.");
            }
            else
            {
                ePUsername.Clear();
            } 
            if (string.IsNullOrEmpty(password))
            {
                ePPassword.SetError(textBoxPassword, "Vui lòng nhập mật khẩu.");
            }
            else
            {
                ePPassword.Clear();
            }

            if (isRegisterMode)
            {
                bool isNotExistUser = _userBLL.RegisterUser(username, password);
                if (isNotExistUser)
                {
                    MessageBox.Show("Đăng ký thành công!", "Đăng kí", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Tài khoản đã tồn tại. Vui lòng thử lại.", "Lỗi đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                bool isValidUser = _userBLL.ValidateLogin(username, password);
                if (isValidUser)
                {
                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Chuyển sang màn hình chính hoặc form khác
                    this.Hide();
                    Main main = new Main(_userBLL);
                    main.Show();
                }
                else
                {
                    MessageBox.Show("Tên tài khoản hoặc mật khẩu không hợp lệ. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }     
        }

        private void linkLabelRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            isRegisterMode = !isRegisterMode;

            if (isRegisterMode)
            {
                textBoxUsername.Text = string.Empty;
                textBoxPassword.Text = string.Empty;
                linkLabelRegister.Text = "Đăng nhập";
                buttonLogin.Text = "Đăng kí";
            }
            else
            {
                textBoxUsername.Text = string.Empty;
                textBoxPassword.Text = string.Empty;
                linkLabelRegister.Text = "Đăng kí";
                buttonLogin.Text = "Đăng nhập";
            }
        }

        private void checkBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowPassword.Checked)
            {
                textBoxPassword.PasswordChar = '\0';
            }
            else
            {
                textBoxPassword.PasswordChar = '*';
            }
        }
    }
}
