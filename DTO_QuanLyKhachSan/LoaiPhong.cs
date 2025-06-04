namespace DTO_QuanLyKhachSan
{
    public class LoaiPhong
    {
        public string MaLoaiPhong { get; set; }
        public string TenLoaiPhong { get; set; }
        public DateTime? NgayTao { get; set; }
        public bool TrangThai { get; set; }
        public string GhiChu { get; set; }
        // Thuộc tính để hiển thị tên loại phòng
        public string TrangThaiText => TrangThai ? "Đã xác nhận" : "Chưa xác nhận";

    }
}
