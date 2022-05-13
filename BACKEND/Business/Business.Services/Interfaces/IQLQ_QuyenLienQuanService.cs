using Business.Entities.Domain;
using Core.Common.Utilities;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Business.Services.Interfaces
{
    [ServiceContract]
    public interface IQLQ_QuyenLienQuanService
    {
        #region QLQ_QuyenLienQuan
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QLQ/QLQ_QuyenLienQuan_List")]
        ResultResponse<List<QLQ_QuyenLienQuanMap>> QLQ_QuyenLienQuan_List(QLQ_QuyenLienQuanParam model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QLQ/QLQ_QuyenLienQuan_ById?id={id}")]
        ResultResponse<QLQ_QuyenLienQuanAdd> QLQ_QuyenLienQuan_ById(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QLQ/QLQ_QuyenLienQuan_GetStt")]
        ResultResponse<long> QLQ_QuyenLienQuan_GetStt();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QLQ/QLQ_QuyenLienQuan_InsUpd")]
        ResultResponse<long> QLQ_QuyenLienQuan_InsUpd(QLQ_QuyenLienQuanAdd model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QLQ/QLQ_QuyenLienQuan_Del?quyenLienQuanID={quyenLienQuanID}&userID={userID}")]
        ResultResponse<int> QLQ_QuyenLienQuan_Del(long quyenLienQuanID, Guid userID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QLQ/QLQ_QuyenLienQuan_SearchGCN")]
        ResultResponse<List<QLQ_GiayChungNhanMap>> QLQ_QuyenLienQuan_SearchGCN(QLQ_GiayChungNhanParam model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QLQ/QLQ_QuyenLienQuan_SearchSTT")]
        ResultResponse<List<QLQ_SoThuTuMap>> QLQ_QuyenLienQuan_SearchSTT(QLQ_SoThuTuParam model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QLQ/QLQ_QuyenLienQuan_CapSo")]
        ResultResponse<long> QLQ_QuyenLienQuan_CapSo(QLQ_QuyenLienQuan_CapSo model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QLQ/QLQ_QuyenLienQuan_CheckSoTT?id={id}&soTT={soTT}")]
        ResultResponse<int> QLQ_QuyenLienQuan_CheckSoTT(long id, string soTT);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QLQ/QLQ_QuyenLienQuan_CheckSoGCN?id={id}&soGCN={soGCN}")]
        ResultResponse<int> QLQ_QuyenLienQuan_CheckSoGCN(long id, string soGCN);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QLQ/QLQ_QuyenLienQuan_Duyet?id={id}&loaiNghiepVuID={loaiNghiepVuID}&isDuyet={isDuyet}")]
        ResultResponse<int> QLQ_QuyenLienQuan_Duyet(long id, int loaiNghiepVuID, bool isDuyet);
        #endregion QLQ_QuyenLienQuan

        #region QLQ_CapLai
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QLQ/QLQ_CapLai_ById?id={id}")]
        ResultResponse<QLQ_CapLaiAdd> QLQ_CapLai_ById(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QLQ/QLQ_CapLai_ByHoSoId?id={id}")]
        ResultResponse<QLQ_CapLaiAdd> QLQ_CapLai_ByHoSoId(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QLQ/QLQ_CapLai_ByQuyenLQId?id={id}")]
        ResultResponse<QLQ_CapLaiAdd> QLQ_CapLai_ByQuyenLQId(long id);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QLQ/QLQ_CapLai_InsUpd")]
        ResultResponse<long> QLQ_CapLai_InsUpd(QLQ_CapLaiAdd model);
        #endregion QLQ_CapLai

        #region QLQ_ThuHoi
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QLQ/QLQ_ThuHoi_ById?id={id}")]
        ResultResponse<QLQ_ThuHoiAdd> QLQ_ThuHoi_ById(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QLQ/QLQ_ThuHoi_ByHoSoId?id={id}")]
        ResultResponse<QLQ_ThuHoiAdd> QLQ_ThuHoi_ByHoSoId(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QLQ/QLQ_ThuHoi_ByQuyenLQId?id={id}")]
        ResultResponse<QLQ_ThuHoiAdd> QLQ_ThuHoi_ByQuyenLQId(long id);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QLQ/QLQ_ThuHoi_InsUpd")]
        ResultResponse<long> QLQ_ThuHoi_InsUpd(QLQ_ThuHoiAdd model);
        #endregion QLQ_ThuHoi

        #region QLQ_ChuyenChuSoHuu
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QLQ/QLQ_ChuyenChuSoHuu_ById?id={id}")]
        ResultResponse<QLQ_ChuyenChuSoHuuAdd> QLQ_ChuyenChuSoHuu_ById(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QLQ/QLQ_ChuyenChuSoHuu_ByHoSoId?id={id}")]
        ResultResponse<QLQ_ChuyenChuSoHuuAdd> QLQ_ChuyenChuSoHuu_ByHoSoId(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QLQ/QLQ_ChuyenChuSoHuu_ByQuyenLQId?id={id}")]
        ResultResponse<QLQ_ChuyenChuSoHuuAdd> QLQ_ChuyenChuSoHuu_ByQuyenLQId(long id);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QLQ/QLQ_ChuyenChuSoHuu_InsUpd")]
        ResultResponse<long> QLQ_ChuyenChuSoHuu_InsUpd(QLQ_ChuyenChuSoHuuAdd model);
        #endregion QLQ_ChuyenChuSoHuu

        #region QLQ_CapDoi
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QLQ/QLQ_CapDoi_ById?id={id}")]
        ResultResponse<QLQ_CapDoiAdd> QLQ_CapDoi_ById(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QLQ/QLQ_CapDoi_ByHoSoId?id={id}")]
        ResultResponse<QLQ_CapDoiAdd> QLQ_CapDoi_ByHoSoId(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QLQ/QLQ_CapDoi_ByQuyenLQId?id={id}")]
        ResultResponse<QLQ_CapDoiAdd> QLQ_CapDoi_ByQuyenLQId(long id);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QLQ/QLQ_CapDoi_InsUpd")]
        ResultResponse<long> QLQ_CapDoi_InsUpd(QLQ_CapDoiAdd model);
        #endregion QLQ_CapDoi
    }
}
