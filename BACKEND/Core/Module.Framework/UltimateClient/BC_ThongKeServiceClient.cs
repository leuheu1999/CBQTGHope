using Business.Entities.Domain;
using Core.Common.Utilities;
using Module.Framework.UltimateClient;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Module.Framework
{
    public class BC_ThongKeServiceClient : BaseClient, IDisposable
    {
        private bool _isDisposed;
        public BC_ThongKeServiceClient() : base(AppSetting.BC_ThongKe)
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
        public IRestResponse<ResultResponse<List<BC_ThongKeGiayChungNhanMap>>> BC_ThongKeGiayChungNhan_List(BC_ThongKeGiayChungNhanParam model)
        {
            var request = new RestRequest("BCTK/BC_ThongKeGiayChungNhan_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<BC_ThongKeGiayChungNhanMap>>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<List<BC_CongBaoMap>>> BC_CongBao_List(BC_CongBaoParam model)
        {
            var request = new RestRequest("BCTK/BC_CongBao_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<BC_CongBaoMap>>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<List<BC_BaoCaoTacPhamMap>>> BC_BaoCaoTacPham_List(BC_BaoCaoTacPhamParam model)
        {
            var request = new RestRequest("BCTK/BC_BaoCaoTacPham_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<BC_BaoCaoTacPhamMap>>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<List<BC_BaoCaoTongHopHoatDongMap>>> BC_BaoCaoTongHopHoatDong_List(BC_BaoCaoTongHopHoatDongParam model)
        {
            var request = new RestRequest("BCTK/BC_BaoCaoTongHopHoatDong_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<BC_BaoCaoTongHopHoatDongMap>>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<List<BC_SoGiayChungNhanBanQuyenMap>>> BC_SoGiayChungNhanBanQuyen_List(BC_SoGiayChungNhanBanQuyenParam model)
        {
            var request = new RestRequest("BCTK/BC_SoGiayChungNhanBanQuyen_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<BC_SoGiayChungNhanBanQuyenMap>>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<List<BC_ThongKeHoSoQuyenTacGiaMap>>> BC_ThongKeHoSoQuyenTacGia_Dashboard(BC_ThongKeHoSoQuyenTacGiaParam model)
        {
            var request = new RestRequest("BCTK/BC_ThongKeHoSoQuyenTacGia_Dashboard", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<BC_ThongKeHoSoQuyenTacGiaMap>>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<List<BC_ThongKeHoSoQuyenLienQuanMap>>> BC_ThongKeHoSoQuyenLienQuan_Dashboard(BC_ThongKeHoSoQuyenLienQuanParam model)
        {
            var request = new RestRequest("BCTK/BC_ThongKeHoSoQuyenLienQuan_Dashboard", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<BC_ThongKeHoSoQuyenLienQuanMap>>>(request);
            return restResponse;
        }
    }
}
