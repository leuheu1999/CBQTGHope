using Business.Entities.Domain;
using CMS.Admin.Common;
using CMS.Admin.Constaints;
using CMS.Admin.Models.HoSo;
using Module.Framework.Common;
using Module.Framework;
using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using CMS.Admin.Models.TT_CapQuyen;
using Core.Common.Utilities;

namespace CMS.Admin.Controllers
{
    public class HS_CapSoController : BaseController
    {
        #region dungchung
        private DungChung_1CuaServiceClient _DungChungSrv;
        private HS_CapSoServiceClient _hs_CapSoSrv;
        private int _pageSize;
        #endregion
        public HS_CapSoController()
        {
            var pageSize = DungChung.GetKeyCauHinhHeThong("SoHangHienThi");
            if (!int.TryParse(pageSize, out this._pageSize))
                this._pageSize = 10;
        }
        [CustomAuthorize(RightName = CookieRight.HS_CapSoController_Index)]
        public ActionResult Index()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var result = new TT_CapQuyenViewModel();
            result.Search = new TT_CapQuyenParam();
            try
            {
                result.Search.PageSize = this._pageSize;
                result.Search.PageIndex = 1;

                _hs_CapSoSrv = new HS_CapSoServiceClient();
                var tempList = _hs_CapSoSrv.TT_CapQuyen_List(result.Search);
                if (tempList != null && tempList.Data != null )
                {
                    DungChung.ghinhatkynguoidung("Xem danh sách hồ sơ",
                                               "HS_CapSoController",
                                               "Index", "View");

                    result.Items = tempList.Data.resultObject.ToPagedList(1, result.Search.PageSize);
                }
                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("HS_CapSoController/Index error:" + ex.Message, ex, Request);
                return View(result);
            }
        }

      
        [HttpPost]
        public ActionResult ListHoSo(TT_CapQuyenParam model)
        {
            var result = new TT_CapQuyenViewModel();
            result.Search = model;
            try
            {
                _hs_CapSoSrv = new HS_CapSoServiceClient();
                var tempList = _hs_CapSoSrv.TT_CapQuyen_List(result.Search);
                if (tempList != null && tempList.Data != null)
                {
                    DungChung.ghinhatkynguoidung("Xem danh sách hồ sơ",
                                               "HS_CapSoController",
                                               "Index", "View");

                    result.Items = tempList.Data.resultObject.ToPagedList(1, result.Search.PageSize);
                }
                return PartialView("_PartialList", result.Items);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("HS_CapSoController/ListHoSo error:" + ex.Message, ex, Request);
                return PartialView("_PartialList", result.Items);
            }
        }

        [HttpPost]

        public ActionResult Duyet(List<TT_CapQuyenAddTrangThai> model)
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
                    var message = "";
                    status = true;
                    foreach (var item in model)
                    {
                        try
                        {
                            var user = LoginManager.GetCurrentUser();
                            var gid = new Guid();
                            Guid.TryParse(user.UserId, out gid);
                            item.CreatedUserID = gid;
                            item.TrangThaiDuyet = 1;
                            _hs_CapSoSrv = new HS_CapSoServiceClient();
                            var temp = _hs_CapSoSrv.TT_CapQuyen_UpdTinhTrang(item);
                            if (temp == null || temp.Data == null || temp.Data.resultObject < 0)
                            {
                                status = false;
                                message += (string.IsNullOrEmpty(message) ? "" : ", ") + item.SoBienNhan;
                            }
                        }
                        catch (Exception ex)
                        {
                            DungChung.ghiloghethong("HS_CapSoController/Duyet error:" + ex.Message, ex, Request);
                            status = false;
                            message += (string.IsNullOrEmpty(message) ? "" : ", ") + item.SoBienNhan;
                        }

                    }
                }
                return Json(new { status = status });

            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("HS_CapSoController/Duyet error:" + ex.Message, ex, Request);
                return Json(false);
            }
        }

        [HttpPost]

        public ActionResult KhongDuyet(List<TT_CapQuyenAddTrangThai> model)
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
                    var message = "";
                    status = true;
                    foreach (var item in model)
                    {
                        try
                        {
                            var user = LoginManager.GetCurrentUser();
                            var gid = new Guid();
                            Guid.TryParse(user.UserId, out gid);
                            item.CreatedUserID = gid;
                            item.TrangThaiDuyet = 2;
                            _hs_CapSoSrv = new HS_CapSoServiceClient();
                            var temp = _hs_CapSoSrv.TT_CapQuyen_UpdTinhTrang(item);
                            if (temp == null || temp.Data == null || temp.Data.resultObject < 0)
                            {
                                status = false;
                                message += (string.IsNullOrEmpty(message) ? "" : ", ") + item.SoBienNhan;
                            }
                        }
                        catch (Exception ex)
                        {
                            DungChung.ghiloghethong("HS_CapSoController/KhongDuyet error:" + ex.Message, ex, Request);
                            status = false;
                            message += (string.IsNullOrEmpty(message) ? "" : ", ") + item.SoBienNhan;
                        }

                    }
                }
                return Json(new { status = status });

            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("HS_CapSoController/KhongDuyet error:" + ex.Message, ex, Request);
                return Json(false);
            }
        }

        [HttpPost]
        public ActionResult ListDMQuanHuyen(DM_1CuaParam model)
        {
            try
            {
                var temp = CBODungChung.GetCBOQuanHuyen_1Cua(model.ID);
                return new JsonResult
                {
                    Data = temp.ToList(),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("HS_CapSoController/ListDMQuanHuyen error:" + ex.Message, ex, Request);
                return new JsonResult
                {
                    Data = null,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
        [HttpPost]
        public ActionResult ListDMPhuongXa(DM_1CuaParam model)
        {
            try
            {
                var temp = CBODungChung.GetCBOPhuongXa_1Cua(model.ID);
                return new JsonResult
                {
                    Data = temp.ToList(),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("HS_CapSoController/ListDMPhuongXa error:" + ex.Message, ex, Request);
                return new JsonResult
                {
                    Data = null,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        [HttpPost]
        public ActionResult ListDMThuTuc(DM_1CuaParam model)
        {
            try
            {
                var temp = CBODungChung.GetCBOThuTuc_1Cua(model.ID);
                return new JsonResult
                {
                    Data = temp.ToList(),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("HS_CapSoController/ListDMThuTuc error:" + ex.Message, ex, Request);
                return new JsonResult
                {
                    Data = null,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }


    }
}