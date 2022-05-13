using Business.Entities.Domain;
using Core.Common.Utilities;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Business.Services.Interfaces
{
    [ServiceContract]
    public interface IDVC_QuyenTacGiaService
    {

        #region DVC_QTG_QuyenTacGia
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DVC/DVC_QTG_QuyenTacGia_List")]
        ResultResponse<List<DVC_QTG_QuyenTacGiaMap>> DVC_QTG_QuyenTacGia_List(DVC_QTG_QuyenTacGiaParam model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DVC/DVC_QTG_QuyenTacGia_ById?id={id}")]
        ResultResponse<DVC_QTG_QuyenTacGiaAdd> DVC_QTG_QuyenTacGia_ById(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DVC/DVC_QTG_QuyenTacGia_GetStt")]
        ResultResponse<long> DVC_QTG_QuyenTacGia_GetStt();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DVC/DVC_QTG_QuyenTacGia_InsUpd")]
        ResultResponse<long> DVC_QTG_QuyenTacGia_InsUpd(DVC_QTG_QuyenTacGiaAdd model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DVC/DVC_QTG_QuyenTacGia_Del?quyenTacGiaID={quyenTacGiaID}&userID={userID}")]
        ResultResponse<int> DVC_QTG_QuyenTacGia_Del(long quyenTacGiaID, Guid userID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DVC/DVC_QTG_QuyenTacGia_SearchGCN")]
        ResultResponse<List<DVC_QTG_GiayChungNhanMap>> DVC_QTG_QuyenTacGia_SearchGCN(DVC_QTG_GiayChungNhanParam model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DVC/DVC_QTG_QuyenTacGia_CapSo")]
        ResultResponse<long> DVC_QTG_QuyenTacGia_CapSo(DVC_QTG_QuyenTacGia_CapSo model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DVC/DVC_QTG_QuyenTacGia_CheckSoTT?id={id}&soTT={soTT}")]
        ResultResponse<int> DVC_QTG_QuyenTacGia_CheckSoTT(long id, string soTT);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DVC/DVC_QTG_QuyenTacGia_CheckSoGCN?id={id}&soGCN={soGCN}")]
        ResultResponse<int> DVC_QTG_QuyenTacGia_CheckSoGCN(long id, string soGCN);
        #endregion DVC_QTG_QuyenTacGia
    }
}
