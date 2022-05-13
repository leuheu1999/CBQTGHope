using System.Web.Mvc;
namespace Core.Common.UI
{
    /// <summary>
    /// Layout extensions
    /// </summary>
    public static class LayoutExtensions
    {
        private static readonly IPageHeadBuilder pageHeadBuilder = NinjectFactory.Get<IPageHeadBuilder>();
        /// <summary>
        /// Add title element to the <![CDATA[<head>]]>
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">Title part</param>
        public static void AddTitleParts(this HtmlHelper html, string part)
        {
            pageHeadBuilder.AddTitleParts(part);
        }
        /// <summary>
        /// Append title element to the <![CDATA[<head>]]>
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">Title part</param>
        public static void AppendTitleParts(this HtmlHelper html, string part)
        {
            pageHeadBuilder.AppendTitleParts(part);
        }
        /// <summary>
        /// Generate all title parts
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="addDefaultTitle">A value indicating whether to insert a default title</param>
        /// <param name="part">Title part</param>
        /// <returns>Generated string</returns>
        public static MvcHtmlString NopTitle(this HtmlHelper html, bool addDefaultTitle = true, string part = "")
        {
            html.AppendTitleParts(part);
            return MvcHtmlString.Create(html.Encode(pageHeadBuilder.GenerateTitle(addDefaultTitle)));
        }


        /// <summary>
        /// Add meta description element to the <![CDATA[<head>]]>
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">Meta description part</param>
        public static void AddMetaDescriptionParts(this HtmlHelper html, string part)
        {
            pageHeadBuilder.AddMetaDescriptionParts(part);
        }
        /// <summary>
        /// Append meta description element to the <![CDATA[<head>]]>
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">Meta description part</param>
        public static void AppendMetaDescriptionParts(this HtmlHelper html, string part)
        {
            pageHeadBuilder.AppendMetaDescriptionParts(part);
        }
        /// <summary>
        /// Generate all description parts
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">Meta description part</param>
        /// <returns>Generated string</returns>
        public static MvcHtmlString NopMetaDescription(this HtmlHelper html, string part = "")
        {
            html.AppendMetaDescriptionParts(part);
            return MvcHtmlString.Create(html.Encode(pageHeadBuilder.GenerateMetaDescription()));
        }


        /// <summary>
        /// Add meta keyword element to the <![CDATA[<head>]]>
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">Meta keyword part</param>
        public static void AddMetaKeywordParts(this HtmlHelper html, string part)
        {
            pageHeadBuilder.AddMetaKeywordParts(part);
        }
        /// <summary>
        /// Append meta keyword element to the <![CDATA[<head>]]>
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">Meta keyword part</param>
        public static void AppendMetaKeywordParts(this HtmlHelper html, string part)
        {
           pageHeadBuilder.AppendMetaKeywordParts(part);
        }
        /// <summary>
        /// Generate all keyword parts
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">Meta keyword part</param>
        /// <returns>Generated string</returns>
        public static MvcHtmlString NopMetaKeywords(this HtmlHelper html, string part = "")
        {
            html.AppendMetaKeywordParts(part);
            return MvcHtmlString.Create(html.Encode(pageHeadBuilder.GenerateMetaKeywords()));
        }


        /// <summary>
        /// Add script element
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">Script part</param>
        /// <param name="excludeFromBundle">A value indicating whether to exclude this script from bundling</param>
        /// <param name="isAsync">A value indicating whether to add an attribute "async" or not for js files</param>
        public static void AddScriptParts(this HtmlHelper html, string part, bool excludeFromBundle = false, bool isAsync = false)
        {
            AddScriptParts(html, ResourceLocation.Head, part, excludeFromBundle, isAsync);
        }
        /// <summary>
        /// Add script element
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="location">A location of the script element</param>
        /// <param name="part">Script part</param>
        /// <param name="excludeFromBundle">A value indicating whether to exclude this script from bundling</param>
        /// <param name="isAsync">A value indicating whether to add an attribute "async" or not for js files</param>
        public static void AddScriptParts(this HtmlHelper html, ResourceLocation location, string part, bool excludeFromBundle = false, bool isAsync = false)
        {
            pageHeadBuilder.AddScriptParts(location, part, excludeFromBundle, isAsync);
        }
        /// <summary>
        /// Append script element
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">Script part</param>
        /// <param name="excludeFromBundle">A value indicating whether to exclude this script from bundling</param>
        /// <param name="isAsync">A value indicating whether to add an attribute "async" or not for js files</param>
        public static void AppendScriptParts(this HtmlHelper html, string part, bool excludeFromBundle = false, bool isAsync = false)
        {
            AppendScriptParts(html, ResourceLocation.Head, part, excludeFromBundle, isAsync);
        }
        /// <summary>
        /// Append script element
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="location">A location of the script element</param>
        /// <param name="part">Script part</param>
        /// <param name="excludeFromBundle">A value indicating whether to exclude this script from bundling</param>
        /// <param name="isAsync">A value indicating whether to add an attribute "async" or not for js files</param>
        public static void AppendScriptParts(this HtmlHelper html, ResourceLocation location, string part, bool excludeFromBundle = false, bool isAsync = false)
        {
            pageHeadBuilder.AppendScriptParts(location, part, excludeFromBundle, isAsync);
        }

