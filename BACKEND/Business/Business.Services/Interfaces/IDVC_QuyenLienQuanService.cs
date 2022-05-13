using Business.Entities.Domain;
using Core.Common.Utilities;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Business.Services.Interfaces
{
    [ServiceContract]
    public interface IDVC_QuyenLienQuanService
    {
        #region DVC_QLQ_QuyenLienQuan
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DVC/DVC_QLQ_QuyenLienQuan_List")]
        ResultResponse<List<DVC_QLQ_QuyenLienQuanMap>> DVC_QLQ_QuyenLienQuan_List(DVC_QLQ_QuyenLienQuanParam model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DVC/DVC_QLQ_QuyenLienQuan_ById?id={id}")]
        ResultResponse<DVC_QLQ_QuyenLienQuanAdd> DVC_QLQ_QuyenLienQuan_ById(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DVC/DVC_QLQ_QuyenLienQuan_GetStt")]
        ResultResponse<long> DVC_QLQ_QuyenLienQuan_GetStt();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DVC/DVC_QLQ_QuyenLienQuan_InsUpd")]
        ResultResponse<long> DVC_QLQ_QuyenLienQuan_InsUpd(DVC_QLQ_QuyenLienQuanAdd model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DVC/DVC_QLQ_QuyenLienQuan_Del?quyenLienQuanID={quyenLienQuanID}&userID={userID}")]
        ResultResponse<int> DVC_QLQ_QuyenLienQuan_Del(long quyenLienQuanID, Guid userID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DVC/DVC_QLQ_QuyenLienQuan_SearchGCN")]
        ResultResponse<List<DVC_QLQ_GiayChungNhanMap>> DVC_QLQ_QuyenLienQuan_SearchGCN(DVC_QLQ_GiayChungNhanParam model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DVC/DVC_QLQ_QuyenLienQuan_CapSo")]
        ResultResponse<long> DVC_QLQ_QuyenLienQuan_CapSo(DVC_QLQ_QuyenLienQuan_CapSo model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DVC/DVC_QLQ_QuyenLienQuan_CheckSoTT?id={id}&soTT={soTT}")]
        ResultResponse<int> DVC_QLQ_QuyenLienQuan_CheckSoTT(long id, string soTT);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "DVC/DVC_QLQ_QuyenLienQuan_CheckSoGCN?id={id}&soGCN={soGCN}")]
        ResultResponse<int> DVC_QLQ_QuyenLienQuan_CheckSoGCN(long id, string soGCN);
        #endregion DVC_QLQ_QuyenLienQuan
    }
}
