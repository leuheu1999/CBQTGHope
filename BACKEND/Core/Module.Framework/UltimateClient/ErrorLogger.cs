using log4net;
using System;

namespace Module.Framework.UltimateClient
{
    public class ErrorLogger : IErrorLogger
    {
        protected static readonly ILog Log = LogManager.GetLogger(typeof(ErrorLogger));
        public void LogError(Exception ex, string infoMessage)
        {
            Log.Error(string.Format("Exception: {0} -- info message: {1}", ex.Message, infoMessage));
            //TODO: Log the error to error database
        }
    }
}
