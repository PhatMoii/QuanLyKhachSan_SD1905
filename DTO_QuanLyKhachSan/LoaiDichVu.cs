namespace DTO_QuanLyKhachSan
{
    public class LoaiDichVu
    {
        public string LoaiDichVuID { get; set; }
        public string TenDichVu { get; set; }
        public decimal GiaDichVu { get; set; }
        public string DonViTinh { get; set; }
        public DateTime? NgayTao { get; set; }
        public bool TrangThai { get; set; }
        public string GhiChu { get; set; }
        public string TrangThaiText => TrangThai ? "Đã xác nhận" : "Chưa xác nhận";
    }
}
