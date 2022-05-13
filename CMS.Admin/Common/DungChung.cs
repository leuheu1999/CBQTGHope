using Business.Entities.Domain;
using CMS.Admin.Models;
using Core.Common.Utilities;
using Ganss.XSS;
using Module.Framework;
using Module.Framework.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace CMS.Admin.Common
{
    public static class DungChung
    {
        public static string GetKeyCauHinhHeThong(string Key)
        {
            using (var DungChungServiceClient = new DungChungServiceClient())
            {
                var result = DungChungServiceClient.GetKeyCauHinhHeThong(Key);
                return result != null && !string.IsNullOrEmpty(result.resultObject) ? result.resultObject : "";
            }
        }
        public static string GetKeyDMDonVi(string Key)
        {
            using (var DungChungServiceClient = new DungChungServiceClient())
            {
                var result = DungChungServiceClient.GetKeyDMDonVi(Key);
                return result != null && !string.IsNullOrEmpty(result.resultObject) ? result.resultObject : "";
            }
        }
        public static void ghinhatkynguoidung(string noidung, string controller, string action, string keyword)
        {
            var us = LoginManager.GetCurrentUser();
            if (us != null)
            {
                var modelnhatky = new ND_NhatKyNguoiDungAdd()
                {
                    NguoiDungID = Guid.Parse(us.UserId),
                    TenTaiKhoan = us.UserName,
                    NoiDung = noidung,
                    Controller = controller,
                    Action = action,
                    KeyWord = keyword
                };
                UsersServiceClient _UsersSrv = new UsersServiceClient();
                var nhatky = _UsersSrv.NhatKyNguoiDung_Insert(modelnhatky);
            }

        }
        public static void ghiloghethong(string error, Exception ex, HttpRequestBase Request)
        {
            LoggingServiceClient _Log = new LoggingServiceClient();
            var us = LoginManager.GetCurrentUser();
            if (us != null)
            {
                var modellog = new LogAddView()
                {
                    ShortMessages = error,
                    ex = ex,
                    UserID = Guid.Parse(us.UserId),
                    UrlPath = Request.Url.AbsoluteUri,
                    ReferrerUrl = Request.UrlReferrer != null ? Request.UrlReferrer.PathAndQuery : ""
                };
                _Log.LogError(modellog);
            }
        }
        public static bool CheckTimeDN()
        {
            var user = LoginManager.GetCurrentUser();
            if (user != null && user.UserName != "Host" && user.UserName != "host")
            {
                var TimeStart = DungChung.GetKeyCauHinhHeThong("ThoiGianTruyCapTu");
                var TimeEnd = DungChung.GetKeyCauHinhHeThong("ThoiGianTruyCapDen");
                string TimeNow = DateTime.Now.ToString("H:mm");
                int HourStart = 0;
                int MiniStart = 0;
                int HourNow = 0;
                int MiniNow = 1;
                int HourEnd = 23;
                int MiniEnd = 59;
                if (!string.IsNullOrEmpty(TimeStart))
                {
                    var ArrStart = TimeStart.Split(':');
                    HourStart = int.Parse(ArrStart[0]);
                    MiniStart = int.Parse(ArrStart[1]);
                    var ArrNow = TimeNow.Split(':');
                    HourNow = int.Parse(ArrNow[0]);
                    MiniNow = int.Parse(ArrNow[1]);
                    var ArrEnd = TimeEnd.Split(':');
                    HourEnd = int.Parse(ArrEnd[0]);
                    MiniEnd = int.Parse(ArrEnd[1]);
                }
                if (HourNow < HourStart || HourNow > HourEnd)
                {
                    return false;
                }
                else if (HourStart == HourNow && MiniStart > MiniNow)
                {
                    return false;
                }
                else if (HourEnd == HourNow && MiniEnd < MiniNow)
                {
                    return false;
                }
            }
            return true;
        }
        public static string[] FileExt = new string[] { "doc", "docx", "xls", "xlsx", "pdf", "png", "jpg", "jpge" };
        public static bool FileUploadRegex(string fileName)
        {
            if (CheckExtension(fileName.Split('.').Last(), FileExt))
            {
                return true;
            }
            return false;
        }
        private static bool CheckExtension(string fileext, string[] lstExt)
        {
            foreach (var ext in lstExt)
            {
                if (fileext.ToLower() == ext.ToLower())
                    return true;
            }
            return false;
        }
        public static int GetRandom(int start = 1, int end = 100)
        {
            Random r = new Random();
            return r.Next(start, end);
        }
        public static bool SendEmail(EmailModel modelmail) //tempalte xml,System.Web.Hosting.HostingEnvironment.MapPath("~/" + SmsTemplateDir + "/BaoCaoThongKe.xml");
        {
            try
            {
                string PathTemplate = HttpContext.Current.Server.MapPath(modelmail.TemplateMail);
                if (modelmail != null && !string.IsNullOrEmpty(modelmail.SendTo) && !string.IsNullOrEmpty(PathTemplate))
                {
                    if (!System.IO.File.Exists(PathTemplate))
                    {
                        // Không tìm thấy template
                        return false;
                    }
                    StreamReader doc = System.IO.File.OpenText(PathTemplate);
                    string content = doc.ReadToEnd();
                    if (!string.IsNullOrEmpty(content))
                    {
                        foreach (var item in modelmail.Params)
                        {
                            content = content
                                 .Replace("[" + item.Key + "]", item.Value);
                        }
                    }
                    Email objEmail = new Email();
                    objEmail.receivers = modelmail.SendTo;
                    objEmail.subject = modelmail.Subject;
                    objEmail.content = content;
                    Thread email = new Thread(objEmail.Send);
                    email.Start();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static string FortmatMoney(string value)
        {
            if (string.IsNullOrEmpty(value)) return "0";
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            decimal result = decimal.Parse(value);
            return String.Format(culture, "{0:N0}", result);
        }
        public static string SxxEndcodeNoiDung(string textValue)
        {
            textValue = !string.IsNullOrEmpty(textValue) ? textValue.Replace("&amp;", "&")
                                         .Replace("&lt;", "<")
                                         .Replace("&gt;", ">")
                                         .Replace("&quot;", "\"")
                                         .Replace("&#039;", "\'") : "";
            var htmlSan = new HtmlSanitizer();
            htmlSan.AllowedAttributes.Add("class");
            textValue = htmlSan.Sanitize(textValue);
            return textValue;
        }
        public static string SxxEndcodeText(string textValue, bool _check = false) // check = true: cos ma hoa o duoi len
        {
            if (string.IsNullOrEmpty(textValue)) return null;
            try
            {
                if (_check == true)
                {
                    textValue = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(textValue));
                }
                Regex tagRegex = new Regex(@"<[a-z][\s\S]*>");
                Regex tagRegex2 = new Regex(@"<[/s/S][a-z][\s\S]*>");
                if (tagRegex.IsMatch(textValue) || tagRegex2.IsMatch(textValue))
                {
                    return "";
                }
                textValue = textValue.Replace("&amp;", "&")
                                      .Replace("&lt;", "<")
                                      .Replace("&gt;", ">")
                                      .Replace("&quot;", "\"")
                                      .Replace("&#039;", "\'")
                                      .Replace("&ldquo;", "“")
                                      .Replace("&rdquo;", "”")
                                      .Replace("&#13;", "\r")
                                      .Replace("&#10;", "\n");
                var value2 = textValue;
                foreach (var item in GetNotWhitelistHandler())
                {
                    if (value2.ToUpper().Contains(item.ToUpper()))
                    {
                        return "";
                    }
                }
                var unValue = UnicodeExtensions.convertToUnSign3(value2.Replace("-", "").Replace("\r", "").Replace("\n", "").Replace("“", "").Replace("”", ""));
                var safeValue = System.Web.Security.AntiXss.AntiXssEncoder.HtmlEncode(unValue, true);
                if (unValue != safeValue)
                {
                    return "";
                }
            }
            catch (Exception)
            {
                return "";
            }
            return textValue;
        }
        public static List<string> GetNotWhitelistHandler()
        {
            List<String> fileExtensions = new List<String>();
            fileExtensions.Add("javascript:");
            fileExtensions.Add("eval");
            fileExtensions.Add("onload");
            fileExtensions.Add("alert");
            fileExtensions.Add("script");
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
            fileExtensions.Add("onanimationcancel");
            fileExtensions.Add("onanimationiteration");
            fileExtensions.Add("onauxclick");
            fileExtensions.Add("oncanplay");
            fileExtensions.Add("oncanplaythrough");
            fileExtensions.Add("onclose");
            fileExtensions.Add("oncuechange");
            fileExtensions.Add("onfullscreenchange");
            fileExtensions.Add("oninvalid");
            fileExtensions.Add("onmozfullscreenchange");
            fileExtensions.Add("onpagehide");
            fileExtensions.Add("onpageshow");
            fileExtensions.Add("onpaste");
            fileExtensions.Add("onplay");
            fileExtensions.Add("onplaying");
            fileExtensions.Add("onpointerdown");
            fileExtensions.Add("onpointerenter");
            fileExtensions.Add("onpointerleave");
            fileExtensions.Add("onpointermove");
            fileExtensions.Add("onpointerout");
            fileExtensions.Add("onpointerover");
            fileExtensions.Add("onpointerrawupdate");
            fileExtensions.Add("onpointerup");
            fileExtensions.Add("onsearch");
            fileExtensions.Add("onshow");
            fileExtensions.Add("ontimeupdate");
            fileExtensions.Add("ontoggle");
            fileExtensions.Add("ontouchend");
            fileExtensions.Add("ontouchmove");
            fileExtensions.Add("ontouchstart");
            fileExtensions.Add("ontransitioncancel");
            fileExtensions.Add("ontransitionrun");
            fileExtensions.Add("onunhandledrejection");
            fileExtensions.Add("onvolumechange");
            fileExtensions.Add("onwaiting");
            fileExtensions.Add("onwebkitanimationiteration");
            fileExtensions.Add("onwheel");
            return fileExtensions;
        }
        public static string GetAntiForgeryToken()
        {
            HttpContext.Current.Request.Cookies.Clear();
            var antiForgery = System.Web.Helpers.AntiForgery.GetHtml().ToString();
            System.Text.RegularExpressions.Match value = System.Text.RegularExpressions.Regex.Match(antiForgery, "(?:value=\")(.*)(?:\")");
            if (value.Success)
            {
                return value.Groups[1].Value;
            }
            return "";
        }
        public static bool CheckSxxEndcodeText(string textValue, bool _check = false) // check = true: cos ma hoa o duoi len
        {
            if (string.IsNullOrEmpty(textValue)) return true;
            try
            {
                if (_check == true)
                {
                    textValue = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(textValue));
                }
                Regex tagRegex = new Regex(@"<[a-z][\s\S]*>");
                Regex tagRegex2 = new Regex(@"<[/s/S][a-z][\s\S]*>");
                if (tagRegex.IsMatch(textValue) || tagRegex2.IsMatch(textValue))
                {
                    return false;
                }
                textValue = textValue.Replace("&amp;", "&")
                                      .Replace("&lt;", "<")
                                      .Replace("&gt;", ">")
                                      .Replace("&quot;", "\"")
                                      .Replace("&#039;", "\'");
                var value2 = textValue;
                foreach (var item in GetNotWhitelistHandler())
                {
                    if (value2.ToUpper().Contains(item.ToUpper()))
                    {
                        return false;
                    }
                }
                var unValue = UnicodeExtensions.convertToUnSign3(value2.Replace("-", "").Replace("–","").Replace("L'oreal",""));
                var safeValue = System.Web.Security.AntiXss.AntiXssEncoder.HtmlEncode(unValue, true);
                if (unValue != safeValue)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}