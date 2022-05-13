using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Script.Serialization;
using Business.Entities.Domain;
using Newtonsoft.Json;
namespace Module.Framework.Common
{
    public static class Common
    {
        public static T GetValue<T>(this object obj, string propertyName)
        {
            var data = obj.GetType().GetProperty(propertyName);
            return (T)data.GetValue(obj);
        }
        //public static void SaveSettings()
        //{
        //    SettingQuery.Save();
        //}
        public static string JsonSerialize(object target)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(target);
        }
        public static T JsonDeserialize<T>(string json)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(json);
        }
        //public static SettingViewModel GetSettings()
        //{
        //    var model = HttpContext.Current.Session[Constaints.Session.Setting] as SettingViewModel;
        //    if (model == null)
        //    {
        //        model = SettingQuery.Load();
        //        HttpContext.Current.Session[Constaints.Session.Setting] = model;
        //    }
        //    return model ?? (model = new SettingViewModel());
        //}
        public static string GetFormatDoubleString(this double value)
        {
            var formatString = System.Web.Configuration.WebConfigurationManager.AppSettings["formatDoubleString"];
            return String.Format(formatString, value);
        }

        //format int
        public static string GetFormatIntString(this int value)
        {
            var formatString = System.Web.Configuration.WebConfigurationManager.AppSettings["formatIntString"];
            return String.Format(formatString, value);
        }

        public static string GetFormatDecimalString(this decimal value)
        {
            var formatString = System.Web.Configuration.WebConfigurationManager.AppSettings["formatDoubleString"];
            return String.Format(formatString, value);
        }
        public static string ToBase64BitString(this HttpPostedFileBase target)
        {
            var binaryData = new Byte[target.InputStream.Length];
            long bytesRead = target.InputStream.Read(binaryData, 0,
                                 (int)target.InputStream.Length);
            return System.Convert.ToBase64String(binaryData, 0, binaryData.Length);
        }
        public static bool IsInRight(this IPrincipal a, string right)
        {
            var user = LoginManager.GetCurrentUser();
            //return user != null && (user.Right.Contains(right));
            bool admin = false;
            if(!string.IsNullOrEmpty(user.Role))
            {
                var arr = user.Role.Split(',');
               if(arr.Length>0)
                {
                    foreach(var i in arr)
                    {
                        if(i=="1")
                        {
                            admin = true;
                            break;
                        }
                    }
                }
            }
            bool rs = user != null && ((admin == true) || (user.Right.Contains(right)));
                return rs;
            
        }
        //public static bool IsInPermissionArea(this IPrincipal a, PermissionArea permissionArea)
        //{
        //    var user = CBODungChung.GetCurrentUser();

        //    return user != null && user.PermissionAreas != null && user.PermissionAreas.ContainsKey(permissionArea.GetValue().ToString());
        //}
        public static int GetValue(this Enum target)
        {
            return System.Convert.ToInt32(target);
        }
        public static string Reverse(this string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static string ToJson(this object obj, Formatting setting = Formatting.None)
        {
            return JsonConvert.SerializeObject(obj, setting);
        }
        
    }
}