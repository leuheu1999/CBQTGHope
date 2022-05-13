using Business.Entities.DataModel;
using Core.Common.Utilities;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Business.Services.Interfaces
{
    [ServiceContract]
    public interface ITraCuuService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "Interfaces/getDanhSachGiayChungNhan")]
        ResultResponse<List<TC_GiayChungNhanMap>> TC_GiayChungNhan_List(TC_GiayChungNhanMapParam model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "Interfaces/getChiTietGiayChungNhan")]
        ResultResponse<TC_GiayChungNhanAdd> TC_GiayChungNhan_GetDetail(TC_GiayChungNhanAddParam model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "Interfaces/getDanhSachGiayChungNhanCongBao")]
        ResultResponse<List<TC_GiayChungNhanCongBaoMap>> TC_GiayChungNhanCongBao_List(TC_GiayChungNhanCongBaoParam model);
    }
}
