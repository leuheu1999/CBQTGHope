using Module.Framework;
using PagedList;
using System.Linq;
using System.Web.Mvc;
using CMS.Admin.Models;
using Business.Entities.Domain;
using System;
using CMS.Admin.Common;
using CMS.Admin.Constaints;
using Module.Framework.Common;
using CMS.Admin.Controllers;

namespace CMS.Admin.Controllers
{
    public class DM_TinhThanhController : BaseController
    {
        private DungChungServiceClient _DungChungSrv;
        private int _pageSize;
        public DM_TinhThanhController()
        {
            var pageSize = DungChung.GetKeyCauHinhHeThong("SoHangHienThi");
            if (!int.TryParse(pageSize, out this._pageSize))
                this._pageSize = 10;
        }
        // GET: DM_TinhThanh
        [CustomAuthorize(RightName = CookieRight.DM_TinhThanhController_Index)]

        public ActionResult Index()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var result = new DM_TinhThanhViewModel();
            _DungChungSrv = new DungChungServiceClient();
            result.Search = new DM_TinhThanhMapParam();
            result.Search.PageIndex = 1;
            result.Search.PageSize = this._pageSize;
            try
            {
                var temp = _DungChungSrv.DM_TinhThanh_List(result.Search);
                if (temp.Data != null && temp.Data.resultObject != null && temp.Data.resultObject.Any())
                {
                    DungChung.ghinhatkynguoidung("Xem danh sách tỉnh thành",
                                             "DM_TinhThanhController",
                                             "Index", "View");
                    result.Items = temp.Data.resultObject.ToPagedList(result.Search.PageIndex, this._pageSize);
                }
                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_TinhThanhController/Index error:" + ex.Message, ex, Request);
                return View(result);
            }
        }
        [HttpPost]
        public ActionResult ListDMTinhThanh(DM_TinhThanhMapParam model)
        {
            _DungChungSrv = new DungChungServiceClient();
            //model.PageSize = this._pageSize;
            try
            {
                var temp = _DungChungSrv.DM_TinhThanh_List(model);
                var result = new DM_TinhThanhViewModel();
                if (temp.Data != null&& temp.Data.resultObject!=null && temp.Data.resultObject.Any())
                {
                    DungChung.ghinhatkynguoidung("Tìm kiếm tỉnh thành",
                                             "DM_TinhThanhController",
                                             "Index", "View");
                    result.Items = temp.Data.resultObject.ToPagedList(1, this._pageSize);
                }
                return PartialView("_PartialList", result.Items);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_TinhThanhController/ListDMTinhThanh error:" + ex.Message, ex, Request);
                return PartialView("_PartialList", null);
            }
        }
        [HttpPost]
        [CustomAuthorize(RightName = CookieRight.DM_TinhThanhController_ThemMoiDMTinhThanh)]

        public ActionResult ThemMoiDMTinhThanh(DM_TinhThanhMapAdd model)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            bool status = false;
            try
            {
                if (model != null)
                {
                    _DungChungSrv = new DungChungServiceClient();
                    //kiem tra co tont tai ma chua
                    if (model.ID == 0)
                    {
                        var checkMa = _DungChungSrv.GetTinhThanhByMa(model.Ma);
                        if (checkMa.Data != null && checkMa.Data.resultObject != null)
                            return Json(new { status = status, checkMa = true });
                    }
                    else
                    {
                        var temp = _DungChungSrv.GetTinhThanhById(model.ID);
                        if (temp.Data != null && temp.Data.resultObject != null)
                        {
                            if (temp.Data.resultObject.Ma != model.Ma)
                            {
                                var checkMa = _DungChungSrv.GetTinhThanhByMa(model.Ma);
                                if (checkMa.Data != null && checkMa.Data.resultObject != null)
                                {
                                    DungChung.ghinhatkynguoidung("Thêm mới tỉnh thành",
                                            "DM_TinhThanhController",
                                            "ThemMoiDMTinhThanh", "Create");
                                    return Json(new { status = status, checkMa = true });
                                }
                            }
                        }
                    }
                    var result = _DungChungSrv.DM_TinhThanh_InsUpd(model);
                    if (result.Data != null && result.Data.resultObject > 0)
                        status = true;
                }
                return Json(new { status = status, checkMa = false });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_TinhThanhController/ThemMoiDMTinhThanh error:" + ex.Message, ex, Request);
                return Json(new { status = false });
            }
        }
        [HttpPost]
        public ActionResult CapNhatTinhThanh(long id)
        {
            var model = new DM_TinhThanhMapAdd();
            try
            {
                if (id > 0)
                {
                    _DungChungSrv = new DungChungServiceClient();
                    var temp = _DungChungSrv.GetTinhThanhById(id);
                    if (temp.Data != null && temp.Data.resultObject != null)
                        model = temp.Data.resultObject;
                }
                return PartialView("DialogUpdate/Update", model);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_TinhThanhController/CapNhatTinhThanh error:" + ex.Message, ex, Request);
                return PartialView("DialogUpdate/Update", null);
            }
        }
        [CustomAuthorize(RightName = CookieRight.DM_TinhThanhController_Delete)]

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
                        long idtinhthanh = Int64.Parse(id);
                        Guid lastupduserid = new Guid();
                        using (_DungChungSrv = new DungChungServiceClient())
                        {
                            var tempList = _DungChungSrv.DM_TinhThanh_Del(idtinhthanh, lastupduserid);
                            if (tempList.Data != null && tempList.Data.resultObject > 0)
                            {
                                DungChung.ghinhatkynguoidung("Xóa tỉnh thành",
                                             "DM_TinhThanhController",
                                             "Delete", "Delete");
                                status = true;
                            }
                        }
                    }
                    return Json(new { status = status });
                }
                catch (Exception ex)
                {
                    DungChung.ghiloghethong("DM_TinhThanhController/Delete error:" + ex.Message, ex, Request);
                    return Json(new { status = status });
                }
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_TinhThanhController/Delete error:" + ex.Message, ex, Request);
                return Json(false);
            }
        }
    }
}