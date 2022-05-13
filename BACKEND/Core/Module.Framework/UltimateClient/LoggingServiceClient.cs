using System;
using Core.Common.Utilities;
using RestSharp;
using Newtonsoft.Json;
using Module.Framework.UltimateClient;
using Business.Entities.Domain;
using System.Collections.Generic;

namespace Module.Framework
{
    public class LoggingServiceClient : BaseClient, IDisposable
    {
        private bool _isDisposed;
        public LoggingServiceClient() : base(AppSetting.Log)
        {
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!_isDisposed && disposing)
            {
                DisposeCore();
            }

            _isDisposed = true;
        }

        protected virtual void DisposeCore()
        {
        }
        public void LogError(LogAddView model)
        {    
            
            var request = new RestRequest("DC/LogError", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);
            var restResponse = Execute<ResultResponse<long>>(request);

        }
        public void LogInformation(LogAddView model)
        {
            var request = new RestRequest("DC/LogInformation", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);
            var restResponse = Execute<ResultResponse<long>>(request);

        }
        public void LogDebug(LogAddView model)
        {
            var request = new RestRequest("DC/LogDebug", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);
            var restResponse = Execute<ResultResponse<long>>(request);

        }
        public void LogWarning(LogAddView model)
        {
            var request = new RestRequest("DC/LogWarning", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);
            var restResponse = Execute<ResultResponse<long>>(request);

        }
        public void LogFatal(LogAddView model)
        {
            var request = new RestRequest("DC/LogFatal", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);
            var restResponse = Execute<ResultResponse<long>>(request);

        }
        public IRestResponse<ResultResponse<List<LogMap>>> LogSystem_List(LogParam model)
        {
            var request = new RestRequest("DC/LogSystem_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<LogMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<LogAdd>> GetLogSystemById(long id)
        {
            var request = new RestRequest("DC/GetLogSystemById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<LogAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<int>> LogSystem_DelByID(long id)
        {
            var request = new RestRequest("DC/LogSystem_DelByID", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<int>> LogSystem_DelLstID(List<long> ids)
        {
            var request = new RestRequest("DC/LogSystem_DelLstID", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(ids, settings);
            request.AddParameter("application/json", json, null, ParameterType.RequestBody);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<int>> LogSystem_Trancate()
        {
            var request = new RestRequest("DC/LogSystem_Trancate", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }

    }
}
