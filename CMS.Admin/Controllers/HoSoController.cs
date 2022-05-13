using Business.Entities.Domain;
using CMS.Admin.Common;
using CMS.Admin.Constaints;
using CMS.Admin.Models.HoSo;
using Module.Framework;
using Module.Framework.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace CMS.Admin.Controllers
{
    public class HoSoController : BaseController
    {
        #region dungchung
        private DungChung_1CuaServiceClient _DungChungSrv;
        private int _pageSize;
        #endregion
        public HoSoController()
        {
            var pageSize = DungChung.GetKeyCauHinhHeThong("SoHangHienThi");
            if (!int.TryParse(pageSize, out this._pageSize))
                this._pageSize = 10;
        }
        [CustomAuthorize(RightName = CookieRight.HoSoController_Index)]
        public ActionResult Index()
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            var result = new HoSoViewModel();
            result.Search = new HoSoParam();
            try
            {
                result.Search.PageSize = this._pageSize;
                result.Search.PageIndex = 1;
                var user = LoginManager.GetCurrentUser();
                var modelSearch = new HoSo_1CuaParam()
                {
                    PageIndex = result.Search.PageIndex,
                    PageSize = result.Search.PageSize,
                    NguoiNhan = user.UserPortalID.ToString(),
                    DonViID = (int)user.DonViID,
                    PhongBanID = (int)user.PhongBanID,
                    ChucNangHienTai = "THAMDINHHOSO",
                    Follow = result.Search.HoSoDangTheoDoi ? 1 : 0,
                    SoBienNhan = result.Search.SoBienNhan,
                    SoBoHoSo = result.Search.MaBoHoSo,
                    TinhThanhID = result.Search.TinhThanhID.HasValue ? result.Search.TinhThanhID.Value.ToString() : "",
                    HuyenThiThanhPhoID = result.Search.QuanHuyenID.HasValue ? result.Search.QuanHuyenID.Value.ToString() : "",
                    PhuongXaID = result.Search.PhuongXaID.HasValue ? result.Search.PhuongXaID.Value.ToString() : "",
                    ChucNangKeTiep = result.Search.TenBuocKeTiep,
                    //TinhTrangPT = result.Search.TinhTrangID.HasValue ? result.Search.TinhTrangID.Value.ToString() : "",
                    TinhTrang = result.Search.TinhTrangID.HasValue ? result.Search.TinhTrangID.Value.ToString() : "",
                    NguoiDungTenHoSo = result.Search.CongDanToChuc,
                    LinhVucID = result.Search.LinhVucID.HasValue ? result.Search.LinhVucID.Value.ToString() : "",
                    ThuTucHanhChinhID = result.Search.ThuTucID.HasValue ? result.Search.ThuTucID.Value.ToString() : "",
                    TuNgay = result.Search.TuNgay,
                    DenNgay = result.Search.DenNgay,
                    LoaiXuLy = "1,3,4,5", // Màn hình hs của tôi gắn cứng 1,3,4,5
                    NguoiNhanHoSo = "",
                    ChucNangKeTiepNguoiNhan = "",
                    LoaiXuLyNguoiNhan = "",
                    TenTacPham = "",
                };
                _DungChungSrv = new DungChung_1CuaServiceClient();
                var tempList = _DungChungSrv.DanhSachHoSo(modelSearch);
                if (tempList.Data != null && tempList.Data != null)
                {
                    DungChung.ghinhatkynguoidung("Xem danh sách hồ sơ",
                                               "HoSoController",
                                               "Index", "View");

                    result.Items = tempList.Data;
                }
                return View(result);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("HoSoController/Index error:" + ex.Message, ex, Request);
                return View(result);
            }
        }
        [HttpPost]
        public ActionResult ListHoSo(HoSoParam model)
        {
            var result = new HoSoViewModel();
            result.Search = model;
            try
            {
                var user = LoginManager.GetCurrentUser();
                var modelSearch = new HoSo_1CuaParam()
                {
                    PageIndex = result.Search.PageIndex,
                    PageSize = result.Search.PageSize,
                    NguoiNhan = user.UserPortalID.ToString(),
                    DonViID = (int)user.DonViID,
                    PhongBanID = (int)user.PhongBanID,
                    ChucNangHienTai = "THAMDINHHOSO",
                    Follow = result.Search.HoSoDangTheoDoi ? 1 : 0,
                    SoBienNhan = result.Search.SoBienNhan,
                    SoBoHoSo = result.Search.MaBoHoSo,
                    TinhThanhID = result.Search.TinhThanhID.HasValue ? result.Search.TinhThanhID.Value.ToString() : "",
                    HuyenThiThanhPhoID = result.Search.QuanHuyenID.HasValue ? result.Search.QuanHuyenID.Value.ToString() : "",
                    PhuongXaID = result.Search.PhuongXaID.HasValue ? result.Search.PhuongXaID.Value.ToString() : "",
                    ChucNangKeTiep = result.Search.TenBuocKeTiep,
                    //TinhTrangPT = result.Search.TinhTrangID.HasValue ? result.Search.TinhTrangID.Value.ToString() : "",
                    TinhTrang = result.Search.TinhTrangID.HasValue ? result.Search.TinhTrangID.Value.ToString() : "",
                    NguoiDungTenHoSo = result.Search.CongDanToChuc,
                    LinhVucID = result.Search.LinhVucID.HasValue ? result.Search.LinhVucID.Value.ToString() : "",
                    ThuTucHanhChinhID = result.Search.ThuTucID.HasValue ? result.Search.ThuTucID.Value.ToString() : "",
                    TuNgay = result.Search.TuNgay != null ? DateTime.ParseExact(result.Search.TuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") : null,
                    DenNgay = result.Search.DenNgay != null ? DateTime.ParseExact(result.Search.DenNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") : null,
                    LoaiXuLy = "1,3,4,5", // Màn hình hs của tôi gắn cứng 1,3,4,5
                    NguoiNhanHoSo = "",
                    ChucNangKeTiepNguoiNhan = "",
                    LoaiXuLyNguoiNhan = "",
                    TenTacPham = result.Search.TenTacPham,
                };
                if (result.Search.HoSoSapDenHan)
                    modelSearch.TinhTrangXLHS = "2";
                if (result.Search.HoSoQuaHan)
                    modelSearch.TinhTrangXLHS = (!string.IsNullOrEmpty(modelSearch.TinhTrangXLHS) ? "," : "") + "1";
                if (!string.IsNullOrEmpty(model.DuongDiNguoiNhan))
                {
                    var arr = model.DuongDiNguoiNhan.Split(',');
                    foreach (var item in arr)
                    {
                        var arr2 = item.Split('_');
                        if (arr2.Length > 2)
                        {
                            modelSearch.ChucNangKeTiepNguoiNhan += (string.IsNullOrEmpty(modelSearch.ChucNangKeTiepNguoiNhan) ? "" : ",") + arr2[0];
                            modelSearch.LoaiXuLyNguoiNhan += (string.IsNullOrEmpty(modelSearch.LoaiXuLyNguoiNhan) ? "" : ",") + arr2[1];
                            modelSearch.NguoiNhanHoSo += (string.IsNullOrEmpty(modelSearch.NguoiNhanHoSo) ? "" : ",") + arr2[2];
                        }

                    }
                }
                _DungChungSrv = new DungChung_1CuaServiceClient();
                var tempList = _DungChungSrv.DanhSachHoSo(modelSearch);
                if (tempList.Data != null && tempList.Data != null)
                {
                    DungChung.ghinhatkynguoidung("Xem danh sách hồ sơ",
                                               "HoSoController",
                                               "Index", "View");

                    result.Items = tempList.Data;
                }
                return PartialView("_PartialList", result.Items);
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("HoSoController/ListHoSo error:" + ex.Message, ex, Request);
                return PartialView("_PartialList", result.Items);
            }
        }
        [HttpPost]
        public ActionResult NhanHoSo(List<NhanHoSo_1CuaAdd> model)
        {
            try
            {
                _DungChungSrv = new DungChung_1CuaServiceClient();
                if (model == null)
                {
                    return new JsonResult
                    {
                        Data = new
                        {
                            status = false,
                            message = "Vui lòng chọn hồ sồ cần nhận.!!!"
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                var status = true;
                var message = "";
                foreach (var item in model)
                {
                    var user = LoginManager.GetCurrentUser();
                    item.NguoiXuLyID = user.UserPortalID.ToString();
                    item.PhongBanXuLyID = (int)user.PhongBanID;
                    item.DonViXuLyID = (int)user.DonViID;
                    var temp = _DungChungSrv.NhanHoSo(item);
                    if (temp == null || temp.Data == null || temp.Data.IsSuccessed == false)
                    {
                        status = false;
                        message += (string.IsNullOrEmpty(message) ? "" : ", ") + item.SoBienNhan;
                    }

                }
                DungChung.ghinhatkynguoidung("Nhận hồ sơ",
                                           "HoSoController",
                                           "NhanHoSo", "View");
                return new JsonResult
                {
                    Data = new
                    {
                        status = status,
                        message = status ? "" : ("Nhận hồ sơ " + message + " thất bại.!!!")
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("HoSoController/NhanHoSo error:" + ex.Message, ex, Request);
                return new JsonResult
                {
                    Data = new
                    {
                        status = false,
                        message = "Nhận hồ sơ thất bại.!!!"
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
        [HttpPost]
        public ActionResult ChuyenHoSo(List<ChuyenHoSo_1CuaAdd> model)
        {
            try
            {
                _DungChungSrv = new DungChung_1CuaServiceClient();
                if (model == null)
                {
                    return new JsonResult
                    {
                        Data = new
                        {
                            status = false,
                            message = "Vui lòng chọn hồ sồ cần chuyển.!!!"
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                var status = true;
                var message = "";
                var messageCapSo = "";
                var ckCapSo = false;
                foreach (var item in model)
                {
                    using (var _hoSoSRV = new HS_CapSoServiceClient())
                    {
                        var capSo = _hoSoSRV.HS_HoSo_CheckCapSo(item.HoSoID);
                        if (capSo != null && capSo.Data != null && capSo.Data.resultObject == 1)
                            ckCapSo = true;
                        else
                            ckCapSo = false;
                    }
                    if(ckCapSo == true)
                    {
                        var user = LoginManager.GetCurrentUser();
                        item.UserID = (int)user.UserPortalID;
                        item.PhongBanID = (int)user.PhongBanID;
                        item.DonViID = (int)user.DonViID;
                        item.UserName = user.UserName;
                        var temp = _DungChungSrv.ChuyenHoSo(item);
                        if (temp == null || temp.Data == null || temp.Data.IsSuccessed == false)
                        {
                            status = false;
                            message += (string.IsNullOrEmpty(message) ? "" : ", ") + item.SoBienNhan;
                        }
                    }
                    else
                    {
                        status = false;
                        messageCapSo += (string.IsNullOrEmpty(messageCapSo) ? "" : ", ") + item.SoBienNhan;
                    }
                }
                DungChung.ghinhatkynguoidung("Chuyển hồ sơ",
                                           "HoSoController",
                                           "ChuyenHoSo", "View");
                return new JsonResult
                {
                    Data = new
                    {
                        status = status,
                        message = status ? "" : ("Chuyển hồ sơ " + message + " thất bại, hồ sơ " + messageCapSo + " chưa cấp số.!!!")
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("HoSoController/ChuyenHoSo error:" + ex.Message, ex, Request);
                return new JsonResult
                {
                    Data = new
                    {
                        status = false,
                        message = "Chuyển hồ sơ thất bại.!!!"
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
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
                DungChung.ghiloghethong("HoSoController/ListDMQuanHuyen error:" + ex.Message, ex, Request);
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
                DungChung.ghiloghethong("HoSoController/ListDMPhuongXa error:" + ex.Message, ex, Request);
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
                DungChung.ghiloghethong("HoSoController/ListDMThuTuc error:" + ex.Message, ex, Request);
                return new JsonResult
                {
                    Data = null,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        [HttpPost]
        public ActionResult ListDMTenBuocKeTiep(DM_TenBuocKeTiep_1CuaParam model)
        {
            try
            {
                var temp = CBODungChung.GetCBOTenBuocKeTiep_1Cua(model.LinhVucID, model.ThuTucHanhChinhID, model.ChucNangHienTai, model.LoaiXuLy);
                return new JsonResult
                {
                    Data = temp.ToList(),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("HoSoController/ListDMThuTuc error:" + ex.Message, ex, Request);
                return new JsonResult
                {
                    Data = null,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        [HttpPost]
        public ActionResult HoSoChuaTiepNhan()
        {
            bool status = false; long data = 0;
            try
            {
                var user = LoginManager.GetCurrentUser();
                var modelSearch = new HoSo_1CuaParam()
                {
                    PageIndex = 1,
                    PageSize = 1,
                    NguoiNhan = user.UserPortalID.ToString(),
                    DonViID = (int)user.DonViID,
                    PhongBanID = (int)user.PhongBanID,
                    ChucNangHienTai = "THAMDINHHOSO",
                    Follow = 0,
                    SoBienNhan = null,
                    SoBoHoSo = null,
                    TinhThanhID = "0",
                    HuyenThiThanhPhoID = "",
                    PhuongXaID = "",
                    ChucNangKeTiep = "",
                    TinhTrangPT = "",
                    NguoiDungTenHoSo = "",
                    LinhVucID = "",
                    ThuTucHanhChinhID = "",
                    TuNgay = null,
                    DenNgay = null,
                    LoaiXuLy = "1,3,4,5", // Màn hình hs của tôi gắn cứng 1,3,4,5
                    NguoiNhanHoSo = "",
                    ChucNangKeTiepNguoiNhan = "",
                    LoaiXuLyNguoiNhan = "",
                };
                _DungChungSrv = new DungChung_1CuaServiceClient();
                var tempList = _DungChungSrv.DanhSachHoSo(modelSearch);
                if (tempList.Data != null && tempList.Data != null)
                {
                    DungChung.ghinhatkynguoidung("Xem danh sách hồ sơ chưa tiếp nhận",
                                               "HoSoController",
                                               "HoSoChuaTiepNhan", "View");
                    status = true;
                    data = tempList.Data.ListHoSoThuLy[0].TotalRecords;
                }
                return Json(new { status = status, data = data });
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("HoSoController/HoSoChuaTiepNhan error:" + ex.Message, ex, Request);
                return Json(new { status = status, data = data });
            }
        }

        [HttpPost]
        public ActionResult GetLoaiNghiepVu(long id)
        {
            int loaiNghiepVuId = 0;
            try
            {
                using (var capSo = new HS_CapSoServiceClient())
                {
                    var temp = capSo.TT_CapQuyen_GetLoaiNghiepVuId(id);
                    if (temp != null && temp.Data != null && temp.Data.resultObject > 0)
                    {
                        loaiNghiepVuId = temp.Data.resultObject;
                    }
                }
                return new JsonResult { Data = loaiNghiepVuId, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("HoSoController/ListDMQuanHuyen error:" + ex.Message, ex, Request);
                return new JsonResult
                {
                    Data = null,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
    }
}