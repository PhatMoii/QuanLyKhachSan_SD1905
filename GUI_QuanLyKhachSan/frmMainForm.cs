using BLL_QuanLyKhachSan;
using DTO_QuanLyKhachSan;
using Util_QuanLyKhachSan;

namespace GUI_QuanLyKhachSan
{
    public partial class frmMainForm : Form
    {
        public frmMainForm()
        {
            InitializeComponent();
        }





        private void frmMainForm_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmNhanVien()); // frmNhanVien là form quản lý nhân viên
        }

        private void guna2Panel6_Paint(object sender, PaintEventArgs e)
        {

        }
        private void OpenChildForm(Form childForm)
        {
            guna2Panel6.Controls.Clear();  // Xóa form cũ trong panel (nếu có)
            childForm.TopLevel = false;    // Đặt form con không phải là top-level
            childForm.FormBorderStyle = FormBorderStyle.None; // Ẩn viền form con
            childForm.Dock = DockStyle.Fill;  // Chiếm toàn bộ panel
            guna2Panel6.Controls.Add(childForm); // Thêm form vào panel
            childForm.Show(); // Hiển thị form
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmPhong()); // frmNhanVien là form quản lý nhân viên

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmDatPhong()); // frmNhanVien là form quản lý nhân viên

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmKhachHang()); // frmNhanVien là form quản lý nhân viên

        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmLoaiPhong()); // frmNhanVien là form quản lý nhân viên

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmLoaiDichVu()); // frmNhanVien là form quản lý nhân viên
        }
    }
}
