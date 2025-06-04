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
    public class DALDatPhongcs
    {
        public string autoHoaDonThueID()
        {
            string hoadon = "HD";
            string sql = "SELECT MAX(HoaDonThueID) FROM DatPhong"; // Lấy mã phiếu lớn nhất hiện có
            List<object> thamSo = new List<object>();
            object ketqua = DBUtil.ScalarQuery(sql, thamSo);
            if (ketqua != null && ketqua.ToString().StartsWith(hoadon))
            {
                string soHienTaitrongphieubanhang = ketqua.ToString().Substring(hoadon.Length); // Lấy phần số phía sau "PHB"
                int soMoi = int.Parse(soHienTaitrongphieubanhang) + 1; // Tăng số lên 1
                return $"{hoadon}{soMoi:D3}"; // ghep thanh so moi
            }
            else
            {
                // Trường hợp chưa có phiếu nào trong hệ thống => bắt đầu từ "PHB001"
                return $"{hoadon}001";
            }
        }




        public List<DatPhongDTO> SelectBySql(string sql, List<object> args, CommandType cmdType = CommandType.Text)
        {
            List<DatPhongDTO> list = new List<DatPhongDTO>();
            try
            {
                SqlDataReader reader = DBUtil.Query(sql, args);
                while (reader.Read())
                {
                    DatPhongDTO entity = new DatPhongDTO();
                    entity.HoaDonThueID = reader.GetString("HoaDonThueID");
                    entity.KhachHangID = reader.GetString("KhachHangID");
                    entity.PhongID = reader.GetString("PhongID");
                    entity.NgayDen = reader.GetDateTime("NgayDen");
                    entity.NgayDi = reader.GetDateTime("NgayDi");
                    entity.MaNV = reader.GetString("MaNV");
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



        public List<DatPhongDTO> selectAll(string hoadon)
        {
            //String sql = "SELECT * FROM PhieuBanHang";
            List<object> param = new List<object>();
            string sql = "SELECT datphong.HoaDonThueID, khachhang.KhachHangID, phong.PhongID, datphong.NgayDen, datphong.NgayDi, nhanvien.MaNV, datphong.GhiChu " +
                "FROM DatPhong datphong INNER JOIN NhanVien nhanvien ON datphong.MaNV = nhanvien.MaNV " +
                "INNER JOIN KhachHang khachhang ON khachhang.KhachHangID = datphong.KhachHangID" + "INNER JOIN Phong phong ON phong.PhongID = datphong.PhongID";
            if (!string.IsNullOrEmpty(hoadon))
            {
                sql = "SELECT datphong.HoaDonThueID, khachhang.KhachHangID, phong.PhongID, datphong.NgayDen, datphong.NgayDi, nhanvien.MaNV, datphong.GhiChu " +
                "FROM DatPhong datphong INNER JOIN NhanVien nhanvien ON datphong.MaNV = nhanvien.MaNV " +
                "INNER JOIN KhachHang khachhang ON khachhang.KhachHangID = datphong.KhachHangID" + "INNER JOIN Phong phong ON phong.PhongID = datphong.PhongID" +
                "WHERE khachhang.KhachHangID = @0" +
                "WHERE nhanvien.Manv = @0" +
                "WHERE phong.PhongID = @0";
                param.Add(hoadon);
            }

            return SelectBySql(sql, param);
        }



        public void AddDatPhong(DatPhongDTO dt)
        {
            try
            {
                string sql = @"INSERT INTO DatPhong (HoaDonThueID, KhachHangID, PhongID, MaNV, NgayDen, NgayDi, GhiChu) 
                   VALUES (@0, @1, @2, @3, @4)";
                List<object> thamSo = new List<object>();
                thamSo.Add(dt.HoaDonThueID);
                thamSo.Add(dt.KhachHangID);
                thamSo.Add(dt.PhongID);
                thamSo.Add(dt.MaNV);
                thamSo.Add(dt.NgayDen);
                thamSo.Add(dt.NgayDi);
                thamSo.Add(dt.GhiChu);

                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }
        }




        public void updateDatPhong(DatPhongDTO dt)
        {
            try
            {
                string sql = @"UPDATE DatPhong 
                   SET KhachHangID = @1, PhongID = @2, MaNV = @3, NgayDen = @4, NgayDi = @5, GhiChu = @6
                   WHERE HoaDonThueID = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(dt.HoaDonThueID);
                thamSo.Add(dt.KhachHangID);
                thamSo.Add(dt.PhongID);
                thamSo.Add(dt.MaNV);
                thamSo.Add(dt.NgayDen);
                thamSo.Add(dt.NgayDi);
                thamSo.Add(dt.GhiChu);

                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }

        }






        public void deleteHoaDon(string HoaDon)
        {
            try
            {
                string sql = "DELETE FROM DatPhong WHERE HoaDonThueID = @0";
                List<object> thamSo = new List<object>();
                thamSo.Add(HoaDon);
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }

        }


    }
}
