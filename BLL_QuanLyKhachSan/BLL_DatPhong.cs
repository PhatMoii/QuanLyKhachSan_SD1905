using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QuanLyKhachSan;
using DAL_QuanLyKhachSan;

namespace BLL_QuanLyKhachSan
{
    public class BLL_DatPhong
    {
        DALDatPhongcs dal = new DALDatPhongcs();
        public List<DatPhongDTO> GetDatPhongDTOs(string maThe)
        {
            return dal.selectAll(maThe);
        }


        public string AddPhieuBanHang(DatPhongDTO dto)
        {
            try
            {
                dto.HoaDonThueID = dal.autoHoaDonThueID();
                if (string.IsNullOrEmpty(dto.HoaDonThueID))
                {
                    return "Mã thẻ sai";
                }
                dal.AddDatPhong(dto);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }




        public string UpdatePhieuBanHang(DatPhongDTO dto)
        {
            try
            {
                if (string.IsNullOrEmpty(dto.HoaDonThueID))
                {
                    return " Hoa Don không được để trống";
                }
                dal.updateDatPhong(dto);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }




        public string DeletePhieuBanHang(string HoaDon)
        {
            try
            {
                if (string.IsNullOrEmpty(HoaDon))
                {
                    return "Mã phiếu không được để trống";
                }
                dal.deleteHoaDon(HoaDon);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }



    }
}
