using Business.Entities.Domain;
using Core.Common.Utilities;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Business.Services.Interfaces
{
    [ServiceContract]
    public interface IQTG_QuyenTacGiaService
    {

        #region QTG_QuyenTacGia
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QTG/QTG_QuyenTacGia_List")]
        ResultResponse<List<QTG_QuyenTacGiaMap>> QTG_QuyenTacGia_List(QTG_QuyenTacGiaParam model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QTG/QTG_QuyenTacGia_ById?id={id}")]
        ResultResponse<QTG_QuyenTacGiaAdd> QTG_QuyenTacGia_ById(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QTG/QTG_QuyenTacGia_GetStt")]
        ResultResponse<long> QTG_QuyenTacGia_GetStt();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QTG/QTG_QuyenTacGia_InsUpd")]
        ResultResponse<long> QTG_QuyenTacGia_InsUpd(QTG_QuyenTacGiaAdd model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QTG/QTG_QuyenTacGia_Del?quyenTacGiaID={quyenTacGiaID}&userID={userID}")]
        ResultResponse<int> QTG_QuyenTacGia_Del(long quyenTacGiaID, Guid userID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QTG/QTG_QuyenTacGia_SearchGCN")]
        ResultResponse<List<QTG_GiayChungNhanMap>> QTG_QuyenTacGia_SearchGCN(QTG_GiayChungNhanParam model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QTG/QTG_QuyenTacGia_SearchSTT")]
        ResultResponse<List<QTG_SoThuTuMap>> QTG_QuyenTacGia_SearchSTT(QTG_SoThuTuParam model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QTG/QTG_QuyenTacGia_CapSo")]
        ResultResponse<long> QTG_QuyenTacGia_CapSo(QTG_QuyenTacGia_CapSo model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QTG/QTG_QuyenTacGia_CheckSoTT?id={id}&soTT={soTT}")]
        ResultResponse<int> QTG_QuyenTacGia_CheckSoTT(long id, string soTT);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QTG/QTG_QuyenTacGia_CheckSoGCN?id={id}&soGCN={soGCN}")]
        ResultResponse<int> QTG_QuyenTacGia_CheckSoGCN(long id, string soGCN);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QTG/QTG_QuyenTacGia_Duyet?id={id}&loaiNghiepVuID={loaiNghiepVuID}&isDuyet={isDuyet}")]
        ResultResponse<int> QTG_QuyenTacGia_Duyet(long id, int loaiNghiepVuID, bool isDuyet);
        #endregion QTG_QuyenTacGia

        #region QTG_CapLai
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QTG/QTG_CapLai_ById?id={id}")]
        ResultResponse<QTG_CapLaiAdd> QTG_CapLai_ById(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QTG/QTG_CapLai_ByHoSoId?id={id}")]
        ResultResponse<QTG_CapLaiAdd> QTG_CapLai_ByHoSoId(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QTG/QTG_CapLai_ByQuyenTGId?id={id}")]
        ResultResponse<QTG_CapLaiAdd> QTG_CapLai_ByQuyenTGId(long id);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QTG/QTG_CapLai_InsUpd")]
        ResultResponse<long> QTG_CapLai_InsUpd(QTG_CapLaiAdd model);
        #endregion QTG_CapLai

        #region QTG_ThuHoi
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QTG/QTG_ThuHoi_ById?id={id}")]
        ResultResponse<QTG_ThuHoiAdd> QTG_ThuHoi_ById(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QTG/QTG_ThuHoi_ByHoSoId?id={id}")]
        ResultResponse<QTG_ThuHoiAdd> QTG_ThuHoi_ByHoSoId(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QTG/QTG_ThuHoi_ByQuyenTGId?id={id}")]
        ResultResponse<QTG_ThuHoiAdd> QTG_ThuHoi_ByQuyenTGId(long id);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QTG/QTG_ThuHoi_InsUpd")]
        ResultResponse<long> QTG_ThuHoi_InsUpd(QTG_ThuHoiAdd model);
        #endregion QTG_ThuHoi

        #region QTG_ChuyenChuSoHuu
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QTG/QTG_ChuyenChuSoHuu_ById?id={id}")]
        ResultResponse<QTG_ChuyenChuSoHuuAdd> QTG_ChuyenChuSoHuu_ById(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QTG/QTG_ChuyenChuSoHuu_ByHoSoId?id={id}")]
        ResultResponse<QTG_ChuyenChuSoHuuAdd> QTG_ChuyenChuSoHuu_ByHoSoId(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QTG/QTG_ChuyenChuSoHuu_ByQuyenTGId?id={id}")]
        ResultResponse<QTG_ChuyenChuSoHuuAdd> QTG_ChuyenChuSoHuu_ByQuyenTGId(long id);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QTG/QTG_ChuyenChuSoHuu_InsUpd")]
        ResultResponse<long> QTG_ChuyenChuSoHuu_InsUpd(QTG_ChuyenChuSoHuuAdd model);
        #endregion QTG_ChuyenChuSoHuu

        #region QTG_CapDoi
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QTG/QTG_CapDoi_ById?id={id}")]
        ResultResponse<QTG_CapDoiAdd> QTG_CapDoi_ById(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QTG/QTG_CapDoi_ByHoSoId?id={id}")]
        ResultResponse<QTG_CapDoiAdd> QTG_CapDoi_ByHoSoId(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QTG/QTG_CapDoi_ByQuyenTGId?id={id}")]
        ResultResponse<QTG_CapDoiAdd> QTG_CapDoi_ByQuyenTGId(long id);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "QTG/QTG_CapDoi_InsUpd")]
        ResultResponse<long> QTG_CapDoi_InsUpd(QTG_CapDoiAdd model);
        #endregion QTG_CapDoi
    }
}
