using System;
using System.Collections.Generic;

namespace Business.Entities.Domain
{
    public class QTG_QuyenTacGiaMap : PagesModel
    {
        public string STT { get; set; }
        public long QuyenTacGiaID { get; set; }
        public string SoGCN { get; set; }
        public string NgayCap { get; set; }
        public string TenTacPham { get; set; }
        public string HinhDaiDienUrl { get; set; }
        public int TrangThaiID { get; set; }
        public string TenTrangThai { get; set; }
        public string TenVungMien { get; set; }
        public string CreatedUser { get; set; }
        public bool IsDauKy { get; set; }
        public int LoaiNghiepVuID { get; set; }
        public long KeyMapID { get; set; }
        public long HoSoID { get; set; }
        public List<QTG_TacGiaAdd> ListTacGia { get; set; }
        public List<QTG_ChuSoHuuAdd> ListChuSoHuu { get; set; }
    }
    public class QTG_QuyenTacGiaParam : PagesParamModel
    {
        public string TuKhoa { get; set; }
        public string SoGCN { get; set; }
        public string NgayCapTu { get; set; }
        public string NgayCapDen { get; set; }
        public string TenTacGia { get; set; }
        public string TenChuSoHuu { get; set; }
        public string CreatedUser { get; set; }
        public int VungMienID { get; set; }
        public int TrangThaiID { get; set; }
    }
    public class QTG_QuyenTacGiaAdd
    {
        public long QuyenTacGiaID { get; set; }
        public string STT { get; set; }
        public string SoGCN { get; set; }
        public int VungMienID { get; set; }
        public string NgayCap { get; set; }
        public bool CoNguoiDaiDien { get; set; }
        public string NDDHoVaTen { get; set; }
        public string NDDSoCMND { get; set; }
        public string NDDSoDienThoai { get; set; }
        public string NDDDiaChi { get; set; }
        public bool IsToChuc { get; set; }
        public int LoaiDangKyID { get; set; }
        public string NgayHT { get; set; }
        public string TenTacPham { get; set; }
        public long PhimID { get; set; }
        public int PhimStt { get; set; }
        public string TenPhim { get; set; }
        public int TrangThaiCongBoID { get; set; }
        public string NgayCongBo { get; set; }
        public int LoaiHinhID { get; set; }
        public string NoiCongBo { get; set; }
        public long HinhDaiDienID { get; set; }
        public int HinhDaiDienStt { get; set; }
        public string HinhDaiDienUrl { get; set; }
        public int LoaiNghiepVuID { get; set; }
        public string TenLoaiNghiepVu { get; set; }
        public bool IsDauKy { get; set; }
        public int TrangThaiID { get; set; }
        public string TenTrangThai { get; set; }
        public long KeyMapID { get; set; }
        public int SoLanCapLai { get; set; }
        public int SoLanThuHoi { get; set; }
        public int SoLanCapDoi { get; set; }
        public int SoLanDoiChu { get; set; }
        public int NguoiKyID { get; set; }
        public string NgayKy { get; set; }
        public long StaticID { get; set; }
        public long DVCID { get; set; }
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
        public List<QTG_ChuSoHuuAdd> ListChuSoHuu { get; set; }
        public List<QTG_DinhKemAdd> ListDinhKem { get; set; }
        public List<QTG_LichSuMap> ListLichSu { get; set; }

    }
    public class QTG_QuyenTacGiaChange
    {
        public long QuyenTacGiaID { get; set; }
        public string STT { get; set; }
        public string SoGCN { get; set; }
        public string NgayCap { get; set; }
        public int TrangThaiID { get; set; }
        public string TenTrangThai { get; set; }
        public int LoaiNghiepVuID { get; set; }
        public long KeyMapID { get; set; }
        public int SoLanCapLai { get; set; }
        public int SoLanThuHoi { get; set; }
        public int SoLanCapDoi { get; set; }
        public int SoLanDoiChu { get; set; }
        public Guid UserID { get; set; }
    }
    public class QTG_ChuSoHuuAdd
    {
        public long ChuSoHuuID { get; set; }
        public string HoVaTen { get; set; }
        public string QuocTich { get; set; }
        public string SoCMND { get; set; }
        public string SoDKKD { get; set; }
        public string DiaChi { get; set; }
        public Guid UserID { get; set; }
    }
    public class QTG_TacGiaAdd
    {
        public long TacGiaID { get; set; }
        public string HoVaTen { get; set; }
        public string QuocTich { get; set; }
        public string SoCMND { get; set; }
        public string DiaChi { get; set; }
        public Guid UserID { get; set; }
    }
    public class QTG_DinhKemAdd
    {
        public long DinhKemID { get; set; }
        public int LoaiXuLyID { get; set; }
        public string Ten { get; set; }
        public string Tag { get; set; }
        public string TenFile { get; set; } 
        public string DuongDan { get; set; }
        public string GhiChu { get; set; }
        public string CreatedUser { get; set; }
        public string CreatedDate { get; set; }
        public Guid UserID { get; set; }
        public bool IsMotCua { get; set; }
    }
    public class QTG_GiayChungNhanMap : PagesModel
    {
        public string STT { get; set; }
        public long QuyenTacGiaID { get; set; }
        public string SoGCN { get; set; }
        public string NgayCap { get; set; }
        public string TenTacPham { get; set; }
        public string TenVungMien { get; set; }
        public string CreatedUser { get; set; }
    }
    public class QTG_GiayChungNhanParam : PagesParamModel
    {
        public string TuKhoa { get; set; }
    }
    public class QTG_SoThuTuMap : PagesModel
    {
        public string STT { get; set; }
        public long QuyenTacGiaID { get; set; }
        public string TenTacPham { get; set; }
        public string TenVungMien { get; set; }
        public string CreatedUser { get; set; }
    }
    public class QTG_SoThuTuParam : PagesParamModel
    {
        public string TuKhoa { get; set; }
    }
    public class QTG_QuyenTacGia_CapSo
    {
        public long ID { get; set; }
        public int LoaiNghiepVuID { get; set; }
        public string STT { get; set; }
        public string SoGCN { get; set; }
        public Guid UserID { get; set; }
    }
}
