using DTO_QuanLyKhachSan;
using DAL_QuanLyKhachSan;
using BLL_QuanLyKhachSan;

namespace GUI_QuanLyKhachSan;

    public partial class frmLoaiDichVu : Form
    {
        public frmLoaiDichVu()
        {
            InitializeComponent();
        }
        private void frmLoaiDichVu_Load(object sender, EventArgs e)
        {
            LoadLoaiDichVu();
        }

        private void LoadLoaiDichVu()
        {
            BUSLoaiDichVu bus = new BUSLoaiDichVu();
            dgvLoaiDichVu.DataSource = null;
            dgvLoaiDichVu.DataSource = bus.GetLoaiDichVuList();

            dgvLoaiDichVu.Columns["LoaiDichVuID"].HeaderText = "Mã Dịch Vụ";
            dgvLoaiDichVu.Columns["TenDichVu"].HeaderText = "Tên Dịch Vụ";
            dgvLoaiDichVu.Columns["GiaDichVu"].HeaderText = "Giá Dịch Vụ";
            dgvLoaiDichVu.Columns["DonViTinh"].HeaderText = "Đơn Vị Tính";
            dgvLoaiDichVu.Columns["NgayTao"].HeaderText = "Ngày Tạo";
            dgvLoaiDichVu.Columns["TrangThai"].Visible = false;
            dgvLoaiDichVu.Columns["GhiChu"].HeaderText = "Ghi Chú";
            dgvLoaiDichVu.Columns["TrangThaiText"].HeaderText = "Trạng Thái";

            dgvLoaiDichVu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ClearForm()
        {
            txtIDLoaiDichVu.Clear();
            txtTenDichVu.Clear();
            txtGiaDichVu.Clear();
            txtDonViTinh.Clear();
            dtpNgayTao.Value = DateTime.Now;
            rbDaXacNhan.Checked = true;
            txtGhiChu.Clear();

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtIDLoaiDichVu.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenDichVu.Text) || string.IsNullOrEmpty(txtGiaDichVu.Text) || string.IsNullOrEmpty(txtDonViTinh.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo");
                return;
            }

            LoaiDichVu ldv = new LoaiDichVu
            {
                TenDichVu = txtTenDichVu.Text.Trim(),
                GiaDichVu = decimal.Parse(txtGiaDichVu.Text.Trim()),
                DonViTinh = txtDonViTinh.Text.Trim(),
                NgayTao = dtpNgayTao.Value,
                TrangThai = rbDaXacNhan.Checked,
                GhiChu = txtGhiChu.Text.Trim()
            };

            BUSLoaiDichVu bus = new BUSLoaiDichVu();
            string result = bus.InsertLoaiDichVu(ldv);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Thêm dịch vụ thành công.");
                LoadLoaiDichVu();
                ClearForm();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIDLoaiDichVu.Text) || string.IsNullOrEmpty(txtTenDichVu.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            LoaiDichVu ldv = new LoaiDichVu
            {
                LoaiDichVuID = txtIDLoaiDichVu.Text.Trim(),
                TenDichVu = txtTenDichVu.Text.Trim(),
                GiaDichVu = decimal.Parse(txtGiaDichVu.Text.Trim()),
                DonViTinh = txtDonViTinh.Text.Trim(),
                NgayTao = dtpNgayTao.Value,
                TrangThai = rbDaXacNhan.Checked,
                GhiChu = txtGhiChu.Text.Trim()
            };

            BUSLoaiDichVu bus = new BUSLoaiDichVu();
            string result = bus.UpdateLoaiDichVu(ldv);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật dịch vụ thành công.");
                LoadLoaiDichVu();
                ClearForm();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string ma = txtIDLoaiDichVu.Text.Trim();
            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Vui lòng chọn dịch vụ để xóa.");
                return;
            }

            DialogResult dr = MessageBox.Show("Bạn có chắc muốn xóa?", "Xóa dịch vụ", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                BUSLoaiDichVu bus = new BUSLoaiDichVu();
                bus.DeleteLoaiDichVu(ma);
                LoadLoaiDichVu();
                ClearForm();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadLoaiDichVu();
        }

        private void dgvLoaiDichVu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvLoaiDichVu.Rows[e.RowIndex];
            txtIDLoaiDichVu.Text = row.Cells["LoaiDichVuID"].Value.ToString();
            txtTenDichVu.Text = row.Cells["TenDichVu"].Value.ToString();
            txtGiaDichVu.Text = row.Cells["GiaDichVu"].Value.ToString();
            txtDonViTinh.Text = row.Cells["DonViTinh"].Value.ToString();
            dtpNgayTao.Value = Convert.ToDateTime(row.Cells["NgayTao"].Value);
            rbDaXacNhan.Checked = Convert.ToBoolean(row.Cells["TrangThai"].Value);
            rbChuaXacNhan.Checked = !rbDaXacNhan.Checked;
            txtGhiChu.Text = row.Cells["GhiChu"].Value.ToString();

            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            txtIDLoaiDichVu.Enabled = false;
        }
        private void SearchInAllCells(string KeyWord)
        {
            foreach (DataGridViewRow row in dgvLoaiDichVu.Rows)
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
    }

