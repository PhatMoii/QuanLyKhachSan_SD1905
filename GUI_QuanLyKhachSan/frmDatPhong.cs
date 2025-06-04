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
    public partial class frmDatPhong : Form
    {
        public frmDatPhong()
        {
            InitializeComponent();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void DatPhong_Load(object sender, EventArgs e)
        {
            ClearForm();
            LoadNhanVien();
            Loadkhachhang();
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void ClearForm()
        {
            btnthem.Enabled = true;
            btnsua.Enabled = false;
            btnxoa.Enabled = true;
            txthoadon.Clear();
            txtTimkiem.Clear();
            txtghichu.Clear();
            dtpngayden.Enabled = true;
            dtpngayden.Value = DateTime.Now;
            dtpngaydi.Enabled = true;
            dtpngaydi.Value = DateTime.Now;

        }

        private void LoadNhanVien()
        {
            try
            {
                BLL_NhanVien bus = new BLL_NhanVien();
                List<NhanVienDTO> nv = bus.GetNhanVIenList();
                cbonhanvien.DataSource = nv;
                cbonhanvien.ValueMember = "MaNV";
                cbonhanvien.DisplayMember = "HoTen";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách loại sản phẩm" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Loadkhachhang()
        {
            try
            {
                BUSKhachHang bus = new BUSKhachHang();
                List<KhachHang> kh = bus.GetKhachHangList();
                cbonhanvien.DataSource = kh;
                cbonhanvien.ValueMember = "KhachHangID";
                cbonhanvien.DisplayMember = "HoTen";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách loại sản phẩm" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPhongID()
        {

        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

        }
    }
}
