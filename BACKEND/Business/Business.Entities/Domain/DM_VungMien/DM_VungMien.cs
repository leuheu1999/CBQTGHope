using System;

namespace Business.Entities.Domain
{
    public class DM_VungMienMap : PagesModel
    {
        public long Id { get; set; }
        public string Ma { get; set; }
        public string Prefix { get; set; }
        public string TenVungMien { get; set; }
        public string MoTa { get; set; }
        public bool IsActive { get; set; }
    }
    public class DM_VungMienMapParam : PagesParamModel
    {
        public string TuKhoa { get; set; }
        public string Ma { get; set; }
        public string TenVungMien { get; set; }
        public bool IsActive { get; set; }
    }
    public class DM_VungMienMapAdd
    {
        public long Id { get; set; }
        public string Ma { get; set; }
        public string Prefix { get; set; }
        public string TenVungMien { get; set; }
        public string MoTa { get; set; }
        public bool IsActive { get; set; }
        public Guid CreatedUserID { get; set; }
        public Guid LastUpdUserID { get; set; }
    }
}
