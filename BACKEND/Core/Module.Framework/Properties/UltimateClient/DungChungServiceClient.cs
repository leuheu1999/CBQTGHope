
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Module.Framework.UltimateClient
{
    public class DungChungServiceClient /*: BaseClient, IDisposable*/
    {
        //private bool _isDisposed;

        //public DungChungServiceClient() : base(AppSetting.MCApiBaseUrl)
        //{

        //}
        //#region Dispose
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //private void Dispose(bool disposing)
        //{
        //    if (!_isDisposed && disposing)
        //    {
        //        DisposeCore();
        //    }

        //    _isDisposed = true;
        //}

        //protected virtual void DisposeCore()
        //{
        //}
        //#endregion

        //#region DM_DONVI
        //public IRestResponse<List<DM_DonVi>> DM_DonVi_GetAll()
        //{
        //    var request = new RestRequest("DC/DonVi_GetAll", Method.GET);
        //    var restResponse = Execute<List<DM_DonVi>>(request);
        //    return restResponse;
        //}
        //public IRestResponse DM_DonVi_GetByID(long donViID)
        //{

        //    var request = new RestRequest("DC/DonVi_GetByID", Method.GET)
        //    {
        //        RequestFormat = DataFormat.Json,
        //        JsonSerializer = new JsonSerializer()
        //    };
        //    request.AddParameter("donViID", donViID);
        //    return Execute(request);
        //}
        //public IRestResponse<List<LoaiDonVi>> DM_DonVi_GetByLoaiDonViID(long loaiDonViID)
        //{

        //    var request = new RestRequest("DC/DonVi_GetByLoaiDonViID", Method.GET)
        //    {
        //        RequestFormat = DataFormat.Json,
        //        JsonSerializer = new JsonSerializer()
        //    };
        //    request.AddParameter("loaiDonViID", loaiDonViID);
        //    return Execute<List<LoaiDonVi>>(request);
        //}
        //public IRestResponse DM_DonVi_GetByCap(int cap)
        //{
            
        //    var request = new RestRequest("DC/DonVi_GetByCap", Method.GET)
        //    {
        //        RequestFormat = DataFormat.Json,
        //        JsonSerializer = new JsonSerializer()
        //    };
        //    request.AddBody(new { cap });
        //    return Execute(request);
        //}
        //public IRestResponse<List<LoaiDonVi>> LoaiDonVi_GetAll()
        //{
        //    var request = new RestRequest("DC/LoaiDonVi_GetAll", Method.GET);
        //    var restResponse = Execute<List<LoaiDonVi>>(request);
        //    return restResponse;
        //}

        ////public PagedData<DM_DonViList> DonVi_List(string tenDonVi, string tenVietTat, string maDonVi,
        ////    string maHanhChinh, long donViCapChaID, int cap, int pageIndex, int pageSize)
        ////{
        ////    int totalItems;
        ////    var datas = _donViRepository.DungChung_DM_DonVi_List(tenDonVi, tenVietTat, maDonVi,
        ////                maHanhChinh, donViCapChaID, cap, pageIndex, pageSize, out totalItems).ToList();
        ////    return new PagedData<DM_DonViList>
        ////    {
        ////        Data = datas,
        ////        PageIndex = pageIndex,
        ////        PageSize = pageSize,
        ////        TotalItems = totalItems
        ////    };
        ////}

        //public IRestResponse<PagedData<DM_DonViList>> DonVi_List(string tenDonVi, string tenVietTat, string maDonVi,
        //    string maHanhChinh, long donViCapChaID, int cap, int pageIndex, int pageSize)
        //{
        //    var request = new RestRequest("DC/DonVi_List", Method.GET);
        //    request.AddParameter("tenDonVi", tenDonVi);
        //    request.AddParameter("tenVietTat", tenVietTat);
        //    request.AddParameter("maDonVi", maDonVi);
        //    request.AddParameter("maHanhChinh", maHanhChinh);
        //    request.AddParameter("donViCapChaID", donViCapChaID);
        //    request.AddParameter("cap", cap);
        //    request.AddParameter("pageIndex", 1);
        //    request.AddParameter("pageSize", int.MaxValue);
        //    return Execute<PagedData<DM_DonViList>>(request);
        //}
        //#endregion

        //#region CHỨC VỤ

        //public IRestResponse<List<ChucVu>> ChucVu_GetAll()
        //{
        //    var request = new RestRequest("DC/ChucVu_GetAll", Method.GET);
        //    var restResponse = Execute<List<ChucVu>>(request);
        //    return restResponse;
        //}
        //public IRestResponse<PagedData<ChucVuList>> ChucVu_List(string ten, string ma, string hoatDong, int pageIndex, int pageSize)
        //{
        //    var request = new RestRequest("DC/ChucVu_List", Method.GET);
        //    request.AddParameter("ten", ten);
        //    request.AddParameter("ma", ma);
        //    request.AddParameter("hoatDong", hoatDong);
        //    request.AddParameter("pageIndex", 1);
        //    request.AddParameter("pageSize", int.MaxValue);
        //    return Execute<PagedData<ChucVuList>>(request);
        //}
        //#endregion
        //public IRestResponse DM_To_GetByPhongBanID(long phongBanID)
        //{

        //    var request = new RestRequest("DC/To_GetByPhongBanID", Method.GET)
        //    {
        //        RequestFormat = DataFormat.Json,
        //        JsonSerializer = new JsonSerializer()
        //    };
        //    request.AddParameter("phongBanID", phongBanID);
        //    return Execute(request);
        //}
        //public IRestResponse<List<PhuongXa>> PhuongXa_GetByQuanHuyen(long quanHuyenID)
        //{
        //    var request = new RestRequest("DC/PhuongXa_GetByQuanHuyenID", Method.GET)
        //    {
        //        RequestFormat = DataFormat.Json,
        //        JsonSerializer = new JsonSerializer()
        //    };
        //    request.AddParameter("quanHuyenID", quanHuyenID);
        //    var restResponse = Execute<List<PhuongXa>>(request);
        //    return restResponse;
        //}
        //public IRestResponse<List<QuocGia>> QuocGia_GetAll()
        //{
        //    var request = new RestRequest("DC/QuocGia_GetAll", Method.GET);
        //    var restResponse = Execute<List<QuocGia>>(request);
        //    return restResponse;
        //}
        //public IRestResponse<List<Duong>> Duong_GetAll()
        //{
        //    var request = new RestRequest("DC/Duong_GetAll", Method.GET);
        //    var restResponse = Execute<List<Duong>>(request);
        //    return restResponse;
        //}
        //public IRestResponse<List<Duong>> Duong_GetByPhuongID(string PhuongID)
        //{

        //    var request = new RestRequest("DC/Duong_GetByPhuongID", Method.GET)
        //    {
        //        RequestFormat = DataFormat.Json,
        //        JsonSerializer = new JsonSerializer()
        //    };
        //    request.AddParameter("PhuongID", PhuongID);
        //    var restResponse = Execute<List<Duong>>(request);
        //    return restResponse;
        //}
    }
}
