using System;

namespace Business.Entities.Domain
{
    public class TT_PhimMap : PagesModel
    {
        public int Stt { get; set; }
        public long PhimID { get; set; }
        public string Ten { get; set; }
        public string TenTepTin { get; set; }
        public string TenGoc { get; set; }
        public string Tag { get; set; }
        public string DuongDan { get; set; }
        public string GhiChu { get; set; }
        public int Checked { get; set; }
        public string CreatedUser { get; set; }
        public string CreatedDate { get; set; }
    }
    public class TT_PhimParam : PagesParamModel
    {
        public long QuyenID { get; set; }
        public int LinhVucID { get; set; }
        public string Ten { get; set; }
        public string TenGoc { get; set; }
        public string Tag { get; set; }
        public string GhiChu { get; set; }
        public string KeyPage { get; set; }
        public int Checked { get; set; }
    }
    public class TT_PhimAdd
    {
        public long PhimID { get; set; }
        public long QuyenID { get; set; }
        public long LinhVucID { get; set; }
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
