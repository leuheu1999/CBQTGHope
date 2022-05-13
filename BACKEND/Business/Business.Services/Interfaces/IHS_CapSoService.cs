using Business.Entities.Domain;
using Core.Common.Utilities;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Business.Services.Interfaces
{
    [ServiceContract]
    public interface IHS_CapSoService
    {
        #region TT_CapQuyen
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "HSCS/TT_CapQuyen_CheckCapSo?hoSoID={hoSoID}")]
        ResultResponse<int> TT_CapQuyen_CheckCapSo(long hoSoID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "HSCS/TT_CapQuyen_List")]
        ResultResponse<List<TT_CapQuyenMap>> TT_CapQuyen_List(TT_CapQuyenParam model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "HSCS/TT_CapQuyen_GetByHoSoId?hoSoID={hoSoID}")]
        ResultResponse<TT_CapQuyenAdd> TT_CapQuyen_GetByHoSoId(long hoSoID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "HSCS/TT_CapQuyen_UpdTinhTrang")]
        ResultResponse<long> TT_CapQuyen_UpdTinhTrang(TT_CapQuyenAddTrangThai model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "HSCS/TT_CapQuyen_GetLoaiNghiepVuId?hoSoID={hoSoID}")]
        ResultResponse<int> TT_CapQuyen_GetLoaiNghiepVuId(long hoSoID);
        #endregion TT_CapQuyen

        #region HS_CapSo
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "HSCS/HS_CapSo_GenSo?loaiNghiepVuID={loaiNghiepVuID}")]
        ResultResponse<HS_GenSo> HS_CapSo_GenSo(int loaiNghiepVuID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "HSCS/DVC_HS_CapSo_GenSo?loaiNghiepVuID={loaiNghiepVuID}")]
        ResultResponse<HS_GenSo> DVC_HS_CapSo_GenSo(int loaiNghiepVuID);
        #endregion HS_CapSo

        #region HS_HoSo
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "HS/HS_HoSo_CheckCapSo?hoSoID={hoSoID}")]
        ResultResponse<int> HS_HoSo_CheckCapSo(long hoSoID);
        #endregion
    }
}
