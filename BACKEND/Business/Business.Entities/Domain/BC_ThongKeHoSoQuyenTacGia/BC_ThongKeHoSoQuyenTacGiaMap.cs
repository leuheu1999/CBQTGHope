using System;

namespace Business.Entities.Domain
{
    public class BC_ThongKeHoSoQuyenTacGiaMap : PagesModel
    {
        public int STT { get; set; }
        public string Ten { get; set; }
        public int CapMoi { get; set; }
        public int CapLai { get; set; }
        public int CapDoi { get; set; }
        public int ChuyenChuSoHuu { get; set; }
        public int ThuHoi { get; set; }
    }
    public class BC_ThongKeHoSoQuyenTacGiaParam : PagesParamModel
    {
       public int Nam { get; set; }
    }
}
