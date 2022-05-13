using Module.Framework;
using PagedList;
using System.Linq;
using System.Web.Mvc;
using CMS.Admin.Models;
using Business.Entities.Domain;
using System;
using System.Collections.Generic;
using CMS.Admin.Common;
using CMS.Admin.Constaints;
using Module.Framework.Common;

namespace CMS.Admin.Controllers
{
    public class LogSystemController : BaseController
    {
        private LoggingServiceClient _LogSrv;
        private int _pageSize;
        public LogSystemController()
        {
            var pageSize = DungChung.GetKeyCauHinhHeThong("SoHangHienThi");
            if (!int.TryParse(pageSize, out this._pageSize))
                this._pageSize = 10;
        }
        // GET: DM_QuocGia
        [CustomAuthorize(RightName = CookieRight.LogSystemController_Index)]
        public ActionResult Index()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var result = new LogViewModel();
            _LogSrv = new LoggingServiceClient();
            result.Search = new LogParam();
            result.Search.PageIndex = 1;
            result.Search.PageSize = this._pageSize;
            result.Search.CreatedTo = DateTime.Now.ToString("dd/MM/yyyy");
            try
            {
                var temp = _LogSrv.LogSystem_List(result.Search);
                if (temp.Data != null && temp.Data.resultObject != null && temp.Data.resultObject.Any())
                {

                    result.List = temp.Data.resultObject.ToPagedList(result.Search.PageIndex, this._pageSize);
                }
                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("LogSystemController/Index error:" + ex.Message, ex, Request);
                return View(result);
            }
        }
        [HttpPost]
        public ActionResult ListLog(LogParam model)
        {
            _LogSrv = new LoggingServiceClient();
            //model.PageSize = this._pageSize;
            try
            {
                var temp = _LogSrv.LogSystem_List(model);
                var result = new LogViewModel();
                if (temp.Data != null&& temp.Data.resultObject!=null && temp.Data.resultObject.Any())
                {
                    result.List = temp.Data.resultObject.ToPagedList(1, this._pageSize);
                }
                return PartialView("_PartialList", result.List);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("LogSystemController/ListLog error:" + ex.Message, ex, Request);
                return PartialView("_PartialList", null);
            }
        }        
        [HttpPost]
        public ActionResult ChiTietLog(long id)
        {
            try
            {
                var model = new LogAdd();
                if (id > 0)
                {
                    _LogSrv = new LoggingServiceClient();
                    var temp = _LogSrv.GetLogSystemById(id);
                    if (temp.Data != null && temp.Data.resultObject != null)
                        model = temp.Data.resultObject;
                }
                return PartialView("Dialog/Detail", model);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("LogSystemController/ChiTietLog error:" + ex.Message, ex, Request);
               return PartialView("Dialog/Detail", null);
            }
            
        }
        [CustomAuthorize(RightName = CookieRight.LogSystemController_Delete)]
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
                        long idlog = Int64.Parse(id);
                        using (_LogSrv = new LoggingServiceClient())
                        {
                            var tempList = _LogSrv.LogSystem_DelByID(idlog);
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
                    DungChung.ghiloghethong("LogSystemController/Delete error:" + ex.Message, ex, Request);
                    return Json(new { status = status });
                }
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("LogSystemController/Delete error:" + ex.Message, ex, Request);
                return Json(false);
            }
        }
        [CustomAuthorize(RightName = CookieRight.LogSystemController_Delete)]
        public ActionResult DeleteLst(List<long> ids)
        {
                bool status = false;
                try
                {
                    if (ids!=null && ids.Count>0)
                    {                        
                        using (_LogSrv = new LoggingServiceClient())
                        {
                            var tempList = _LogSrv.LogSystem_DelLstID(ids);
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
                DungChung.ghiloghethong("LogSystemController/DeleteLst error:" + ex.Message, ex, Request);
                    return Json(new { status = status });
                }
        }
        [CustomAuthorize(RightName = CookieRight.LogSystemController_Delete)]
        public ActionResult Trancate()
        {
            bool status = false;
            try
            {
                    using (_LogSrv = new LoggingServiceClient())
                    {
                        var tempList = _LogSrv.LogSystem_Trancate();
                        if (tempList.Data != null && tempList.Data.resultObject > 0)
                        {
                            status = true;
                        }
                    }
               
                return Json(new { status = status });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("LogSystemController/Trancate error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }
    }
}