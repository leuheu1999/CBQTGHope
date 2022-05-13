using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using Data.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Data.Core.Repositories.Base
{
    /// <summary>
    /// Default logger
    /// </summary>
    public partial class DefaultLogger: ILogger
    {
        #region Fields

        private readonly ILogRepository _logRepository;
        private readonly IWebHelper _webHelper;
        #endregion
        public DefaultLogger(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }
        #region Methods
        public virtual long InsertLog(LogLevel logLevel, string shortMessage, string fullMessage = "", Guid customerid = new Guid())
        {
            var response = new ResponseModel();
            var log = new LogAdd();
            HttpContextBase abstractContext = new System.Web.HttpContextWrapper(HttpContext.Current);
            var wb = new WebHelper(abstractContext);
            log.LogLevelId = (int)logLevel;
            log.ShortMessage = shortMessage;
            log.FullMessage = fullMessage;
            log.IpAddress = wb.GetCurrentIpAddress();
            log.CustomerId = customerid;
            log.PageUrl = wb.GetThisPageUrl(false);
            log.ReferrerUrl = wb.GetUrlReferrer();
            //Request.UrlReferrer.PathAndQuery
            // UrlPath = Request.Url.AbsoluteUri
            var rs = _logRepository.InsertLog(log, out response);

            return rs;
        }

        #endregion
    }
}
