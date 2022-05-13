using System;

namespace Business.Entities.Domain
{
    public class DM_PhuongXaMap : PagesModel
    {
        public int STT { get; set; }
        public long ID { get; set; }
        public long QuanHuyenID { get; set; }
        public string TenQuanHuyen { get; set; }
        public string TenTinhThanh { get; set; }
        public string Ma { get; set; }
        public string MaLienThong { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }
        public string ThuTuHienThi { get; set; }
        public bool CongKhai { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
    }
    public class DM_PhuongXaMapParam : PagesParamModel
    {
        public string TuKhoa { get; set; }
        public long QuanHuyenID { get; set; }
        public long TinhThanhID { get; set; }
    }
    public class DM_PhuongXaMapAdd
    {
        public long ID { get; set; }
        public long QuanHuyenID { get; set; }
        public long TinhThanhID { get; set; }
        public string Ma { get; set; }
        public string MaLienThong { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }
        public bool CongKhai { get; set; }
        public int ThuTuHienThi { get; set; }
        public Guid CreatedUserID { get; set; }
        public Guid LastUpdUserID { get; set; }
    }
}
