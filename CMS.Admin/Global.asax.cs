using Module.Framework.Interfaces;
using System;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace CMS.Admin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            HttpCookie authCookieNIGOL = Request.Cookies[FormsAuthentication.FormsCookieName + "NIGOL"];
            var target = HttpContext.Current.User as RolePrincipal;
            var currentContext = new HttpContextWrapper(HttpContext.Current);
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var routeData = urlHelper.RouteCollection.GetRouteData(currentContext);
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                var serializer = new JavaScriptSerializer();
                if (authTicket != null && authTicket.UserData == "OAuth") return;
                var serializeModel = serializer.Deserialize<CustomPrincipalSerializedModel>(authTicket.UserData);
                if (serializeModel != null)
                {
                    var newUser = new CustomPrincipal(authTicket.Name)
                    {
                        UserId = serializeModel.UserId,
                        UserName = serializeModel.UserName,
                        UserFullName = serializeModel.UserFullName,
                        Right = serializeModel.Right,
                        Role = serializeModel.Role,
                        DefaultAction = serializeModel.DefaultAction,
                        DefaultController = serializeModel.DefaultController,
                        NgonNguID = serializeModel.NgonNguID,
                        DonViID = serializeModel.DonViID,
                        PhongBanID = serializeModel.PhongBanID,
                        UserPortalID = serializeModel.UserPortalID,
                    };
                    HttpContext.Current.User = newUser;
                }
                // Set the secure flag, which Chrome's changes will require for Same
                authCookie.Secure = true;
                // Set the cookie to HTTP only which is good practice unless you really do need
                // to access it client side in scripts.
                authCookie.HttpOnly = true;
                // Add the SameSite attribute, this will emit the attribute with a value of none.
                // To not emit the attribute at all set the SameSite property to -1.
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var error = Server.GetLastError();
            var cryptoEx = error as CryptographicException;
            if (cryptoEx != null)
            {
                FormsAuthentication.SignOut();
                Server.ClearError();
            }
        }
    }
}
