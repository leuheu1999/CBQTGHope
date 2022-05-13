using System;
using System.Collections.Generic;

namespace Business.Entities.Domain
{
    public class HoSo_1CuaMap
    {
        public int TotalRecords { get; set; }
        public int HoSoID { get; set; }
        public int HoSoOnlineID { get; set; }
        public string SoBienNhan { get; set; }
        public string NguoiDungTenHoSo { get; set; }
        public string TenThuTuc { get; set; }

        public string NgayNhan { get; set; }
        public string NgayHenTra { get; set; }
        public string ThoiGian { get; set; }
        public string NoiDungXuLy { get; set; }
        public string TrangThaiHoSo { get; set; }
        public int CoTheChuyen { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int LinhVucID { get; set; }
        public int ThuTucHanhChinhID { get; set; }
        public string MaQuyTrinh { get; set; }

        public string TenLinhVuc { get; set; }
        public int LoaiXuLy { get; set; }
        public int QuaTrinhXuLyHienTaiID { get; set; }
        public int PhongBanXuLyID { get; set; }
        public string ChucNangHienTai { get; set; }
        public string DuongDiHoSo { get; set; }
        public int XuLyLai { get; set; }
        public int NguoiXuLyID { get; set; }
        public int DonViXuLyID { get; set; }
        public int DonViNhanID { get; set; }
        public string TenTacPham { get; set; }
        public int SoGioConLai { get; set; }
        public bool HoanTat { get; set; }
        public bool DaNhan { get; set; }

    }
    public class NguoiNhanHoSo_1CuaMap
    {
        public string STT { get; set; }
        public string ParentID { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }
    }
    public class DSHoSo_1CuaMap
    {
        public List<HoSo_1CuaMap> ListHoSoThuLy { get; set; }
        public List<NguoiNhanHoSo_1CuaMap> ListNguoiNhan { get; set; }
    }
    public class HoSoParam : PagesParamModel
    {
       
        public int? TinhThanhID { get; set; }
        public int? QuanHuyenID { get; set; }
        public int? PhuongXaID { get; set; }
        public string TenBuocKeTiep { get; set; }

        public int? LinhVucID { get; set; }
        public int? ThuTucID { get; set; }
        public string CongDanToChuc { get; set; }
        public string NguoiNhanIDs { get; set; }

        public string SoBienNhan { get; set; }
        public int? TinhTrangPhieuTrinhID { get; set; }
        public int? TinhTrangID { get; set; }

        public string MaBoHoSo { get; set; }
        public string TuNgay { get; set; }
        public string DenNgay { get; set; }
        public bool HoSoDangTheoDoi { get; set; }
        public bool HoSoSapDenHan { get; set; }
        public bool HoSoQuaHan { get; set; }
        public string DuongDiNguoiNhan { get; set; }
        public string TenTacPham { get; set; }
    }
    public class HoSo_1CuaParam : PagesParamModel
    {

        public string NguoiDungTenHoSo { get; set; }
        public string LinhVucID { get; set; }
        public string ThuTucHanhChinhID { get; set; }

        public string TuNgay { get; set; }
        public string DenNgay { get; set; }
        public string SoBienNhan { get; set; }

        public string NguoiNhan { get; set; }
        public int DonViID { get; set; }
        public int PhongBanID { get; set; }

        public string NguoiNhanHoSo { get; set; }

        public string ChucNangKeTiepNguoiNhan { get; set; }
        public string LoaiXuLyNguoiNhan { get; set; }
        public string TinhTrangXLHS { get; set; }
        public string TinhTrang { get; set; }

        public string TinhThanhID { get; set; }
        public string HuyenThiThanhPhoID { get; set; }
        public string PhuongXaID { get; set; }
        public string SoBoHoSo { get; set; }

        public string ChucNangKeTiep { get; set; }
        public int Follow { get; set; }
        public string TinhTrangPT { get; set; }
        public string ChucNangHienTai { get; set; }

        public string LoaiXuLy { get; set; }

        public string TenTacPham { get; set; }
    }
    public class ChuyenHoSo_1CuaAdd
    {

        public int NguoiXuLyID { get; set; }
        public int UserID { get; set; }
        public int PhongBanID { get; set; }

        public int DonViID { get; set; }
        public string UserName { get; set; }
        public int PhongBanXuLyID { get; set; }

        public int DonViXuLyID { get; set; }
        public int HoSoID { get; set; }
        public int HoSoOnlineID { get; set; }
        public int ThuTucHanhChinhID { get; set; }

