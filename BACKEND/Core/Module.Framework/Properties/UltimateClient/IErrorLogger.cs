using System;

namespace Module.Framework.UltimateClient
{
    public interface IErrorLogger
    {
        void LogError(Exception ex, string infoMessage);
    }
}
