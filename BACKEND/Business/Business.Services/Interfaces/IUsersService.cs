using Business.Entities.Domain;
using Core.Common.Utilities;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Business.Services.Interfaces
{
    [ServiceContract]
    public interface IUsersService
    {
        #region nguoi dung
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
              UriTemplate = "ND/NguoiDungHeThong_List")]
        ResultResponse<List<NguoiDungHeThongMap>> NguoiDungHeThong_List(NguoiDungHeThongMapParam model);
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                      UriTemplate = "ND/NguoiDungHeThong_GetById?id={id}")]
        ResultResponse<NguoiDungHeThongMapAdd> NguoiDungHeThong_GetById(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                             UriTemplate = "ND/NguoiDungHeThong_GetByGuid?id={id}")]
        ResultResponse<NguoiDungHeThongMapAdd> NguoiDungHeThong_GetByGuid(Guid id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                      UriTemplate = "ND/NguoiDungHeThong_GetByTenTaiKhoan?tentaikhoan={tentaikhoan}")]
        ResultResponse<NguoiDungHeThongMapAdd> NguoiDungHeThong_GetByTenTaiKhoan(string tentaikhoan);
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                     UriTemplate = "ND/NguoiDungHeThong_GetBySDT?sodienthoai={sodienthoai}")]
        ResultResponse<NguoiDungHeThongMapAdd> NguoiDungHeThong_GetBySDT(string sodienthoai);
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                     UriTemplate = "ND/NguoiDungHeThong_GetByEmail?email={email}")]
        ResultResponse<NguoiDungHeThongMapAdd> NguoiDungHeThong_GetByEmail(string email);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                            UriTemplate = "ND/NguoiDungHeThong_CheckMCUserId?userId={userId}")]
        ResultResponse<int> NguoiDungHeThong_CheckMCUserId(long userId);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
                       UriTemplate = "ND/NguoiDungHeThong_InsUpd")]
        ResultResponse<Guid> NguoiDungHeThong_InsUpd(NguoiDungHeThongMapAdd model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                             UriTemplate = "ND/NguoiDungHeThong_Del?id={id}&lastupduserid={lastupduserid}")]
        ResultResponse<int> NguoiDungHeThong_Del(Guid id, Guid lastupduserid);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
                              UriTemplate = "ND/NguoiDungHeThong_ChangePass")]
        ResultResponse<int> NguoiDungHeThong_ChangePass(NguoiDungHeThongChagePass model);
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "ND/NguoiDungHeThong_Khoa?UserID={UserID}&Khoa={Khoa}")]
        ResultResponse<long> NguoiDungHeThong_Khoa(Guid UserID, bool Khoa);
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                            UriTemplate = "ND/NguoiDungHeThong_NhomQuyen?id={id}")]
        ResultResponse<NguoiDungHeThong_NhomQuyenMap> NguoiDungHeThong_NhomQuyen(Guid id);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
                       UriTemplate = "ND/NguoiDungHeThong_NhomQuyen_Insert")]
        ResultResponse<long> NguoiDungHeThong_NhomQuyen_Insert(NguoiDungHeThong_NhomQuyenMapAdd model);
        #endregion

        #region Phan quyen chuc nang
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
                              UriTemplate = "PQ/PQ_PhanQuyenChucNang")]
        ResultResponse<SYS_PhanQuyenMap> PQ_PhanQuyenChucNang();
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
                                     UriTemplate = "PQ/PQ_PhanQuyenChucNang_Ins")]
        ResultResponse<long> PQ_PhanQuyenChucNang_Ins(List<PQ_PhanQuyenChucNang> model);
        #endregion

        #region nhat ky nguoi dung
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
                     UriTemplate = "ND/NhatKyNguoiDung_Insert")]
        ResultResponse<long> NhatKyNguoiDung_Insert(ND_NhatKyNguoiDungAdd model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
                            UriTemplate = "ND/NhatKyNguoiDung_List")]
        ResultResponse<List<ND_NhatKyNguoiDungMap>> NhatKyNguoiDung_List(ND_NhatKyNguoiDungParam model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                                    UriTemplate = "ND/NhatKyNguoiDung_Del?id={id}")]
        ResultResponse<int> NhatKyNguoiDung_Del(long id);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
                             UriTemplate = "ND/NhatKyNguoiDung_DelLstID")]
        ResultResponse<int> NhatKyNguoiDung_DelLstID(List<long> lstid);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
                                     UriTemplate = "ND/LogTypeND_List")]
        ResultResponse<List<DSLogTypeNDmap>> LogTypeND_List(LogTypeNDParam model);
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                                             UriTemplate = "ND/LogTypeND_Update?ids={ids}&idsUn={idsUn}")]
        ResultResponse<int> LogTypeND_Update(string ids, string idsUn);

        #endregion

        #region login
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
                                            UriTemplate = "ND/ValidateUser")]
        ResultResponse<int> ValidateUser(LoginParam model);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
                                           UriTemplate = "ND/IsActive")]
        ResultResponse<bool> IsActive(string username);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
                                           UriTemplate = "ND/BanIP_List")]
        ResultResponse<List<BanIPMap>> BanIP_List();
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                                            UriTemplate = "ND/BanIP_byIp?ip={ip}")]
        ResultResponse<BanIPMap> BanIP_byIp(string ip);
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                                           UriTemplate = "ND/BanIP_deletebyIp?ip={ip}")]
        ResultResponse<int> BanIP_deletebyIp(string ip);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
                                           UriTemplate = "ND/BanIP_InsUpd")]
        ResultResponse<long> BanIP_InsUpd(BanIPMap model);
        #endregion

        #region Phan quyen
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                                                  UriTemplate = "ND/GetRolesByUser?username={username}")]
        ResultResponse<List<Roles>> GetRolesByUser(string username);
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                                                  UriTemplate = "ND/GetRightsByUser?username={username}")]
        ResultResponse<List<Right>> GetRightsByUser(string username);
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                                                  UriTemplate = "ND/GetUrlViewModel?DSQuyenChucNang={DSQuyenChucNang}")]
        ResultResponse<UrlViewModel> GetUrlViewModel(string DSQuyenChucNang);
        #endregion

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "ND/ND_NguoiDungHeThong_UpdateConnectionSignalR?UserID={UserID}&ConnectionId={ConnectionId}&IsOnline={IsOnline}")]
        ResultResponse<string> ND_NguoiDungHeThong_UpdateConnectionSignalR(Guid UserID, string ConnectionId, bool IsOnline);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "ND/ND_NguoiDungHeThong_CountOnline")]
        ResultResponse<int> ND_NguoiDungHeThong_CountOnline();
    }
}
