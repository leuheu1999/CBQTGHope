using Aspose.Cells;
using Aspose.Words;
using Business.Entities.Domain;
using Core.Common.Utilities;
using Module.Framework;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace CMS.Admin.Controllers
{
    public class PrintController : BaseController
    {
        private DungChungServiceClient _DungChungSrv;
        // GET: Print
        #region Export File Excel
        public ActionResult ExportFileExcel(string Param, string Key)
        {
            try
            {
                using (_DungChungSrv = new DungChungServiceClient())
                {
                    string result = string.Empty;
                    var _dt = _DungChungSrv.GetInfoParametersByKeyCode(Key);
                    if (_dt != null && _dt.Data != null && _dt.Data.resultObject != null)
                    {
                        var _dtParam = _dt.Data.resultObject;
                        Print_DataMap model = new Print_DataMap();
                        model.StoreProcedure = _dtParam.Procdures;
                        model.Parameters = Param.ToString();
                        var _result = _DungChungSrv.Print(model);
                        var _data = (_result != null && _result.Data != null && _result.Data.StatusCode == 200 && _result.Data.resultObject != null) ? _result.Data.resultObject : null;
                        if (!string.IsNullOrEmpty(_data))
                        {
                            var pathExport = Server.MapPath(_dtParam.Path);
                            var wbExport = new Workbook();
                            wbExport.Open(pathExport);
                            DataSet Data = JsonConvert.DeserializeObject<DataSet>(_data);
                            var ds = new DataSet("office");
                            ds = Data;

                            int iIndex = _dtParam.RowStart - 1; //start
                            int _countColumns = _dtParam.TotalCoumns; // tổng số columns

                            if (Key == "ExportQTG_QuyenTacGiaList_Excel" || Key == "ExportQLQ_QuyenLienQuanList_Excel")
                            {
                                string[] stringArray = { ".gif", ".jpg", ".jpeg", ".png" };
                                if (ds.Tables.Count > 0)
                                {
                                    int i = 0, j = 0;
                                    DataTable dt = ds.Tables[0];

                                    for (i = 0; i < dt.Rows.Count; i++)
                                    {
                                        var _ColumnsData = dt.Rows[i];
                                        for (j = 0; j < _countColumns; j++)
                                        {
                                            if (_ColumnsData[j].ToString() != "" && Array.Exists(stringArray, e => _ColumnsData[j].ToString().Contains(e)))
                                            {
                                                var path = Path.Combine(Server.MapPath('~' + _ColumnsData[j].ToString()));
                                                if (System.IO.File.Exists(path))
                                                {
                                                    wbExport.Worksheets[0].Cells.SetRowHeight(iIndex, 64);
                                                    wbExport.Worksheets[0].Cells.SetColumnWidth(j, 12);
                                                    wbExport.Worksheets[0].Pictures.Add(iIndex, j, iIndex + 1, j + 1, path);
                                                }
                                            }
                                            else
                                            {
                                                var str = HttpUtility.HtmlDecode(_ColumnsData[j].ToString());
                                                wbExport.Worksheets[0].Cells[iIndex, j].Value = Regex.Replace(str, "<.*?>", String.Empty).Trim();
                                            }
                                        }
                                        iIndex++;
                                    }

                                    if (ds.Tables.Count == 2)
                                    {
                                        var dt2 = ds.Tables[1];
                                        var cells = wbExport.Worksheets[0].Cells;
                                        var opts = new FindOptions();
                                        Cell startcell = null;
                                        for (i = 0; i < dt2.Columns.Count; i++)
                                        {
                                            startcell = null;
                                            do
                                            {
                                                startcell = cells.Find("<<" + dt2.Columns[i] + ">>", startcell, opts);
                                                if (startcell != null)
                                                {

                                                    if (dt2.Rows.Count > 0)
                                                        startcell.Value = dt2.Rows[0][i];
                                                    else
                                                        startcell.Value = "";
                                                }
                                            } while (startcell != null);
                                        }
                                        startcell = null;
                                        do
                                        {
                                            startcell = cells.Find("<<", startcell, opts);
                                            if (startcell != null)
                                            {
                                                startcell.Value = "";
                                            }
                                        } while (startcell != null);
                                    }

                                }
                                CommonHelper.FullStyleBorderExcel(wbExport, _dtParam.StartColumns + (_dtParam.RowStart - 1), _dtParam.EndColumns + (iIndex));
                                var namefile = _dtParam.Description + "_" + string.Format("{0:yyyyMMddhhmmssfff}", DateTime.Now) + ".xlsx";
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    wbExport.Save(ms, Aspose.Cells.SaveFormat.Xlsx);
                                    ms.Position = 0;
                                    byte[] byteBuffer = ms.ToArray();
                                    ms.Close();
                                    result = Convert.ToBase64String(byteBuffer);
                                    byteBuffer = null;
                                }
                                return Json(new { data = result, fileName = namefile }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                if (ds.Tables.Count > 0)
                                {
                                    int i = 0, j = 0;
                                    DataTable dt = ds.Tables[0];

                                    for (i = 0; i < dt.Rows.Count; i++)
                                    {
                                        var _ColumnsData = dt.Rows[i];
                                        for (j = 0; j < _countColumns; j++)
                                        {
                                            var str = HttpUtility.HtmlDecode(_ColumnsData[j].ToString());
                                            wbExport.Worksheets[0].Cells[iIndex, j].Value = Regex.Replace(str, "<.*?>", String.Empty).Trim();
                                        }
                                        iIndex++;
                                    }

                                    if (ds.Tables.Count == 2)
                                    {
                                        var dt2 = ds.Tables[1];
                                        var cells = wbExport.Worksheets[0].Cells;
                                        var opts = new FindOptions();
                                        Cell startcell = null;
                                        for (i = 0; i < dt2.Columns.Count; i++)
                                        {
                                            startcell = null;
                                            do
                                            {
                                                startcell = cells.Find("<<" + dt2.Columns[i] + ">>", startcell, opts);
                                                if (startcell != null)
                                                {

                                                    if (dt2.Rows.Count > 0)
                                                        startcell.Value = dt2.Rows[0][i];
                                                    else
                                                        startcell.Value = "";
                                                }
                                            } while (startcell != null);
                                        }
                                        startcell = null;
                                        do
                                        {
                                            startcell = cells.Find("<<", startcell, opts);
                                            if (startcell != null)
                                            {
                                                startcell.Value = "";
                                            }
                                        } while (startcell != null);
                                    }

                                }
                                CommonHelper.FullStyleBorderExcelAutoFit(wbExport, _dtParam.StartColumns + (_dtParam.RowStart - 1), _dtParam.EndColumns + (iIndex));
                                var namefile = _dtParam.Description + "_" + string.Format("{0:yyyyMMddhhmmssfff}", DateTime.Now) + ".xlsx";
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    wbExport.Save(ms, Aspose.Cells.SaveFormat.Xlsx);
                                    ms.Position = 0;
                                    byte[] byteBuffer = ms.ToArray();
                                    ms.Close();
                                    result = Convert.ToBase64String(byteBuffer);
                                    byteBuffer = null;
                                }
                                return Json(new { data = result, fileName = namefile }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
                return Json(new { data = "", fileName = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = "", fileName = "" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion Export File Excel

        #region Export File Word
        public ActionResult ExportFileWord(string Param, string Key)
        {
            try
            {
                using (_DungChungSrv = new DungChungServiceClient())
                {
                    var _dt = _DungChungSrv.GetInfoParametersByKeyCode(Key);
                    if (_dt != null && _dt.Data != null && _dt.Data.StatusCode == 200 && _dt.Data.resultObject != null)
                    {
                        var _info = _dt.Data.resultObject;
                        Print_DataMap model = new Print_DataMap();
                        model.StoreProcedure = _info.Procdures;
                        model.Parameters = Param.ToString();
                        var _result = _DungChungSrv.Print(model);
                        var _data = (_result != null && _result.Data != null && _result.Data.StatusCode == 200 && _result.Data.resultObject != null) ? _result.Data.resultObject : null;

                        if (!string.IsNullOrEmpty(_data))
                        {
                            DataSet Data = JsonConvert.DeserializeObject<DataSet>(_data);
                            for (int i = 0; i < Data.Tables.Count; i++)
                            {
                                Data.Tables[i].TableName = "Table" + i;
                            }
                            var pathExport = Server.MapPath(_info.Path);
                            var ds = new DataSet("office");
                            ds = Data;
                            var format = _info.TypePrint;
                            var namefile = _info.FileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + "." + format.ToLower();                          
                            var data = WordPdfFile(pathExport, ds, format, _info.FileName);
                            return Json(new { data = data, fileName = namefile }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    return Json(new { data = "", fileName = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { data = "", fileName = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        public string WordPdfFile(string strTemplateFileFullName, DataSet inputDataSet, string format, string name)
        {
            var tabIdx = 1;
            string result = string.Empty;
            const string pattern1 = @"<{2}.*?>{2}";
            const string pattern2 = @"«{1}.*?»{1}";
            string[] stringArray = { ".gif", ".jpg", ".jpeg", ".png" };
            try
            {
                var doc = new Document(strTemplateFileFullName);

                foreach (DataTable table in inputDataSet.Tables)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        foreach (DataColumn col in table.Columns)
                        {
                            var value = table.Rows[i][col].ToString();
                            if (value != "" && Array.Exists(stringArray, e => value.Contains(e)))
                            {
                                var path = Path.Combine(Server.MapPath('~' + value));
                                table.Rows[i][col] = path;
                            }

                        }
                    }
                }

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
                        var isExist = true;
                        do
                        {
                            isExist = dbPr.MoveToMergeField(field);
                            if (isExist)
                                dbPr.Write(value);
                        }
                        //value = ReplaceSpecialChar(value);
                        while (isExist);

                        //doc.Range.Replace("<<" + field + ">>", value, false, false);

                        doc.Range.Replace(new Regex(@"<<" + field + ">>"), new ReplaceWithHtmlEvaluator(value), false);
                    }
                    tabIdx++;
                }

                doc.Range.Replace(new Regex(pattern1), string.Empty);
                doc.Range.Replace(new Regex(pattern2), string.Empty);

                var file = new FileInfo(strTemplateFileFullName);
                var saveOptions = Aspose.Words.Saving.SaveOptions.CreateSaveOptions(format.ToLower() == "pdf" ? Aspose.Words.SaveFormat.Pdf : Aspose.Words.SaveFormat.Docx);

                //var url = "/Portals/_default/Uploads/FileWord" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                //var path = Path.Combine(Server.MapPath('~' + "/" + url));
                //bool isExists = Directory.Exists(path);
                //if (!isExists)
                //    Directory.CreateDirectory(path);
                //var namefile = name + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + "." + format.ToLower();
                //var pathsave = Path.GetDirectoryName(path) + @"\" + namefile;
                //doc.Save(pathsave, saveOptions);
                using (MemoryStream ms = new MemoryStream())
                {
                    doc.Save(ms, saveOptions);
                    ms.Position = 0;
                    byte[] byteBuffer = ms.ToArray();
                    ms.Close();
                    result = Convert.ToBase64String(byteBuffer);
                    byteBuffer = null;
                }
                //return url + namefile;
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #endregion

        public static void CreateCellsFormatting(Workbook workbook, int col1, int col2)
        {
            Aspose.Cells.Style stl0 = workbook.CreateStyle();
            Aspose.Cells.Style stl1 = workbook.CreateStyle();
            // Set a custom shading color of the cells.
            stl0.Borders[Aspose.Cells.BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            stl0.Borders[Aspose.Cells.BorderType.TopBorder].Color = Color.Black;
            stl0.Borders[Aspose.Cells.BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            stl0.Borders[Aspose.Cells.BorderType.LeftBorder].Color = Color.Black;
            stl0.Borders[Aspose.Cells.BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            stl0.Borders[Aspose.Cells.BorderType.BottomBorder].Color = Color.Black;
            stl0.Borders[Aspose.Cells.BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            stl0.Borders[Aspose.Cells.BorderType.RightBorder].Color = Color.Black;

            stl1.Font.IsBold = true;
            stl1.Borders[Aspose.Cells.BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            stl1.Borders[Aspose.Cells.BorderType.TopBorder].Color = Color.Black;
            stl1.Borders[Aspose.Cells.BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            stl1.Borders[Aspose.Cells.BorderType.LeftBorder].Color = Color.Black;
            stl1.Borders[Aspose.Cells.BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            stl1.Borders[Aspose.Cells.BorderType.BottomBorder].Color = Color.Black;
            stl1.Borders[Aspose.Cells.BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            stl1.Borders[Aspose.Cells.BorderType.RightBorder].Color = Color.Black;

            // Define a style flag struct.      
            StyleFlag flag = new StyleFlag();
            flag.FontBold = true;
            flag.Borders = true;
            flag.FontColor = true;

            //Row row = workbook.Worksheets[0].Cells.Rows[cell];
            Aspose.Cells.Range range_0 = workbook.Worksheets[0].Cells.CreateRange("A" + col1.ToString(), "AK" + col2.ToString());
            Aspose.Cells.Range range_1 = workbook.Worksheets[0].Cells.CreateRange("A" + col2.ToString(), "AK" + col2.ToString());
            Aspose.Cells.Range range_2 = workbook.Worksheets[0].Cells.CreateRange("A" + col2.ToString(), "B" + col2.ToString());

            range_0.ApplyStyle(stl0, flag);
            range_1.ApplyStyle(stl1, flag);
            range_2.Merge();
        }

        protected T ColectionItem<T>(string nameRequest)
        {
            try
            {
                var model = Request.Form[nameRequest];
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                };

                var item = JsonConvert.DeserializeObject<T>(model, settings);
                return item;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}