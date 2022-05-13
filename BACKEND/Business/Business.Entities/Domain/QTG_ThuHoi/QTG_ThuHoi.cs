using System;
using System.Collections.Generic;

namespace Business.Entities.Domain
{
    public class QTG_ThuHoiAdd
    {
        public long ThuHoiID { get; set; }
        public string STT { get; set; }
        public string SoGCN { get; set; }
        public string NgayCapGCN { get; set; }
        public string ThongTinThuHoi { get; set; }
        public string SoQD { get; set; }
        public string NgayQD { get; set; }
        public string LyDoThuHoi { get; set; }
        public string ThongTinTacPham { get; set; }
        public int SoLanThuHoi { get; set; }
        public int NguoiKyID { get; set; }
        public string NgayKy { get; set; }
        public long StaticID { get; set; }
        public long QuyenTacGiaID { get; set; }
        public Guid UserID { get; set; }
        //Thông tin hồ sơ
        public long HoSoID { get; set; }
        public long ThuTucID { get; set; }
        public string TenThuTuc { get; set; }
        public string MaBoHoSo { get; set; }
        public string SoBienNhan { get; set; }
        public List<QTG_DinhKemAdd> ListDinhKem { get; set; }
    }
}
