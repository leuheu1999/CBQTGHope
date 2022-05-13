using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Data.Core.Repositories.Base
{
    /// <summary>
    /// Represents a common helper
    /// </summary>
    public partial class WebHelper : IWebHelper
    {
        #region Fields 

        private readonly HttpContextBase _httpContext;
        private readonly string[] _staticFileExtensions;

        #endregion

        #region Constructor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="httpContext">HTTP context</param>
        public WebHelper(HttpContextBase httpContext)
        {
            this._httpContext = httpContext;
            this._staticFileExtensions = new[] { ".axd", ".ashx", ".bmp", ".css", ".gif", ".htm", ".html", ".ico", ".jpeg", ".jpg", ".js", ".png", ".rar", ".zip" };
        }
        
        #endregion
        public virtual string GetCurrentIpAddress()
        {
            if (!IsRequestAvailable(_httpContext))
                return string.Empty;

            var result = "";
            try
            {
                if (_httpContext.Request.Headers != null)
                {
                    //The X-Forwarded-For (XFF) HTTP header field is a de facto standard
                    //for identifying the originating IP address of a client
                    //connecting to a web server through an HTTP proxy or load balancer.
                    var forwardedHttpHeader = "X-FORWARDED-FOR";
                    if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["ForwardedHTTPheader"]))
                    {
                        //but in some cases server use other HTTP header
                        //in these cases an administrator can specify a custom Forwarded HTTP header
                        //e.g. CF-Connecting-IP, X-FORWARDED-PROTO, etc
                        forwardedHttpHeader = ConfigurationManager.AppSettings["ForwardedHTTPheader"];
                    }

                    //it's used for identifying the originating IP address of a client connecting to a web server
                    //through an HTTP proxy or load balancer. 
                    string xff = _httpContext.Request.Headers.AllKeys
                        .Where(x => forwardedHttpHeader.Equals(x, StringComparison.InvariantCultureIgnoreCase))
                        .Select(k => _httpContext.Request.Headers[k])
                        .FirstOrDefault();

                    //if you want to exclude private IP addresses, then see http://stackoverflow.com/questions/2577496/how-can-i-get-the-clients-ip-address-in-asp-net-mvc
                    if (!String.IsNullOrEmpty(xff))
                    {
                        string lastIp = xff.Split(new[] { ',' }).FirstOrDefault();
                        result = lastIp;
                    }
                }

                if (String.IsNullOrEmpty(result) && _httpContext.Request.UserHostAddress != null)
                {
                    result = _httpContext.Request.UserHostAddress;
                }
            }
            catch
            {
                return result;
            }

            //some validation
            if (result == "::1")
                result = "127.0.0.1";
            //remove port
            if (!String.IsNullOrEmpty(result))
            {
                int index = result.IndexOf(":", StringComparison.InvariantCultureIgnoreCase);
                if (index > 0)
                    result = result.Substring(0, index);
            }
            return result;

        }
        protected virtual Boolean IsRequestAvailable(HttpContextBase httpContext)
        {
            if (httpContext == null)
                return false;

            try
            {
                if (httpContext.Request == null)
                    return false;
            }
            catch (HttpException)
            {
                return false;
            }

            return true;
        }
        public virtual string GetThisPageUrl(bool includeQueryString)
        {
            bool useSsl = IsCurrentConnectionSecured();
            return GetThisPageUrl(includeQueryString, useSsl);
        }
        public virtual string GetThisPageUrl(bool includeQueryString, bool useSsl)
        {
            if (!IsRequestAvailable(_httpContext))
                return string.Empty;

            //get the host considering using SSL
            var url = GetStoreHost(useSsl).TrimEnd('/');

            //get full URL with or without query string
            url += includeQueryString ? _httpContext.Request.RawUrl : _httpContext.Request.Path;

            return url.ToLowerInvariant();
        }
        public virtual string GetUrlReferrer()
        {
            string referrerUrl = string.Empty;

            //URL referrer is null in some case (for example, in IE 8)
            if (IsRequestAvailable(_httpContext) && _httpContext.Request.UrlReferrer != null)
                referrerUrl = _httpContext.Request.UrlReferrer.PathAndQuery;

            return referrerUrl;
        }
        public virtual bool IsCurrentConnectionSecured()
        {
            bool useSsl = false;
            if (IsRequestAvailable(_httpContext))
            {
                //when your hosting uses a load balancer on their server then the Request.IsSecureConnection is never got set to true

                //1. use HTTP_CLUSTER_HTTPS?
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Use_HTTP_CLUSTER_HTTPS"]) &&
                   Convert.ToBoolean(ConfigurationManager.AppSettings["Use_HTTP_CLUSTER_HTTPS"]))
                {
                    useSsl = ServerVariables("HTTP_CLUSTER_HTTPS") == "on";
                }
                //2. use HTTP_X_FORWARDED_PROTO?
                else if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Use_HTTP_X_FORWARDED_PROTO"]) &&
                   Convert.ToBoolean(ConfigurationManager.AppSettings["Use_HTTP_X_FORWARDED_PROTO"]))
                {
                    useSsl = string.Equals(ServerVariables("HTTP_X_FORWARDED_PROTO"), "https", StringComparison.OrdinalIgnoreCase);
                }
                else
                {
                    useSsl = _httpContext.Request.IsSecureConnection;
                }
            }

            return useSsl;
        }
        public virtual string GetStoreHost(bool useSsl)
        {
            var result = "";
            var httpHost = ServerVariables("HTTP_HOST");
            if (!String.IsNullOrEmpty(httpHost))
            {
                result = "http://" + httpHost;
                if (!result.EndsWith("/"))
                    result += "/";
            }
                #region Database is not installed
                if (useSsl)
                {
                    //Secure URL is not specified.
                    //So a store owner wants it to be detected automatically.
                    result = result.Replace("http:/", "https:/");
                }
                #endregion
           
            if (!result.EndsWith("/"))
                result += "/";
            return result.ToLowerInvariant();
        }
        public virtual string ServerVariables(string name)
        {
            string result = string.Empty;

            try
            {
                if (!IsRequestAvailable(_httpContext))
                    return result;

                //put this method is try-catch 
                //as described here http://www.nopcommerce.com/boards/t/21356/multi-store-roadmap-lets-discuss-update-done.aspx?p=6#90196
                if (_httpContext.Request.ServerVariables[name] != null)
                {
                    result = _httpContext.Request.ServerVariables[name];
                }
            }
            catch
            {
                result = string.Empty;
            }
            return result;
        }


    }
}
