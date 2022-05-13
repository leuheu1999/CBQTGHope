using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Module.Framework.Common
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public string RightName { get; set; }        
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var routeData = httpContext.Request.RequestContext.RouteData;
            var controller = routeData.GetRequiredString("controller");
            var action = routeData.GetRequiredString("action");
            var user = System.Web.HttpContext.Current.User;
            if (user.Identity.IsAuthenticated &&!String.IsNullOrEmpty(this.RightName))
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
            if (string.IsNullOrEmpty(this.RightName))
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