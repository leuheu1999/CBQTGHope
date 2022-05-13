using Business.Entities.Domain;
using Core.Common.Utilities;
using Module.Framework.UltimateClient;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Module.Framework
{
    public class UsersServiceClient : BaseClient, IDisposable
    {
        private bool _isDisposed;
        public UsersServiceClient() : base(AppSetting.Users)
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
        #region NguoiDungHeThong
        public IRestResponse<ResultResponse<List<NguoiDungHeThongMap>>> NguoiDungHeThong_List(NguoiDungHeThongMapParam model)
        {
            var request = new RestRequest("ND/NguoiDungHeThong_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<NguoiDungHeThongMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<NguoiDungHeThongMapAdd>> NguoiDungHeThong_GetById(long id)
        {
            var request = new RestRequest("ND/NguoiDungHeThong_GetById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<NguoiDungHeThongMapAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<NguoiDungHeThongMapAdd>> NguoiDungHeThong_GetByGuid(Guid id)
        {
            var request = new RestRequest("ND/NguoiDungHeThong_GetByGuid", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<NguoiDungHeThongMapAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<NguoiDungHeThongMapAdd>> NguoiDungHeThong_GetByTenTaiKhoan(string tentaikhoan)
        {
            var request = new RestRequest("ND/NguoiDungHeThong_GetByTenTaiKhoan", Method.GET);
            request.AddParameter("tentaikhoan", tentaikhoan);
            var restResponse = Execute<ResultResponse<NguoiDungHeThongMapAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<NguoiDungHeThongMapAdd>> NguoiDungHeThong_GetBySDT(string sodienthoai)
        {
            var request = new RestRequest("ND/NguoiDungHeThong_GetBySDT", Method.GET);
            request.AddParameter("sodienthoai", sodienthoai);
            var restResponse = Execute<ResultResponse<NguoiDungHeThongMapAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<NguoiDungHeThongMapAdd>> NguoiDungHeThong_GetByEmail(string email)
        {
            var request = new RestRequest("ND/NguoiDungHeThong_GetByEmail", Method.GET);
            request.AddParameter("email", email);
            var restResponse = Execute<ResultResponse<NguoiDungHeThongMapAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<int>> NguoiDungHeThong_Del(Guid id, Guid lastupduserid)
        {
            var request = new RestRequest("ND/NguoiDungHeThong_Del", Method.GET);
            request.AddParameter("id", id);
            request.AddParameter("lastupduserid", lastupduserid);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<int>> NguoiDungHeThong_CheckMCUserId(long userId)
        {
            var request = new RestRequest("ND/NguoiDungHeThong_CheckMCUserId", Method.GET);
            request.AddParameter("userId", userId);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<Guid>> NguoiDungHeThong_InsUpd(NguoiDungHeThongMapAdd model)
        {
            var request = new RestRequest("ND/NguoiDungHeThong_InsUpd", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<Guid>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<int>> NguoiDungHeThong_ChangePass(NguoiDungHeThongChagePass model)
        {
            var request = new RestRequest("ND/NguoiDungHeThong_ChangePass", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);
            request.AddParameter("application/json", json, null, ParameterType.RequestBody);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> NguoiDungHeThong_Khoa(Guid UserID, bool Khoa)
        {
            var request = new RestRequest("ND/NguoiDungHeThong_Khoa", Method.GET);
            request.AddParameter("UserID", UserID);
            request.AddParameter("Khoa", Khoa);
            var restResponse = Execute<ResultResponse<long>>(request);
            return restResponse;
        }
        #endregion

        #region nguoi dung he thong nhom quyen
        public IRestResponse<ResultResponse<NguoiDungHeThong_NhomQuyenMap>> NguoiDungHeThong_NhomQuyen(Guid id)
        {
            var request = new RestRequest("ND/NguoiDungHeThong_NhomQuyen", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<NguoiDungHeThong_NhomQuyenMap>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> NguoiDungHeThong_NhomQuyen_Insert(NguoiDungHeThong_NhomQuyenMapAdd model)
        {
            var request = new RestRequest("ND/NguoiDungHeThong_NhomQuyen_Insert", Method.POST)
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
        #endregion

        #region Phan quyen chuc nang
        public IRestResponse<ResultResponse<SYS_PhanQuyenMap>> PhanQuyenChucNang_getall()
        {
            var request = new RestRequest("PQ/PQ_PhanQuyenChucNang", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var restResponse = Execute<ResultResponse<SYS_PhanQuyenMap>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> PQ_PhanQuyenChucNang_Ins(List<PQ_PhanQuyenChucNang> model)
        {
            var request = new RestRequest("PQ/PQ_PhanQuyenChucNang_Ins", Method.POST)
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

        #endregion

        #region nhat ky nguoi dung
        public IRestResponse<ResultResponse<long>> NhatKyNguoiDung_Insert(ND_NhatKyNguoiDungAdd model)
        {
            
            var request = new RestRequest("ND/NhatKyNguoiDung_Insert", Method.POST)
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
        public IRestResponse<ResultResponse<List<ND_NhatKyNguoiDungMap>>> NhatKyNguoiDung_List(ND_NhatKyNguoiDungParam model)
        {
            var request = new RestRequest("ND/NhatKyNguoiDung_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);
            var restResponse = Execute<ResultResponse<List<ND_NhatKyNguoiDungMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<int>> NhatKyNguoiDung_Del(long id)
        {
            var request = new RestRequest("ND/NhatKyNguoiDung_Del", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<int>> NhatKyNguoiDung_DelLstID(List<long> ids)
        {
            var request = new RestRequest("ND/NhatKyNguoiDung_DelLstID", Method.POST)
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
        public IRestResponse<ResultResponse<List<DSLogTypeNDmap>>> LogTypeND_List(LogTypeNDParam model)
        {
            var request = new RestRequest("ND/LogTypeND_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);
            var restResponse = Execute<ResultResponse<List<DSLogTypeNDmap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<int>> LogTypeND_Update(string ids, string idsUn)
        {
            var request = new RestRequest("ND/LogTypeND_Update", Method.GET);
            request.AddParameter("ids", ids);
            request.AddParameter("idsUn", idsUn);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        #endregion

        #region login
        public IRestResponse<ResultResponse<int>> ValidateUser(LoginParam model)
        {
            var request = new RestRequest("ND/ValidateUser", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<bool>> IsActive(string username)
        {
            var request = new RestRequest("ND/IsActive", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(username, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<bool>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<List<BanIPMap>>> BanIP_List()
        {
            var request = new RestRequest("ND/BanIP_List", Method.POST);
            var restResponse = Execute<ResultResponse<List<BanIPMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<BanIPMap>> BanIP_byIp(string ip)
        {
            var request = new RestRequest("ND/BanIP_byIp", Method.GET);
            request.AddParameter("ip", ip);
            var restResponse = Execute<ResultResponse<BanIPMap>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<int>> BanIP_deletebyIp(string ip)
        {
            var request = new RestRequest("ND/BanIP_deletebyIp", Method.GET);
            request.AddParameter("ip", ip);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> BanIP_InsUpd(BanIPMap model)
        {
            var request = new RestRequest("ND/BanIP_InsUpd", Method.POST)
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
        #endregion

        #region phan quyen
        public IRestResponse<ResultResponse<List<Roles>>> GetRolesByUser(string username)
        {
            var request = new RestRequest("ND/GetRolesByUser", Method.GET);
            request.AddParameter("username", username);
            var restResponse = Execute<ResultResponse<List<Roles>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<List<Right>>> GetRightsByUser(string username)
        {
            var request = new RestRequest("ND/GetRightsByUser", Method.GET);
            request.AddParameter("username", username);
            var restResponse = Execute<ResultResponse<List<Right>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<UrlViewModel>> GetUrlViewModel(string DSQuyenChucNang)
        {
            var request = new RestRequest("ND/GetUrlViewModel", Method.GET);
            request.AddParameter("DSQuyenChucNang", DSQuyenChucNang);
            var restResponse = Execute<ResultResponse<UrlViewModel>>(request);
            return restResponse;
        }
        #endregion

        public IRestResponse<ResultResponse<string>> ND_NguoiDungHeThong_UpdateConnectionSignalR(Guid UserID, string ConnectionId, bool IsOnline)
        {
            var request = new RestRequest("ND/ND_NguoiDungHeThong_UpdateConnectionSignalR", Method.GET);
            request.AddParameter("UserID", UserID);
            request.AddParameter("ConnectionId", ConnectionId);
            request.AddParameter("IsOnline", IsOnline);
            var restResponse = Execute<ResultResponse<string>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> ND_NguoiDungHeThong_CountOnline()
        {
            var request = new RestRequest("ND/ND_NguoiDungHeThong_CountOnline", Method.GET);
            var restResponse = Execute<ResultResponse<long>>(request);
            return restResponse;
        }
    }
}
