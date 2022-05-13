using System;

namespace Business.Entities.Domain
{
    public class TT_ChuSoHuuMap : PagesModel
    {
        public long ChuSoHuuID { get; set; }
        public string HoVaTen { get; set; }
        public string QuocTich { get; set; }
        public string SoCMND { get; set; }
        public string NgayCap { get; set; }
        public string SoDKKD { get; set; }
        public string NgayCapDKKD { get; set; }
        public string DiaChi { get; set; }
    }
    public class TT_ChuSoHuuParam : PagesParamModel
    {
        public string HoVaTen { get; set; }
        public string SoCMND { get; set; }
        public string NgayCap { get; set; }
        public string SoDKKD { get; set; }
        public string NgayCapDKKD { get; set; }
        public string DiaChi { get; set; }
    }
    public class TT_ChuSoHuuAdd
    {
        public long ChuSoHuuID { get; set; }
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
        public Guid UserID { get; set; }
    }
}
