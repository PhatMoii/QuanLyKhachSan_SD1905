namespace DTO_QuanLyKhachSan
{
    public class KhachHang
    {
        public string KhachHangID { get; set; }
        public string HoTen { get; set; }
        public string DiaChi { get; set; }
        public string GioiTinh { get; set; }
        public string SoDienThoai { get; set; }
        public string CCCD { get; set; }
        public DateTime? NgayTao { get; set; }
        public bool TrangThai { get; set; }
        public string GhiChu { get; set; }
        public string TrangThaiText => TrangThai ? "Đã xác nhận" : "Chưa xác nhận";
    }
}
