using System;
using System.Collections.Generic;
using Core.Common.Utilities;
using RestSharp;
using Newtonsoft.Json;
using Business.Entities;
using Module.Framework.UltimateClient;
using PagedList;
using Business.Entities.Domain;
using Business.Entities.Domain.Print;
using System.Web.Configuration;

namespace Module.Framework
{
    public class DungChung_1CuaServiceClient : BaseClient, IDisposable
    {
        private bool _isDisposed;
        public DungChung_1CuaServiceClient() : base(AppSetting.DC_1Cua)
        {
        }
        private string Token = WebConfigurationManager.AppSettings["1Cua_Token"];
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
        public Response_1CuaMap<List<DM_TinhThanh_1CuaMap>> GetDMTinhThanh()
        {
            var url = WebConfigurationManager.AppSettings["1Cua_TinhThanh"];
            var request = new RestRequest(url, Method.GET);
            request.AddHeader("Authorization", Token);
            var restResponse = Execute<Response_1CuaMap<List<DM_TinhThanh_1CuaMap>>>(request);
            return restResponse.Data;
        }
        public Response_1CuaMap<List<DM_QuanHuyen_1CuaMap>> GetDMQuanHuyen(string tinhThanhID)
        {
            var url = WebConfigurationManager.AppSettings["1Cua_QuanHuyen"];
            var request = new RestRequest(url, Method.GET);
            request.AddHeader("Authorization", Token);
            request.AddParameter("tinhThanhID", tinhThanhID);
            var restResponse = Execute<Response_1CuaMap<List<DM_QuanHuyen_1CuaMap>>>(request);
            return restResponse.Data;
        }
        public Response_1CuaMap<List<DM_PhuongXa_1CuaMap>> GetDMPhuongXa(string quanHuyenID)
        {
            var url = WebConfigurationManager.AppSettings["1Cua_PhuongXa"];
            var request = new RestRequest(url, Method.GET);
            request.AddHeader("Authorization", Token);
            request.AddParameter("quanHuyenID", quanHuyenID);
            var restResponse = Execute<Response_1CuaMap<List<DM_PhuongXa_1CuaMap>>>(request);
            return restResponse.Data;
        }
        public Response_1CuaMap<List<DM_LinhVucThuTuc_1CuaMap>> GetDMLinhVucThuTuc(DM_LinhVucThuTuc_1CuaParam model)
        {
            var url = WebConfigurationManager.AppSettings["1Cua_LinhVucThuTuc"];
            var request = new RestRequest(url, Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            request.AddHeader("Authorization", Token);
            request.AddJsonBody(model);
            var restResponse = Execute<Response_1CuaMap<List<DM_LinhVucThuTuc_1CuaMap>>>(request);
            return restResponse.Data;
        }
        public Response_1CuaMap<List<DM_DungChung_1CuaMap>> GetDanhMuc(DM_DungChung_1Cuaparam model)
        {
            var url = WebConfigurationManager.AppSettings["1Cua_DanhMuc"];
            var request = new RestRequest(url, Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            request.AddHeader("Authorization", Token);
            request.AddJsonBody(model);
            var restResponse = Execute<Response_1CuaMap<List<DM_DungChung_1CuaMap>>>(request);
            return restResponse.Data;
        }

        public Response_1CuaMap<DSHoSo_1CuaMap> DanhSachHoSo(HoSo_1CuaParam model)
        {
            var url = WebConfigurationManager.AppSettings["1Cua_DSHoSo"];
            var request = new RestRequest(url, Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            request.AddHeader("Authorization", Token);
            request.AddJsonBody(model);
            var restResponse = Execute<Response_1CuaMap<DSHoSo_1CuaMap>>(request);
            return restResponse.Data;
        }

        public Response_1CuaMap<ChuyenNhanHoSo_1CuaReturn> ChuyenHoSo(ChuyenHoSo_1CuaAdd model)
        {
            var url = WebConfigurationManager.AppSettings["1Cua_ChuyenHoSo"];
            var request = new RestRequest(url, Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            request.AddHeader("Authorization", Token);
            request.AddJsonBody(model);
            var restResponse = Execute<Response_1CuaMap<ChuyenNhanHoSo_1CuaReturn>>(request);
            return restResponse.Data;
        }

        public Response_1CuaMap<ChuyenNhanHoSo_1CuaReturn> NhanHoSo(NhanHoSo_1CuaAdd model)
        {
            var url = WebConfigurationManager.AppSettings["1Cua_NhanHoSo"];
            var request = new RestRequest(url, Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            request.AddHeader("Authorization", Token);
            request.AddJsonBody(model);
            var restResponse = Execute<Response_1CuaMap<ChuyenNhanHoSo_1CuaReturn>>(request);
            return restResponse.Data;
        }

        public Response_1CuaMap<List<DM_TenBuocKeTiep_1CuaMap>> GetTenBuocKeTiep(DM_TenBuocKeTiep_1CuaParam model)
        {
            var url = WebConfigurationManager.AppSettings["1Cua_TenBuocKeTiep"];
            var request = new RestRequest(url, Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            request.AddHeader("Authorization", Token);
            request.AddJsonBody(model);
            var restResponse = Execute<Response_1CuaMap<List<DM_TenBuocKeTiep_1CuaMap>>>(request);
            return restResponse.Data;
        }

        public Response_1CuaMap<ChiTietHoSo_1CuaMap> GetChiTietHoSo(long hoSoID, long userID)
        {
            var url = WebConfigurationManager.AppSettings["1Cua_ChiTietHoSo"];
            var request = new RestRequest(url, Method.GET);
            request.AddHeader("Authorization", Token);
            request.AddParameter("hoSoID", hoSoID);
            request.AddParameter("userID", userID);
            var restResponse = Execute<Response_1CuaMap<ChiTietHoSo_1CuaMap>>(request);
            return restResponse.Data;
        }

        public string EncryptedAccount(string userName, string password)
        {
            var url = WebConfigurationManager.AppSettings["1Cua_EncryptedAccount"];
            var request = new RestRequest(url, Method.GET);
            request.AddHeader("Authorization", Token);
            request.AddParameter("UserName", userName);
            request.AddParameter("Password", password);
            var restResponse = Execute(request);
            return restResponse.Content.ToString().Replace("\"", "");
        }

        public ResponseUser_1CuaMap<string> ValidateUser(string encrypted)
        {
            var url = WebConfigurationManager.AppSettings["1Cua_GetToken"];
            var request = new RestRequest(url, Method.GET);
            request.AddParameter("encryptedText", encrypted);
            var restResponse = Execute<ResponseUser_1CuaMap<string>>(request);
            return restResponse.Data;
        }

        public Response_1CuaMap<NguoiDungHeThong1CuaMapAdd> GetThongTinUser(string token, string userName)
        {
            var url = WebConfigurationManager.AppSettings["1Cua_GetThongTinUser"];
            var request = new RestRequest(url, Method.GET);
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddParameter("strUserName", userName);
            var restResponse = Execute<Response_1CuaMap<NguoiDungHeThong1CuaMapAdd>>(request);
            return restResponse.Data;
        }
    }
}
