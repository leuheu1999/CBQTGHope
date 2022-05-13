using Business.Entities.Domain;
using CMS.Admin.Common;
using CMS.Admin.Constaints;
using CMS.Admin.Models.DM_QuyenChucNang;
using Module.Framework.Common;
using log4net;
using Module.Framework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Admin.Controllers
{
    public class DM_QuyenChucNangController : BaseController
    {
        #region dungchung
        private DungChungServiceClient _DungChungSrv;
        private int _pageSize;
        #endregion
        public DM_QuyenChucNangController()
        {
            var pageSize = DungChung.GetKeyCauHinhHeThong("SoHangHienThi");
            if (!int.TryParse(pageSize, out this._pageSize))
                this._pageSize = 10;
        }
        // GET: DM_QuyenChucNang
        [CustomAuthorize(RightName = CookieRight.DM_QuyenChucNangController_Index)]

        public ActionResult Index()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var result = new DM_QuyenChucNangViewModel();
            result.Search = new DM_QuyenChucNangMapParam();
            try
            {
                result.Search.PageSize = this._pageSize;
                result.Search.PageIndex = 1;
                _DungChungSrv = new DungChungServiceClient();
                var tempList = _DungChungSrv.DM_QuyenChucNang_List(result.Search);
                if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Any())
                {
                    DungChung.ghinhatkynguoidung("Xem danh sách quyền chức năng",
                                              "DM_QuyenChucNangController",
                                              "Index", "View");
                    result.Items = tempList.Data.resultObject.ToPagedList(result.Search.PageIndex, result.Search.PageSize);
                }
                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_QuyenChucNangController/Index error:" + ex.Message, ex, Request);
                return View(result);
            }
        }
        [HttpPost]
        public ActionResult TimKiem(DM_QuyenChucNangMapParam model)
        {
            var result = new DM_QuyenChucNangViewModel();
            try
            {
                _DungChungSrv = new DungChungServiceClient();
                //model.PageSize = this._pageSize;
                var tempList = _DungChungSrv.DM_QuyenChucNang_List(model);
                if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Any())
                {
                    DungChung.ghinhatkynguoidung("Tìm kiếm quyền chức năng",
                                              "DM_QuyenChucNangController",
                                              "Index", "View");
                    result.Items = tempList.Data.resultObject.ToPagedList(1, model.PageSize);
                }
                return PartialView("_PartialList", result.Items);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_QuyenChucNangController/TimKiem error:" + ex.Message, ex, Request);
                return PartialView("_PartialList", result.Items);
            }
        }
        [HttpPost]
        [CustomAuthorize(RightName = CookieRight.DM_QuyenChucNangController_ThemMoi)]
        [CheckForAntiForgeryToken]

        public ActionResult ThemMoi(DM_QuyenChucNangAdd model)
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
                    if (model.Id == 0)
                    {
                        var checkMa = _DungChungSrv.DM_QuyenChucNang_GetByMa(model.Ma);
                        if (checkMa.Data != null && checkMa.Data.resultObject != null)
                            return Json(new { status = status, checkMa = true });
                    }
                    else
                    {
                        var temp = _DungChungSrv.DM_QuyenChucNang_GetById(model.Id);
                        if (temp.Data != null && temp.Data.resultObject != null)
                        {
                            if (temp.Data.resultObject.Ma != model.Ma)
                            {
                                var checkMa = _DungChungSrv.DM_QuyenChucNang_GetByMa(model.Ma);
                                if (checkMa.Data != null && checkMa.Data.resultObject != null)
                                {
                                    DungChung.ghinhatkynguoidung("Thêm mới quyền chức năng",
                                              "DM_QuyenChucNangController",
                                              "ThemMoi", "Create");
                                    return Json(new { status = status, checkMa = true });
                                }
                            }
                        }
                    }
                    var result = _DungChungSrv.DM_QuyenChuNang_InsUpd(model);
                    if (result.Data != null && result.Data.resultObject > 0)
                        status = true;
                }
                return Json(new { status = status, checkMa = false });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_QuyenChucNangController/ThemMoi error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }

        }
        [HttpPost]
        public ActionResult CapNhat(long id)
        {
            var model = new DM_QuyenChucNangAdd();
            try
            {
                if (id > 0)
                {
                    _DungChungSrv = new DungChungServiceClient();
                    var temp = _DungChungSrv.DM_QuyenChucNang_GetById(id);
                    if (temp.Data != null && temp.Data.resultObject != null)
                        model = temp.Data.resultObject;
                }
                return PartialView("DialogUpdate/Update", model);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_QuyenChucNangController/CapNhat error:" + ex.Message, ex, Request);
                return PartialView("DialogUpdate/Update", model);
            }
        }
        [CustomAuthorize(RightName = CookieRight.DM_QuyenChucNangController_Xoa)]

        public ActionResult Xoa(long id)
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
                    _DungChungSrv = new DungChungServiceClient();
                    Guid userId = Guid.NewGuid();
                    var temp = _DungChungSrv.DM_QuyenChucNang_Delete(id, userId);
                    if (temp.Data != null && temp.Data.resultObject == true)
                    {
                        DungChung.ghinhatkynguoidung("Xóa quyền chức năng",
                                              "DM_QuyenChucNangController",
                                              "Xoa", "Delete");
                        status = temp.Data.resultObject;
                    }
                }
                return Json(new { status = status });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_QuyenChucNangController/Xoa error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }

        }
        public ActionResult CBOActionByController(string id)
        {
            var list = new List<SelectListItem>();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    list = CBODungChung.GetAllAction(id).ToList(); ;
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
                DungChung.ghiloghethong("DM_QuyenChucNangController/CBOActionByController error:" + ex.Message, ex, Request);
                return new JsonResult
                {
                    Data = new
                    {
                        data = list
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

        }
    }
}