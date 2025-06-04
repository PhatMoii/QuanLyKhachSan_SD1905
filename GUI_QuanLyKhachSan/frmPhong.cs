using BLL_QuanLyKhachSan;
using DAL_QuanLyKhachSan;
using DTO_QuanLyKhachSan;
using Microsoft.VisualBasic;
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
    public partial class frmPhong : Form
    {
        public frmPhong()
        {
            InitializeComponent();
        }


        public void ClearForm(string mathe)
        {
            btnthem.Enabled = true;
            btnsua.Enabled = false;
            btnxoa.Enabled = true;
            txttenphong.Clear();
            txtghichu.Clear();
            txtgiaphong.Clear();
            txtphongid.Clear();
            cbomaloaiphong.Enabled = true;
            dtpngaytao.Enabled = true;
            dtpngaytao.Value = DateTime.Now;
            raddadat.Enabled = true;
            radtrong.Enabled = true;
            dtpngaytao.Value = DateTime.Now;
            raddadat.Checked = true;

        }

        private bool isLoadingMaLoaiPhongData = true;


        private void Loadmaloaiphong()
        {
            BUSLoaiPhong bus = new BUSLoaiPhong();
            List<LoaiPhong> lst = bus.GetLoaiPhongList();
            lst.Insert(0, new LoaiPhong() { MaLoaiPhong = string.Empty, TenLoaiPhong = string.Format("--Tất Cả--") });
            cbomaloaiphong.DataSource = lst;
            cbomaloaiphong.ValueMember = "MaLoaiPhong";
            cbomaloaiphong.DisplayMember = "TenLoaiPhong";
            isLoadingMaLoaiPhongData = false;
        }


        public void LoadPhong(string phong)
        {
            BUSPhong bus = new BUSPhong();
            List<Phong> lst = bus.GetListPhong(phong);
            if (lst.Count > 0)
            {
                dgvphong.DataSource = lst;
                dgvphong.Columns["PhongID"].HeaderText = "ID Phòng";
                dgvphong.Columns["TenPhong"].HeaderText = "Tên Phòng";
                dgvphong.Columns["MaLoaiPhong"].HeaderText = "Mã Loại Phòng";
                dgvphong.Columns["GiaPhong"].HeaderText = "Giá Phòng";
                dgvphong.Columns["NgayTao"].HeaderText = "Ngày Tạo";
                dgvphong.Columns["TinhTrang"].HeaderText = "Tình Trạng";
                dgvphong.Columns["GhiChu"].HeaderText = "Ghi Chú";
                dgvphong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else
            {
                dgvphong.DataSource = null;
            }
        }


        private void btnthem_Click(object sender, EventArgs e)
        {
            string tenphong = txttenphong.Text.Trim();
            string ghichu = txtghichu.Text.Trim();
            string giaphong = txtgiaphong.Text.Trim();

            // ✅ Lấy đúng mã loại phòng từ combobox
            if (cbomaloaiphong.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Mã Loại Phòng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string maloaiphong = cbomaloaiphong.SelectedValue.ToString();

            DateTime ngaytao = dtpngaytao.Value;

            // ✅ Gán tình trạng phòng
            bool trangthai = raddadat.Checked;

            // ✅ Kiểm tra giá phòng
            if (!decimal.TryParse(giaphong, out decimal giaPhongValue))
            {
                MessageBox.Show("Giá phòng phải là số hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ✅ Tạo đối tượng Phong
            Phong phong = new Phong()
            {
                PhongID = string.Empty, // Để hệ thống tự sinh mã?
                MaLoaiPhong = maloaiphong,
                TenPhong = tenphong,
                NgayTao = ngaytao,
                TinhTrang = trangthai,
                GiaPhong = giaPhongValue,
                GhiChu = ghichu
            };

            // ✅ Gọi BLL xử lý
            BUSPhong bus = new BUSPhong();
            string result = bus.AddPhong(phong);

            // ✅ Hiển thị kết quả
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Thêm phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPhong("");
                ClearForm(maloaiphong);
                Loadmaloaiphong();
                cbomaloaiphong.SelectedValue = maloaiphong;
            }
            else
            {
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmPhong_Load(object sender, EventArgs e)
        {
            LoadPhong("");
            Loadmaloaiphong();
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            string phongid = txtphongid.Text.Trim();
            string maLoai = cbomaloaiphong.SelectedValue?.ToString();
            string tenLoai = cbomaloaiphong.Text;
            if (string.IsNullOrEmpty(phongid))
            {
                if (dgvphong.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvphong.SelectedRows[0];
                    phongid = selectedRow.Cells["PhongID"].Value.ToString();
                    maLoai = selectedRow.Cells["MaLoaiPhong"].Value.ToString();
                    tenLoai = selectedRow.Cells["TenLoaiPhong"].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn thông tin phiếu bán hàng cần xóa xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (string.IsNullOrEmpty(phongid))
            {
                MessageBox.Show("Xóa không thành công.");
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa phiếu bán hàng {phongid} - {tenLoai}?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                BUSPhong bus = new BUSPhong();
                string kq = bus.DeletePhong(phongid);

                if (string.IsNullOrEmpty(kq))
                {
                    MessageBox.Show($"Xóa thông tin phiếu bán hàng {phongid} - {tenLoai} thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm(maLoai);
                    LoadPhong("");
                    Loadmaloaiphong();

                    cbomaloaiphong.SelectedValue = maLoai;
                }
                else
                {
                    MessageBox.Show(kq, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            string maloai = cbomaloaiphong.SelectedValue.ToString();
            string phongid = txtphongid.Text.Trim();
            string ghichu = txtghichu.Text.Trim();
            string donGiaText = txtgiaphong.Text.Trim();

            DateTime ngaytao = dtpngaytao.Value;
            bool trangthai;
            if (raddadat.Checked)
            {
                trangthai = true; // Đã thanh toán
            }
            else
            {
                trangthai = false; // Chưa thanh toán
            }
            if (string.IsNullOrEmpty(maloai))
            {
                MessageBox.Show("Vui lòng chọn thẻ và nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Chuyển đổi đơn giá
            if (!decimal.TryParse(donGiaText, out decimal donGia))
            {
                MessageBox.Show("Đơn giá không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Phong phong = new Phong
            {
                PhongID = txtphongid.Text.Trim(),
                MaLoaiPhong = maloai,
                TenPhong = txttenphong.Text.Trim(),
                GiaPhong = donGia,
                NgayTao = ngaytao,
                TinhTrang = trangthai,
                GhiChu = ghichu,
            };
            BUSPhong bus = new BUSPhong();
            string result = bus.UpdatePhong(phong);
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật phiếu bán hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Loadmaloaiphong();
                ClearForm(maloai);
                LoadPhong("");
                cbomaloaiphong.SelectedValue = maloai;
            }
            else
            {
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }







        private void dgvphong_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvphong.Rows[e.RowIndex];
            txtphongid.Text = row.Cells["PhongID"].Value.ToString();
            txttenphong.Text = row.Cells["TenPhong"].Value.ToString();
            cbomaloaiphong.SelectedValue = row.Cells["MaLoaiPhong"].Value.ToString();
            txtgiaphong.Text = row.Cells["GiaPhong"].Value.ToString();
            txtghichu.Text = row.Cells["GhiChu"].Value.ToString();
            dtpngaytao.Value = Convert.ToDateTime(row.Cells["NgayTao"].Value);
            raddadat.Checked = Convert.ToBoolean(row.Cells["TinhTrang"].Value);
            radtrong.Checked = !raddadat.Checked;

            btnthem.Enabled = false;
            btnsua.Enabled = true;
            btnxoa.Enabled = true;
        }

        private void btnmoi_Click(object sender, EventArgs e)
        {
            ClearForm("");
            Loadmaloaiphong();
            LoadPhong("");
            cbomaloaiphong.SelectedValue = string.Empty;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                LoadPhong(""); // load lại toàn bộ
            }

            // Dù có rỗng hay không, vẫn highlight theo keyword
            SearchInAllCells(keyword);
        }
        private void SearchInAllCells(string KeyWord)
        {
            foreach (DataGridViewRow row in dgvphong.Rows)
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
    }
}
