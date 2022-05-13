using Business.Entities;
using CMS.Admin.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CMS.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index(int id)
        {
            //var model = new EFormMapAdd();
            //if (id > 0)
            //{
            //    _EformSrv = new EFormServiceClient();
            //    var temp = _EformSrv.QL_EForm_GetByID(id);
            //    if (temp.Data != null && temp.Data.resultObject != null)
            //    {
            //        model = temp.Data.resultObject;
            //    }
            //}
            //var genHtml = _EformSrv.Eform_Design_DM_Control_GetAll();
            //ViewBag.GenHtml = null;
            //if (genHtml != null && genHtml.Data != null && genHtml.Data.resultObject != null && genHtml.Data.resultObject.Count > 0)
            //{
            //    ViewBag.GenHtml = genHtml.Data.resultObject;
            //}
            return View();
        }
        public ActionResult PartialLeftMenu()
        {
            //_EformSrv = new EFormServiceClient();
            //var model = _EformSrv.EForm_GetAllGroupControlAndControl(1);
            //var result = new List<E_GroupControlDesignEformMap>();
            //if(model != null && model.Data != null && model.Data.resultObject != null && model.Data.resultObject.Count > 0)
            //{
            //    result = model.Data.resultObject;
            //}
            return PartialView("_PartialLeftMenu");
        }
        [HttpPost]
        public ActionResult CBOUsePresetList(string table)
        {
            var list = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(table))
            {
                var search = new CBO_DungChungParam();
                search.TableName = table;
                list = CBODungChung.GetCBODungChung(search).ToList();
            }
            return new JsonResult
            {
                Data = new
                {
                    data = list
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
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
    }
}