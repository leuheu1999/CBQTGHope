using Business.Entities.Domain;
using Core.Common.Utilities;
using Module.Framework.UltimateClient;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Module.Framework
{
    public class DVC_QuyenLienQuanServiceClient : BaseClient, IDisposable
    {
        private bool _isDisposed;
        public DVC_QuyenLienQuanServiceClient() : base(AppSetting.DVC_QuyenLienQuan)
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

        #region DVC_QLQ_QuyenLienQuan
        public IRestResponse<ResultResponse<List<DVC_QLQ_QuyenLienQuanMap>>> DVC_QLQ_QuyenLienQuan_List(DVC_QLQ_QuyenLienQuanParam model)
        {
            var request = new RestRequest("DVC/DVC_QLQ_QuyenLienQuan_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<DVC_QLQ_QuyenLienQuanMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<DVC_QLQ_QuyenLienQuanAdd>> DVC_QLQ_QuyenLienQuan_ById(long id)
        {
            var request = new RestRequest("DVC/DVC_QLQ_QuyenLienQuan_ById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<DVC_QLQ_QuyenLienQuanAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> DVC_QLQ_QuyenLienQuan_GetStt()
        {
            var request = new RestRequest("DVC/DVC_QLQ_QuyenLienQuan_GetStt", Method.GET);
            var restResponse = Execute<ResultResponse<long>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> DVC_QLQ_QuyenLienQuan_InsUpd(DVC_QLQ_QuyenLienQuanAdd model)
        {
            var request = new RestRequest("DVC/DVC_QLQ_QuyenLienQuan_InsUpd", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<long>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<int>> DVC_QLQ_QuyenLienQuan_Del(long quyenLienQuanID, Guid userID)
        {
            var request = new RestRequest("DVC/DVC_QLQ_QuyenLienQuan_Del", Method.GET);
            request.AddParameter("quyenLienQuanID", quyenLienQuanID);
            request.AddParameter("userID", userID);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<List<DVC_QLQ_GiayChungNhanMap>>> DVC_QLQ_QuyenLienQuan_SearchGCN(DVC_QLQ_GiayChungNhanParam model)
        {
            var request = new RestRequest("DVC/DVC_QLQ_QuyenLienQuan_SearchGCN", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<DVC_QLQ_GiayChungNhanMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> DVC_QLQ_QuyenLienQuan_CapSo(DVC_QLQ_QuyenLienQuan_CapSo model)
        {
            var request = new RestRequest("DVC/DVC_QLQ_QuyenLienQuan_CapSo", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<long>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<int>> DVC_QLQ_QuyenLienQuan_CheckSoTT(long id, string soTT)
        {
            var request = new RestRequest("DVC/DVC_QLQ_QuyenLienQuan_CheckSoTT", Method.GET);
            request.AddParameter("id", id);
            request.AddParameter("soTT", soTT);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<int>> DVC_QLQ_QuyenLienQuan_CheckSoGCN(long id, string soGCN)
        {
            var request = new RestRequest("DVC/DVC_QLQ_QuyenLienQuan_CheckSoGCN", Method.GET);
            request.AddParameter("id", id);
            request.AddParameter("soGCN", soGCN);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        #endregion DVC_QLQ_QuyenLienQuan
    }
}
