using Business.Entities;
using Business.Entities.Domain;
using Business.Entities.Domain.Print;
using Core.Common.Utilities;
using Module.Framework.UltimateClient;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Module.Framework
{
    public class DungChungServiceClient : BaseClient, IDisposable
    {
        private bool _isDisposed;
        public DungChungServiceClient() : base(AppSetting.DC)
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
        public ResultResponse<List<CBO_DungChungViewModel>> CBO_DungChung_GetAll(CBO_DungChungParam model)
        {
            var request = new RestRequest("DC/CBO_DungChung_GetAll", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            request.AddJsonBody(model);
            var restResponse = Execute<ResultResponse<List<CBO_DungChungViewModel>>>(request);
            return restResponse.Data;
        }
        public ResultResponse<List<CBO_DungChungViewModel>> CBO_DungChung_GetAllMater(CBO_DungChungParam model)
        {
            var request = new RestRequest("DC/CBO_DungChung_GetAllMater", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            request.AddJsonBody(model);
            var restResponse = Execute<ResultResponse<List<CBO_DungChungViewModel>>>(request);
            return restResponse.Data;
        }
        public ResultResponse<string> GetKeyCauHinhHeThong(string Key)
        {
            var request = new RestRequest("DC/GetKeyCauHinhHeThong", Method.GET);
            request.AddParameter("Key", Key);
            var restResponse = Execute<ResultResponse<string>>(request);
            return restResponse.Data;
        }
        public ResultResponse<string> GetKeyDMDonVi(string Key)
        {
            var request = new RestRequest("DC/GetKeyDMDonVi", Method.GET);
            request.AddParameter("Key", Key);
            var restResponse = Execute<ResultResponse<string>>(request);
            return restResponse.Data;
        }
        public ResultResponse<bool> XaoCacheService(string Key)
        {
            var request = new RestRequest("DC/DisposeCache", Method.GET);
            request.AddParameter("Key", Key);
            var restResponse = Execute<ResultResponse<bool>>(request);
            return restResponse.Data;
        }

        
        #region DM_QuocGia
        public IRestResponse<ResultResponse<List<DM_QuocGiaMap>>> DM_QuocGia_List(DM_QuocGiaMapParam model)
        {
            var request = new RestRequest("DM/DM_QuocGia_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<DM_QuocGiaMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<DM_QuocGiaMapAdd>> GetQuocGiaById(long id)
        {
            var request = new RestRequest("DM/GetQuocGiaById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<DM_QuocGiaMapAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<int>> DM_QuocGia_Del(long id, Guid lastupduserid)
        {
            var request = new RestRequest("DM/DM_QuocGia_Del", Method.GET);
            request.AddParameter("id", id);
            request.AddParameter("lastupduserid", lastupduserid);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<long>> DM_QuocGia_InsUpdate(DM_QuocGiaMapAdd model)
        {
            var request = new RestRequest("DM/DM_QuocGia_InsUpdate", Method.POST)
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
        public IRestResponse<ResultResponse<DM_QuocGiaMapAdd>> GetQuocGiaByMa(string ma)
        {
            var request = new RestRequest("DM/GetQuocGiaByMa", Method.GET);
            request.AddParameter("ma", ma);
            var restResponse = Execute<ResultResponse<DM_QuocGiaMapAdd>>(request);
            return restResponse;
        }
        #endregion

        #region DM_TinhThanh
        public IRestResponse<ResultResponse<List<DM_TinhThanhMap>>> DM_TinhThanh_List(DM_TinhThanhMapParam model)
        {
            var request = new RestRequest("DM/DM_TinhThanh_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<DM_TinhThanhMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<DM_TinhThanhMapAdd>> GetTinhThanhById(long id)
        {
            var request = new RestRequest("DM/DM_TinhThanh_GetById", Method.GET);
            request.AddParameter("tinhThanhID", id);
            var restResponse = Execute<ResultResponse<DM_TinhThanhMapAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<int>> DM_TinhThanh_Del(long id, Guid lastupduserid)
        {
            var request = new RestRequest("DM/DM_TinhThanh_Del", Method.GET);
            request.AddParameter("id", id);
            request.AddParameter("lastupduserid", lastupduserid);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> DM_TinhThanh_InsUpd(DM_TinhThanhMapAdd model)
        {
            var request = new RestRequest("DM/DM_TinhThanh_InsUpd", Method.POST)
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
        public IRestResponse<ResultResponse<DM_TinhThanhMapAdd>> GetTinhThanhByMa(string ma)
        {
            var request = new RestRequest("DM/DM_TinhThanh_GetByMa", Method.GET);
            request.AddParameter("maTinhThanh", ma);
            var restResponse = Execute<ResultResponse<DM_TinhThanhMapAdd>>(request);
            return restResponse;
        }
        #endregion

        #region dm_nhomquyen
        public IRestResponse<ResultResponse<List<DM_NhomQuyenMap>>> DM_NhomQuyen_List(DM_NhomQuyenParam model)
        {
            var request = new RestRequest("DM/DM_NhomQuyen_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<DM_NhomQuyenMap>>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<DM_NhomQuyenAdd>> DM_NhomQuyen_GetById(long id)
        {
            var request = new RestRequest("DM/DM_NhomQuyen_GetById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<DM_NhomQuyenAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<DM_NhomQuyenAdd>> DM_NhomQuyen_GetByMa(string ma)
        {
            var request = new RestRequest("DM/DM_NhomQuyen_GetByMa", Method.GET);
            request.AddParameter("ma", ma);
            var restResponse = Execute<ResultResponse<DM_NhomQuyenAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> DM_NhomQuyen_InsUpd(DM_NhomQuyenAdd model)
        {
            var request = new RestRequest("DM/DM_NhomQuyen_InsUpd", Method.POST)
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
        public IRestResponse<ResultResponse<bool>> DM_NhomQuyen_Delete(long id, Guid userId)
        {
            var request = new RestRequest("DM/DM_NhomQuyen_Delete", Method.GET);
            request.AddParameter("id", id);
            request.AddParameter("userId", userId);
            var restResponse = Execute<ResultResponse<bool>>(request);
            return restResponse;
        }
        #endregion

        #region danh muc quyen
        public IRestResponse<ResultResponse<List<DM_QuyenMap>>> DM_Quyen_List(DM_QuyenMapParam model)
        {
            var request = new RestRequest("DM/DM_Quyen_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<DM_QuyenMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<DM_QuyenMapAdd>> DM_Quyen_GetById(long id)
        {
            var request = new RestRequest("DM/DM_Quyen_GetById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<DM_QuyenMapAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<DM_QuyenMapAdd>> DM_Quyen_GetByMa(string ma)
        {
            var request = new RestRequest("DM/DM_Quyen_GetByMa", Method.GET);
            request.AddParameter("ma", ma);
            var restResponse = Execute<ResultResponse<DM_QuyenMapAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> DM_Quyen_InsUpd(DM_QuyenMapAdd model)
        {
            var request = new RestRequest("DM/DM_Quyen_InsUpd", Method.POST)
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
        public IRestResponse<ResultResponse<bool>> DM_Quyen_Delete(long id, Guid userId)
        {
            var request = new RestRequest("DM/DM_Quyen_Delete", Method.GET);
            request.AddParameter("id", id);
            request.AddParameter("userId", userId);
            var restResponse = Execute<ResultResponse<bool>>(request);
            return restResponse;
        }
        #endregion

        #region danh muc quyen chuc nang
        public IRestResponse<ResultResponse<List<DM_QuyenChucNangMap>>> DM_QuyenChucNang_List(DM_QuyenChucNangMapParam model)
        {
            var request = new RestRequest("DM/DM_QuyenChucNang_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<DM_QuyenChucNangMap>>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<DM_QuyenChucNangAdd>> DM_QuyenChucNang_GetById(long id)
        {
            var request = new RestRequest("DM/DM_QuyenChucNang_GetById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<DM_QuyenChucNangAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<DM_QuyenChucNangAdd>> DM_QuyenChucNang_GetByMa(string ma)
        {
            var request = new RestRequest("DM/DM_QuyenChucNang_GetByMa", Method.GET);
            request.AddParameter("ma", ma);
            var restResponse = Execute<ResultResponse<DM_QuyenChucNangAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> DM_QuyenChuNang_InsUpd(DM_QuyenChucNangAdd model)
        {
            var request = new RestRequest("DM/DM_QuyenChuNang_InsUpd", Method.POST)
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
        public IRestResponse<ResultResponse<bool>> DM_QuyenChucNang_Delete(long id, Guid userId)
        {
            var request = new RestRequest("DM/DM_QuyenChucNang_Delete", Method.GET);
            request.AddParameter("id", id);
            request.AddParameter("userId", userId);
            var restResponse = Execute<ResultResponse<bool>>(request);
            return restResponse;
        }
        #endregion

        #region DM_PhuongXa
        public IRestResponse<ResultResponse<List<DM_PhuongXaMap>>> DM_PhuongXa_List(DM_PhuongXaMapParam model)
        {
            var request = new RestRequest("DM/DM_PhuongXa_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<DM_PhuongXaMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<DM_PhuongXaMapAdd>> GetPhuongXaById(long id)
        {
            var request = new RestRequest("DM/DM_PhuongXa_GetById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<DM_PhuongXaMapAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<int>> DM_PhuongXa_Del(long id, Guid lastupduserid)
        {
            var request = new RestRequest("DM/DM_PhuongXa_Del", Method.GET);
            request.AddParameter("id", id);
            request.AddParameter("lastupduserid", lastupduserid);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> DM_PhuongXa_InsUpd(DM_PhuongXaMapAdd model)
        {
            var request = new RestRequest("DM/DM_PhuongXa_InsUpd", Method.POST)
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
        public IRestResponse<ResultResponse<DM_PhuongXaMapAdd>> GetPhuongXaByMa(string ma)
        {
            var request = new RestRequest("DM/DM_PhuongXa_GetByMa", Method.GET);
            request.AddParameter("ma", ma);
            var restResponse = Execute<ResultResponse<DM_PhuongXaMapAdd>>(request);
            return restResponse;
        }
        #endregion

        #region danh muc quyen
        public IRestResponse<ResultResponse<List<DM_QuanHuyenMap>>> DM_QuanHuyen_List(DM_QuanHuyenMapParam model)
        {
            var request = new RestRequest("DM/DM_QuanHuyen_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<DM_QuanHuyenMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<DM_QuanHuyenMapAdd>> DM_QuanHuyen_GetById(long id)
        {
            var request = new RestRequest("DM/DM_QuanHuyen_GetById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<DM_QuanHuyenMapAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<DM_QuanHuyenMapAdd>> DM_QuanHuyen_GetByMa(string ma)
        {
            var request = new RestRequest("DM/DM_QuanHuyen_GetByMa", Method.GET);
            request.AddParameter("ma", ma);
            var restResponse = Execute<ResultResponse<DM_QuanHuyenMapAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> DM_QuanHuyen_InsUpd(DM_QuanHuyenMapAdd model)
        {
            var request = new RestRequest("DM/DM_QuanHuyen_InsUpd", Method.POST)
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
        public IRestResponse<ResultResponse<bool>> DM_QuanHuyen_Delete(long id, Guid userId)
        {
            var request = new RestRequest("DM/DM_QuanHuyen_Delete", Method.GET);
            request.AddParameter("id", id);
            request.AddParameter("userId", userId);
            var restResponse = Execute<ResultResponse<bool>>(request);
            return restResponse;
        }
        #endregion
              
        #region CauHinhHeThong
        public IRestResponse<ResultResponse<CauHinhHeThongAll>> GetAllCauHinh()
        {
            var request = new RestRequest("CH/GetAllCauHinh", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var restResponse = Execute<ResultResponse<CauHinhHeThongAll>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> CauHinhheThong_Insert(CauHinhHeThongAll model)
        {
            var request = new RestRequest("CH/CauHinhheThong_Insert", Method.POST)
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

        public IRestResponse<ResultResponse<int>> CauHinhMail_DoiMatKhau(string pass)
        {
            var request = new RestRequest("CH/CauHinhMail_DoiMatKhau", Method.GET);
            request.AddParameter("pass", pass);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<bool>> LuotTruyCapHeThong_Insert()
        {
            var request = new RestRequest("CH/LuotTruyCapHeThong_Insert", Method.GET);
            var restResponse = Execute<ResultResponse<bool>>(request);
            return restResponse;
        }
        #endregion
      
        #region "Print"
        public IRestResponse<ResultResponse<string>> Print(Print_DataMap model)
        {
            var request = new RestRequest("DC/Print", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };

            request.AddJsonBody(model);
            var restResponse = Execute<ResultResponse<string>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<E_ParametersMap>> GetInfoParametersByKeyCode(string model)
        {
            var request = new RestRequest("DC/GetInfoParametersByKeyCode", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };

            request.AddJsonBody(model);
            var restResponse = Execute<ResultResponse<E_ParametersMap>>(request);
            return restResponse;
        }
        #endregion

        #region TT_ChuSoHuu
        public IRestResponse<ResultResponse<List<TT_ChuSoHuuMap>>> TT_ChuSoHuu_List(TT_ChuSoHuuParam model)
        {
            var request = new RestRequest("TT/TT_ChuSoHuu_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<TT_ChuSoHuuMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<TT_ChuSoHuuAdd>> TT_ChuSoHuu_ById(long id)
        {
            var request = new RestRequest("TT/TT_ChuSoHuu_ById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<TT_ChuSoHuuAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<int>> TT_ChuSoHuu_CheckCCCD(string cccd, long chuSoHuuID)
        {
            var request = new RestRequest("TT/TT_ChuSoHuu_CheckCCCD", Method.GET);
            request.AddParameter("cccd", cccd);
            request.AddParameter("chuSoHuuID", chuSoHuuID);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> TT_ChuSoHuu_InsUpd(TT_ChuSoHuuAdd model)
        {
            var request = new RestRequest("TT/TT_ChuSoHuu_InsUpd", Method.POST)
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
        #endregion TT_ChuSoHuu

        #region TT_CongDan
        public IRestResponse<ResultResponse<List<TT_CongDanMap>>> TT_CongDan_List(TT_CongDanParam model)
        {
            var request = new RestRequest("TT/TT_CongDan_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<TT_CongDanMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<TT_CongDanAdd>> TT_CongDan_ById(long id)
        {
            var request = new RestRequest("TT/TT_CongDan_ById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<TT_CongDanAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<int>> TT_CongDan_CheckCCCD(string cccd, long congDanID)
        {
            var request = new RestRequest("TT/TT_CongDan_CheckCCCD", Method.GET);
            request.AddParameter("cccd", cccd);
            request.AddParameter("congDanID", congDanID);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<int>> TT_CongDan_CheckDKKD(string dkkd, long congDanID)
        {
            var request = new RestRequest("TT/TT_CongDan_CheckDKKD", Method.GET);
            request.AddParameter("dkkd", dkkd);
            request.AddParameter("congDanID", congDanID);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> TT_CongDan_InsUpd(TT_CongDanAdd model)
        {
            var request = new RestRequest("TT/TT_CongDan_InsUpd", Method.POST)
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
        #endregion TT_CongDan

        #region TT_TacGia
        public IRestResponse<ResultResponse<List<TT_TacGiaMap>>> TT_TacGia_List(TT_TacGiaParam model)
        {
            var request = new RestRequest("TT/TT_TacGia_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<TT_TacGiaMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<TT_TacGiaAdd>> TT_TacGia_ById(long id)
        {
            var request = new RestRequest("TT/TT_TacGia_ById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<TT_TacGiaAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<int>> TT_TacGia_CheckCCCD(string cccd, long tacGiaID)
        {
            var request = new RestRequest("TT/TT_TacGia_CheckCCCD", Method.GET);
            request.AddParameter("cccd", cccd);
            request.AddParameter("tacGiaID", tacGiaID);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> TT_TacGia_InsUpd(TT_TacGiaAdd model)
        {
            var request = new RestRequest("TT/TT_TacGia_InsUpd", Method.POST)
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
        #endregion TT_TacGia

        #region TT_HinhAnh
        public IRestResponse<ResultResponse<List<TT_HinhAnhMap>>> TT_HinhAnh_List(TT_HinhAnhParam model)
        {
            var request = new RestRequest("TT/TT_HinhAnh_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<TT_HinhAnhMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<TT_HinhAnhAdd>> TT_HinhAnh_ById(long id)
        {
            var request = new RestRequest("TT/TT_HinhAnh_ById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<TT_HinhAnhAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> TT_HinhAnh_InsUpd(TT_HinhAnhAdd model)
        {
            var request = new RestRequest("TT/TT_HinhAnh_InsUpd", Method.POST)
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
        public IRestResponse<ResultResponse<int>> TT_HinhAnh_Del(long id, Guid userId)
        {
            var request = new RestRequest("TT/TT_HinhAnh_Del", Method.GET);
            request.AddParameter("id", id);
            request.AddParameter("userId", userId);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        #endregion TT_HinhAnh

        #region TT_Phim
        public IRestResponse<ResultResponse<List<TT_PhimMap>>> TT_Phim_List(TT_PhimParam model)
        {
            var request = new RestRequest("TT/TT_Phim_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<TT_PhimMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<TT_PhimAdd>> TT_Phim_ById(long id)
        {
            var request = new RestRequest("TT/TT_Phim_ById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<TT_PhimAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> TT_Phim_InsUpd(TT_PhimAdd model)
        {
            var request = new RestRequest("TT/TT_Phim_InsUpd", Method.POST)
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
        public IRestResponse<ResultResponse<int>> TT_Phim_Del(long id, Guid userId)
        {
            var request = new RestRequest("TT/TT_Phim_Del", Method.GET);
            request.AddParameter("id", id);
            request.AddParameter("userId", userId);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        #endregion TT_Phim

        #region TT_DinhKem
        public IRestResponse<ResultResponse<List<TT_DinhKemMap>>> TT_DinhKem_List(TT_DinhKemParam model)
        {
            var request = new RestRequest("TT/TT_DinhKem_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<TT_DinhKemMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<TT_DinhKemAdd>> TT_DinhKem_ById(long id)
        {
            var request = new RestRequest("TT/TT_DinhKem_ById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<TT_DinhKemAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> TT_DinhKem_InsUpd(TT_DinhKemAdd model)
        {
            var request = new RestRequest("TT/TT_DinhKem_InsUpd", Method.POST)
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
        public IRestResponse<ResultResponse<int>> TT_DinhKem_Del(long id, Guid userId)
        {
            var request = new RestRequest("TT/TT_DinhKem_Del", Method.GET);
            request.AddParameter("id", id);
            request.AddParameter("userId", userId);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        #endregion TT_DinhKem

        #region TT_NguoiBieuDien
        public IRestResponse<ResultResponse<List<TT_NguoiBieuDienMap>>> TT_NguoiBieuDien_List(TT_NguoiBieuDienParam model)
        {
            var request = new RestRequest("TT/TT_NguoiBieuDien_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<TT_NguoiBieuDienMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<TT_NguoiBieuDienAdd>> TT_NguoiBieuDien_ById(long id)
        {
            var request = new RestRequest("TT/TT_NguoiBieuDien_ById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<TT_NguoiBieuDienAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<int>> TT_NguoiBieuDien_CheckCCCD(string cccd, long nguoiBieuDienID)
        {
            var request = new RestRequest("TT/TT_NguoiBieuDien_CheckCCCD", Method.GET);
            request.AddParameter("cccd", cccd);
            request.AddParameter("nguoiBieuDienID", nguoiBieuDienID);
            var restResponse = Execute<ResultResponse<int>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<long>> TT_NguoiBieuDien_InsUpd(TT_NguoiBieuDienAdd model)
        {
            var request = new RestRequest("TT/TT_NguoiBieuDien_InsUpd", Method.POST)
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
        #endregion TT_NguoiBieuDien

        #region MAP_ThuTuc_ManHinh
        public IRestResponse<ResultResponse<MAP_ThuTuc_ManHinhAdd>> MAP_ThuTuc_ManHinh_GetById(long id)
        {
            var request = new RestRequest("MAP/MAP_ThuTuc_ManHinh_GetById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<MAP_ThuTuc_ManHinhAdd>>(request);
            return restResponse;
        }
        #endregion

        #region DM_LoaiHinhDangKy
        public IRestResponse<ResultResponse<long>> DM_LoaiHinhDangKy_InsUpd(DM_LoaiHinhDangKyMapAdd model)
        {
            var request = new RestRequest("DM/DM_LoaiHinhDangKy_InsUpd", Method.POST)
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

        public IRestResponse<ResultResponse<List<DM_LoaiHinhDangKyMap>>> DM_LoaiHinhDangKy_List(DM_LoaiHinhDangKyMapParam model)
        {
            var request = new RestRequest("DM/DM_LoaiHinhDangKy_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<DM_LoaiHinhDangKyMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<DM_LoaiHinhDangKyMapAdd>> DM_LoaiHinhDangKy_GetById(long id)
        {
            var request = new RestRequest("DM/DM_LoaiHinhDangKy_GetById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<DM_LoaiHinhDangKyMapAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<DM_LoaiHinhDangKyMapAdd>> DM_LoaiHinhDangKy_GetByMa(string ma)
        {
            var request = new RestRequest("DM/DM_LoaiHinhDangKy_GetByMa", Method.GET);
            request.AddParameter("ma", ma);
            var restResponse = Execute<ResultResponse<DM_LoaiHinhDangKyMapAdd>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<bool>> DM_LoaiHinhDangKy_Delete(long id, Guid userId)
        {
            var request = new RestRequest("DM/DM_LoaiHinhDangKy_Delete", Method.GET);
            request.AddParameter("id", id);
            request.AddParameter("userId", userId);
            var restResponse = Execute<ResultResponse<bool>>(request);
            return restResponse;
        }
        #endregion DM_LoaiHinhDangKy

        #region DM_LoaiHinhTacPham
        public IRestResponse<ResultResponse<long>> DM_LoaiHinhTacPham_InsUpd(DM_LoaiHinhTacPhamMapAdd model)
        {
            var request = new RestRequest("DM/DM_LoaiHinhTacPham_InsUpd", Method.POST)
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

        public IRestResponse<ResultResponse<List<DM_LoaiHinhTacPhamMap>>> DM_LoaiHinhTacPham_List(DM_LoaiHinhTacPhamMapParam model)
        {
            var request = new RestRequest("DM/DM_LoaiHinhTacPham_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<DM_LoaiHinhTacPhamMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<DM_LoaiHinhTacPhamMapAdd>> DM_LoaiHinhTacPham_GetById(long id)
        {
            var request = new RestRequest("DM/DM_LoaiHinhTacPham_GetById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<DM_LoaiHinhTacPhamMapAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<DM_LoaiHinhTacPhamMapAdd>> DM_LoaiHinhTacPham_GetByMa(string ma)
        {
            var request = new RestRequest("DM/DM_LoaiHinhTacPham_GetByMa", Method.GET);
            request.AddParameter("ma", ma);
            var restResponse = Execute<ResultResponse<DM_LoaiHinhTacPhamMapAdd>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<bool>> DM_LoaiHinhTacPham_Delete(long id, Guid userId)
        {
            var request = new RestRequest("DM/DM_LoaiHinhTacPham_Delete", Method.GET);
            request.AddParameter("id", id);
            request.AddParameter("userId", userId);
            var restResponse = Execute<ResultResponse<bool>>(request);
            return restResponse;
        }
        #endregion DM_LoaiHinhTacPham

        #region DM_VungMien
        public IRestResponse<ResultResponse<long>> DM_VungMien_InsUpd(DM_VungMienMapAdd model)
        {
            var request = new RestRequest("DM/DM_VungMien_InsUpd", Method.POST)
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

        public IRestResponse<ResultResponse<List<DM_VungMienMap>>> DM_VungMien_List(DM_VungMienMapParam model)
        {
            var request = new RestRequest("DM/DM_VungMien_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<DM_VungMienMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<DM_VungMienMapAdd>> DM_VungMien_GetById(long id)
        {
            var request = new RestRequest("DM/DM_VungMien_GetById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<DM_VungMienMapAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<DM_VungMienMapAdd>> DM_VungMien_GetByMa(string ma)
        {
            var request = new RestRequest("DM/DM_VungMien_GetByMa", Method.GET);
            request.AddParameter("ma", ma);
            var restResponse = Execute<ResultResponse<DM_VungMienMapAdd>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<bool>> DM_VungMien_Delete(long id, Guid userId)
        {
            var request = new RestRequest("DM/DM_VungMien_Delete", Method.GET);
            request.AddParameter("id", id);
            request.AddParameter("userId", userId);
            var restResponse = Execute<ResultResponse<bool>>(request);
            return restResponse;
        }
        #endregion DM_VungMien

        #region DM_CoQuanCap
        public IRestResponse<ResultResponse<long>> DM_CoQuanCap_InsUpd(DM_CoQuanCapMapAdd model)
        {
            var request = new RestRequest("DM/DM_CoQuanCap_InsUpd", Method.POST)
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

        public IRestResponse<ResultResponse<List<DM_CoQuanCapMap>>> DM_CoQuanCap_List(DM_CoQuanCapMapParam model)
        {
            var request = new RestRequest("DM/DM_CoQuanCap_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<DM_CoQuanCapMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<DM_CoQuanCapMapAdd>> DM_CoQuanCap_GetById(long id)
        {
            var request = new RestRequest("DM/DM_CoQuanCap_GetById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<DM_CoQuanCapMapAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<DM_CoQuanCapMapAdd>> DM_CoQuanCap_GetByMa(string ma)
        {
            var request = new RestRequest("DM/DM_CoQuanCap_GetByMa", Method.GET);
            request.AddParameter("ma", ma);
            var restResponse = Execute<ResultResponse<DM_CoQuanCapMapAdd>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<bool>> DM_CoQuanCap_Delete(long id, Guid userId)
        {
            var request = new RestRequest("DM/DM_CoQuanCap_Delete", Method.GET);
            request.AddParameter("id", id);
            request.AddParameter("userId", userId);
            var restResponse = Execute<ResultResponse<bool>>(request);
            return restResponse;
        }
        #endregion DM_CoQuanCap

        #region DM_NguoiKy
        public IRestResponse<ResultResponse<long>> DM_NguoiKy_InsUpd(DM_NguoiKyMapAdd model)
        {
            var request = new RestRequest("DM/DM_NguoiKy_InsUpd", Method.POST)
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

        public IRestResponse<ResultResponse<List<DM_NguoiKyMap>>> DM_NguoiKy_List(DM_NguoiKyMapParam model)
        {
            var request = new RestRequest("DM/DM_NguoiKy_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<DM_NguoiKyMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<DM_NguoiKyMapAdd>> DM_NguoiKy_GetById(long id)
        {
            var request = new RestRequest("DM/DM_NguoiKy_GetById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<DM_NguoiKyMapAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<DM_NguoiKyMapAdd>> DM_NguoiKy_GetByMa(string ma)
        {
            var request = new RestRequest("DM/DM_NguoiKy_GetByMa", Method.GET);
            request.AddParameter("ma", ma);
            var restResponse = Execute<ResultResponse<DM_NguoiKyMapAdd>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<bool>> DM_NguoiKy_Delete(long id, Guid userId)
        {
            var request = new RestRequest("DM/DM_NguoiKy_Delete", Method.GET);
            request.AddParameter("id", id);
            request.AddParameter("userId", userId);
            var restResponse = Execute<ResultResponse<bool>>(request);
            return restResponse;
        }
        #endregion DM_NguoiKy

        #region DM_LoaiSo
        public IRestResponse<ResultResponse<long>> DM_LoaiSo_InsUpd(DM_LoaiSoMapAdd model)
        {
            var request = new RestRequest("DM/DM_LoaiSo_InsUpd", Method.POST)
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

        public IRestResponse<ResultResponse<List<DM_LoaiSoMap>>> DM_LoaiSo_List(DM_LoaiSoMapParam model)
        {
            var request = new RestRequest("DM/DM_LoaiSo_List", Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new UltimateClient.JsonSerializer()
            };
            var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            var json = JsonConvert.SerializeObject(model, settings);

            request.AddParameter("application/json", json, null, ParameterType.RequestBody);

            var restResponse = Execute<ResultResponse<List<DM_LoaiSoMap>>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<DM_LoaiSoMapAdd>> DM_LoaiSo_GetById(long id)
        {
            var request = new RestRequest("DM/DM_LoaiSo_GetById", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<DM_LoaiSoMapAdd>>(request);
            return restResponse;
        }
        public IRestResponse<ResultResponse<DM_LoaiSoMapAdd>> DM_LoaiSo_GetByLoaiNghieVuId(long loaiNghiepVuId)
        {
            var request = new RestRequest("DM/DM_LoaiSo_GetByLoaiNghieVuId", Method.GET);
            request.AddParameter("loaiNghiepVuId", loaiNghiepVuId);
            var restResponse = Execute<ResultResponse<DM_LoaiSoMapAdd>>(request);
            return restResponse;
        }

        public IRestResponse<ResultResponse<bool>> DM_LoaiSo_Delete(long id)
        {
            var request = new RestRequest("DM/DM_LoaiSo_Delete", Method.GET);
            request.AddParameter("id", id);
            var restResponse = Execute<ResultResponse<bool>>(request);
            return restResponse;
        }
        #endregion DM_LoaiSo
    }
}
