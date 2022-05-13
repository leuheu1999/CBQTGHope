using Business.Entities;
using Business.Entities.Domain;
using Business.Entities.Domain.Print;
using Core.Common.Utilities;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Business.Services.Interfaces
{
    [ServiceContract]
    public interface IDungChungService
    {

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DC/CBO_DungChung_GetAll")]
        ResultResponse<List<CBO_DungChungViewModel>> CBO_DungChung_GetAll(CBO_DungChungParam model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DC/CBO_DungChung_GetAllMater")]
        ResultResponse<List<CBO_DungChungViewModel>> CBO_DungChung_GetAllMater(CBO_DungChungParam model);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
          UriTemplate = "DC/GetKeyCauHinhHeThong?Key={Key}")]
        ResultResponse<string> GetKeyCauHinhHeThong(string Key);
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
          UriTemplate = "DC/GetKeyDMDonVi?Key={Key}")]
        ResultResponse<string> GetKeyDMDonVi(string Key);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
         UriTemplate = "DC/DisposeCache?Key={Key}")]
        ResultResponse<bool> DisposeCache(string Key);

        #region DM_QuocGia       
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
               UriTemplate = "DM/DM_QuocGia_List")]
        ResultResponse<List<DM_QuocGiaMap>> DM_QuocGia_List(DM_QuocGiaMapParam model);
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                      UriTemplate = "DM/GetQuocGiaById?id={id}")]
        ResultResponse<DM_QuocGiaMapAdd> GetQuocGiaById(long id);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
                       UriTemplate = "DM/DM_QuocGia_InsUpdate")]
        ResultResponse<long> DM_QuocGia_InsUpdate(DM_QuocGiaMapAdd model);
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                             UriTemplate = "DM/DM_QuocGia_Del?id={id}&lastupduserid={lastupduserid}")]
        ResultResponse<int> DM_QuocGia_Del(long id, Guid lastupduserid);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                              UriTemplate = "DM/GetQuocGiaByMa?ma={ma}")]
        ResultResponse<DM_QuocGiaMapAdd> GetQuocGiaByMa(string ma);
        #endregion

        #region DM_TinhThanh       
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
               UriTemplate = "DM/DM_TinhThanh_List")]
        ResultResponse<List<DM_TinhThanhMap>> DM_TinhThanh_List(DM_TinhThanhMapParam model);
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                      UriTemplate = "DM/DM_TinhThanh_GetById?tinhThanhID={tinhThanhID}")]
        ResultResponse<DM_TinhThanhMapAdd> DM_TinhThanh_GetById(long tinhThanhID);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
                       UriTemplate = "DM/DM_TinhThanh_InsUpd")]
        ResultResponse<long> DM_TinhThanh_InsUpd(DM_TinhThanhMapAdd model);
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                             UriTemplate = "DM/DM_TinhThanh_Del?id={id}&lastupduserid={lastupduserid}")]
        ResultResponse<int> DM_TinhThanh_Del(long id, Guid lastupduserid);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                              UriTemplate = "DM/DM_TinhThanh_GetByMa?maTinhThanh={maTinhThanh}")]
        ResultResponse<DM_TinhThanhMapAdd> DM_TinhThanh_GetByMa(string maTinhThanh);
        #endregion

        #region danh muc nhom quyen
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
              UriTemplate = "DM/DM_NhomQuyen_List")]
        ResultResponse<List<DM_NhomQuyenMap>> DM_NhomQuyen_List(DM_NhomQuyenParam model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
          UriTemplate = "DM/DM_NhomQuyen_GetById?id={id}")]
        ResultResponse<DM_NhomQuyenAdd> DM_NhomQuyen_GetById(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "DM/DM_NhomQuyen_GetByMa?ma={ma}")]
        ResultResponse<DM_NhomQuyenAdd> DM_NhomQuyen_GetByMa(string ma);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DM/DM_NhomQuyen_InsUpd")]
        ResultResponse<long> DM_NhomQuyen_InsUpd(DM_NhomQuyenAdd model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DM/DM_NhomQuyen_Delete?id={id}&userId={userId}")]
        ResultResponse<bool> DM_NhomQuyen_Delete(long id, Guid userId);
        #endregion

        #region danh muc quyen
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
              UriTemplate = "DM/DM_Quyen_List")]
        ResultResponse<List<DM_QuyenMap>> DM_Quyen_List(DM_QuyenMapParam model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
          UriTemplate = "DM/DM_Quyen_GetById?id={id}")]
        ResultResponse<DM_QuyenMapAdd> DM_Quyen_GetById(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "DM/DM_Quyen_GetByMa?ma={ma}")]
        ResultResponse<DM_QuyenMapAdd> DM_Quyen_GetByMa(string ma);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DM/DM_Quyen_InsUpd")]
        ResultResponse<long> DM_Quyen_InsUpd(DM_QuyenMapAdd model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DM/DM_Quyen_Delete?id={id}&userId={userId}")]
        ResultResponse<bool> DM_Quyen_Delete(long id, Guid userId);
        #endregion

        #region danh muc quyen chuc nang
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
              UriTemplate = "DM/DM_QuyenChucNang_List")]
        ResultResponse<List<DM_QuyenChucNangMap>> DM_QuyenChucNang_List(DM_QuyenChucNangMapParam model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
          UriTemplate = "DM/DM_QuyenChucNang_GetById?id={id}")]
        ResultResponse<DM_QuyenChucNangAdd> DM_QuyenChucNang_GetById(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "DM/DM_QuyenChucNang_GetByMa?ma={ma}")]
        ResultResponse<DM_QuyenChucNangAdd> DM_QuyenChucNang_GetByMa(string ma);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DM/DM_QuyenChuNang_InsUpd")]
        ResultResponse<long> DM_QuyenChuNang_InsUpd(DM_QuyenChucNangAdd model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DM/DM_QuyenChucNang_Delete?id={id}&userId={userId}")]
        ResultResponse<bool> DM_QuyenChucNang_Delete(long id, Guid userId);
        #endregion

        #region DM_PhuongXa
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
              UriTemplate = "DM/DM_PhuongXa_List")]
        ResultResponse<List<DM_PhuongXaMap>> DM_PhuongXa_List(DM_PhuongXaMapParam model);
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                      UriTemplate = "DM/DM_PhuongXa_GetById?id={id}")]
        ResultResponse<DM_PhuongXaMapAdd> DM_PhuongXa_GetById(long id);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
                       UriTemplate = "DM/DM_PhuongXa_InsUpd")]
        ResultResponse<long> DM_PhuongXa_InsUpd(DM_PhuongXaMapAdd model);
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                             UriTemplate = "DM/DM_PhuongXa_Del?id={id}&lastupduserid={lastupduserid}")]
        ResultResponse<int> DM_PhuongXa_Del(long id, Guid lastupduserid);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                              UriTemplate = "DM/DM_PhuongXa_GetByMa?ma={ma}")]
        ResultResponse<DM_PhuongXaMapAdd> DM_PhuongXa_GetByMa(string ma);
        #endregion

        #region danh muc quan huyen
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
              UriTemplate = "DM/DM_QuanHuyen_List")]
        ResultResponse<List<DM_QuanHuyenMap>> DM_QuanHuyen_List(DM_QuanHuyenMapParam model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
          UriTemplate = "DM/DM_QuanHuyen_GetById?id={id}")]
        ResultResponse<DM_QuanHuyenMapAdd> DM_QuanHuyen_GetById(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "DM/DM_QuanHuyen_GetByMa?ma={ma}")]
        ResultResponse<DM_QuanHuyenMapAdd> DM_QuanHuyen_GetByMa(string ma);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DM/DM_QuanHuyen_InsUpd")]
        ResultResponse<long> DM_QuanHuyen_InsUpd(DM_QuanHuyenMapAdd model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DM/DM_QuanHuyen_Delete?id={id}&userId={userId}")]
        ResultResponse<bool> DM_QuanHuyen_Delete(long id, Guid userId);
        #endregion

        #region CauHinhheThong
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
              UriTemplate = "CH/GetAllCauHinh")]
        ResultResponse<CauHinhHeThongAll> GetAllCauHinh();

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "CH/CauHinhheThong_Insert")]
        ResultResponse<long> CauHinhheThong_Insert(CauHinhHeThongAll model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                      UriTemplate = "CH/CauHinhMail_DoiMatKhau?pass={pass}")]
        ResultResponse<int> CauHinhMail_DoiMatKhau(string pass);
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "CH/LuotTruyCapHeThong_Insert")]
        ResultResponse<bool> LuotTruyCapHeThong_Insert();
        #endregion

        #region Export
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "DC/Print")]
        ResultResponse<string> Print(Print_DataMap param);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "DC/GetInfoParametersByKeyCode")]
        ResultResponse<E_ParametersMap> GetInfoParametersByKeyCode(string model);
        #endregion

        #region TT_ChuSoHuu
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TT/TT_ChuSoHuu_List")]
        ResultResponse<List<TT_ChuSoHuuMap>> TT_ChuSoHuu_List(TT_ChuSoHuuParam model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TT/TT_ChuSoHuu_ById?id={id}")]
        ResultResponse<TT_ChuSoHuuAdd> TT_ChuSoHuu_ById(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TT/TT_ChuSoHuu_CheckCCCD?cccd={cccd}&chuSoHuuID={chuSoHuuID}")]
        ResultResponse<int> TT_ChuSoHuu_CheckCCCD(string cccd, long chuSoHuuID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TT/TT_ChuSoHuu_InsUpd")]
        ResultResponse<long> TT_ChuSoHuu_InsUpd(TT_ChuSoHuuAdd model);
        #endregion TT_ChuSoHuu

        #region TT_CongDan
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TT/TT_CongDan_List")]
        ResultResponse<List<TT_CongDanMap>> TT_CongDan_List(TT_CongDanParam model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TT/TT_CongDan_ById?id={id}")]
        ResultResponse<TT_CongDanAdd> TT_CongDan_ById(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TT/TT_CongDan_CheckCCCD?cccd={cccd}&congDanID={congDanID}")]
        ResultResponse<int> TT_CongDan_CheckCCCD(string cccd, long congDanID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TT/TT_CongDan_CheckDKKD?dkkd={dkkd}&congDanID={congDanID}")]
        ResultResponse<int> TT_CongDan_CheckDKKD(string dkkd, long congDanID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TT/TT_CongDan_InsUpd")]
        ResultResponse<long> TT_CongDan_InsUpd(TT_CongDanAdd model);
        #endregion TT_CongDan

        #region TT_TacGia
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TT/TT_TacGia_List")]
        ResultResponse<List<TT_TacGiaMap>> TT_TacGia_List(TT_TacGiaParam model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TT/TT_TacGia_ById?id={id}")]
        ResultResponse<TT_TacGiaAdd> TT_TacGia_ById(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TT/TT_TacGia_CheckCCCD?cccd={cccd}&tacGiaID={tacGiaID}")]
        ResultResponse<int> TT_TacGia_CheckCCCD(string cccd, long tacGiaID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TT/TT_TacGia_InsUpd")]
        ResultResponse<long> TT_TacGia_InsUpd(TT_TacGiaAdd model);
        #endregion TT_TacGia

        #region TT_HinhAnh
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TT/TT_HinhAnh_List")]
        ResultResponse<List<TT_HinhAnhMap>> TT_HinhAnh_List(TT_HinhAnhParam model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TT/TT_HinhAnh_ById?id={id}")]
        ResultResponse<TT_HinhAnhAdd> TT_HinhAnh_ById(long id);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TT/TT_HinhAnh_InsUpd")]
        ResultResponse<long> TT_HinhAnh_InsUpd(TT_HinhAnhAdd model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TT/TT_HinhAnh_Del?id={id}&userId={userId}")]
        ResultResponse<int> TT_HinhAnh_Del(long id, Guid userId);
        #endregion TT_HinhAnh

        #region TT_Phim
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TT/TT_Phim_List")]
        ResultResponse<List<TT_PhimMap>> TT_Phim_List(TT_PhimParam model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TT/TT_Phim_ById?id={id}")]
        ResultResponse<TT_PhimAdd> TT_Phim_ById(long id);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TT/TT_Phim_InsUpd")]
        ResultResponse<long> TT_Phim_InsUpd(TT_PhimAdd model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TT/TT_Phim_Del?id={id}&userId={userId}")]
        ResultResponse<int> TT_Phim_Del(long id, Guid userId);
        #endregion TT_Phim

        #region TT_DinhKem
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TT/TT_DinhKem_List")]
        ResultResponse<List<TT_DinhKemMap>> TT_DinhKem_List(TT_DinhKemParam model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TT/TT_DinhKem_ById?id={id}")]
        ResultResponse<TT_DinhKemAdd> TT_DinhKem_ById(long id);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TT/TT_DinhKem_InsUpd")]
        ResultResponse<long> TT_DinhKem_InsUpd(TT_DinhKemAdd model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TT/TT_DinhKem_Del?id={id}&userId={userId}")]
        ResultResponse<int> TT_DinhKem_Del(long id, Guid userId);
        #endregion TT_DinhKem

        #region TT_NguoiBieuDien
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TT/TT_NguoiBieuDien_List")]
        ResultResponse<List<TT_NguoiBieuDienMap>> TT_NguoiBieuDien_List(TT_NguoiBieuDienParam model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TT/TT_NguoiBieuDien_ById?id={id}")]
        ResultResponse<TT_NguoiBieuDienAdd> TT_NguoiBieuDien_ById(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TT/TT_NguoiBieuDien_CheckCCCD?cccd={cccd}&nguoiBieuDienID={nguoiBieuDienID}")]
        ResultResponse<int> TT_NguoiBieuDien_CheckCCCD(string cccd, long nguoiBieuDienID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "TT/TT_NguoiBieuDien_InsUpd")]
        ResultResponse<long> TT_NguoiBieuDien_InsUpd(TT_NguoiBieuDienAdd model);
        #endregion TT_NguoiBieuDien

        #region MAP_ThuTuc_ManHinh
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "MAP/MAP_ThuTuc_ManHinh_GetById?id={id}")]
        ResultResponse<MAP_ThuTuc_ManHinhAdd> MAP_ThuTuc_ManHinh_GetById(long id);
        #endregion

        #region DM_LoaiHinhDangKy
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DM/DM_LoaiHinhDangKy_InsUpd")]
        ResultResponse<long> DM_LoaiHinhDangKy_InsUpd(DM_LoaiHinhDangKyMapAdd model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
              UriTemplate = "DM/DM_LoaiHinhDangKy_List")]
        ResultResponse<List<DM_LoaiHinhDangKyMap>> DM_LoaiHinhDangKy_List(DM_LoaiHinhDangKyMapParam model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
          UriTemplate = "DM/DM_LoaiHinhDangKy_GetById?id={id}")]
        ResultResponse<DM_LoaiHinhDangKyMapAdd> DM_LoaiHinhDangKy_GetById(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "DM/DM_LoaiHinhDangKy_GetByMa?ma={ma}")]
        ResultResponse<DM_LoaiHinhDangKyMapAdd> DM_LoaiHinhDangKy_GetByMa(string ma);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DM/DM_LoaiHinhDangKy_Delete?id={id}&userId={userId}")]
        ResultResponse<bool> DM_LoaiHinhDangKy_Delete(long id, Guid userId);
        #endregion DM_LoaiHinhDangKy

        #region DM_LoaiHinhTacPham
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DM/DM_LoaiHinhTacPham_InsUpd")]
        ResultResponse<long> DM_LoaiHinhTacPham_InsUpd(DM_LoaiHinhTacPhamMapAdd model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
              UriTemplate = "DM/DM_LoaiHinhTacPham_List")]
        ResultResponse<List<DM_LoaiHinhTacPhamMap>> DM_LoaiHinhTacPham_List(DM_LoaiHinhTacPhamMapParam model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
          UriTemplate = "DM/DM_LoaiHinhTacPham_GetById?id={id}")]
        ResultResponse<DM_LoaiHinhTacPhamMapAdd> DM_LoaiHinhTacPham_GetById(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "DM/DM_LoaiHinhTacPham_GetByMa?ma={ma}")]
        ResultResponse<DM_LoaiHinhTacPhamMapAdd> DM_LoaiHinhTacPham_GetByMa(string ma);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DM/DM_LoaiHinhTacPham_Delete?id={id}&userId={userId}")]
        ResultResponse<bool> DM_LoaiHinhTacPham_Delete(long id, Guid userId);
        #endregion DM_LoaiHinhTacPham

        #region DM_VungMien
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DM/DM_VungMien_InsUpd")]
        ResultResponse<long> DM_VungMien_InsUpd(DM_VungMienMapAdd model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
              UriTemplate = "DM/DM_VungMien_List")]
        ResultResponse<List<DM_VungMienMap>> DM_VungMien_List(DM_VungMienMapParam model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
          UriTemplate = "DM/DM_VungMien_GetById?id={id}")]
        ResultResponse<DM_VungMienMapAdd> DM_VungMien_GetById(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "DM/DM_VungMien_GetByMa?ma={ma}")]
        ResultResponse<DM_VungMienMapAdd> DM_VungMien_GetByMa(string ma);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DM/DM_VungMien_Delete?id={id}&userId={userId}")]
        ResultResponse<bool> DM_VungMien_Delete(long id, Guid userId);
        #endregion DM_VungMien

        #region DM_CoQuanCap
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DM/DM_CoQuanCap_InsUpd")]
        ResultResponse<long> DM_CoQuanCap_InsUpd(DM_CoQuanCapMapAdd model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
              UriTemplate = "DM/DM_CoQuanCap_List")]
        ResultResponse<List<DM_CoQuanCapMap>> DM_CoQuanCap_List(DM_CoQuanCapMapParam model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
          UriTemplate = "DM/DM_CoQuanCap_GetById?id={id}")]
        ResultResponse<DM_CoQuanCapMapAdd> DM_CoQuanCap_GetById(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "DM/DM_CoQuanCap_GetByMa?ma={ma}")]
        ResultResponse<DM_CoQuanCapMapAdd> DM_CoQuanCap_GetByMa(string ma);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DM/DM_CoQuanCap_Delete?id={id}&userId={userId}")]
        ResultResponse<bool> DM_CoQuanCap_Delete(long id, Guid userId);
        #endregion DM_CoQuanCap

        #region DM_NguoiKy
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DM/DM_NguoiKy_InsUpd")]
        ResultResponse<long> DM_NguoiKy_InsUpd(DM_NguoiKyMapAdd model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
              UriTemplate = "DM/DM_NguoiKy_List")]
        ResultResponse<List<DM_NguoiKyMap>> DM_NguoiKy_List(DM_NguoiKyMapParam model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
          UriTemplate = "DM/DM_NguoiKy_GetById?id={id}")]
        ResultResponse<DM_NguoiKyMapAdd> DM_NguoiKy_GetById(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "DM/DM_NguoiKy_GetByMa?ma={ma}")]
        ResultResponse<DM_NguoiKyMapAdd> DM_NguoiKy_GetByMa(string ma);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DM/DM_NguoiKy_Delete?id={id}&userId={userId}")]
        ResultResponse<bool> DM_NguoiKy_Delete(long id, Guid userId);
        #endregion DM_NguoiKy

        #region DM_LoaiSo
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DM/DM_LoaiSo_InsUpd")]
        ResultResponse<long> DM_LoaiSo_InsUpd(DM_LoaiSoMapAdd model);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
              UriTemplate = "DM/DM_LoaiSo_List")]
        ResultResponse<List<DM_LoaiSoMap>> DM_LoaiSo_List(DM_LoaiSoMapParam model);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
          UriTemplate = "DM/DM_LoaiSo_GetById?id={id}")]
        ResultResponse<DM_LoaiSoMapAdd> DM_LoaiSo_GetById(long id);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "DM/DM_LoaiSo_GetByLoaiNghieVuId?loaiNghiepVuId={loaiNghiepVuId}")]
        ResultResponse<DM_LoaiSoMapAdd> DM_LoaiSo_GetByLoaiNghieVuId(long loaiNghiepVuId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DM/DM_LoaiSo_Delete?id={id}")]
        ResultResponse<bool> DM_LoaiSo_Delete(long id);
        #endregion DM_LoaiSo
    }
}
