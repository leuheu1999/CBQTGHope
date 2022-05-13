using System;

namespace Business.Entities.Domain
{
    public class DM_QuyenChucNangMap: PagesModel
    {
        public long Id { get; set; }
        public long QuyenID { get; set; }
        public string TenQuyen { get; set; }
        public string CodeCookie { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }
        public string TenHienThi { get; set; }
        public string MoTa { get; set; }
        public int ThuTuHienThi { get; set; }
        public bool CongKhai { get; set; }
    }
    public class DM_QuyenChucNangMapParam : PagesParamModel
    {
        public string TuKhoa { get; set; }
        public long QuyenID { get; set; }
    }
    public class DM_QuyenChucNangAdd
    {
        public long Id { get; set; }
        public long QuyenID { get; set; }
        public string CodeCookie { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }
        public string TenHienThi { get; set; }
        public string MoTa { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public bool IsDefault { get; set; }
        public int ThuTuHienThi { get; set; }
        public bool CongKhai { get; set; }
        public Guid CreatedUserID { get; set; }
        public string DefaultController { get; set; }
    }
}
