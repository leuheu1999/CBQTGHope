//Contributor : MVCContrib

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
namespace Core.Common.UI.Paging
{
    /// <summary>
    /// Renders a pager component from an IPageableModel datasource.
    /// </summary>
    public partial class Pager : IHtmlString
    {
        protected readonly IPageableModel model;
        protected readonly ViewContext viewContext;
        protected string pageQueryName = "page";
        protected bool showTotalSummary;
        protected bool showPagerItems = true;
        protected bool showFirst = true;
        protected bool showPrevious = true;
        protected bool showNext = true;
        protected bool showLast = true;
        protected bool showIndividualPages = true;
        protected bool renderEmptyParameters = true;
        protected int individualPagesDisplayedCount = 5;
        protected Func<int, string> urlBuilder;
        protected IList<string> booleanParameterNames;

        public Pager(IPageableModel model, ViewContext context)
        {
            this.model = model;
            this.viewContext = context;
            this.urlBuilder = CreateDefaultUrl;
            this.booleanParameterNames = new List<string>();
        }

        protected ViewContext ViewContext
        {
            get { return viewContext; }
        }

        public Pager QueryParam(string value)
        {
            this.pageQueryName = value;
            return this;
        }
        public Pager ShowTotalSummary(bool value)
        {
            this.showTotalSummary = value;
            return this;
        }
        public Pager ShowPagerItems(bool value)
        {
            this.showPagerItems = value;
            return this;
        }
        public Pager ShowFirst(bool value)
        {
            this.showFirst = value;
            return this;
        }
        public Pager ShowPrevious(bool value)
        {
            this.showPrevious = value;
            return this;
        }
        public Pager ShowNext(bool value)
        {
            this.showNext = value;
            return this;
        }
        public Pager ShowLast(bool value)
        {
            this.showLast = value;
            return this;
        }
        public Pager ShowIndividualPages(bool value)
        {
            this.showIndividualPages = value;
            return this;
        }
        public Pager RenderEmptyParameters(bool value)
        {
            this.renderEmptyParameters = value;
            return this;
        }
        public Pager IndividualPagesDisplayedCount(int value)
        {
            this.individualPagesDisplayedCount = value;
            return this;
        }
        public Pager Link(Func<int, string> value)
        {
            this.urlBuilder = value;
            return this;
        }
        //little hack here due to ugly MVC implementation
        //find more info here: http://www.mindstorminteractive.com/topics/jquery-fix-asp-net-mvc-checkbox-truefalse-value/
        public Pager BooleanParameterName(string paramName)
        {
            booleanParameterNames.Add(paramName);
            return this;
        }

        public override string ToString()
        {
            return ToHtmlString();
        }
        public virtual string ToHtmlString()
        {
            if (model.TotalItems == 0)
                return null;
            var links = new StringBuilder();
            if (showTotalSummary && (model.TotalPages > 0))
            {
                links.Append("<li class=\"total-summary\">");
                links.Append(string.Format("Trang {0} {1} ({2} tổng số)", model.PageIndex + 1, model.TotalPages, model.TotalItems));
                links.Append("</li>");
            }
            if (showPagerItems && (model.TotalPages > 1))
            {
                if (showFirst)
                {
                    //first page
                    if ((model.PageIndex >= 3) && (model.TotalPages > individualPagesDisplayedCount))
                    {
                        links.Append(CreatePageLink(1, "<i class='fas fa-fast-backward'></i>", "first-page page-item"));
                    }
                }
                if (showPrevious)
                {
                    //previous page
                    if (model.PageIndex > 0)
                    {
                        links.Append(CreatePageLink(model.PageIndex, "<i class='fas fa-step-backward'></i>", "previous-page page-item"));
                    }
                }
                if (showIndividualPages)
                {
                    //individual pages
                    int firstIndividualPageIndex = GetFirstIndividualPageIndex();
                    int lastIndividualPageIndex = GetLastIndividualPageIndex();
                    for (int i = firstIndividualPageIndex; i <= lastIndividualPageIndex; i++)
                    {
                        if (model.PageIndex == i)
                        {
                            links.AppendFormat("<li class=\"page-item active\"><span class='page-link'>{0}</span></li>", (i + 1));
                        }
                        else
                        {
                            links.Append(CreatePageLink(i + 1, (i + 1).ToString(), "page-item"));
                        }
                    }
                }
                if (showNext)
                {
                    //next page
                    if ((model.PageIndex + 1) < model.TotalPages)
                    {
                        links.Append(CreatePageLink(model.PageIndex + 2, "<i class='fas fa-step-forward'></i>", "next-page page-item"));
                    }
                }
                if (showLast)
                {
                    //last page
                    if (((model.PageIndex + 3) < model.TotalPages) && (model.TotalPages > individualPagesDisplayedCount))
                    {
                        links.Append(CreatePageLink(model.TotalPages, "<i class='fas fa-fast-forward'></i>", "last-page page-item"));
                    }
                }
            }

            var result = links.ToString();
            if (!String.IsNullOrEmpty(result))
            {
                result = "<ul class='pagination justify-content-end'>" + result + "</ul>";
            }
            return result;
        }
        public virtual bool IsEmpty()
        {
            var html = ToString();
            return string.IsNullOrEmpty(html);
        }

