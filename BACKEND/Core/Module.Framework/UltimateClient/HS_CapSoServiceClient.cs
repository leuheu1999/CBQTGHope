using Business.Entities.Domain;
using Core.Common.Utilities;
using Module.Framework.UltimateClient;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Module.Framework
{
    public class HS_CapSoServiceClient : BaseClient, IDisposable
    {
        private bool _isDisposed;
        public HS_CapSoServiceClient() : base(AppSetting.HS_CapSo)
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
        #region TT_CapQuyen
        public IRestResponse<ResultResponse<int>> TT_CapQuyen_CheckCapSo(long hoSoID)
        {
            var request = new RestRequest("HSCS/TT_CapQuyen_CheckCapSo", Method.GET);
            request.AddParameter("hoSoID", hoSoID);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<TT_CapQuyenAdd>> TT_CapQuyen_GetByHoSoId(long hoSoID)
        {
            var request = new RestRequest("HSCS/TT_CapQuyen_GetByHoSoId", Method.GET);
            request.AddParameter("hoSoID", hoSoID);
            var restResponse = Execute<ResultResponse<TT_CapQuyenAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<List<TT_CapQuyenMap>>> TT_CapQuyen_List(TT_CapQuyenParam model)
        {
            var request = new RestRequest("HSCS/TT_CapQuyen_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            request.AddJsonBody(model);
            var restResponse = Execute<ResultResponse<List<TT_CapQuyenMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> TT_CapQuyen_UpdTinhTrang(TT_CapQuyenAddTrangThai model)
        {
            var request = new RestRequest("HSCS/TT_CapQuyen_UpdTinhTrang", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            request.AddJsonBody(model);
            var restResponse = Execute<ResultResponse<long>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<int>> TT_CapQuyen_GetLoaiNghiepVuId(long hoSoID)
        {
            var request = new RestRequest("HSCS/TT_CapQuyen_GetLoaiNghiepVuId", Method.GET);
            request.AddParameter("hoSoID", hoSoID);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        #endregion TT_CapQuyen

        #region HS_CapSo
        public IRestResponse<ResultResponse<HS_GenSo>> HS_CapSo_GenSo(int loaiNghiepVuID)
        {
            var request = new RestRequest("HSCS/HS_CapSo_GenSo", Method.GET);
            request.AddParameter("loaiNghiepVuID", loaiNghiepVuID);
            var restResponse = Execute<ResultResponse<HS_GenSo>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<HS_GenSo>> DVC_HS_CapSo_GenSo(int loaiNghiepVuID)
        {
            var request = new RestRequest("HSCS/DVC_HS_CapSo_GenSo", Method.GET);
            request.AddParameter("loaiNghiepVuID", loaiNghiepVuID);
            var restResponse = Execute<ResultResponse<HS_GenSo>>(request);
            return restResponse;
        }
        #endregion HS_CapSo

        #region HS_HoSo
        public IRestResponse<ResultResponse<int>> HS_HoSo_CheckCapSo(long hoSoID)
        {
            var request = new RestRequest("HS/HS_HoSo_CheckCapSo", Method.GET);
            request.AddParameter("hoSoID", hoSoID);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        #endregion HS_HoSo
    }
}
