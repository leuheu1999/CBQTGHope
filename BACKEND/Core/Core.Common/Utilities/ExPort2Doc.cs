using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Aspose.Words;
using Module.Framework;
namespace Core.Common.Utilities
{
    public static class ExPort2Doc
    {
        public static bool Export2DocPdf(string strTemplateFileFullName, string format, DataTable dt, DataTable dtReplace = null)
        {
            var col = "";
            try
            {
                dt.TableName = "Table0";
                var doc = new Document(strTemplateFileFullName);
                doc.MailMerge.ExecuteWithRegions(dt);
                if (dtReplace != null)
                {
                    if ((dtReplace.Rows.Count > 0))
                    {
                        var r = dtReplace.Rows[0];
                        for (var i = 0; i <= dtReplace.Columns.Count - 1; i++)
                        {

                                col = dtReplace.Columns[i].ColumnName;
                                doc.Range.Replace("<<" + col + ">>", (ReferenceEquals(r[i], DBNull.Value) ? "" : r[i].ToString()), false, false);
                            
                                doc.MailMerge.Execute(new[] { col }, new object[] { (ReferenceEquals(r[i], DBNull.Value) ? "" : r[i].ToString()) });
                           
                        }
                    }
                }
                var f = new FileInfo(strTemplateFileFullName);
                var saveOptions = Aspose.Words.Saving.SaveOptions.CreateSaveOptions(format.ToLower() == "pdf" ? SaveFormat.Pdf : SaveFormat.Doc);

                var outStream = new MemoryStream();
                doc.Save(outStream, saveOptions);
                var docBytes = outStream.ToArray();
                var mimeType = MimeMapping.GetMimeMapping(f.Name.Replace(f.Extension, "." + format));

                HttpContext.Current.Response.ContentType = mimeType;
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + f.Name.Replace(f.Extension, "." + format));
                HttpContext.Current.Response.BinaryWrite(docBytes);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {                
                return false;
            }
            return true;
        }

        public static void ExecuteFull(string strTemplateFileFullName, DataSet inputDataSet, bool pdf)
        {
            var tabIdx = 1;

            const string pattern = @"<{2}.*?>{2}";
            const string pattern2 = @"«{1}.*?»{1}";
            try
            {
                var doc = new Document(strTemplateFileFullName);

                doc.MailMerge.ExecuteWithRegions(inputDataSet);

                var dbPr = new DocumentBuilder(doc);
                foreach (DataTable table in inputDataSet.Tables)
                {
                    if (table.Rows.Count <= 0 || table.Columns.Count <= 0)
                    {
                        var fields = doc.Range.Fields;
                        var tableTemp = table;
                        foreach (var field in from object f in fields
                                              select f.GetType().GetProperty("Result").GetValue(f, null).ToString() into fieldName
                                              select fieldName.Replace("»", string.Empty)
                                                  .Replace("«", string.Empty)
                                                  .Replace(">>", string.Empty)
                                                  .Replace("<<", string.Empty) into field
                                              where !field.Contains("TableStart") && !field.Contains("TableEnd")
                                              where !tableTemp.Columns.Contains(field)
                                              select field)
                        {
                            table.Columns.Add(field);
                        }

                        var emptyRow = table.NewRow();
                        table.Rows.InsertAt(emptyRow, 0);
                    }

                    if (string.IsNullOrEmpty(table.TableName))
                        table.TableName = "Table" + tabIdx;

                    tabIdx++;
                }

                foreach (DataTable table in inputDataSet.Tables)
                {
                    foreach (DataColumn col in table.Columns)
                    {
                        var field = col.ColumnName;
                        var value = table.Rows[0][col].ToString();

                        //value = ReplaceSpecialChar(value);
                        if (dbPr.MoveToMergeField(field))
                            dbPr.Write(value);
                        //doc.Range.Replace("<<" + field + ">>", value, false, false);

                        doc.Range.Replace(new Regex(@"<<" + field + ">>"), new ReplaceWithHtmlEvaluator(value), false);
                    }
                    tabIdx++;
                }

                doc.Range.Replace(new Regex(pattern), string.Empty);
                doc.Range.Replace(new Regex(pattern2), string.Empty);

                var file = new FileInfo(strTemplateFileFullName);
                var saveOptions = Aspose.Words.Saving.SaveOptions.CreateSaveOptions(pdf ? SaveFormat.Pdf : SaveFormat.Doc);

                var outStream = new MemoryStream();
                doc.Save(outStream, saveOptions);
                var docBytes = outStream.ToArray();
                var mimeType = MimeMapping.GetMimeMapping(file.Name.Replace(file.Extension, pdf ? ".pdf" : ".doc"));

                HttpContext.Current.Response.ContentType = mimeType;
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name.Replace(file.Extension, pdf ? ".pdf" : ".doc"));
                HttpContext.Current.Response.BinaryWrite(docBytes);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
               
            }
        }

