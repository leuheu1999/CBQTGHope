using System;
using System.Web.Configuration;

namespace Module.Framework.Utilities
{
    public static class ParameterSetting
    {
        public static string RootUrlupload
        {
            get
            {
                return WebConfigurationManager.AppSettings["Urlupload"];
            }
        }
        public static string PathFileUpload
        {
            get
            {
                return "Uploads";//ReadValueSetting("PathFileUpload");
            }
        }
        public static string PathImgSlider
        {
            get
            {
                return "Sliders";//ReadValueSetting("PathFileUpload");
            }
        }
        public static string PathFileThanhPhanThuTuc
        {
            get
            {
                return WebConfigurationManager.AppSettings["folderBieuMau"];
            }
        }
        public static string Avatar
        {
            get
            {
                return "Avatar";
            }
        }
        public static string PathFileAlbumHinhAnh
        {
            get
            {
                return "AlbumHinhAnh";//ReadValueSetting("PathFileUpload");
            }
        }
    }
}
