using System;

namespace Business.Entities.Domain
{
    public class DM_CoQuanCapMap : PagesModel
    {
        public long Id { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }
        public bool IsActive { get; set; }
    }

    public class DM_CoQuanCapMapParam : PagesParamModel
    {
        public string TuKhoa { get; set; }
        public bool IsActive { get; set; }
    }

    public class DM_CoQuanCapMapAdd
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