        protected virtual int GetFirstIndividualPageIndex()
        {
            if ((model.TotalPages < individualPagesDisplayedCount) ||
                ((model.PageIndex - (individualPagesDisplayedCount / 2)) < 0))
            {
                return 0;
            }
            if ((model.PageIndex + (individualPagesDisplayedCount / 2)) >= model.TotalPages)
            {
                return (model.TotalPages - individualPagesDisplayedCount);
            }
            return (model.PageIndex - (individualPagesDisplayedCount / 2));
        }
        protected virtual int GetLastIndividualPageIndex()
        {
            int num = individualPagesDisplayedCount / 2;
            if ((individualPagesDisplayedCount % 2) == 0)
            {
                num--;
            }
            if ((model.TotalPages < individualPagesDisplayedCount) ||
                ((model.PageIndex + num) >= model.TotalPages))
            {
                return (model.TotalPages - 1);
            }
            if ((model.PageIndex - (individualPagesDisplayedCount / 2)) < 0)
            {
                return (individualPagesDisplayedCount - 1);
            }
            return (model.PageIndex + num);
        }
        protected virtual string CreatePageLink(int pageNumber, string text, string cssClass)
        {
            var liBuilder = new TagBuilder("li");
            if (!String.IsNullOrWhiteSpace(cssClass))
                liBuilder.AddCssClass(cssClass);

            var aBuilder = new TagBuilder("a");
            //aBuilder.SetInnerText(text);
            aBuilder.InnerHtml = text;
            aBuilder.MergeAttribute("href", urlBuilder(pageNumber));
            aBuilder.AddCssClass("page-link");

            liBuilder.InnerHtml += aBuilder;

            return liBuilder.ToString(TagRenderMode.Normal);
        }
        protected virtual string CreateDefaultUrl(int pageNumber)
        {
            var routeValues = new RouteValueDictionary();

            var parametersWithEmptyValues = new List<string>();
            foreach (var key in viewContext.RequestContext.HttpContext.Request.QueryString.AllKeys.Where(key => key != null))
            {
                var value = viewContext.RequestContext.HttpContext.Request.QueryString[key];
                if (renderEmptyParameters && String.IsNullOrEmpty(value))
                {
                    //we store query string parameters with empty values separately
                    //we need to do it because they are not properly processed in the UrlHelper.GenerateUrl method (dropped for some reasons)
                    parametersWithEmptyValues.Add(key);
                }
                else
                {
                    if (booleanParameterNames.Contains(key, StringComparer.InvariantCultureIgnoreCase))
                    {
                        //little hack here due to ugly MVC implementation
                        //find more info here: http://www.mindstorminteractive.com/topics/jquery-fix-asp-net-mvc-checkbox-truefalse-value/
                        if (!String.IsNullOrEmpty(value) && value.Equals("true,false", StringComparison.InvariantCultureIgnoreCase))
                        {
                            value = "true";
                        }
                    }
                    routeValues[key] = value;
                }
            }

            if (pageNumber > 1)
            {
                routeValues[pageQueryName] = pageNumber;
            }
            else
            {
                //SEO. we do not render pageindex query string parameter for the first page
                if (routeValues.ContainsKey(pageQueryName))
                {
                    routeValues.Remove(pageQueryName);
                }
            }

            var url = UrlHelper.GenerateUrl(null, null, null, routeValues, RouteTable.Routes, viewContext.RequestContext, true);
            if (renderEmptyParameters && parametersWithEmptyValues.Any())
            {
                foreach (var key in parametersWithEmptyValues)
                {
                    url = ModifyQueryString(url, key + "=", null);
                }
            }
            return url;
        }
        public virtual string ModifyQueryString(string url, string queryStringModification, string anchor)
        {
            if (url == null)
                url = string.Empty;
            url = url.ToLowerInvariant();

            if (queryStringModification == null)
                queryStringModification = string.Empty;
            queryStringModification = queryStringModification.ToLowerInvariant();

            if (anchor == null)
                anchor = string.Empty;
            anchor = anchor.ToLowerInvariant();


            string str = string.Empty;
            string str2 = string.Empty;
            if (url.Contains("#"))
            {
                str2 = url.Substring(url.IndexOf("#") + 1);
                url = url.Substring(0, url.IndexOf("#"));
            }
            if (url.Contains("?"))
            {
                str = url.Substring(url.IndexOf("?") + 1);
                url = url.Substring(0, url.IndexOf("?"));
            }
            if (!string.IsNullOrEmpty(queryStringModification))
            {
                if (!string.IsNullOrEmpty(str))
                {
                    var dictionary = new Dictionary<string, string>();
                    foreach (string str3 in str.Split(new[] { '&' }))
                    {
                        if (!string.IsNullOrEmpty(str3))
                        {
                            string[] strArray = str3.Split(new[] { '=' });
                            if (strArray.Length == 2)
                            {
                                if (!dictionary.ContainsKey(strArray[0]))
                                {
                                    //do not add value if it already exists
                                    //two the same query parameters? theoretically it's not possible.
                                    //but MVC has some ugly implementation for checkboxes and we can have two values
                                    //find more info here: http://www.mindstorminteractive.com/topics/jquery-fix-asp-net-mvc-checkbox-truefalse-value/
                                    //we do this validation just to ensure that the first one is not overridden
                                    dictionary[strArray[0]] = strArray[1];
                                }
                            }
                            else
                            {
                                dictionary[str3] = null;
                            }
                        }
                    }
                    foreach (string str4 in queryStringModification.Split(new[] { '&' }))
                    {
                        if (!string.IsNullOrEmpty(str4))
                        {
                            string[] strArray2 = str4.Split(new[] { '=' });
                            if (strArray2.Length == 2)
                            {
                                dictionary[strArray2[0]] = strArray2[1];
                            }
                            else
                            {
                                dictionary[str4] = null;
                            }
                        }
                    }
                    var builder = new StringBuilder();
                    foreach (string str5 in dictionary.Keys)
                    {
                        if (builder.Length > 0)
                        {
                            builder.Append("&");
                        }
                        builder.Append(str5);
                        if (dictionary[str5] != null)
                        {
                            builder.Append("=");
                            builder.Append(dictionary[str5]);
                        }
                    }
                    str = builder.ToString();
                }
                else
                {
                    str = queryStringModification;
                }
            }
            if (!string.IsNullOrEmpty(anchor))
            {
                str2 = anchor;
            }
            return (url + (string.IsNullOrEmpty(str) ? "" : ("?" + str)) + (string.IsNullOrEmpty(str2) ? "" : ("#" + str2))).ToLowerInvariant();
        }
    }
}