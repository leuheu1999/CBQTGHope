using System;

namespace Business.Entities.Domain
{
    public class HS_CapSoMap : PagesModel
    {
        public long CapSoID { get; set; }
        public string NgayCap { get; set; }

        public string So { get; set; }
        public string Prefix { get; set; }
        public string TenLoaiSo { get; set; }
    }
    public class HS_CapSoMapParam : PagesParamModel
    {
        public string TuKhoa { get; set; }
        public long HoSoID { get; set; }
    }

    public class HS_CapSoMapAdd
    {
        public long CapSoID { get; set; }
        public int LoaiSoID { get; set; }
        public string So { get; set; }
        public string Prefix { get; set; }
        public string NgayCap { get; set; }
        public string CoQuanCapID { get; set; }
        public string NguoiKyID { get; set; }

        public string GhiChu { get; set; }
        public long HoSoID { get; set; }
        public Guid CreatedUserID { get; set; }

        // biến logic 
        public bool TuTang { get; set; }
        public int LoaiNghiepVuID { get; set; }
    }
    public class HS_GenSo
    {
        public string STT { get; set; }
        public string SoGCN { get; set; }
    }
}
