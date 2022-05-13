using Business.Entities.Domain;
using System;
using System.Collections.Generic;

namespace Data.Core.Repositories.Base
{
    /// <summary>
    /// Logger interface
    /// </summary>
    public partial interface ILogger
    {     
        long InsertLog(LogLevel logLevel, string shortMessage, string fullMessage = "", Guid customerid = new Guid());
    }
}
