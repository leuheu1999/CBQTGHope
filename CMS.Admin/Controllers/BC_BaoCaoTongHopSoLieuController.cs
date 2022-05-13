using Business.Entities.Domain;
using CMS.Admin.Common;
using CMS.Admin.Constaints;
using CMS.Admin.Models;
using System.Web.Mvc;

namespace CMS.Admin.Controllers
{
    public class BC_BaoCaoTongHopSoLieuController : BaseController
    {
        [CustomAuthorize(RightName = CookieRight.BC_BaoCaoTongHopSoLieu_Index)]
        public ActionResult Index()
        {
            var result = new BC_BaoCaoTongHopSoLieuViewModel();
            result.Search = new BC_BaoCaoTongHopSoLieuParam();
            if (DungChung.CheckTimeDN() == false)
            {
                return RedirectToAction("LogOff", "NguoiDungHeThong");
            }
            return View(result);
        }
    }
}