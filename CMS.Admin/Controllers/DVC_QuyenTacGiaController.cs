using Business.Entities.Domain;
using CMS.Admin.Common;
using CMS.Admin.Constaints;
using CMS.Admin.Models;
using Module.Framework.Common;
using Module.Framework;
using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using Core.Common.Utilities;
using Module.Framework.Utilities;
using System.Web.Configuration;
using System.Net;

namespace CMS.Admin.Controllers
{
    public class DVC_QuyenTacGiaController : BaseController
    {
        private int _pageSize;
        private Guid _nguoiDungID;
        private long _userPortalID;
        private string _userFullName;
        public DVC_QuyenTacGiaController()
        {
            var pageSize = DungChung.GetKeyCauHinhHeThong("SoHangHienThi");
            if (!int.TryParse(pageSize, out this._pageSize))
                this._pageSize = 10;
            _nguoiDungID = LoginManager.GetCurrentUser() != null ? Guid.Parse(LoginManager.GetCurrentUser().UserId) : Guid.Empty;
            _userPortalID = LoginManager.GetCurrentUser() != null ? LoginManager.GetCurrentUser().UserPortalID : 0;
            _userFullName = LoginManager.GetCurrentUser() != null ? LoginManager.GetCurrentUser().UserFullName : "";
        }

