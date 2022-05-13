using CMS.Admin.Common;
using CMS.Admin.Constaints;
using Module.Framework;
using System;
using System.Web.Mvc;

namespace CMS.Admin.Controllers
{
    public class HoSoChuyenNganhController : BaseController
    {
        #region dungchung
        private DungChungServiceClient _dungChungSrv;
        private int _pageSize;
        #endregion
        public HoSoChuyenNganhController()
        {
            var pageSize = DungChung.GetKeyCauHinhHeThong("SoHangHienThi");
            if (!int.TryParse(pageSize, out this._pageSize))
                this._pageSize = 10;
        }
        [CustomAuthorize(RightName = CookieRight.HoSoChuyenNganhController_Index)]
        public ActionResult Index(string hoSoID, string thuTucHanhChinhID, string key)
        {
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            string controller = "", action = ""; string keyMapId = "";
            try
            {
                using (_dungChungSrv = new DungChungServiceClient())
                {
                    if (thuTucHanhChinhID != "" && thuTucHanhChinhID != null)
                    {
                        var tempList = _dungChungSrv.MAP_ThuTuc_ManHinh_GetById(long.Parse(thuTucHanhChinhID));
                        if (tempList != null && tempList.Data != null && tempList.Data.resultObject != null)
                            controller = tempList.Data.resultObject.MaLinhVuc; action = tempList.Data.resultObject.ManHinh;
                    }
                }
                using (var _capSoSrv = new HS_CapSoServiceClient())
                {
                    if (hoSoID != "" && hoSoID != null)
                    {
                        var tempList = _capSoSrv.TT_CapQuyen_GetByHoSoId(long.Parse(hoSoID));
                        if (tempList != null && tempList.Data != null && tempList.Data.resultObject != null)
                            keyMapId = tempList.Data.resultObject.KeyMapID.ToString();
                    }
                }
                if (controller.Length > 0 && action.Length > 0)
                    return RedirectToAction(action, controller, new { id = keyMapId, hoSoID = hoSoID, isMap = true, key = key });
                else
                    return View();
            }
            catch (Exception ex)
            {
                DungChung.ghiloghethong("QTG_QuyenTacGiaController/SaveTacGia error:" + ex.Message, ex, Request);
                return View();
            }
        }
    }
}