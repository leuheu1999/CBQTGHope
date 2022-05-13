using System;

namespace Business.Entities.Domain
{
    public class LogMap : PagesModel
    {
        public int STT { get; set; }
        public long Id { get; set; }
        public int LogLevelId { get; set; }
        public string LogLevelName { get; set; }
        public string ShortMessage { get; set; }
        public string FullMessage { get; set; }
        public Guid CustomerId { get; set; }
        public string PageUrl { get; set; }
        public string ReferrerUrl { get; set; }
        public string IpAddress { get; set; }
        public string CreatedDate { get; set; }
    }
    public class LogParam : PagesParamModel
    {
        public string CreatedFrom{ get; set; }
        public string CreatedTo { get; set; }
        public string Message { get; set; }
        public int LoglevelID { get; set; }
        public string TypeSort { get; set; }
    }
    public class LogAdd
    {
        public long Id { get; set; }
        public int LogLevelId { get; set; }
        public string LogLevelName { get; set; }
        public string ShortMessage { get; set; }
        public string FullMessage { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string IpAddress { get; set; }
        public string PageUrl { get; set; }
        public string ReferrerUrl { get; set; }
        public string CreatedDate { get; set; }
    }
   public class LogAddView
    {
        public Exception ex { get; set; }
        public string ShortMessages { get; set; }
        public Guid UserID { get; set; }
        public string UrlPath { get; set; }
        public string ReferrerUrl { get; set; }

    }

}
