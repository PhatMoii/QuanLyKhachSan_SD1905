using DAL_QuanLyKhachSan;
using DTO_QuanLyKhachSan;

namespace BLL_QuanLyKhachSan
{
    public class BUSLoaiDichVu
    {
        DALLoaiDichVu dalLDV = new DALLoaiDichVu();

        public List<LoaiDichVu> GetLoaiDichVuList()
        {
            return dalLDV.SelectAll();
        }

        public LoaiDichVu? GetLoaiDichVuByID(string loaiDichVuID)
        {
            if (string.IsNullOrEmpty(loaiDichVuID))
            {
                return null;
            }

            var list = dalLDV.SelectBySql("SELECT * FROM LoaiDichVu WHERE LoaiDichVuID = @0", new List<object> { loaiDichVuID });
            return list.Count > 0 ? list[0] : null;
        }

        public string InsertLoaiDichVu(LoaiDichVu ldv)
        {
            try
            {
                ldv.LoaiDichVuID = dalLDV.GenerateNewID();

                if (string.IsNullOrEmpty(ldv.TenDichVu) || string.IsNullOrEmpty(ldv.DonViTinh))
                {
                    return "Vui lòng nhập đầy đủ thông tin dịch vụ.";
                }

                ldv.NgayTao = DateTime.Now;
                dalLDV.Insert(ldv);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi thêm loại dịch vụ: " + ex.Message;
            }
        }

        public string UpdateLoaiDichVu(LoaiDichVu ldv)
        {
            try
            {
                if (string.IsNullOrEmpty(ldv.LoaiDichVuID))
                {
                    return "Vui lòng chọn dịch vụ để cập nhật.";
                }

                dalLDV.Update(ldv);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi cập nhật loại dịch vụ: " + ex.Message;
            }
        }

        public void DeleteLoaiDichVu(string loaiDichVuID)
        {
            try
            {
                dalLDV.Delete(loaiDichVuID);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi xóa loại dịch vụ", ex);
            }
        }

        public string GenerateMaLoaiDichVu()
        {
            return dalLDV.GenerateNewID();
        }

        public bool CapNhatTrangThai(string id, bool trangThai)
        {
            try
            {
                return dalLDV.UpdateTrangThai(id, trangThai);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật trạng thái dịch vụ", ex);
            }
        }
    }
}
