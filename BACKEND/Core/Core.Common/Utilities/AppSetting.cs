using System.Configuration;
using System.Web.Configuration;

namespace Core.Common.Utilities
{
    public static class AppSetting
    {
        public static string ReadSetting(string key)
        {
            try
            {
                var appSettings = WebConfigurationManager.AppSettings;
                return appSettings[key];
            }
            catch (ConfigurationErrorsException)
            {
                return string.Empty;
            }
        }
        public static string TaiKhoanCongDan
        {
            get
            {
                return ReadSetting("TaiKhoanCongDan.ApiBaseUrl");
            }
        }
        public static string Users
        {
            get
            {
                return ReadSetting("Users.ApiBaseUrl");
            }
        }
        public static string DC
        {
            get
            {
                return ReadSetting("DungChung.ApiBaseUrl");
            }
        }
        public static string Page
        {
            get
            {
                return ReadSetting("Page.ApiBaseUrl");
            }
        }
        public static string Log
        {
            get
            {
                return ReadSetting("Logging.ApiBaseUrl");
            }
        }
        public static string QuyenTacGia
        {
            get
            {
                return ReadSetting("QuyenTacGia.ApiBaseUrl");
            }
        }
        public static string QuyenLienQuan
        {
            get
            {
                return ReadSetting("QuyenLienQuan.ApiBaseUrl");
            }
        }
        public static string DVC_QuyenTacGia
        {
            get
            {
                return ReadSetting("DVC_QuyenTacGia.ApiBaseUrl");
            }
        }
        public static string DVC_QuyenLienQuan
        {
            get
            {
                return ReadSetting("DVC_QuyenLienQuan.ApiBaseUrl");
            }
        }
        public static string HS_CapSo
        {
            get
            {
                return ReadSetting("HS_CapSo.ApiBaseUrl");
            }
        }
        public static string DC_1Cua
        {
            get
            {
                return ReadSetting("DC_1Cua.ApiBaseUrl");
            }
        }
        public static string BC_ThongKe
        {
            get
            {
                return ReadSetting("BC_ThongKe.ApiBaseUrl");
            }
        }
        public static string DC_DVC
        {
            get
            {
                return ReadSetting("DC_DVC.ApiBaseUrl");
            }
        }
    }
}
