using BLL_QuanLyKhachSan;
using DTO_QuanLyKhachSan;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_QuanLyKhachSan
{
    public partial class frmNhanVien : Form
    {
        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            LoadDSNhanVien();
        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void LoadDSNhanVien()
        {
            BLL_NhanVien busnhanvien = new BLL_NhanVien();
            dgvNhanVien.DataSource = null;
            dgvNhanVien.DataSource = busnhanvien.GetNhanVIenList();
            dgvNhanVien.Columns["MaNV"].HeaderText = " Mã Nhân Viên ";
            dgvNhanVien.Columns["HoTen"].HeaderText = " Họ Tên ";
            dgvNhanVien.Columns["GioiTinh"].HeaderText = " Giới Tính ";
            dgvNhanVien.Columns["Email"].HeaderText = " Email ";
            dgvNhanVien.Columns["MatKhau"].HeaderText = " Mật Khẩu ";
            dgvNhanVien.Columns["DiaChi"].HeaderText = " Địa Chỉ ";
            dgvNhanVien.Columns["VaiTroText"].HeaderText = " Vai Trò ";
            dgvNhanVien.Columns["TinhTrangText"].HeaderText = " Trạng Thái ";
            dgvNhanVien.Columns["VaiTro"].Visible = false;
            dgvNhanVien.Columns["TinhTrang"].Visible = false;

            dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNhanVien.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue; // Màu nền khi chọn

        }

        private void dgvNhanVien_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ClearForm()
        {
            // Xóa nội dung các TextBox
            txtMaNhanVien.Text = "";
            txtHoTen.Text = "";
            txtEmail.Text = "";
            txtMatKhau.Text = "";
            txtXNMatKhau.Text = "";
            txtDiaChi.Text = "";

            RadNam.Checked = false;
            RadNu.Checked = false;
            // Reset RadioButton về mặc định (ví dụ: chọn "Nhân Viên")
            RadNhanVien.Checked = false;
            RadQuanLy.Checked = false;


            // Reset Trạng Thái về mặc định (ví dụ: "Hoạt Động")
            RadHoatDong.Checked = false;
            RadTamNgung.Checked = false;


            // Nếu có ô tìm kiếm
            txtTimKiem.Text = "";
            LoadDSNhanVien(); // Hàm này bạn đã có, dùng để nạp lại danh sách nhân viên


            // Bỏ chọn dòng đang chọn trong DataGridView nếu có
            dgvNhanVien.ClearSelection();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maNhanVien = txtMaNhanVien.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string gioiTinh = RadNam.Checked ? "Nam" : (RadNu.Checked ? "Nữ" : string.Empty); string email = txtEmail.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string xacNhanMk = txtXNMatKhau.Text.Trim();
            bool vaiTro;
            if (RadQuanLy.Checked)
            {
                vaiTro = true;
            }
            else
            {
                vaiTro = false;
            }
            bool tinhTrang;
            if (RadHoatDong.Checked)// dang hoat dong
            {
                tinhTrang = true;
            }
            else
            {
                tinhTrang = false;
            }
            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(matKhau) || string.IsNullOrEmpty(xacNhanMk))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }
            if (matKhau != xacNhanMk)
            {
                MessageBox.Show("Mật khẩu không khớp!");
                return;
            }
            NhanVienDTO nhanVien = new NhanVienDTO
            {
                MaNV = maNhanVien,
                HoTen = hoTen,
                GioiTinh = gioiTinh,
                Email = email,
                MatKhau = matKhau,
                DiaChi = diaChi,
                VaiTro = vaiTro,
                TinhTrang = tinhTrang
            };
            BLL_NhanVien busNhanVien = new BLL_NhanVien();
            string result = busNhanVien.CapNhatNhanVien(nhanVien);
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Thêm nhân viên thành công!");
                ClearForm();
                LoadDSNhanVien();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maNhanVien = txtMaNhanVien.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string gioiTinh = RadNam.Checked ? "Nam" : (RadNu.Checked ? "Nữ" : string.Empty); string email = txtEmail.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string xacNhanMk = txtXNMatKhau.Text.Trim();
            bool vaiTro;
            if (RadQuanLy.Checked)
            {
                vaiTro = true;
            }
            else
            {
                vaiTro = false;
            }
            bool tinhTrang;
            if (RadHoatDong.Checked)// dang hoat dong
            {
                tinhTrang = true;
            }
            else
            {
                tinhTrang = false;
            }
            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(matKhau) || string.IsNullOrEmpty(xacNhanMk))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }
            if (matKhau != xacNhanMk)
            {
                MessageBox.Show("Mật khẩu không khớp!");
                return;
            }
            NhanVienDTO nhanVien = new NhanVienDTO
            {
                MaNV = maNhanVien,
                HoTen = hoTen,
                GioiTinh = gioiTinh,
                Email = email,
                MatKhau = matKhau,
                DiaChi = diaChi,
                VaiTro = vaiTro,
                TinhTrang = tinhTrang
            };
            BLL_NhanVien busNhanVien = new BLL_NhanVien();
            string result = busNhanVien.ThemNhanVien(nhanVien);
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Thêm nhân viên thành công!");
                ClearForm();
                LoadDSNhanVien();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maNhanVien = txtMaNhanVien.Text.Trim();
            if (string.IsNullOrEmpty(maNhanVien))
            {
                MessageBox.Show("Vui lòng chọn nhân viên để xóa!");
                return;
            }
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này không?", "Xóa nhân viên", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                BLL_NhanVien busNhanVien = new BLL_NhanVien();
                string message = busNhanVien.XoaNhanVien(maNhanVien);
                if (string.IsNullOrEmpty(message))
                {
                    MessageBox.Show("Xóa nhân viên thành công!");
                    LoadDSNhanVien();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show(message);
                }
            }

        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadDSNhanVien();
        }

        private void HighlightCells(string keyword)
        {
            keyword = keyword.ToLower();

            foreach (DataGridViewRow row in dgvNhanVien.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString().ToLower().Contains(keyword))
                    {
                        cell.Style.BackColor = Color.Yellow; // tô màu vàng
                    }
                    else
                    {
                        cell.Style.BackColor = Color.White;  // reset lại màu nền
                    }
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                LoadDSNhanVien(); // load lại toàn bộ
            }

            // Dù có rỗng hay không, vẫn highlight theo keyword
            HighlightCells(keyword);
        }

        private void dgvNhanVien_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMaNhanVien_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel9_Click(object sender, EventArgs e)
        {

        }

        private void txtGioiTinh_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel8_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel10_Click(object sender, EventArgs e)
        {
        }

        private void guna2HtmlLabel6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtHoTen_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvNhanVien_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];

                txtMaNhanVien.Text = row.Cells["MaNV"].Value?.ToString() ?? "";
                txtHoTen.Text = row.Cells["HoTen"].Value?.ToString() ?? "";
                string gioiTinh = row.Cells["GioiTinh"].Value.ToString();
                if (gioiTinh == "Nam")
                {
                    RadNam.Checked = true;
                }
                else if (gioiTinh == "Nữ")
                {
                    RadNu.Checked = true;
                }
                else
                {
                    RadNam.Checked = false;
                    RadNu.Checked = false;
                }
                txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
                txtMatKhau.Text = row.Cells["MatKhau"].Value?.ToString() ?? "";

                txtXNMatKhau.Text = row.Cells["MatKhau"].Value?.ToString() ?? "";
                txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString() ?? "";

                // Vai Trò
                int vaiTro = Convert.ToInt32(row.Cells["VaiTro"].Value);
                bool vaitro = Convert.ToBoolean(row.Cells["VaiTro"].Value);
                if (vaitro == false)
                {
                    RadNhanVien.Checked = true;
                }
                else
                {
                    RadQuanLy.Checked = true;
                }

                // Trạng Thái
                int trangThai = Convert.ToInt32(row.Cells["TinhTrang"].Value);
                RadHoatDong.Checked = (trangThai == 1);
                RadTamNgung.Checked = (trangThai == 0);
            }
        }
    }
}
