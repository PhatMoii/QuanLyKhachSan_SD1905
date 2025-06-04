using DTO_QuanLyKhachSan;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QuanLyKhachSan
{
    public class NhanVienDAL
    {
        public NhanVienDTO? getNhanVien1(string email, string matkhau)
        {
            const string sql = "SELECT TOP 1 * FROM NhanVien WHERE Email = @0 AND MatKhau = @1";
            var parameters = new List<object> { email, matkhau };

            using SqlDataReader reader = DBUtil.Query(sql, parameters);

            if (reader.Read())
            {
                return new NhanVienDTO
                {
                    MaNV = reader["MaNV"]?.ToString() ?? string.Empty,
                    HoTen = reader["HoTen"]?.ToString() ?? string.Empty,
                    Email = reader["Email"]?.ToString() ?? string.Empty,
                    MatKhau = reader["MatKhau"]?.ToString() ?? string.Empty,
                    VaiTro = reader["VaiTro"] is bool v ? v : false,
                    TinhTrang = reader["TinhTrang"] is bool t ? t : false
                };
            }

            return null;
        }
        public NhanVienDTO SelectByID(string username, string password)
        {
            string query = "SELECT * FROM NhanVien WHERE Email = @p0 AND MatKhau = @p1";
            List<object> args = new List<object> { username, password };

            using SqlCommand cmd = DBUtil.GetCommand(query, args, CommandType.Text);
            SqlConnection conn = cmd.Connection;

            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                NhanVienDTO nv = new NhanVienDTO();
                nv.MaNV = reader.GetString(reader.GetOrdinal("MaNV"));
                nv.HoTen = reader.GetString(reader.GetOrdinal("HoTen"));
                nv.GioiTinh = reader.GetString(reader.GetOrdinal("GioiTinh"));
                nv.Email = reader.GetString(reader.GetOrdinal("Email"));
                nv.MatKhau = reader.GetString(reader.GetOrdinal("MatKhau"));
                nv.DiaChi = reader.GetString(reader.GetOrdinal("DiaChi"));
                nv.VaiTro = reader.GetBoolean("VaiTro");
                nv.TinhTrang = reader.GetBoolean("TinhTrang");
                return nv;
            }

            // ➡️ Nếu không tìm thấy thông tin nhân viên, trả về null
            return null;
        }





        public List<NhanVienDTO> SelectBySql(string sql, List<object> args, CommandType cmdType)
        {
            List<NhanVienDTO> list = new List<NhanVienDTO>();
            try
            {
                SqlDataReader reader = DBUtil.Query(sql, args);
                while (reader.Read())
                {
                    NhanVienDTO entity = new NhanVienDTO();
                    entity.MaNV = reader.GetString(reader.GetOrdinal("MaNV"));
                    entity.HoTen = reader.GetString(reader.GetOrdinal("HoTen"));
                    entity.GioiTinh = reader.GetString(reader.GetOrdinal("GioiTinh"));

                    entity.Email = reader.GetString(reader.GetOrdinal("Email"));
                    entity.MatKhau = reader.GetString(reader.GetOrdinal("MatKhau"));
                    entity.DiaChi = reader.GetString(reader.GetOrdinal("DiaChi"));
                    entity.VaiTro = reader.GetBoolean("VaiTro");
                    entity.TinhTrang = reader.GetBoolean("TinhTrang");
                    list.Add(entity);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }





        public List<NhanVienDTO> selectAll()
        {
            String sql = "SELECT * FROM NhanVien";
            return SelectBySql(sql, new List<object>(), CommandType.Text);
        }









        public void ThemNhanVien(NhanVienDTO nv)
        {
            try
            {
                string sql = @"INSERT INTO NhanVien (MaNV, HoTen, GioiTinh, Email, MatKhau, DiaChi, VaiTro, TinhTrang)
                   VALUES (@0, @1, @2, @3, @4, @5, @6, @7)";
                List<object> thamso = new List<object>();
                thamso.Add(nv.MaNV);        // @0
                thamso.Add(nv.HoTen);       // @1
                thamso.Add(nv.GioiTinh);    // @2
                thamso.Add(nv.Email);       // @3
                thamso.Add(nv.MatKhau);     // @4
                thamso.Add(nv.DiaChi);      // @5
                thamso.Add(nv.VaiTro);      // @6
                thamso.Add(nv.TinhTrang);   // @7

                DBUtil.Update(sql, thamso);
            }
            catch (Exception)
            {
                throw;
            }
        }






        public void XoaNhanVien(string maNhanVien)
        {
            try
            {
                string sql = @"DELETE FROM NhanVien WHERE MaNV = @0";
                List<object> thamso = new List<object>();
                thamso.Add(maNhanVien);  // @0
                DBUtil.Update(sql, thamso);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string generateMaNV()
        {
            string prefix = "NV";
            string sql = "SELECT MAX(MaNV) FROM NhanVien";
            List<object> thamSo = new List<object>();
            object result = DBUtil.ScalarQuery(sql, thamSo);
            if (result != null && result.ToString().StartsWith(prefix))
            {
                string maxCode = result.ToString().Substring(3);
                int newNumber = int.Parse(maxCode) + 1;
                return $"{prefix}{newNumber:D3}";
            }

            return $"{prefix}001";
        }



        public void UpdateNhanVien(NhanVienDTO nv)
        {
            try
            {
                string sql = @"UPDATE NhanVien
                   SET HoTen = @1, Email = @2, MatKhau = @3, VaiTro = @4, TinhTrang = @5
                   WHERE MaNV = @0";

                List<object> thamso = new List<object>();
                thamso.Add(nv.MaNV);        // @0
                thamso.Add(nv.HoTen);       // @1
                thamso.Add(nv.Email);       // @2
                thamso.Add(nv.MatKhau);     // @3
                thamso.Add(nv.VaiTro);      // @4
                thamso.Add(nv.TinhTrang);   // @5

                DBUtil.Update(sql, thamso);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật nhân viên: " + ex.Message);
            }
        }

        }
    }

