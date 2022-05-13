using Business.Entities.Domain;
using CMS.Admin.Common;
using CMS.Admin.Constaints;
using CMS.Admin.Models;
using Module.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Admin.Controllers
{
    public class BC_BaoCaoTongHopHoatDongController : BaseController
    {
        #region dungchung
        private BC_ThongKeServiceClient _bC_ThongKeSRV;
        private int _pageSize;
        #endregion
        public BC_BaoCaoTongHopHoatDongController()
        {
            this._pageSize = 100;
        }
        [CustomAuthorize(RightName = CookieRight.BC_BaoCaoTongHopHoatDong_Index)]
        public ActionResult Index()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var result = new BC_BaoCaoTongHopHoatDongViewModel();
            result.Search = new BC_BaoCaoTongHopHoatDongParam();
            try
            {
                result.Search.PageSize = this._pageSize;
                result.Search.PageIndex = 1;
                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("BC_BaoCaoTongHopHoatDongController/Index error:" + ex.Message, ex, Request);
                return View(result);
            }
        }
        [HttpPost]
        public ActionResult ListBC_BaoCaoTongHopHoatDong(BC_BaoCaoTongHopHoatDongParam model)
        {
            var result = new BC_BaoCaoTongHopHoatDongViewModel();
            result.Search = model;
            try
            {
                _bC_ThongKeSRV = new BC_ThongKeServiceClient();
                var tempList = _bC_ThongKeSRV.BC_BaoCaoTongHopHoatDong_List(model);
                if (tempList.Data != null && tempList.Data != null)
                {
                    DungChung.ghinhatkynguoidung("Báo cáo tổng hợp hoạt động",
                                               "BC_BaoCaoTongHopHoatDongController",
                                               "Index", "View");

                    result.Items = tempList.Data.resultObject;
                }
                return PartialView("_PartialList", result.Items);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("BC_BaoCaoTongHopHoatDongController/ListBC_BaoCaoTongHopHoatDong error:" + ex.Message, ex, Request);
                return PartialView("_PartialList", result.Items);
            }
        }
    }
}