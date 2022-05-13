using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Business.Entities.Domain
{
    public class NguoiDungHeThongMap : PagesModel
    {
        public int STT { get; set; }
        public long ID { get; set; }
        public Guid TaiKhoanGuid { get; set; }
        public string TenHienThi { get; set; }
        public string TenTaiKhoan { get; set; }
        public string DSNhomQuyen { get; set; }
        public string NgaySinh { get; set; }
        public string Email { get; set; }
        public string DienThoai { get; set; }
        public bool Khoa { get; set; }
    }
    public class NguoiDungHeThongMapParam : PagesParamModel
    {
        public string HoVaTen { get; set; }
        public string Email { get; set; }
        public string DienThoai { get; set; }
        public long NhomQuyen { get; set; }
    }
    public class NguoiDungHeThongMapAdd
    {
        public long ID { get; set; }
        public Guid TaiKhoanGuid { get; set; }
        public string TenTaiKhoan { get; set; }
        public string NgaySinh { get; set; }
        public string HoVaTen { get; set; }
        public int GioiTinh { get; set; }
        public string CMND { get; set; }
        public string Avatar { get; set; }
        public string DiaChi { get; set; }
        public string MatKhau { get; set; }
        public string NhapLaimatKhau { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
        public string DienThoai { get; set; }
        public bool Khoa { get; set; }
        public string TenHienThi { get; set; }
        public string DSNhomQuyen { get; set; }
        public string NgayCapCMND { get; set; }
        public string NoiCapCMND { get; set; }
        public long MCUserID { get; set; }
        public long MCPhongBanID { get; set; }
        public string MCTenPhongBan { get; set; }
        public long MCDonViID { get; set; }
        public string MCTenDonVi { get; set; }
        public int MCChucVuID { get; set; }
        public string MCTenChucVu { get; set; }
        public Guid CreatedUserID { get; set; }
        public Guid LastUpdUserID { get; set; }
        public string Token { get; set; }
        public string MatKhauCu { get; set; }
    }
    public class NguoiDungHeThongChagePass
    {
        public long ID { get; set; }
        public Guid TaiKhoanGuid { get; set; }
        public string MatKhau { get; set; }
        public string NhapLaimatKhau { get; set; }
        public string Salt { get; set; }
        public Guid LastUpdUserID { get; set; }
        public string MatKhauCu { get; set; }
    }
    public class NguoiDungHeThong_NhomQuyenMap
    {
        public Guid NguoiDungPQId { get; set; }
        public string TenHienThi { get; set; }
        public string TenTaiKhoan { get; set; }
        public List<DM_NhomQuyenAdd> danhsachquyen { get; set; }

    }
    public class NguoiDungHeThong_NhomQuyenMapAdd
    {
        public string NhomQuyenIDs { get; set; }
        public Guid NguoiDungID { get; set; }
    }
    public class LoginParam
    {
        [Required(ErrorMessage = "Bắt buộc nhập {0}")]
        [StringLength(100,ErrorMessage = "{0} không vượt quá 100 ký tự")]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bắt buộc nhập {0}")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
    }
    public class BanIPMap
    {
        public long Id { get; set; }
        public string IPAdress { get; set; }
        public int Status { get; set; }
        public DateTime? DateEnd { get; set; }
        public int? WrongNum { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Modified_Date { get; set; }
    }
    public class TokenObject
    {
        public string Token { get; set; }
    }
    public class NguoiDungHeThong1CuaMapAdd
    {
        public string UserName { get; set; }
        public long? UserID { get; set; }
        public string UserHoTen { get; set; }
        public long? UserPhongBanID { get; set; }
        public string UserTenPhongBan { get; set; }
        public long? UserDonViID { get; set; }
        public string UserTenDonVi { get; set; }
        public int? UserTinhThanhID { get; set; }
        public int? UserQuanHuyenID { get; set; }
        public long? UserConNguoiID { get; set; }
        public string UserAvatar { get; set; }
        public string NgaySinh { get; set; }
        public string ThangSinh { get; set; }
        public string NamSinh { get; set; }
        public string SoDT { get; set; }
        public string ChoOhienTai { get; set; }
        public string Email { get; set; }
        public int? ChucVuID { get; set; }
        public string TenChucVu { get; set; }
        public int? VanThu { get; set; }
    }
}
