using System.Data;
using DAL_QuanLyKhachSan;
using Microsoft.Data.SqlClient;
using DTO_QuanLyKhachSan;

namespace DAL_QuanLyKhachSan
{
    public class DALKhachHang
    {
        // Lấy khách hàng theo mã
        public KhachHang? GetKhachHang(string maKhachHang)
        {
            string sql = "SELECT * FROM KhachHang WHERE KhachHangID = @0";
            List<object> thamSo = new List<object>() { maKhachHang };
            SqlDataReader reader = DBUtil.Query(sql, thamSo);

            if (reader.Read())
            {
                return MapKhachHang(reader);
            }
            return null;
        }

        // Hàm map từ SqlDataReader sang KhachHang
        private KhachHang MapKhachHang(SqlDataReader reader)
        {
            return new KhachHang
            {
                KhachHangID = reader["KhachHangID"].ToString(),
                HoTen = reader["HoTen"].ToString(),
                DiaChi = reader["DiaChi"].ToString(),
                GioiTinh = reader["GioiTinh"].ToString(),
                SoDienThoai = reader["SoDienThoai"].ToString(),
                CCCD = reader["CCCD"].ToString(),
                NgayTao = reader["NgayTao"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(reader["NgayTao"]),
                TrangThai = Convert.ToBoolean(reader["TrangThai"]),
                GhiChu = reader["GhiChu"].ToString()
            };
        }

        // Lấy danh sách theo câu SQL tùy chỉnh
        public List<KhachHang> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<KhachHang> list = new List<KhachHang>();
            try
            {
                SqlDataReader reader = DBUtil.Query(sql, args);
                while (reader.Read())
                {
                    list.Add(MapKhachHang(reader));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn dữ liệu khách hàng", ex);
            }
            return list;
        }

        // Lấy tất cả khách hàng
        public List<KhachHang> SelectAll()
        {
            string sql = "SELECT * FROM KhachHang";
            return SelectBySql(sql, new List<object>());
        }

        // Thêm khách hàng mới
        public void InsertKhachHang(KhachHang kh)
        {
            try
            {
                string sql = @"INSERT INTO KhachHang (KhachHangID, HoTen, DiaChi, GioiTinh, SoDienThoai, CCCD, NgayTao, TrangThai, GhiChu)
                               VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8)";
                List<object> thamSo = new List<object>
                {
                    kh.KhachHangID,
                    kh.HoTen,
                    kh.DiaChi,
                    kh.GioiTinh,
                    kh.SoDienThoai,
                    kh.CCCD,
                    kh.NgayTao,
                    kh.TrangThai,
                    kh.GhiChu
                };
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm khách hàng", ex);
            }
        }

        // Cập nhật khách hàng
        public void UpdateKhachHang(KhachHang kh)
        {
            try
            {
                string sql = @"UPDATE KhachHang 
                               SET HoTen = @0, DiaChi = @1, GioiTinh = @2, SoDienThoai = @3, CCCD = @4, NgayTao = @5, TrangThai = @6, GhiChu = @7
                               WHERE KhachHangID = @8";
                List<object> thamSo = new List<object>
                {
                    kh.HoTen,
                    kh.DiaChi,
                    kh.GioiTinh,
                    kh.SoDienThoai,
                    kh.CCCD,
                    kh.NgayTao,
                    kh.TrangThai,
                    kh.GhiChu,
                    kh.KhachHangID
                };
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật khách hàng", ex);
            }
        }

        // Xóa khách hàng
        public void DeleteKhachHang(string maKH)
        {
            try
            {
                string sql = "DELETE FROM KhachHang WHERE KhachHangID = @0";
                List<object> thamSo = new List<object> { maKH };
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi xóa khách hàng", ex);
            }
        }

        // Tự động sinh mã khách hàng (KH001, KH002,...)
        public string GenerateMaKH()
        {
            string sql = "SELECT TOP 1 KhachHangID FROM KhachHang ORDER BY KhachHangID DESC";
            SqlDataReader reader = DBUtil.Query(sql, new List<object>());
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    string maKH = reader["KhachHangID"].ToString();
                    int so = int.Parse(maKH.Substring(2)) + 1;
                    return "KH" + so.ToString("D3");
                }
            }
            return "KH001";
        }
    }
}
