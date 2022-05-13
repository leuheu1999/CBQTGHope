using System;

namespace Business.Entities.Domain
{
    public class TT_TacGiaMap : PagesModel
    {
        public long TacGiaID { get; set; }
        public string HoVaTen { get; set; }
        public string QuocTich { get; set; }
        public string SoCMND { get; set; }
        public string NgayCap { get; set; }
        public string DiaChi { get; set; }
        public string ButDanh { get; set; }
    }
    public class TT_TacGiaParam : PagesParamModel
    {
        public string HoVaTen { get; set; }
        public string SoCMND { get; set; }
        public string NgayCap { get; set; }
        public string DiaChi { get; set; }
        public string ButDanh { get; set; }
    }
    public class TT_TacGiaAdd
    {
        public long TacGiaID { get; set; }
        public string HoVaTen { get; set; }
        public int QuocTichID { get; set; }
        public string QuocTich { get; set; }
        public string SoCMND { get; set; }
        public string NgayCapCMND { get; set; }
        public int NoiCapID { get; set; }
        public string NoiCap { get; set; }
        public string DiaChi { get; set; }
        public string ButDanh { get; set; }
        public Guid UserID { get; set; }
    }
}
