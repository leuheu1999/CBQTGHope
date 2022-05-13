using System;
using System.IO;
using System.Linq;
using System.Web;
namespace Core.Common.Utilities
{
    public static class FileControl
    {
        public static Files SaveFileUpload(string root, string url, HttpPostedFileBase file)
        {
			var fileinfo = new Files();
            if (file != null && file.ContentLength > 0)
            {
                DateTime now = DateTime.Now;
                string month = now.Month.ToString();
                string year = now.Year.ToString();
                string pathFile = Path.Combine(root, url, year, month);
                DirectoryInfo dir = new DirectoryInfo(pathFile);
                if (!dir.Exists)
                {
                    Directory.CreateDirectory(pathFile);
                }
                var fname = Path.GetFileName(file.FileName);
	            var originalName = Guid.NewGuid() + Path.GetExtension(fname);
				fileinfo.TenGoc = fname;
	            fileinfo.TenUpload = originalName;
				url = Path.Combine(url, year, month, originalName);
                pathFile = Path.Combine(root, url);
                file.SaveAs(pathFile);
            }
			fileinfo.Url = url;
            return fileinfo;
        }

        public static string GetAbsolutePathFileUpload(string root, string url, string fname, string folder = "")
        {           
            DateTime now = DateTime.Now;
            string month = now.Month.ToString();
            string year = now.Year.ToString();
            string pathFile = Path.Combine(root, url, folder, year, month);
            DirectoryInfo dir = new DirectoryInfo(pathFile);
            if (!dir.Exists) Directory.CreateDirectory(pathFile);
            fname = string.Format("{0}_{1}{2}", Path.GetFileNameWithoutExtension(fname), string.Format("{0:yyyyMMddHHmmssfff}", now), Path.GetExtension(fname));
            url = Path.Combine(root, url, folder, year, month, fname);
            return url;
        }

        public static string GetFileLastWriteTime(string path)
        {
            var directory = new DirectoryInfo(path);
            var filePath = (from f in directory.GetFiles()
                            orderby f.LastWriteTime descending
                            select f).First();
            return filePath.FullName;
        }

        public static string GetPathForTime(string root, string url, string folder = "")
        {
            DateTime now = DateTime.Now;
            string month = now.Month.ToString();
            string year = now.Year.ToString();
            string pathFile = Path.Combine(root, url, folder, year, month);
            return pathFile;
        }
    }
	public class Files
	{
		public string TenGoc { get; set; }
		public string TenUpload { get; set; }
		public string Url { get; set; }
	}
}
