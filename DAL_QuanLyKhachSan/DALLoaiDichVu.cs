using System.Data;
using DAL_QuanLyKhachSan;
using DTO_QuanLyKhachSan;
using Microsoft.Data.SqlClient;

namespace DAL_QuanLyKhachSan
{
    public class DALLoaiDichVu
    {
        public List<LoaiDichVu> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<LoaiDichVu> list = new List<LoaiDichVu>();
            try
            {
                SqlDataReader reader = DBUtil.Query(sql, args);
                while (reader.Read())
                {
                    LoaiDichVu ldv = new LoaiDichVu();
                    ldv.LoaiDichVuID = reader["LoaiDichVuID"].ToString();
                    ldv.TenDichVu = reader["TenDichVu"].ToString();
                    ldv.GiaDichVu = Convert.ToDecimal(reader["GiaDichVu"]);
                    ldv.DonViTinh = reader["DonViTinh"].ToString();
                    ldv.NgayTao = reader["NgayTao"] == DBNull.Value ? null : (DateTime?)reader["NgayTao"];
                    ldv.TrangThai = Convert.ToBoolean(reader["TrangThai"]);
                    ldv.GhiChu = reader["GhiChu"].ToString();
                    list.Add(ldv);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi đọc dữ liệu Loại Dịch Vụ", ex);
            }
            return list;
        }

        public List<LoaiDichVu> SelectAll()
        {
            string sql = "SELECT * FROM LoaiDichVu";
            return SelectBySql(sql, new List<object>());
        }

        public void Insert(LoaiDichVu ldv)
        {
            try
            {
                string sql = @"INSERT INTO LoaiDichVu (LoaiDichVuID, TenDichVu, GiaDichVu, DonViTinh, NgayTao, TrangThai, GhiChu)
                               VALUES (@0, @1, @2, @3, @4, @5, @6)";
                List<object> args = new List<object>
                {
                    ldv.LoaiDichVuID,
                    ldv.TenDichVu,
                    ldv.GiaDichVu,
                    ldv.DonViTinh,
                    ldv.NgayTao,
                    ldv.TrangThai,
                    ldv.GhiChu
                };
                DBUtil.Update(sql, args);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm Loại Dịch Vụ", ex);
            }
        }

        public void Update(LoaiDichVu ldv)
        {
            try
            {
                string sql = @"UPDATE LoaiDichVu SET TenDichVu=@0, GiaDichVu=@1, DonViTinh=@2, NgayTao=@3, TrangThai=@4, GhiChu=@5
                               WHERE LoaiDichVuID=@6";
                List<object> args = new List<object>
                {
                    ldv.TenDichVu,
                    ldv.GiaDichVu,
                    ldv.DonViTinh,
                    ldv.NgayTao,
                    ldv.TrangThai,
                    ldv.GhiChu,
                    ldv.LoaiDichVuID
                };
                DBUtil.Update(sql, args);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật Loại Dịch Vụ", ex);
            }
        }

        public void Delete(string id)
        {
            try
            {
                string sql = "DELETE FROM LoaiDichVu WHERE LoaiDichVuID=@0";
                List<object> args = new List<object> { id };
                DBUtil.Update(sql, args);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi xóa Loại Dịch Vụ", ex);
            }
        }

        public string GenerateNewID()
        {
            string sql = "SELECT TOP 1 LoaiDichVuID FROM LoaiDichVu ORDER BY LoaiDichVuID DESC";
            SqlDataReader reader = DBUtil.Query(sql, new List<object>());
            if (reader.HasRows)
            {
                reader.Read();
                string lastID = reader["LoaiDichVuID"].ToString();
                int so = int.Parse(lastID.Substring(3)) + 1;
                return "LDV" + so.ToString("D3");
            }
            return "LDV001";
        }

        public bool UpdateTrangThai(string id, bool trangThai)
        {
            try
            {
                string sql = "UPDATE LoaiDichVu SET TrangThai=@0 WHERE LoaiDichVuID=@1";
                List<object> args = new List<object> { trangThai, id };
                DBUtil.Update(sql, args);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật trạng thái", ex);
            }
        }
    }
}