        public int LinhVucID { get; set; }
        public int QuaTrinhXuLyHienTaiID { get; set; }
        public string DuongDiHoSo { get; set; }
        public string DuongDiNguoiNhan { get; set; }
        public string SoBienNhan { get; set; }
        public int LoaiXuLy { get; set; }
        public int DonViNhanID { get; set; }
        public int XuLyLai { get; set; }
    }
    public class ChuyenNhanHoSo_1CuaReturn
    {
        public bool IsSuccessed { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class NhanHoSo_1CuaAdd
    {

        public string NguoiXuLyID { get; set; }
        public int HoSoID { get; set; }
        public int ThuTucHanhChinhID { get; set; }

        public int LinhVucID { get; set; }
        public int PhongBanXuLyID { get; set; }
        public int DonViXuLyID { get; set; }
        public string ChucNangHienTai { get; set; }
        public int QuaTrinhXuLyID { get; set; }

        public string SoBienNhan { get; set; }
    }
    public class ChiTietHoSo_1CuaMap
    {
        public int HoSoID { get; set; }
        public int HoSoOnlineID { get; set; }
        public string SoBienNhan { get; set; }
        public string MaTraCuu { get; set; }
        public int ThuTucHanhChinhID { get; set; }
        public string TenThuTucHanhChinh { get; set; }
        public int LinhVucID { get; set; }
        public string TenLinhVuc { get; set; }
        public int DonViNhanID { get; set; }
        public string TenDonVi { get; set; }
        public DateTime NgayNhan { get; set; }
        public DateTime NgayHopLe { get; set; }
        public DateTime NgayHenTra { get; set; }
        public string NgayPhatHanh { get; set; }
        public string NgayThucTra { get; set; }
        public string NgayHoanTatHoSo { get; set; }
        public string NoiDungKhac { get; set; }
        public string TrangThaiHoSo { get; set; }
        public string ChucNangHienTai { get; set; }
        public string TenChucNangHienTai { get; set; }
        public int TinhTrangQuaHanXuLy { get; set; }
        public int TinhTrangID { get; set; }
        public string TenTinhTrang { get; set; }
        public bool DanhDauSao { get; set; }
        public DateTime LastUpdDate { get; set; }
        public string LastUpdDate_INT { get; set; }
        public string DangKy_DiaChiDangKy { get; set; }
        public string DangKy_SoNha { get; set; }
        public int? DangKy_DuongID { get; set; }
        public string DangKy_TenDuong { get; set; }
        public int? DangKy_PhuongID { get; set; }
        public string DangKy_MaPhuongXa { get; set; }
        public string DangKy_TenPhuongXa { get; set; }
        public int? DangKy_QuanID { get; set; }
        public string DangKy_MaQuanHuyen { get; set; }
        public string DangKy_TenQuanHuyen { get; set; }
        public string TrangThaiQuaTrinh { get; set; }
        public int QuaTrinhXuLyID { get; set; }
        public string MaQuyTrinh { get; set; }
        public List<NguoiDungTen_1CuaMap> DSNguoiDungTenDoanhNghiep { get; set; }
        public List<NguoiDungTen_1CuaMap> DSNguoiDungTenCaNhan { get; set; }
        public string LH_HoTen { get; set; }
        public string LH_GiayUyQuyen { get; set; }
        public string LH_ThongTinLienLac { get; set; }
        public string LH_TBEmail { get; set; }
        public string LH_TBDienThoai { get; set; }
        public string LH_KQNhanBuuDien { get; set; }
        public List<HoSoKemTheo_1CuaMap> DSHoSoKemTheo { get; set; }
    }
    public class NguoiDungTen_1CuaMap
    {
        public string DungTen_HoVaTen { get; set; }
        public string DungTen_GioiTinh { get; set; }
        public string DungTen_NgaySinh { get; set; }
        public string DungTen_ThangSinh { get; set; }
        public string DungTen_NamSinh { get; set; }
        public int DungTen_LoaiGiayToID { get; set; }
        public string DungTen_SoGiayToTuyThan { get; set; }
        public string DungTen_NgayCap { get; set; }
        public string DungTen_NoiCap { get; set; }
        public string DungTen_DienThoai { get; set; }
        public string DungTen_Email { get; set; }
        public string DungTen_DiaChiThuongTru { get; set; }
    }
    public class HoSoKemTheo_1CuaMap
    {
        public long HoSoKemTheoID {get;set;}
        public string TenHoSoKemTheo {get;set;}
        public int SoBanChinh {get;set;}
        public int SoBanSao {get;set;}
        public int SoBanPhoto {get;set;}
        public string NguoiDinhKem {get;set;}
        public bool IsMotCua {get;set;}
        public string GhiChu {get;set;}
        public string OriginalName {get;set;}
        public string UploadName {get;set;}
        public string Action {get;set;}
        public string FileSize {get;set;}
    }
    public class TepGiayTo_DVC
    {
        public int id { get; set; }
        public int hoSoGiayToId { get; set; }
        public string tenTepTin { get; set; }
        public string fileId { get; set; }
    }
    public class ThongTinDangKy_DVC
    {
        public string label { get; set; }
        public string name { get; set; }
        public string value { get; set; }
    }
    public class HoSoGiayTo_DVC
    {
        public int id { get; set; }
        public int hoSoId { get; set; }
        public string tenGiayTo { get; set; }
        public int? soBanSao { get; set; }
        public int? soBanChinh { get; set; }
        public int? soBanPhoto { get; set; }
        public string ghiChu { get; set; }
        public bool? isBieuMau { get; set; }
        public List<TepGiayTo_DVC> tepGiayTos { get; set; }
    }
    public class ChiTietHoSo_DVC
    {
        public int id { get; set; }
        public int? toChucHayCaNhan { get; set; }
        public string tenThuTucHanhChinh { get; set; }
        public string tenDonVi { get; set; }
        public string tenLinhVuc { get; set; }
        public int? thuTucHanhChinhLienThongId { get; set; }
        public int? donViLienThongId { get; set; }
        public int? linhVucLienThongId { get; set; }
        public DateTime ngayNopHoSo { get; set; }
        public string maTraCuu { get; set; }
        public int? loaiGiayTo { get; set; }
        public string soGiayTo { get; set; }
        public DateTime ngayCap { get; set; }
        public string hoTenNguoiNop { get; set; }
        public string sdtNguoiNop { get; set; }
        public string emailNguoiNop { get; set; }
        public string soNha { get; set; }
        public string tenNguoiLienHe { get; set; }
        public string emailLienHe { get; set; }
        public string sdtLienHe { get; set; }
        public string soNhaLienHe { get; set; }
        public string diaDiemNopHoSo { get; set; }
        public string diaDiemNhanKetQua { get; set; }
        public string mucDo { get; set; }
        public string eform { get; set; }
        public List<HoSoGiayTo_DVC> hoSoGiayTos { get; set; }
        public List<ThongTinDangKy_DVC> thongTinDangKy { get; set; }
    }
}
