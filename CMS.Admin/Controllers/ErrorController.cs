using System.Web.Mvc;
namespace CMS.Admin.Controllers
{
    public class ErrorController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Browsers()
        {
            return View();
        }

        public ActionResult Permission()
        {
            return View();
        }
	}
}