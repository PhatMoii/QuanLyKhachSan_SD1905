using DAL_QuanLyKhachSan;
using DTO_QuanLyKhachSan;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_QuanLyKhachSan
{
    public class BLL_NhanVien
    {
        NhanVienDAL nhanvienDAL = new NhanVienDAL();


        



        public NhanVienDTO? DangNhap(string username, string matkhau)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(matkhau))
                return null;

            return nhanvienDAL.getNhanVien1(username, matkhau);

        }



        public string XoaNhanVien(string maNhanVien)
        {
            try
            {
                if (string.IsNullOrEmpty(maNhanVien))
                {
                    return "Mã nhân viên không được để trống";
                }
                nhanvienDAL.XoaNhanVien(maNhanVien);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "loi:" + ex.Message;
            }
        }



        public string ThemNhanVien(NhanVienDTO nv)
        {
            try
            {
                nv.MaNV = nhanvienDAL.generateMaNV();

                if (string.IsNullOrEmpty(nv.MaNV))
                {
                    return "Mã nhân viên không được để trống";
                }
                nhanvienDAL.ThemNhanVien(nv);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "loi:" + ex.Message;
            }
        }



        public string CapNhatNhanVien(NhanVienDTO nv)
        {
            try
            {
                if (string.IsNullOrEmpty(nv.MaNV))
                {
                    return "Mã nhân viên không được để trống";
                }
                nhanvienDAL.UpdateNhanVien(nv);
                return string.Empty;

            }
            catch (Exception ex)
            {
                return "loi:" + ex.Message;
            }

        }






        public List<NhanVienDTO> GetNhanVIenList()
        {
            return nhanvienDAL.selectAll();
        }

    }
}