        public static byte[] ExecuteBytesFull(string strTemplateFileFullName, DataSet inputDataSet, bool pdf)
        {
            var tabIdx = 1;

            const string pattern = @"<{2}.*?>{2}";
            const string pattern2 = @"«{1}.*?»{1}";
            try
            {
                var doc = new Document(strTemplateFileFullName);

                doc.MailMerge.ExecuteWithRegions(inputDataSet);

                var dbPr = new DocumentBuilder(doc);
                foreach (DataTable table in inputDataSet.Tables)
                {
                    if (table.Rows.Count <= 0 || table.Columns.Count <= 0)
                    {
                        var fields = doc.Range.Fields;
                        var tableTemp = table;
                        foreach (var field in from object f in fields
                                              select f.GetType().GetProperty("Result").GetValue(f, null).ToString() into fieldName
                                              select fieldName.Replace("»", string.Empty)
                                                  .Replace("«", string.Empty)
                                                  .Replace(">>", string.Empty)
                                                  .Replace("<<", string.Empty) into field
                                              where !field.Contains("TableStart") && !field.Contains("TableEnd")
                                              where !tableTemp.Columns.Contains(field)
                                              select field)
                        {
                            table.Columns.Add(field);
                        }

                        var emptyRow = table.NewRow();
                        table.Rows.InsertAt(emptyRow, 0);
                    }

                    if (string.IsNullOrEmpty(table.TableName))
                        table.TableName = "Table" + tabIdx;

                    tabIdx++;
                }

                foreach (DataTable table in inputDataSet.Tables)
                {
                    foreach (DataColumn col in table.Columns)
                    {
                        var field = col.ColumnName;
                        var value = table.Rows[0][col].ToString();

                        //value = ReplaceSpecialChar(value);
                        if (dbPr.MoveToMergeField(field))
                            dbPr.Write(value);
                        //doc.Range.Replace("<<" + field + ">>", value, false, false);

                        doc.Range.Replace(new Regex(@"<<" + field + ">>"), new ReplaceWithHtmlEvaluator(value), false);
                        doc.Range.Replace("True", "x", true, true);
                        doc.Range.Replace("False", "", true, true);
                    }
                    tabIdx++;
                }

                doc.Range.Replace(new Regex(pattern), string.Empty);
                doc.Range.Replace(new Regex(pattern2), string.Empty);

                var saveOptions = Aspose.Words.Saving.SaveOptions.CreateSaveOptions(pdf ? SaveFormat.Pdf : SaveFormat.Doc);

                var outStream = new MemoryStream();
                doc.Save(outStream, saveOptions);
                doc.Clone();
                return outStream.ToArray();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

    public class NoReplaceEvaluator : IReplacingCallback
    {
        ReplaceAction IReplacingCallback.Replacing(ReplacingArgs e)
        {
            e.Replacement = e.Match.ToString();
            return ReplaceAction.Replace;
        }
    }

    public class ReplaceEvaluator : IReplacingCallback
    {
        ReplaceAction IReplacingCallback.Replacing(ReplacingArgs e)
        {
            var index1 = e.Match.ToString().IndexOf(">", StringComparison.Ordinal);
            var index2 = e.Match.ToString().LastIndexOf("<", StringComparison.Ordinal);
            var strReplaced = e.Match.ToString().Substring(index1 + 1, index2 - (index1 + 1));
            e.Replacement = strReplaced;
            return ReplaceAction.Replace;
        }
    }

    public class ReplaceWithHtmlEvaluator : IReplacingCallback
    {
        private readonly string _value;

        public ReplaceWithHtmlEvaluator(string value)
        {
            _value = value;
        }

        ReplaceAction IReplacingCallback.Replacing(ReplacingArgs e)
        {
            DocumentBuilder builder = new DocumentBuilder((Document)e.MatchNode.Document);
            builder.MoveTo(e.MatchNode);
            builder.InsertHtml(_value);
            e.Replacement = "";
            return ReplaceAction.Replace;
        }
    }
}

