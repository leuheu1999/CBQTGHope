using System;

namespace Business.Entities.Domain
{
    public class TT_CapQuyenMap : PagesModel
    {
        public long CapQuyenID { get; set; }
        public long HoSoID { get; set; }
        public string SoBienNhan { get; set; }
        public string NgayNhan { get; set; }
        public string NgayHenTra { get; set; }
        public string HoVaTen { get; set; }
        public string TenPhuongXa { get; set; }
        public string ThuTucID { get; set; }
        public string TenThuTuc { get; set; }
    }
    public class TT_CapQuyenAdd
    {
        public long CapQuyenID { get; set; }
        public long HoSoID { get; set; }
        public long ThuTucID { get; set; }
        public string TenThuTuc { get; set; }
        public string MaBoHoSo { get; set; }
        public string SoBienNhan { get; set; }
        public int LoaiNghiepVuID { get; set; }
        public string TenLoaiNghiepVu { get; set; }
        public long KeyMapID { get; set; }
        public int LinhVucID { get; set; }
        public string TenLinhVuc { get; set; }
        public long NguoiKyID { get; set; }
        public string HoVaTen { get; set; }
        public long TinhThanhID { get; set; }
        public string TenTinhThanh { get; set; }
        public long QuanHuyenID { get; set; }
        public string TenQuanHuyen { get; set; }
        public long PhuongXaID { get; set; }
        public string TenPhuongXa { get; set; }
        public string DiaChi { get; set; }
        public string NgayNhan { get; set; }
        public string NgayHenTra { get; set; }
        public int SoNgayGiaiQuyet { get; set; }
        public Guid UserID { get; set; }
    }
    public class TT_CapQuyenParam : PagesParamModel
    {
        public string SoBienNhan { get; set; }
        public string MaBoHoSo { get; set; }
        public string CongDanToChuc { get; set; }
        public string TuNgay { get; set; }
        public string DenNgay { get; set; }
        public int TinhThanhID { get; set; }
        public int QuanHuyenID { get; set; }
        public int PhuongXaID { get; set; }
        public int LinhVucID { get; set; }
        public int ThuTucID { get; set; }
    }

    public class TT_CapQuyenAddTrangThai
    {
        public long CapQuyenID { get; set; }
        public int TrangThaiDuyet { get; set; }
        public Guid CreatedUserID { get; set; }
        public string SoBienNhan { get; set; }
    }

    public class TT_CapQuyenAddDuyet
    {
        public int LoaiNghiepVuID { get; set; }
        public int KeyMapID { get; set; }
        public Guid CreatedUserID { get; set; }
    }
}
