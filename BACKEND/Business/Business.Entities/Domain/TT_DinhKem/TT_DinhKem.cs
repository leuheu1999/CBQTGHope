using System;

namespace Business.Entities.Domain
{
    public class TT_DinhKemMap : PagesModel
    {
        public int Stt { get; set; }
        public long DinhKemID { get; set; }
        public string Ten { get; set; }
        public string TenTepTin { get; set; }
        public string TenGoc { get; set; }
        public string Tag { get; set; }
        public string DuongDan { get; set; }
        public string GhiChu { get; set; }
        public string CreatedUser { get; set; }
        public string CreatedDate { get; set; }
    }
    public class TT_DinhKemParam : PagesParamModel
    {
        public string Ten { get; set; }
        public string TenGoc { get; set; }
        public string Tag { get; set; }
        public string GhiChu { get; set; }
        public string KeyPage { get; set; }
    }
    public class TT_DinhKemAdd
    {
        public long DinhKemID { get; set; }
        public string Ten { get; set; }
        public string TenTepTin { get; set; }
        public string TenGoc { get; set; }
        public string Tag { get; set; }
        public string GhiChu { get; set; }
        public string DuongDan { get; set; }
        public string CreatedUser { get; set; }
        public string CreatedDate { get; set; }
        public Guid UserID { get; set; }
    }
}
