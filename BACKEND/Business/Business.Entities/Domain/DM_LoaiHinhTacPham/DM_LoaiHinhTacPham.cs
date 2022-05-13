using System;

namespace Business.Entities.Domain
{
    public class DM_LoaiHinhTacPhamMap : PagesModel
    {
        public long LoaiHinhId { get; set; }
        public string Ma { get; set; }
        public string Nhom { get; set; }
        public string LoaiHinhDangKy { get; set; }
        public string TenLoaiHinh { get; set; }
        public string MoTa { get; set; }
        public bool IsActive { get; set; }
    }

    public class DM_LoaiHinhTacPhamMapParam : PagesParamModel
    {
        public string TuKhoa { get; set; }
        public string Ma { get; set; }
        public string TenDanhMuc { get; set; }
        public bool IsActive { get; set; }
    }

    public class DM_LoaiHinhTacPhamMapAdd
    {
        public long LoaiHinhId { get; set; }
        public string Ma { get; set; }
        public int LoaiHinhDangKyId { get; set; }
        public string Nhom { get; set; }
        public int ParentID { get; set; }
        public string TenLoaiHinh { get; set; }
        public string MoTa { get; set; }
        public bool IsActive { get; set; }
        public Guid CreatedUserID { get; set; }
        public Guid LastUpdUserID { get; set; }

    }
}
