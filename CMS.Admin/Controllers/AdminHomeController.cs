using Module.Framework;
using System;
using System.Web.Mvc;

namespace CMS.Admin.Controllers
{
    public class AdminHomeController : BaseController
    {
        private DungChungServiceClient _DungChungSrv;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        public virtual ActionResult DisposeCache(string key)
        {
            try
            {
                using (_DungChungSrv = new DungChungServiceClient())
                {
                    _DungChungSrv.XaoCacheService(key);
                }
                return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { status = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}