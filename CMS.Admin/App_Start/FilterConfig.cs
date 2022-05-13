using Core.Common.Utilities;
using Module.Framework.Common;
using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace CMS.Admin
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private string GetAntiForgeryToken()
        {
            HttpContext.Current.Request.Cookies.Clear();
            var antiForgery = System.Web.Helpers.AntiForgery.GetHtml().ToString();
            System.Text.RegularExpressions.Match value = System.Text.RegularExpressions.Regex.Match(antiForgery, "(?:value=\")(.*)(?:\")");
            if (value.Success)
            {
                return value.Groups[1].Value;
            }
            return "";
        }
        public string RightName { get; set; }

        public bool IsDelete { get; set; } = false;
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var routeData = httpContext.Request.RequestContext.RouteData;
            var controller = routeData.GetRequiredString("controller");
            var action = routeData.GetRequiredString("action");
            var user = System.Web.HttpContext.Current.User;

            // [NEED_TO_TRANS]Check Ajax error
            string keyLogin = string.Empty;
            HttpCookie authCookieNIGOL1 = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName + "NIGOL"];
            var sessionCookie1 = HttpContext.Current.Session != null && HttpContext.Current.Session[FormsAuthentication.FormsCookieName + "NIGOL"] != null ? HttpContext.Current.Session[FormsAuthentication.FormsCookieName + "NIGOL"].ToString() : string.Empty;
            if ((string.IsNullOrEmpty(sessionCookie1) || authCookieNIGOL1.Value != sessionCookie1))
            {
                return false;
            }
            if (this.IsDelete)
            {
                HttpCookie authCookie = httpContext.Request.Cookies.Get(AntiForgeryConfig.CookieName);// filterContext.RequestContext.HttpContext..Cookies[AntiForgeryConfig.CookieName];
                HttpCookie authCookieNIGOL = httpContext.Request.Cookies.Get(AntiForgeryConfig.CookieName + "XAJA");
                var sessionCookie = HttpContext.Current.Session[AntiForgeryConfig.CookieName + "XAJA"] != null ? HttpContext.Current.Session[AntiForgeryConfig.CookieName + "XAJA"].ToString() : string.Empty;
                if (authCookie != null)
                {
                    if (string.IsNullOrEmpty(sessionCookie) || authCookieNIGOL.Value != sessionCookie)
                    {
                        var _salt = Encrypt_Decrypt.GenerateSalt();
                        string _antiForgeryToken = GetAntiForgeryToken();
                        var _cookieAjax = Encrypt_Decrypt.EncodePassword(_antiForgeryToken, _salt);
                        var _antiForgeryConfig = new HttpCookie(AntiForgeryConfig.CookieName, _antiForgeryToken);
                        _antiForgeryConfig.HttpOnly = true;
                        httpContext.Response.Cookies.Add(_antiForgeryConfig);

                        var _antiForgeryConfig_update = new HttpCookie(AntiForgeryConfig.CookieName + "XAJA", _cookieAjax);
                        _antiForgeryConfig_update.HttpOnly = true;
                        httpContext.Response.Cookies.Add(_antiForgeryConfig_update);
                        HttpContext.Current.Session[AntiForgeryConfig.CookieName + "XAJA"] = _cookieAjax;
                        return false;
                    }
                    else
                    {
                        var salt = Encrypt_Decrypt.GenerateSalt();
                        string antiForgeryToken = GetAntiForgeryToken();
                        var cookieAjax = Encrypt_Decrypt.EncodePassword(antiForgeryToken, salt);
                        var antiForgeryConfig = new HttpCookie(AntiForgeryConfig.CookieName, antiForgeryToken);
                        antiForgeryConfig.HttpOnly = true;
                        httpContext.Response.Cookies.Add(antiForgeryConfig);

                        var antiForgeryConfig_update = new HttpCookie(AntiForgeryConfig.CookieName + "XAJA", cookieAjax);
                        antiForgeryConfig_update.HttpOnly = true;
                        httpContext.Response.Cookies.Add(antiForgeryConfig_update);
                        HttpContext.Current.Session[AntiForgeryConfig.CookieName + "XAJA"] = cookieAjax;
                        if (user.Identity.IsAuthenticated && !String.IsNullOrEmpty(this.RightName))
                        {
                            return user.IsInRight(this.RightName);
                        }
                    }
                }
            }


            if (user.Identity.IsAuthenticated && !String.IsNullOrEmpty(this.RightName))
            {
                return user.IsInRight(this.RightName);
            }

            if (HttpContext.Current.Session["IsExpired"] == null && controller != "Account")
            {
                //return false;
            }
            return base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var context = filterContext.HttpContext;
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            string url = null;
            var user = System.Web.HttpContext.Current.User;

            if (string.IsNullOrEmpty(this.RightName) || (this.IsDelete == true))
            {
                if (user.Identity.IsAuthenticated)
                {
                    url = urlHelper.Action("LogOff", "NguoiDungHeThong");
                }
                else
                {
                    url = urlHelper.Action("Login", "NguoiDungHeThong");
                }

            }
            else
            {
                url = urlHelper.Action("Permission", "Error");
            }

            // [NEED_TO_TRANS]Check Ajax error
            if (context.Request.IsAjaxRequest())
            {
                //var result = new JavaScriptResult();
                //result.Script = string.Format("Common.RedirectLoginUrl()");
                //filterContext.Result = result;
                filterContext.HttpContext.Response.StatusCode = 401;
                //filterContext.HttpContext.Response.End();
                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        Error = "NotAuthorized",
                        LogOnUrl = url
                    },
                    //JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                //filterContext.HttpContext.Response.End();
            }
            else
            {
                filterContext.Result = new RedirectResult(url);
            }
        }
    }
}
