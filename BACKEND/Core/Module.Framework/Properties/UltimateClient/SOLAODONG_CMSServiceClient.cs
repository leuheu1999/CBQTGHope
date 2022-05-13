
using Core.Common.Utilities;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Framework.UltimateClient
{
    public class SOLAODONG_CMSServiceClient : BaseClient, IDisposable
    {
        private bool _isDisposed;

        
        public SOLAODONG_CMSServiceClient() : base(AppSetting.SLD_CMS)
        {

        }
        #region Dispose
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


        #endregion
        #region CONG VIEC TRONG DIEM
        //// Tim Kiem Co ban
        //public IRestResponse<List<CongViecTrongDiem>> CongViecTrongDiem_GetAll()
        //{
        //    var request = new RestRequest("HC/CongViecTrongDiem_GetAll", Method.GET);
        //    var restResponse = Execute<List<CongViecTrongDiem>>(request);
        //    return restResponse;
        //}
        //public IRestResponse<PagedData<CongViecTrongDiemList>> CongViecTrongDiem_ListBasic(string tuKhoa, int pageIndex, int pageSize)
        //{
        //    var request = new RestRequest("HC/CongViecTrongDiem_ListBasic", Method.GET);
        //    request.AddParameter("tuKhoa", tuKhoa);
        //    request.AddParameter("pageIndex", pageIndex);
        //    request.AddParameter("pageSize", pageSize);
        //    return Execute<PagedData<CongViecTrongDiemList>>(request);
        //}
        //// Tìm kiếm nâng cao
        //public IRestResponse<PagedData<CongViecTrongDiemList>> CongViecTrongDiem_List(string tieuDe, string ngayBatDau, string ngayKetThuc,
        //    string hienThi, int pageIndex, int pageSize)
        //{
        //    var request = new RestRequest("HC/CongViecTrongDiem_List", Method.GET);
        //    request.AddParameter("tieuDe", tieuDe);
        //    request.AddParameter("ngayBatDau", ngayBatDau);
        //    request.AddParameter("ngayKetThuc", ngayKetThuc);
        //    request.AddParameter("hienThi", hienThi);
        //    request.AddParameter("pageIndex", pageIndex);
        //    request.AddParameter("pageSize", pageSize);
        //    return Execute<PagedData<CongViecTrongDiemList>>(request);
        //}

        //// Luu
        //public IRestResponse CongViecTrongDiem_Save(CongViecTrongDiem congViecTrongDiem)
        //{
        //    var request = new RestRequest("HC/CongViecTrongDiem_Save", Method.POST)
        //    {
        //        RequestFormat = DataFormat.Json,
        //        JsonSerializer = new JsonSerializer()
        //    };
        //    var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
        //    var json = JsonConvert.SerializeObject(congViecTrongDiem, settings);

        //    request.AddParameter("application/json", json, null, ParameterType.RequestBody);

        //    return Execute(request);
        //}

        //// Xoa

        //public IRestResponse CongViecTrongDiem_DeleteByID(long congViecTrongDiemID)
        //{
        //    var request = new RestRequest("HC/CongViecTrongDiem_DeleteByID", Method.POST)
        //    {
        //        RequestFormat = DataFormat.Json,
        //        JsonSerializer = new JsonSerializer()
        //    };
        //    request.AddBody(new { congViecTrongDiemID });
        //    return Execute(request);
        //}

        //// Get By ID
        //public IRestResponse<CongViecTrongDiem> CongViecTrongDiem_GetByID(long congViecTrongDiemID)
        //{
        //    var request = new RestRequest("HC/CongViecTrongDiem_GetByID", Method.GET);
        //    request.AddParameter("congViecTrongDiemID", congViecTrongDiemID);
        //    var restResponse = Execute<CongViecTrongDiem>(request);
        //    return restResponse;
        //}
        #endregion

        
    }
}
