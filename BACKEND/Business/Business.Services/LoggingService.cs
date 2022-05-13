using Business.Services.Interfaces;
using Core.Common.Utilities;
using log4net;
using Business.Entities.Domain;
using Data.Core.Repositories.Base;
using System.Web;
using Data.Core.Repositories.Interfaces;
using System.Collections.Generic;

namespace Business.Services
{
    public class LoggingService : ILoggingService
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(LoggingService));
        private readonly ILogRepository _logRepository;
        public LoggingService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }
     
        public ResultResponse<long> LogError(LogAddView model)
        {
            var response = new ResponseModel();
            //log file
            log4net.Config.XmlConfigurator.Configure();
            _logger.Error(model.ShortMessages+": " + model.ex.StackTrace);
            // log db
            HttpContextBase abstractContext = new System.Web.HttpContextWrapper(HttpContext.Current);
            var wb = new WebHelper(abstractContext);
            var logadd = new LogAdd()
            {
                LogLevelId = (int)LogLevel.Error,
                ShortMessage = model.ShortMessages,
                FullMessage = model.ex.StackTrace,
                CustomerId = model.UserID,
                IpAddress = wb.GetCurrentIpAddress(),
                PageUrl = model.UrlPath,
                ReferrerUrl = model.ReferrerUrl
            };
            var data = _logRepository.InsertLog(logadd, out response);
            return new ResultResponse<long>(response, data);
        }
        public ResultResponse<long> LogDebug(LogAddView model)
        {
            log4net.Config.XmlConfigurator.Configure();
            _logger.Error(model.ShortMessages + ": " + model.ex.StackTrace);
            var response = new ResponseModel();
            HttpContextBase abstractContext = new System.Web.HttpContextWrapper(HttpContext.Current);
            var wb = new WebHelper(abstractContext);
            var logadd = new LogAdd()
            {
                LogLevelId = (int)LogLevel.Debug,
                ShortMessage = model.ShortMessages,
                FullMessage = model.ex.StackTrace,
                CustomerId = model.UserID,
                IpAddress = wb.GetCurrentIpAddress(),
                PageUrl = model.UrlPath,
                ReferrerUrl = model.ReferrerUrl
            };
            var data = _logRepository.InsertLog(logadd, out response);
            return new ResultResponse<long>(response, data);
        }
        public ResultResponse<long> LogInformation(LogAddView model)
        {
            var response = new ResponseModel();
            HttpContextBase abstractContext = new System.Web.HttpContextWrapper(HttpContext.Current);
            var wb = new WebHelper(abstractContext);
            var logadd = new LogAdd()
            {
                LogLevelId = (int)LogLevel.Information,
                ShortMessage = model.ShortMessages,
                FullMessage = model.ex.StackTrace,
                CustomerId = model.UserID,
                IpAddress = wb.GetCurrentIpAddress(),
                PageUrl = model.UrlPath,
                ReferrerUrl = model.ReferrerUrl
            };
            var data = _logRepository.InsertLog(logadd, out response);
            return new ResultResponse<long>(response, data);
        }
        public ResultResponse<long> LogWarning(LogAddView model)
        {
            var response = new ResponseModel();
            HttpContextBase abstractContext = new System.Web.HttpContextWrapper(HttpContext.Current);
            var wb = new WebHelper(abstractContext);
            var logadd = new LogAdd()
            {
                LogLevelId = (int)LogLevel.Warning,
                ShortMessage = model.ShortMessages,
                FullMessage = model.ex.StackTrace,
                CustomerId = model.UserID,
                IpAddress = wb.GetCurrentIpAddress(),
                PageUrl = model.UrlPath,
                ReferrerUrl = model.ReferrerUrl
            };
            var data = _logRepository.InsertLog(logadd, out response);
            return new ResultResponse<long>(response, data);
        }
        public ResultResponse<long> LogFatal(LogAddView model)
        {
            log4net.Config.XmlConfigurator.Configure();
            _logger.Error(model.ShortMessages + ": " + model.ex.StackTrace);
            var response = new ResponseModel();
            HttpContextBase abstractContext = new System.Web.HttpContextWrapper(HttpContext.Current);
            var wb = new WebHelper(abstractContext);
            var logadd = new LogAdd()
            {
                LogLevelId = (int)LogLevel.Fatal,
                ShortMessage = model.ShortMessages,
                FullMessage = model.ex.StackTrace,
                CustomerId = model.UserID,
                IpAddress = wb.GetCurrentIpAddress(),
                PageUrl = model.UrlPath,
                ReferrerUrl = model.ReferrerUrl
            };
            var data = _logRepository.InsertLog(logadd, out response);
            return new ResultResponse<long>(response, data);
        }       
        public ResultResponse<List<LogMap>> LogSystem_List(LogParam model)
        {
            var response = new ResponseModel();
            var data = _logRepository.LogSystem_List(model, out response);
            var result = new ResultResponse<List<LogMap>>(response, data);
            return result;
        }
        public ResultResponse<LogAdd> GetLogSystemById(long id)
        {
            var response = new ResponseModel();
            var data = _logRepository.GetLogSystemById(id, out response);
            var result = new ResultResponse<LogAdd>(response, data);
            return result;
        }
        public ResultResponse<int> LogSystem_DelByID(long id)
        {
            var response = new ResponseModel();
            var data = _logRepository.LogSystem_DelByID(id, out response);
            var result = new ResultResponse<int>(response, data);
            return result;
        }
        public ResultResponse<int> LogSystem_DelLstID(List<long> lstid)
        {
            var response = new ResponseModel();
            var data = _logRepository.LogSystem_DelLstID(lstid, out response);
            var result = new ResultResponse<int>(response, data);
            return result;
        }
        public ResultResponse<int> LogSystem_Trancate()
        {
            var response = new ResponseModel();
            var data = _logRepository.LogSystem_Trancate( out response);
            var result = new ResultResponse<int>(response, data);
            return result;
        }
    }
}
