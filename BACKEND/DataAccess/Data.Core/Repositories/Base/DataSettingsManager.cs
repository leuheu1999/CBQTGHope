using Newtonsoft.Json;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Collections.Specialized;
using System.Text;

namespace Data.Core.Repositories.Base
{
    public partial class DataSettingsManager
    {
        protected const char separator = ':';
        protected const string filename = "Settings.json";

        /// <summary>
        /// Parse settings
        /// </summary>
        /// <param name="text">Text of settings file</param>
        /// <returns>Parsed data settings</returns>
        protected virtual DataSettings ParseSettings(string text)
        {
            var shellSettings = new DataSettings();
            if (String.IsNullOrEmpty(text))
                return shellSettings;
            string str = "";
            using (var reader = new StringReader(text))
            {
                str = reader.ReadToEnd();
            }
            try
            {
                shellSettings = Base64ToObject<DataSettings>(str);
               //shellSettings = JsonConvert.DeserializeObject<DataSettings>(str);
            }
            catch
            {

            }

            return shellSettings;
        }
       
        /// <summary>
        /// Convert data settings to string representation
        /// </summary>
        /// <param name="settings">Settings</param>
        /// <returns>Text</returns>
        protected virtual string ComposeSettings(DataSettings settings)
        {
            if (settings == null)
                return "";

            return JsonConvert.SerializeObject(settings);
        }
        /// <summary>
        /// Load settings
        /// </summary>
        /// <param name="filePath">File path; pass null to use default settings file path</param>
        /// <returns></returns>
        public virtual DataSettings LoadSettings(string filePath = null)
        {
            if (String.IsNullOrEmpty(filePath))
            {
                filePath = Path.Combine(CommonHelper.MapPath("~/Connecttion/"), filename);
            }
            else
            {
                filePath = Path.Combine(CommonHelper.MapPath("~/Connecttion/"), filePath);
            }
            if (File.Exists(filePath))
            {
                string text = File.ReadAllText(filePath);
                return ParseSettings(text);
            }

            return new DataSettings();
        }
        /// <summary>
        /// Load settings
        /// </summary>
        /// <param name="filePath">File path; pass null to use default settings file path</param>
        /// <returns></returns>
        public virtual DataSettings WebLoadSettings(string filePath = null)
        {
            if (String.IsNullOrEmpty(filePath))
            {
                filePath = Path.Combine(CommonHelper.MapPath("~/Connecttion/FontEnd/"), filename);
            }
            else
            {
                filePath = Path.Combine(CommonHelper.MapPath("~/Connecttion/FontEnd/"), filePath);
            }
            if (File.Exists(filePath))
            {
                string text = File.ReadAllText(filePath);
                return ParseSettings(text);
            }

            return new DataSettings();
        }
        /// <summary>
        /// Save settings to a file
        /// </summary>
        /// <param name="settings"></param>
        public virtual void SaveSettings(DataSettings settings,string filePath)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");
            if(string.IsNullOrEmpty(filePath))
                filePath = Path.Combine(CommonHelper.MapPath("~/Connecttion/"), filename);
            if (!File.Exists(filePath))
            {
                using (File.Create(filePath))
                {
                    //we use 'using' to close the file after it's created
                }
            }

            var text = ComposeSettings(settings);

            byte[] bytes = Encoding.UTF8.GetBytes(text);

            File.WriteAllText(filePath, Convert.ToBase64String(bytes));
        }
        public virtual string GetStringConnect()
        {
            HttpContext httpContext = HttpContext.Current;
            NameValueCollection headerList = httpContext.Request.Headers;
            var token = headerList.Get("UserToken");
            var fileName = "";
            if (!string.IsNullOrEmpty(token))
                fileName = token + ".Settings.json";
            var strConnect = LoadSettings(fileName);
            var conn = new SqlConnectionStringBuilder(strConnect.DataConnectionString);
            return conn.ToString();
        }
        public virtual string GetStringConnectChuyenNganh()
        {
            string fileName = "SettingsChuyenNganh.json";
            var strConnect = LoadSettings(fileName);
            var conn = new SqlConnectionStringBuilder(strConnect.DataConnectionString);
            return conn.ToString();
        }
        public virtual string GetStringConnectMaster()
        {
            string fileName = "SettingsMaster.json";
            var strConnect = LoadSettings(fileName);
            var conn = new SqlConnectionStringBuilder(strConnect.DataConnectionString);
            return conn.ToString();
        }
        public virtual int GetNgonNguCurrent()
        {
            HttpContext httpContext = HttpContext.Current;
            NameValueCollection headerList = httpContext.Request.Headers;
            var token = headerList.Get("LanguageId");
            int ngonNguId = 0;
            if (!string.IsNullOrEmpty(token))
                ngonNguId = int.Parse(token);
            return ngonNguId;
        }
        public virtual string GetStringConnectWeb()
        {
            HttpContext httpContext = HttpContext.Current;
            NameValueCollection headerList = httpContext.Request.Headers;
            var wardName = headerList.Get("WardName");
            var languageIdWeb = headerList.Get("UniqueSeoCode");
            var fileName = "";
            if (!string.IsNullOrEmpty(wardName) && !string.IsNullOrEmpty(languageIdWeb))
                fileName = wardName + "-" + languageIdWeb + ".Settings.json";
            var strConnect = WebLoadSettings(fileName);
            var conn = new SqlConnectionStringBuilder(strConnect.DataConnectionString);
            return conn.ToString();
        }
        private T Base64ToObject<T>(string base64)
        {
            if (string.IsNullOrEmpty(base64))
            {
                HttpContext httpContext = HttpContext.Current;
                return default(T);
            }
            try
            {
                var byteArray = Convert.FromBase64String(base64);
                string jsonBack = Encoding.UTF8.GetString(byteArray);
                var obj = JsonConvert.DeserializeObject<T>(jsonBack);
                return (T)obj;

            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}
