using Business.Entities.Domain;
using Core.Common.Utilities;
using Module.Framework.UltimateClient;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Module.Framework
{
    public class QuyenTacGiaServiceClient : BaseClient, IDisposable
    {
        private bool _isDisposed;
        public QuyenTacGiaServiceClient() : base(AppSetting.QuyenTacGia)
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

        #region QTG_QuyenTacGia
        public IRestResponse<ResultResponse<List<QTG_QuyenTacGiaMap>>> QTG_QuyenTacGia_List(QTG_QuyenTacGiaParam model)
        {
            var request = new RestRequest("QTG/QTG_QuyenTacGia_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<QTG_QuyenTacGiaMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<QTG_QuyenTacGiaAdd>> QTG_QuyenTacGia_ById(long id)
        {
            var request = new RestRequest("QTG/QTG_QuyenTacGia_ById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<QTG_QuyenTacGiaAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> QTG_QuyenTacGia_GetStt()
        {
            var request = new RestRequest("QTG/QTG_QuyenTacGia_GetStt", Method.GET);
            var restResponse = Execute<ResultResponse<long>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> QTG_QuyenTacGia_InsUpd(QTG_QuyenTacGiaAdd model)
        {
            var request = new RestRequest("QTG/QTG_QuyenTacGia_InsUpd", Method.POST)
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
        public IRestResponse<ResultResponse<int>> QTG_QuyenTacGia_Del(long quyenTacGiaID, Guid userID)
        {
            var request = new RestRequest("QTG/QTG_QuyenTacGia_Del", Method.GET);
            request.AddParameter("quyenTacGiaID", quyenTacGiaID);
            request.AddParameter("userID", userID);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<List<QTG_GiayChungNhanMap>>> QTG_QuyenTacGia_SearchGCN(QTG_GiayChungNhanParam model)
        {
            var request = new RestRequest("QTG/QTG_QuyenTacGia_SearchGCN", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<QTG_GiayChungNhanMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<List<QTG_SoThuTuMap>>> QTG_QuyenTacGia_SearchSTT(QTG_SoThuTuParam model)
        {
            var request = new RestRequest("QTG/QTG_QuyenTacGia_SearchSTT", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<QTG_SoThuTuMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> QTG_QuyenTacGia_CapSo(QTG_QuyenTacGia_CapSo model)
        {
            var request = new RestRequest("QTG/QTG_QuyenTacGia_CapSo", Method.POST)
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
        public IRestResponse<ResultResponse<int>> QTG_QuyenTacGia_CheckSoTT(long id, string soTT)
        {
            var request = new RestRequest("QTG/QTG_QuyenTacGia_CheckSoTT", Method.GET);
            request.AddParameter("id", id);
            request.AddParameter("soTT", soTT);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<int>> QTG_QuyenTacGia_CheckSoGCN(long id, string soGCN)
        {
            var request = new RestRequest("QTG/QTG_QuyenTacGia_CheckSoGCN", Method.GET);
            request.AddParameter("id", id);
            request.AddParameter("soGCN", soGCN);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<int>> QTG_QuyenTacGia_Duyet(long id, int loaiNghiepVuID, bool isDuyet)
        {
            var request = new RestRequest("QTG/QTG_QuyenTacGia_Duyet", Method.GET);
            request.AddParameter("id", id);
            request.AddParameter("LoaiNghiepVuID", loaiNghiepVuID);
            request.AddParameter("IsDuyet", isDuyet);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        #endregion QTG_QuyenTacGia

        #region QTG_CapLai
        public IRestResponse<ResultResponse<QTG_CapLaiAdd>> QTG_CapLai_ById(long id)
        {
            var request = new RestRequest("QTG/QTG_CapLai_ById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<QTG_CapLaiAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<QTG_CapLaiAdd>> QTG_CapLai_ByHoSoId(long id)
        {
            var request = new RestRequest("QTG/QTG_CapLai_ByHoSoId", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<QTG_CapLaiAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<QTG_CapLaiAdd>> QTG_CapLai_ByQuyenTGId(long id)
        {
            var request = new RestRequest("QTG/QTG_CapLai_ByQuyenTGId", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<QTG_CapLaiAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> QTG_CapLai_InsUpd(QTG_CapLaiAdd model)
        {
            var request = new RestRequest("QTG/QTG_CapLai_InsUpd", Method.POST)
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
        #endregion QTG_CapLai

        #region QTG_ThuHoi
        public IRestResponse<ResultResponse<QTG_ThuHoiAdd>> QTG_ThuHoi_ById(long id)
        {
            var request = new RestRequest("QTG/QTG_ThuHoi_ById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<QTG_ThuHoiAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<QTG_ThuHoiAdd>> QTG_ThuHoi_ByHoSoId(long id)
        {
            var request = new RestRequest("QTG/QTG_ThuHoi_ByHoSoId", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<QTG_ThuHoiAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<QTG_ThuHoiAdd>> QTG_ThuHoi_ByQuyenTGId(long id)
        {
            var request = new RestRequest("QTG/QTG_ThuHoi_ByQuyenTGId", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<QTG_ThuHoiAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> QTG_ThuHoi_InsUpd(QTG_ThuHoiAdd model)
        {
            var request = new RestRequest("QTG/QTG_ThuHoi_InsUpd", Method.POST)
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
        #endregion QTG_ThuHoi

        #region QTG_ChuyenChuSoHuu
        public IRestResponse<ResultResponse<QTG_ChuyenChuSoHuuAdd>> QTG_ChuyenChuSoHuu_ById(long id)
        {
            var request = new RestRequest("QTG/QTG_ChuyenChuSoHuu_ById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<QTG_ChuyenChuSoHuuAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<QTG_ChuyenChuSoHuuAdd>> QTG_ChuyenChuSoHuu_ByHoSoId(long id)
        {
            var request = new RestRequest("QTG/QTG_ChuyenChuSoHuu_ByHoSoId", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<QTG_ChuyenChuSoHuuAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<QTG_ChuyenChuSoHuuAdd>> QTG_ChuyenChuSoHuu_ByQuyenTGId(long id)
        {
            var request = new RestRequest("QTG/QTG_ChuyenChuSoHuu_ByQuyenTGId", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<QTG_ChuyenChuSoHuuAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> QTG_ChuyenChuSoHuu_InsUpd(QTG_ChuyenChuSoHuuAdd model)
        {
            var request = new RestRequest("QTG/QTG_ChuyenChuSoHuu_InsUpd", Method.POST)
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
        #endregion QTG_ChuyenChuSoHuu

        #region QTG_CapDoi
        public IRestResponse<ResultResponse<QTG_CapDoiAdd>> QTG_CapDoi_ById(long id)
        {
            var request = new RestRequest("QTG/QTG_CapDoi_ById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<QTG_CapDoiAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<QTG_CapDoiAdd>> QTG_CapDoi_ByHoSoId(long id)
        {
            var request = new RestRequest("QTG/QTG_CapDoi_ByHoSoId", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<QTG_CapDoiAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<QTG_CapDoiAdd>> QTG_CapDoi_ByQuyenTGId(long id)
        {
            var request = new RestRequest("QTG/QTG_CapDoi_ByQuyenTGId", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<QTG_CapDoiAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> QTG_CapDoi_InsUpd(QTG_CapDoiAdd model)
        {
            var request = new RestRequest("QTG/QTG_CapDoi_InsUpd", Method.POST)
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
        #endregion QTG_CapDoi
    }
}
