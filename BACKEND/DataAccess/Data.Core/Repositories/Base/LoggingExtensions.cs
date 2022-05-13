using Business.Entities.Domain;
using System;

namespace Data.Core.Repositories.Base
{
    public static class LoggingExtensions
    {
        public static void Debug(this ILogger logger, string message, Exception exception = null, Guid customerid = new Guid())
        {
            FilteredLog(logger, LogLevel.Debug, message, exception, customerid);
        }
        public static void Information(this ILogger logger, string message, Exception exception = null, Guid customerid = new Guid())
        {
            FilteredLog(logger, LogLevel.Information, message, exception, customerid);
        }
        public static void Warning(this ILogger logger, string message, Exception exception = null, Guid customerid = new Guid())
        {
            FilteredLog(logger, LogLevel.Warning, message, exception, customerid);
        }
        public static void Error(this ILogger logger, string message, Exception exception = null, Guid customerid = new Guid())
        {
            FilteredLog(logger, LogLevel.Error, message, exception, customerid);
        }
        public static void Fatal(this ILogger logger, string message, Exception exception = null, Guid customerid = new Guid())
        {
            FilteredLog(logger, LogLevel.Fatal, message, exception, customerid);
        }
        private static void FilteredLog(ILogger logger,LogLevel level, string message, Exception exception = null, Guid customerid = new Guid())
        {
            //don't log thread abort exception
            if (exception is System.Threading.ThreadAbortException)
                return;
            string fullMessage = exception == null ? string.Empty : exception.ToString();
            logger.InsertLog(level, message, fullMessage, customerid);
           
        }
    }
}
