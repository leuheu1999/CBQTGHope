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

namespace QuanTriModules.Controllers
{
    public class DM_QuocGiaController : BaseController
    {
        private DungChungServiceClient _DungChungSrv;
        private int _pageSize;
        public DM_QuocGiaController()
        {
            var pageSize = DungChung.GetKeyCauHinhHeThong("SoHangHienThi");
            if (!int.TryParse(pageSize, out this._pageSize))
                this._pageSize = 10;
        }
        // GET: DM_QuocGia
        [CustomAuthorize(RightName = CookieRight.Dm_QuocGiaController_Index)]

        public ActionResult Index()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var result = new DM_QuocGiaViewModel();
            _DungChungSrv = new DungChungServiceClient();
            result.Search = new DM_QuocGiaMapParam();
            result.Search.PageIndex = 1;
            result.Search.PageSize = this._pageSize;
            try
            {
                var temp = _DungChungSrv.DM_QuocGia_List(result.Search);
                if (temp.Data != null && temp.Data.resultObject != null && temp.Data.resultObject.Any())
                {
                    DungChung.ghinhatkynguoidung("Xem danh sách quốc gia",
                                              "DM_QuocGiaController",
                                              "Index", "View");
                    result.List = temp.Data.resultObject.ToPagedList(result.Search.PageIndex, this._pageSize);
                }
                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_QuocGiaController/Index error:" + ex.Message, ex, Request);
                return View(result);
            }
        }
        [HttpPost]
        public ActionResult ListDMQuocGia(DM_QuocGiaMapParam model)
        {
            _DungChungSrv = new DungChungServiceClient();
            //model.PageSize = this._pageSize;
            try
            {
                var temp = _DungChungSrv.DM_QuocGia_List(model);
                var result = new DM_QuocGiaViewModel();
                if (temp.Data != null && temp.Data.resultObject !=null&& temp.Data.resultObject.Any())
                {
                    DungChung.ghinhatkynguoidung("Tìm kiếm quốc gia",
                                              "DM_QuocGiaController",
                                              "Index", "View");
                    result.List = temp.Data.resultObject.ToPagedList(1, this._pageSize);
                }
                return PartialView("_PartialList", result.List);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_QuocGiaController/ListDMQuocGia error:" + ex.Message, ex, Request);
                return PartialView("_PartialList", null);
            }
        }
        [HttpPost]
        [CustomAuthorize(RightName = CookieRight.Dm_QuocGiaController_ThemMoiDMQuocGia)]
        [CheckForAntiForgeryToken]

        public ActionResult ThemMoiDMQuocGia(DM_QuocGiaMapAdd model)
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
                    if (model.QuocGiaID == 0)
                    {
                        var checkMa = _DungChungSrv.GetQuocGiaByMa(model.MaQuocGia);
                        if (checkMa.Data != null && checkMa.Data.resultObject != null)
                        {
                            DungChung.ghinhatkynguoidung("Thêm mới quốc gia",
                                              "DM_QuocGiaController",
                                              "ThemMoiDMQuocGia", "Create");
                            return Json(new { status = status, checkMa = true });
                        }
                    }
                    else
                    {
                        var temp = _DungChungSrv.GetQuocGiaById(model.QuocGiaID);
                        if (temp.Data != null && temp.Data.resultObject != null)
                        {
                            if (temp.Data.resultObject.MaQuocGia != model.MaQuocGia)
                            {
                                var checkMa = _DungChungSrv.GetQuocGiaByMa(model.MaQuocGia);
                                if (checkMa.Data != null && checkMa.Data.resultObject != null)
                                    return Json(new { status = status, checkMa = true });
                            }
                        }
                    }
                    var result = _DungChungSrv.DM_QuocGia_InsUpdate(model);
                    if (result.Data != null && result.Data.resultObject > 0)
                        status = true;
                }
                return Json(new { status = status, checkMa = false });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_QuocGiaController/ThemMoiDMQuocGia error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }
        [HttpPost]
        public ActionResult CapNhatQuocGia(long id)
        {
            try
            {
                var model = new DM_QuocGiaMapAdd();
                if (id > 0)
                {
                    _DungChungSrv = new DungChungServiceClient();
                    var temp = _DungChungSrv.GetQuocGiaById(id);
                    if (temp.Data != null && temp.Data.resultObject != null)
                        model = temp.Data.resultObject;
                }
                return PartialView("DialogUpdate/Update", model);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_QuocGiaController/CapNhatQuocGia error:" + ex.Message, ex, Request);
               return PartialView("DialogUpdate/Update", null);
            }
            
        }
        [CustomAuthorize(RightName = CookieRight.DM_QuocGiaController_Delete)]

        public ActionResult Delete(string id)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            try
            {
                bool status = false;
                try
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        long idquocgia = Int64.Parse(id);
                        Guid lastupduserid = new Guid();
                        using (_DungChungSrv = new DungChungServiceClient())
                        {
                            var tempList = _DungChungSrv.DM_QuocGia_Del(idquocgia, lastupduserid);
                            if (tempList.Data != null && tempList.Data.resultObject > 0)
                            {
                                DungChung.ghinhatkynguoidung("Xóa quốc gia",
                                              "DM_QuocGiaController",
                                              "Delete", "Delete");
                                status = true;
                            }
                        }
                    }
                    return Json(new { status = status });
                }
                catch (Exception ex)
                {
                    DungChung.ghiloghethong("DM_QuocGiaController/Delete error:" + ex.Message, ex, Request);
                    return Json(new { status = status });
                }
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_QuocGiaController/Delete error:" + ex.Message, ex, Request);
                return Json(false);
            }
        }
    }
}