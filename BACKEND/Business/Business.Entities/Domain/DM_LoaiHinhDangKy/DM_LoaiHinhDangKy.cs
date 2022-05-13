using System;

namespace Business.Entities.Domain
{
    public class DM_LoaiHinhDangKyMap : PagesModel
    {
        public long LoaiHinhId { get; set; }
        public string Ma { get; set; }
        public string TenLoaiHinh { get; set; }
        public string MoTa { get; set; }
        public bool IsActive { get; set; }
    }

    public class DM_LoaiHinhDangKyMapParam : PagesParamModel
    {
        public string TuKhoa { get; set; }
        public string Ma { get; set; }
        public string TenDanhMuc { get; set; }
        public bool IsActive { get; set; }
    }

    public class DM_LoaiHinhDangKyMapAdd
    {
        public long LoaiHinhId { get; set; }
        public string Ma { get; set; }
        public string TenLoaiHinh { get; set; }
        public string MoTa { get; set; }
        public bool IsActive { get; set; }
        public Guid CreatedUserID { get; set; }
        public Guid LastUpdUserID { get; set; }

    }
}
