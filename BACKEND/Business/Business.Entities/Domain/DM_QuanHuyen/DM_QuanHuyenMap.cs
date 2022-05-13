using System;

namespace Business.Entities.Domain
{
    public class DM_QuanHuyenMap : PagesModel
    {
        public long Id { get; set; }
        public long TinhThanhID { get; set; }
        public string TenTinhThanh { get; set; }
        public string Ma { get; set; }
        public string MaLienThong { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }
        public int ThuTuHienThi { get; set; }
        public bool CongKhai { get; set; }

    }
    public class DM_QuanHuyenMapParam : PagesParamModel
    {
        public string TuKhoa { get; set; }
        public long TinhThanhID { get; set; }
    }
    public class DM_QuanHuyenMapAdd
    {
        public long Id { get; set; }
        public long TinhThanhID { get; set; }
        public string TenTinhThanh { get; set; }
        public string Ma { get; set; }
        public string MaLienThong { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }
        public int ThuTuHienThi { get; set; }
        public bool CongKhai { get; set; }
        public Guid CreatedUserID { get; set; }
    }
}