        #region QuyenTacGia
        [CustomAuthorize(RightName = CookieRight.DVC_QuyenTacGia_Index)]
        public ActionResult Index()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var result = new DVC_QuyenTacGiaViewModel();
            result.Search = new DVC_QTG_QuyenTacGiaParam();
            try
            {
                result.Search.PageSize = this._pageSize;
                result.Search.PageIndex = 1;
                using (var _quyenTacGiaSVR = new DVC_QuyenTacGiaServiceClient())
                {
                    var tempList = _quyenTacGiaSVR.DVC_QTG_QuyenTacGia_List(result.Search);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Any())
                    {
                        DungChung.ghinhatkynguoidung("Xem danh sách quyền tác giả",
                                               "DVC_QuyenTacGiaController",
                                               "Index", "View");
                        result.List = tempList.Data.resultObject.ToPagedList(1, result.Search.PageSize);
                    }
                }
                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/Index error:" + ex.Message, ex, Request);
                return View(result);
            }
        }
        [HttpPost]
        public ActionResult Index(DVC_QTG_QuyenTacGiaParam model)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var result = new DVC_QuyenTacGiaViewModel();
            try
            {
                //model.PageSize = this._pageSize;
                using (var _quyenTacGiaSVR = new DVC_QuyenTacGiaServiceClient())
                {
                    var tempList = _quyenTacGiaSVR.DVC_QTG_QuyenTacGia_List(model);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Any())
                    {
                        DungChung.ghinhatkynguoidung("Xem danh sách quyền tác giả",
                                               "DVC_QuyenTacGiaController",
                                               "Index", "View");
                        result.List = tempList.Data.resultObject.ToPagedList(1, model.PageSize);
                    }
                }
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_IndexPartialView", result);
                }
                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/Index error:" + ex.Message, ex, Request);
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_IndexPartialView", result);
                }
                return View(result);
            }
        }

        [CustomAuthorize]
        [CustomAuthorize(RightName = CookieRight.DVC_QuyenTacGia_InsUpd)]
        public ActionResult Insert(string id)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var result = new DVC_QTG_QuyenTacGiaAdd();
            result.LoaiDangKyID = 1;
            result.HinhDaiDienStt = -1;
            result.PhimStt = -1;
            result.NgayCap = DateTime.Today.ToString("dd/MM/yyyy");
            try
            {
                ViewBag.TieuDe = "Cấp giấy chứng nhận đăng ký quyền tác giả - Nhập tác phẩm";
                if (id != "" && id != null)
                {
                    using (var _quyenTacGiaSVR = new DVC_QuyenTacGiaServiceClient())
                    {
                        var tempList = _quyenTacGiaSVR.DVC_QTG_QuyenTacGia_ById(long.Parse(id));
                        if (tempList != null && tempList.Data != null && tempList.Data.resultObject != null)
                        {
                            result = tempList.Data.resultObject;
                        }
                    }
                }
                result.UserFullName = _userFullName;
                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/Insert error:" + ex.Message, ex, Request);
                return View(result);
            }
        }      

        [HttpPost]
        public ActionResult CapSo(long id, int loaiNghiepVuID)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            bool status = false; string stt = ""; string soGCN = "";
            try
            {
                using (var _capSoSVR = new HS_CapSoServiceClient())
                {
                    var tempList = _capSoSVR.DVC_HS_CapSo_GenSo(1);
                    if (tempList.Data != null && tempList.Data.resultObject != null)
                    {
                        status = true; stt = tempList.Data.resultObject.STT; soGCN = tempList.Data.resultObject.SoGCN;
                    }
                }
                return Json(new { status = status, id = id, stt = stt, soGCN = soGCN });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/CapSo error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }

        [HttpPost]
        [CustomAuthorize(RightName = CookieRight.DVC_QuyenTacGia_InsUpd)]
        [CheckForAntiForgeryToken]
        public ActionResult SaveQuyenTacGia()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            bool status = false; long id = 0; bool checkSoGCN = false; bool checkSoTT = false;
            try
            {
                var model = ColectionItem<DVC_QTG_QuyenTacGiaAdd>("modelThongTin");
                if (model != null)
                {
                    model.UserID = _nguoiDungID;
                    using (var _quyenTacGiaSVR = new DVC_QuyenTacGiaServiceClient())
                    {
                        var ckSoTT = _quyenTacGiaSVR.DVC_QTG_QuyenTacGia_CheckSoTT(model.QuyenTacGiaID, model.STT);
                        if (ckSoTT != null && ckSoTT.Data != null && ckSoTT.Data.resultObject > 0)
                        {
                            checkSoTT = true;
                            var ckSoGCN = _quyenTacGiaSVR.DVC_QTG_QuyenTacGia_CheckSoGCN(model.QuyenTacGiaID, model.SoGCN);
                            if (ckSoGCN != null && ckSoGCN.Data != null && ckSoGCN.Data.resultObject > 0)
                            {
                                checkSoGCN = true;
                                var tempList = _quyenTacGiaSVR.DVC_QTG_QuyenTacGia_InsUpd(model);
                                if (tempList.Data != null && tempList.Data.resultObject > 0)
                                {
                                    DungChung.ghinhatkynguoidung("Thêm mới quyền tác giả",
                                                     "DVC_QuyenTacGiaController",
                                                     "SaveQuyenTacGia", "Create");
                                    status = true; id = tempList.Data.resultObject;
                                }
                            }
                        }
                    }
                }
                return Json(new { checkSoTT = checkSoTT, checkSoGCN = checkSoGCN, status = status, data = id });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/SaveTacGia error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }

        [HttpPost]
        public ActionResult DeleteQuyenTacGia(long id)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            bool status = false;
            try
            {
                if (id > 0)
                {
                    using (var _quyenTacGiaSVR = new DVC_QuyenTacGiaServiceClient())
                    {
                        var tempList = _quyenTacGiaSVR.DVC_QTG_QuyenTacGia_Del(id, _nguoiDungID);
                        if (tempList.Data != null && tempList.Data.resultObject > 0)
                        {
                            DungChung.ghinhatkynguoidung("Xóa quyền tác giả",
                                         "DVC_QuyenTacGiaController",
                                         "DeleteQuyenTacGia", "Delete");
                            status = true;
                        }
                    }
                }
                return Json(new { status = status });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/DeleteQuyenTacGia error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }
        #endregion QuyenTacGia

        #region TacGia
        [HttpPost]
        public ActionResult SearchTacGia()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var lstTacGia = new List<TT_TacGiaMap>();
            try
            {
                var model = ColectionItem<TT_TacGiaParam>("modelSearch");
                //model.PageSize = this._pageSize;
                using (var _dungChungSVR = new DungChungServiceClient())
                {
                    var tempList = _dungChungSVR.TT_TacGia_List(model);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Any())
                    {
                        DungChung.ghinhatkynguoidung("Tìm kiếm tác giả",
                                                  "DVC_QuyenTacGiaController",
                                                  "SearchTacGia", "View");
                        lstTacGia = tempList.Data.resultObject;
                    }
                }
                return PartialView("DialogSearchTacGia/List", lstTacGia);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/SearchTacGia error:" + ex.Message, ex, Request);
                return PartialView("DialogSearchTacGia/List", lstTacGia);
            }
        }

        [HttpPost]
        [CustomAuthorize(RightName = CookieRight.TT_TacGia_InsUpd)]
        [CheckForAntiForgeryToken]
        public ActionResult SaveTacGia()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            bool status = false, checkCCCD = false;
            try
            {
                var model = ColectionItem<TT_TacGiaAdd>("modelThongTin");
                if (model != null)
                {
                    model.UserID = _nguoiDungID;
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var resultCheckCCCD = _dungChungSVR.TT_TacGia_CheckCCCD(model.SoCMND, model.TacGiaID);
                        if (resultCheckCCCD.Data != null && resultCheckCCCD.Data.resultObject > 0)
                        {
                            var tempList = _dungChungSVR.TT_TacGia_InsUpd(model);
                            if (tempList.Data != null && tempList.Data.resultObject > 0)
                            {
                                DungChung.ghinhatkynguoidung("Thêm mới tác giả",
                                                 "DVC_QuyenTacGiaController",
                                                 "SaveTacGia", "Create");
                                status = true;
                            }
                        }
                        else
                            checkCCCD = true;
                    }
                }
                return Json(new { status = status, checkCCCD = checkCCCD });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/SaveTacGia error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }

        [HttpPost]
        public ActionResult UpdateTacGia(long id)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var resultModel = new TT_TacGiaAdd();
            try
            {
                if (id > 0)
                {
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var tempData = _dungChungSVR.TT_TacGia_ById(id);
                        if (tempData.Data != null && tempData.Data.resultObject != null)
                        {
                            DungChung.ghinhatkynguoidung("Chỉnh sửa tác giả",
                                                      "DVC_QuyenTacGiaController",
                                                      "UpdateTacGia", "View");
                            resultModel = tempData.Data.resultObject;
                        }
                    }
                }
                return PartialView("DialogSearchTacGia/Update", resultModel);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/UpdateTacGia error:" + ex.Message, ex, Request);
                return PartialView("DialogSearchTacGia/Update", resultModel);
            }
        }
        #endregion TacGia

        #region ChuSoHuu
        [HttpPost]
        public ActionResult SearchChuSoHuu()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var lstChuSoHuu = new List<TT_ChuSoHuuMap>();
            try
            {
                var model = ColectionItem<TT_ChuSoHuuParam>("modelSearch");
                //model.PageSize = this._pageSize;
                using (var _dungChungSVR = new DungChungServiceClient())
                {
                    var tempList = _dungChungSVR.TT_ChuSoHuu_List(model);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Any())
                    {
                        DungChung.ghinhatkynguoidung("Tìm kiếm chủ sở hữu",
                                                  "DVC_QuyenTacGiaController",
                                                  "SearchChuSoHuu", "View");
                        lstChuSoHuu = tempList.Data.resultObject;
                    }
                }
                return PartialView("DialogSearchChuSoHuu/List", lstChuSoHuu);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/SearchChuSoHuu error:" + ex.Message, ex, Request);
                return PartialView("DialogSearchChuSoHuu/List", lstChuSoHuu);
            }
        }

        [HttpPost]
        [CustomAuthorize(RightName = CookieRight.TT_ChuSoHuu_InsUpd)]
        [CheckForAntiForgeryToken]
        public ActionResult SaveChuSoHuu()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            bool status = false, checkCCCD = false;
            try
            {
                var model = ColectionItem<TT_ChuSoHuuAdd>("modelThongTin");
                if (model != null)
                {
                    model.UserID = _nguoiDungID;
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var resultCheckCCCD = _dungChungSVR.TT_ChuSoHuu_CheckCCCD(model.SoCMND, model.ChuSoHuuID);
                        if (resultCheckCCCD.Data != null && resultCheckCCCD.Data.resultObject > 0)
                        {
                            var tempList = _dungChungSVR.TT_ChuSoHuu_InsUpd(model);
                            if (tempList.Data != null && tempList.Data.resultObject > 0)
                            {
                                DungChung.ghinhatkynguoidung("Thêm mới chủ sở hữu",
                                                 "DVC_QuyenTacGiaController",
                                                 "SaveChuSoHuu", "Create");
                                status = true;
                            }
                        }
                        else
                            checkCCCD = true;
                    }
                }
                return Json(new { status = status, checkCCCD = checkCCCD });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/SaveChuSoHuu error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }

        [HttpPost]
        public ActionResult UpdateChuSoHuu(long id)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var resultModel = new TT_ChuSoHuuAdd();
            try
            {
                if (id > 0)
                {
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var tempData = _dungChungSVR.TT_ChuSoHuu_ById(id);
                        if (tempData.Data != null && tempData.Data.resultObject != null)
                        {
                            DungChung.ghinhatkynguoidung("Chỉnh sửa chủ sở hữu",
                                                      "DVC_QuyenTacGiaController",
                                                      "UpdateChuSoHuu", "View");
                            resultModel = tempData.Data.resultObject;
                        }
                    }
                }
                return PartialView("DialogSearchChuSoHuu/Update", resultModel);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/UpdateChuSoHuu error:" + ex.Message, ex, Request);
                return PartialView("DialogSearchChuSoHuu/Update", resultModel);
            }
        }
        #endregion ChuSoHuu

        #region CongDan
        [HttpPost]
        public ActionResult SearchCongDan()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var lstCongDan = new List<TT_CongDanMap>();
            try
            {
                var model = ColectionItem<TT_CongDanParam>("modelSearch");
                //model.PageSize = this._pageSize;
                using (var _dungChungSVR = new DungChungServiceClient())
                {
                    var tempList = _dungChungSVR.TT_CongDan_List(model);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Any())
                    {
                        DungChung.ghinhatkynguoidung("Tìm kiếm thông tin công dân",
                                                  "DVC_QuyenTacGiaController",
                                                  "SearchCongDan", "View");
                        lstCongDan = tempList.Data.resultObject;
                    }
                }
                return PartialView("DialogSearchCongDan/List", lstCongDan);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/SearchCongDan error:" + ex.Message, ex, Request);
                return PartialView("DialogSearchCongDan/List", lstCongDan);
            }
        }

        [HttpPost]
        [CustomAuthorize(RightName = CookieRight.TT_CongDan_InsUpd)]
        [CheckForAntiForgeryToken]
        public ActionResult SaveCongDan()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            bool status = false, checkCCCD = false, checkDKKD = false;
            try
            {
                var model = ColectionItem<TT_CongDanAdd>("modelThongTin");
                if (model != null)
                {
                    model.UserID = _nguoiDungID;
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var resultCheckCCCD = _dungChungSVR.TT_CongDan_CheckCCCD(model.SoCMND, model.CongDanID);
                        if (resultCheckCCCD.Data != null && resultCheckCCCD.Data.resultObject < 0)
                            checkCCCD = true;
                        var resultCheckDKKD = _dungChungSVR.TT_CongDan_CheckDKKD(model.SoDKKD, model.CongDanID);
                        if (resultCheckDKKD.Data != null && resultCheckDKKD.Data.resultObject < 0)
                            checkDKKD = true;
                        if (checkCCCD == false && checkDKKD == false)
                        {
                            var tempList = _dungChungSVR.TT_CongDan_InsUpd(model);
                            if (tempList.Data != null && tempList.Data.resultObject > 0)
                            {
                                DungChung.ghinhatkynguoidung("Thêm mới thông tin công dân",
                                                 "DVC_QuyenTacGiaController",
                                                 "SaveCongDan", "Create");
                                status = true;
                            }
                        }
                    }
                }
                return Json(new { status = status, checkCCCD = checkCCCD, checkDKKD = checkDKKD });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/SaveCongDan error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }

        [HttpPost]
        public ActionResult UpdateCongDan(long id)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var resultModel = new TT_CongDanAdd();
            try
            {
                if (id > 0)
                {
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var tempData = _dungChungSVR.TT_CongDan_ById(id);
                        if (tempData.Data != null && tempData.Data.resultObject != null)
                        {
                            DungChung.ghinhatkynguoidung("Chỉnh sửa thông tin công dân",
                                                      "DVC_QuyenTacGiaController",
                                                      "UpdateCongDan", "View");
                            resultModel = tempData.Data.resultObject;
                        }
                    }
                }
                return PartialView("DialogSearchCongDan/Update", resultModel);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/UpdateCongDan error:" + ex.Message, ex, Request);
                return PartialView("DialogSearchCongDan/Update", resultModel);
            }
        }
        #endregion CongDan

        #region HinhAnh
        [HttpPost]
        public ActionResult SearchHinhAnh()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var lstHinhAnh = new List<TT_HinhAnhMap>();
            try
            {
                var model = ColectionItem<TT_HinhAnhParam>("modelSearch");
                //model.PageSize = this._pageSize;
                using (var _dungChungSVR = new DungChungServiceClient())
                {
                    var tempList = _dungChungSVR.TT_HinhAnh_List(model);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Any())
                    {
                        DungChung.ghinhatkynguoidung("Tìm kiếm hình ảnh",
                                                  "DVC_QuyenTacGiaController",
                                                  "SearchHinhAnh", "View");
                        lstHinhAnh = tempList.Data.resultObject;
                    }
                }
                return PartialView(model.KeyPage == "search" ? "DialogSearchHinhAnh/List" : "DialogSearchHinhAnh/Update", lstHinhAnh);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/SearchHinhAnh error:" + ex.Message, ex, Request);
                return PartialView("DialogSearchHinhAnh/List", lstHinhAnh);
            }
        }

        [HttpPost]
        [CustomAuthorize(RightName = CookieRight.TT_HinhAnh_InsUpd)]
        [CheckForAntiForgeryToken]
        public ActionResult SaveHinhAnh()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            bool status = false;
            try
            {
                var model = ColectionItem<TT_HinhAnhAdd>("modelThongTin");
                if (model != null)
                {
                    model.UserID = _nguoiDungID;
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var tempList = _dungChungSVR.TT_HinhAnh_InsUpd(model);
                        if (tempList.Data != null && tempList.Data.resultObject > 0)
                        {
                            DungChung.ghinhatkynguoidung("Thêm mới hình ảnh",
                                             "DVC_QuyenTacGiaController",
                                             "SaveHinhAnh", "Create");
                            status = true;
                        }
                    }
                }
                return Json(new { status = status });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/SaveHinhAnh error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }

        [HttpPost]
        public ActionResult UpdateHinhAnh(long id)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var resultModel = new TT_HinhAnhAdd();
            try
            {
                if (id > 0)
                {
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var tempData = _dungChungSVR.TT_HinhAnh_ById(id);
                        if (tempData.Data != null && tempData.Data.resultObject != null)
                        {
                            DungChung.ghinhatkynguoidung("Chỉnh sửa hình ảnh",
                                                      "DVC_QuyenTacGiaController",
                                                      "UpdateHinhAnh", "View");
                            resultModel = tempData.Data.resultObject;
                            return Json(new { status = true, data = tempData.Data.resultObject });
                        }
                    }
                }
                return Json(new { status = false });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/UpdateHinhAnh error:" + ex.Message, ex, Request);
                return Json(new { status = false });
            }
        }

        [HttpPost]
        public ActionResult DeleteHinhAnh(long id)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            bool status = false;
            try
            {
                if (id > 0)
                {
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var tempList = _dungChungSVR.TT_HinhAnh_Del(id, _nguoiDungID);
                        if (tempList.Data != null && tempList.Data.resultObject > 0)
                        {
                            DungChung.ghinhatkynguoidung("Xóa hình ảnh",
                                         "DVC_QuyenTacGiaController",
                                         "Delete", "Delete");
                            status = true;
                        }
                    }
                }
                return Json(new { status = status });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/DeleteHinhAnh error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }
        #endregion HinhAnh

        #region Phim
        [HttpPost]
        public ActionResult SearchPhim()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var lstPhim = new List<TT_PhimMap>();
            try
            {
                var model = ColectionItem<TT_PhimParam>("modelSearch");
                //model.PageSize = this._pageSize;
                using (var _dungChungSVR = new DungChungServiceClient())
                {
                    var tempList = _dungChungSVR.TT_Phim_List(model);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Any())
                    {
                        DungChung.ghinhatkynguoidung("Tìm kiếm phim",
                                                  "DVC_QuyenTacGiaController",
                                                  "SearchPhim", "View");
                        lstPhim = tempList.Data.resultObject;
                    }
                }
                return PartialView(model.KeyPage == "search" ? "DialogSearchPhim/List" : "DialogSearchPhim/Update", lstPhim);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/SearchPhim error:" + ex.Message, ex, Request);
                return PartialView("DialogSearchPhim/List", lstPhim);
            }
        }

        [HttpPost]
        [CustomAuthorize(RightName = CookieRight.TT_Phim_InsUpd)]
        [CheckForAntiForgeryToken]
        public ActionResult SavePhim()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            bool status = false;
            try
            {
                var model = ColectionItem<TT_PhimAdd>("modelThongTin");
                if (model != null)
                {
                    model.UserID = _nguoiDungID;
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var tempList = _dungChungSVR.TT_Phim_InsUpd(model);
                        if (tempList.Data != null && tempList.Data.resultObject > 0)
                        {
                            DungChung.ghinhatkynguoidung("Thêm mới phim",
                                             "DVC_QuyenTacGiaController",
                                             "SavePhim", "Create");
                            status = true;
                        }
                    }
                }
                return Json(new { status = status });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/SavePhim error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }

        [HttpPost]
        public ActionResult UpdatePhim(long id)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var resultModel = new TT_PhimAdd();
            try
            {
                if (id > 0)
                {
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var tempData = _dungChungSVR.TT_Phim_ById(id);
                        if (tempData.Data != null && tempData.Data.resultObject != null)
                        {
                            DungChung.ghinhatkynguoidung("Chỉnh sửa phim",
                                                      "DVC_QuyenTacGiaController",
                                                      "UpdatePhim", "View");
                            resultModel = tempData.Data.resultObject;
                            return Json(new { status = true, data = tempData.Data.resultObject });
                        }
                    }
                }
                return Json(new { status = false });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/UpdatePhim error:" + ex.Message, ex, Request);
                return Json(new { status = false });
            }
        }

        [HttpPost]
        public ActionResult DeletePhim(long id)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            bool status = false;
            try
            {
                if (id > 0)
                {
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var tempList = _dungChungSVR.TT_Phim_Del(id, _nguoiDungID);
                        if (tempList.Data != null && tempList.Data.resultObject > 0)
                        {
                            DungChung.ghinhatkynguoidung("Xóa phim",
                                         "DVC_QuyenTacGiaController",
                                         "Delete", "Delete");
                            status = true;
                        }
                    }
                }
                return Json(new { status = status });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/DeletePhim error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }
        #endregion Phim

        #region DinhKem
        [HttpPost]
        public ActionResult SearchDinhKem()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var lstDinhKem = new List<TT_DinhKemMap>();
            try
            {
                var model = ColectionItem<TT_DinhKemParam>("modelSearch");
                //model.PageSize = this._pageSize;
                using (var _dungChungSVR = new DungChungServiceClient())
                {
                    var tempList = _dungChungSVR.TT_DinhKem_List(model);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Any())
                    {
                        DungChung.ghinhatkynguoidung("Tìm kiếm hình ảnh",
                                                  "DVC_QuyenTacGiaController",
                                                  "SearchDinhKem", "View");
                        lstDinhKem = tempList.Data.resultObject;
                    }
                }
                return PartialView("DialogSearchDinhKem/List", lstDinhKem);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/SearchDinhKem error:" + ex.Message, ex, Request);
                return PartialView("DialogSearchDinhKem/List", lstDinhKem);
            }
        }

        [HttpPost]
        [CustomAuthorize(RightName = CookieRight.TT_DinhKem_InsUpd)]
        [CheckForAntiForgeryToken]
        public ActionResult SaveDinhKem()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            bool status = false;
            try
            {
                var model = ColectionItem<TT_DinhKemAdd>("modelThongTin");
                if (model != null)
                {
                    model.UserID = _nguoiDungID;
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var tempList = _dungChungSVR.TT_DinhKem_InsUpd(model);
                        if (tempList.Data != null && tempList.Data.resultObject > 0)
                        {
                            DungChung.ghinhatkynguoidung("Thêm mới hình ảnh",
                                             "DVC_QuyenTacGiaController",
                                             "SaveDinhKem", "Create");
                            status = true;
                        }
                    }
                }
                return Json(new { status = status });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/SaveDinhKem error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }

        [HttpPost]
        public ActionResult UpdateDinhKem(long id)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var resultModel = new TT_DinhKemAdd();
            try
            {
                if (id > 0)
                {
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var tempData = _dungChungSVR.TT_DinhKem_ById(id);
                        if (tempData.Data != null && tempData.Data.resultObject != null)
                        {
                            DungChung.ghinhatkynguoidung("Chỉnh sửa hình ảnh",
                                                      "DVC_QuyenTacGiaController",
                                                      "UpdateDinhKem", "View");
                            resultModel = tempData.Data.resultObject;
                            return Json(new { status = true, data = tempData.Data.resultObject });
                        }
                    }
                }
                return Json(new { status = false });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/UpdateDinhKem error:" + ex.Message, ex, Request);
                return Json(new { status = false });
            }
        }

        [HttpPost]
        public ActionResult DeleteDinhKem(long id)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            bool status = false;
            try
            {
                if (id > 0)
                {
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var tempList = _dungChungSVR.TT_DinhKem_Del(id, _nguoiDungID);
                        if (tempList.Data != null && tempList.Data.resultObject > 0)
                        {
                            DungChung.ghinhatkynguoidung("Xóa hình ảnh",
                                         "DVC_QuyenTacGiaController",
                                         "Delete", "Delete");
                            status = true;
                        }
                    }
                }
                return Json(new { status = status });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/DeleteDinhKem error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }
        #endregion DinhKem

        #region Upload
        [HttpPost]
        public ActionResult UploadFile()
        {
            bool status = false;
            string FileName = "";
            string FileNameGoc = "";
            string FileUrl = "";
            try
            {
                if (Request.Files.Count > 0)
                {
                    var root = Path.Combine(Server.MapPath('~' + ""));
                    var fileUpload = Request.Files[0];
                    FileNameGoc = fileUpload.FileName;
                    var fileInfo = FileControl.SaveFileUpload(root, ParameterSetting.PathFileUpload + "\\DinhKem", fileUpload);
                    var urlupload = "\\" + fileInfo.Url;
                    FileUrl = urlupload;
                    FileName = fileInfo.TenUpload;
                    status = true;
                }
                return Json(new { status = status, FileName = FileName, FileUrl = FileUrl, FileNameGoc = FileNameGoc });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/UploadFile error:" + ex.Message, ex, Request);
                return Json(new { status = false });
            }
        }

        [HttpPost]
        public ActionResult UploadPicture()
        {
            bool status = false;
            string FileName = "";
            string FileNameGoc = "";
            string FileUrl = "";
            try
            {
                if (Request.Files.Count > 0)
                {
                    var root = Path.Combine(Server.MapPath('~' + ""));
                    var fileUpload = Request.Files[0];
                    FileNameGoc = fileUpload.FileName;
                    var fileInfo = FileControl.SaveFileUpload(root, ParameterSetting.PathFileUpload + "\\HinhAnh", fileUpload);
                    var urlupload = "\\" + fileInfo.Url;
                    FileUrl = urlupload;
                    FileName = fileInfo.TenUpload;
                    status = true;
                }
                return Json(new { status = status, FileName = FileName, FileUrl = FileUrl, FileNameGoc = FileNameGoc });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/UploadPicture error:" + ex.Message, ex, Request);
                return Json(new { status = false });
            }
        }

        [HttpPost]
        public ActionResult UploadVideo()
        {
            bool status = false;
            string FileName = "";
            string FileNameGoc = "";
            string FileUrl = "";
            try
            {
                if (Request.Files.Count > 0)
                {
                    var root = Path.Combine(Server.MapPath('~' + ""));
                    var fileUpload = Request.Files[0];
                    FileNameGoc = fileUpload.FileName;
                    var fileInfo = FileControl.SaveFileUpload(root, ParameterSetting.PathFileUpload + "\\Phim", fileUpload);
                    var urlupload = "\\" + fileInfo.Url;
                    FileUrl = urlupload;
                    FileName = fileInfo.TenUpload;
                    status = true;
                }
                return Json(new { status = status, FileName = FileName, FileUrl = FileUrl, FileNameGoc = FileNameGoc });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/UploadPicture error:" + ex.Message, ex, Request);
                return Json(new { status = false });
            }
        }
        #endregion Upload

        #region ChiTiet
        [CustomAuthorize]
        [CustomAuthorize(RightName = CookieRight.DVC_QuyenTacGia_ChiTiet)]
        public ActionResult ChiTiet(string id)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var result = new DVC_QTG_QuyenTacGiaAdd();
            try
            {
                ViewBag.TieuDe = "Thông tin quyền tác giả";
                if (id != "" && id != null)
                {
                    using (var _quyenTacGiaSVR = new DVC_QuyenTacGiaServiceClient())
                    {
                        var resultTacGia = _quyenTacGiaSVR.DVC_QTG_QuyenTacGia_ById(long.Parse(id));
                        if (resultTacGia != null && resultTacGia.Data != null && resultTacGia.Data.resultObject != null)
                        {
                            result = resultTacGia.Data.resultObject;
                        }
                    }
                }
                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/ChiTiet error:" + ex.Message, ex, Request);
                return View(result);
            }
        }
        #endregion ChiTiet

        #region SearchGiayChungNhan
        [HttpPost]
        public ActionResult SearchGiayChungNhan()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var lstGCN = new List<DVC_QTG_GiayChungNhanMap>();
            try
            {
                var model = ColectionItem<DVC_QTG_GiayChungNhanParam>("modelSearch");
                //model.PageSize = this._pageSize;
                using (var _quyenTacGiaSVR = new DVC_QuyenTacGiaServiceClient())
                {
                    var tempList = _quyenTacGiaSVR.DVC_QTG_QuyenTacGia_SearchGCN(model);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Any())
                    {
                        DungChung.ghinhatkynguoidung("Tìm kiếm giấy chứng nhận",
                                                  "DVC_QuyenTacGiaController",
                                                  "SearchGiayChungNhan", "View");
                        lstGCN = tempList.Data.resultObject;
                    }
                }
                return PartialView("DialogSearchGiayChungNhan/List", lstGCN);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DVC_QuyenTacGiaController/SearchGiayChungNhan error:" + ex.Message, ex, Request);
                return PartialView("DialogSearchGiayChungNhan/List", lstGCN);
            }
        }
        #endregion SearchGiayChungNhan

        protected T ColectionItem<T>(string nameRequest)
        {
            try
            {
                var model = Request.Form[nameRequest];
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                };

                var item = JsonConvert.DeserializeObject<T>(model, settings);
                return item;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}