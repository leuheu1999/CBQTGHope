using Business.Entities.Domain;
using CMS.Admin.Common;
using CMS.Admin.Constaints;
using CMS.Admin.Controllers;
using CMS.Admin.Models;
using Core.Common.Utilities;
using Module.Framework;
using Module.Framework.Common;
using Module.Framework.Interfaces;
using Module.Framework.Utilities;
using PagedList;
using System;
using System.DirectoryServices.Protocols;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace QuanTriModules.Controllers
{
    public class NguoiDungHeThongController : BaseController
    {
        private UsersServiceClient _UsersSrv;
        private DungChungServiceClient _DungChungSrv;
        private DungChung_1CuaServiceClient _DungChung1CuaSrv;
        private int _pageSize;
        public NguoiDungHeThongController()
        {
            var pageSize = DungChung.GetKeyCauHinhHeThong("SoHangHienThi");
            if (!int.TryParse(pageSize, out this._pageSize))
                this._pageSize = 10;
        }
        // GET: NguoiDungHeThongController
        [CustomAuthorize(RightName = CookieRight.DM_NguoiDungHeThongController_Index)]

        public ActionResult Index()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var result = new NguoiDungHeThongViewModel();
            _UsersSrv = new UsersServiceClient();
            try
            {
                result.Search = new NguoiDungHeThongMapParam();
                result.Search.PageIndex = 1;
                result.Search.PageSize = this._pageSize;
                var temp = _UsersSrv.NguoiDungHeThong_List(result.Search);
                if (temp.Data != null && temp.Data.resultObject != null && temp.Data.resultObject.Any())
                {
                    DungChung.ghinhatkynguoidung("Xem danh sách người dùng hệ thống",
                                        "NguoiDungHeThongController",
                                        "Index", "View");
                    result.Items = temp.Data.resultObject.ToPagedList(result.Search.PageIndex, this._pageSize);
                }
                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("NguoiDungHeThongController/Index error:" + ex.Message, ex, Request);
                return View(result);
            }

        }

        [HttpPost]
        public ActionResult ListNguoiDung(NguoiDungHeThongMapParam model)
        {
            try
            {
                _UsersSrv = new UsersServiceClient();
                //model.PageSize = this._pageSize;
                var temp = _UsersSrv.NguoiDungHeThong_List(model);
                var result = new NguoiDungHeThongViewModel();
                if (temp.Data != null && temp.Data.resultObject != null && temp.Data.resultObject.Any())
                {
                    DungChung.ghinhatkynguoidung("Xem danh sách người dùng hệ thống",
                                        "NguoiDungHeThongController",
                                        "Index", "View");
                    result.Items = temp.Data.resultObject.ToPagedList(1, model.PageSize > 0 ? model.PageSize : this._pageSize);
                }
                return PartialView("_PartialList", result.Items);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("NguoiDungHeThongController/ListNguoiDung error:" + ex.Message, ex, Request);
                return PartialView("_PartialList", null);
            }
        }

        [HttpPost]
        [CustomAuthorize(RightName = CookieRight.DM_NguoiDungHeThongController_ThemMoiNguoiDung)]
        [CheckForAntiForgeryToken]
        public ActionResult ThemMoiNguoiDung(NguoiDungHeThongMapAdd model)
        {
            try
            {
                if (DungChung.CheckTimeDN() == false)
                {
                    return RedirectToAction("LogOff", "NguoiDungHeThong");
                }
                bool status = false;
                if (model != null)
                {
                    if (model.MatKhau != model.NhapLaimatKhau)
                    {
                        return Json(new { status = status, checkMK = false });

                    }
                    _UsersSrv = new UsersServiceClient();
                    //kiem tra tên tài khoản co tont tai ma chua
                    if (model.TaiKhoanGuid == Guid.Empty)
                    {
                        var checkMa = _UsersSrv.NguoiDungHeThong_GetByTenTaiKhoan(model.TenTaiKhoan);
                        if (checkMa.Data != null && checkMa.Data.resultObject != null)
                            return Json(new { status = status, checkMa = true });
                        //check trung sdt da dang ky
                        var checksdt = _UsersSrv.NguoiDungHeThong_GetBySDT(model.DienThoai);
                        if (checksdt.Data != null && checksdt.Data.resultObject != null)
                            return Json(new { status = status, checkSDT = true });
                        //check trung email da dan ky
                        var checkemail = _UsersSrv.NguoiDungHeThong_GetByEmail(model.Email);
                        if (checkemail.Data != null && checkemail.Data.resultObject != null)
                            return Json(new { status = status, checkEmail = true });
                        var salt = Encrypt_Decrypt.GenerateSalt();
                        model.Salt = salt;
                        model.MatKhau = Encrypt_Decrypt.EncodePassword(model.MatKhau, salt);
                        model.TaiKhoanGuid = Guid.NewGuid();
                    }
                    else
                    {
                        var temp = _UsersSrv.NguoiDungHeThong_GetByGuid(model.TaiKhoanGuid);
                        if (temp.Data != null && temp.Data.resultObject != null)
                        {
                            // check ten tai khoan
                            if (temp.Data.resultObject.TenTaiKhoan != model.TenTaiKhoan)
                            {
                                var checkMa = _UsersSrv.NguoiDungHeThong_GetByTenTaiKhoan(model.TenTaiKhoan);
                                if (checkMa.Data != null && checkMa.Data.resultObject != null)
                                {
                                    return Json(new { status = status, checkMa = true });
                                }
                            }
                            // check SDT
                            if (temp.Data.resultObject.DienThoai != model.DienThoai)
                            {
                                var checkSDT = _UsersSrv.NguoiDungHeThong_GetBySDT(model.DienThoai);
                                if (checkSDT.Data != null && checkSDT.Data.resultObject != null)
                                {
                                    return Json(new { status = status, checkSDT = true });
                                }
                            }
                            // check Email
                            if (temp.Data.resultObject.Email != model.Email)
                            {
                                var checkemail = _UsersSrv.NguoiDungHeThong_GetByEmail(model.Email);
                                if (checkemail.Data != null && checkemail.Data.resultObject != null)
                                {
                                    return Json(new { status = status, checkEmail = true });
                                }
                            }
                        }
                    }
                    model.TenTaiKhoan = DungChung.SxxEndcodeText(model.TenTaiKhoan);
                    model.NgaySinh = DungChung.SxxEndcodeText(model.NgaySinh);
                    model.HoVaTen = DungChung.SxxEndcodeText(model.HoVaTen);
                    model.CMND = DungChung.SxxEndcodeText(model.CMND);
                    model.DiaChi = DungChung.SxxEndcodeText(model.DiaChi);
                    model.DienThoai = DungChung.SxxEndcodeText(model.DienThoai);
                    model.TenHienThi = DungChung.SxxEndcodeText(model.TenHienThi);
                    model.NgayCapCMND = DungChung.SxxEndcodeText(model.NgayCapCMND);
                    model.NoiCapCMND = DungChung.SxxEndcodeText(model.NoiCapCMND);

                    var result = _UsersSrv.NguoiDungHeThong_InsUpd(model);
                    if (result.Data != null && result.Data.resultObject != Guid.Empty)
                    {
                        status = true;
                        //log nhat ky nguoi dung
                        if (model.TaiKhoanGuid != Guid.Empty)
                        {
                            DungChung.ghinhatkynguoidung("Cập nhật người dùng",
                                           "NguoiDungHeThongController",
                                           "ThemMoiNguoiDung", "Update");
                        }
                        else
                        {
                            model.TaiKhoanGuid = result.Data.resultObject;
                            DungChung.ghinhatkynguoidung("Thêm mới người dùng",
                                           "NguoiDungHeThongController",
                                           "ThemMoiNguoiDung", "Create");
                        }
                    }
                }
                return Json(new { status = status, checkMa = false, taikhoanguid = model.TaiKhoanGuid });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("NguoiDungHeThongController/ThemMoiNguoiDung error:" + ex.Message, ex, Request);
                return Json(new { status = false });
            }
        }

        [CustomAuthorize(RightName = CookieRight.DM_NguoiDungHeThongController_ThemMoiNguoiDung)]
        public ActionResult Update(string id)
        {
            var model = new NguoiDungHeThongMapAdd();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var idl = Guid.Parse(id);
                    _UsersSrv = new UsersServiceClient();
                    var temp = _UsersSrv.NguoiDungHeThong_GetByGuid(idl);
                    if (temp.Data != null && temp.Data.resultObject != null)
                        model = temp.Data.resultObject;
                    ViewBag.Title = "Cập nhật thông tin người dùng hệ thống";
                }
                else
                {
                    ViewBag.Title = "Thêm mới người dùng hệ thống";
                }
                return View(model);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("NguoiDungHeThongController/CapNhatNguoiDung error:" + ex.Message, ex, Request);
                return View(model);
            }
        }

        #region Change Password
        public ActionResult DoiMatKhau(string id)
        {
            var model = new NguoiDungHeThongMapAdd();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var idl = Guid.Parse(id);
                    _UsersSrv = new UsersServiceClient();
                    var temp = _UsersSrv.NguoiDungHeThong_GetByGuid(idl);
                    if (temp.Data != null && temp.Data.resultObject != null)

                        model = temp.Data.resultObject;
                }
                model.MatKhau = "";
                return PartialView(model);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("NguoiDungHeThongController/CapNhatNguoiDung error:" + ex.Message, ex, Request);
                return PartialView(model);
            }
        }

        [HttpPost]
        [CustomAuthorize]
        public ActionResult DoiMatKhau(NguoiDungHeThongChagePass model)
        {
            try
            {
                if (DungChung.CheckTimeDN() == false)
                {
                    return RedirectToAction("LogOff", "NguoiDungHeThong");
                }
                bool status = false;
                if (model != null)
                {
                    if (model.MatKhau != model.NhapLaimatKhau)
                    {
                        return Json(new { status = status, checkMK = false });
                    }
                    _UsersSrv = new UsersServiceClient();
                    model.LastUpdUserID = new Guid();
                    var salt = Encrypt_Decrypt.GenerateSalt();
                    model.Salt = salt;
                    model.MatKhau = Encrypt_Decrypt.EncodePassword(model.MatKhau, salt);
                    var result = _UsersSrv.NguoiDungHeThong_ChangePass(model);
                    if (result.Data != null && result.Data.resultObject > 0)
                    {
                        status = true;
                        DungChung.ghinhatkynguoidung("Đổi mật khẩu người dùng",
                                      "NguoiDungHeThongController",
                                      "DoiMatKhau", "Update");
                    }
                }
                return Json(new { status = status, checkMa = false });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("NguoiDungHeThongController/DoiMatKhau error:" + ex.Message, ex, Request);
                return Json(new { status = false });
            }
        }
        #endregion

        [CustomAuthorize(RightName = CookieRight.DM_NguoiDungHeThongController_Delete)]
        public ActionResult Delete(string id)
        {
            try
            {
                if (DungChung.CheckTimeDN() == false)
                {
                    return RedirectToAction("LogOff", "NguoiDungHeThong");
                }
                bool status = false;
                try
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        Guid idnd = Guid.Parse(id);
                        Guid lastupduserid = new Guid();
                        using (_UsersSrv = new UsersServiceClient())
                        {
                            var tempList = _UsersSrv.NguoiDungHeThong_Del(idnd, lastupduserid);
                            if (tempList.Data != null && tempList.Data.resultObject > 0)
                            {
                                status = true;
                                // ghi nhật ký người dùng
                                DungChung.ghinhatkynguoidung("Xóa người dùng",
                                      "NguoiDungHeThongController",
                                      "Delete", "Delete");
                            }
                        }
                    }
                    return Json(new { status = status });
                }
                catch (Exception ex)
                {
                    DungChung.ghiloghethong("NguoiDungHeThongController/Delete error:" + ex.Message, ex, Request);
                    return Json(new { status = status });
                }
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("NguoiDungHeThongController/Delete error:" + ex.Message, ex, Request);
                return Json(false);
            }
        }

        #region phan quyen
        public ActionResult PhanQuyen(string id)
        {
            var model = new NguoiDungHeThong_NhomQuyenMap();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var idl = Guid.Parse(id);

                    _UsersSrv = new UsersServiceClient();
                    var temp = _UsersSrv.NguoiDungHeThong_NhomQuyen(idl);
                    if (temp.Data != null && temp.Data.resultObject != null)
                        model = temp.Data.resultObject;


                }

                return PartialView(model);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("NguoiDungHeThongController/PhanQuyen error:" + ex.Message, ex, Request);
                return PartialView(model);
            }
        }
        [HttpPost]
        [CustomAuthorize(RightName = CookieRight.DM_NguoiDungHeThongController_PhanQuyen)]

        public ActionResult PhanQuyen(NguoiDungHeThong_NhomQuyenMapAdd model)
        {
            try
            {
                if (DungChung.CheckTimeDN() == false)
                {
                    return RedirectToAction("LogOff", "NguoiDungHeThong");
                }
                bool status = false;
                if (model != null)
                {
                    _UsersSrv = new UsersServiceClient();

                    var result = _UsersSrv.NguoiDungHeThong_NhomQuyen_Insert(model);
                    if (result.Data != null && result.Data.resultObject > 0)
                    {
                        status = true;
                        // ghi nhật ký người dùng
                        DungChung.ghinhatkynguoidung("Phân quyền người dùng",
                                    "NguoiDungHeThongController",
                                    "PhanQuyen", "Update");
                    }
                }
                return Json(new { status = status, checkMa = false });
            }
            catch (Exception ex)
            {
                //nhat ký hệ thống
                DungChung.ghiloghethong("NguoiDungHeThongController/PhanQuyen error:" + ex.Message, ex, Request);
                return Json(new { status = false });
            }
        }
        #endregion

        #region Login
        [AllowAnonymous]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Login(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = Regex.Replace(returnUrl, "#", "%23", RegexOptions.Multiline);
                ViewBag.ReturnUrl = returnUrl;

                if (User != null && User.Identity.IsAuthenticated)
                {
                    var us = LoginManager.GetCurrentUser();
                    DungChung.ghinhatkynguoidung("Đăng nhập",
                                   "NguoiDungHeThongController",
                                   "Login", "Login");
                    return Redirect(returnUrl);
                }
            }
            else if (User != null && User.Identity.IsAuthenticated)
            {
                var us = LoginManager.GetCurrentUser();
                var urlViewModel = LoginManager.GetDefaultUrlFromPermission();
                if (urlViewModel == null || string.IsNullOrEmpty(urlViewModel.Action) || string.IsNullOrEmpty(urlViewModel.Controller))
                    return RedirectToAction("AccessDenied", "NguoiDungHeThong");
                return RedirectToAction(urlViewModel.Action, urlViewModel.Controller);
            }
            _DungChungSrv = new DungChungServiceClient();
            var temp = _DungChungSrv.GetAllCauHinh();
            var cauhinhdonvi = new DonViMap();
            if (temp.Data != null && temp.Data.resultObject != null)
            {
                cauhinhdonvi = temp.Data.resultObject.donvi;
            }
            ViewBag.thongtindonvi = cauhinhdonvi;
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Login(LoginParam model, string returnUrl)
        {
            _DungChungSrv = new DungChungServiceClient();
            _DungChung1CuaSrv = new DungChung_1CuaServiceClient();
            _UsersSrv = new UsersServiceClient();
            var temp = _DungChungSrv.GetAllCauHinh();
            var cauhinhdonvi = new DonViMap();
            var cauhinhhethong = new CauHinhHeThongMap();
            if (temp.Data != null && temp.Data.resultObject != null)
            {
                cauhinhdonvi = temp.Data.resultObject.donvi;
                cauhinhhethong = temp.Data.resultObject.cauhinhhethong;
            }
            ViewBag.thongtindonvi = cauhinhdonvi;
            if (ModelState.IsValid)
            {
                if (LoginManager.IsBlockIP())
                {
                    ModelState.AddModelError("", "Vui lòng liên hệ quản trị để đăng nhập lại");
                    var cookies = new HttpCookie("IsBlock", "3");
                    cookies.Expires = DateTime.Now.AddYears(1);
                    Response.Cookies.Add(cookies);
                    return View(model);
                }
                var date = Request.Cookies.AllKeys.Contains("IsMinute") ? DateTime.Parse(LoginManager.MD5Hash(Request.Cookies["IsMinute"].Value)) : LoginManager.GetTimeBlockIP();
                if (DateTime.Now <= date)
                {
                    ModelState.AddModelError("", "Vui lòng đăng nhập lại sau 15p");
                    var authTicket = new FormsAuthenticationTicket(date.ToString(), false, 15);
                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    var cookies = new HttpCookie("IsMinute", encTicket.ToString());
                    cookies.Expires = DateTime.Now.AddYears(1);
                    Response.Cookies.Add(cookies);
                    return View(model);
                }
                // login LDAP or local site
                string IsLoginLDAP = WebConfigurationManager.AppSettings["IsLoginLDAP"];
                if (IsLoginLDAP != "1") // login local site
                {
                    var lg = _DungChung1CuaSrv.EncryptedAccount(model.UserName, model.Password);
                    if (lg == null || lg == "")
                    {
                        ModelState.AddModelError("", "Lỗi không xác định");
                        return View(model);
                    }
                    else
                    {
                        var tokenLogin = _DungChung1CuaSrv.ValidateUser(lg);
                        if (tokenLogin.StatusCode == -29)
                        {
                            ModelState.AddModelError("", "Tài khoản chưa kích hoạt");
                            return View(model);
                        }
                        if (tokenLogin.StatusCode == -15)
                        {
                            ModelState.AddModelError("", "Sai User name hoặc password");
                            return View(model);
                        }
                        if (tokenLogin == null || tokenLogin.Token == null || tokenLogin.Token == "")
                        {
                            ModelState.AddModelError("", "Lỗi không xác định");
                            return View(model);
                        }
                        if (tokenLogin.StatusCode == 0)
                        {
                            var infoUser = _DungChung1CuaSrv.GetThongTinUser(tokenLogin.Token, model.UserName);
                            if (infoUser != null && infoUser.StatusCode == 0)
                            {
                                var userId = infoUser.Data.UserID > 0 ? infoUser.Data.UserID : 0;
                                var checkUser = _UsersSrv.NguoiDungHeThong_CheckMCUserId((long)userId);
                                if (checkUser != null && checkUser.Data.resultObject == 0)
                                {
                                    NguoiDungHeThongMapAdd nguoiDung = new NguoiDungHeThongMapAdd();
                                    nguoiDung.TaiKhoanGuid = Guid.NewGuid();
                                    if (infoUser.Data.UserID != null)
                                        nguoiDung.MCUserID = (long)infoUser.Data.UserID;
                                    nguoiDung.TenTaiKhoan = infoUser.Data.UserName;
                                    nguoiDung.TenHienThi = infoUser.Data.UserHoTen;
                                    nguoiDung.HoVaTen = infoUser.Data.UserHoTen;
                                    if (infoUser.Data.UserPhongBanID != null)
                                        nguoiDung.MCPhongBanID = (long)infoUser.Data.UserPhongBanID;
                                    nguoiDung.MCTenPhongBan = infoUser.Data.UserTenPhongBan;
                                    if (infoUser.Data.UserDonViID != null)
                                        nguoiDung.MCDonViID = (long)infoUser.Data.UserDonViID;
                                    nguoiDung.MCTenDonVi = infoUser.Data.UserTenDonVi;
                                    nguoiDung.DienThoai = infoUser.Data.SoDT;
                                    nguoiDung.DiaChi = infoUser.Data.ChoOhienTai;
                                    nguoiDung.Email = infoUser.Data.Email;
                                    if (infoUser.Data.ChucVuID != null)
                                        nguoiDung.MCChucVuID = (int)infoUser.Data.ChucVuID;
                                    nguoiDung.MCTenChucVu = infoUser.Data.TenChucVu;
                                    nguoiDung.Khoa = true;
                                    var result = _UsersSrv.NguoiDungHeThong_InsUpd(nguoiDung);
                                    if (result.Data == null || result.Data.resultObject == Guid.Empty)
                                    {
                                        ModelState.AddModelError("", "Thêm thông tin người dùng không thành công");
                                        return View(model);
                                    }
                                }
                            }

                            LoginManager.RemoveIPBan();
                            if (Request.Cookies.AllKeys.Contains("IsMinute"))
                            {
                                var cookie1 = Request.Cookies["IsMinute"];
                                cookie1.Expires = DateTime.Now.AddDays(-1);
                                Response.Cookies.Add(cookie1);
                            };
                            if (Request.Cookies.AllKeys.Contains("WrongNum"))
                            {
                                var cookie1 = Request.Cookies["WrongNum"];
                                cookie1.Expires = DateTime.Now.AddDays(-1);
                                Response.Cookies.Add(cookie1);
                            };
                            if (!_UsersSrv.IsActive(model.UserName).Data.resultObject)
                            {
                                ModelState.AddModelError("", "Tài khoản đã bị khóa");
                                return View(model);
                            }
                            var principal = SetupFormsAuthTicket(model.UserName, 1, true);
                            if (principal == null)
                            {
                                ModelState.AddModelError("", "Không tìm thấy đường dẫn mặc định cho user này");
                            }
                            else
                            {
                                DungChung.ghinhatkynguoidung("Đăng nhập hệ thống",
                                                    "NguoiDungHeThongController",
                                                    "Login", "Login");
                                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                                && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                                {
                                    return Redirect(returnUrl);
                                }
                                else
                                {
                                    return RedirectToAction(principal.DefaultAction, principal.DefaultController);
                                }
                            }
                        }
                        var wrongNum = LoginManager.SaveIPBan(Response.Cookies);
                        var cookie = new HttpCookie("WrongNum", wrongNum.ToString());
                        cookie.Expires = DateTime.Now.AddYears(1);
                        Response.Cookies.Add(cookie);
                        if (wrongNum > cauhinhhethong.SoLanDuocDangNhapSai)
                        {
                            var cookie1 = new HttpCookie("IsMinute", DateTime.Now.AddMinutes(15).ToString());
                            cookie.Expires = DateTime.Now.AddYears(1);
                            Response.Cookies.Add(cookie);
                        }
                    }
                }
                else // login ldap
                {
                    string Domain = WebConfigurationManager.AppSettings["LDomain"];
                    string LUrl = WebConfigurationManager.AppSettings["Lurl"];
                    string Port = WebConfigurationManager.AppSettings["Lport"];
                    var f = validateUserByBind(model.UserName, model.Password, Domain, LUrl, int.Parse(Port));
                    if (f == false)
                    {
                        ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                        return View(model);
                    }
                    else
                    {
                        var salt = Encrypt_Decrypt.GenerateSalt();
                        var MatKhau = Encrypt_Decrypt.EncodePassword(model.Password, salt);
                        var modelChagePass = new NguoiDungHeThongChagePass();
                        var CheckUser = _UsersSrv.NguoiDungHeThong_GetByTenTaiKhoan(model.UserName);// neu dang nhap bang LDAP thi check user trong he thong, neu co thi update lai pass = pass ldap, neu k cso thi tao moi ng dung
                        if (CheckUser != null && CheckUser.Data != null && CheckUser.Data.resultObject != null) // co user trong he thong thi update pass
                        {
                            modelChagePass.TaiKhoanGuid = CheckUser.Data.resultObject.TaiKhoanGuid;
                            modelChagePass.MatKhau = MatKhau;
                            modelChagePass.Salt = salt;
                            modelChagePass.LastUpdUserID = Guid.Empty;
                            var result = _UsersSrv.NguoiDungHeThong_ChangePass(modelChagePass);
                            if (result == null || result.Data == null || result.Data.resultObject <= 0)
                            {
                                ModelState.AddModelError("", "Có lỗi xảy ra!");
                                return View(model);
                            }
                        }
                        else // khong co trong he thong thi tao moi nguoi dung
                        {
                            var modelAdd = new NguoiDungHeThongMapAdd();
                            modelAdd.TaiKhoanGuid = Guid.NewGuid();
                            modelAdd.TenTaiKhoan = model.UserName;
                            modelAdd.MatKhau = MatKhau;
                            modelAdd.Salt = salt;
                            modelAdd.Khoa = true;
                            var result = _UsersSrv.NguoiDungHeThong_InsUpd(modelAdd);
                            if (result == null || result.Data == null || result.Data.resultObject == null)
                            {
                                ModelState.AddModelError("", "Có lỗi xảy ra!");
                                return View(model);
                            }
                            var modelPhanQUyen = new NguoiDungHeThong_NhomQuyenMapAdd();
                            modelPhanQUyen.NguoiDungID = modelAdd.TaiKhoanGuid;
                            modelPhanQUyen.NhomQuyenIDs = "1";
                            _UsersSrv.NguoiDungHeThong_NhomQuyen_Insert(modelPhanQUyen);
                        }
                        // đăng nhập

                        var lg = _UsersSrv.ValidateUser(model);
                        if (lg == null || lg.Data == null)
                        {
                            ModelState.AddModelError("", "Lỗi không xác định");
                            return View(model);
                        }
                        var principal = SetupFormsAuthTicket(model.UserName, lg.Data.resultObject, true);
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                           && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "CauHinhHeThong");
                        }
                    }
                }
                //else if (wrongNum == 4)
                //{
                //    var cookie1 = new HttpCookie("IsBlock", "3");
                //    cookie.Expires = DateTime.Now.AddYears(1);
                //    Response.Cookies.Add(cookie);
                //}
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
            }
            else
            {
                //var message = string.Join(", ", ModelState.Values
                //                    .SelectMany(v => v.Errors)
                //                    .Select(e => e.ErrorMessage));
                //ModelState.AddModelError("", message);
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không được rỗng");
            }
            ViewBag.ReturnUrl = returnUrl;
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        public ActionResult LogOff()
        {
            DungChung.ghinhatkynguoidung("Đăng xuất hệ thống",
                                  "NguoiDungHeThongController",
                                  "LogOff", "LogOff");

            FormsAuthentication.SignOut();
            Session.Abandon();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            SessionStateSection sessionStateSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
            HttpCookie cookie2 = new HttpCookie(sessionStateSection.CookieName, "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);

            if (Request.Cookies["WebsiteID"] != null)
            {
                Response.Cookies["WebsiteID"].Expires = DateTime.Now.AddDays(-1);
            }
            if (Request.Cookies["NgonNguID"] != null)
            {
                Response.Cookies["NgonNguID"].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction(actionName: "Login", controllerName: "NguoiDungHeThong");
        }

        private CustomPrincipalSerializedModel SetupFormsAuthTicket(string userName, int ngonNguID, bool persistanceFlag)
        {
            _UsersSrv = new UsersServiceClient();
            var user = _UsersSrv.NguoiDungHeThong_GetByTenTaiKhoan(userName);
            if (user == null || user.Data == null || user.Data.resultObject == null)
            {
                return null;
            }
            var provider = new CustomRoleProvider();
            var userId = user.Data.resultObject.TaiKhoanGuid;
            //var userRoles = String.Join(",", System.Web.Security.Roles.GetRolesForUser(user.Data.resultObject.TenTaiKhoan));
            var rights = provider.GetRightsForUser(user.Data.resultObject.TenTaiKhoan);
            var codes = provider.GetCodeCookieForUser(user.Data.resultObject.TenTaiKhoan);
            var userRights = String.Join(",", codes);
            var role = provider.GetRoleLeastByUser(user.Data.resultObject.TenTaiKhoan);
            //var idBranch = BachKhoa.Queries.Common.BaseQuery.GetIdBranchUser(userId);
            var userRoles = String.Join(",", LoginManager.GetRolesForUser(user.Data.resultObject.TenTaiKhoan));// lay danh sach nhom quyen cua user
            UrlViewModel urlViewModel = null;
            bool admin = false;
            if (!string.IsNullOrEmpty(userRoles))
            {
                var arr = userRoles.Split(',');
                if (arr.Length > 0)
                {
                    foreach (var i in arr)
                    {
                        if (i == "1")
                        {
                            admin = true;
                            break;
                        }
                    }
                }
            }
            if (rights.Any())
            {
                //dtt009873
                string strR = "|";
                foreach (var i in rights)
                {
                    strR += i + "|";
                }
                var r = rights.Select(Guid.Parse);
                var temp = _UsersSrv.GetUrlViewModel(strR);
                if (temp != null && temp.Data != null && temp.Data.resultObject != null)
                {
                    urlViewModel = temp.Data.resultObject;
                }
            }
            else if (admin == true)
            {
                urlViewModel = new UrlViewModel();
                urlViewModel.Controller = "CauHinhHeThong";
                urlViewModel.Action = "Index";

            }
            if (urlViewModel != null)
            {
                var serializeModel = new CustomPrincipalSerializedModel
                {
                    UserId = userId.ToString(),
                    UserName = user.Data.resultObject.TenTaiKhoan,
                    Right = "," + userRights + ",",
                    UserFullName = user.Data.resultObject.TenHienThi,
                    NgonNguID = ngonNguID,
                    Role = userRoles.ToUpper(),
                    DefaultController = urlViewModel.Controller,
                    DefaultAction = urlViewModel.Action,
                    UserPortalID = user.Data.resultObject.MCUserID,
                    DonViID = user.Data.resultObject.MCDonViID,
                    PhongBanID = user.Data.resultObject.MCPhongBanID,
                    WorkingHour = false
                };

                var serializer = new JavaScriptSerializer();
                string userData = serializer.Serialize(serializeModel);

                var authTicket = new FormsAuthenticationTicket(
                    1, userName, DateTime.Now, DateTime.Now.AddHours(8), false, userData);
                string encTicket = FormsAuthentication.Encrypt(authTicket);
                //int maxByteSize = 4000;
                //if (System.Text.ASCIIEncoding.ASCII.GetByteCount(encTicket) > maxByteSize)
                //{
                //    // Raise the alarm that the cookie is going to get rejected by the browser  
                //}  

                #region Check refresh cookie đăng nhập
                var _saltLogin = Encrypt_Decrypt.GenerateSalt();
                string _antiForgeryTokenLogin = DungChung.GetAntiForgeryToken();
                var _cookieLogin = Encrypt_Decrypt.EncodePassword(_antiForgeryTokenLogin, _saltLogin);

                var faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                faCookie.HttpOnly = true;
                Response.Cookies.Add(faCookie);

                var faCookie_update = new HttpCookie(FormsAuthentication.FormsCookieName + "NIGOL", _cookieLogin);
                faCookie_update.HttpOnly = true;
                Response.Cookies.Add(faCookie_update);
                Session[FormsAuthentication.FormsCookieName + "NIGOL"] = _cookieLogin;
                #endregion

                #region cookie ajax
                var _salt = Encrypt_Decrypt.GenerateSalt();
                string _antiForgeryToken = DungChung.GetAntiForgeryToken();
                var _cookieAjax = Encrypt_Decrypt.EncodePassword(_antiForgeryToken, _salt);
                var _antiForgeryConfig = new HttpCookie(AntiForgeryConfig.CookieName, _antiForgeryToken);
                _antiForgeryConfig.HttpOnly = true;
                Response.Cookies.Add(_antiForgeryConfig);

                var _antiForgeryConfig_update = new HttpCookie(AntiForgeryConfig.CookieName + "XAJA", _cookieAjax);
                _antiForgeryConfig_update.HttpOnly = true;
                Response.Cookies.Add(_antiForgeryConfig_update);
                Session[AntiForgeryConfig.CookieName + "XAJA"] = _cookieAjax;
                #endregion

                Session["IsExpired"] = false;
                return serializeModel;
            }
            return null;
        }
        #endregion
        [HttpPost]
        public ActionResult UploadFile()
        {
            bool status = false;
            string url = "";
            try
            {

                HttpPostedFile myFile = System.Web.HttpContext.Current.Request.Files["UploadedFiles"];
                if (Request.Files.Count > 0)
                {
                    var root = Path.Combine(Server.MapPath('~' + ""));
                    var fileUpload = Request.Files[0];
                    //check file
                    var fileName = Path.GetFileName(myFile.FileName);
                    var contentType = MimeMapping.GetMimeMapping(fileName);
                    var fileExtension = Path.GetExtension(fileName);
                    if (!String.IsNullOrEmpty(fileExtension))
                        fileExtension = fileExtension.ToLowerInvariant();
                    if (FileControl.GetImageFileExtensions().Contains(fileExtension))
                    {
                        if (FileControl.GetImageMimeTypes().Contains(contentType))
                        {
                            var fileInfo = FileControl.SaveFileUpload(root, ParameterSetting.PathFileUpload + "\\" + ParameterSetting.PathFileAlbumHinhAnh + "\\", fileUpload);
                            var urlupload = ParameterSetting.RootUrlupload + fileInfo.Url;
                            url = urlupload;
                            status = true;
                        }
                        else
                        {
                            status = false;
                        }
                    }
                    else
                    {
                        status = false;
                    }
                }
                return Json(new { status = status, url = url });
            }
            catch (Exception ex)
            {
                return Json(new { status = false });
            }
        }
        public ActionResult IndexSuaThongTin(string doipass)
        {
            var us = LoginManager.GetCurrentUser();
            var model = new NguoiDungHeThongMapAdd();
            try
            {
                ViewBag.controller = "NguoiDungHeThong";
                ViewBag.action = "Login";
                if (us != null)
                {
                    var idl = Guid.Parse(us.UserId);
                    _UsersSrv = new UsersServiceClient();
                    var temp = _UsersSrv.NguoiDungHeThong_GetByGuid(idl);
                    if (temp.Data != null && temp.Data.resultObject != null)
                        model = temp.Data.resultObject;
                    ViewBag.Title = "Sửa thông tin cá nhân";
                    if (!string.IsNullOrEmpty(doipass))
                    {
                        ViewBag.doipass = "1";
                    }
                    else
                    {
                        ViewBag.doipass = "0";
                    }
                    var provider = new CustomRoleProvider();
                    var rights = provider.GetRightsForUser(us.UserName);
                    UrlViewModel urlViewModel = null;
                    if (us.Right.Any())
                    {
                        //dtt009873
                        string strR = "";
                        foreach (var i in rights)
                        {
                            strR += i + "|";
                        }
                        var r = rights.Select(Guid.Parse);
                        var temp1 = _UsersSrv.GetUrlViewModel(strR);
                        if (temp1 != null && temp1.Data != null && temp1.Data.resultObject != null)
                        {
                            urlViewModel = temp1.Data.resultObject;
                        }
                        ViewBag.controller = urlViewModel.Controller;
                        ViewBag.action = urlViewModel.Action;
                    }
                }

                return View(model);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("NguoiDungHeThongController/IndexSuaThongTin error:" + ex.Message, ex, Request);
                return View(model);
            }
        }
        [CheckForAntiForgeryToken]

        public ActionResult SuaThongTinCaNhan(NguoiDungHeThongMapAdd model)
        {
            try
            {
                bool status = false;
                if (model != null)
                {
                    if (model.MatKhau != model.NhapLaimatKhau)
                    {
                        return Json(new { status = status, checkMK = false });

                    }
                    _UsersSrv = new UsersServiceClient();
                    //kiem tra tên tài khoản co tont tai ma chua
                    if (model.TaiKhoanGuid == Guid.Empty)
                    {
                        var checkMa = _UsersSrv.NguoiDungHeThong_GetByTenTaiKhoan(model.TenTaiKhoan);
                        if (checkMa.Data != null && checkMa.Data.resultObject != null)
                            return Json(new { status = status, checkMa = true });
                        model.TaiKhoanGuid = Guid.NewGuid();
                    }
                    else
                    {
                        var temp = _UsersSrv.NguoiDungHeThong_GetByGuid(model.TaiKhoanGuid);
                        if (temp.Data != null && temp.Data.resultObject != null)
                        {
                            if (temp.Data.resultObject.TenTaiKhoan != model.TenTaiKhoan)
                            {
                                var checkMa = _UsersSrv.NguoiDungHeThong_GetByTenTaiKhoan(model.TenTaiKhoan);
                                if (checkMa.Data != null && checkMa.Data.resultObject != null)
                                {
                                    return Json(new { status = status, checkMa = true });
                                }
                            }
                        }
                    }
                    var salt = Encrypt_Decrypt.GenerateSalt();
                    model.Salt = salt;
                    model.MatKhau = Encrypt_Decrypt.EncodePassword(model.MatKhau, salt);
                    model.TenTaiKhoan = DungChung.SxxEndcodeText(model.TenTaiKhoan);
                    model.NgaySinh = DungChung.SxxEndcodeText(model.NgaySinh);
                    model.HoVaTen = DungChung.SxxEndcodeText(model.HoVaTen);
                    model.CMND = DungChung.SxxEndcodeText(model.CMND);
                    model.DiaChi = DungChung.SxxEndcodeText(model.DiaChi);
                    model.DienThoai = DungChung.SxxEndcodeText(model.DienThoai);
                    model.TenHienThi = DungChung.SxxEndcodeText(model.TenHienThi);
                    model.NgayCapCMND = DungChung.SxxEndcodeText(model.NgayCapCMND);
                    model.NoiCapCMND = DungChung.SxxEndcodeText(model.NoiCapCMND);
                    var result = _UsersSrv.NguoiDungHeThong_InsUpd(model);
                    if (result.Data != null && result.Data.resultObject != Guid.Empty)
                    {
                        status = true;
                        //log nhat ky nguoi dung
                        if (model.TaiKhoanGuid != Guid.Empty)
                        {
                            DungChung.ghinhatkynguoidung("Sửa thông tin cá nhân",
                                           "NguoiDungHeThongController",
                                           "SuaThongTinCaNhan", "Update");
                        }
                    }
                }
                return Json(new { status = status, checkMa = false, taikhoanguid = model.TaiKhoanGuid });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("NguoiDungHeThongController/SuaThongTinCaNhan error:" + ex.Message, ex, Request);
                return Json(new { status = false });
            }
        }
        public ActionResult DoiMatKhauCaNhan(string id)
        {
            var model = new NguoiDungHeThongMapAdd();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var idl = Guid.Parse(id);
                    _UsersSrv = new UsersServiceClient();
                    var temp = _UsersSrv.NguoiDungHeThong_GetByGuid(idl);
                    if (temp.Data != null && temp.Data.resultObject != null)

                        model = temp.Data.resultObject;
                }
                model.MatKhau = "";
                return PartialView(model);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("NguoiDungHeThongController/DoiMatKhauCaNhan error:" + ex.Message, ex, Request);
                return PartialView(model);
            }
        }
        [HttpPost]
        public ActionResult DoiMatKhauCaNhan(NguoiDungHeThongChagePass model)
        {
            try
            {
                bool status = false;
                if (model != null)
                {
                    _UsersSrv = new UsersServiceClient();
                    var temp = _UsersSrv.NguoiDungHeThong_GetByGuid(model.TaiKhoanGuid);
                    if (temp.Data != null && temp.Data.resultObject != null)
                    {
                        if (model.MatKhau != model.NhapLaimatKhau)
                        {
                            return Json(new { status = status, checkMK = false });
                        }
                        _UsersSrv = new UsersServiceClient();
                        model.LastUpdUserID = new Guid();
                        var salt = Encrypt_Decrypt.GenerateSalt();
                        model.Salt = salt;
                        model.MatKhau = Encrypt_Decrypt.EncodePassword(model.MatKhau, salt);
                        model.MatKhauCu = Encrypt_Decrypt.EncodePassword(model.MatKhauCu, temp.Data.resultObject.Salt);
                        if (temp.Data.resultObject.MatKhau == model.MatKhauCu)
                        {
                            var result = _UsersSrv.NguoiDungHeThong_ChangePass(model);
                            if (result.Data != null && result.Data.resultObject > 0)
                            {
                                status = true;
                                DungChung.ghinhatkynguoidung("Đổi mật khẩu cá nhân",
                                              "NguoiDungHeThongController",
                                              "DoiMatKhauCaNhan", "Update");
                            }
                        }
                    }
                }
                return Json(new { status = status, checkMa = false });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("NguoiDungHeThongController/DoiMatKhauCaNhan error:" + ex.Message, ex, Request);
                return Json(new { status = false });
            }
        }
        [CustomAuthorize(RightName = CookieRight.DM_NguoiDungHeThongController_Khoa)]
        [CustomAuthorize(RightName = CookieRight.DM_NguoiDungHeThongController_MoKhoa)]
        public ActionResult Khoa_MoKhoaTaiKhoa(string UserID
            , bool Khoa)
        {
            try
            {
                if (DungChung.CheckTimeDN() == false)
                {
                    return RedirectToAction("LogOff", "NguoiDungHeThong");
                }
                bool status = false;
                if (!string.IsNullOrEmpty(UserID))
                {
                    using (var _UsersSrv = new UsersServiceClient())
                    {
                        var data = _UsersSrv.NguoiDungHeThong_Khoa(Guid.Parse(UserID), Khoa);
                        if (data != null && data.Data != null && data.Data.resultObject > 0)
                        {
                            status = true;
                        }
                    }
                }
                return Json(new { status = status }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("NguoiDungHeThongController/Khoa_MoKhoaTaiKhoa error:" + ex.Message, ex, Request);
                return Json(new { status = false }, JsonRequestBehavior.AllowGet);
            }
        }
        #region Ldap
        public bool validateUserByBind(string username, string password, string domain, string url, int portNumber)
        {
            bool result = true;
            var credentials = new NetworkCredential(username, password, domain);
            var serverId = new LdapDirectoryIdentifier(url, portNumber);
            var conn = new LdapConnection(serverId, credentials);
            try
            {
                conn.Bind();
                //var searchRequest = new SearchRequest("dc=quangninh.vn,dc=QUANGNINH", "uid=" + username, System.DirectoryServices.Protocols.SearchScope.Subtree);
                //var a = (SearchResponse)conn.SendRequest(searchRequest, new TimeSpan(0, 0, 0, 30));
                //if (a.Entries.Count == 0)
                //    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString(), ex.ToString());
                result = false;
            }
            conn.Dispose();
            return result;
        }
        #endregion
    }
}