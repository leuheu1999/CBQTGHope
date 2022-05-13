using System;

namespace Business.Entities.Domain
{
    public class ND_NhatKyNguoiDungMap : PagesModel
    {
        public int STT { get; set; }
        public long ID { get; set; }
        public string TenTaiKhoan { get; set; }
        public string LogTypeName { get; set; }
        public string DiaChiIP { get; set; }
        public string NoiDung { get; set; }
        public string NgayTao { get; set; }
    }
    public class ND_NhatKyNguoiDungParam : PagesParamModel
    {
        public string TuKhoa { get; set; }
        public string TuNgay { get; set; }
        public string DenNgay { get; set; }
        public int LogTypeID { get; set; }
    }
    public class ND_NhatKyNguoiDungAdd
    {
        public Guid NguoiDungID { get; set; }
        public string TenTaiKhoan { get; set; }
        public string NoiDung { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public long? LogTypeID { get; set; }
        public string KeyWord { get; set; }
        public string LogTypeName { get; set; }
        public string DiaChiIP { get; set; }
    }
    public class LogTypeNDmap
    {
        public long LogTypeID { get; set; }
        public string KeyWord {get;set;}
        public string LogTypeName { get; set; }
    }
    public class DSLogTypeNDmap : PagesModel
    {
        public int STT { get; set; }
        public long LogTypeID { get; set; }
        public string KeyWord { get; set; }
        public string LogTypeName { get; set; }
        public bool Used { get; set; }
    }
    public class LogTypeNDParam : PagesParamModel
    {
        public string TuKhoa { get; set; }
    }
}
