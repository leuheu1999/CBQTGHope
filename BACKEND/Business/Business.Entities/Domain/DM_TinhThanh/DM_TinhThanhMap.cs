using System;

namespace Business.Entities.Domain
{
    public class DM_TinhThanhMap : PagesModel
    {
        public int STT { get; set; }
        public long ID { get; set; }
        public long QuocGiaID { get; set; }
        public string TenQuocGia { get; set; }
        public string Ma{ get; set; }
        public string MaLienThong { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }
        public bool CongKhai { get; set; }
        public int ThuTuHienThi { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
    }
    public class DM_TinhThanhMapParam : PagesParamModel
    {
        public string TuKhoa { get; set; }
        public long QuocGiaID { get; set; }
       
    }
    public class DM_TinhThanhMapAdd
    {
        public long ID { get; set; }
        public long QuocGiaID { get; set; }
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
