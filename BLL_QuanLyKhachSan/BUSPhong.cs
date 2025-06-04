using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QuanLyKhachSan;
using DTO_QuanLyKhachSan;

namespace BLL_QuanLyKhachSan
{
    public class BUSPhong
    {
        DalPhong dal = new DalPhong();
        public List<Phong> GetListPhong(string phong)
        {
            return dal.selectAll(phong);
        }



        public string AddPhong(Phong dto)
        {
            try
            {
                dto.PhongID = dal.autothemphong();
                if (string.IsNullOrEmpty(dto.PhongID))
                {
                    return "Mã thẻ sai";
                }
                dal.AddPhong(dto);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }




        public string UpdatePhong(Phong dto)
        {
            try
            {
                dal.updatephong(dto);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }



        public string DeletePhong(string phong)
        {
            try
            {
                if (string.IsNullOrEmpty(phong))
                {
                    return "Mã phiếu không được để trống";
                }
                dal.deletePhong(phong);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }


    }
}
