using System;

namespace Business.Entities.Domain
{
    public class TT_CongDanMap : PagesModel
    {
        public long CongDanID { get; set; }
        public string HoVaTen { get; set; }
        public string QuocTich { get; set; }
        public string SoCMND { get; set; }
        public string NgayCap { get; set; }
        public string SoDKKD { get; set; }
        public string NgayCapDKKD { get; set; }
        public string ButDanh { get; set; }
        public string DiaChi { get; set; }
        public string Key { get; set; }
    }
    public class TT_CongDanParam : PagesParamModel
    {
        public string HoVaTen { get; set; }
        public string SoCMND { get; set; }
        public string NgayCap { get; set; }
        public string DiaChi { get; set; }
        public string SoDKKD { get; set; }
        public string NgayCapDKKD { get; set; }
        public string ButDanh { get; set; }
        public string Key { get; set; }
    }
    public class TT_CongDanAdd
    {
        public long CongDanID { get; set; }
        public string HoVaTen { get; set; }
        public int QuocTichID { get; set; }
        public string QuocTich { get; set; }
        public string SoCMND { get; set; }
        public string NgayCapCMND { get; set; }
        public int NoiCapID { get; set; }
        public string NoiCap { get; set; }
        public string SoDKKD { get; set; }
        public string NgayCapDKKD { get; set; }
        public int NoiCapDKKDID { get; set; }
        public string NoiCapDKKD { get; set; }
        public string DiaChi { get; set; }
        public string ButDanh { get; set; }
        public Guid UserID { get; set; }
    }
}
