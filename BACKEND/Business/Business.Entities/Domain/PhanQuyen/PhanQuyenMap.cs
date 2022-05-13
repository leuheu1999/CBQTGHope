using System;
using System.Collections.Generic;

namespace Business.Entities.Domain
{
    public class SYS_PhanQuyenMap
    {
        public List<PQ_DanhSachNhomQuyen> DanhSachNhomQuyen { get; set; }
        public List<DM_QuyenChucNangMap> DanhSachQuyenChucNang { get; set; }

    }
    public class PQ_DanhSachNhomQuyen
    {
        public long IdNhomQuyen { get; set; }
        public string TenNhomQuyen { get; set; }
        public string DanhSachQuyenChucNangID { get; set; }

    }
    public class PQ_PhanQuyenChucNang
    {
        public long ID { get; set; }
        public long NhomQuyenID { get; set; }
        public long QuyenChucNangID { get; set; }
        public Guid CreatedUserID { get; set; }
        public Guid LastUpdUserID { get; set; }
    }
    public class Roles // nhom quyen
    {
        public long Id { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }
        public string DefaultController { get; set; }
        public string DefaultAction { get; set; }
    }
    public class Right //quyen chuc nang
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
