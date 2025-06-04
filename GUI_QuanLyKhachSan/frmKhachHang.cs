using DTO_QuanLyKhachSan;
using DAL_QuanLyKhachSan;
using BLL_QuanLyKhachSan;

namespace GUI_QuanLyKhachSan;

public partial class frmKhachHang : Form
{
    public frmKhachHang()
    {
        InitializeComponent();
    }

    private void frmKhachHang_Load(object sender, EventArgs e)
    {
        LoadKhachHang();
    }

    private void LoadKhachHang()
    {
        BUSKhachHang busKH = new BUSKhachHang();
        dgvKhachHang.DataSource = null;
        dgvKhachHang.DataSource = busKH.GetKhachHangList();
        dgvKhachHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
    }

    private void dgvKhachHang_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];
        txtIDKhachHang.Text = row.Cells["KhachHangID"].Value.ToString();
        txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
        txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
        txtGioiTinh.Text = row.Cells["GioiTinh"].Value.ToString(); // TextBox
        txtSDT.Text = row.Cells["SoDienThoai"].Value.ToString();
        txtCCCD.Text = row.Cells["CCCD"].Value.ToString();
        dtpNgayTao.Value = Convert.ToDateTime(row.Cells["NgayTao"].Value);
        bool trangThai = Convert.ToBoolean(row.Cells["TrangThai"].Value);
        rbDaXacNhan.Checked = trangThai;
        rbChuaXacNhan.Checked = !trangThai;
        txtGhiChu.Text = row.Cells["GhiChu"].Value.ToString();

        btnThem.Enabled = false;
        btnSua.Enabled = true;
        btnXoa.Enabled = true;
        txtIDKhachHang.Enabled = false;
    }
    private void SearchInAllCells(string KeyWord)
    {
        foreach (DataGridViewRow row in dgvKhachHang.Rows)
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
    private void btnSua_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtHoTen.Text.Trim()) || string.IsNullOrEmpty(txtSDT.Text.Trim()))
        {
            MessageBox.Show("Vui lòng điền đầy đủ thông tin bắt buộc.");
            return;
        }

        KhachHang kh = new KhachHang();
        kh.KhachHangID = txtIDKhachHang.Text.Trim();
        kh.HoTen = txtHoTen.Text.Trim();
        kh.DiaChi = txtDiaChi.Text.Trim();
        kh.GioiTinh = txtGioiTinh.Text.Trim();
        kh.SoDienThoai = txtSDT.Text.Trim();
        kh.CCCD = txtCCCD.Text.Trim();
        kh.NgayTao = dtpNgayTao.Value;
        kh.TrangThai = rbDaXacNhan.Checked;
        kh.GhiChu = txtGhiChu.Text.Trim();

        BUSKhachHang busKH = new BUSKhachHang();
        string result = busKH.UpdateKhachHang(kh);

        if (string.IsNullOrEmpty(result))
        {
            MessageBox.Show("Cập nhật khách hàng thành công!");
            ClearForm();
            LoadKhachHang();
        }
        else
        {
            MessageBox.Show(result);
        }
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        string maKH = txtIDKhachHang.Text.Trim();
        if (string.IsNullOrEmpty(maKH))
        {
            MessageBox.Show("Vui lòng chọn khách hàng để xóa!");
            return;
        }

        DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", "Xóa khách hàng", MessageBoxButtons.YesNo);
        if (result == DialogResult.Yes)
        {
            BUSKhachHang busKH = new BUSKhachHang();
            busKH.DeleteKhachHang(maKH);
            ClearForm();
            LoadKhachHang();
        }
    }
    private void ClearForm()
    {
        txtIDKhachHang.Clear();
        txtHoTen.Clear();
        txtDiaChi.Clear();
        txtGioiTinh.Clear();
        txtSDT.Clear();
        txtCCCD.Clear();
        dtpNgayTao.Value = DateTime.Now;
        rbDaXacNhan.Checked = false;
        rbChuaXacNhan.Checked = true;
        txtGhiChu.Clear();

        btnThem.Enabled = true;
        btnSua.Enabled = false;
        btnXoa.Enabled = false;
        txtIDKhachHang.Enabled = true;
    }

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        ClearForm();
        LoadKhachHang();
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        KhachHang kh = new KhachHang();
        kh.KhachHangID = txtIDKhachHang.Text.Trim();
        kh.HoTen = txtHoTen.Text.Trim();
        kh.DiaChi = txtDiaChi.Text.Trim();
        kh.GioiTinh = txtGioiTinh.Text.Trim(); // TextBox
        kh.SoDienThoai = txtSDT.Text.Trim();
        kh.CCCD = txtCCCD.Text.Trim();
        kh.NgayTao = dtpNgayTao.Value;
        kh.TrangThai = rbDaXacNhan.Checked; // true nếu đã xác nhận, false nếu chưa
        kh.GhiChu = txtGhiChu.Text.Trim();

        BUSKhachHang busKH = new BUSKhachHang();
        string result = busKH.InsertKhachHang(kh);

        if (string.IsNullOrEmpty(result))
        {
            MessageBox.Show("Thêm khách hàng thành công!");
            ClearForm();
            LoadKhachHang();
        }
        else
        {
            MessageBox.Show(result);
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
