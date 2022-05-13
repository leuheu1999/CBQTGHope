using System;
using System.Collections.Generic;
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
                var originalName = string.Format("{0}_{1}{2}", Path.GetFileNameWithoutExtension(fname), string.Format("{0:ddHHmmssfff}", now), Path.GetExtension(fname));
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
        public static List<string> GetImageFileExtensions()
        {
            List<String> fileExtensions = new List<String>();
            fileExtensions.Add(".bmp");
            fileExtensions.Add(".gif");
            fileExtensions.Add(".jpeg");
            fileExtensions.Add(".jpg");
            fileExtensions.Add(".jpe");
            fileExtensions.Add(".jfif");
            fileExtensions.Add(".pjpeg");
            fileExtensions.Add(".pjp");
            fileExtensions.Add(".png");
            fileExtensions.Add(".tiff");
            fileExtensions.Add("tif");
            fileExtensions.Add(".mp4");
            fileExtensions.Add(".avi");
            fileExtensions.Add(".flv");
            fileExtensions.Add(".wmv");
            fileExtensions.Add(".mov");
            fileExtensions.Add(".mpeg");
            return fileExtensions;
        }
        public static List<string> GetImageMimeTypes()
        {
            List<String> fileTypes = new List<String>();
            fileTypes.Add("image/bmp");
            fileTypes.Add("image/gif");
            fileTypes.Add("image/jpeg");
            fileTypes.Add("image/pjpeg");
            fileTypes.Add("image/png");
            fileTypes.Add("image/tiff");
            fileTypes.Add("video/mp4");
            return fileTypes;
        }
        public static List<string> GetNotWhitelistCreateFolder()
        {
            List<String> nameFolder = new List<String>();
            nameFolder.Add(".");
            nameFolder.Add("./");
            nameFolder.Add("/");
            nameFolder.Add("../");
            nameFolder.Add(".../");
            nameFolder.Add("/..");
            nameFolder.Add("/...");
            nameFolder.Add("/.");
            nameFolder.Add(".././.../");
            return nameFolder;
        }
        public static List<string> GetNotWhitelistHandler()
        {
            List<String> fileExtensions = new List<String>();
            fileExtensions.Add("javascript:");
            fileExtensions.Add("svg/onload");
            fileExtensions.Add("alert(");
            fileExtensions.Add("<script>");
            fileExtensions.Add("FSCommand");
            fileExtensions.Add("onAbort");
            fileExtensions.Add("onActivate");
            fileExtensions.Add("onAfterPrint");
            fileExtensions.Add("onAfterUpdate");
            fileExtensions.Add("onBeforeActivate");
            fileExtensions.Add("onBeforeCopy");
            fileExtensions.Add("onBeforeCut");
            fileExtensions.Add("onBeforeDeactivate");
            fileExtensions.Add("onBeforeEditFocus");
            fileExtensions.Add("onBeforePaste");
            fileExtensions.Add("onBeforePrint");
            fileExtensions.Add("onBeforeUnload");
            fileExtensions.Add("onBeforeUpdate");
            fileExtensions.Add("onBegin");
            fileExtensions.Add("onBlur");
            fileExtensions.Add("onBounce");
            fileExtensions.Add("onCellChange");
            fileExtensions.Add("onChange");
            fileExtensions.Add("onClick");
            fileExtensions.Add("onContextMenu");
            fileExtensions.Add("onControlSelect");
            fileExtensions.Add("onCopy");
            fileExtensions.Add("onCut");
            fileExtensions.Add("onDataAvailable");
            fileExtensions.Add("onDataSetChanged");
            fileExtensions.Add("onDataSetComplete");
            fileExtensions.Add("onDblClick");
            fileExtensions.Add("onDeactivate");
            fileExtensions.Add("onDrag");
            fileExtensions.Add("onDragEnd");
            fileExtensions.Add("onDragLeave");
            fileExtensions.Add("onDragEnter");
            fileExtensions.Add("onDragOver");
            fileExtensions.Add("onDragDrop");
            fileExtensions.Add("onDragStart");
            fileExtensions.Add("onDrop");
            fileExtensions.Add("onEnd");
            fileExtensions.Add("onError");
            fileExtensions.Add("onErrorUpdate");
            fileExtensions.Add("onFilterChange");
            fileExtensions.Add("onFinish");
            fileExtensions.Add("onFocus");
            fileExtensions.Add("onFocusIn");
            fileExtensions.Add("onFocusOut");
            fileExtensions.Add("onHashChange");
            fileExtensions.Add("onHelp");
            fileExtensions.Add("onInput");
            fileExtensions.Add("onKeyDown");
            fileExtensions.Add("onKeyPress");
            fileExtensions.Add("onKeyUp");
            fileExtensions.Add("onLayoutComplete");
            fileExtensions.Add("onLoad");
            fileExtensions.Add("onLoseCapture");
            fileExtensions.Add("onMediaComplete");
            fileExtensions.Add("onMediaError");
            fileExtensions.Add("onMessage");
            fileExtensions.Add("onMouseDown");
            fileExtensions.Add("onMouseEnter");
            fileExtensions.Add("onMouseLeave");
            fileExtensions.Add("onMouseMove");
            fileExtensions.Add("onMouseOut");
            fileExtensions.Add("onMouseOver");
            fileExtensions.Add("onMouseUp");
            fileExtensions.Add("onMouseWheel");
            fileExtensions.Add("onMove");
            fileExtensions.Add("onMoveEnd");
            fileExtensions.Add("onMoveStart");
            fileExtensions.Add("onOffline");
            fileExtensions.Add("onOnline");
            fileExtensions.Add("onOutOfSync");
            fileExtensions.Add("onPause");
            fileExtensions.Add("onPopState");
            fileExtensions.Add("onProgress");
            fileExtensions.Add("onPropertyChange");
            fileExtensions.Add("onReadyStateChange");
            fileExtensions.Add("onRedo");
            fileExtensions.Add("onRepeat");
            fileExtensions.Add("onReset");
            fileExtensions.Add("onResize");
            fileExtensions.Add("onResizeEnd");
            fileExtensions.Add("onResizeStart");
            fileExtensions.Add("onResume");
            fileExtensions.Add("onReverse");
            fileExtensions.Add("onRowsEnter");
            fileExtensions.Add("onRowExit");
            fileExtensions.Add("onRowDelete");
            fileExtensions.Add("onRowInserted");
            fileExtensions.Add("onScroll");
            fileExtensions.Add("onSeek");
            fileExtensions.Add("onSelect");
            fileExtensions.Add("onSelectionChange");
            fileExtensions.Add("onSelectStart");
            fileExtensions.Add("onStart");
            fileExtensions.Add("onStop");
            fileExtensions.Add("onStorage");
            fileExtensions.Add("onSyncRestored");
            fileExtensions.Add("onSubmit");
            fileExtensions.Add("onTimeError");
            fileExtensions.Add("onTrackChange");
            fileExtensions.Add("onUndo");
            fileExtensions.Add("onUnload");
            fileExtensions.Add("onURLFlip");
            fileExtensions.Add("seekSegmentTime");
            return fileExtensions;
        }
    }
    public class Files
    {
        public string TenGoc { get; set; }
        public string TenUpload { get; set; }
        public string Url { get; set; }
    }
}
