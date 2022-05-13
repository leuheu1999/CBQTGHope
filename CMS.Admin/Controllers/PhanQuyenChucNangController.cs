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
using Module.Framework.Common;
using CMS.Admin.Constaints;
using CMS.Admin.Controllers;

namespace QuanTriModules.Controllers
{
    public class PhanQuyenChucNangController : BaseController
    {
        private UsersServiceClient _UsersSrv;
        private int _pageSize;
        private Guid NguoiDungID;
        public PhanQuyenChucNangController()
        {
            var pageSize = DungChung.GetKeyCauHinhHeThong("SoHangHienThi");
            if (!int.TryParse(pageSize, out this._pageSize))
                this._pageSize = 10;
            var us = LoginManager.GetCurrentUser();
            NguoiDungID = us != null ?Guid.Parse(us.UserId) : Guid.Empty;
        }
        // GET: NguoiDungHeThongController
        [CustomAuthorize(RightName = CookieRight.DM_PhanQuyenChucNangController_Index)]

        public ActionResult Index()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var result = new SYS_PhanQuyenMap();
            _UsersSrv = new UsersServiceClient();
            try
            { 
                var temp = _UsersSrv.PhanQuyenChucNang_getall();
                if (temp.Data != null && temp.Data.resultObject != null)
                {
                    DungChung.ghinhatkynguoidung("Xem danh sách phân quyền",
                                  "PhanQuyenChucNangController",
                                  "Index", "View");
                    result = temp.Data.resultObject;
                }
                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("PhanQuyenChucNangController/Index error:" + ex.Message, ex, Request);
                return View(result);
            }

        }
        [HttpPost]
        [CustomAuthorize(RightName = CookieRight.DM_PhanQuyenChucNangController_CapNhat)]
        [CheckForAntiForgeryToken]


        public ActionResult CapNhat(List<PQ_PhanQuyenChucNang> model)
        {
            try
            {
                if (DungChung.CheckTimeDN() == false)
                {
                    return RedirectToAction("LogOff", "NguoiDungHeThong");
                }
                bool status = true;
                if (model != null && model.Count>0)
                {
                    model[0].CreatedUserID = NguoiDungID;
                    model[0].LastUpdUserID = NguoiDungID;
                    _UsersSrv = new UsersServiceClient();
                    var result = _UsersSrv.PQ_PhanQuyenChucNang_Ins(model);
                    if (result.Data != null && result.Data.resultObject > 0)
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                }
                //log nhat ky nguoi dung
                DungChung.ghinhatkynguoidung("Phân quyền người dùng",
                                 "PhanQuyenChucNangController",
                                 "CapNhat", "Update");
                return Json(new { status = status});
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("PhanQuyenChucNangController/CapNhat error:" + ex.Message, ex, Request);
                return Json(new { status = false});
            }
        }
    }
}