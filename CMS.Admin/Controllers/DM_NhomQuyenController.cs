using Business.Entities.Domain;
using CMS.Admin.Common;
using CMS.Admin.Constaints;
using CMS.Admin.Models.DM_NhomQuyen;
using Module.Framework.Common;
using Module.Framework;
using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CMS.Admin.Controllers
{
    public class DM_NhomQuyenController : BaseController
    {
        #region dungchung
        private DungChungServiceClient _DungChungSrv;
        private int _pageSize;
        #endregion
        public DM_NhomQuyenController()
        {
            var pageSize = DungChung.GetKeyCauHinhHeThong("SoHangHienThi");
            if (!int.TryParse(pageSize, out this._pageSize))
                this._pageSize = 10;
        }
        [CustomAuthorize(RightName = CookieRight.DM_NhomQuyenController_Index)]
        public ActionResult Index()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var result = new DM_NhomQuyenViewModel();
            result.Search = new DM_NhomQuyenParam();
            try
            {
                result.Search.PageSize = this._pageSize;
                result.Search.PageIndex = 1;
                _DungChungSrv = new DungChungServiceClient();
                var tempList = _DungChungSrv.DM_NhomQuyen_List(result.Search);
                if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Any())
                {
                    DungChung.ghinhatkynguoidung("Xem danh sách nhóm quyền",
                                               "DM_NhomQuyenController",
                                               "Index", "View");
                    result.Items = tempList.Data.resultObject.ToPagedList(result.Search.PageIndex, result.Search.PageSize);
                }
                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_NhomQuyenController/Index error:" + ex.Message, ex, Request);
                return View(result);
            }
        }
        [HttpPost]
        public ActionResult ListDMNhomQuyen(DM_NhomQuyenParam model)
        {
            var result = new DM_NhomQuyenViewModel();
            try
            {
                _DungChungSrv = new DungChungServiceClient();
                //model.PageSize = this._pageSize;
                var tempList = _DungChungSrv.DM_NhomQuyen_List(model);
                if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Any())
                {
                    DungChung.ghinhatkynguoidung("Tìm kiếm nhóm quyền",
                                               "DM_NhomQuyenController",
                                               "Index", "View");
                    result.Items = tempList.Data.resultObject.ToPagedList(1, this._pageSize);
                }
                return PartialView("_PartialList", result.Items);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_NhomQuyenController/ListDMNhomQuyen error:" + ex.Message, ex, Request);
                return PartialView("_PartialList", result.Items);
            }
        }
        [HttpPost]
        [CustomAuthorize(RightName = CookieRight.DM_NhomQuyenController_ThemMoi)]
        [CheckForAntiForgeryToken]

        public ActionResult ThemMoi(DM_NhomQuyenAdd model)
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
                        var checkMa = _DungChungSrv.DM_NhomQuyen_GetByMa(model.Ma);
                        if (checkMa.Data != null && checkMa.Data.resultObject != null)
                            return Json(new { status = status, checkMa = true });
                    }
                    else
                    {
                        var temp = _DungChungSrv.DM_NhomQuyen_GetById(model.Id);
                        if (temp.Data != null && temp.Data.resultObject != null)
                        {
                            if (temp.Data.resultObject.Ma != model.Ma)
                            {
                                var checkMa = _DungChungSrv.DM_NhomQuyen_GetByMa(model.Ma);
                                if (checkMa.Data != null && checkMa.Data.resultObject != null)
                                {
                                    
                                    return Json(new { status = status, checkMa = true });

                                }
                            }
                        }
                    }
                    model.Ma = DungChung.SxxEndcodeText(model.Ma);
                    model.Ten = DungChung.SxxEndcodeText(model.Ten);
                    model.MoTa = DungChung.SxxEndcodeText(model.MoTa);
                    var result = _DungChungSrv.DM_NhomQuyen_InsUpd(model);
                    if (result.Data != null && result.Data.resultObject > 0)
                    {
                        DungChung.ghinhatkynguoidung("Thêm sửa nhóm quyền",
                                               "DM_NhomQuyenController",
                                               "ThemMoi", "Create");
                        status = true;
                    }
                }
                return Json(new { status = status, checkMa = false });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_NhomQuyenController/ThemMoi error:" + ex.Message, ex, Request);
                return Json(new { status = false });
            }

        }
        [HttpPost]
        public ActionResult CapNhat(string id)
        {
            try
            {
                var model = new DM_NhomQuyenAdd();
                if(!string.IsNullOrEmpty(id))
                {
                    _DungChungSrv = new DungChungServiceClient();
                    var temp = _DungChungSrv.DM_NhomQuyen_GetById(Int64.Parse(id));
                    if (temp.Data != null && temp.Data.resultObject != null)
                        model = temp.Data.resultObject;
                }
                return PartialView("DialogUpdate/Update", model);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_NhomQuyenController/CapNhat error:" + ex.Message, ex, Request);
                return PartialView("DialogUpdate/Update", null);
            }

        }
        [CustomAuthorize(RightName = CookieRight.DM_NhomQuyenController_Xoa)]
        public ActionResult Xoa(long id)
        {
            try
            {
                if (DungChung.CheckTimeDN() == false)
                {
                    return RedirectToAction("LogOff", "NguoiDungHeThong");
                }
                bool status = false;
                if (id > 0)
                {
                    _DungChungSrv = new DungChungServiceClient();
                    Guid userId = Guid.NewGuid();
                    var temp = _DungChungSrv.DM_NhomQuyen_Delete(id, userId);
                    if (temp.Data != null && temp.Data.resultObject == true)
                    {
                        DungChung.ghinhatkynguoidung("Xóa nhóm quyền",
                                               "DM_NhomQuyenController",
                                               "Xoa", "Delete");
                        status = temp.Data.resultObject;

                    }
                }
                return Json(new { status = status });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_NhomQuyenController/Xoa error:" + ex.Message, ex, Request);
                return Json(new { status = false });
            }
        }
    }
}