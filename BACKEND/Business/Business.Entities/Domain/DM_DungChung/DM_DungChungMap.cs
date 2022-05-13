using System;

namespace Business.Entities.Domain
{
    public class DM_DungChungMap : PagesModel
    {
        public int STT { get; set; }
        public long ID { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }
    }
    public class DM_DungChungMapParam : PagesParamModel
    {
        public string TuKhoa { get; set; }
        public string Table { get; set; }
    }
    public class DM_DungChungMapAdd
    {
        public long ID { get; set; }
        public string Ma { get; set; }
        public string Table { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }
        public Guid CreatedUserID { get; set; }
    }
}
