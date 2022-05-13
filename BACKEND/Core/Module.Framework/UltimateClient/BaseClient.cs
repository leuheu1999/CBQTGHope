using Module.Framework.Common;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Linq;
using System.Web;

namespace Module.Framework.UltimateClient
{
    public class BaseClient : RestClient
    {
        private readonly ICacheService _cache;
        private readonly IErrorLogger _errorLogger;

        protected BaseClient(string baseUrl)
        {
            _cache = new InMemoryCache();
            _errorLogger = new ErrorLogger();
            IDeserializer serializer = new JsonSerializer();
            AddHandler("application/json", serializer);
            AddHandler("text/json", serializer);
            AddHandler("text/x-json", serializer);
            BaseUrl = new Uri(baseUrl);
        }

        private void TimeoutCheck(IRestRequest request, IRestResponse response)
        {
            if (response.StatusCode == 0)
            {
                LogError(BaseUrl, request, response);
            }
        }

        public override IRestResponse Execute(IRestRequest request)
        {
            var response = base.Execute(request);
            TimeoutCheck(request, response);
            return response;
        }

        public override IRestResponse<T> Execute<T>(IRestRequest request)
        {
            System.Web.HttpCookie language = HttpContext.Current.Request.Cookies.Get("culture");
            System.Web.HttpCookie wardName = HttpContext.Current.Request.Cookies.Get("WardName");

            var us = LoginManager.GetCurrentUser();
            if (us != null)
            {
                request.AddHeader("UserToken", us.UserId);
                request.AddHeader("LanguageId", us.NgonNguID.ToString());
            }
            if (language != null)
            {
                request.AddHeader("UniqueSeoCode", language.Value);
            }
            if (wardName != null)
            {
                request.AddHeader("WardName", wardName.Value);
            }
            var response = base.Execute<T>(request);
            TimeoutCheck(request, response);
            return response;
        }

        public T Get<T>(IRestRequest request) where T : new()
        {
            var response = Execute<T>(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Data;
            }
            else
            {
                LogError(BaseUrl, request, response);
                return default(T);
            }
        }

        public T GetFromCache<T>(IRestRequest request, string cacheKey) where T : class, new()
        {
            var item = _cache.Get<T>(cacheKey);
            //if (item == null)
            //{
            var response = Execute<T>(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                _cache.Set(cacheKey, response.Data, 30);
                item = response.Data;
            }
            else
            {
                LogError(BaseUrl, request, response);
                return default(T);
            }
            //}
            return item;
        }
        public void DisposeCache(string cacheKey)
        {
            _cache.Set(cacheKey, null, 1);
        }
        public void DisposeAllCache(string cacheKey)
        {
            _cache.Dispose(cacheKey);
        }

        private void LogError(Uri baseUrl, IRestRequest request, IRestResponse response)
        {
            //Get the values of the parameters passed to the API
            string parameters = string.Join(", ", request.Parameters.Select(x => x.Name.ToString() + "=" + (x.Value ?? "NULL")).ToArray());

            //Set up the information message with the URL, the status code, and the parameters.
            string info = "Request to " + baseUrl.AbsoluteUri + request.Resource + " failed with status code " + response.StatusCode + ", parameters: "
            + parameters + ", and content: " + response.Content;

            //Acquire the actual exception
            Exception ex;
            if (response.ErrorException != null)
            {
                ex = response.ErrorException;
            }
            else
            {
                ex = new Exception(info);
                info = string.Empty;
            }

            //Log the exception and info message
            _errorLogger.LogError(ex, info);
        }

    }
}
