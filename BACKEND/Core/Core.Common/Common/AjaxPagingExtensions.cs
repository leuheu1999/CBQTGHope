using System;
using System.Web.Mvc;
using System.Web.UI;

namespace Core.Common.Common
{
    public static class AjaxPagingExtensions
    {
        public static string Pager(this AjaxHelper ajaxHelper, Options options)
        {
            var divRow = new TagBuilder("div");
            divRow.AddCssClass("mt-3 form-row mr-0 ml-0");
            var divCol3 = new TagBuilder("div");
            divCol3.AddCssClass("col-md-3");
            var divCol2 = new TagBuilder("div");
            divCol2.AddCssClass("col-md-2");
            var divCol7 = new TagBuilder("div");
            divCol7.AddCssClass("col-md-7 text-right");
            //Create the new label.
            //var div = new TagBuilder("div");
            //div.AddCssClass("pagination pagination-normal pagination-right");

            string showing = string.Empty;
            string pagesize = string.Empty;
            var ul = new TagBuilder("ul");
            ul.AddCssClass("pagination justify-content-end");
            //var totalPage = options.TotalItemCount <= options.PageSize ? 1 : options.TotalItemCount % options.PageSize == 0 ? options.TotalItemCount / options.PageSize : options.TotalItemCount / options.PageSize + 1;
            int totalPage;
            if (options.TotalItemCount % options.PageSize == 0)
            {
                totalPage = options.TotalItemCount / options.PageSize;
            }
            else
            {
                totalPage = options.TotalItemCount / options.PageSize + 1;
            }
            if (options.LimitPage != null)
            {
                int from;
                int to;
                if (totalPage - options.CurrentPage >= options.LimitPage / 2)
                {
                    if (options.CurrentPage > options.LimitPage.Value / 2)
                    {
                        from = options.CurrentPage - (options.LimitPage.Value / 2);
                        to = (options.LimitPage.Value - (options.LimitPage.Value / 2)) + options.CurrentPage;
                    }
                    else
                    {
                        from = 1;
                        to = options.LimitPage.Value + 1;
                    }

                }
                else
                {
                    from = totalPage - options.LimitPage.Value + 1;
                    to = totalPage + 1;
                }
                if (from < 1) from = 1;
                if (to > totalPage) to = totalPage + 1;

                if (options.IsShowFirstLast && from > 1)
                {
                    var link = String.Format(options.Link, 1);
                    var onclick = !string.IsNullOrEmpty(options.OnClick) ? String.Format(options.OnClick, 1) : "";
                    var isActive = 1 == options.CurrentPage ? "active" : "";
                    ul.InnerHtml += String.Format("<li class='page-item {0}'><a href='{1}' data-page='1' onclick='{2}'>{3}</a></li>", isActive, link, onclick, "<span class='fa fa-angle-double-left'></span>");
                }
                //1 page thi khong hien thi
                if (to > 2)
                {
                    for (var i = from; i < to; i++)
                    {
                        var li = new TagBuilder("li");
                        var isActive = i == options.CurrentPage ? "active" : "";
                        var link = String.Format(options.Link, i);
                        var onclick = !string.IsNullOrEmpty(options.OnClick) ? String.Format(options.OnClick, i) : "";
                        ul.InnerHtml += String.Format("<li class='page-item {0}'><a href='{1}' data-page='{3}' onclick='{2}'>{3}</a></li>", isActive, link, onclick, i);
                    }
                }

                if (options.IsShowFirstLast && to - 1 < totalPage)
                {
                    var link = String.Format(options.Link, totalPage);
                    var onclick = !string.IsNullOrEmpty(options.OnClick) ? String.Format(options.OnClick, totalPage) : "";
                    var isActive = totalPage == options.CurrentPage ? "active" : "";
                    ul.InnerHtml += String.Format("<li class='page-item {0}'><a href='{1}' data-page='{4}' onclick='{2}'>{3}</a></li>", isActive, link, onclick, "<span class='fa fa-angle-double-right'></span>", totalPage);
                }

                //Showing 21 to 30 of 57 entries
                if (options.CurrentPage > 0 && options.PageSize > 0)
                {
                    showing = String.Format("<span>Hiển thị {0} đến {1} của {2} kết quả </span>", (((options.CurrentPage - 1) * options.PageSize) + 1), (options.CurrentPage * options.PageSize) > options.TotalItemCount ? options.TotalItemCount : (options.CurrentPage * options.PageSize), options.TotalItemCount);
                  
                }

                var onchange = !string.IsNullOrEmpty(options.OnChangePageSize) ? String.Format(options.OnChangePageSize) : "";
                pagesize = String.Format("<span>Số dòng hiển thị: </span> <select id='set-page-size' onchange='{0}' class='custom-select custom-select-sm form-control form-control-sm' style='width: 80px; font-size: 13px;'>", onchange);
                var arrPage = new int[] { 10, 20, 50, 100 };
                for (var i = 0; i < arrPage.Length; i++)
                {

                    pagesize += String.Format("<option value='{0}' {1}>{0}</option>", arrPage[i], arrPage[i] == options.PageSize ? "selected":"");
                }
                pagesize += String.Format("</select>");

            }
            divCol3.InnerHtml = showing.ToString();
            divCol2.InnerHtml = pagesize.ToString();
            divCol7.InnerHtml = ul.ToString();
            //divRow.InnerHtml = divCol5.ToString() + divCol7.ToString();
            divRow.InnerHtml = divCol3.ToString() + divCol2.ToString() + divCol7.ToString();
            return divRow.ToString();
        }
    }
    public class Options
    {
        public string ActionName;
        public string ControllerName;
        public int CurrentPage;
        public int PageSize;
        public int TotalItemCount;
        public string Link = "";
        public string OnClick = "";
        public string OnChangePageSize = "";
        public Options() { }
        public int? LimitPage = 5;
        public string CssClass { get; set; }
        public bool IsShowControls { get; set; }
        public bool IsShowFirstLast { get; set; }
        public bool IsShowPages { get; set; }


    }
}
