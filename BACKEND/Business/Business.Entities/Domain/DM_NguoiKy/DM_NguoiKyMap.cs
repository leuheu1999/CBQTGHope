using System;

namespace Business.Entities.Domain
{
    public class DM_NguoiKyMap : PagesModel
    {
        public long Id { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }
        public bool IsActive { get; set; }
    }

    public class DM_NguoiKyMapParam : PagesParamModel
    {
        public string TuKhoa { get; set; }
        public bool IsActive { get; set; }
    }

    public class DM_NguoiKyMapAdd
    {
        public long Id { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }
        public bool IsActive { get; set; }
        public Guid CreatedUserID { get; set; }
        public Guid LastUpdUserID { get; set; }

    }
}
