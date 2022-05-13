using Business.Entities.Domain;
using Core.Common.Utilities;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Business.Services.Interfaces
{
    [ServiceContract]
    public interface IBC_ThongKeService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "BCTK/BC_ThongKeGiayChungNhan_List")]
        ResultResponse<List<BC_ThongKeGiayChungNhanMap>> BC_ThongKeGiayChungNhan_List(BC_ThongKeGiayChungNhanParam model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "BCTK/BC_CongBao_List")]
        ResultResponse<List<BC_CongBaoMap>> BC_CongBao_List(BC_CongBaoParam model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "BCTK/BC_BaoCaoTacPham_List")]
        ResultResponse<List<BC_BaoCaoTacPhamMap>> BC_BaoCaoTacPham_List(BC_BaoCaoTacPhamParam model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "BCTK/BC_BaoCaoTongHopHoatDong_List")]
        ResultResponse<List<BC_BaoCaoTongHopHoatDongMap>> BC_BaoCaoTongHopHoatDong_List(BC_BaoCaoTongHopHoatDongParam model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "BCTK/BC_SoGiayChungNhanBanQuyen_List")]
        ResultResponse<List<BC_SoGiayChungNhanBanQuyenMap>> BC_SoGiayChungNhanBanQuyen_List(BC_SoGiayChungNhanBanQuyenParam model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "BCTK/BC_ThongKeHoSoQuyenTacGia_Dashboard")]
        ResultResponse<List<BC_ThongKeHoSoQuyenTacGiaMap>> BC_ThongKeHoSoQuyenTacGia_Dashboard(BC_ThongKeHoSoQuyenTacGiaParam model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "BCTK/BC_ThongKeHoSoQuyenLienQuan_Dashboard")]
        ResultResponse<List<BC_ThongKeHoSoQuyenLienQuanMap>> BC_ThongKeHoSoQuyenLienQuan_Dashboard(BC_ThongKeHoSoQuyenLienQuanParam model);
    }
}
