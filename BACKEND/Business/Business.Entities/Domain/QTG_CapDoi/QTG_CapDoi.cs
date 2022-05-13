using System;
using System.Collections.Generic;

namespace Business.Entities.Domain
{
    public class QTG_CapDoiAdd
    {
        public long QuyenTacGiaID { get; set; }
        public long QuyenTacGiaCuID { get; set; }
        public string STT { get; set; }
        public string SoGCN { get; set; }
        public string NgayCapGCN { get; set; }
        public string STTCu { get; set; }
        public string SoGCNCu { get; set; }
        public string NgayCapGCNCu { get; set; }
        public int VungMienID { get; set; }
        public bool CoNguoiDaiDien { get; set; }
        public string NDDHoVaTen { get; set; }
        public string NDDSoCMND { get; set; }
        public string NDDSoDienThoai { get; set; }
        public string NDDDiaChi { get; set; }
        public bool IsToChuc { get; set; }
        public int LoaiDangKyID { get; set; }
        public string TenTacPham { get; set; }
        public long PhimID { get; set; }
        public string TenPhim { get; set; }
        public int TrangThaiCongBoID { get; set; }
        public string NgayCongBo { get; set; }
        public int LoaiHinhID { get; set; }
        public string NoiCongBo { get; set; }
        public long HinhDaiDienID { get; set; }
        public string HinhDaiDienUrl { get; set; }
        public int SoLanCapDoi { get; set; }

        //Thông tin cấp đổi
        public long CapDoiID { get; set; }
        public string ThongTinCapDoi { get; set; }
        public string SoQD { get; set; }
        public string NgayQD { get; set; }
        public string LyDoCapDoi { get; set; }
        public string ThongTinTacPham { get; set; }
        public int NguoiKyID { get; set; }
        public string NgayKy { get; set; }
        public long StaticID { get; set; }
        public Guid UserID { get; set; }
        //Thông tin hồ sơ
        public long HoSoID { get; set; }
        public long ThuTucID { get; set; }
        public string TenThuTuc { get; set; }
        public string MaBoHoSo { get; set; }
        public string SoBienNhan { get; set; }
        public List<TT_HinhAnhAdd> ListHinhAnh { get; set; }
        public List<TT_PhimAdd> ListPhim { get; set; }
        public List<QTG_TacGiaAdd> ListTacGia { get; set; }
        public List<QTG_DinhKemAdd> ListDinhKem { get; set; }
    }
}
