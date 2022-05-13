using Business.Entities.Domain;
using Core.Common.Utilities;
using Module.Framework.UltimateClient;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Module.Framework
{
    public class QuyenLienQuanServiceClient : BaseClient, IDisposable
    {
        private bool _isDisposed;
        public QuyenLienQuanServiceClient() : base(AppSetting.QuyenLienQuan)
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

        #region QLQ_QuyenLienQuan
        public IRestResponse<ResultResponse<List<QLQ_QuyenLienQuanMap>>> QLQ_QuyenLienQuan_List(QLQ_QuyenLienQuanParam model)
        {
            var request = new RestRequest("QLQ/QLQ_QuyenLienQuan_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<QLQ_QuyenLienQuanMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<QLQ_QuyenLienQuanAdd>> QLQ_QuyenLienQuan_ById(long id)
        {
            var request = new RestRequest("QLQ/QLQ_QuyenLienQuan_ById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<QLQ_QuyenLienQuanAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> QLQ_QuyenLienQuan_GetStt()
        {
            var request = new RestRequest("QLQ/QLQ_QuyenLienQuan_GetStt", Method.GET);
            var restResponse = Execute<ResultResponse<long>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> QLQ_QuyenLienQuan_InsUpd(QLQ_QuyenLienQuanAdd model)
        {
            var request = new RestRequest("QLQ/QLQ_QuyenLienQuan_InsUpd", Method.POST)
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
        public IRestResponse<ResultResponse<int>> QLQ_QuyenLienQuan_Del(long quyenLienQuanID, Guid userID)
        {
            var request = new RestRequest("QLQ/QLQ_QuyenLienQuan_Del", Method.GET);
            request.AddParameter("quyenLienQuanID", quyenLienQuanID);
            request.AddParameter("userID", userID);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<List<QLQ_GiayChungNhanMap>>> QLQ_QuyenLienQuan_SearchGCN(QLQ_GiayChungNhanParam model)
        {
            var request = new RestRequest("QLQ/QLQ_QuyenLienQuan_SearchGCN", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<QLQ_GiayChungNhanMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<List<QLQ_SoThuTuMap>>> QLQ_QuyenLienQuan_SearchSTT(QLQ_SoThuTuParam model)
        {
            var request = new RestRequest("QLQ/QLQ_QuyenLienQuan_SearchSTT", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<QLQ_SoThuTuMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> QLQ_QuyenLienQuan_CapSo(QLQ_QuyenLienQuan_CapSo model)
        {
            var request = new RestRequest("QLQ/QLQ_QuyenLienQuan_CapSo", Method.POST)
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
        public IRestResponse<ResultResponse<int>> QLQ_QuyenLienQuan_CheckSoTT(long id, string soTT)
        {
            var request = new RestRequest("QLQ/QLQ_QuyenLienQuan_CheckSoTT", Method.GET);
            request.AddParameter("id", id);
            request.AddParameter("soTT", soTT);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<int>> QLQ_QuyenLienQuan_CheckSoGCN(long id, string soGCN)
        {
            var request = new RestRequest("QLQ/QLQ_QuyenLienQuan_CheckSoGCN", Method.GET);
            request.AddParameter("id", id);
            request.AddParameter("soGCN", soGCN);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<int>> QLQ_QuyenLienQuan_Duyet(long id, int loaiNghiepVuID, bool isDuyet)
        {
            var request = new RestRequest("QLQ/QLQ_QuyenLienQuan_Duyet", Method.GET);
            request.AddParameter("id", id);
            request.AddParameter("LoaiNghiepVuID", loaiNghiepVuID);
            request.AddParameter("IsDuyet", isDuyet);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        #endregion QLQ_QuyenLienQuan

        #region QLQ_CapLai
        public IRestResponse<ResultResponse<QLQ_CapLaiAdd>> QLQ_CapLai_ById(long id)
        {
            var request = new RestRequest("QLQ/QLQ_CapLai_ById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<QLQ_CapLaiAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<QLQ_CapLaiAdd>> QLQ_CapLai_ByHoSoId(long id)
        {
            var request = new RestRequest("QLQ/QLQ_CapLai_ByHoSoId", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<QLQ_CapLaiAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<QLQ_CapLaiAdd>> QLQ_CapLai_ByQuyenLQId(long id)
        {
            var request = new RestRequest("QLQ/QLQ_CapLai_ByQuyenLQId", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<QLQ_CapLaiAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> QLQ_CapLai_InsUpd(QLQ_CapLaiAdd model)
        {
            var request = new RestRequest("QLQ/QLQ_CapLai_InsUpd", Method.POST)
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
        #endregion QLQ_CapLai

        #region QLQ_ThuHoi
        public IRestResponse<ResultResponse<QLQ_ThuHoiAdd>> QLQ_ThuHoi_ById(long id)
        {
            var request = new RestRequest("QLQ/QLQ_ThuHoi_ById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<QLQ_ThuHoiAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<QLQ_ThuHoiAdd>> QLQ_ThuHoi_ByHoSoId(long id)
        {
            var request = new RestRequest("QLQ/QLQ_ThuHoi_ByHoSoId", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<QLQ_ThuHoiAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<QLQ_ThuHoiAdd>> QLQ_ThuHoi_ByQuyenLQId(long id)
        {
            var request = new RestRequest("QLQ/QLQ_ThuHoi_ByQuyenLQId", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<QLQ_ThuHoiAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> QLQ_ThuHoi_InsUpd(QLQ_ThuHoiAdd model)
        {
            var request = new RestRequest("QLQ/QLQ_ThuHoi_InsUpd", Method.POST)
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
        #endregion QLQ_ThuHoi

        #region QLQ_ChuyenChuSoHuu
        public IRestResponse<ResultResponse<QLQ_ChuyenChuSoHuuAdd>> QLQ_ChuyenChuSoHuu_ById(long id)
        {
            var request = new RestRequest("QLQ/QLQ_ChuyenChuSoHuu_ById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<QLQ_ChuyenChuSoHuuAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<QLQ_ChuyenChuSoHuuAdd>> QLQ_ChuyenChuSoHuu_ByHoSoId(long id)
        {
            var request = new RestRequest("QLQ/QLQ_ChuyenChuSoHuu_ByHoSoId", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<QLQ_ChuyenChuSoHuuAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<QLQ_ChuyenChuSoHuuAdd>> QLQ_ChuyenChuSoHuu_ByQuyenLQId(long id)
        {
            var request = new RestRequest("QLQ/QLQ_ChuyenChuSoHuu_ByQuyenLQId", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<QLQ_ChuyenChuSoHuuAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> QLQ_ChuyenChuSoHuu_InsUpd(QLQ_ChuyenChuSoHuuAdd model)
        {
            var request = new RestRequest("QLQ/QLQ_ChuyenChuSoHuu_InsUpd", Method.POST)
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
        #endregion QLQ_ChuyenChuSoHuu

        #region QLQ_CapDoi
        public IRestResponse<ResultResponse<QLQ_CapDoiAdd>> QLQ_CapDoi_ById(long id)
        {
            var request = new RestRequest("QLQ/QLQ_CapDoi_ById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<QLQ_CapDoiAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<QLQ_CapDoiAdd>> QLQ_CapDoi_ByHoSoId(long id)
        {
            var request = new RestRequest("QLQ/QLQ_CapDoi_ByHoSoId", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<QLQ_CapDoiAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<QLQ_CapDoiAdd>> QLQ_CapDoi_ByQuyenLQId(long id)
        {
            var request = new RestRequest("QLQ/QLQ_CapDoi_ByQuyenLQId", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<QLQ_CapDoiAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> QLQ_CapDoi_InsUpd(QLQ_CapDoiAdd model)
        {
            var request = new RestRequest("QLQ/QLQ_CapDoi_InsUpd", Method.POST)
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
        #endregion QLQ_CapDoi
    }
}
