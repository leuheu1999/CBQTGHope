using System;

namespace Business.Entities.Domain
{
    public class BC_CongBaoMap : PagesModel
    {
        public string STT { get; set; }
        public string SoGCN { get; set; }
        public string NgayCap { get; set; }
        public string TenQuyen { get; set; }
        public string TenLoaiThuTuc { get; set; }
        public string TacPham { get; set; }
        public string TacGia { get; set; }
        public string ChuSoHuu { get; set; }
    }
    public class BC_CongBaoParam : PagesParamModel
    {
        public int KyBaoCao { get; set; }
        public int Nam { get; set; }
        public int Quy { get; set; }
        public int Thang { get; set; }
        public string TuNgay { get; set; }
        public string DenNgay { get; set; }

        public int LoaiHinhID { get; set; }
        public int VungMienID { get; set; }
        public int QuyenID { get; set; }
        public int LoaiThuTucID { get; set; }
        public string TacGia { get; set; }
        public string ChuSoHuu { get; set; }
        public string NguoiDaiDien { get; set; }
        public bool IsToChuc { get; set; }
    }
}
