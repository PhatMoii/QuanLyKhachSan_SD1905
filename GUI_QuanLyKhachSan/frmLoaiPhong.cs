using BLL_QuanLyKhachSan;
using DTO_QuanLyKhachSan;

namespace GUI_QuanLyKhachSan
{
    public partial class frmLoaiPhong : Form
    {
        public frmLoaiPhong()
        {
            InitializeComponent();
        }

        private void frmLoaiPhong_Load(object sender, EventArgs e)
        {
            LoadLoaiPhong();
        }

        private void LoadLoaiPhong()
        {
            BUSLoaiPhong bus = new BUSLoaiPhong();
            dgvLoaiPhong.DataSource = null;
            dgvLoaiPhong.DataSource = bus.GetLoaiPhongList();
            dgvLoaiPhong.Columns["MaLoaiPhong"].HeaderText = "Mã Loại Phòng";
            dgvLoaiPhong.Columns["TenLoaiPhong"].HeaderText = "Tên Loại Phòng";
            dgvLoaiPhong.Columns["NgayTao"].HeaderText = "Ngày Tạo";
            dgvLoaiPhong.Columns["TrangThai"].Visible = false;
            dgvLoaiPhong.Columns["GhiChu"].HeaderText = "Ghi Chú";
            dgvLoaiPhong.Columns["TrangThaiText"].HeaderText = "Trạng Thái";

            dgvLoaiPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ClearForm()
        {
            txtMaLoaiPhong.Clear();
            txtTenLoaiPhong.Clear();
            dtpNgayTao.Value = DateTime.Now;
            rbDaXacNhan.Checked = true;
            txtGhiChu.Clear();

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtMaLoaiPhong.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaLoaiPhong.Text) || string.IsNullOrEmpty(txtTenLoaiPhong.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo");
                return;
            }

            LoaiPhong lp = new LoaiPhong
            {
                MaLoaiPhong = txtMaLoaiPhong.Text.Trim(),
                TenLoaiPhong = txtTenLoaiPhong.Text.Trim(),
                NgayTao = dtpNgayTao.Value,
                TrangThai = rbDaXacNhan.Checked,
                GhiChu = txtGhiChu.Text.Trim()
            };

            BUSLoaiPhong bus = new BUSLoaiPhong();
            string result = bus.InsertLoaiPhong(lp);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Thêm loại phòng thành công.");
                LoadLoaiPhong();
                ClearForm();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaLoaiPhong.Text) || string.IsNullOrEmpty(txtTenLoaiPhong.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            LoaiPhong lp = new LoaiPhong
            {
                MaLoaiPhong = txtMaLoaiPhong.Text.Trim(),
                TenLoaiPhong = txtTenLoaiPhong.Text.Trim(),
                NgayTao = dtpNgayTao.Value,
                TrangThai = rbDaXacNhan.Checked,
                GhiChu = txtGhiChu.Text.Trim()
            };

            BUSLoaiPhong bus = new BUSLoaiPhong();
            string result = bus.UpdateLoaiPhong(lp);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thành công.");
                LoadLoaiPhong();
                ClearForm();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string ma = txtMaLoaiPhong.Text.Trim();
            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Vui lòng chọn loại phòng để xóa.");
                return;
            }

            DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa?", "Xóa loại phòng", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                BUSLoaiPhong bus = new BUSLoaiPhong();
                bus.DeleteLoaiPhong(ma);
                LoadLoaiPhong();
                ClearForm();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadLoaiPhong();
        }
        private void SearchInAllCells(string KeyWord)
        {
            foreach (DataGridViewRow row in dgvLoaiPhong.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && cell.Value.ToString().ToLower().Contains(KeyWord.ToLower()))
                    {
                        row.Selected = true;
                        break;
                    }
                    else
                    {
                        row.Selected = false;
                    }
                }
            }
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string KeyWord = txtTimKiem.Text;

            if (!string.IsNullOrWhiteSpace(KeyWord))
            {
                SearchInAllCells(KeyWord);
            }
        }

        private void dgvLoaiPhong_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvLoaiPhong.Rows[e.RowIndex];
            txtMaLoaiPhong.Text = row.Cells["MaLoaiPhong"].Value.ToString();
            txtTenLoaiPhong.Text = row.Cells["TenLoaiPhong"].Value.ToString();
            dtpNgayTao.Value = Convert.ToDateTime(row.Cells["NgayTao"].Value);
            rbDaXacNhan.Checked = Convert.ToBoolean(row.Cells["TrangThai"].Value);
            rbChuaXacNhan.Checked = !rbDaXacNhan.Checked;
            txtGhiChu.Text = row.Cells["GhiChu"].Value.ToString();

            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            txtMaLoaiPhong.Enabled = false;
        }
    }
}
