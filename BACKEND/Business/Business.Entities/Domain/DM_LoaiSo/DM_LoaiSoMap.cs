using System;

namespace Business.Entities.Domain
{
    public class DM_LoaiSoMap : PagesModel
    {
        public int STT { get; set; }
        public long ID { get; set; }
        public string Ten { get; set; }
        public int SoHienTai { get; set; }
        public int Nam { get; set; }
        public string Prefix { get; set; }
        public bool ResetTheoNam { get; set; }
        public bool TuTang { get; set; }
        public int LoaiNghiepVuID { get; set; }
        public string TenNghiepVu { get; set; }
    }
    public class DM_LoaiSoMapParam : PagesParamModel
    {
        public string TuKhoa { get; set; }
    }
    public class DM_LoaiSoMapAdd
    {
        public long ID { get; set; }
        public string Ten { get; set; }
        public int SoHienTai { get; set; }
        public int Nam { get; set; }
        public string Prefix { get; set; }
        public bool ResetTheoNam { get; set; }
        public bool TuTang { get; set; }
        public int LoaiNghiepVuID { get; set; }
    }
}
