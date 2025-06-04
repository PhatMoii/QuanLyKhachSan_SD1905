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
    public class DalPhong
    {
        public string autothemphong()
        {
            string phong = "P";
            string sql = "SELECT MAX(PhongID) FROM Phong"; // Lấy mã phiếu lớn nhất hiện có
            List<object> thamSo = new List<object>();
            object ketqua = DBUtil.ScalarQuery(sql, thamSo);
            if (ketqua != null && ketqua.ToString().StartsWith(phong))
            {
                string soHienTaitrongphieubanhang = ketqua.ToString().Substring(phong.Length); // Lấy phần số phía sau "PHB"
                int soMoi = int.Parse(soHienTaitrongphieubanhang) + 1; // Tăng số lên 1
                return $"{phong}{soMoi:D3}"; // ghep thanh so moi
            }
            else
            {
                // Trường hợp chưa có phiếu nào trong hệ thống => bắt đầu từ "PHB001"
                return $"{phong}001";
            }
        }


        public List<Phong> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<Phong> list = new List<Phong>();
            try
            {
                SqlDataReader reader = DBUtil.Query(sql, args);
                while (reader.Read())
                {
                    Phong entity = new Phong();
                    entity.PhongID = reader.GetString("PhongID");
                    entity.TenPhong = reader.GetString("TenPhong");
                    entity.MaLoaiPhong = reader.GetString("MaLoaiPhong");
entity.GiaPhong = reader.GetDecimal(reader.GetOrdinal("GiaPhong")); // ✅ Đúng
                    entity.NgayTao = reader.GetDateTime("NgayTao");
                    entity.TinhTrang = reader.GetBoolean("TinhTrang");
                    entity.GhiChu = reader.GetString("GhiChu");

                    list.Add(entity);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }




        public List<Phong> selectAll(string phong)
        {
            List<object> param = new List<object>();
            string sql = "SELECT phong.PhongID, phong.TenPhong, loaiphong.MaLoaiPhong, phong.GiaPhong, phong.NgayTao, phong.TinhTrang, phong.GhiChu " +
    "FROM Phong phong INNER JOIN LoaiPhong loaiphong ON phong.MaLoaiPhong = loaiphong.MaLoaiPhong ";

            if (!string.IsNullOrEmpty(phong))
            {
                sql = "SELECT phong.PhongID, phong.TenPhong, loaiphong.MaLoaiPhong, phong.GiaPhong, phong.NgayTao, phong.TinhTrang, phong.GhiChu " +
      "FROM Phong phong INNER JOIN LoaiPhong loaiphong ON phong.MaLoaiPhong = loaiphong.MaLoaiPhong " +
      "WHERE loaiphong.MaLoaiPhong = @0";

                param.Add(phong);
            }

            return SelectBySql(sql, param);
        }




        public void AddPhong(Phong phong)
        {
            try
            {
                string sql = @"INSERT INTO Phong (PhongID, TenPhong, MaLoaiPhong, GiaPhong, NgayTao, TinhTrang, GhiChu) 
                   VALUES (@0, @1, @2, @3, @4, @5, @6 )";
                List<object> thamSo = new List<object>();
                thamSo.Add(phong.PhongID);
                thamSo.Add(phong.TenPhong);
                thamSo.Add(phong.MaLoaiPhong);
                thamSo.Add(phong.GiaPhong);
                thamSo.Add(phong.NgayTao);
                thamSo.Add(phong.TinhTrang);
                thamSo.Add(phong.GhiChu);

                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }
        }



            public void updatephong(Phong phong)
        {
            try
            {
                string sql = @"UPDATE Phong 
                   SET TenPhong = @1, MaLoaiPhong = @2, GiaPhong = @3, NgayTao = @4 , TinhTrang = @5, GhiChu = @6
                   WHERE PhongID = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(phong.PhongID);
                thamSo.Add(phong.TenPhong);
                thamSo.Add(phong.MaLoaiPhong);
                thamSo.Add(phong.GiaPhong);
                thamSo.Add(phong.NgayTao);
                thamSo.Add(phong.TinhTrang);
                thamSo.Add(phong.GhiChu);

                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public void deletePhong(string phong)
        {
            try
            {
                string sql = "DELETE FROM Phong WHERE PhongID = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(phong);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }

        }


    }
}

