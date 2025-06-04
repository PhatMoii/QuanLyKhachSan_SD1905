using DAL_QuanLyKhachSan;
using DTO_QuanLyKhachSan;
using Microsoft.Data.SqlClient;

namespace DAL_QuanLyKhachSan
{
    public class DALLoaiPhong
    {
        public List<LoaiPhong> SelectBySql(string sql, List<object> args)
        {
            List<LoaiPhong> list = new List<LoaiPhong>();
            try
            {
                SqlDataReader reader = DBUtil.Query(sql, args);
                while (reader.Read())
                {
                    LoaiPhong lp = new LoaiPhong();
                    lp.MaLoaiPhong = reader["MaLoaiPhong"].ToString();
                    lp.TenLoaiPhong = reader["TenLoaiPhong"].ToString();
                    lp.NgayTao = reader["NgayTao"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["NgayTao"]);
                    lp.TrangThai = Convert.ToBoolean(reader["TrangThai"]);
                    lp.GhiChu = reader["GhiChu"].ToString();
                    list.Add(lp);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        public List<LoaiPhong> SelectAll()
        {
            string sql = "SELECT * FROM LoaiPhong";
            return SelectBySql(sql, new List<object>());
        }

        public LoaiPhong? GetLoaiPhong(string maLoaiPhong)
        {
            string sql = "SELECT * FROM LoaiPhong WHERE MaLoaiPhong = @0";
            List<object> args = new List<object>() { maLoaiPhong };
            SqlDataReader reader = DBUtil.Query(sql, args);
            if (reader.Read())
            {
                LoaiPhong lp = new LoaiPhong();
                lp.MaLoaiPhong = reader["MaLoaiPhong"].ToString();
                lp.TenLoaiPhong = reader["TenLoaiPhong"].ToString();
                lp.NgayTao = reader["NgayTao"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["NgayTao"]);
                lp.TrangThai = Convert.ToBoolean(reader["TrangThai"]);
                lp.GhiChu = reader["GhiChu"].ToString();
                return lp;
            }
            return null;
        }

        public void InsertLoaiPhong(LoaiPhong lp)
        {
            try
            {
                string sql = "INSERT INTO LoaiPhong (MaLoaiPhong, TenLoaiPhong, NgayTao, TrangThai, GhiChu) VALUES (@0, @1, @2, @3, @4)";
                List<object> args = new List<object>()
                {
                    lp.MaLoaiPhong,
                    lp.TenLoaiPhong,
                    lp.NgayTao,
                    lp.TrangThai,
                    lp.GhiChu
                };
                DBUtil.Update(sql, args);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm loại phòng", ex);
            }
        }

        public void UpdateLoaiPhong(LoaiPhong lp)
        {
            try
            {
                string sql = "UPDATE LoaiPhong SET TenLoaiPhong = @0, NgayTao = @1, TrangThai = @2, GhiChu = @3 WHERE MaLoaiPhong = @4";
                List<object> args = new List<object>()
                {
                    lp.TenLoaiPhong,
                    lp.NgayTao,
                    lp.TrangThai,
                    lp.GhiChu,
                    lp.MaLoaiPhong
                };
                DBUtil.Update(sql, args);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật loại phòng", ex);
            }
        }

        public void DeleteLoaiPhong(string maLoaiPhong)
        {
            try
            {
                string sql = "DELETE FROM LoaiPhong WHERE MaLoaiPhong = @0";
                List<object> args = new List<object>() { maLoaiPhong };
                DBUtil.Update(sql, args);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi xóa loại phòng", ex);
            }
        }

        public string GenerateMaLoaiPhong()
        {
            string sql = "SELECT TOP 1 MaLoaiPhong FROM LoaiPhong ORDER BY MaLoaiPhong DESC";
            List<object> args = new List<object>();
            SqlDataReader reader = DBUtil.Query(sql, args);
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    string maLP = reader["MaLoaiPhong"].ToString();
                    int so = int.Parse(maLP.Substring(2)) + 1;
                    string maMoi = "LP" + so.ToString("D3");
                    return maMoi;
                }
            }
            return "LP001";
        }
    }
}
