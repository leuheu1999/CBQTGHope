using Aspose.Cells;
using Business.Entities;
using log4net.Config;
using Spire.Barcode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Module.Framework.Common
{
    //public static class BaseQuery
    //{
    //    public static HttpRequest request;
    //    public static string urlLogin;

    //    //thong tin user
    //    public static CustomPrincipal GetCurrentUser()
    //    {
    //        return System.Web.HttpContext.Current.User as CustomPrincipal;
    //    }
    //    public static bool IsInRight(this IPrincipal a, string right)
    //    {
    //        var user = BaseQuery.GetCurrentUser();
    //        return user != null && (user.Role.Contains(Role.AdminProvider) || (user.Right.Contains(right)));
    //    }
    //    public static UrlViewModel GetDefaultUrlFromPermissionArea()
    //    {
    //        //IUsersBusinessUI userBus = new UsersBusinessUI();
    //        UrlViewModel result = null;
    //        //var user = BaseQuery.GetCurrentUser();
    //        //if (user == null) return result;
    //        //var rights = user.Right.Split(',');
    //        //var model = userBus.GetRights();
    //        //if (model != null)
    //        //{
    //        //    if (model.Count > 0)
    //        //    {
    //        //        var temp = (from r in model
    //        //                    where rights.Contains(r.CodeCookie) && r.IsDefault == true
    //        //                    orderby r.CategoryId ascending, r.OrderIndex ascending
    //        //                    select r).FirstOrDefault();
    //        //        if (temp != null)
    //        //        {
    //        //            result = new UrlViewModel()
    //        //            {
    //        //                Action = temp.Action,
    //        //                Controller = temp.Controller,
    //        //            };
    //        //        }
    //        //    }
    //        //}
    //        return result;
    //    }

    //    public static UrlViewModel GetUrlFromRight(IEnumerable<Guid> rights)
    //    {
    //        var i = rights.ToString();
    //        UrlViewModel result = null;
    //        //IUsersBusinessUI userBus = new UsersBusinessUI();
    //        //var model = userBus.GetRights();
    //        //if (model != null)
    //        //{
    //        //    if (model.Count > 0)
    //        //    {
    //        //        var temp = (from r in model
    //        //                    where rights.Contains(r.RightsId) && r.IsDefault == true
    //        //                    orderby r.CategoryId ascending, r.OrderIndex ascending
    //        //                    select r).FirstOrDefault();
    //        //        if (temp != null)
    //        //        {
    //        //            result = new UrlViewModel()
    //        //            {
    //        //                Action = temp.Action,
    //        //                Controller = temp.Controller,
    //        //            };
    //        //        }
    //        //    }
    //        //}
    //        return result;
    //    }

    //    public static string GetImage(Guid idUser)
    //    {
    //        //IUsersBusinessUI userBus = new UsersBusinessUI();
    //        //if (idUser != Guid.Empty)
    //        //{
    //        //    return userBus.GetImage(idUser);
    //        //}
    //        return null;
    //    }

    //    //Get danh sách Số dòng hiển thị
    //    public static IEnumerable<SelectListItem> GetBinaryPageEntries()
    //    {
    //        return new List<SelectListItem>()
    //               {
    //                   new SelectListItem()
    //                   {
    //                       Text = "10",
    //                       Value = "10"
    //                   },
    //                   new SelectListItem()
    //                   {
    //                       Text = "25",
    //                       Value = "25"
    //                   },
    //                   new SelectListItem()
    //                   {
    //                     Text = "50",
    //                     Value = "50"
    //                   },
    //                   new SelectListItem()
    //                   {
    //                     Text = "100",
    //                     Value = "100"
    //                   },
    //               };
    //    }

    //    //Get danh sách Số dòng hiển thị
    //    public static IEnumerable<SelectListItem> GetTrangThaiCoHoi()
    //    {
    //        return new List<SelectListItem>()
    //               {
    //                   new SelectListItem()
    //                   {
    //                       Text = "Đang báo giá",
    //                       Value = "DANGBAOGIA"
    //                   },
    //                   new SelectListItem()
    //                   {
    //                       Text = "Đã chốt",
    //                       Value = "DACHOT"
    //                   },
    //                   new SelectListItem()
    //                   {
    //                     Text = "Không hợp tác",
    //                     Value = "KHONGHOPTAC"
    //                   }
    //               };
    //    }
    //    //Get danh sách trang thai don hang
    //    public static IEnumerable<SelectListItem> GetTrangThaiDonHang()
    //    {
    //        return new List<SelectListItem>()
    //               {
    //                   new SelectListItem()
    //                   {
    //                       Text = "Mới tạo",
    //                       Value = "MOITAO"
    //                   },
    //                   new SelectListItem()
    //                   {
    //                       Text = "Đã tiếp nhận",
    //                       Value = "DATIEPNHAN"
    //                   },
    //                   new SelectListItem()
    //                   {
    //                     Text = "Đang vận chuyển",
    //                     Value = "DANGVANCHUYEN"
    //                   },
    //                   new SelectListItem()
    //                   {
    //                       Text = "Đến kho tổng",
    //                       Value = "DENKHOTONG"
    //                   },
    //                   new SelectListItem()
    //                   {
    //                       Text = "Chốt giá",
    //                       Value = "CHOTGIA"
    //                   },
    //                   new SelectListItem()
    //                   {
    //                     Text = "Đã giao hàng",
    //                     Value = "DAGIAOHANG"
    //                   },
    //                   new SelectListItem()
    //                   {
    //                     Text = "Hoàn tất",
    //                     Value = "HOANTAT"
    //                   }
    //               };
    //    }
    //    //get danh sach loai hang hoa
    //    public static IEnumerable<SelectListItem> GetLoaiHangHoa()
    //    {
    //        return new List<SelectListItem>()
    //               {
    //                    new SelectListItem()
    //                   {
    //                       Text = "Hàng hóa",
    //                       Value = "HANGHOA"
    //                   },
    //                   new SelectListItem()
    //                   {
    //                       Text = "Hồ sơ",
    //                       Value = "HOSO"
    //                   }
    //               };
    //    }

    //    //get danh sach quý báo cáo
    //    public static IEnumerable<SelectListItem> GetQuyBaoCao()
    //    {
    //        return new List<SelectListItem>()
    //               {
    //                    new SelectListItem()
    //                   {
    //                       Text = "Quý 1",
    //                       Value = "1"
    //                   },
    //                   new SelectListItem()
    //                   {
    //                       Text = "Quý 2",
    //                       Value = "2"
    //                   },
    //                   new SelectListItem()
    //                   {
    //                       Text = "Quý 3",
    //                       Value = "3"
    //                   },
    //                   new SelectListItem()
    //                   {
    //                       Text = "Quý 4",
    //                       Value = "4"
    //                   }
    //               };
    //    }

    //    //get danh sach năm báo cáo
    //    public static IEnumerable<SelectListItem> GetNamBaoCao()
    //    {
    //        return new List<SelectListItem>()
    //        {
    //         new SelectListItem()
    //        {
    //            Text = "2017",
    //            Value = "2017"
    //        },
    //        new SelectListItem()
    //        {
    //            Text = "2018",
    //            Value = "2018"
    //        }
    //    };
    //    }

    //    public static string MD5Hash(string text)
    //    {
    //        FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(text);

    //        string encTicket = authTicket.Name;
    //        return encTicket;
    //    }

    //    public static string GetIP4Address()
    //    {
    //        string IP4Address = String.Empty;
    //        foreach (IPAddress IPA in Dns.GetHostAddresses(System.Web.HttpContext.Current.Request.UserHostAddress))
    //        {
    //            if (IPA.AddressFamily.ToString() == "InterNetwork")
    //            {
    //                IP4Address = IPA.ToString();
    //                break;
    //            }
    //        }
    //        if (IP4Address != String.Empty)
    //        {
    //            return IP4Address;
    //        }
    //        foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
    //        {
    //            if (IPA.AddressFamily.ToString() == "InterNetwork")
    //            {
    //                IP4Address = IPA.ToString();
    //                break;
    //            }
    //        }
    //        return IP4Address;
    //    }

    //    public static string ToBase64BitString(this HttpPostedFileBase target)
    //    {
    //        var binaryData = new Byte[target.InputStream.Length];
    //        long bytesRead = target.InputStream.Read(binaryData, 0,
    //                             (int)target.InputStream.Length);
    //        return System.Convert.ToBase64String(binaryData, 0, binaryData.Length);
    //    }

    //    public static string CreateCode(string table)
    //    {
    //        string result = "";
    //        //ICommonBusinessUI _comBus = new CommonBusinessUI();
    //        //if (table == null) throw new Exception("No Table Index Identity");
    //        //var number = _comBus.GetNumber(table);
    //        //if (number != null)
    //        //{
    //        //    int index = number.IndentityNumber;
    //        //    if (index > 99999999)
    //        //    {
    //        //        _comBus.UpdateNumber(table, 1);
    //        //        index = 1;
    //        //    }
    //        //    else
    //        //    {
    //        //        _comBus.UpdateNumber(table, (index + 1));
    //        //    }
    //        //    result = string.Format("{0}{1}", number.Prefix, index.ToString("d3"));
    //        //}
    //        return result;
    //    }
    //    public static string FormatCurrency(string number, string point)
    //    {
    //        string kq = "";
    //        string dau = "";
    //        //if (number.IndexOf('.') != -1)
    //        //{
    //        //    int digit = int.Parse(number.Substring((number.IndexOf('.') + 1), 1));
    //        //    number = digit >= 5 ? Math.Round(decimal.Parse(number)).ToString() : number.Substring(0, number.IndexOf('.'));
    //        //}
    //        if (!string.IsNullOrEmpty(number))
    //        {
    //            double numberRound = double.Parse(number);
    //            number = Math.Round(numberRound).ToString();
    //            if (number.Substring(0, 1) == "-")
    //            {
    //                dau = "-";
    //                number = number.Substring(1);
    //            }
    //            int length = number.Length;
    //            while (length > 3)
    //            {
    //                kq = point + number.Substring(length - 3, 3) + kq;
    //                length -= 3;
    //            }
    //            kq = dau + number.Substring(0, length) + kq;

    //        }
    //        return kq;
    //    }

    //    public static bool Export2ExcelPdf(string strTemplateFileFullName, string format, DataTable dt, DataTable dtDate, DataTable dtReplace = null)
    //    {
    //        try
    //        {
    //            dt.TableName = "Table0";
    //            dtDate.TableName = "Table1";
    //            //Aspose.Cells.License license = new Aspose.Cells.License();
    //            //license.SetLicense("Aspose.Cells.lic");

    //            //Create WorkbookDesigner object.
    //            var wd = new WorkbookDesigner();

    //            wd.Workbook.Open(strTemplateFileFullName);

    //            var worksheet = wd.Workbook.Worksheets[0];

    //            if (dtReplace != null && dtReplace.Rows.Count == 1)
    //            {
    //                for (var j = 0; j < dtReplace.Columns.Count; j++)
    //                {
    //                    var keyword = "<" + dtReplace.Columns[j].ColumnName + ">";
    //                    var val = dtReplace.Rows[0][j].ToString();
    //                    worksheet.Replace(keyword, val);
    //                }
    //            }
    //            if (dtDate.Rows.Count > 0)
    //            {
    //                worksheet.Replace("$!", "&=[" + dtDate.TableName + "].");
    //                wd.SetDataSource(dtDate);
    //            }
               
    //            if (dt.Rows.Count > 0)
    //            {
    //                worksheet.Replace("$", "&=[" + dt.TableName + "].");
    //            }
    //            //Set the datatable as the data source.
    //            wd.SetDataSource(dt);

    //            //Process the smart markers to fill the data into the worksheets.
    //            wd.Process(true);
    //            wd.Workbook.Worksheets[0].AutoFitRows();
    //            while (wd.Workbook.Worksheets.Count > 1) wd.Workbook.Worksheets.RemoveAt(wd.Workbook.Worksheets.Count - 1);

    //            var f = new FileInfo(strTemplateFileFullName);
    //            //Save the excel file.
    //            var outStream = new MemoryStream();
    //            wd.Workbook.Save(outStream, format.ToLower() == "pdf" ? Aspose.Cells.SaveFormat.Pdf : Aspose.Cells.SaveFormat.Excel97To2003);
    //            var docBytes = outStream.ToArray();
    //            var mimeType = MimeMapping.GetMimeMapping(f.Name.Replace(f.Extension, "." + format));
    //            HttpContext.Current.Response.ContentType = mimeType;
    //            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + f.Name.Replace(f.Extension, DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + "." + format));
    //            HttpContext.Current.Response.BinaryWrite(docBytes);
    //            HttpContext.Current.Response.End();
    //            HttpContext.Current.ApplicationInstance.CompleteRequest();
    //        }
    //        catch (Exception ex)
    //        {
    //            XmlConfigurator.Configure();
    //            return false;
    //        }
    //        return true;
    //    }

    //    public static bool ValidateRoleName(RoleAllViewModel model)
    //    {
    //        //var userService = new UsersServices();
    //        bool result = false;
    //        //var admin = Guid.Parse(CommonConstaints.AdminProvider);
    //        //if (admin != Guid.Empty)
    //        //{
    //        //    var role = userService.GetAllRole(admin);
    //        //    if (role.FirstOrDefault(m => m.RoleName.Equals(model.RoleName) && (m.RoleId != model.RoleId)) != null)
    //        //    {
    //        //        result = true;
    //        //    }
    //        //}
    //        return result;
    //    }
    //    public static string CreateBarcode(string barcode, string folder)
    //    {
    //        if (!Directory.Exists("barcode"))
    //            Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "Content/images/", "barcode"));

    //        if (!Directory.Exists(folder))
    //            Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "Content/images/barcode/", folder));

    //        string barcodeSavePath = AppDomain.CurrentDomain.BaseDirectory + "Content/images/barcode/" + folder + "/" + barcode + ".png";
    //        BarcodeSettings setting = new BarcodeSettings();
    //        setting.Data = barcode;
    //        setting.Data2D = barcode;
    //        setting.Type = BarCodeType.Code128;
    //        setting.ShowTextOnBottom = true;
    //        System.Drawing.Font font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold);
    //        setting.TextFont = font;
    //        BarCodeGenerator bar = new BarCodeGenerator(setting);
    //        System.Drawing.Image image = bar.GenerateImage();
    //        image.Save(barcodeSavePath, ImageFormat.Png);
    //        return barcode + ".png";
    //    }
    //    //get danh sach tiêu chí đánh giá đơn hàng
    //    public static IEnumerable<SelectListItem> GetTieuChiDanhGiaDonHang()
    //    {
    //        return new List<SelectListItem>()
    //        {
    //         new SelectListItem()
    //        {
    //            Text = "Đánh giá chung",
    //            Value = "Đánh giá chung"
    //        },
    //        new SelectListItem()
    //        {
    //            Text = "Thái độ tư vấn viên",
    //            Value = "Thái độ tư vấn viên"
    //        },
    //        new SelectListItem()
    //        {
    //            Text = "Thời gian chuyển hàng",
    //            Value = "Thời gian chuyển hàng"
    //        },
    //        new SelectListItem()
    //        {
    //            Text = "Giao tiếp trong quá trình làm việc",
    //            Value = "Giao tiếp trong quá trình làm việc"
    //        },
    //        new SelectListItem()
    //        {
    //            Text = "Giá cả",
    //            Value = "Giá cả"
    //        }
    //    };
    //    }

    //    //public static IEnumerable<SelectListItem> GetDanhMucCauHoi()
    //    //{
    //    //    var cauTraLoiBusGui = new DM_CauTraLoiBusinessUI();
    //    //    var listCategories = new List<SelectListItem>();
    //    //    var query = cauTraLoiBusGui.GetDMCauHoi(); ;
    //    //    listCategories = query.Select(m => new SelectListItem()
    //    //    {
    //    //        Value = m.Id.ToString(),
    //    //        Text = m.Name
    //    //    }).ToList();
    //    //    return listCategories;
    //    //}

    //    ///// <summary>
    //    ///// danh mục dân tộc
    //    ///// </summary>
    //    ///// <returns></returns>
    //    //public static IEnumerable<SelectListItem> DanhSachDanTocNew()
    //    //{
    //    //    var commonService = new CommonService();
    //    //    return commonService.DanhSachDanToc();
    //    //}
    //    ///// <summary>
    //    ///// danh mục quốc gia
    //    ///// </summary>
    //    ///// <returns></returns>
    //    //public static IEnumerable<SelectListItem> DanhSachQuocGiaNew()
    //    //{
    //    //    var commonService = new CommonService();
    //    //    return commonService.DanhSachQuocGiaNew();
    //    //}

    //    ///// <summary>
    //    ///// danh mục chức vụ
    //    ///// </summary>
    //    ///// <returns></returns>
    //    //public static IEnumerable<SelectListItem> DanhSachChucVuNew()
    //    //{
    //    //    var commonService = new CommonService();
    //    //    return commonService.DanhSachChucVuNew();
    //    //}
    //    ///// <summary>
    //    ///// Danh sách quầy tiếp nhận
    //    ///// </summary>
    //    ///// <returns></returns>
    //    //public static IEnumerable<SelectListItem> DanhSachQuayTiepNhan()
    //    //{
    //    //    var commonService = new CommonService();
    //    //    return commonService.DanhSachQuayTiepNhan();
    //    //}
    //    //public static ThongKeIpad ThongKeIpadDesktop(Guid idUser)
    //    //{
    //    //    var commonService = new CommonService();
    //    //    return commonService.ThongKeIpadDesktop(idUser);
    //    //}
    //    public static IEnumerable<SelectListItem> GetGenders()
    //    {
    //        var list = new List<SelectListItem>();
    //        var item = new SelectListItem { Text = "Nam", Value = "1", Selected = true };
    //        list.Add(item);
    //        item = new SelectListItem { Text = "Nữ", Value = "2" };
    //        list.Add(item);
    //        return list;
    //    }
    //}
}