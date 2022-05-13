using Business.Entities.Domain;
using Module.Framework.Interfaces;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;

namespace Module.Framework.Common
{
    public static class LoginManager
    {
        #region login
        public static bool IsBlockIP()
        {
            try
            {
                UsersServiceClient _uservice = new UsersServiceClient();
                var ipadress = GetIP4Address();
                var bip = _uservice.BanIP_byIp(ipadress);
                return bip != null && bip.Data != null && bip.Data.resultObject != null && bip.Data.resultObject.Status == 3;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static string GetIP4Address()
        {
            string IP4Address = String.Empty;
            foreach (IPAddress IPA in Dns.GetHostAddresses(System.Web.HttpContext.Current.Request.UserHostAddress))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }
            if (IP4Address != String.Empty)
            {
                return IP4Address;
            }
            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }
            return IP4Address;
        }
        public static string MD5Hash(string text)
        {
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(text);
            string encTicket = authTicket.Name;
            return encTicket;
        }
        public static DateTime? GetTimeBlockIP()
        {
            try
            {
                UsersServiceClient _uservice = new UsersServiceClient();
                var ipadress = GetIP4Address();
                var bip = _uservice.BanIP_byIp(ipadress);
                if (bip != null && bip.Data != null && bip.Data.resultObject != null && bip.Data.resultObject.Status == 2)
                {
                    return bip.Data.resultObject.DateEnd.Value;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        public static void RemoveIPBan()
        {
            try
            {
                UsersServiceClient _uservice = new UsersServiceClient();
                var ipadress = GetIP4Address();
                var bip = _uservice.BanIP_deletebyIp(ipadress);
            }
            catch (Exception ex)
            {
            }
        }
        public static int SaveIPBan(HttpCookieCollection cookies)
        {
            try
            {
                UsersServiceClient _uservice = new UsersServiceClient();
                var ipadress = GetIP4Address();
                var bip = _uservice.BanIP_byIp(ipadress);
                var data = new BanIPMap();
                data.Modified_Date = DateTime.Now;

                if (bip == null || bip.Data == null || bip.Data.resultObject == null)
                {
                    data.IPAdress = ipadress;
                    data.WrongNum = 0;
                    data.Created_Date = DateTime.Now;
                    var kq = _uservice.BanIP_InsUpd(data);
                    if (kq == null || kq.Data == null || kq.Data.resultObject < 0) return 0;
                }
                else
                {
                    data = bip.Data.resultObject;
                }
                if (cookies.AllKeys.Contains("WrongNum"))
                {
                    data.WrongNum = int.Parse(cookies["WrongNum"].Value) + 1;
                }
                else
                {
                    data.WrongNum = bip.Data.resultObject.WrongNum + 1;
                }
                if (data.WrongNum == 3)
                {
                    data.Status = 2;
                    data.DateEnd = DateTime.Now.AddMinutes(15);
                }
                //else if (data.WrongNum == 4)
                //{
                //    data.Status = 3;//khoa, lien he quan tri
                //}
                else
                {
                    data.Status = 1;
                }
                var rs = _uservice.BanIP_InsUpd(data);
                if (rs == null || rs.Data == null || rs.Data.resultObject < 0) return 0;
                return data.WrongNum.Value;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static TimeSpan? GetTime(HttpCookieCollection cookie)
        {
            var ip = GetIP4Address();
            try
            {
                if (cookie.AllKeys.Contains("IsMinute"))
                {
                    var time2 = MD5Hash(cookie["IsMinute"].Value);
                    TimeSpan times = DateTime.Parse(time2) - DateTime.Now;
                    if (times.TotalSeconds > 0)
                    {
                        return times;
                    }
                }
            }
            catch (Exception ex) { }
            UsersServiceClient _uservice = new UsersServiceClient();
            var time = _uservice.BanIP_byIp(ip);
            if (time != null && time.Data != null && time.Data.resultObject != null && time.Data.resultObject.DateEnd.HasValue)
            {
                TimeSpan times = time.Data.resultObject.DateEnd.Value - DateTime.Now;
                if (times.TotalSeconds > 0)
                {
                    return times;
                }
            }
            return null;
        }
        public static CustomPrincipal GetCurrentUser()
        {
            return System.Web.HttpContext.Current.User as CustomPrincipal;
        }
        public static bool CheckAdmin()
        {
            var user = System.Web.HttpContext.Current.User as CustomPrincipal;
            bool admin = false;
            if (user!=null && !string.IsNullOrEmpty(user.Role))
            {
                var arr = user.Role.Split(',');
                if (arr.Length > 0)
                {
                    foreach (var i in arr)
                    {
                        if (i == "1")
                        {
                            admin = true;
                            break;
                        }
                    }
                }
                return admin;
            }
            return false;
        }
        public static string[] GetRolesForUser(string username)// lấy danh sách nhóm quyền by usernam
        {
                UsersServiceClient _UsersSrv = new UsersServiceClient();
                var userRoles = _UsersSrv.GetRolesByUser(username);// lấy danh sách nhóm quyền by usernam
                var result = new string[0];
                if (userRoles != null && userRoles.Data != null && userRoles.Data.resultObject.Count > 0)
                {
                    result = userRoles.Data.resultObject.Select(item => item.Id.ToString().ToUpper()).ToArray();
                }
                return result;
            }

        public static UrlViewModel GetDefaultUrlFromPermission()
        {
            UsersServiceClient _UsersSrv = new UsersServiceClient();
            var user = LoginManager.GetCurrentUser();
            var rights = user.Right.Split(',');
            UrlViewModel urlViewModel = new UrlViewModel();
            urlViewModel.Action = user.DefaultAction;
            urlViewModel.Controller = user.DefaultController;
            //if (rights.Any())
            //{
            //    //dtt009873
            //    string strR = "";
            //    foreach (var i in rights)
            //    {
            //        strR += i + "|";
            //    }
            //    var r = rights.Select(Guid.Parse);
            //    var temp = _UsersSrv.GetUrlViewModel(strR);
            //    if (temp != null && temp.Data != null && temp.Data.resultObject != null)
            //    {
            //        urlViewModel = temp.Data.resultObject;
            //    }
            //}
            return urlViewModel;
        }
        public static string getvalue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
        #endregion

    }
}