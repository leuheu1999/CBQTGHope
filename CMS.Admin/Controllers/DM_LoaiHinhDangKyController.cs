using Business.Entities.Domain;
using CMS.Admin.Common;
using CMS.Admin.Constaints;
using CMS.Admin.Models.DM_LoaiHinhDangKy;
using Module.Framework;
using Module.Framework.Common;
using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CMS.Admin.Controllers
{
    public class DM_LoaiHinhDangKyController : BaseController
    {
        #region dungchung
        private DungChungServiceClient _DungChungSrv;
        private int _pageSize;
        private Guid _nguoiDungID;
        #endregion

        public DM_LoaiHinhDangKyController()
        {
            var pageSize = DungChung.GetKeyCauHinhHeThong("SoHangHienThi");
            if (!int.TryParse(pageSize, out this._pageSize))
                this._pageSize = 10;
            _nguoiDungID = LoginManager.GetCurrentUser() != null ? Guid.Parse(LoginManager.GetCurrentUser().UserId) : Guid.Empty;
        }

        // GET: DM_LoaiHinhDanhKy
        [CustomAuthorize(RightName = CookieRight.DM_LoaiHinhDangKyController_Index)]
        public ActionResult Index()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var result = new DM_LoaiHinhDangKyViewModel();
            result.Search = new DM_LoaiHinhDangKyMapParam();
            try
            {
                result.Search.PageSize = this._pageSize;
                result.Search.PageIndex = 1;
                _DungChungSrv = new DungChungServiceClient();

                var tempList = _DungChungSrv.DM_LoaiHinhDangKy_List(result.Search);
                if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Any())
                {
                    DungChung.ghinhatkynguoidung("Xem danh sách loại hình đăng kí",
                                              "DM_LoaiHinhDangKyController",
                                              "Index", "View");
                    result.Items = tempList.Data.resultObject.ToPagedList(result.Search.PageIndex, result.Search.PageSize);
                }

                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_LoaiHinhDangKyController/Index error:" + ex.Message, ex, Request);
                return View(result);
            }
        }

        [HttpPost]
        [CustomAuthorize(RightName = CookieRight.DM_LoaiHinhDangKyController_ThemMoi)]
        [CheckForAntiForgeryToken]

        public ActionResult ThemMoi(DM_LoaiHinhDangKyMapAdd model)
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
                    model.CreatedUserID = _nguoiDungID;
                    model.LastUpdUserID = _nguoiDungID;
                    _DungChungSrv = new DungChungServiceClient();

                    if (model.LoaiHinhId == 0)
                    {
                        var checkMa = _DungChungSrv.DM_LoaiHinhDangKy_GetByMa(model.Ma);
                        if (checkMa.Data != null && checkMa.Data.resultObject != null)
                        {
                            DungChung.ghinhatkynguoidung("Thêm mới loại hình đăng ký",
                                              "DM_LoaiHinhDangKyController",
                                              "ThemMoi", "Create");
                            return Json(new { status = status, checkMa = true });
                        }
                    }
                    else
                    {
                        var temp = _DungChungSrv.DM_LoaiHinhDangKy_GetById(model.LoaiHinhId);
                        if (temp.Data != null && temp.Data.resultObject != null)
                        {
                            if (temp.Data.resultObject.Ma != model.Ma)
                            {
                                var checkMa = _DungChungSrv.DM_LoaiHinhDangKy_GetByMa(model.Ma);
                                if (checkMa.Data != null && checkMa.Data.resultObject != null)
                                    return Json(new { status = status, checkMa = true });
                            }
                        }
                    }

                    var result = _DungChungSrv.DM_LoaiHinhDangKy_InsUpd(model);
                    if (result.Data != null && result.Data.resultObject > 0)
                        status = true;
                }
                return Json(new { status = status});
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_LoaiHinhDangKyController/ThemMoi error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }

        [HttpPost]
        public ActionResult TimKiem(DM_LoaiHinhDangKyMapParam model)
        {
            var result = new DM_LoaiHinhDangKyViewModel();
            try
            {
                _DungChungSrv = new DungChungServiceClient();
                var tempList = _DungChungSrv.DM_LoaiHinhDangKy_List(model);
                if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Any())
                {
                    DungChung.ghinhatkynguoidung("Tìm kiếm loại hình đăng ký",
                                              "DM_LoaiHinhDangKyController",
                                              "TimKiem", "View");
                    result.Items = tempList.Data.resultObject.ToPagedList(1, model.PageSize);
                }
                return PartialView("_PartialList", result.Items);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_LoaiHinhDangKyController/TimKiem error:" + ex.Message, ex, Request);
                return PartialView("_PartialList", result.Items);
            }
        }
        [HttpPost]
        public ActionResult CapNhat(long id)
        {
            var model = new DM_LoaiHinhDangKyMapAdd();
            try
            {
                if (id > 0)
                {
                    _DungChungSrv = new DungChungServiceClient();
                    var temp = _DungChungSrv.DM_LoaiHinhDangKy_GetById(id);
                    if (temp.Data != null && temp.Data.resultObject != null)
                        model = temp.Data.resultObject;
                }
                return PartialView("DialogUpdate/Update", model);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_LoaiHinhDangKyController/Index error:" + ex.Message, ex, Request);
                return PartialView("DialogUpdate/Update", model);
            }

        }

        [HttpPost]
        [CustomAuthorize(RightName = CookieRight.DM_LoaiHinhDangKyController_Delete)]

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
                    var temp = _DungChungSrv.DM_LoaiHinhDangKy_Delete(id, userId);
                    if (temp.Data != null && temp.Data.resultObject == true)
                    {
                        DungChung.ghinhatkynguoidung("Xóa danh mục loại hình đăng ký",
                                              "DM_LoaiHinhDangKyController",
                                              "Xoa", "Delete");
                        status = temp.Data.resultObject;
                    }
                }
                return Json(new { status = status });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("DM_LoaiHinhDangKyController/Xoa error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }

        }
    }
}