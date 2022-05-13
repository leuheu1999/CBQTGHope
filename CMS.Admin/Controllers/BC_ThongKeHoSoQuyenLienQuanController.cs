using Business.Entities.Domain;
using CMS.Admin.Common;
using CMS.Admin.Constaints;
using CMS.Admin.Models.BC_ThongKeHoSoQuyenLienQuan;
using Module.Framework.Common;
using Module.Framework;
using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using Core.Common.Utilities;

namespace CMS.Admin.Controllers
{
    public class BC_ThongKeHoSoQuyenLienQuanController : BaseController
    {
        #region dungchung
        private BC_ThongKeServiceClient _bC_ThongKeSRV;
        private int _pageSize;
        #endregion
        public BC_ThongKeHoSoQuyenLienQuanController()
        {
            //var pageSize = DungChung.GetKeyCauHinhHeThong("SoHangHienThi");
            //if (!int.TryParse(pageSize, out this._pageSize))
            //    this._pageSize = 10;
            this._pageSize = 100;
        }
        [CustomAuthorize(RightName = CookieRight.BC_ThongKeHoSoQuyenLienQuanController_Index)]
        public ActionResult Index()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var result = new BC_ThongKeHoSoQuyenLienQuanViewModel();
            result.Search = new BC_ThongKeHoSoQuyenLienQuanParam();
            try
            {
                result.Search.PageSize = this._pageSize;
                result.Search.PageIndex = 1;
                var modelSearch = new BC_ThongKeHoSoQuyenLienQuanParam()
                {
                    Nam = DateTime.Today.Year
                };
                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("BC_ThongKeHoSoQuyenLienQuanController/Index error:" + ex.Message, ex, Request);
                return View(result);
            }
        }
        [HttpPost]
        public ActionResult ListBC_ThongKeHoSoQuyenLienQuan(BC_ThongKeHoSoQuyenLienQuanParam model)
        {
            var result = new BC_ThongKeHoSoQuyenLienQuanViewModel();
            result.Search = model;
            try
            {
                _bC_ThongKeSRV = new BC_ThongKeServiceClient();
                var tempList = _bC_ThongKeSRV.BC_ThongKeHoSoQuyenLienQuan_Dashboard(model);
                if (tempList.Data != null && tempList.Data != null)
                {
                    DungChung.ghinhatkynguoidung("BIỂU ĐỒ THỐNG KÊ HỒ SƠ QUYỀN TÁC GIẢ",
                                               "BC_ThongKeHoSoQuyenLienQuanController",
                                               "Index", "View");

                    result.Items = tempList.Data.resultObject;
                }
                return new JsonResult
                {
                    Data = result.Items,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("BC_ThongKeHoSoQuyenLienQuanController/ListBC_ThongKeHoSoQuyenLienQuan error:" + ex.Message, ex, Request);
                return PartialView("_PartialList", result.Items);
            }
        }
       
    }
}