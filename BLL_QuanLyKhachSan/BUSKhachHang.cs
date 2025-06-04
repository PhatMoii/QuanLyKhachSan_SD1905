using DTO_QuanLyKhachSan;
using DAL_QuanLyKhachSan;

namespace BLL_QuanLyKhachSan
{
    public class BUSKhachHang
    {
        DALKhachHang dalkh = new DALKhachHang();

        // Lấy khách hàng theo mã
        public KhachHang? GetKhachHangByMa(string maKhachHang)
        {
            if (string.IsNullOrEmpty(maKhachHang))
            {
                return null;
            }
            return dalkh.GetKhachHang(maKhachHang);
        }

        // Lấy danh sách tất cả khách hàng
        public List<KhachHang> GetKhachHangList()
        {
            return dalkh.SelectAll();
        }

        // Thêm khách hàng mới
        public string InsertKhachHang(KhachHang kh)
        {
            try
            {
                kh.KhachHangID = dalkh.GenerateMaKH();
                if (string.IsNullOrEmpty(kh.KhachHangID))
                {
                    return "Vui lòng nhập đầy đủ thông tin";
                }
                dalkh.InsertKhachHang(kh);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi thêm khách hàng: " + ex.Message;
            }
        }

        // Cập nhật khách hàng
        public string UpdateKhachHang(KhachHang kh)
        {
            try
            {
                if (string.IsNullOrEmpty(kh.KhachHangID))
                {
                    return "Vui lòng nhập đầy đủ thông tin";
                }
                dalkh.UpdateKhachHang(kh);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi cập nhật khách hàng: " + ex.Message;
            }
        }

        // Xóa khách hàng
        public void DeleteKhachHang(string maKH)
        {
            try
            {
                dalkh.DeleteKhachHang(maKH);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi xóa khách hàng", ex);
            }
        }

        // Sinh mã khách hàng tự động
        public string GenerateMaKH()
        {
            return dalkh.GenerateMaKH();
        }
    }
}
