using Business.Services.Interfaces;
using Data.Core.Repositories.Interfaces;
using System.Collections.Generic;
using Core.Common.Utilities;
using log4net;
using System.Configuration;
using Business.Entities.Domain;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Data.Core.Repositories.Base;
using System.Linq;

namespace Business.Services
{
    public class UsersService : IUsersService
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(UsersService));

        #region Private Repository & contractor       
        private static INguoiDungHeThongRepository _nguoiDungHeThongRepository;
        private readonly IDM_NhomQuyenRepository _dm_NhomQuyenRepository;
        private readonly IDM_QuyenChucNangRepository _dm_QuyenChucNangRepository;
        private readonly INhatKyNguoiDungRepository _nhatKyNguoiDungRepository;
        private static string Conn = null;
        public UsersService(INguoiDungHeThongRepository nguoiDungHeThongRepository,
                            IDM_NhomQuyenRepository dm_NhomQuyenRepository,
                            IDM_QuyenChucNangRepository dm_QuyenChucNangRepository,
                             INhatKyNguoiDungRepository nhatKyNguoiDungRepository
                            )
        {
            _nguoiDungHeThongRepository = nguoiDungHeThongRepository;
            _dm_NhomQuyenRepository = dm_NhomQuyenRepository;
            _dm_QuyenChucNangRepository = dm_QuyenChucNangRepository;
            _nhatKyNguoiDungRepository = nhatKyNguoiDungRepository;
            var settings = new DataSettingsManager();
            Conn = settings.GetStringConnectMaster();

        }
        #endregion
        #region NguoiDung
        public ResultResponse<List<NguoiDungHeThongMap>> NguoiDungHeThong_List(NguoiDungHeThongMapParam model)
        {
            var response = new ResponseModel();
            var data = _nguoiDungHeThongRepository.NguoiDungHeThong_List(model, out response);
            return new ResultResponse<List<NguoiDungHeThongMap>>(response, data);
        }
        public ResultResponse<NguoiDungHeThongMapAdd> NguoiDungHeThong_GetById(long id)
        {
            var response = new ResponseModel();
            var data = _nguoiDungHeThongRepository.NguoiDungHeThong_GetById(id, out response);
            return new ResultResponse<NguoiDungHeThongMapAdd>(response, data);
        }
        public ResultResponse<NguoiDungHeThongMapAdd> NguoiDungHeThong_GetByGuid(Guid id)
        {
            var response = new ResponseModel();
            var data = _nguoiDungHeThongRepository.NguoiDungHeThong_GetByGuid(id, out response);
            return new ResultResponse<NguoiDungHeThongMapAdd>(response, data);
        }
        public ResultResponse<NguoiDungHeThongMapAdd> NguoiDungHeThong_GetByTenTaiKhoan(string tentaikhoan)
        {
            var response = new ResponseModel();
            var data = _nguoiDungHeThongRepository.NguoiDungHeThong_GetByTenTaiKhoan(tentaikhoan, out response);
            return new ResultResponse<NguoiDungHeThongMapAdd>(response, data);
        }
        public ResultResponse<NguoiDungHeThongMapAdd> NguoiDungHeThong_GetBySDT(string sodienthoai)
        {
            var response = new ResponseModel();
            var data = _nguoiDungHeThongRepository.NguoiDungHeThong_GetBySDT(sodienthoai, out response);
            return new ResultResponse<NguoiDungHeThongMapAdd>(response, data);
        }
        public ResultResponse<NguoiDungHeThongMapAdd> NguoiDungHeThong_GetByEmail(string email)
        {
            var response = new ResponseModel();
            var data = _nguoiDungHeThongRepository.NguoiDungHeThong_GetByEmail(email, out response);
            return new ResultResponse<NguoiDungHeThongMapAdd>(response, data);
        }
        public ResultResponse<int> NguoiDungHeThong_CheckMCUserId(long userId)
        {
            var response = new ResponseModel();
            var data = _nguoiDungHeThongRepository.NguoiDungHeThong_CheckMCUserId(userId, out response);
            return new ResultResponse<int>(response, data);
        }
        public ResultResponse<Guid> NguoiDungHeThong_InsUpd(NguoiDungHeThongMapAdd model)
        {
            var response = new ResponseModel();
            var data = _nguoiDungHeThongRepository.NguoiDungHeThong_InsUpd(model, out response);
            return new ResultResponse<Guid>(response, data);
        }
        public ResultResponse<int> NguoiDungHeThong_Del(Guid id, Guid lastupduserid)
        {
            var response = new ResponseModel();
            var data = _nguoiDungHeThongRepository.NguoiDungHeThong_Del(id, lastupduserid, out response);
            return new ResultResponse<int>(response, data);
        }
        public ResultResponse<int> NguoiDungHeThong_ChangePass(NguoiDungHeThongChagePass model)
        {
            var response = new ResponseModel();
            var data = _nguoiDungHeThongRepository.NguoiDungHeThong_ChangePass(model, out response);
            return new ResultResponse<int>(response, data);
        }
        public ResultResponse<long> NguoiDungHeThong_Khoa(Guid UserID, bool Khoa)
        {
            var response = new ResponseModel();
            var data = _nguoiDungHeThongRepository.NguoiDungHeThong_Khoa(UserID, Khoa, out response);
            return new ResultResponse<long>(response, data);
        }

        #endregion
        #region nguoi dung he thong nhom quyen
        public ResultResponse<NguoiDungHeThong_NhomQuyenMap> NguoiDungHeThong_NhomQuyen(Guid taikhoanguid)
        {
            var response = new ResponseModel();
            var model = new NguoiDungHeThong_NhomQuyenMap();
            var nguoidung = _nguoiDungHeThongRepository.NguoiDungHeThong_GetByGuid(taikhoanguid, out response);
            if (nguoidung != null)
            {
                var data = _nguoiDungHeThongRepository.NguoiDungHeThong_NhomQuyen(taikhoanguid, out response);
                model.TenHienThi = nguoidung.TenHienThi;
                model.NguoiDungPQId = nguoidung.TaiKhoanGuid;
                model.TenTaiKhoan = nguoidung.TenTaiKhoan;
                model.danhsachquyen = data;
            }
            return new ResultResponse<NguoiDungHeThong_NhomQuyenMap>(response, model);
        }
        public ResultResponse<long> NguoiDungHeThong_NhomQuyen_Insert(NguoiDungHeThong_NhomQuyenMapAdd model)
        {
            var response = new ResponseModel();
            var data = _nguoiDungHeThongRepository.NguoiDungHeThong_NhomQuyen_Insert(model, out response);
            return new ResultResponse<long>(response, data);
        }
        #endregion
        #region Phan quyen chuc nang
        public ResultResponse<SYS_PhanQuyenMap> PQ_PhanQuyenChucNang()
        {
            var response = new ResponseModel();
            var model = new SYS_PhanQuyenMap();
            var danhsachnhomquyen = _nguoiDungHeThongRepository.PQ_DanhSachNhomQuyen(out response);
            if (danhsachnhomquyen != null && danhsachnhomquyen.Count > 0)
            {
                foreach (var i in danhsachnhomquyen)
                {

                    string strdanhsachquyenchucnag = "";

                    var danhsachquyenchucnangbynhomquyen = _nguoiDungHeThongRepository.PQ_PhanQuyenChucNang_ByNhomQuyenID(i.IdNhomQuyen, out response);
                    if (danhsachquyenchucnangbynhomquyen != null && danhsachquyenchucnangbynhomquyen.Count > 0)
                    {
                        foreach (var j in danhsachquyenchucnangbynhomquyen)
                        {
                            strdanhsachquyenchucnag += ","+ j.QuyenChucNangID + ",";
                        }
                    }
                    //if(!string.IsNullOrEmpty(strdanhsachquyenchucnag))
                    //{
                    //    strdanhsachquyenchucnag = strdanhsachquyenchucnag.Substring(strdanhsachquyenchucnag.Length - 2, 1);
                    //}
                    i.DanhSachQuyenChucNangID = strdanhsachquyenchucnag;
                }
            }
            var danhsachquyenchucnang = _nguoiDungHeThongRepository.PQ_QuyenChucNang_List(out response);
            model.DanhSachNhomQuyen = danhsachnhomquyen;
            model.DanhSachQuyenChucNang = danhsachquyenchucnang;
            return new ResultResponse<SYS_PhanQuyenMap>(response, model);
        }
        public ResultResponse<long> PQ_PhanQuyenChucNang_Ins(List<PQ_PhanQuyenChucNang> model)
        {
            IDbConnection conns = new SqlConnection(Conn);
            conns.Open();
            IDbTransaction transCC = conns.BeginTransaction();
            var response = new ResponseModel();
            try
            {
                var data = (long)0;
                if (model != null && model.Count > 0)
                {
                    if (_nguoiDungHeThongRepository.PQ_QuyenChucNang_Del(conns, transCC, out response) < 0)
                    {
                        if (transCC != null) transCC.Rollback();
                        return new ResultResponse<long>(response, -1);
                    }
                    foreach (var i in model)
                    {
                        data = _nguoiDungHeThongRepository.PhanQuyenChucNang_InsUpd(i, conns, transCC, out response);
                    }
                }
                transCC.Commit();
                return new ResultResponse<long>(response, data);

            }
            catch (Exception ex)
            {
                if (transCC != null) transCC.Rollback();
                return new ResultResponse<long>(response, -1);
            }
            finally
            {
                if (transCC != null) transCC.Dispose();
                if (conns != null) conns.Dispose();
            }

        }
        #endregion
        #region nhat ky nguoi dung
        public ResultResponse<long> NhatKyNguoiDung_Insert(ND_NhatKyNguoiDungAdd model)
        {
            var response = new ResponseModel();
            if (model != null)
            {

                var logtype = _nhatKyNguoiDungRepository.LogTypeND_ByKeyWord(model.KeyWord, out response);
                if (logtype != null)
                {
                    HttpContextBase abstractContext = new System.Web.HttpContextWrapper(HttpContext.Current);
                    var wb = new WebHelper(abstractContext);
                    model.LogTypeID = logtype.LogTypeID;
                    model.LogTypeName = logtype.LogTypeName;
                    model.DiaChiIP = wb.GetCurrentIpAddress();
                }
            }
            var data = _nhatKyNguoiDungRepository.NhatKyNguoiDung_Insert(model, out response);
            return new ResultResponse<long>(response, data);
        }
        public ResultResponse<List<ND_NhatKyNguoiDungMap>> NhatKyNguoiDung_List(ND_NhatKyNguoiDungParam model)
        {
            var response = new ResponseModel();
            var data = _nhatKyNguoiDungRepository.NhatKyNguoiDung_List(model, out response);
            return new ResultResponse<List<ND_NhatKyNguoiDungMap>>(response, data);
        }
        public ResultResponse<int> NhatKyNguoiDung_Del(long id)
        {
            var response = new ResponseModel();
            var data = _nhatKyNguoiDungRepository.NhatKyNguoiDung_Del(id, out response);
            return new ResultResponse<int>(response, data);
        }
        public ResultResponse<int> NhatKyNguoiDung_DelLstID(List<long> lstid)
        {
            var response = new ResponseModel();
            var data = _nhatKyNguoiDungRepository.NhatKyNguoiDung_DelLstID(lstid, out response);
            var result = new ResultResponse<int>(response, data);
            return result;
        }
        public ResultResponse<List<DSLogTypeNDmap>> LogTypeND_List(LogTypeNDParam model)
        {
            var response = new ResponseModel();
            var data = _nhatKyNguoiDungRepository.LogTypeND_List(model, out response);
            var result = new ResultResponse<List<DSLogTypeNDmap>>(response, data);
            return result;
        }
        public ResultResponse<int> LogTypeND_Update(string ids, string idsUn)
        {
            var response = new ResponseModel();
            var data = _nhatKyNguoiDungRepository.LogTypeND_Update(ids, idsUn, out response);
            var result = new ResultResponse<int>(response, data);
            return result;
        }
        #endregion
        #region login
        public ResultResponse<int> ValidateUser(LoginParam model)
        {
            var response = new ResponseModel();
            if (model == null) return new ResultResponse<int>(response, -3);
            string username = model.UserName;
            string password = model.Password;
            if (String.IsNullOrEmpty(username)) return new ResultResponse<int>(response, -2);// không co user
            if (String.IsNullOrEmpty(password)) return new ResultResponse<int>(response, -1);// không có pass
            
            var salt = _nguoiDungHeThongRepository.GetSalt(username);
            if (!string.IsNullOrEmpty(salt))
            {
                password = Encrypt_Decrypt.EncodePassword(password, salt);
                var user = _nguoiDungHeThongRepository.GetUserLogin(username, password, out response);
                if (user != null && user.TaiKhoanGuid != Guid.Empty)
                {
                    return new ResultResponse<int>(response, 1);
                }
            }
            return new ResultResponse<int>(response, 0);
        }
        public ResultResponse<bool> IsActive(string username)
        {
            var response = new ResponseModel();
            var flag = _nguoiDungHeThongRepository.IsActive(username, out response);
            return new ResultResponse<bool>(response, flag);
        }
        public ResultResponse<List<BanIPMap>> BanIP_List()
        {
            var response = new ResponseModel();
            var data = _nguoiDungHeThongRepository.BanIP_List(out response);
            return new ResultResponse<List<BanIPMap>>(response, data);
        }
        public ResultResponse<BanIPMap> BanIP_byIp(string ip)
        {
            var response = new ResponseModel();
            var data = _nguoiDungHeThongRepository.BanIP_byIp(ip, out response);
            return new ResultResponse<BanIPMap>(response, data);
        }
        public ResultResponse<int> BanIP_deletebyIp(string ip)
        {
            var response = new ResponseModel();
            var data = _nguoiDungHeThongRepository.BanIP_deletebyIp(ip, out response);
            return new ResultResponse<int>(response, data);
        }
        public ResultResponse<long> BanIP_InsUpd(BanIPMap model)
        {
            var response = new ResponseModel();
            var data = _nguoiDungHeThongRepository.BanIP_InsUpd(model, out response);
            return new ResultResponse<long>(response, data);
        }
        #endregion

        #region phan quyen
        public ResultResponse<List<Roles>> GetRolesByUser(string username)
        {
            var response = new ResponseModel();
            var data = _nguoiDungHeThongRepository.GetRolesByUser(username, out response);
            return new ResultResponse<List<Roles>>(response, data);
        }
        public ResultResponse<List<Right>> GetRightsByUser(string username)
        {
            var response = new ResponseModel();
            var data = _nguoiDungHeThongRepository.GetRightsByUser(username, out response);
            return new ResultResponse<List<Right>>(response, data);
        }
        public ResultResponse<UrlViewModel> GetUrlViewModel(string DSQuyenChucNang)
        {
            var response = new ResponseModel();
            var data = _nguoiDungHeThongRepository.GetUrlViewModel(DSQuyenChucNang, out response);
            return new ResultResponse<UrlViewModel>(response, data);
        }
        #endregion

        public ResultResponse<string> ND_NguoiDungHeThong_UpdateConnectionSignalR(Guid UserID, string ConnectionId, bool IsOnline)
        {
            var response = new ResponseModel();
            var data = _nguoiDungHeThongRepository.ND_NguoiDungHeThong_UpdateConnectionSignalR(UserID, ConnectionId, IsOnline, out response);
            return new ResultResponse<string>(response, data);
        }

        public ResultResponse<int> ND_NguoiDungHeThong_CountOnline()
        {
            var response = new ResponseModel();
            var data = _nguoiDungHeThongRepository.ND_NguoiDungHeThong_CountOnline(out response);
            return new ResultResponse<int>(response, data);
        }
    }
}
