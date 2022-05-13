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
using System.ServiceModel.Web;

namespace Business.Services
{
    public class AuthService : IAuthService
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(UsersService));

        #region Private Repository & contractor       
        private static INguoiDungHeThongRepository _nguoiDungHeThongRepository;
        private static string Conn = null;
        private string _key = "";
        public AuthService(INguoiDungHeThongRepository nguoiDungHeThongRepository)
        {
            _nguoiDungHeThongRepository = nguoiDungHeThongRepository;
            Conn = ConfigurationManager.ConnectionStrings["BaoDienTuMaster.ConnString"].ConnectionString;
            IncomingWebRequestContext headers = WebOperationContext.Current.IncomingRequest;
            _key = headers.Headers["Authorization"] != null ? headers.Headers["Authorization"] : "";

        }
        #endregion

        #region Login
        public ResultResponse<TokenObject> ValidateUser(LoginParam model)
        {
            if (_key == HeaderKey.Key)
            {
                try
                {
                    if (model == null)
                    {
                        return new ResultResponse<TokenObject>(new ResponseModel("Không tìm thấy tài khoản.", 404), new TokenObject());
                    }
                    string username = model.UserName;
                    string password = model.Password;
                    if (String.IsNullOrEmpty(username))
                    {
                        return new ResultResponse<TokenObject>(new ResponseModel("Tên đăng nhập rỗng.", 404), new TokenObject());
                    }
                    if (String.IsNullOrEmpty(password))
                    {
                        return new ResultResponse<TokenObject>(new ResponseModel("Mật khẩu rỗng.", 404), new TokenObject());
                    }

                    var salt = _nguoiDungHeThongRepository.GetSalt(username);
                    if (!string.IsNullOrEmpty(salt))
                    {
                        var response = new ResponseModel();
                        password = Encrypt_Decrypt.EncodePassword(password, salt);
                        var user = _nguoiDungHeThongRepository.GetUserLogin(username, password, out response);
                        if (user != null && user.TaiKhoanGuid != Guid.Empty)
                        {
                            // update token theo time
                            var Token = user.TaiKhoanGuid + "tokenExpireAt" + DateTime.Now.Ticks + "AuthorizationName" + user.TenTaiKhoan + "AuthorizationPass" + password;
                            var TokenEndcode = base64Encode(Token);
                            var kq = _nguoiDungHeThongRepository.ND_NguoiDungHeThong_UpdateToken(user.TaiKhoanGuid, TokenEndcode, out response);
                            if (kq == 1)
                            {

                                return new ResultResponse<TokenObject>(response,new TokenObject{ Token = TokenEndcode});
                            }

                        }
                    }

                    return new ResultResponse<TokenObject>(new ResponseModel("Có lỗi xảy ra.", 404), new TokenObject());
                }
                catch (Exception ex)
                {
                    return new ResultResponse<TokenObject>(new ResponseModel("Có lỗi xảy ra.", 404), new TokenObject());
                }
            }
            return new ResultResponse<TokenObject>(new ResponseModel("Lỗi xác thực", 403), new TokenObject());
        }
        public ResultResponse<bool> ValidateToken(TokenObject model)
        {
            if (_key == HeaderKey.Key)
            {
                try
                {
                    if(model==null)
                    {
                        return new ResultResponse<bool>(new ResponseModel("Không tìm thấy tài khoản.", 404), false);
                    }
                    string token = model.Token;
                    if (string.IsNullOrEmpty(token))
                    {
                        return new ResultResponse<bool>(new ResponseModel("Không tìm thấy tài khoản.", 404), false);
                    }
                    var TokenDecode = base64Decode(token);
                    if(string.IsNullOrEmpty(TokenDecode))
                    {
                        return new ResultResponse<bool>(new ResponseModel("Có lỗi xảy ra.", 403), false);
                    }
                    string Pass = "";
                    string UserName = "";
                    string Time = "";
                    string TaiKhoanID = "";
                    var arrPass = TokenDecode.Split(new[] { "AuthorizationPass" }, StringSplitOptions.None);
                    if(arrPass!=null && arrPass.Length>1)
                    {
                        Pass = arrPass[1];
                    }
                    var arrUserName = arrPass[0].Split(new[] { "AuthorizationName" }, StringSplitOptions.None);
                    if (arrUserName != null && arrUserName.Length > 1)
                    {
                        UserName = arrUserName[1];
                    }
                    var arrTime = arrUserName[0].Split(new[] { "tokenExpireAt" }, StringSplitOptions.None);
                    if (arrTime != null && arrTime.Length > 1)
                    {
                        Time = arrTime[1];
                        TaiKhoanID = arrTime[0];
                    }
                    var response = new ResponseModel();
                    var data = _nguoiDungHeThongRepository.GetUserLogin(UserName, Pass, out response);
                    if(data!=null )
                    {
                        return new ResultResponse<bool>(response, true);
                    }
                    else
                    {
                        return new ResultResponse<bool>(new ResponseModel("Token không chính xác.", 404), false);
                    }
                }
                catch (Exception ex)
                {
                    return new ResultResponse<bool>(new ResponseModel("Có lỗi xảy ra.", 404), false);
                }
            }
            return new ResultResponse<bool>(new ResponseModel("Lỗi xác thực", 403), false);
        }
        public string base64Encode(string data)
        {

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(data);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public string base64Decode(string sData)
        {

            var base64EncodedBytes = System.Convert.FromBase64String(sData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        #endregion

    }
}
