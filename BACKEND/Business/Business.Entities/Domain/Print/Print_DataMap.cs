namespace Business.Entities.Domain
{
    public class Print_DataMap: PagesParamModel
    {
        public string StoreProcedure { get; set; }
        public string Parameters { get; set; }
        public string Ma { get; set; }
        public string TuKhoa { get; set; }
        public string Where { get; set; }
        public string ParameterSort { get; set; }

        public string TenDoanhNghiep { get; set; }
        public string TuNgay { get; set; }
        public string DenNgay { get; set; }
        public string CTKMNhomSanPhamID { get; set; }
        public string CTKMTenSanPhamDuocKhuyenMai { get; set; }
        public string CTKMHinhThucKhuyenMaiID { get; set; }
        public string QuanHuyenID { get; set; }


    }
}
