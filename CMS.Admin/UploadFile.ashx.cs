using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace KhuyenMai.Admin
{
    /// <summary>
    /// Summary description for UploadFile
    /// </summary>
    public class UploadFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpPostedFile uploads = context.Request.Files["upload"];
            string CKEditorFuncNum = context.Request["CKEditorFuncNum"];
            string filename = Path.GetFileName(uploads.FileName);
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            var pathDefault = "/UPLOADS/" + year + '/' + month + '/';
            var path = Path.Combine(HttpContext.Current.Server.MapPath('~' + pathDefault));
            var uploadName = DateTime.Now.ToString("yyyyMMddhhmmssffff") + Path.GetExtension(filename);
            bool isExists = Directory.Exists(path);
            if (!isExists)
                Directory.CreateDirectory(path);
            var pathString = path + Path.DirectorySeparatorChar + uploadName;
            uploads.SaveAs(pathString);
            string url = pathDefault + uploadName;
            context.Response.Write("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\");</script>");
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}