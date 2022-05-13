namespace Business.Entities.Domain
{
    public class CauHinhHeThongMap : PagesModel
    {
        public long ID { get; set; }
        public string ImgUrl { get; set; }
        public int SoHangHienThi { get; set; }
        public string TieuDeTrang { get; set; }
        public string TuKhoa { get; set; }
        public string MoTaTuKhoa { get; set; }
        public string Logo { get; set; }
        public string ThoiGianTruyCapTu { get; set; }
        public string ThoiGianTruyCapDen { get; set; }
        public long TongSoLuongTruyCap { get; set; }
        public int SoLanDuocDangNhapSai { get; set; }
        public string Nam { get; set; }
    }
    public class CauHinhChungMap : PagesModel
    {
        public long CauHinhID { get; set; }
        public string ThoiGianTruyCapTu { get; set; }
        public string ThoiGianTruyCapDen { get; set; }
        public long TongSoLuongTruyCap { get; set; }
        public long SoLuongDangKy { get; set; }
        public long SoLuongDangKyMoi { get; set; }
    }
    public class CauHinhMailMap
    {
        public string Email { get; set; }
        public string TenHienThi { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string NguoiDung { get; set; }
        public string MatKhau { get; set; }
    }
    public class DonViMap
    {
        public string TenDonVi { get; set; }
        public string TenVietTat { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Copyright { get; set; }
        public string FaceBook { get; set; }
        public string Zalo { get; set; }
        public string Twitter { get; set; }
        public string Youtube { get; set; }
        public string MoTaWebsite { get; set; }
    }
    public class CauHinhHeThongAll
    {
        public CauHinhHeThongMap cauhinhhethong { get; set; }
        public CauHinhMailMap cauhinhmail { get; set; }
        public DonViMap donvi { get; set; }
    }
}
