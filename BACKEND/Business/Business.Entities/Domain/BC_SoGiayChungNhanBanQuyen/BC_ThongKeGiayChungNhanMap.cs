using System;

namespace Business.Entities.Domain
{
    public class BC_ThongKeGiayChungNhanMap : PagesModel
    {
        public string STT { get; set; }
        public string Ten { get; set; }
        public int CapMoi { get; set; }
        public int CapLai { get; set; }
        public int CapDoi { get; set; }
        public int ChuyenChuSoHuu { get; set; }
        public int ThuHoi { get; set; }
    }
    public class BC_ThongKeGiayChungNhanParam : PagesParamModel
    {
        public int KyBaoCao { get; set; }
        public int Nam { get; set; }
        public int Quy { get; set; }
        public int Thang { get; set; }
        public int LoaiHinhID { get; set; }
        public int VungMienID { get; set; }
        public string TuNgay { get; set; }
        public string DenNgay { get; set; }
    }
}
