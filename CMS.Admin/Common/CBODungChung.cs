using Business.Entities;
using Business.Entities.Domain;
using Core.Common.Utilities;
using Microsoft.Ajax.Utilities;
using Module.Framework;
using Module.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace CMS.Admin.Common
{
    public static class CBODungChung
    {
        public static IEnumerable<SelectListItem> GetCBOQuyen()
        {
            DungChungServiceClient _serviceDungChung = new DungChungServiceClient();


            var listDonVi = new List<SelectListItem>();
            var model = new CBO_DungChungParam();
            model.TableName = "DM_Quyen";
            var query = _serviceDungChung.CBO_DungChung_GetAllMater(model);
            if (query != null && query.resultObject != null && query.resultObject.Any())
            {
                listDonVi = query.resultObject.Select(m => new SelectListItem()
                {
                    Value = m.Ma1.ToString(),
                    Text = m.Ten1
                }).ToList();
            }
            return listDonVi;
        }
        public static IEnumerable<SelectListItem> GetAllController()
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            var controlleractionlist = asm.GetTypes()
                    .Where(type => typeof(System.Web.Mvc.Controller).IsAssignableFrom(type))
                    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                    .Select(x => new { Controller = x.DeclaringType.Name, Action = x.Name, ReturnType = x.ReturnType.Name, Attributes = String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))) })
                    .OrderBy(x => x.Controller).ThenBy(x => x.Action).GroupBy(x => x.Controller).ToList();
            var listController = new List<SelectListItem>();
            foreach (var item in controlleractionlist)
            {
                var itemSelect = new SelectListItem()
                {
                    Value = item.Key,
                    Text = item.Key
                };
                listController.Add(itemSelect);
            }
            return listController;
        }
        public static IEnumerable<SelectListItem> GetAllAction(string controller)
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            var controlleractionlist = asm.GetTypes()
                    .Where(type => typeof(System.Web.Mvc.Controller).IsAssignableFrom(type))
                    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                    .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                    .Select(x => new { Controller = x.DeclaringType.Name, Action = x.Name, ReturnType = x.ReturnType.Name, Attributes = String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))) })
                    .OrderBy(x => x.Controller).ThenBy(x => x.Action).Where(x => x.Controller == controller).ToList();
            var listController = new List<SelectListItem>();
            foreach (var item in controlleractionlist)
            {
                var itemSelect = new SelectListItem()
                {
                    Value = item.Action,
                    Text = item.Action
                };
                listController.Add(itemSelect);
            }
            return listController;
        }
        public static IEnumerable<SelectListItem> GetCBOQuocGia()
        {
            DungChungServiceClient _serviceDungChung = new DungChungServiceClient();
            var listQuocGia = new List<SelectListItem>();
            var model = new CBO_DungChungParam();
            model.TableName = "DM_QUOCGIA";
            var query = _serviceDungChung.CBO_DungChung_GetAll(model);
            if (query != null && query.resultObject != null && query.resultObject.Any())
            {
                listQuocGia = query.resultObject.Select(m => new SelectListItem()
                {
                    Value = m.Ma1.ToString(),
                    Text = m.Ten1
                }).ToList();
            }
            return listQuocGia;
        }
        public static IEnumerable<SelectListItem> GetCBOTinhThanh()
        {
            DungChungServiceClient _serviceDungChung = new DungChungServiceClient();
            var listTinhThanh = new List<SelectListItem>();
            var model = new CBO_DungChungParam();
            model.TableName = "DM_TINHTHANH";
            var query = _serviceDungChung.CBO_DungChung_GetAll(model);
            if (query != null && query.resultObject != null && query.resultObject.Any())
            {
                listTinhThanh = query.resultObject.Select(m => new SelectListItem()
                {
                    Value = m.Ma1.ToString(),
                    Text = m.Ten1
                }).ToList();
            }
            return listTinhThanh;
        }

        public static IEnumerable<SelectListItem> GetCBOQuanHuyen(string id)
        {
            DungChungServiceClient _serviceDungChung = new DungChungServiceClient();
            var listQuanHuyen = new List<SelectListItem>();
            var model = new CBO_DungChungParam();
            model.TableName = "DM_QUANHUYEN";
            if (!string.IsNullOrEmpty(id))
            {
                model.ParentID1 = id;
            }
            var query = _serviceDungChung.CBO_DungChung_GetAll(model);
            if (query != null && query.resultObject != null && query.resultObject.Any())
            {
                listQuanHuyen = query.resultObject.Select(m => new SelectListItem()
                {
                    Value = m.Ma1.ToString(),
                    Text = m.Ten1
                }).ToList();
            }
            return listQuanHuyen;
        }
        public static IEnumerable<SelectListItem> GetCBOPhuongXa(string id)
        {
            DungChungServiceClient _serviceDungChung = new DungChungServiceClient();
            var listQuanHuyen = new List<SelectListItem>();
            var model = new CBO_DungChungParam();
            model.TableName = "DM_PHUONGXA";
            if (!string.IsNullOrEmpty(id))
            {
                model.ParentID1 = id;
            }
            var query = _serviceDungChung.CBO_DungChung_GetAllMater(model);
            if (query != null && query.resultObject != null && query.resultObject.Any())
            {
                listQuanHuyen = query.resultObject.Select(m => new SelectListItem()
                {
                    Value = m.Ma1.ToString(),
                    Text = m.Ten1
                }).ToList();
            }
            return listQuanHuyen;
        }
        public static IEnumerable<SelectListItem> GetCBOLogLevel()
        {
            DungChungServiceClient _serviceDungChung = new DungChungServiceClient();
            var list = new List<SelectListItem>();
            var model = new CBO_DungChungParam();
            model.TableName = "LogLevel";
            var query = _serviceDungChung.CBO_DungChung_GetAll(model);
            if (query != null && query.resultObject != null && query.resultObject.Any())
            {
                list = query.resultObject.Select(m => new SelectListItem()
                {
                    Value = m.Ma1.ToString(),
                    Text = m.Ten1
                }).ToList();
            }
            return list;
        }
        public static IEnumerable<SelectListItem> GetCBOLogSort()
        {
            DungChungServiceClient _serviceDungChung = new DungChungServiceClient();
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem
            {
                Value = "1",
                Text = "Ngày tạo mới nhất"
            });
            list.Add(new SelectListItem
            {
                Value = "2",
                Text = "Ngày tạo cũ nhất"
            });
            return list;
        }
        public static IEnumerable<SelectListItem> GetCBONhomQuyen()
        {
            DungChungServiceClient _serviceDungChung = new DungChungServiceClient();
            var list = new List<SelectListItem>();
            var model = new CBO_DungChungParam();
            model.TableName = "NhomQuyen";
            var query = _serviceDungChung.CBO_DungChung_GetAllMater(model);
            if (query != null && query.resultObject != null && query.resultObject.Any())
            {
                list = query.resultObject.Select(m => new SelectListItem()
                {
                    Value = m.Ma1.ToString(),
                    Text = m.Ten1
                }).ToList();
            }
            return list;
        }
        public static IEnumerable<SelectListItem> GetCBOLoaiTaiKhoan()
        {
            DungChungServiceClient _serviceDungChung = new DungChungServiceClient();
            var list = new List<SelectListItem>();
            var model = new CBO_DungChungParam();
            model.TableName = "LoaiTaiKhoan";
            var query = _serviceDungChung.CBO_DungChung_GetAll(model);
            if (query != null && query.resultObject != null && query.resultObject.Any())
            {
                list = query.resultObject.Select(m => new SelectListItem()
                {
                    Value = m.Ma1.ToString(),
                    Text = m.Ten1
                }).ToList();
            }
            return list;
        }
        public static IEnumerable<SelectListItem> GetCBOLogType()
        {
            DungChungServiceClient _serviceDungChung = new DungChungServiceClient();
            var list = new List<SelectListItem>();
            var model = new CBO_DungChungParam();
            model.TableName = "LogTypeND";
            var query = _serviceDungChung.CBO_DungChung_GetAllMater(model);
            if (query != null && query.resultObject != null && query.resultObject.Any())
            {
                list = query.resultObject.Select(m => new SelectListItem()
                {
                    Value = m.Ma1.ToString(),
                    Text = m.Ten1
                }).ToList();
            }
            return list;
        }
        public static IEnumerable<SelectListItem> GetCBOTrangThai()
        {
            DungChungServiceClient _serviceDungChung = new DungChungServiceClient();
            var list = new List<SelectListItem>();
            var model = new CBO_DungChungParam();
            model.TableName = "E_TrangThai";
            var query = _serviceDungChung.CBO_DungChung_GetAll(model);
            if (query != null && query.resultObject != null && query.resultObject.Any())
            {
                list = query.resultObject.Select(m => new SelectListItem()
                {
                    Value = m.Ma1.ToString(),
                    Text = m.Ten1
                }).ToList();
            }
            return list;
        }
        public static IEnumerable<SelectListItem> GetCBODungChung(CBO_DungChungParam model)
        {
            DungChungServiceClient _serviceDungChung = new DungChungServiceClient();
            var list = new List<SelectListItem>();
            var query = _serviceDungChung.CBO_DungChung_GetAll(model);
            if (query != null && query.resultObject != null && query.resultObject.Any())
            {
                list = query.resultObject.Select(m => new SelectListItem()
                {
                    Value = m.Ma1.ToString(),
                    Text = m.Ten1
                }).ToList();
            }
            return list;
        }
        public static IEnumerable<SelectListItem> GetCBODungChungMaster(CBO_DungChungParam model)
        {
            DungChungServiceClient _serviceDungChung = new DungChungServiceClient();
            var list = new List<SelectListItem>();
            var query = _serviceDungChung.CBO_DungChung_GetAllMater(model);
            if (query != null && query.resultObject != null && query.resultObject.Any())
            {
                list = query.resultObject.Select(m => new SelectListItem()
                {
                    Value = m.Ma1.ToString(),
                    Text = m.Ten1
                }).ToList();
            }
            return list;
        }
        public static IEnumerable<SelectListItem> GetKhuVucPhamVi()
        {
            DungChungServiceClient _serviceDungChung = new DungChungServiceClient();
            var listQuanHuyen = new List<SelectListItem>();
            var model = new CBO_DungChungParam();
            model.TableName = "DM_QUANHUYEN";

            model.ParentID1 = "49";

            var query = _serviceDungChung.CBO_DungChung_GetAll(model);
            if (query != null && query.resultObject != null && query.resultObject.Any())
            {
                listQuanHuyen = query.resultObject.Select(m => new SelectListItem()
                {
                    Value = m.Ma1.ToString(),
                    Text = m.Ten1
                }).ToList();
            }
            return listQuanHuyen;
        }

        public static IEnumerable<SelectListItem> GetCBOViTriLienKetHinhAnh()
        {
            DungChungServiceClient _serviceDungChung = new DungChungServiceClient();
            var list = new List<SelectListItem>();
            var model = new CBO_DungChungParam();
            model.TableName = "E_ViTriLienKetHinhAnh";
            var query = _serviceDungChung.CBO_DungChung_GetAll(model);
            if (query != null && query.resultObject != null && query.resultObject.Any())
            {
                list = query.resultObject.Select(m => new SelectListItem()
                {
                    Value = m.Ma1.ToString(),
                    Text = m.Ten1
                }).ToList();
            }
            return list;
        }
        public static IEnumerable<SelectListItem> GetCBOLoaiLienKetWebsite()
        {
            DungChungServiceClient _serviceDungChung = new DungChungServiceClient();
            var list = new List<SelectListItem>();
            var model = new CBO_DungChungParam();
            model.TableName = "TT_LoaiLienKetWebsite";
            var query = _serviceDungChung.CBO_DungChung_GetAll(model);
            if (query != null && query.resultObject != null && query.resultObject.Any())
            {
                list = query.resultObject.Select(m => new SelectListItem()
                {
                    Value = m.Ma1.ToString(),
                    Text = m.Ten1
                }).ToList();
            }
            return list;
        }
        public static IEnumerable<SelectListItem> GetCBOLoaiNoiDungThongBao()
        {
            DungChungServiceClient _serviceDungChung = new DungChungServiceClient();
            var list = new List<SelectListItem>();
            var model = new CBO_DungChungParam();
            model.TableName = "TT_LoaiNoiDungThongBao";
            var query = _serviceDungChung.CBO_DungChung_GetAll(model);
            if (query != null && query.resultObject != null && query.resultObject.Any())
            {
                list = query.resultObject.Select(m => new SelectListItem()
                {
                    Value = m.Ma1.ToString(),
                    Text = m.Ten1
                }).ToList();
            }
            return list;
        }

        public static IEnumerable<SelectListItem> GetAllCBOTableDM(string tableName)
        {
            DungChungServiceClient _serviceDungChung = new DungChungServiceClient();
            var list = new List<SelectListItem>();
            var listDonVi = new List<SelectListItem>();
            var model = new CBO_DungChungParam();
            model.TableName = tableName;
            var query = _serviceDungChung.CBO_DungChung_GetAll(model);
            if (query != null && query.resultObject != null && query.resultObject.Any())
            {
                listDonVi = query.resultObject.Select(m => new SelectListItem()
                {
                    Value = m.Ma1.ToString(),
                    Text = m.Ten1
                }).ToList();
            }
            return listDonVi;
        }
        public static IEnumerable<SelectListItem> GetAllMasterCBOTableDM(string tableName)
        {
            DungChungServiceClient _serviceDungChung = new DungChungServiceClient();
            var list = new List<SelectListItem>();
            var listResult = new List<SelectListItem>();
            var model = new CBO_DungChungParam();
            model.TableName = tableName;
            var query = _serviceDungChung.CBO_DungChung_GetAllMater(model);
            if (query != null && query.resultObject != null && query.resultObject.Any())
            {
                listResult = query.resultObject.Select(m => new SelectListItem()
                {
                    Value = m.Ma1.ToString(),
                    Text = m.Ten1
                }).ToList();
            }
            return listResult;
        }


        public static IEnumerable<SelectListItem> GetCBOTarget()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem
            {
                Value = "_blank",
                Text = "_blank"
            });
            list.Add(new SelectListItem
            {
                Value = "_parent",
                Text = "_parent"
            });
            list.Add(new SelectListItem
            {
                Value = "_self",
                Text = "_self"
            });
            list.Add(new SelectListItem
            {
                Value = "_top",
                Text = "_top"
            });
            return list;
        }
        public static IEnumerable<SelectListItem> GetListVideoType()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem
            {
                Value = "0",
                Text = "Youtube"
            });
            list.Add(new SelectListItem
            {
                Value = "1",
                Text = "FaceBook"
            });
            return list;
        }

        public static IEnumerable<SelectListItem> GetCBOLinhVuc_1Cua()
        {
            DungChung_1CuaServiceClient _serviceDungChung = new DungChung_1CuaServiceClient();
            var list = new List<SelectListItem>();
            //var model = new CBO_DungChungParam();
            //model.TableName = "DM_TINHTHANH";
            var user = LoginManager.GetCurrentUser();
            var query = _serviceDungChung.GetDMLinhVucThuTuc(new DM_LinhVucThuTuc_1CuaParam()
            {
                TableName = "LINHVUC_USER",
                DonviID = user.DonViID.ToString(),
                DieuKien = user.UserPortalID.ToString()
            });
            if (query.Data != null && query.Data.Any())
            {
                list = query.Data.Select(m => new SelectListItem()
                {
                    Value = m.Ma,
                    Text = m.Ten
                }).ToList();
            }
            return list;
        }

        public static IEnumerable<SelectListItem> GetCBOThuTuc_1Cua(string id)
        {
            DungChung_1CuaServiceClient _serviceDungChung = new DungChung_1CuaServiceClient();
            var list = new List<SelectListItem>();
            var user = LoginManager.GetCurrentUser();
            var query = _serviceDungChung.GetDMLinhVucThuTuc(new DM_LinhVucThuTuc_1CuaParam()
            {
                TableName = "THUTUCHANHCHINH_USED",
                DonviID = user.DonViID.ToString(),
                DieuKien = id
            });
            if (query.Data != null && query.Data.Any())
            {
                list = query.Data.Select(m => new SelectListItem()
                {
                    Value = m.Ma,
                    Text = m.Ten
                }).ToList();
            }
            return list;
        }

        public static IEnumerable<SelectListItem> GetCBOTinhTrangPhieuTrinh_1Cua()
        {
            DungChung_1CuaServiceClient _serviceDungChung = new DungChung_1CuaServiceClient();
            var list = new List<SelectListItem>();
            var model = new DM_DungChung_1Cuaparam();
            model.TableName = "DM_TINHTRANGPHIEUTRINH";
            var query = _serviceDungChung.GetDanhMuc(model);
            if (query.Data != null && query.Data.Any())
            {
                list = query.Data.Select(m => new SelectListItem()
                {
                    Value = m.Ma,
                    Text = m.Ten
                }).ToList();
            }
            return list;
        }
        public static IEnumerable<SelectListItem> GetCBOTenBuocKeTiep_1Cua(string LinhVucID, string ThuTucID, string ChucNangHienTai, string LoaiXuLy)
        {
            DungChung_1CuaServiceClient _serviceDungChung = new DungChung_1CuaServiceClient();
            var list = new List<SelectListItem>();
            var model = new DM_TenBuocKeTiep_1CuaParam()
            {
                LinhVucID = LinhVucID,
                ThuTucHanhChinhID = ThuTucID,
                ChucNangHienTai = ChucNangHienTai,
                LoaiXuLy = LoaiXuLy
            };

            var query = _serviceDungChung.GetTenBuocKeTiep(model);
            if (query.Data != null && query.Data.Any())
            {
                list = query.Data.Select(m => new SelectListItem()
                {
                    Value = m.Ma,
                    Text = m.Ten
                }).ToList();
            }
            return list;
        }
        public static IEnumerable<SelectListItem> GetCBONguoiNhan_1Cua()
        {
            DungChung_1CuaServiceClient _serviceDungChung = new DungChung_1CuaServiceClient();
            var list = new List<SelectListItem>();
            var model = new DM_DungChung_1Cuaparam();
            model.TableName = "DM_NGUOINHAN";
            var query = _serviceDungChung.GetDanhMuc(model);
            if (query.Data != null && query.Data.Any())
            {
                list = query.Data.Select(m => new SelectListItem()
                {
                    Value = m.Ma,
                    Text = m.Ten
                }).ToList();
            }
            return list;
        }
        public static IEnumerable<SelectListItem> GetCBOTinhTrang_1Cua()
        {
            DungChung_1CuaServiceClient _serviceDungChung = new DungChung_1CuaServiceClient();
            var list = new List<SelectListItem>();
            var model = new DM_DungChung_1Cuaparam();
            model.TableName = "DM_TRANGTHAI";
            var query = _serviceDungChung.GetDanhMuc(model);
            if (query.Data != null && query.Data.Any())
            {
                list = query.Data.Select(m => new SelectListItem()
                {
                    Value = m.Ma,
                    Text = m.Ten
                }).ToList();
            }
            return list;
        }

        public static IEnumerable<SelectListItem> GetCBOTinhThanh_1Cua()
        {
            DungChung_1CuaServiceClient _serviceDungChung = new DungChung_1CuaServiceClient();
            var list = new List<SelectListItem>();
            var query = _serviceDungChung.GetDMTinhThanh();
            if (query.Data != null && query.Data.Any())
            {
                list = query.Data.Select(m => new SelectListItem()
                {
                    Value = m.TinhThanhID.ToString(),
                    Text = m.TenTinhThanh
                }).ToList();
            }
            return list;
        }

        public static IEnumerable<SelectListItem> GetCBOQuanHuyen_1Cua(string id)
        {
            DungChung_1CuaServiceClient _serviceDungChung = new DungChung_1CuaServiceClient();
            var list = new List<SelectListItem>();
            var query = _serviceDungChung.GetDMQuanHuyen(id);
            if (query.Data != null && query.Data.Any())
            {
                list = query.Data.Select(m => new SelectListItem()
                {
                    Value = m.QuanHuyenID.ToString(),
                    Text = m.TenQuanHuyen
                }).ToList();
            }
            return list;
        }
        public static IEnumerable<SelectListItem> GetCBOPhuongXa_1Cua(string id)
        {
            DungChung_1CuaServiceClient _serviceDungChung = new DungChung_1CuaServiceClient();
            var list = new List<SelectListItem>();
            var query = _serviceDungChung.GetDMPhuongXa(id);
            if (query.Data != null && query.Data.Any())
            {
                list = query.Data.Select(m => new SelectListItem()
                {
                    Value = m.PhuongXaID.ToString(),
                    Text = m.TenPhuongXa
                }).ToList();
            }
            return list;
        }

        public static IEnumerable<SelectListItem> GetCBOLoaiSo(string LoaiNghiepVuID)
        {
            DungChungServiceClient _serviceDungChung = new DungChungServiceClient();
            var list = new List<SelectListItem>();
            var model = new CBO_DungChungParam();
            model.TableName = "DM_LoaiSo";
            model.ParentID1 = LoaiNghiepVuID;
            var query = _serviceDungChung.CBO_DungChung_GetAll(model);
            if (query != null && query.resultObject != null && query.resultObject.Any())
            {
                list = query.resultObject.Select(m => new SelectListItem()
                {
                    Value = m.Ma1.ToString(),
                    Text = m.Ten1
                }).ToList();
            }
            return list;
        }
        public static IEnumerable<SelectListItem> GetCBOCoQuanCap()
        {
            DungChungServiceClient _serviceDungChung = new DungChungServiceClient();
            var list = new List<SelectListItem>();
            var model = new CBO_DungChungParam();
            model.TableName = "DM_CoQuanCap";
            var query = _serviceDungChung.CBO_DungChung_GetAll(model);
            if (query != null && query.resultObject != null && query.resultObject.Any())
            {
                list = query.resultObject.Select(m => new SelectListItem()
                {
                    Value = m.Ma1.ToString(),
                    Text = m.Ten1
                }).ToList();
            }
            return list;
        }
        public static IEnumerable<SelectListItem> GetCBONguoiKy()
        {
            DungChungServiceClient _serviceDungChung = new DungChungServiceClient();
            var list = new List<SelectListItem>();
            var model = new CBO_DungChungParam();
            model.TableName = "DM_NguoiKy";
            var query = _serviceDungChung.CBO_DungChung_GetAll(model);
            if (query != null && query.resultObject != null && query.resultObject.Any())
            {
                list = query.resultObject.Select(m => new SelectListItem()
                {
                    Value = m.Ma1.ToString(),
                    Text = m.Ten1
                }).ToList();
            }
            return list;
        }

        public static IEnumerable<SelectListItem> GetCBOKyBaoCao()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "1",
                Text = "Năm"

            });
            list.Add(new SelectListItem()
            {
                Value = "2",
                Text = "Quý"

            });
            list.Add(new SelectListItem()
            {
                Value = "3",
                Text = "Tháng"

            });
            list.Add(new SelectListItem()
            {
                Value = "4",
                Text = "Ngày"

            });
            return list;
        }
        public static IEnumerable<SelectListItem> GetCBONam()
        {
            var list = new List<SelectListItem>();
            var year = DateTime.Today.Year;
            list.Add(new SelectListItem()
            {
                Value = year.ToString(),
                Text = "Năm " + year.ToString()

            });
            for (var i = 1; i <= 3; i++)
            {
                var y = year - i;
                list.Add(new SelectListItem()
                {
                    Value = y.ToString(),
                    Text = "Năm " + y.ToString()

                });
            }
            return list;
        }
        public static IEnumerable<SelectListItem> GetCBOQuy()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "1",
                Text = "Quý 1"

            });
            list.Add(new SelectListItem()
            {
                Value = "2",
                Text = "Quý 2"

            });
            list.Add(new SelectListItem()
            {
                Value = "3",
                Text = "Quý 3"

            });
            list.Add(new SelectListItem()
            {
                Value = "4",
                Text = "Quý 4"

            });
            return list;
        }
        public static IEnumerable<SelectListItem> GetCBOThang()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "1",
                Text = "Tháng 1"

            });
            list.Add(new SelectListItem()
            {
                Value = "2",
                Text = "Tháng 2"

            });
            list.Add(new SelectListItem()
            {
                Value = "3",
                Text = "Tháng 3"

            });
            list.Add(new SelectListItem()
            {
                Value = "4",
                Text = "Tháng 4"

            });
            list.Add(new SelectListItem()
            {
                Value = "5",
                Text = "Tháng 5"

            });
            list.Add(new SelectListItem()
            {
                Value = "6",
                Text = "Tháng 6"

            });
            list.Add(new SelectListItem()
            {
                Value = "7",
                Text = "Tháng 7"

            });
            list.Add(new SelectListItem()
            {
                Value = "8",
                Text = "Tháng 8"

            });
            list.Add(new SelectListItem()
            {
                Value = "9",
                Text = "Tháng 9"

            });
            list.Add(new SelectListItem()
            {
                Value = "10",
                Text = "Tháng 10"

            });
            list.Add(new SelectListItem()
            {
                Value = "11",
                Text = "Tháng 11"

            });
            list.Add(new SelectListItem()
            {
                Value = "12",
                Text = "Tháng 12"

            });
            return list;
        }
        public static IEnumerable<SelectListItem> GetCBOLoaiHinhTacPham()
        {
            DungChungServiceClient _serviceDungChung = new DungChungServiceClient();
            var list = new List<SelectListItem>();
            var model = new CBO_DungChungParam();
            model.TableName = "DM_LoaiHinhTacPham";
            var query = _serviceDungChung.CBO_DungChung_GetAll(model);
            if (query != null && query.resultObject != null && query.resultObject.Any())
            {
                list = query.resultObject.Select(m => new SelectListItem()
                {
                    Value = m.Ma1.ToString(),
                    Text = m.Ten1
                }).ToList();
            }
            return list;
        }
        public static IEnumerable<SelectListItem> GetCBOVungMien()
        {
            DungChungServiceClient _serviceDungChung = new DungChungServiceClient();
            var list = new List<SelectListItem>();
            var model = new CBO_DungChungParam();
            model.TableName = "DM_VungMien";
            var query = _serviceDungChung.CBO_DungChung_GetAll(model);
            if (query != null && query.resultObject != null && query.resultObject.Any())
            {
                list = query.resultObject.Select(m => new SelectListItem()
                {
                    Value = m.Ma1.ToString(),
                    Text = m.Ten1
                }).ToList();
            }
            return list;
        }
        public static IEnumerable<SelectListItem> GetCBOBCQuyen()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "1",
                Text = "Quyền tác giả"

            });
            list.Add(new SelectListItem()
            {
                Value = "2",
                Text = "Quyền liên quan"

            });
            return list;
        }
        public static IEnumerable<SelectListItem> GetCBOBCLoaiThuTuc()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "1",
                Text = "Cấp mới"

            });
            list.Add(new SelectListItem()
            {
                Value = "2",
                Text = "Cấp lại"

            });
            list.Add(new SelectListItem()
            {
                Value = "3",
                Text = "Thu hồi"

            });
            list.Add(new SelectListItem()
            {
                Value = "4",
                Text = "Chuyển chủ sở hữu"

            });
            list.Add(new SelectListItem()
            {
                Value = "5",
                Text = "Cấp đổi"

            });
            return list;
        }

        public static IEnumerable<SelectListItem> GetCBOLoaiHinhTacPham(string id)
        {
            DungChungServiceClient _serviceDungChung = new DungChungServiceClient();
            var listData = new List<SelectListItem>();
            var model = new CBO_DungChungParam();
            model.TableName = "DM_LOAIHINHTACPHAM";
            if (!string.IsNullOrEmpty(id))
            {
                model.ParentID1 = id;
            }
            var query = _serviceDungChung.CBO_DungChung_GetAll(model);
            if (query != null && query.resultObject != null && query.resultObject.Any())
            {
                listData = GenLoaiHinhTacPham(query.resultObject);
            }
            return listData;
        }

        private static List<SelectListItem> GenLoaiHinhTacPham(List<CBO_DungChungViewModel> options)
        {
            var list = new List<SelectListItem>();            
            var lstParent = options.Where(n => n.ParentID == null)
                                   .Select(m => new CBO_DungChungViewModel()
                                   {
                                       Ma1 = m.Ma1.ToString(),
                                       Ten1 = m.Ten1,
                                   }).ToList();

            foreach (var item in lstParent)
            {
                var listItem = new SelectListItem()
                {
                    Value = item.Ma1,
                    Text = item.Ten1,
                };
                list.Add(listItem);
                var lstChild = options.Where(n => n.ParentID == item.Ma1)
                                      .Select(m => new SelectListItem()
                                      {
                                          Value = m.Ma1,
                                          Text = " - " + m.Ten1
                                      }).ToList();
                list.AddRange(lstChild);
            }
            return list;
        }
    }
}