        /// <summary>
        /// Add CSS element
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">CSS part</param>
        /// <param name="excludeFromBundle">A value indicating whether to exclude this script from bundling</param>
        public static void AddCssFileParts(this HtmlHelper html, string part, bool excludeFromBundle = false)
        {
            AddCssFileParts(html, ResourceLocation.Head, part, excludeFromBundle);
        }
        /// <summary>
        /// Add CSS element
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="location">A location of the script element</param>
        /// <param name="part">CSS part</param>
        /// <param name="excludeFromBundle">A value indicating whether to exclude this script from bundling</param>
        public static void AddCssFileParts(this HtmlHelper html, ResourceLocation location, string part, bool excludeFromBundle = false)
        {
            pageHeadBuilder.AddCssFileParts(location, part, excludeFromBundle);
        }
        /// <summary>
        /// Append CSS element
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">CSS part</param>
        /// <param name="excludeFromBundle">A value indicating whether to exclude this script from bundling</param>
        public static void AppendCssFileParts(this HtmlHelper html, string part, bool excludeFromBundle = false)
        {
            AppendCssFileParts(html, ResourceLocation.Head, part, excludeFromBundle);
        }
        /// <summary>
        /// Append CSS element
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="location">A location of the script element</param>
        /// <param name="part">CSS part</param>
        /// <param name="excludeFromBundle">A value indicating whether to exclude this script from bundling</param>
        public static void AppendCssFileParts(this HtmlHelper html, ResourceLocation location, string part, bool excludeFromBundle = false)
        {
            pageHeadBuilder.AppendCssFileParts(location, part, excludeFromBundle);
        }

        /// <summary>
        /// Add canonical URL element to the <![CDATA[<head>]]>
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">Canonical URL part</param>
        public static void AddCanonicalUrlParts(this HtmlHelper html, string part)
        {
            pageHeadBuilder.AddCanonicalUrlParts(part);
        }
        /// <summary>
        /// Append canonical URL element to the <![CDATA[<head>]]>
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">Canonical URL part</param>
        public static void AppendCanonicalUrlParts(this HtmlHelper html, string part)
        {
            pageHeadBuilder.AppendCanonicalUrlParts(part);
        }
        /// <summary>
        /// Generate all canonical URL parts
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">Canonical URL part</param>
        /// <returns>Generated string</returns>
        public static MvcHtmlString NopCanonicalUrls(this HtmlHelper html, string part = "")
        {
            html.AppendCanonicalUrlParts(part);
            return MvcHtmlString.Create(pageHeadBuilder.GenerateCanonicalUrls());
        }


        /// <summary>
        /// Add any custom element to the <![CDATA[<head>]]> element
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">The entire element. For example, <![CDATA[<meta name="msvalidate.01" content="123121231231313123123" />]]></param>
        public static void AddHeadCustomParts(this HtmlHelper html, string part)
        {
            pageHeadBuilder.AddHeadCustomParts(part);
        }
        /// <summary>
        /// Append any custom element to the <![CDATA[<head>]]> element
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">The entire element. For example, <![CDATA[<meta name="msvalidate.01" content="123121231231313123123" />]]></param>
        public static void AppendHeadCustomParts(this HtmlHelper html, string part)
        {
            pageHeadBuilder.AppendHeadCustomParts(part);
        }
        /// <summary>
        /// Generate all custom elements
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <returns>Generated string</returns>
        public static MvcHtmlString NopHeadCustom(this HtmlHelper html)
        {
            return MvcHtmlString.Create(pageHeadBuilder.GenerateHeadCustom());
        }


        /// <summary>
        /// Add CSS class to the <![CDATA[<head>]]> element
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">CSS class</param>
        public static void AddPageCssClassParts(this HtmlHelper html, string part)
        {
            pageHeadBuilder.AddPageCssClassParts(part);
        }
        /// <summary>
        /// Append CSS class to the <![CDATA[<head>]]> element
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">CSS class</param>
        public static void AppendPageCssClassParts(this HtmlHelper html, string part)
        {
            pageHeadBuilder.AppendPageCssClassParts(part);
        }
        /// <summary>
        /// Generate all title parts
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="part">CSS class</param>
        /// <param name="includeClassElement">A value indicating whether to include "class" attributes</param>
        /// <returns>Generated string</returns>
        public static MvcHtmlString NopPageCssClasses(this HtmlHelper html, string part = "", bool includeClassElement = true)
        {
            html.AppendPageCssClassParts(part);
            var classes = pageHeadBuilder.GeneratePageCssClasses();

            if (string.IsNullOrEmpty(classes))
                return null;

            var result = includeClassElement ? string.Format("class=\"{0}\"", classes) : classes;
            return MvcHtmlString.Create(result);
        }


        /// <summary>
        /// Specify system name of admin menu item that should be selected (expanded)
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <param name="systemName">System name</param>
        public static void SetActiveMenuItemSystemName(this HtmlHelper html, string systemName)
        {
            pageHeadBuilder.SetActiveMenuItemSystemName(systemName);
        }
        /// <summary>
        /// Get system name of admin menu item that should be selected (expanded)
        /// </summary>
        /// <param name="html">HTML helper</param>
        /// <returns>System name</returns>
        public static string GetActiveMenuItemSystemName(this HtmlHelper html)
        {
            return pageHeadBuilder.GetActiveMenuItemSystemName();
        }
    }
}
