using Aspose.Cells;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web.Hosting;
using System.Collections.Generic;
using System.Web;

namespace Core.Common.Utilities
{
    public static class CommonHelper
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            email = email.Trim();
            var result = Regex.IsMatch(email, "^(?:[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+\\.)*[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\\.)){0,61}[a-zA-Z0-9]?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\\[(?:(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\.){3}(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\]))$", RegexOptions.IgnoreCase);
            return result;
        }

        public static bool IsValidIpAddress(string ipAddress)
        {
            IPAddress ip;
            return IPAddress.TryParse(ipAddress, out ip);
        }

        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            string str = string.Empty;
            for (int i = 0; i < length; i++)
                str = string.Concat(str, random.Next(10).ToString());
            return str;
        }

        public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }

        public static string ConvertEnum(string str)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;
            string result = string.Empty;
            foreach (var c in str)
                if (c.ToString() != c.ToString().ToLower())
                    result += " " + c;
                else
                    result += c.ToString();
            result = result.TrimStart();
            return result;
        }

        public static int GetDifferenceInYears(DateTime startDate, DateTime endDate)
        {
            int age = endDate.Year - startDate.Year;
            if (startDate > endDate.AddYears(-age))
                age--;
            return age;
        }

        public static string MapPath(string path)
        {
            if (HostingEnvironment.IsHosted)
            {
                return HostingEnvironment.MapPath(path);
            }

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
            return Path.Combine(baseDirectory, path);
        }

        public static void FullStyleBorderExcel(Workbook wbExport, string col1, string col2)
        {
            string[] _Colum = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            Worksheet worksheet = wbExport.Worksheets[0];

            string _keyEnd = col2.Substring(0, 1);
            int _end = Array.IndexOf(_Colum, _keyEnd);

            Aspose.Cells.Range rangevp = worksheet.Cells.CreateRange(col1, col2);

            Aspose.Cells.Style stl = wbExport.Styles[wbExport.Styles.Add()];
            stl.Font.IsBold = true;
            stl.Borders[Aspose.Cells.BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            stl.Borders[Aspose.Cells.BorderType.TopBorder].Color = Color.Black;
            stl.Borders[Aspose.Cells.BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            stl.Borders[Aspose.Cells.BorderType.LeftBorder].Color = Color.Black;
            stl.Borders[Aspose.Cells.BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            stl.Borders[Aspose.Cells.BorderType.BottomBorder].Color = Color.Black;
            stl.Borders[Aspose.Cells.BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            stl.Borders[Aspose.Cells.BorderType.RightBorder].Color = Color.Black;

            StyleFlag flg = new StyleFlag();
            flg.Borders = true;
            rangevp.ApplyStyle(stl, flg);

        }
        public static void FullStyleBorderExcelAutoFit(Workbook wbExport, string col1, string col2)
        {
            string[] _Colum = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            Worksheet worksheet = wbExport.Worksheets[0];

            string _keyEnd = col2.Substring(0, 1);
            int _end = Array.IndexOf(_Colum, _keyEnd);

            Aspose.Cells.Range rangevp = worksheet.Cells.CreateRange(col1, col2);
            worksheet.AutoFitColumns();
            worksheet.AutoFitRows();

            Aspose.Cells.Style stl = wbExport.Styles[wbExport.Styles.Add()];
            stl.Font.IsBold = true;
            stl.Borders[Aspose.Cells.BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            stl.Borders[Aspose.Cells.BorderType.TopBorder].Color = Color.Black;
            stl.Borders[Aspose.Cells.BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            stl.Borders[Aspose.Cells.BorderType.LeftBorder].Color = Color.Black;
            stl.Borders[Aspose.Cells.BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            stl.Borders[Aspose.Cells.BorderType.BottomBorder].Color = Color.Black;
            stl.Borders[Aspose.Cells.BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            stl.Borders[Aspose.Cells.BorderType.RightBorder].Color = Color.Black;

            StyleFlag flg = new StyleFlag();
            flg.Borders = true;
            rangevp.ApplyStyle(stl, flg);

        }
        public static void FullStyleBorderWithBoldExcel(Workbook wbExport, string col1, string col2)
        {
            string[] _Colum = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            Worksheet worksheet = wbExport.Worksheets[0];

            string _keyEnd = col2.Substring(0, 1);
            int _end = Array.IndexOf(_Colum, _keyEnd);

            Aspose.Cells.Range range_0 = wbExport.Worksheets[0].Cells.CreateRange(col1, col2);

            Aspose.Cells.Style stl0 = wbExport.Styles[wbExport.Styles.Add()];
            stl0.Font.IsBold = true;
            stl0.Borders[Aspose.Cells.BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            stl0.Borders[Aspose.Cells.BorderType.TopBorder].Color = Color.Black;
            stl0.Borders[Aspose.Cells.BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            stl0.Borders[Aspose.Cells.BorderType.LeftBorder].Color = Color.Black;
            stl0.Borders[Aspose.Cells.BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            stl0.Borders[Aspose.Cells.BorderType.BottomBorder].Color = Color.Black;
            stl0.Borders[Aspose.Cells.BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            stl0.Borders[Aspose.Cells.BorderType.RightBorder].Color = Color.Black;

            StyleFlag flg = new StyleFlag();
            flg.FontBold = true;
            flg.Borders = true;
            range_0.ApplyStyle(stl0, flg);
        }
        public static string GetBodyMail(string inputMail, dynamic dt)
        {
            string str = string.Empty;
            if (!string.IsNullOrEmpty(inputMail))
            {
                dt.GetType().GetProperties();
                foreach (var prop in dt.GetType().GetProperties())
                {
                    var field = prop.Name;
                    var value = prop.GetValue(dt);
                    if (inputMail.Contains(@"#" + field))
                    {
                        str = inputMail.Replace(@"#" + field, value.ToString());
                        inputMail = str;
                    }
                }
                str = Regex.Replace(inputMail, @"\#\w+", "", RegexOptions.Multiline);
            }
            return HttpUtility.HtmlDecode(str);
        }
        public static string UnescapeHtml(string str)
        {
            str = str.Replace("&amp;", "&")
                .Replace("&lt;", "<")
                .Replace("&gt;", ">")
                .Replace("&quot;", "\"")
                .Replace("&#039;", "'");
            return str;
        }
    }
}
