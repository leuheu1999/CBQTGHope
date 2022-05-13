using System;

namespace Business.Entities.Domain
{
    public class DM_QuocGiaMap : PagesModel
    {
        public int STT { get; set; }
        public long QuocGiaID { get; set; }
        public string MaQuocGia { get; set; }
        public string TenQuocGia { get; set; }
        public string MoTa { get; set; }
        public bool Used { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int ThuTuHienThi { get; set; }
    }
    public class DM_QuocGiaMapParam : PagesParamModel
    {
        public string TuKhoa { get; set; }
    }
    public class DM_QuocGiaMapAdd
    {
        public long QuocGiaID { get; set; }
        public string MaQuocGia { get; set; }
        public string TenQuocGia { get; set; }
        public string MoTa { get; set; }
        public bool Used { get; set; }
        public int ThuTuHienThi { get; set; }
        public Guid CreatedUserID { get; set; }
        public Guid LastUpdUserID { get; set; }
    }
}
