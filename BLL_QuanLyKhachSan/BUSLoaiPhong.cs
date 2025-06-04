using DAL_QuanLyKhachSan;
using DTO_QuanLyKhachSan;

namespace BLL_QuanLyKhachSan
{
    public class BUSLoaiPhong
    {
        DALLoaiPhong dallp = new DALLoaiPhong();

        public List<LoaiPhong> GetLoaiPhongList()
        {
            return dallp.SelectAll();
        }

        public LoaiPhong? GetLoaiPhongByMa(string maLoaiPhong)
        {
            if (string.IsNullOrEmpty(maLoaiPhong))
            {
                return null;
            }
            return dallp.GetLoaiPhong(maLoaiPhong);
        }

        public string InsertLoaiPhong(LoaiPhong lp)
        {
            try
            {
                lp.MaLoaiPhong = dallp.GenerateMaLoaiPhong();
                if (string.IsNullOrEmpty(lp.TenLoaiPhong))
                {
                    return "Vui lòng nhập đầy đủ thông tin loại phòng.";
                }
                dallp.InsertLoaiPhong(lp);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi thêm loại phòng: " + ex.Message;
            }
        }

        public string UpdateLoaiPhong(LoaiPhong lp)
        {
            try
            {
                if (string.IsNullOrEmpty(lp.MaLoaiPhong))
                {
                    return "Vui lòng chọn loại phòng để cập nhật.";
                }
                dallp.UpdateLoaiPhong(lp);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi cập nhật loại phòng: " + ex.Message;
            }
        }

        public void DeleteLoaiPhong(string maLoaiPhong)
        {
            try
            {
                dallp.DeleteLoaiPhong(maLoaiPhong);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi xóa loại phòng", ex);
            }
        }

        public string GenerateMaLoaiPhong()
        {
            return dallp.GenerateMaLoaiPhong();
        }
    }
}
