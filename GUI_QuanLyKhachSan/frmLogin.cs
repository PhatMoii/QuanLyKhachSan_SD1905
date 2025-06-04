using BLL_QuanLyKhachSan;
using DTO_QuanLyKhachSan;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Util_QuanLyKhachSan;

namespace GUI_QuanLyKhachSan
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtTaiKhoan.Text = "Tên đăng nhập";
            txtTaiKhoan.ForeColor = Color.Gray;

            txtMatKhau.Text = "Mật khẩu";
            txtMatKhau.ForeColor = Color.Gray;
            txtMatKhau.UseSystemPasswordChar = false;
        }




        BLL_NhanVien bll = new BLL_NhanVien();

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string username = txtTaiKhoan.Text;
            string password = txtMatKhau.Text;
            NhanVienDTO? nhanVien = bll.DangNhap(username, password);

            if (nhanVien == null)
            {
                MessageBox.Show("Tài khoản mật khẩu không chính xác");
                return;
            }

            if (nhanVien.TinhTrang == false)
            {
                MessageBox.Show("Tài khoản đã bị khóa");
                return;
            }

            AuthUtil.user = nhanVien;

            frmMainForm main = new frmMainForm();
            main.Show();
            this.Hide();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTenDangNhap_TextChanged(object sender, EventArgs e)
        {

        }




        private void txtTaiKhoan_Enter(object sender, EventArgs e)
        {
            if (txtTaiKhoan.Text == "Tên đăng nhập")
            {
                txtTaiKhoan.Text = "";
                txtTaiKhoan.ForeColor = Color.White;
            }
        }

        private void txtTaiKhoan_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaiKhoan.Text))
            {
                txtTaiKhoan.Text = "Tên đăng nhập";
                txtTaiKhoan.ForeColor = Color.Gray;
            }
        }

        private void txtMatKhau_Enter(object sender, EventArgs e)
        {
            if (txtMatKhau.Text == "Mật khẩu")
            {
                txtMatKhau.Text = "";
                txtMatKhau.ForeColor = Color.White;
            }

            // Chỉ dùng password char nếu chưa bật "Hiện mật khẩu"
            if (!chkHienThiMatKhau.Checked)
            {
                txtMatKhau.UseSystemPasswordChar = true;
            }
        }

        private void txtMatKhau_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                txtMatKhau.Text = "Mật khẩu";
                txtMatKhau.ForeColor = Color.White;
                txtMatKhau.UseSystemPasswordChar = false;
            }
        }

        private void chkHienThiMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            // Nếu checkbox bật và text hiện không phải placeholder
            if (chkHienThiMatKhau.Checked && txtMatKhau.Text != "Mật khẩu")
            {
                txtMatKhau.UseSystemPasswordChar = false;
            }
            else if (txtMatKhau.Text != "Mật khẩu") // nếu không bật mà không phải placeholder
            {
                txtMatKhau.UseSystemPasswordChar = true;
            }
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
