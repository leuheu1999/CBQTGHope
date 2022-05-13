using Module.Framework;
using PagedList;
using System.Linq;
using System.Web.Mvc;
using CMS.Admin.Models;
using Business.Entities.Domain;
using CMS.Admin.Common;
using System.Collections.Generic;
using System;
using CMS.Admin.Constaints;
using Module.Framework.Common;
using CMS.Admin.Controllers;

namespace QuanTriModules.Controllers
{
    public class DM_PhuongXaController : BaseController
    {
        private DungChungServiceClient _DungChungSrv;
        private int _pageSize;
        public DM_PhuongXaController()
        {
            var pageSize = DungChung.GetKeyCauHinhHeThong("SoHangHienThi");
            if (!int.TryParse(pageSize, out this._pageSize))
                this._pageSize = 10;
        }
        [CustomAuthorize(RightName = CookieRight.DM_PhuongXaController_Index)]
        public ActionResult Index()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var result = new DM_PhuongXaViewModel();
            _DungChungSrv = new DungChungServiceClient();
            try
            {                
                result.Search = new DM_PhuongXaMapParam();
                result.Search.PageIndex = 1;
                result.Search.PageSize = this._pageSize;
                var temp = _DungChungSrv.DM_PhuongXa_List(result.Search);
                if (temp.Data != null && temp.Data.resultObject != null && temp.Data.resultObject.Any())
                {
                    DungChung.ghinhatkynguoidung("Xem danh sách phường xã",
                                              "DM_PhuongXaController",
                                              "Index", "View");
                    result.Items = temp.Data.resultObject.ToPagedList(result.Search.PageIndex, this._pageSize);
                }
                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_PhuongXaController/Index error:" + ex.Message, ex, Request);
                return View(result);
            }

        }
        [HttpPost]
        public ActionResult ListDMPhuongXa(DM_PhuongXaMapParam model)
        {
            try
            {
                _DungChungSrv = new DungChungServiceClient();
                //model.PageSize = this._pageSize;
                var temp = _DungChungSrv.DM_PhuongXa_List(model);
                var result = new DM_PhuongXaViewModel();
                if (temp.Data != null&& temp.Data.resultObject!=null && temp.Data.resultObject.Any())
                {
                    DungChung.ghinhatkynguoidung("Tìm kiếm phường xã",
                                              "DM_PhuongXaController",
                                              "Index", "View");
                    result.Items = temp.Data.resultObject.ToPagedList(1, this._pageSize);
                }
                return PartialView("_PartialList", result.Items);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_PhuongXaController/ListDMPhuongXa error:" + ex.Message, ex, Request);
                return PartialView("_PartialList", null);
            }
        }
        [HttpPost]
        [CustomAuthorize(RightName = CookieRight.DM_PhuongXaController_ThemMoiDMPhuongXa)]
        [CheckForAntiForgeryToken]

        public ActionResult ThemMoiDMPhuongXa(DM_PhuongXaMapAdd model)
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
                    _DungChungSrv = new DungChungServiceClient();
                    //kiem tra co tont tai ma chua
                    // if (model.QuanHuyenID == 0)
                    if (model.ID == 0)
                    {
                        var checkMa = _DungChungSrv.GetPhuongXaByMa(model.Ma);
                        if (checkMa.Data != null && checkMa.Data.resultObject != null)
                            return Json(new { status = status, checkMa = true });
                    }
                    else
                    {
                        var temp = _DungChungSrv.GetPhuongXaById(model.ID);
                        if (temp.Data != null && temp.Data.resultObject != null)
                        {
                            if (temp.Data.resultObject.Ma != model.Ma)
                            {
                                var checkMa = _DungChungSrv.GetPhuongXaByMa(model.Ma);
                                if (checkMa.Data != null && checkMa.Data.resultObject != null)
                                {
                                    DungChung.ghinhatkynguoidung("Thêm mới phường xã",
                                              "DM_PhuongXaController",
                                              "ThemMoiDMPhuongXa", "Create");
                                    return Json(new { status = status, checkMa = true });
                                }
                                   
                            }
                        }
                    }
                    var result = _DungChungSrv.DM_PhuongXa_InsUpd(model);
                    if (result.Data != null && result.Data.resultObject > 0)
                        status = true;
                }
                return Json(new { status = status, checkMa = false });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_PhuongXaController/ThemMoiDMPhuongXa error:" + ex.Message, ex, Request);
                return Json(new { status = false});
            }
        }
        [HttpPost]
        public ActionResult CBOQuanHuyenById(string id)
        {
            var list = new List<SelectListItem>();
            try
            {               
                if (!string.IsNullOrEmpty(id))
                {
                    list = CBODungChung.GetCBOQuanHuyen(id).ToList(); ;
                }
                return new JsonResult
                {
                    Data = new
                    {
                        data = list
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_PhuongXaController/CBOQuanHuyenById error:" + ex.Message, ex, Request);
                return new JsonResult{Data = new{data = list},JsonRequestBehavior = JsonRequestBehavior.AllowGet};
            }
        }
        [HttpPost]
        public ActionResult CapNhatPhuongXa(long id)
        {
            var model = new DM_PhuongXaMapAdd();
            try
            {

                if (id > 0)
                {
                    _DungChungSrv = new DungChungServiceClient();
                    var temp = _DungChungSrv.GetPhuongXaById(id);
                    if (temp.Data != null && temp.Data.resultObject != null)
                        model = temp.Data.resultObject;
                }
                return PartialView("DialogUpdate/Update", model);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_PhuongXaController/CapNhatPhuongXa error:" + ex.Message, ex, Request);
                return PartialView("DialogUpdate/Update", model);
            }
        }
        [CustomAuthorize(RightName = CookieRight.DM_PhuongXaController_Delete)]

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
                        long idphuongxa = Int64.Parse(id);
                        Guid lastupduserid = new Guid();
                        using (_DungChungSrv = new DungChungServiceClient())
                        {
                            var tempList = _DungChungSrv.DM_PhuongXa_Del(idphuongxa, lastupduserid);
                            if (tempList.Data != null && tempList.Data.resultObject > 0)
                            {
                                DungChung.ghinhatkynguoidung("Xóa phường xã",
                                             "DM_PhuongXaController",
                                             "Delete", "Delete");
                                status = true;
                            }
                        }
                    }
                    return Json(new { status = status });
                }
                catch (Exception ex)
                {
                    DungChung.ghiloghethong("DM_PhuongXaController/Delete error:" + ex.Message, ex, Request);
                    return Json(new { status = status });
                }
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_PhuongXaController/Delete error:" + ex.Message, ex, Request);
                return Json(false);
            }
        }
    }
}