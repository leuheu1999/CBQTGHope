using Business.Entities.Domain;
using Core.Common.Utilities;
using Module.Framework.UltimateClient;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Module.Framework
{
    public class DVC_QuyenTacGiaServiceClient : BaseClient, IDisposable
    {
        private bool _isDisposed;
        public DVC_QuyenTacGiaServiceClient() : base(AppSetting.DVC_QuyenTacGia)
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

        #region DVC_QTG_QuyenTacGia
        public IRestResponse<ResultResponse<List<DVC_QTG_QuyenTacGiaMap>>> DVC_QTG_QuyenTacGia_List(DVC_QTG_QuyenTacGiaParam model)
        {
            var request = new RestRequest("DVC/DVC_QTG_QuyenTacGia_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<DVC_QTG_QuyenTacGiaMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<DVC_QTG_QuyenTacGiaAdd>> DVC_QTG_QuyenTacGia_ById(long id)
        {
            var request = new RestRequest("DVC/DVC_QTG_QuyenTacGia_ById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<DVC_QTG_QuyenTacGiaAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> DVC_QTG_QuyenTacGia_GetStt()
        {
            var request = new RestRequest("DVC/DVC_QTG_QuyenTacGia_GetStt", Method.GET);
            var restResponse = Execute<ResultResponse<long>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> DVC_QTG_QuyenTacGia_InsUpd(DVC_QTG_QuyenTacGiaAdd model)
        {
            var request = new RestRequest("DVC/DVC_QTG_QuyenTacGia_InsUpd", Method.POST)
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
        public IRestResponse<ResultResponse<int>> DVC_QTG_QuyenTacGia_Del(long quyenTacGiaID, Guid userID)
        {
            var request = new RestRequest("DVC/DVC_QTG_QuyenTacGia_Del", Method.GET);
            request.AddParameter("quyenTacGiaID", quyenTacGiaID);
            request.AddParameter("userID", userID);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<List<DVC_QTG_GiayChungNhanMap>>> DVC_QTG_QuyenTacGia_SearchGCN(DVC_QTG_GiayChungNhanParam model)
        {
            var request = new RestRequest("DVC/DVC_QTG_QuyenTacGia_SearchGCN", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<DVC_QTG_GiayChungNhanMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> DVC_QTG_QuyenTacGia_CapSo(DVC_QTG_QuyenTacGia_CapSo model)
        {
            var request = new RestRequest("DVC/DVC_QTG_QuyenTacGia_CapSo", Method.POST)
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
        public IRestResponse<ResultResponse<int>> DVC_QTG_QuyenTacGia_CheckSoTT(long id, string soTT)
        {
            var request = new RestRequest("DVC/DVC_QTG_QuyenTacGia_CheckSoTT", Method.GET);
            request.AddParameter("id", id);
            request.AddParameter("soTT", soTT);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<int>> DVC_QTG_QuyenTacGia_CheckSoGCN(long id, string soGCN)
        {
            var request = new RestRequest("DVC/DVC_QTG_QuyenTacGia_CheckSoGCN", Method.GET);
            request.AddParameter("id", id);
            request.AddParameter("soGCN", soGCN);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        #endregion DVC_QTG_QuyenTacGia
    }
}
