using Business.Entities.Domain;
using CMS.Admin.Common;
using CMS.Admin.Constaints;
using CMS.Admin.Models;
using Module.Framework.Common;
using Module.Framework;
using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using Core.Common.Utilities;
using Module.Framework.Utilities;
using System.Web.Configuration;
using System.Net;

namespace CMS.Admin.Controllers
{
    public class QTG_QuyenTacGiaController : BaseController
    {
        private int _pageSize;
        private Guid _nguoiDungID;
        private long _userPortalID;
        public QTG_QuyenTacGiaController()
        {
            var pageSize = DungChung.GetKeyCauHinhHeThong("SoHangHienThi");
            if (!int.TryParse(pageSize, out this._pageSize))
                this._pageSize = 10;
            _nguoiDungID = LoginManager.GetCurrentUser() != null ? Guid.Parse(LoginManager.GetCurrentUser().UserId) : Guid.Empty;
            _userPortalID = LoginManager.GetCurrentUser() != null ? LoginManager.GetCurrentUser().UserPortalID : 0;
        }

        #region QuyenTacGia
        [CustomAuthorize(RightName = CookieRight.QTG_QuyenTacGia_Index)]
        public ActionResult Index()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var result = new QTG_QuyenTacGiaViewModel();
            result.Search = new QTG_QuyenTacGiaParam();
            try
            {
                result.Search.PageSize = this._pageSize;
                result.Search.PageIndex = 1;
                using (var _quyenTacGiaSVR = new QuyenTacGiaServiceClient())
                {
                    var tempList = _quyenTacGiaSVR.QTG_QuyenTacGia_List(result.Search);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Any())
                    {
                        DungChung.ghinhatkynguoidung("Xem danh sách quyền tác giả",
                                               "QTG_QuyenTacGiaController",
                                               "Index", "View");
                        result.List = tempList.Data.resultObject.ToPagedList(1, result.Search.PageSize);
                    }
                }
                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/Index error:" + ex.Message, ex, Request);
                return View(result);
            }
        }
        [HttpPost]
        public ActionResult Index(QTG_QuyenTacGiaParam model)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var result = new QTG_QuyenTacGiaViewModel();
            try
            {
                //model.PageSize = this._pageSize;
                using (var _quyenTacGiaSVR = new QuyenTacGiaServiceClient())
                {
                    var tempList = _quyenTacGiaSVR.QTG_QuyenTacGia_List(model);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Any())
                    {
                        DungChung.ghinhatkynguoidung("Xem danh sách quyền tác giả",
                                               "QTG_QuyenTacGiaController",
                                               "Index", "View");
                        result.List = tempList.Data.resultObject.ToPagedList(1, model.PageSize);
                    }
                }
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_IndexPartialView", result);
                }
                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/Index error:" + ex.Message, ex, Request);
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_IndexPartialView", result);
                }
                return View(result);
            }
        }

        [CustomAuthorize]
        [CustomAuthorize(RightName = CookieRight.QTG_QuyenTacGia_InsUpd)]
        public ActionResult Insert(string id)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var result = new QTG_QuyenTacGiaAdd();
            result.LoaiDangKyID = 1;
            result.HinhDaiDienStt = -1;
            result.PhimStt = -1;
            result.NgayCap = DateTime.Today.ToString("dd/MM/yyyy");
            try
            {
                ViewBag.TieuDe = "Cấp giấy chứng nhận đăng ký quyền tác giả - Nhập tác phẩm";
                if (id != "" && id != null)
                {
                    using (var _quyenTacGiaSVR = new QuyenTacGiaServiceClient())
                    {
                        var tempList = _quyenTacGiaSVR.QTG_QuyenTacGia_ById(long.Parse(id));
                        if (tempList != null && tempList.Data != null && tempList.Data.resultObject != null)
                        {
                            result = tempList.Data.resultObject;
                        }
                    }
                }
                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/Insert error:" + ex.Message, ex, Request);
                return View(result);
            }
        }

        [CustomAuthorize]
        [CustomAuthorize(RightName = CookieRight.QTG_QuyenTacGia_InsUpd)]
        public ActionResult CapMoi(string id, string hoSoId, bool? isMap)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            int hoSoOnlineID = 0;
            var result = new QTG_QuyenTacGiaAdd();
            result.ListDinhKem = new List<QTG_DinhKemAdd>();
            result.LoaiDangKyID = 1;
            result.HinhDaiDienStt = -1;
            result.PhimStt = -1;
            result.NgayCap = DateTime.Today.ToString("dd/MM/yyyy");
            if (hoSoId != "" && hoSoId != null)
                result.HoSoID = long.Parse(hoSoId);
            try
            {
                ViewBag.TieuDe = "Cấp giấy chứng nhận đăng ký quyền tác giả - Nhập tác phẩm";
                //isMap == true đi từ màn hình hồ sơ qua
                //isMap == null chọn từ kho DVC
                if (hoSoId != "" && hoSoId != null)
                {
                    if (!string.IsNullOrEmpty(id) && isMap == null)
                    {
                        using (var _dvcQuyenTacGiaSVR = new DVC_QuyenTacGiaServiceClient())
                        {
                            var tempList = _dvcQuyenTacGiaSVR.DVC_QTG_QuyenTacGia_ById(long.Parse(id));
                            if (tempList != null && tempList.Data != null && tempList.Data.resultObject != null)
                            {
                                result.STT = tempList.Data.resultObject.STT;
                                result.VungMienID = tempList.Data.resultObject.VungMienID;
                                result.CoNguoiDaiDien = tempList.Data.resultObject.CoNguoiDaiDien;
                                result.NDDHoVaTen = tempList.Data.resultObject.NDDHoVaTen;
                                result.NDDSoCMND = tempList.Data.resultObject.NDDSoCMND;
                                result.NDDSoDienThoai = tempList.Data.resultObject.NDDSoDienThoai;
                                result.NDDDiaChi = tempList.Data.resultObject.NDDDiaChi;
                                result.IsToChuc = tempList.Data.resultObject.IsToChuc;
                                result.LoaiDangKyID = tempList.Data.resultObject.LoaiDangKyID;
                                result.NgayHT = tempList.Data.resultObject.NgayHT;
                                result.TenTacPham = tempList.Data.resultObject.TenTacPham;
                                result.PhimID = tempList.Data.resultObject.PhimID;
                                result.PhimStt = tempList.Data.resultObject.PhimStt;
                                result.TenPhim = tempList.Data.resultObject.TenPhim;
                                result.TrangThaiCongBoID = tempList.Data.resultObject.TrangThaiCongBoID;
                                result.NgayCongBo = tempList.Data.resultObject.NgayCongBo;
                                result.LoaiHinhID = tempList.Data.resultObject.LoaiHinhID;
                                result.NoiCongBo = tempList.Data.resultObject.NoiCongBo;
                                result.HinhDaiDienID = tempList.Data.resultObject.HinhDaiDienID;
                                result.HinhDaiDienStt = tempList.Data.resultObject.HinhDaiDienStt;
                                result.HinhDaiDienUrl = tempList.Data.resultObject.HinhDaiDienUrl;
                                result.LoaiNghiepVuID = tempList.Data.resultObject.LoaiNghiepVuID;
                                result.TenLoaiNghiepVu = tempList.Data.resultObject.TenLoaiNghiepVu;
                                result.IsDauKy = tempList.Data.resultObject.IsDauKy;
                                result.TrangThaiID = tempList.Data.resultObject.TrangThaiID;
                                result.TenTrangThai = tempList.Data.resultObject.TenTrangThai;
                                result.KeyMapID = tempList.Data.resultObject.KeyMapID;
                                result.SoLanCapLai = tempList.Data.resultObject.SoLanCapLai;
                                result.SoLanThuHoi = tempList.Data.resultObject.SoLanThuHoi;
                                result.SoLanCapDoi = tempList.Data.resultObject.SoLanCapDoi;
                                result.SoLanDoiChu = tempList.Data.resultObject.SoLanDoiChu;
                                result.NguoiKyID = tempList.Data.resultObject.NguoiKyID;
                                result.NgayKy = tempList.Data.resultObject.NgayKy;
                                result.StaticID = tempList.Data.resultObject.StaticID;
                                result.DVCID = long.Parse(id);
                                result.UserID = tempList.Data.resultObject.UserID;
                                result.HoSoID = tempList.Data.resultObject.HoSoID;
                                result.ThuTucID = tempList.Data.resultObject.ThuTucID;
                                result.TenThuTuc = tempList.Data.resultObject.TenThuTuc;
                                result.MaBoHoSo = tempList.Data.resultObject.MaBoHoSo;
                                result.SoBienNhan = tempList.Data.resultObject.SoBienNhan;
                                result.ListHinhAnh = tempList.Data.resultObject.ListHinhAnh;
                                result.ListPhim = tempList.Data.resultObject.ListPhim;
                                if (tempList.Data.resultObject.ListTacGia != null && tempList.Data.resultObject.ListTacGia.Any())
                                {
                                    result.ListTacGia = tempList.Data.resultObject.ListTacGia.Select(m => new QTG_TacGiaAdd()
                                    {
                                        TacGiaID = m.TacGiaID,
                                        HoVaTen = m.HoVaTen,
                                        QuocTich = m.QuocTich,
                                        SoCMND = m.SoCMND,
                                        DiaChi = m.DiaChi,
                                        UserID = m.UserID
                                    }).ToList();
                                }
                                if (tempList.Data.resultObject.ListChuSoHuu != null && tempList.Data.resultObject.ListChuSoHuu.Any())
                                {
                                    result.ListChuSoHuu = tempList.Data.resultObject.ListChuSoHuu.Select(m => new QTG_ChuSoHuuAdd()
                                    {
                                        ChuSoHuuID = m.ChuSoHuuID,
                                        HoVaTen = m.HoVaTen,
                                        QuocTich = m.QuocTich,
                                        SoCMND = m.SoCMND,
                                        DiaChi = m.DiaChi,
                                        UserID = m.UserID
                                    }).ToList();
                                }
                                if (tempList.Data.resultObject.ListDinhKem != null && tempList.Data.resultObject.ListDinhKem.Any())
                                {
                                    result.ListDinhKem = tempList.Data.resultObject.ListDinhKem.Select(m => new QTG_DinhKemAdd()
                                    {
                                        DinhKemID = m.DinhKemID,
                                        LoaiXuLyID = m.LoaiXuLyID,
                                        Ten = m.Ten,
                                        Tag = m.Tag,
                                        TenFile = m.TenFile,
                                        DuongDan = m.DuongDan,
                                        GhiChu = m.GhiChu,
                                        CreatedUser = m.CreatedUser,
                                        CreatedDate = m.CreatedDate,
                                        UserID = m.UserID,
                                        IsMotCua = m.IsMotCua
                                    }).ToList();
                                }
                            }
                        }
                    }
                    else if (!string.IsNullOrEmpty(id) && isMap == true)
                    {
                        using (var _quyenTacGiaSVR = new QuyenTacGiaServiceClient())
                        {
                            var tempList = _quyenTacGiaSVR.QTG_QuyenTacGia_ById(long.Parse(id));
                            if (tempList != null && tempList.Data != null && tempList.Data.resultObject != null)
                            {
                                result = tempList.Data.resultObject;
                            }
                        }
                    }
                    else
                    {
                        using (var _dungChungSVR = new DungChung_1CuaServiceClient())
                        {
                            var tempList = _dungChungSVR.GetChiTietHoSo(long.Parse(hoSoId), _userPortalID);
                            if (tempList != null && tempList.Data != null)
                            {
                                hoSoOnlineID = tempList.Data.HoSoOnlineID;
                                result.TenTacPham = tempList.Data.NoiDungKhac;
                                if (tempList.Data.DSNguoiDungTenCaNhan != null && tempList.Data.DSNguoiDungTenCaNhan.Any())
                                {
                                    result.CoNguoiDaiDien = true;
                                    result.NDDHoVaTen = tempList.Data.DSNguoiDungTenCaNhan[0].DungTen_HoVaTen;
                                    result.NDDSoCMND = tempList.Data.DSNguoiDungTenCaNhan[0].DungTen_SoGiayToTuyThan;
                                    result.NDDSoDienThoai = tempList.Data.DSNguoiDungTenCaNhan[0].DungTen_DienThoai;
                                    result.NDDDiaChi = tempList.Data.DSNguoiDungTenCaNhan[0].DungTen_DiaChiThuongTru;
                                }
                                //if (tempList.Data.DSHoSoKemTheo != null && tempList.Data.DSHoSoKemTheo.Any())
                                //{
                                //    foreach (var item in tempList.Data.DSHoSoKemTheo)
                                //    {
                                //        var dinhKem = new QTG_DinhKemAdd();
                                //        dinhKem.DinhKemID = item.HoSoKemTheoID;
                                //        dinhKem.Ten = item.TenHoSoKemTheo;
                                //        dinhKem.CreatedUser = item.NguoiDinhKem;
                                //        dinhKem.TenFile = item.OriginalName;
                                //        dinhKem.GhiChu = item.GhiChu;
                                //        dinhKem.DuongDan = item.UploadName;
                                //        dinhKem.IsMotCua = true;
                                //        result.ListDinhKem.Add(dinhKem);
                                //    }
                                //}
                            }
                        }
                        if (hoSoOnlineID > 0)
                        {
                            QTG_TacGiaAdd tacGia = new QTG_TacGiaAdd();
                            QTG_ChuSoHuuAdd chuSoHuu = new QTG_ChuSoHuuAdd();
                            TT_CongDanAdd congDanTG = new TT_CongDanAdd();
                            TT_CongDanAdd congDanCSH = new TT_CongDanAdd();
                            using (var _dungChungSVR = new DungChung_DVCServiceClient())
                            {
                                var detailDVCHoSo = _dungChungSVR.GetChiTietHoSo(hoSoOnlineID);
                                if (detailDVCHoSo != null && detailDVCHoSo.data != null && detailDVCHoSo.data.thongTinDangKy.Any())
                                {
                                    foreach (var item in detailDVCHoSo.data.thongTinDangKy)
                                    {
                                        switch (item.name)
                                        {
                                            case "NgayHoanThanhTP":
                                                result.NgayHT = item.value;
                                                break;
                                            case "CongBo":
                                                result.TrangThaiCongBoID = (item.value == "Đã công bố" ? 1 : 2);
                                                break;
                                            case "NoiCongBo":
                                                result.NoiCongBo = item.value;
                                                break;
                                            case "NgayCongBo":
                                                result.NgayCongBo = item.value;
                                                break;
                                            case "TenTacGia":
                                                tacGia.HoVaTen = item.value;
                                                congDanTG.HoVaTen = item.value;
                                                break;
                                            case "ButDanh":
                                                congDanTG.ButDanh = item.value;
                                                break;
                                            case "CCCDTacGia":
                                                tacGia.SoCMND = item.value;
                                                congDanTG.SoCMND = item.value;
                                                break;
                                            case "NgayCapCCCDTG":
                                                congDanTG.NgayCapCMND = item.value;
                                                break;
                                            case "NoiCapCCCDTG":
                                                congDanTG.NoiCap = item.value;
                                                break;
                                            case "QTTacGia":
                                                tacGia.QuocTich = item.value;
                                                congDanTG.QuocTich = item.value;
                                                break;
                                            case "DiaChiTacGia":
                                                tacGia.DiaChi = item.value;
                                                congDanTG.DiaChi = item.value;
                                                break;
                                            case "TenChuSoHuu":
                                                chuSoHuu.HoVaTen = item.value;
                                                congDanCSH.HoVaTen = item.value;
                                                break;
                                            case "CCCDChuSoHuu":
                                                chuSoHuu.SoCMND = item.value;
                                                congDanCSH.SoCMND = item.value;
                                                break;
                                            case "NgayCapCCCDChu":
                                                congDanCSH.NgayCapCMND = item.value;
                                                break;
                                            case "NoiCaoCCCDChu":
                                                congDanCSH.NoiCap = item.value;
                                                break;
                                            case "DKKD":
                                                chuSoHuu.SoDKKD = item.value;
                                                congDanCSH.SoDKKD = item.value;
                                                break;
                                            case "NgayCapDKKD":
                                                congDanCSH.NgayCapDKKD = item.value;
                                                break;
                                            case "NoiCapDKKD":
                                                congDanCSH.NoiCapDKKD = item.value;
                                                break;
                                            case "QTChuSoHuu":
                                                chuSoHuu.QuocTich = item.value;
                                                congDanCSH.QuocTich = item.value;
                                                break;
                                            case "DiaChiChu":
                                                chuSoHuu.DiaChi = item.value;
                                                congDanCSH.DiaChi = item.value;
                                                break;
                                        }
                                    }

                                }
                            }
                            using (var _dungChungSVR = new DungChungServiceClient())
                            {
                                if (congDanTG != null && congDanTG.HoVaTen != null && congDanTG.HoVaTen != "")
                                {
                                    congDanTG.UserID = _nguoiDungID;
                                    var tempList = _dungChungSVR.TT_CongDan_InsUpd(congDanTG);
                                    if (tempList.Data != null && tempList.Data.resultObject > 0)
                                    {
                                        DungChung.ghinhatkynguoidung("Thêm mới thông tin công dân",
                                                         "QTG_QuyenTacGiaController",
                                                         "CapMoi", "Create");
                                        tacGia.TacGiaID = tempList.Data.resultObject;
                                        tacGia.UserID = _nguoiDungID;
                                        if (result.ListTacGia == null)
                                            result.ListTacGia = new List<QTG_TacGiaAdd>();
                                        result.ListTacGia.Add(tacGia);
                                    }
                                }
                                if (congDanCSH != null && congDanCSH.HoVaTen != null && congDanCSH.HoVaTen != "")
                                {
                                    congDanCSH.UserID = _nguoiDungID;
                                    var tempList = _dungChungSVR.TT_CongDan_InsUpd(congDanCSH);
                                    if (tempList.Data != null && tempList.Data.resultObject > 0)
                                    {
                                        DungChung.ghinhatkynguoidung("Thêm mới thông tin công dân",
                                                         "QTG_QuyenTacGiaController",
                                                         "CapMoi", "Create");
                                        chuSoHuu.ChuSoHuuID = tempList.Data.resultObject;
                                        chuSoHuu.UserID = _nguoiDungID;
                                        if (result.ListChuSoHuu == null)
                                            result.ListChuSoHuu = new List<QTG_ChuSoHuuAdd>();
                                        result.ListChuSoHuu.Add(chuSoHuu);
                                    }
                                }
                            }
                        }
                    }
                    using (var _dungChungSVR = new DungChung_1CuaServiceClient())
                    {
                        var tempList = _dungChungSVR.GetChiTietHoSo(long.Parse(hoSoId), _userPortalID);
                        if (tempList != null && tempList.Data != null && tempList.Data.DSHoSoKemTheo != null && tempList.Data.DSHoSoKemTheo.Any())
                        {
                            if (result.ListDinhKem == null)
                                result.ListDinhKem = new List<QTG_DinhKemAdd>();
                            foreach (var item in tempList.Data.DSHoSoKemTheo)
                            {
                                var dinhKem = new QTG_DinhKemAdd();
                                dinhKem.DinhKemID = item.HoSoKemTheoID;
                                dinhKem.Ten = item.TenHoSoKemTheo;
                                dinhKem.CreatedUser = item.NguoiDinhKem;
                                dinhKem.TenFile = item.OriginalName;
                                dinhKem.GhiChu = item.GhiChu;
                                dinhKem.DuongDan = item.UploadName;
                                dinhKem.IsMotCua = true;
                                result.ListDinhKem.Add(dinhKem);
                            }
                        }
                    }
                }
                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/CapMoi error:" + ex.Message, ex, Request);
                return View(result);
            }
        }

        [HttpPost]
        public ActionResult CapSo(long id, int loaiNghiepVuID)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            bool status = false; string stt = ""; string soGCN = "";
            try
            {
                if (id > 0 && loaiNghiepVuID > 0)
                {
                    QTG_QuyenTacGia_CapSo model = new QTG_QuyenTacGia_CapSo();
                    model.ID = id;
                    model.LoaiNghiepVuID = loaiNghiepVuID;
                    model.UserID = _nguoiDungID;
                    //Gen so
                    using (var _capSoSVR = new HS_CapSoServiceClient())
                    {
                        var tempList = _capSoSVR.HS_CapSo_GenSo(1);
                        if (tempList.Data != null && tempList.Data.resultObject != null)
                        {
                            model.STT = tempList.Data.resultObject.STT;
                            model.SoGCN = tempList.Data.resultObject.SoGCN;
                        }
                    }
                    //Update so vao quyen
                    using (var _quyenTacGiaSVR = new QuyenTacGiaServiceClient())
                    {
                        var tempList = _quyenTacGiaSVR.QTG_QuyenTacGia_CapSo(model);
                        if (tempList.Data != null && tempList.Data.resultObject > 0)
                        {
                            DungChung.ghinhatkynguoidung("Cấp số quyền tác giả",
                                             "QTG_QuyenTacGiaController",
                                             "CapSo", "Update");
                            status = true; id = tempList.Data.resultObject; stt = model.STT; soGCN = model.SoGCN;
                        }
                    }
                }
                return Json(new { status = status, id = id, stt = stt, soGCN = soGCN });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/CapSo error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }

        [HttpPost]
        [CustomAuthorize(RightName = CookieRight.QTG_QuyenTacGia_InsUpd)]
        [CheckForAntiForgeryToken]
        public ActionResult SaveQuyenTacGia()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            bool status = false; long id = 0; bool checkSoGCN = false; bool checkSoTT = false;
            try
            {
                var model = ColectionItem<QTG_QuyenTacGiaAdd>("modelThongTin");
                if (model != null)
                {
                    model.UserID = _nguoiDungID;
                    using (var _dungChungSVR = new DungChung_1CuaServiceClient())
                    {
                        var tempList = _dungChungSVR.GetChiTietHoSo(model.HoSoID, _userPortalID);
                        if (tempList != null && tempList.Data != null)
                        {
                            model.ThuTucID = tempList.Data.ThuTucHanhChinhID;
                            model.TenThuTuc = tempList.Data.TenThuTucHanhChinh;
                            model.MaBoHoSo = tempList.Data.MaTraCuu;
                            model.SoBienNhan = tempList.Data.SoBienNhan;
                        }
                    }
                    using (var _quyenTacGiaSVR = new QuyenTacGiaServiceClient())
                    {
                        var ckSoTT = _quyenTacGiaSVR.QTG_QuyenTacGia_CheckSoTT(model.QuyenTacGiaID, model.STT);
                        if (ckSoTT != null && ckSoTT.Data != null && ckSoTT.Data.resultObject > 0)
                        {
                            checkSoTT = true;
                            var ckSoGCN = _quyenTacGiaSVR.QTG_QuyenTacGia_CheckSoGCN(model.QuyenTacGiaID, model.SoGCN);
                            if (ckSoGCN != null && ckSoGCN.Data != null && ckSoGCN.Data.resultObject > 0)
                            {
                                checkSoGCN = true;
                                var tempList = _quyenTacGiaSVR.QTG_QuyenTacGia_InsUpd(model);
                                if (tempList.Data != null && tempList.Data.resultObject > 0)
                                {
                                    DungChung.ghinhatkynguoidung("Thêm mới quyền tác giả",
                                                     "QTG_QuyenTacGiaController",
                                                     "SaveQuyenTacGia", "Create");
                                    status = true; id = tempList.Data.resultObject;
                                }
                            }
                        }
                    }
                }
                return Json(new { checkSoTT = checkSoTT, checkSoGCN = checkSoGCN, status = status, data = id });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/SaveTacGia error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }

        [HttpPost]
        public ActionResult DeleteQuyenTacGia(long id)
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
                    using (var _quyenTacGiaSVR = new QuyenTacGiaServiceClient())
                    {
                        var tempList = _quyenTacGiaSVR.QTG_QuyenTacGia_Del(id, _nguoiDungID);
                        if (tempList.Data != null && tempList.Data.resultObject > 0)
                        {
                            DungChung.ghinhatkynguoidung("Xóa quyền tác giả",
                                         "QTG_QuyenTacGiaController",
                                         "DeleteQuyenTacGia", "Delete");
                            status = true;
                        }
                    }
                }
                return Json(new { status = status });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/DeleteQuyenTacGia error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }

        [HttpPost]
        public ActionResult Duyet(long id, int loaiNghiepVuID, bool isDuyet)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            bool status = false;
            try
            {
                if (id > 0 && loaiNghiepVuID > 0)
                {
                    using (var _quyenTacGiaSVR = new QuyenTacGiaServiceClient())
                    {
                        var tempList = _quyenTacGiaSVR.QTG_QuyenTacGia_Duyet(id, loaiNghiepVuID, isDuyet);
                        if (tempList.Data != null && tempList.Data.resultObject > 0)
                        {
                            DungChung.ghinhatkynguoidung("Duyệt quyền tác giả",
                                             "QTG_QuyenTacGiaController",
                                             "Duyet", "Update");
                            status = true;
                        }
                    }
                }
                return Json(new { status = status });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/Duyet error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }
        #endregion QuyenTacGia

        #region TacGia
        [HttpPost]
        public ActionResult SearchTacGia()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var lstTacGia = new List<TT_TacGiaMap>();
            try
            {
                var model = ColectionItem<TT_TacGiaParam>("modelSearch");
                //model.PageSize = this._pageSize;
                using (var _dungChungSVR = new DungChungServiceClient())
                {
                    var tempList = _dungChungSVR.TT_TacGia_List(model);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Any())
                    {
                        DungChung.ghinhatkynguoidung("Tìm kiếm tác giả",
                                                  "QTG_QuyenTacGiaController",
                                                  "SearchTacGia", "View");
                        lstTacGia = tempList.Data.resultObject;
                    }
                }
                return PartialView("DialogSearchTacGia/List", lstTacGia);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/SearchTacGia error:" + ex.Message, ex, Request);
                return PartialView("DialogSearchTacGia/List", lstTacGia);
            }
        }

        [HttpPost]
        [CustomAuthorize(RightName = CookieRight.TT_TacGia_InsUpd)]
        [CheckForAntiForgeryToken]
        public ActionResult SaveTacGia()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            bool status = false, checkCCCD = false;
            try
            {
                var model = ColectionItem<TT_TacGiaAdd>("modelThongTin");
                if (model != null)
                {
                    model.UserID = _nguoiDungID;
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var resultCheckCCCD = _dungChungSVR.TT_TacGia_CheckCCCD(model.SoCMND, model.TacGiaID);
                        if (resultCheckCCCD.Data != null && resultCheckCCCD.Data.resultObject > 0)
                        {
                            var tempList = _dungChungSVR.TT_TacGia_InsUpd(model);
                            if (tempList.Data != null && tempList.Data.resultObject > 0)
                            {
                                DungChung.ghinhatkynguoidung("Thêm mới tác giả",
                                                 "QTG_QuyenTacGiaController",
                                                 "SaveTacGia", "Create");
                                status = true;
                            }
                        }
                        else
                            checkCCCD = true;
                    }
                }
                return Json(new { status = status, checkCCCD = checkCCCD });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/SaveTacGia error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }

        [HttpPost]
        public ActionResult UpdateTacGia(long id)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var resultModel = new TT_TacGiaAdd();
            try
            {
                if (id > 0)
                {
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var tempData = _dungChungSVR.TT_TacGia_ById(id);
                        if (tempData.Data != null && tempData.Data.resultObject != null)
                        {
                            DungChung.ghinhatkynguoidung("Chỉnh sửa tác giả",
                                                      "QTG_QuyenTacGiaController",
                                                      "UpdateTacGia", "View");
                            resultModel = tempData.Data.resultObject;
                        }
                    }
                }
                return PartialView("DialogSearchTacGia/Update", resultModel);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/UpdateTacGia error:" + ex.Message, ex, Request);
                return PartialView("DialogSearchTacGia/Update", resultModel);
            }
        }
        #endregion TacGia

        #region ChuSoHuu
        [HttpPost]
        public ActionResult SearchChuSoHuu()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var lstChuSoHuu = new List<TT_ChuSoHuuMap>();
            try
            {
                var model = ColectionItem<TT_ChuSoHuuParam>("modelSearch");
                //model.PageSize = this._pageSize;
                using (var _dungChungSVR = new DungChungServiceClient())
                {
                    var tempList = _dungChungSVR.TT_ChuSoHuu_List(model);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Any())
                    {
                        DungChung.ghinhatkynguoidung("Tìm kiếm chủ sở hữu",
                                                  "QTG_QuyenTacGiaController",
                                                  "SearchChuSoHuu", "View");
                        lstChuSoHuu = tempList.Data.resultObject;
                    }
                }
                return PartialView("DialogSearchChuSoHuu/List", lstChuSoHuu);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/SearchChuSoHuu error:" + ex.Message, ex, Request);
                return PartialView("DialogSearchChuSoHuu/List", lstChuSoHuu);
            }
        }

        [HttpPost]
        [CustomAuthorize(RightName = CookieRight.TT_ChuSoHuu_InsUpd)]
        [CheckForAntiForgeryToken]
        public ActionResult SaveChuSoHuu()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            bool status = false, checkCCCD = false;
            try
            {
                var model = ColectionItem<TT_ChuSoHuuAdd>("modelThongTin");
                if (model != null)
                {
                    model.UserID = _nguoiDungID;
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var resultCheckCCCD = _dungChungSVR.TT_ChuSoHuu_CheckCCCD(model.SoCMND, model.ChuSoHuuID);
                        if (resultCheckCCCD.Data != null && resultCheckCCCD.Data.resultObject > 0)
                        {
                            var tempList = _dungChungSVR.TT_ChuSoHuu_InsUpd(model);
                            if (tempList.Data != null && tempList.Data.resultObject > 0)
                            {
                                DungChung.ghinhatkynguoidung("Thêm mới chủ sở hữu",
                                                 "QTG_QuyenTacGiaController",
                                                 "SaveChuSoHuu", "Create");
                                status = true;
                            }
                        }
                        else
                            checkCCCD = true;
                    }
                }
                return Json(new { status = status, checkCCCD = checkCCCD });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/SaveChuSoHuu error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }

        [HttpPost]
        public ActionResult UpdateChuSoHuu(long id)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var resultModel = new TT_ChuSoHuuAdd();
            try
            {
                if (id > 0)
                {
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var tempData = _dungChungSVR.TT_ChuSoHuu_ById(id);
                        if (tempData.Data != null && tempData.Data.resultObject != null)
                        {
                            DungChung.ghinhatkynguoidung("Chỉnh sửa chủ sở hữu",
                                                      "QTG_QuyenTacGiaController",
                                                      "UpdateChuSoHuu", "View");
                            resultModel = tempData.Data.resultObject;
                        }
                    }
                }
                return PartialView("DialogSearchChuSoHuu/Update", resultModel);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/UpdateChuSoHuu error:" + ex.Message, ex, Request);
                return PartialView("DialogSearchChuSoHuu/Update", resultModel);
            }
        }
        #endregion ChuSoHuu

        #region CongDan
        [HttpPost]
        public ActionResult SearchCongDan()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var lstCongDan = new List<TT_CongDanMap>();
            try
            {
                var model = ColectionItem<TT_CongDanParam>("modelSearch");
                //model.PageSize = this._pageSize;
                using (var _dungChungSVR = new DungChungServiceClient())
                {
                    var tempList = _dungChungSVR.TT_CongDan_List(model);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Any())
                    {
                        DungChung.ghinhatkynguoidung("Tìm kiếm thông tin công dân",
                                                  "QTG_QuyenTacGiaController",
                                                  "SearchCongDan", "View");
                        lstCongDan = tempList.Data.resultObject;
                    }
                }
                return PartialView("DialogSearchCongDan/List", lstCongDan);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/SearchCongDan error:" + ex.Message, ex, Request);
                return PartialView("DialogSearchCongDan/List", lstCongDan);
            }
        }

        [HttpPost]
        [CustomAuthorize(RightName = CookieRight.TT_CongDan_InsUpd)]
        [CheckForAntiForgeryToken]
        public ActionResult SaveCongDan()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            bool status = false, checkCCCD = false, checkDKKD = false;
            try
            {
                var model = ColectionItem<TT_CongDanAdd>("modelThongTin");
                if (model != null)
                {
                    model.UserID = _nguoiDungID;
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var resultCheckCCCD = _dungChungSVR.TT_CongDan_CheckCCCD(model.SoCMND, model.CongDanID);
                        if (resultCheckCCCD.Data != null && resultCheckCCCD.Data.resultObject < 0)
                            checkCCCD = true;
                        var resultCheckDKKD = _dungChungSVR.TT_CongDan_CheckDKKD(model.SoDKKD, model.CongDanID);
                        if (resultCheckDKKD.Data != null && resultCheckDKKD.Data.resultObject < 0)
                            checkDKKD = true;
                        if (checkCCCD == false && checkDKKD == false)
                        {
                            var tempList = _dungChungSVR.TT_CongDan_InsUpd(model);
                            if (tempList.Data != null && tempList.Data.resultObject > 0)
                            {
                                DungChung.ghinhatkynguoidung("Thêm mới thông tin công dân",
                                                 "QTG_QuyenTacGiaController",
                                                 "SaveCongDan", "Create");
                                status = true;
                            }
                        }
                    }
                }
                return Json(new { status = status, checkCCCD = checkCCCD, checkDKKD = checkDKKD });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/SaveCongDan error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }

        [HttpPost]
        public ActionResult UpdateCongDan(long id)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var resultModel = new TT_CongDanAdd();
            try
            {
                if (id > 0)
                {
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var tempData = _dungChungSVR.TT_CongDan_ById(id);
                        if (tempData.Data != null && tempData.Data.resultObject != null)
                        {
                            DungChung.ghinhatkynguoidung("Chỉnh sửa thông tin công dân",
                                                      "QTG_QuyenTacGiaController",
                                                      "UpdateCongDan", "View");
                            resultModel = tempData.Data.resultObject;
                        }
                    }
                }
                return PartialView("DialogSearchCongDan/Update", resultModel);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/UpdateCongDan error:" + ex.Message, ex, Request);
                return PartialView("DialogSearchCongDan/Update", resultModel);
            }
        }
        #endregion CongDan

        #region HinhAnh
        [HttpPost]
        public ActionResult SearchHinhAnh()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var lstHinhAnh = new List<TT_HinhAnhMap>();
            try
            {
                var model = ColectionItem<TT_HinhAnhParam>("modelSearch");
                //model.PageSize = this._pageSize;
                using (var _dungChungSVR = new DungChungServiceClient())
                {
                    var tempList = _dungChungSVR.TT_HinhAnh_List(model);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Any())
                    {
                        DungChung.ghinhatkynguoidung("Tìm kiếm hình ảnh",
                                                  "QTG_QuyenTacGiaController",
                                                  "SearchHinhAnh", "View");
                        lstHinhAnh = tempList.Data.resultObject;
                    }
                }
                return PartialView(model.KeyPage == "search" ? "DialogSearchHinhAnh/List" : "DialogSearchHinhAnh/Update", lstHinhAnh);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/SearchHinhAnh error:" + ex.Message, ex, Request);
                return PartialView("DialogSearchHinhAnh/List", lstHinhAnh);
            }
        }

        [HttpPost]
        [CustomAuthorize(RightName = CookieRight.TT_HinhAnh_InsUpd)]
        [CheckForAntiForgeryToken]
        public ActionResult SaveHinhAnh()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            bool status = false;
            try
            {
                var model = ColectionItem<TT_HinhAnhAdd>("modelThongTin");
                if (model != null)
                {
                    model.UserID = _nguoiDungID;
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var tempList = _dungChungSVR.TT_HinhAnh_InsUpd(model);
                        if (tempList.Data != null && tempList.Data.resultObject > 0)
                        {
                            DungChung.ghinhatkynguoidung("Thêm mới hình ảnh",
                                             "QTG_QuyenTacGiaController",
                                             "SaveHinhAnh", "Create");
                            status = true;
                        }
                    }
                }
                return Json(new { status = status });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/SaveHinhAnh error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }

        [HttpPost]
        public ActionResult UpdateHinhAnh(long id)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var resultModel = new TT_HinhAnhAdd();
            try
            {
                if (id > 0)
                {
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var tempData = _dungChungSVR.TT_HinhAnh_ById(id);
                        if (tempData.Data != null && tempData.Data.resultObject != null)
                        {
                            DungChung.ghinhatkynguoidung("Chỉnh sửa hình ảnh",
                                                      "QTG_QuyenTacGiaController",
                                                      "UpdateHinhAnh", "View");
                            resultModel = tempData.Data.resultObject;
                            return Json(new { status = true, data = tempData.Data.resultObject });
                        }
                    }
                }
                return Json(new { status = false });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/UpdateHinhAnh error:" + ex.Message, ex, Request);
                return Json(new { status = false });
            }
        }

        [HttpPost]
        public ActionResult DeleteHinhAnh(long id)
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
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var tempList = _dungChungSVR.TT_HinhAnh_Del(id, _nguoiDungID);
                        if (tempList.Data != null && tempList.Data.resultObject > 0)
                        {
                            DungChung.ghinhatkynguoidung("Xóa hình ảnh",
                                         "QTG_QuyenTacGiaController",
                                         "Delete", "Delete");
                            status = true;
                        }
                    }
                }
                return Json(new { status = status });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/DeleteHinhAnh error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }
        #endregion HinhAnh

        #region Phim
        [HttpPost]
        public ActionResult SearchPhim()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var lstPhim = new List<TT_PhimMap>();
            try
            {
                var model = ColectionItem<TT_PhimParam>("modelSearch");
                //model.PageSize = this._pageSize;
                using (var _dungChungSVR = new DungChungServiceClient())
                {
                    var tempList = _dungChungSVR.TT_Phim_List(model);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Any())
                    {
                        DungChung.ghinhatkynguoidung("Tìm kiếm phim",
                                                  "QTG_QuyenTacGiaController",
                                                  "SearchPhim", "View");
                        lstPhim = tempList.Data.resultObject;
                    }
                }
                return PartialView(model.KeyPage == "search" ? "DialogSearchPhim/List" : "DialogSearchPhim/Update", lstPhim);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/SearchPhim error:" + ex.Message, ex, Request);
                return PartialView("DialogSearchPhim/List", lstPhim);
            }
        }

        [HttpPost]
        [CustomAuthorize(RightName = CookieRight.TT_Phim_InsUpd)]
        [CheckForAntiForgeryToken]
        public ActionResult SavePhim()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            bool status = false;
            try
            {
                var model = ColectionItem<TT_PhimAdd>("modelThongTin");
                if (model != null)
                {
                    model.UserID = _nguoiDungID;
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var tempList = _dungChungSVR.TT_Phim_InsUpd(model);
                        if (tempList.Data != null && tempList.Data.resultObject > 0)
                        {
                            DungChung.ghinhatkynguoidung("Thêm mới phim",
                                             "QTG_QuyenTacGiaController",
                                             "SavePhim", "Create");
                            status = true;
                        }
                    }
                }
                return Json(new { status = status });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/SavePhim error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }

        [HttpPost]
        public ActionResult UpdatePhim(long id)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var resultModel = new TT_PhimAdd();
            try
            {
                if (id > 0)
                {
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var tempData = _dungChungSVR.TT_Phim_ById(id);
                        if (tempData.Data != null && tempData.Data.resultObject != null)
                        {
                            DungChung.ghinhatkynguoidung("Chỉnh sửa phim",
                                                      "QTG_QuyenTacGiaController",
                                                      "UpdatePhim", "View");
                            resultModel = tempData.Data.resultObject;
                            return Json(new { status = true, data = tempData.Data.resultObject });
                        }
                    }
                }
                return Json(new { status = false });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/UpdatePhim error:" + ex.Message, ex, Request);
                return Json(new { status = false });
            }
        }

        [HttpPost]
        public ActionResult DeletePhim(long id)
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
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var tempList = _dungChungSVR.TT_Phim_Del(id, _nguoiDungID);
                        if (tempList.Data != null && tempList.Data.resultObject > 0)
                        {
                            DungChung.ghinhatkynguoidung("Xóa phim",
                                         "QTG_QuyenTacGiaController",
                                         "Delete", "Delete");
                            status = true;
                        }
                    }
                }
                return Json(new { status = status });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/DeletePhim error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }
        #endregion Phim

        #region DinhKem
        [HttpPost]
        public ActionResult SearchDinhKem()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var lstDinhKem = new List<TT_DinhKemMap>();
            try
            {
                var model = ColectionItem<TT_DinhKemParam>("modelSearch");
                //model.PageSize = this._pageSize;
                using (var _dungChungSVR = new DungChungServiceClient())
                {
                    var tempList = _dungChungSVR.TT_DinhKem_List(model);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Any())
                    {
                        DungChung.ghinhatkynguoidung("Tìm kiếm hình ảnh",
                                                  "QTG_QuyenTacGiaController",
                                                  "SearchDinhKem", "View");
                        lstDinhKem = tempList.Data.resultObject;
                    }
                }
                return PartialView("DialogSearchDinhKem/List", lstDinhKem);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/SearchDinhKem error:" + ex.Message, ex, Request);
                return PartialView("DialogSearchDinhKem/List", lstDinhKem);
            }
        }

        [HttpPost]
        [CustomAuthorize(RightName = CookieRight.TT_DinhKem_InsUpd)]
        [CheckForAntiForgeryToken]
        public ActionResult SaveDinhKem()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            bool status = false;
            try
            {
                var model = ColectionItem<TT_DinhKemAdd>("modelThongTin");
                if (model != null)
                {
                    model.UserID = _nguoiDungID;
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var tempList = _dungChungSVR.TT_DinhKem_InsUpd(model);
                        if (tempList.Data != null && tempList.Data.resultObject > 0)
                        {
                            DungChung.ghinhatkynguoidung("Thêm mới hình ảnh",
                                             "QTG_QuyenTacGiaController",
                                             "SaveDinhKem", "Create");
                            status = true;
                        }
                    }
                }
                return Json(new { status = status });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/SaveDinhKem error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }

        [HttpPost]
        public ActionResult UpdateDinhKem(long id)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var resultModel = new TT_DinhKemAdd();
            try
            {
                if (id > 0)
                {
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var tempData = _dungChungSVR.TT_DinhKem_ById(id);
                        if (tempData.Data != null && tempData.Data.resultObject != null)
                        {
                            DungChung.ghinhatkynguoidung("Chỉnh sửa hình ảnh",
                                                      "QTG_QuyenTacGiaController",
                                                      "UpdateDinhKem", "View");
                            resultModel = tempData.Data.resultObject;
                            return Json(new { status = true, data = tempData.Data.resultObject });
                        }
                    }
                }
                return Json(new { status = false });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/UpdateDinhKem error:" + ex.Message, ex, Request);
                return Json(new { status = false });
            }
        }

        [HttpPost]
        public ActionResult DeleteDinhKem(long id)
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
                    using (var _dungChungSVR = new DungChungServiceClient())
                    {
                        var tempList = _dungChungSVR.TT_DinhKem_Del(id, _nguoiDungID);
                        if (tempList.Data != null && tempList.Data.resultObject > 0)
                        {
                            DungChung.ghinhatkynguoidung("Xóa hình ảnh",
                                         "QTG_QuyenTacGiaController",
                                         "Delete", "Delete");
                            status = true;
                        }
                    }
                }
                return Json(new { status = status });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/DeleteDinhKem error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }
        #endregion DinhKem

        #region Upload
        [HttpPost]
        public ActionResult UploadFile()
        {
            bool status = false;
            string FileName = "";
            string FileNameGoc = "";
            string FileUrl = "";
            try
            {
                if (Request.Files.Count > 0)
                {
                    var root = Path.Combine(Server.MapPath('~' + ""));
                    var fileUpload = Request.Files[0];
                    FileNameGoc = fileUpload.FileName;
                    var fileInfo = FileControl.SaveFileUpload(root, ParameterSetting.PathFileUpload + "\\DinhKem", fileUpload);
                    var urlupload = "\\" + fileInfo.Url;
                    FileUrl = urlupload;
                    FileName = fileInfo.TenUpload;
                    status = true;
                }
                return Json(new { status = status, FileName = FileName, FileUrl = FileUrl, FileNameGoc = FileNameGoc });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/UploadFile error:" + ex.Message, ex, Request);
                return Json(new { status = false });
            }
        }

        [HttpPost]
        public ActionResult UploadPicture()
        {
            bool status = false;
            string FileName = "";
            string FileNameGoc = "";
            string FileUrl = "";
            try
            {
                if (Request.Files.Count > 0)
                {
                    var root = Path.Combine(Server.MapPath('~' + ""));
                    var fileUpload = Request.Files[0];
                    FileNameGoc = fileUpload.FileName;
                    var fileInfo = FileControl.SaveFileUpload(root, ParameterSetting.PathFileUpload + "\\HinhAnh", fileUpload);
                    var urlupload = "\\" + fileInfo.Url;
                    FileUrl = urlupload;
                    FileName = fileInfo.TenUpload;
                    status = true;
                }
                return Json(new { status = status, FileName = FileName, FileUrl = FileUrl, FileNameGoc = FileNameGoc });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/UploadPicture error:" + ex.Message, ex, Request);
                return Json(new { status = false });
            }
        }

        [HttpPost]
        public ActionResult UploadVideo()
        {
            bool status = false;
            string FileName = "";
            string FileNameGoc = "";
            string FileUrl = "";
            try
            {
                if (Request.Files.Count > 0)
                {
                    var root = Path.Combine(Server.MapPath('~' + ""));
                    var fileUpload = Request.Files[0];
                    FileNameGoc = fileUpload.FileName;
                    var fileInfo = FileControl.SaveFileUpload(root, ParameterSetting.PathFileUpload + "\\Phim", fileUpload);
                    var urlupload = "\\" + fileInfo.Url;
                    FileUrl = urlupload;
                    FileName = fileInfo.TenUpload;
                    status = true;
                }
                return Json(new { status = status, FileName = FileName, FileUrl = FileUrl, FileNameGoc = FileNameGoc });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/UploadPicture error:" + ex.Message, ex, Request);
                return Json(new { status = false });
            }
        }
        #endregion Upload

        #region ChiTiet
        [CustomAuthorize]
        [CustomAuthorize(RightName = CookieRight.QTG_QuyenTacGia_ChiTiet)]
        public ActionResult ChiTiet(string id)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var result = new QTG_QuyenTacGiaAdd();
            try
            {
                ViewBag.TieuDe = "Thông tin quyền tác giả";
                if (id != "" && id != null)
                {
                    using (var _quyenTacGiaSVR = new QuyenTacGiaServiceClient())
                    {
                        var resultTacGia = _quyenTacGiaSVR.QTG_QuyenTacGia_ById(long.Parse(id));
                        if (resultTacGia != null && resultTacGia.Data != null && resultTacGia.Data.resultObject != null)
                        {
                            result = resultTacGia.Data.resultObject;
                        }
                    }
                }
                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/ChiTiet error:" + ex.Message, ex, Request);
                return View(result);
            }
        }
        #endregion ChiTiet

        #region CapLai
        [CustomAuthorize]
        [CustomAuthorize(RightName = CookieRight.QTG_QuyenTacGia_CapLai)]
        public ActionResult CapLai(string id, string hoSoId, bool? isMap)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var result = new QTG_CapLaiViewModel();
            try
            {
                ViewBag.TieuDe = "Cấp lại quyền tác giả";
                if (id != "" && id != null)
                {
                    using (var _quyenTacGiaSVR = new QuyenTacGiaServiceClient())
                    {
                        if (hoSoId != "" && hoSoId != null && isMap == true)
                        {
                            var resultCapLai = _quyenTacGiaSVR.QTG_CapLai_ById(long.Parse(id));
                            if (resultCapLai != null && resultCapLai.Data != null && resultCapLai.Data.resultObject != null)
                            {
                                result.CapLai = resultCapLai.Data.resultObject;
                                var resultQuyen = _quyenTacGiaSVR.QTG_QuyenTacGia_ById(resultCapLai.Data.resultObject.QuyenTacGiaID);
                                if (resultQuyen != null && resultQuyen.Data != null && resultQuyen.Data.resultObject != null)
                                {
                                    result.QuyenTacGia = resultQuyen.Data.resultObject;
                                    result.QuyenTacGia.HoSoID = long.Parse(hoSoId);
                                }
                                if (resultCapLai.Data.resultObject.CapLaiID > 0)
                                {
                                    result.QuyenTacGia.STT = resultCapLai.Data.resultObject.STTCu;
                                    result.QuyenTacGia.SoGCN = resultCapLai.Data.resultObject.SoGCNCu;
                                    result.QuyenTacGia.NgayCap = resultCapLai.Data.resultObject.NgayCapGCNCu;
                                }
                            }
                        }
                        else if (hoSoId == "" || hoSoId == null || isMap == false)
                        {
                            var resultQuyen = _quyenTacGiaSVR.QTG_QuyenTacGia_ById(long.Parse(id));
                            if (resultQuyen != null && resultQuyen.Data != null && resultQuyen.Data.resultObject != null)
                            {
                                result.QuyenTacGia = resultQuyen.Data.resultObject;
                                if (hoSoId != "" && hoSoId != null)
                                    result.QuyenTacGia.HoSoID = long.Parse(hoSoId);
                            }
                            var resultCapLai = _quyenTacGiaSVR.QTG_CapLai_ByQuyenTGId(long.Parse(id));
                            if (resultCapLai != null && resultCapLai.Data != null && resultCapLai.Data.resultObject != null)
                            {
                                result.CapLai = resultCapLai.Data.resultObject;
                                if (resultCapLai.Data.resultObject.CapLaiID > 0)
                                {
                                    result.QuyenTacGia.STT = resultCapLai.Data.resultObject.STTCu;
                                    result.QuyenTacGia.SoGCN = resultCapLai.Data.resultObject.SoGCNCu;
                                    result.QuyenTacGia.NgayCap = resultCapLai.Data.resultObject.NgayCapGCNCu;
                                }
                            }
                        }
                    }
                }
                else
                {
                    result.QuyenTacGia = new QTG_QuyenTacGiaAdd();
                    result.QuyenTacGia.ListDinhKem = new List<QTG_DinhKemAdd>();
                    if (hoSoId != "" && hoSoId != null)
                        result.QuyenTacGia.HoSoID = long.Parse(hoSoId);
                    result.CapLai = new QTG_CapLaiAdd();
                }
                if (result.CapLai.CapLaiID == 0)
                    result.CapLai.NgayCapGCN = DateTime.Today.ToString("dd/MM/yyyy");
                if (hoSoId != "" && hoSoId != null)
                {
                    using (var _dungChungSVR = new DungChung_1CuaServiceClient())
                    {
                        var tempList = _dungChungSVR.GetChiTietHoSo(long.Parse(hoSoId), _userPortalID);
                        if (tempList != null && tempList.Data != null && tempList.Data.DSHoSoKemTheo != null && tempList.Data.DSHoSoKemTheo.Any())
                        {
                            if (result.QuyenTacGia.ListDinhKem == null)
                                result.QuyenTacGia.ListDinhKem = new List<QTG_DinhKemAdd>();
                            foreach (var item in tempList.Data.DSHoSoKemTheo)
                            {
                                var dinhKem = new QTG_DinhKemAdd();
                                dinhKem.DinhKemID = item.HoSoKemTheoID;
                                dinhKem.Ten = item.TenHoSoKemTheo;
                                dinhKem.CreatedUser = item.NguoiDinhKem;
                                dinhKem.TenFile = item.OriginalName;
                                dinhKem.GhiChu = item.GhiChu;
                                dinhKem.DuongDan = item.UploadName;
                                dinhKem.IsMotCua = true;
                                result.QuyenTacGia.ListDinhKem.Add(dinhKem);
                            }
                        }
                    }
                }
                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/CapLai error:" + ex.Message, ex, Request);
                return View(result);
            }
        }

        [HttpPost]
        [CustomAuthorize(RightName = CookieRight.QTG_QuyenTacGia_CapLai)]
        [CheckForAntiForgeryToken]
        public ActionResult SaveCapLai()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            bool status = false; long id = 0;
            try
            {
                var model = ColectionItem<QTG_CapLaiAdd>("modelThongTin");
                if (model != null)
                {
                    model.UserID = _nguoiDungID;
                    using (var _dungChungSVR = new DungChung_1CuaServiceClient())
                    {
                        var tempList = _dungChungSVR.GetChiTietHoSo(model.HoSoID, _userPortalID);
                        if (tempList != null && tempList.Data != null)
                        {
                            model.ThuTucID = tempList.Data.ThuTucHanhChinhID;
                            model.TenThuTuc = tempList.Data.TenThuTucHanhChinh;
                            model.MaBoHoSo = tempList.Data.MaTraCuu;
                            model.SoBienNhan = tempList.Data.SoBienNhan;
                        }
                    }
                    using (var _quyenTacGiaSVR = new QuyenTacGiaServiceClient())
                    {
                        var tempList = _quyenTacGiaSVR.QTG_CapLai_InsUpd(model);
                        if (tempList.Data != null && tempList.Data.resultObject > 0)
                        {
                            DungChung.ghinhatkynguoidung("Thêm mới cấp lại",
                                             "QTG_QuyenTacGiaController",
                                             "SaveCapLai", "Create");
                            status = true; id = tempList.Data.resultObject;
                        }
                    }
                }
                return Json(new { status = status, data = id });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/SaveCapLai error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }
        #endregion CapLai

        #region ThuHoi
        [CustomAuthorize]
        [CustomAuthorize(RightName = CookieRight.QTG_QuyenTacGia_ThuHoi)]
        public ActionResult ThuHoi(string id, string hoSoId, bool? isMap)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var result = new QTG_ThuHoiViewModel();
            try
            {
                ViewBag.TieuDe = "Thu hồi quyền tác giả";
                if (id != "" && id != null)
                {
                    using (var _quyenTacGiaSVR = new QuyenTacGiaServiceClient())
                    {
                        if (hoSoId != "" && hoSoId != null && isMap == true)
                        {
                            var resultThuHoi = _quyenTacGiaSVR.QTG_ThuHoi_ById(long.Parse(id));
                            if (resultThuHoi != null && resultThuHoi.Data != null && resultThuHoi.Data.resultObject != null)
                            {
                                result.ThuHoi = resultThuHoi.Data.resultObject;
                                var resultQuyen = _quyenTacGiaSVR.QTG_QuyenTacGia_ById(resultThuHoi.Data.resultObject.QuyenTacGiaID);
                                if (resultQuyen != null && resultQuyen.Data != null && resultQuyen.Data.resultObject != null)
                                {
                                    result.QuyenTacGia = resultQuyen.Data.resultObject;
                                    result.QuyenTacGia.HoSoID = long.Parse(hoSoId);
                                }
                            }
                        }
                        else if (hoSoId == "" || hoSoId == null || isMap == false)
                        {
                            var resultQuyen = _quyenTacGiaSVR.QTG_QuyenTacGia_ById(long.Parse(id));
                            if (resultQuyen != null && resultQuyen.Data != null && resultQuyen.Data.resultObject != null)
                            {
                                result.QuyenTacGia = resultQuyen.Data.resultObject;
                                if (hoSoId != "" && hoSoId != null)
                                    result.QuyenTacGia.HoSoID = long.Parse(hoSoId);
                            }
                            var resultThuHoi = _quyenTacGiaSVR.QTG_ThuHoi_ByQuyenTGId(long.Parse(id));
                            if (resultThuHoi != null && resultThuHoi.Data != null && resultThuHoi.Data.resultObject != null)
                            {
                                result.ThuHoi = resultThuHoi.Data.resultObject;
                            }
                        }
                    }
                }
                else
                {
                    result.QuyenTacGia = new QTG_QuyenTacGiaAdd();
                    result.QuyenTacGia.ListDinhKem = new List<QTG_DinhKemAdd>();
                    if (hoSoId != "" && hoSoId != null)
                        result.QuyenTacGia.HoSoID = long.Parse(hoSoId);
                    result.ThuHoi = new QTG_ThuHoiAdd();
                }
                if (hoSoId != "" && hoSoId != null)
                {
                    using (var _dungChungSVR = new DungChung_1CuaServiceClient())
                    {
                        var tempList = _dungChungSVR.GetChiTietHoSo(long.Parse(hoSoId), _userPortalID);
                        if (tempList != null && tempList.Data != null && tempList.Data.DSHoSoKemTheo != null && tempList.Data.DSHoSoKemTheo.Any())
                        {
                            if (result.QuyenTacGia.ListDinhKem == null)
                                result.QuyenTacGia.ListDinhKem = new List<QTG_DinhKemAdd>();
                            foreach (var item in tempList.Data.DSHoSoKemTheo)
                            {
                                var dinhKem = new QTG_DinhKemAdd();
                                dinhKem.DinhKemID = item.HoSoKemTheoID;
                                dinhKem.Ten = item.TenHoSoKemTheo;
                                dinhKem.CreatedUser = item.NguoiDinhKem;
                                dinhKem.TenFile = item.OriginalName;
                                dinhKem.GhiChu = item.GhiChu;
                                dinhKem.DuongDan = item.UploadName;
                                dinhKem.IsMotCua = true;
                                result.QuyenTacGia.ListDinhKem.Add(dinhKem);
                            }
                        }
                    }
                }
                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/ThuHoi error:" + ex.Message, ex, Request);
                return View(result);
            }
        }

        [HttpPost]
        [CustomAuthorize(RightName = CookieRight.QTG_QuyenTacGia_ThuHoi)]
        [CheckForAntiForgeryToken]
        public ActionResult SaveThuHoi()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            bool status = false; long id = 0;
            try
            {
                var model = ColectionItem<QTG_ThuHoiAdd>("modelThongTin");
                if (model != null)
                {
                    model.UserID = _nguoiDungID;
                    using (var _dungChungSVR = new DungChung_1CuaServiceClient())
                    {
                        var tempList = _dungChungSVR.GetChiTietHoSo(model.HoSoID, _userPortalID);
                        if (tempList != null && tempList.Data != null)
                        {
                            model.ThuTucID = tempList.Data.ThuTucHanhChinhID;
                            model.TenThuTuc = tempList.Data.TenThuTucHanhChinh;
                            model.MaBoHoSo = tempList.Data.MaTraCuu;
                            model.SoBienNhan = tempList.Data.SoBienNhan;
                        }
                    }
                    using (var _quyenTacGiaSVR = new QuyenTacGiaServiceClient())
                    {
                        var tempList = _quyenTacGiaSVR.QTG_ThuHoi_InsUpd(model);
                        if (tempList.Data != null && tempList.Data.resultObject > 0)
                        {
                            DungChung.ghinhatkynguoidung("Thêm mới cấp lại",
                                             "QTG_QuyenTacGiaController",
                                             "SaveThuHoi", "Create");
                            status = true; id = tempList.Data.resultObject;
                        }
                    }
                }
                return Json(new { status = status, data = id });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/SaveThuHoi error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }
        #endregion ThuHoi

        #region ChuyenChu
        [CustomAuthorize]
        [CustomAuthorize(RightName = CookieRight.QTG_QuyenTacGia_ChuyenChu)]
        public ActionResult ChuyenChu(string id, string hoSoId, bool? isMap)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var result = new QTG_ChuyenChuViewModel();
            try
            {
                ViewBag.TieuDe = "Chuyển chủ sở hữu quyền tác giả";
                if (id != "" && id != null)
                {
                    using (var _quyenTacGiaSVR = new QuyenTacGiaServiceClient())
                    {
                        if (hoSoId != "" && hoSoId != null && isMap == true)
                        {
                            var resultChuyenChu = _quyenTacGiaSVR.QTG_ChuyenChuSoHuu_ById(long.Parse(id));
                            if (resultChuyenChu != null && resultChuyenChu.Data != null && resultChuyenChu.Data.resultObject != null)
                            {
                                result.ChuyenChu = resultChuyenChu.Data.resultObject;
                                var resultQuyen = _quyenTacGiaSVR.QTG_QuyenTacGia_ById(resultChuyenChu.Data.resultObject.QuyenTacGiaID);
                                if (resultQuyen != null && resultQuyen.Data != null && resultQuyen.Data.resultObject != null)
                                {
                                    result.QuyenTacGia = resultQuyen.Data.resultObject;
                                    result.QuyenTacGia.HoSoID = long.Parse(hoSoId);
                                }
                                if (resultChuyenChu.Data.resultObject.ChuyenChuSoHuuID > 0)
                                {
                                    result.QuyenTacGia.STT = resultChuyenChu.Data.resultObject.STTCu;
                                    result.QuyenTacGia.SoGCN = resultChuyenChu.Data.resultObject.SoGCNCu;
                                    result.QuyenTacGia.NgayCap = resultChuyenChu.Data.resultObject.NgayCapGCNCu;
                                }
                            }
                        }
                        else if (hoSoId == "" || hoSoId == null || isMap == false)
                        {
                            var resultQuyen = _quyenTacGiaSVR.QTG_QuyenTacGia_ById(long.Parse(id));
                            if (resultQuyen != null && resultQuyen.Data != null && resultQuyen.Data.resultObject != null)
                            {
                                result.QuyenTacGia = resultQuyen.Data.resultObject;
                                if (hoSoId != "" && hoSoId != null)
                                    result.QuyenTacGia.HoSoID = long.Parse(hoSoId);
                            }
                            var resultChuyenChu = _quyenTacGiaSVR.QTG_ChuyenChuSoHuu_ByQuyenTGId(long.Parse(id));
                            if (resultChuyenChu != null && resultChuyenChu.Data != null && resultChuyenChu.Data.resultObject != null)
                            {
                                result.ChuyenChu = resultChuyenChu.Data.resultObject;
                                if (resultChuyenChu.Data.resultObject.ChuyenChuSoHuuID > 0)
                                {
                                    result.QuyenTacGia.STT = resultChuyenChu.Data.resultObject.STTCu;
                                    result.QuyenTacGia.SoGCN = resultChuyenChu.Data.resultObject.SoGCNCu;
                                    result.QuyenTacGia.NgayCap = resultChuyenChu.Data.resultObject.NgayCapGCNCu;
                                }
                            }
                        }
                    }
                }
                else
                {
                    result.QuyenTacGia = new QTG_QuyenTacGiaAdd();
                    result.QuyenTacGia.ListDinhKem = new List<QTG_DinhKemAdd>();
                    if (hoSoId != "" && hoSoId != null)
                        result.QuyenTacGia.HoSoID = long.Parse(hoSoId);
                    result.ChuyenChu = new QTG_ChuyenChuSoHuuAdd();
                }
                if (result.ChuyenChu.ChuyenChuSoHuuID == 0)
                    result.ChuyenChu.NgayCapGCN = DateTime.Today.ToString("dd/MM/yyyy");
                if (hoSoId != "" && hoSoId != null)
                {
                    using (var _dungChungSVR = new DungChung_1CuaServiceClient())
                    {
                        var tempList = _dungChungSVR.GetChiTietHoSo(long.Parse(hoSoId), _userPortalID);
                        if (tempList != null && tempList.Data != null && tempList.Data.DSHoSoKemTheo != null && tempList.Data.DSHoSoKemTheo.Any())
                        {
                            if (result.QuyenTacGia.ListDinhKem == null)
                                result.QuyenTacGia.ListDinhKem = new List<QTG_DinhKemAdd>();
                            foreach (var item in tempList.Data.DSHoSoKemTheo)
                            {
                                var dinhKem = new QTG_DinhKemAdd();
                                dinhKem.DinhKemID = item.HoSoKemTheoID;
                                dinhKem.Ten = item.TenHoSoKemTheo;
                                dinhKem.CreatedUser = item.NguoiDinhKem;
                                dinhKem.TenFile = item.OriginalName;
                                dinhKem.GhiChu = item.GhiChu;
                                dinhKem.DuongDan = item.UploadName;
                                dinhKem.IsMotCua = true;
                                result.QuyenTacGia.ListDinhKem.Add(dinhKem);
                            }
                        }
                    }
                }
                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/ChuyenChu error:" + ex.Message, ex, Request);
                return View(result);
            }
        }

        [HttpPost]
        [CustomAuthorize(RightName = CookieRight.QTG_QuyenTacGia_ChuyenChu)]
        [CheckForAntiForgeryToken]
        public ActionResult SaveChuyenChu()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            bool status = false; long id = 0;
            try
            {
                var model = ColectionItem<QTG_ChuyenChuSoHuuAdd>("modelThongTin");
                if (model != null)
                {
                    model.UserID = _nguoiDungID;
                    using (var _dungChungSVR = new DungChung_1CuaServiceClient())
                    {
                        var tempList = _dungChungSVR.GetChiTietHoSo(model.HoSoID, _userPortalID);
                        if (tempList != null && tempList.Data != null)
                        {
                            model.ThuTucID = tempList.Data.ThuTucHanhChinhID;
                            model.TenThuTuc = tempList.Data.TenThuTucHanhChinh;
                            model.MaBoHoSo = tempList.Data.MaTraCuu;
                            model.SoBienNhan = tempList.Data.SoBienNhan;
                        }
                    }
                    using (var _quyenTacGiaSVR = new QuyenTacGiaServiceClient())
                    {
                        var tempList = _quyenTacGiaSVR.QTG_ChuyenChuSoHuu_InsUpd(model);
                        if (tempList.Data != null && tempList.Data.resultObject > 0)
                        {
                            DungChung.ghinhatkynguoidung("Thêm mới chuyển chủ sở hữu",
                                             "QTG_QuyenTacGiaController",
                                             "SaveChuyenChu", "Create");
                            status = true; id = tempList.Data.resultObject;
                        }
                    }
                }
                return Json(new { status = status, data = id });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/SaveChuyenChu error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }
        #endregion ChuyenChu

        #region CapDoi
        [CustomAuthorize]
        [CustomAuthorize(RightName = CookieRight.QTG_QuyenTacGia_CapDoi)]
        public ActionResult CapDoi(string id, string hoSoId, bool? isMap)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var result = new QTG_CapDoiViewModel();
            try
            {
                ViewBag.TieuDe = "Cấp đổi quyền tác giả";
                if (id != "" && id != null)
                {
                    using (var _quyenTacGiaSVR = new QuyenTacGiaServiceClient())
                    {
                        if (hoSoId != "" && hoSoId != null && isMap == true)
                        {
                            var resultCapDoi = _quyenTacGiaSVR.QTG_CapDoi_ById(long.Parse(id));
                            if (resultCapDoi != null && resultCapDoi.Data != null && resultCapDoi.Data.resultObject != null)
                            {
                                result.CapDoi = resultCapDoi.Data.resultObject;
                                var resultQuyen = _quyenTacGiaSVR.QTG_QuyenTacGia_ById(resultCapDoi.Data.resultObject.QuyenTacGiaID);
                                if (resultQuyen != null && resultQuyen.Data != null && resultQuyen.Data.resultObject != null)
                                {
                                    result.QuyenTacGia = resultQuyen.Data.resultObject;
                                    result.QuyenTacGia.HoSoID = long.Parse(hoSoId);
                                }
                                if (resultCapDoi.Data.resultObject.CapDoiID > 0)
                                {
                                    result.QuyenTacGia.STT = resultCapDoi.Data.resultObject.STTCu;
                                    result.QuyenTacGia.SoGCN = resultCapDoi.Data.resultObject.SoGCNCu;
                                    result.QuyenTacGia.NgayCap = resultCapDoi.Data.resultObject.NgayCapGCNCu;
                                }
                            }
                        }
                        else if (hoSoId == "" || hoSoId == null || isMap == false)
                        {
                            var resultQuyen = _quyenTacGiaSVR.QTG_QuyenTacGia_ById(long.Parse(id));
                            if (resultQuyen != null && resultQuyen.Data != null && resultQuyen.Data.resultObject != null)
                            {
                                result.QuyenTacGia = resultQuyen.Data.resultObject;
                                if (hoSoId != "" && hoSoId != null)
                                    result.QuyenTacGia.HoSoID = long.Parse(hoSoId);
                            }
                            var resultCapDoi = _quyenTacGiaSVR.QTG_CapDoi_ByQuyenTGId(long.Parse(id));
                            if (resultCapDoi != null && resultCapDoi.Data != null && resultCapDoi.Data.resultObject != null)
                            {
                                result.CapDoi = resultCapDoi.Data.resultObject;
                                if (resultCapDoi.Data.resultObject.CapDoiID > 0)
                                {
                                    result.QuyenTacGia.STT = resultCapDoi.Data.resultObject.STTCu;
                                    result.QuyenTacGia.SoGCN = resultCapDoi.Data.resultObject.SoGCNCu;
                                    result.QuyenTacGia.NgayCap = resultCapDoi.Data.resultObject.NgayCapGCNCu;
                                }
                            }
                        }
                    }
                }
                else
                {
                    result.QuyenTacGia = new QTG_QuyenTacGiaAdd();
                    result.QuyenTacGia.ListDinhKem = new List<QTG_DinhKemAdd>();
                    if (hoSoId != "" && hoSoId != null)
                        result.QuyenTacGia.HoSoID = long.Parse(hoSoId);
                    result.CapDoi = new QTG_CapDoiAdd();
                }
                if (result.CapDoi.CapDoiID == 0)
                    result.CapDoi.NgayCapGCN = DateTime.Today.ToString("dd/MM/yyyy");
                if (hoSoId != "" && hoSoId != null)
                {
                    using (var _dungChungSVR = new DungChung_1CuaServiceClient())
                    {
                        var tempList = _dungChungSVR.GetChiTietHoSo(long.Parse(hoSoId), _userPortalID);
                        if (tempList != null && tempList.Data != null && tempList.Data.DSHoSoKemTheo != null && tempList.Data.DSHoSoKemTheo.Any())
                        {
                            if (result.QuyenTacGia.ListDinhKem == null)
                                result.QuyenTacGia.ListDinhKem = new List<QTG_DinhKemAdd>();
                            foreach (var item in tempList.Data.DSHoSoKemTheo)
                            {
                                var dinhKem = new QTG_DinhKemAdd();
                                dinhKem.DinhKemID = item.HoSoKemTheoID;
                                dinhKem.Ten = item.TenHoSoKemTheo;
                                dinhKem.CreatedUser = item.NguoiDinhKem;
                                dinhKem.TenFile = item.OriginalName;
                                dinhKem.GhiChu = item.GhiChu;
                                dinhKem.DuongDan = item.UploadName;
                                dinhKem.IsMotCua = true;
                                result.QuyenTacGia.ListDinhKem.Add(dinhKem);
                            }
                        }
                    }
                }
                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/CapDoi error:" + ex.Message, ex, Request);
                return View(result);
            }
        }

        [HttpPost]
        [CustomAuthorize(RightName = CookieRight.QTG_QuyenTacGia_CapDoi)]
        [CheckForAntiForgeryToken]
        public ActionResult SaveCapDoi()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            bool status = false; long id = 0;
            try
            {
                var model = ColectionItem<QTG_CapDoiAdd>("modelThongTin");
                if (model != null)
                {
                    model.UserID = _nguoiDungID;
                    using (var _dungChungSVR = new DungChung_1CuaServiceClient())
                    {
                        var tempList = _dungChungSVR.GetChiTietHoSo(model.HoSoID, _userPortalID);
                        if (tempList != null && tempList.Data != null)
                        {
                            model.ThuTucID = tempList.Data.ThuTucHanhChinhID;
                            model.TenThuTuc = tempList.Data.TenThuTucHanhChinh;
                            model.MaBoHoSo = tempList.Data.MaTraCuu;
                            model.SoBienNhan = tempList.Data.SoBienNhan;
                        }
                    }
                    using (var _quyenTacGiaSVR = new QuyenTacGiaServiceClient())
                    {
                        var tempList = _quyenTacGiaSVR.QTG_CapDoi_InsUpd(model);
                        if (tempList.Data != null && tempList.Data.resultObject > 0)
                        {
                            DungChung.ghinhatkynguoidung("Thêm mới cấp đổi",
                                             "QTG_QuyenTacGiaController",
                                             "SaveCapDoi", "Create");
                            status = true; id = tempList.Data.resultObject;
                        }
                    }
                }
                return Json(new { status = status, data = id });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/SaveCapDoi error:" + ex.Message, ex, Request);
                return Json(new { status = status });
            }
        }
        #endregion CapDoi

        #region SearchGiayChungNhan
        [HttpPost]
        public ActionResult SearchGiayChungNhan()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var lstGCN = new List<QTG_GiayChungNhanMap>();
            try
            {
                var model = ColectionItem<QTG_GiayChungNhanParam>("modelSearch");
                //model.PageSize = this._pageSize;
                using (var _quyenTacGiaSVR = new QuyenTacGiaServiceClient())
                {
                    var tempList = _quyenTacGiaSVR.QTG_QuyenTacGia_SearchGCN(model);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Any())
                    {
                        DungChung.ghinhatkynguoidung("Tìm kiếm giấy chứng nhận",
                                                  "QTG_QuyenTacGiaController",
                                                  "SearchGiayChungNhan", "View");
                        lstGCN = tempList.Data.resultObject;
                    }
                }
                return PartialView("DialogSearchGiayChungNhan/List", lstGCN);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/SearchGiayChungNhan error:" + ex.Message, ex, Request);
                return PartialView("DialogSearchGiayChungNhan/List", lstGCN);
            }
        }
        #endregion SearchGiayChungNhan

        #region SearchSoThuTu
        [HttpPost]
        public ActionResult SearchSoThuTu()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var lstSTT = new List<QTG_SoThuTuMap>();
            try
            {
                var model = ColectionItem<QTG_SoThuTuParam>("modelSearch");
                //model.PageSize = this._pageSize;
                using (var _quyenTacGiaSVR = new QuyenTacGiaServiceClient())
                {
                    var tempList = _quyenTacGiaSVR.QTG_QuyenTacGia_SearchSTT(model);
                    if (tempList.Data != null && tempList.Data.resultObject != null && tempList.Data.resultObject.Any())
                    {
                        DungChung.ghinhatkynguoidung("Tìm kiếm số thứ tự",
                                                  "QTG_QuyenTacGiaController",
                                                  "SearchSoThuTu", "View");
                        lstSTT = tempList.Data.resultObject;
                    }
                }
                return PartialView("DialogSearchSoThuTu/List", lstSTT);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/SearchSoThuTu error:" + ex.Message, ex, Request);
                return PartialView("DialogSearchSoThuTu/List", lstSTT);
            }
        }
        #endregion SearchSoThuTu

        protected T ColectionItem<T>(string nameRequest)
        {
            try
            {
                var model = Request.Form[nameRequest];
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                };

                var item = JsonConvert.DeserializeObject<T>(model, settings);
                return item;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public ActionResult ViewPDF(string strUrl, bool isMotCua)
        {
            var result = new QTG_FileViewModel();
            if (isMotCua == true)
            {
                var urlFile = AppSetting.DC_1Cua + WebConfigurationManager.AppSettings["1Cua_DownloadFile"] + "?fileid=" + strUrl;
                if (strUrl.Contains(".pdf") || strUrl.Contains(".PDF"))
                {
                    var root = Path.Combine(Server.MapPath('~' + ""));
                    string pathFile = Path.Combine(root, ParameterSetting.PathFileUpload + "\\FileMotCua\\");
                    DirectoryInfo dir = new DirectoryInfo(pathFile);
                    if (!dir.Exists)
                    {
                        Directory.CreateDirectory(pathFile);
                    }
                    using (var client = new WebClient())
                    {
                        if (!System.IO.File.Exists(pathFile + strUrl))
                            client.DownloadFile(urlFile, pathFile + strUrl);
                    }
                    result.Url = "/" + ParameterSetting.PathFileUpload + "/FileMotCua/" + strUrl;
                    result.IsMotCua = isMotCua;
                }
                else
                {
                    result.Url = urlFile;
                    result.IsMotCua = isMotCua;
                }
            }
            else
            {
                result.Url = strUrl.Replace("\\", "/");
                result.IsMotCua = isMotCua;
            }
            return View(result);
        }
    }
}