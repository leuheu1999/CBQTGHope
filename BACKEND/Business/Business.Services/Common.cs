using Business.Entities.Domain;
using Data.Core.Repositories.Base;
using System;
using System.Data.SqlClient;
using System.IO;

namespace Business.Services
{
    public static class Common
    {
        ////public static string CreateConnectionString(WebsiteAdd model)
        ////{
        ////    var builder = new SqlConnectionStringBuilder();
        ////    builder.DataSource = model.SQL_Source;
        ////    builder.InitialCatalog = model.SQL_Catalog;
        ////    builder.UserID = model.SQL_UserID;
        ////    builder.Password = model.SQL_PassWord;
        ////    builder.PersistSecurityInfo = false;
        ////    return builder.ConnectionString;
        ////}
        ////public static DataSettingsManager WriteFileConnection(WebsiteAdd result, Guid userId)
        ////{
        ////    var fileName = "Settings.json";
        ////    if (!string.IsNullOrEmpty(userId.ToString()))
        ////        fileName = userId.ToString() + ".Settings.json";
        ////    string filePath = Path.Combine(Core.Common.Utilities.CommonHelper.MapPath("~/Connecttion/"), fileName);
        ////    if (File.Exists(filePath))
        ////    {
        ////        File.Delete(filePath);
        ////    }
        ////    var settings = new DataSettings
        ////    {
        ////        DataProvider = "sqlserver",
        ////        DataConnectionString = CreateConnectionString(result),
        ////        Hosting_FTP_IP = result.Hosting_FTP_IP,
        ////        Hosting_FTP_Port = result.Hosting_FTP_Port,
        ////        AddressWebsite = result.AddressWebsite,
        ////        Hosting_FTP_User = result.Hosting_FTP_User,
        ////        Hosting_FTP_Password = result.Hosting_FTP_Password,
        ////    };
        ////    var settingsManager = new DataSettingsManager();
        ////    settingsManager.SaveSettings(settings, filePath);
        ////    return settingsManager;
        ////}
    }
}
