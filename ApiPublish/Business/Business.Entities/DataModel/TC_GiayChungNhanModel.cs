using Core.Common.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Business.Entities.DataModel
{
    public class TC_GiayChungNhanMapParam : PagesParamModel
    {
        [Required(ErrorMessage = "Bắt buộc nhập {0}")]
        public int LoaiDangKyID { get; set; }
        [Required(ErrorMessage = "Bắt buộc nhập {0}")]
        public string TuNgay { get; set; }
        public string DenNgay { get; set; }
    }
    public class TC_GiayChungNhanMap : PagesModel
    {
        public long QuyenID { get; set; }
        public string SoGiayChungNhan { get; set; }
        public string NgayCap { get; set; }
        public string LoaiDangKy { get; set; }
        public string LoaiHinh { get; set; }
        public string TacPham { get; set; }
        public string StrTacGia { get; set; }       
        public string StrNguoiBieuDien { get; set; }
        public string StrChuSoHuu { get; set; }
        public int LoaiThuTucID { get; set; }
        public int LoaiDangKyID { get; set; }
    }
    public class TC_GiayChungNhanAddParam
    {
        [Required(ErrorMessage = "Bắt buộc nhập")]
        public string SoGiayChungNhan { get; set; }
        [Required(ErrorMessage = "Bắt buộc nhập")]
        public string NgayCap { get; set; }
        public string TacPham { get; set; }
        public string TacGia { get; set; }
        public string NguoiBieuDien { get; set; }
        public string ChuSoHuu { get; set; }
    }

    public class TC_GiayChungNhanAdd
    {
        public long QuyenID { get; set; }
        public string MaBoHoSo { get; set; }
        public string SoBienNhan { get; set; }
        public string SoGiayChungNhan { get; set; }
        public string NgayCap { get; set; }
        public int TrangThaiID { get; set; }
        public string TacPham { get; set; }
        public string StrTacGia { get; set; }
        public string StrNguoiBieuDien { get; set; }
        public string StrChuSoHuu { get; set; }
        public int LoaiDangKyID { get; set; }
        public int VungMienID { get; set; }
        public string TenVungMien { get; set; }
        public int LoaiHinhID { get; set; }
        public string TenLoaiHinh { get; set; }
        public string NDDHoVaTen { get; set; }
        public string NDDSoCMND { get; set; }
        public string NDDSoDienThoai { get; set; }
        public string NDDDiaChi { get; set; }
    }
    public class TC_GiayChungNhanCongBaoParam : PagesModel
    {
        [Required(ErrorMessage = "Bắt buộc nhập")]
        public int LoaiDangKyID { get; set; }
        [Required(ErrorMessage = "Bắt buộc nhập")]
        public string TuNgay { get; set; }
        public string DenNgay { get; set; }
    }
    public class TC_GiayChungNhanCongBaoMap : EntityBase
    {
        public long QuyenID { get; set; }
        public string SoGiayChungNhan { get; set; }
        public DateTime NgayCap { get; set; }
        public string LoaiDangKy { get; set; }
        public string LoaiHinh { get; set; }
        public string TacPham { get; set; }
        public string StrTacGia { get; set; }
        public string StrNguoiBieuDien { get; set; }
        public string StrChuSoHuu { get; set; }
        public int LoaiThuTucID { get; set; }
        public int LoaiDangKyID { get; set; }
    }
    public class TC_ThongTinSoHuu
    {
        public string StrTacGia { get; set; }
        public string StrNguoiBieuDien { get; set; }
        public string StrChuSoHuu { get; set; }
    }
}
