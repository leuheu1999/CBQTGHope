using Business.Entities.Domain;
using CMS.Admin.Common;
using CMS.Admin.Constaints;
using CMS.Admin.Models.BC_SoGiayChungNhanBanQuyen;
using Module.Framework.Common;
using Module.Framework;
using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using Business.Entities;

namespace CMS.Admin.Controllers
{
    public class BC_SoGiayChungNhanBanQuyenController : BaseController
    {
        #region dungchung
        private BC_ThongKeServiceClient _bC_ThongKeSRV;
        private int _pageSize;
        #endregion
        public BC_SoGiayChungNhanBanQuyenController()
        {
            //var pageSize = DungChung.GetKeyCauHinhHeThong("SoHangHienThi");
            //if (!int.TryParse(pageSize, out this._pageSize))
            //    this._pageSize = 10;
            this._pageSize = 100;
        }
        [CustomAuthorize(RightName = CookieRight.BC_SoGiayChungNhanBanQuyenController_Index)]
        public ActionResult Index()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var result = new BC_SoGiayChungNhanBanQuyenViewModel();
            result.Search = new BC_SoGiayChungNhanBanQuyenParam();
            try
            {
                result.Search.PageSize = this._pageSize;
                result.Search.PageIndex = 1;
                var modelSearch = new BC_SoGiayChungNhanBanQuyenParam()
                {
                    KyBaoCao =1,
                    Nam = DateTime.Today.Year
                };
                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("BC_SoGiayChungNhanBanQuyenController/Index error:" + ex.Message, ex, Request);
                return View(result);
            }
        }
        [HttpPost]
        public ActionResult ListBC_SoGiayChungNhanBanQuyen(BC_SoGiayChungNhanBanQuyenParam model)
        {
            var result = new BC_SoGiayChungNhanBanQuyenViewModel();
            result.Search = model;
            try
            {
                _bC_ThongKeSRV = new BC_ThongKeServiceClient();
                var tempList = _bC_ThongKeSRV.BC_SoGiayChungNhanBanQuyen_List(model);
                if (tempList.Data != null && tempList.Data != null)
                {
                    DungChung.ghinhatkynguoidung("Số giấy chứng nhận bản quyền",
                                               "BC_SoGiayChungNhanBanQuyenController",
                                               "Index", "View");

                    result.Items = tempList.Data.resultObject;
                }
                return PartialView("_PartialList", result.Items);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("BC_SoGiayChungNhanBanQuyenController/ListBC_SoGiayChungNhanBanQuyen error:" + ex.Message, ex, Request);
                return PartialView("_PartialList", result.Items);
            }
        }
        [HttpPost]
        public ActionResult GetCBOLoaiHinhTacPham(string id)
        {
            DungChungServiceClient _serviceDungChung = new DungChungServiceClient();
            var listData = new List<SelectListItem>();
            var model = new CBO_DungChungParam();
            model.TableName = "DM_LOAIHINHTACPHAM";
            if (!string.IsNullOrEmpty(id))
            {
                model.ParentID1 = id;
            }
            var query = _serviceDungChung.CBO_DungChung_GetAll(model);
            if (query != null && query.resultObject != null && query.resultObject.Any())
            {
                listData = GenLoaiHinhTacPham(query.resultObject);
            }
            return new JsonResult { Data = new { data = listData }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        private static List<SelectListItem> GenLoaiHinhTacPham(List<CBO_DungChungViewModel> options)
        {
            var list = new List<SelectListItem>();
            var lstParent = options.Where(n => n.ParentID == null)
                                   .Select(m => new CBO_DungChungViewModel()
                                   {
                                       Ma1 = m.Ma1.ToString(),
                                       Ten1 = m.Ten1,
                                   }).ToList();

            foreach (var item in lstParent)
            {
                var listItem = new SelectListItem()
                {
                    Value = item.Ma1,
                    Text = item.Ten1,
                };
                list.Add(listItem);
                var lstChild = options.Where(n => n.ParentID == item.Ma1)
                                      .Select(m => new SelectListItem()
                                      {
                                          Value = m.Ma1,
                                          Text = " - " + m.Ten1
                                      }).ToList();
                list.AddRange(lstChild);
            }
            return list;
        }
    }
}