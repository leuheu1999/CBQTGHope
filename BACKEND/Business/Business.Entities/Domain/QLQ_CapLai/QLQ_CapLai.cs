using System;
using System.Collections.Generic;

namespace Business.Entities.Domain
{
    public class QLQ_CapLaiAdd
    {
        public long CapLaiID { get; set; }
        public string STT { get; set; }
        public string SoGCN { get; set; }
        public string NgayCapGCN { get; set; }
        public string STTCu { get; set; }
        public string SoGCNCu { get; set; }
        public string NgayCapGCNCu { get; set; }
        public string ThongTinCapLai { get; set; }
        public string SoQD { get; set; }
        public string NgayQD { get; set; }
        public string LyDoCapLai { get; set; }
        public string ThongTinTacPham { get; set; }
        public int SoLanCapLai { get; set; }
        public int NguoiKyID { get; set; }
        public string NgayKy { get; set; }
        public long StaticID { get; set; }
        public long QuyenLienQuanID { get; set; }
        public long QuyenLienQuanCuID { get; set; }
        public Guid UserID { get; set; }

        //Thông tin hồ sơ
        public long HoSoID { get; set; }
        public long ThuTucID { get; set; }
        public string TenThuTuc { get; set; }
        public string MaBoHoSo { get; set; }
        public string SoBienNhan { get; set; }
        public List<QLQ_DinhKemAdd> ListDinhKem { get; set; }
    }
}
