using Module.Framework;
using PagedList;
using System.Linq;
using System.Web.Mvc;
using CMS.Admin.Models;
using Business.Entities.Domain;
using CMS.Admin.Common;
using System.Collections.Generic;
using System;
using Core.Common.Utilities;
using CMS.Admin.Constaints;
using Module.Framework.Common;

namespace CMS.Admin.Controllers
{
    public class NhatKyNguoiDungController : BaseController
    {
        private UsersServiceClient _UsersSrv;
        private int _pageSize;
        private Guid NguoiDungID;
        public NhatKyNguoiDungController()
        {
            var pageSize = DungChung.GetKeyCauHinhHeThong("SoHangHienThi");
            if (!int.TryParse(pageSize, out this._pageSize))
                this._pageSize = 10;
            NguoiDungID = LoginManager.GetCurrentUser() != null ? Guid.Parse(LoginManager.GetCurrentUser().UserId) : Guid.Empty;
        }
        // GET: NhatKyNguoiDungController
        [CustomAuthorize(RightName = CookieRight.NhatKyNguoiDungController_Index)]

        public ActionResult Index()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var result = new NhatKyNguoiDungViewModel();
            _UsersSrv = new UsersServiceClient();
            try
            {                
                result.Search = new ND_NhatKyNguoiDungParam();
                result.Search.PageIndex = 1;
                result.Search.PageSize = this._pageSize;
                TimeSpan aInterval = new System.TimeSpan(5, 0, 0, 0);
                result.Search.DenNgay= DateTime.Now.ToString("dd/MM/yyyy");
                result.Search.TuNgay = DateTime.Now.Subtract(aInterval).ToString("dd/MM/yyyy");
                var temp = _UsersSrv.NhatKyNguoiDung_List(result.Search);
                if (temp.Data != null && temp.Data.resultObject != null && temp.Data.resultObject.Any())
                {
                    result.Items = temp.Data.resultObject.ToPagedList(result.Search.PageIndex, this._pageSize);
                }
                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("NhatKyNguoiDungController/Index error:" + ex.Message, ex, Request);
                return View(result);
            }

        }
        [HttpPost]
        public ActionResult ListNhatKy(ND_NhatKyNguoiDungParam model)
        {
            try
            {
                _UsersSrv = new UsersServiceClient();
                //model.PageSize = this._pageSize;
                var temp = _UsersSrv.NhatKyNguoiDung_List(model);
                var result = new NhatKyNguoiDungViewModel();
                if (temp.Data != null&& temp.Data.resultObject!=null && temp.Data.resultObject.Any())
                {
                    result.Items = temp.Data.resultObject.ToPagedList(1, this._pageSize);
                }
                return PartialView("_PartialList", result.Items);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("NhatKyNguoiDungController/ListNhatKy error:" + ex.Message, ex, Request);
                return PartialView("_PartialList", null);
            }
        }
        [CustomAuthorize(RightName = CookieRight.NhatKyNguoiDungController_Delete)]

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
                        long idnd = long.Parse(id);
                        using (_UsersSrv = new UsersServiceClient())
                        {
                            var tempList = _UsersSrv.NhatKyNguoiDung_Del(idnd);
                            if (tempList.Data != null && tempList.Data.resultObject > 0)
                            {
                                status = true;
                            }
                        }
                    }
                    return Json(new { status = status });
                }
                catch (Exception ex)
                {
                    DungChung.ghiloghethong("NhatKyNguoiDungController/Delete error:" + ex.Message, ex, Request);
                    return Json(new { status = status });
                }
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("NhatKyNguoiDungController/Delete error:" + ex.Message, ex, Request);
                return Json(false);
            }
        }
        [CustomAuthorize(RightName = CookieRight.NhatKyNguoiDungController_Delete)]
        public ActionResult DeleteLst(List<long> ids)
        {
            bool status = false;
            try
            {
                if (ids != null && ids.Count > 0)
                {
                    using (_UsersSrv = new UsersServiceClient())
                    {
                        var tempList = _UsersSrv.NhatKyNguoiDung_DelLstID(ids);
                        if (tempList.Data != null && tempList.Data.resultObject > 0)
                        {
                            status = true;
                        }
                    }
                }
                return Json(new { status = status });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("NhatKyNguoiDungController/DeleteLst error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }
        public ActionResult LogTypeIndex()
        {
            if (NguoiDungID.ToString().ToUpper() != "93D0C829-163A-4315-B298-003CE2126BF3")
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var result = new LogTypeNDViewModel();
            _UsersSrv = new UsersServiceClient();
            try
            {
                result.Search = new LogTypeNDParam();
                result.Search.PageIndex = 1;
                result.Search.PageSize = this._pageSize;
                var temp = _UsersSrv.LogTypeND_List(result.Search);
                if (temp.Data != null && temp.Data.resultObject != null && temp.Data.resultObject.Any())
                {
                    result.Items = temp.Data.resultObject.ToPagedList(result.Search.PageIndex, this._pageSize);
                }
                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("NhatKyNguoiDungController/LogTypeIndex error:" + ex.Message, ex, Request);
                return View(result);
            }

        }
        [HttpPost]
        public ActionResult ListLogType(LogTypeNDParam model)
        {
            try
            {
                _UsersSrv = new UsersServiceClient();
                //model.PageSize = this._pageSize;
                var temp = _UsersSrv.LogTypeND_List(model);
                var result = new LogTypeNDViewModel();
                if (temp.Data != null && temp.Data.resultObject != null && temp.Data.resultObject.Any())
                {
                    result.Items = temp.Data.resultObject.ToPagedList(1, this._pageSize);
                }
                return PartialView("_LogTypePartialList", result.Items);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("NhatKyNguoiDungController/LogTypeIndex error:" + ex.Message, ex, Request);
                return PartialView("_LogTypePartialList", null);
            }
        }
        [HttpPost]
        public ActionResult LuuLogType(string ids, string idsUn)
        {
            bool status = false;
            try
            {
                if (!string.IsNullOrEmpty(ids) || !string.IsNullOrEmpty(idsUn))
                {
                    _UsersSrv = new UsersServiceClient();
                    var result = _UsersSrv.LogTypeND_Update(ids, idsUn);
                    if (result.Data != null && result.Data.resultObject > 0)
                        status = true;
                }
                return Json(new { status = status });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("NhatKyNguoiDungController/ListLogType error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }
    }
}