namespace Business.Entities.Domain
{
    public class BC_BaoCaoTongHopHoatDongMap : PagesModel
    {
        public string STT { get; set; }
        public string LoaiGiayChungNhan { get; set; }
        public long SoGCNDaCap { get; set; }
        public long NopTrucTiep { get; set; }
        public long NopTrucTuyen { get; set; }
        public long NopQuaBuuDien { get; set; }
        public long HoSoDoTGCSHNop { get; set; }
        public long ToChucTuVanDichVu { get; set; }
        public long ToChuCaNhan { get; set; }
        public long CapChoChuTheNuocNgoai { get; set; }
        public long CapChoChuTheTrongNuoc { get; set; }
        public long CapChoDongSoHuu { get; set; }
    }
    public class BC_BaoCaoTongHopHoatDongParam : PagesParamModel
    {
        public int KyBaoCao { get; set; }
        public int Nam { get; set; }
        public int Quy { get; set; }
        public int Thang { get; set; }
        public string TuNgay { get; set; }
        public string DenNgay { get; set; }

    }
}
