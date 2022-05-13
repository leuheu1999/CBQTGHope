using Business.Entities.Domain;
using CMS.Admin.Common;
using CMS.Admin.Constaints;
using Module.Framework;
using Module.Framework.Common;
using System;
using System.Web.Mvc;

namespace CMS.Admin.Controllers
{
    public class CauHinhHeThongController : BaseController
    {
        private DungChungServiceClient _DungChungSrv;
        private Guid NguoiDungID;
        public CauHinhHeThongController()
        {
            NguoiDungID = LoginManager.GetCurrentUser() != null ? Guid.Parse(LoginManager.GetCurrentUser().UserId) : Guid.Empty;
        }
        [CustomAuthorize(RightName = CookieRight.CauHinhHeThongController_Index)]
        public ActionResult Index()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var result = new CauHinhHeThongAll();
            _DungChungSrv = new DungChungServiceClient();
            try
            {                
              
                var temp = _DungChungSrv.GetAllCauHinh();
                if (temp.Data != null && temp.Data.resultObject != null)
                {
                    //log nhat ky nguoi dung
                    DungChung.ghinhatkynguoidung("Xem thông tin cấu hình hệ thống",
                                            "CauHinhHeThongController",
                                            "Index", "View");
                    result = temp.Data.resultObject;
                }
                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("CauHinhHeThongController/Index error" + ex.Message, ex, Request);
                return View(result);
            }
        }
        [HttpPost]
        [CustomAuthorize(RightName = CookieRight.CauHinhHeThongController_Save)]
        [CheckForAntiForgeryToken]

        public ActionResult Save(CauHinhHeThongAll model)
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
                    _DungChungSrv = new DungChungServiceClient();
                    model.cauhinhhethong.ImgUrl = DungChung.SxxEndcodeText(model.cauhinhhethong.ImgUrl);
                    model.cauhinhhethong.TieuDeTrang = DungChung.SxxEndcodeText(model.cauhinhhethong.TieuDeTrang);
                    model.cauhinhhethong.TuKhoa = DungChung.SxxEndcodeText(model.cauhinhhethong.TuKhoa);
                    model.cauhinhhethong.MoTaTuKhoa = DungChung.SxxEndcodeText(model.cauhinhhethong.MoTaTuKhoa);
                    model.cauhinhhethong.Logo = DungChung.SxxEndcodeText(model.cauhinhhethong.Logo);
                    model.cauhinhhethong.ThoiGianTruyCapTu = DungChung.SxxEndcodeText(model.cauhinhhethong.ThoiGianTruyCapTu);
                    model.cauhinhhethong.ThoiGianTruyCapDen = DungChung.SxxEndcodeText(model.cauhinhhethong.ThoiGianTruyCapDen);
                    model.cauhinhmail.Email = DungChung.SxxEndcodeText(model.cauhinhmail.Email);
                    model.cauhinhmail.TenHienThi = DungChung.SxxEndcodeText(model.cauhinhmail.TenHienThi);
                    model.cauhinhmail.Host = DungChung.SxxEndcodeText(model.cauhinhmail.Host);
                    model.cauhinhmail.Port = DungChung.SxxEndcodeText(model.cauhinhmail.Port);
                    model.cauhinhmail.NguoiDung = DungChung.SxxEndcodeText(model.cauhinhmail.NguoiDung);
                    model.cauhinhmail.MatKhau = DungChung.SxxEndcodeText(model.cauhinhmail.MatKhau);
                    model.donvi.TenDonVi = DungChung.SxxEndcodeText(model.donvi.TenDonVi);
                    model.donvi.TenVietTat = DungChung.SxxEndcodeText(model.donvi.TenVietTat);
                    model.donvi.DiaChi = DungChung.SxxEndcodeText(model.donvi.DiaChi);
                    model.donvi.DienThoai = DungChung.SxxEndcodeText(model.donvi.DienThoai);
                    model.donvi.Fax = DungChung.SxxEndcodeText(model.donvi.Fax);
                    //model.donvi.Email = DungChung.SxxEndcodeText(model.donvi.Email);
                    //model.donvi.Website = DungChung.SxxEndcodeText(model.donvi.Website);
                    //model.donvi.Copyright = DungChung.SxxEndcodeText(model.donvi.Copyright);
                    //model.donvi.FaceBook = DungChung.SxxEndcodeText(model.donvi.FaceBook);
                    //model.donvi.Zalo = DungChung.SxxEndcodeText(model.donvi.Zalo);
                    //model.donvi.Twitter = DungChung.SxxEndcodeText(model.donvi.Twitter);
                    //model.donvi.Youtube = DungChung.SxxEndcodeText(model.donvi.Youtube);
                    model.donvi.MoTaWebsite = DungChung.SxxEndcodeText(model.donvi.MoTaWebsite);

                    var result = _DungChungSrv.CauHinhheThong_Insert(model);
                    if (result.Data != null && result.Data.resultObject > 0)
                    {
                        //log nhat ky nguoi dung
                        DungChung.ghinhatkynguoidung("Sửa thông tin cấu hình hệ thống",
                                                "CauHinhHeThongController",
                                                "Save", "Update");
                        status = true;
                        _DungChungSrv.DisposeAllCache("CauHinhheThong");
                        _DungChungSrv.DisposeAllCache("DM_DonVi");
                    }
                    
                }
                return Json(new { status = status });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("CauHinhHeThongController/Luu error" + ex.Message, ex, Request);
                return Json(new { status = false});
            }
        }
        public ActionResult DoiMatKhau(string pass)
        {
            try
            {
                bool status = false;
                if (!string.IsNullOrEmpty(pass))
                {
                    _DungChungSrv = new DungChungServiceClient();
                    var result = _DungChungSrv.CauHinhMail_DoiMatKhau(pass);
                    if (result.Data != null && result.Data.resultObject > 0)
                    {
                        //log nhat ky nguoi dung
                        DungChung.ghinhatkynguoidung("Đổi mật khẩu cấu hình hệ thống",
                                                "CauHinhHeThongController",
                                                "DoiMatKhau", "Update");
                        status = true;
                    }
                }
                return Json(new { status = status });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("CauHinhHeThongController/DoiMatKhau error" + ex.Message, ex, Request);
                return Json(new { status = false });
            }
        }

    }
}