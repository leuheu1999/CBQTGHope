using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Configuration;

namespace Core.Common.Utilities
{
    public class XssAttributes : ValidationAttribute
    {
        private bool _check = false;
        private bool _isHTML = false;
        public XssAttributes(bool check = false,bool isHTML = false)
            : base("{0} Lỗi đầu vào.")
        {
            _check = check;
            _isHTML = isHTML;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            var textValue = value.ToString();
            if (!string.IsNullOrEmpty(textValue))
            {
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
                        return new ValidationResult("Lỗi html");
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
                            var errorMessage = FormatErrorMessage((validationContext.DisplayName));
                            return new ValidationResult(errorMessage);
                        }
                    }
                    if (!_isHTML)
                    {
                        var unValue = UnicodeExtensions.convertToUnSign3(value2.Replace("-",""));
                        var safeValue = System.Web.Security.AntiXss.AntiXssEncoder.HtmlEncode(unValue,true);
                        if (unValue != safeValue)
                        {
                            var errorMessage = FormatErrorMessage((validationContext.DisplayName));
                            return new ValidationResult(errorMessage);
                        }
                    }
                }
                catch (Exception)
                {
                    return new ValidationResult("Lỗi");
                }

            }
            return ValidationResult.Success;
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
    }
    public static class UnicodeExtensions
    {
        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}

