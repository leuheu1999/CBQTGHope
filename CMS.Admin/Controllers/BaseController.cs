using CMS.Admin.Common;
using Core.Common.Utilities;
using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

namespace CMS.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }
        public void Dispose()
        {
            base.Dispose();
        }
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class CheckForAntiForgeryTokenAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            HttpCookie authCookie = filterContext.HttpContext.Request.Cookies.Get(AntiForgeryConfig.CookieName);// filterContext.RequestContext.HttpContext..Cookies[AntiForgeryConfig.CookieName];
            HttpCookie authCookieNIGOL = filterContext.HttpContext.Request.Cookies.Get(AntiForgeryConfig.CookieName + "XAJA");
            if (authCookie != null)
            {
                var sessionCookie = HttpContext.Current.Session[AntiForgeryConfig.CookieName + "XAJA"] != null ? HttpContext.Current.Session[AntiForgeryConfig.CookieName + "XAJA"].ToString() : string.Empty;
                if (string.IsNullOrEmpty(sessionCookie) || authCookieNIGOL.Value != sessionCookie)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "NguoiDungHeThong",
                        action = "LogOff"

                    }));
                }
            }
            var _salt = Encrypt_Decrypt.GenerateSalt();
            string _antiForgeryToken = DungChung.GetAntiForgeryToken();
            var _cookieAjax = Encrypt_Decrypt.EncodePassword(_antiForgeryToken, _salt);
            var _antiForgeryConfig = new HttpCookie(AntiForgeryConfig.CookieName, _antiForgeryToken);
            _antiForgeryConfig.HttpOnly = true;
            filterContext.HttpContext.Response.Cookies.Add(_antiForgeryConfig);

            var _antiForgeryConfig_update = new HttpCookie(AntiForgeryConfig.CookieName + "XAJA", _cookieAjax);
            _antiForgeryConfig_update.HttpOnly = true;
            filterContext.HttpContext.Response.Cookies.Add(_antiForgeryConfig_update);
            HttpContext.Current.Session[AntiForgeryConfig.CookieName + "XAJA"] = _cookieAjax;

            base.OnAuthorization(filterContext);
            //AntiForgeryConfig.   
        }
    }
}