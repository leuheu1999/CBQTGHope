using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data;

namespace Data.Core.Repositories.Interfaces
{
    public interface IQLQ_QuyenLienQuanRepository : IRepository<QLQ_QuyenLienQuanMap>
    {
        #region QLQ_QuyenLienQuan
        List<QLQ_QuyenLienQuanMap> QLQ_QuyenLienQuan_List(QLQ_QuyenLienQuanParam model, out ResponseModel restStatus);
        QLQ_QuyenLienQuanAdd QLQ_QuyenLienQuan_ById(long id, out ResponseModel restStatus);
        long QLQ_QuyenLienQuan_InsUpd(QLQ_QuyenLienQuanAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        int QLQ_NguoiBieuDien_Ins(List<QLQ_NguoiBieuDienAdd> list, long quyenNguoiBieuDienID, Guid userID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        int QLQ_ChuSoHuu_Ins(List<QLQ_ChuSoHuuAdd> list, long quyenNguoiBieuDienID, Guid userID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        int QLQ_DinhKem_Ins(List<QLQ_DinhKemAdd> list, long quyenNguoiBieuDienID, int loaiXuLyID, long keyMapID, Guid userID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        long QLQ_QuyenLienQuan_Change(QLQ_QuyenLienQuanChange model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        List<QLQ_LichSuMap> QLQ_LichSu_ById(long id, long staticID, out ResponseModel restStatus);
        long QLQ_QuyenLienQuan_GetStt(out ResponseModel restStatus);
        List<QLQ_NguoiBieuDienAdd> QLQ_NguoiBieuDien_ByQuyenLQId(long quyenNguoiBieuDienID, out ResponseModel restStatus);
        List<QLQ_ChuSoHuuAdd> QLQ_ChuSoHuu_ByQuyenLQId(long quyenNguoiBieuDienID, out ResponseModel restStatus);
        int QLQ_QuyenLienQuan_Del(long quyenLienQuanID, Guid userID, out ResponseModel restStatus);
        List<QLQ_GiayChungNhanMap> QLQ_QuyenLienQuan_SearchGCN(QLQ_GiayChungNhanParam model, out ResponseModel restStatus);
        List<QLQ_SoThuTuMap> QLQ_QuyenLienQuan_SearchSTT(QLQ_SoThuTuParam model, out ResponseModel restStatus);
        long QLQ_QuyenLienQuan_OldToNew(long id, int loaiNghiepVuID, long keyMapID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        long QLQ_QuyenLienQuan_CapSo(QLQ_QuyenLienQuan_CapSo model, out ResponseModel restStatus);
        int QLQ_QuyenLienQuan_CheckSoTT(long id, string soTT, out ResponseModel restStatus);
        int QLQ_QuyenLienQuan_CheckSoGCN(long id, string soGCN, out ResponseModel restStatus);
        int QLQ_QuyenLienQuan_Duyet(long id, int loaiNghiepVuID, bool isDuyet, out ResponseModel restStatus);
        #endregion QLQ_QuyenLienQuan

        #region QLQ_CapLai
        QLQ_CapLaiAdd QLQ_CapLai_ById(long id, out ResponseModel restStatus);
        QLQ_CapLaiAdd QLQ_CapLai_ByHoSoId(long id, out ResponseModel restStatus);
        QLQ_CapLaiAdd QLQ_CapLai_ByQuyenLQId(long id, out ResponseModel restStatus);
        long QLQ_CapLai_InsUpd(QLQ_CapLaiAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        #endregion QLQ_CapLai

        #region QLQ_ChuyenChuSoHuu
        QLQ_ChuyenChuSoHuuAdd QLQ_ChuyenChuSoHuu_ById(long id, out ResponseModel restStatus);
        QLQ_ChuyenChuSoHuuAdd QLQ_ChuyenChuSoHuu_ByHoSoId(long id, out ResponseModel restStatus);
        QLQ_ChuyenChuSoHuuAdd QLQ_ChuyenChuSoHuu_ByQuyenLQId(long id, out ResponseModel restStatus);
        long QLQ_ChuyenChuSoHuu_InsUpd(QLQ_ChuyenChuSoHuuAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        #endregion

        #region QLQ_CapDoi
        QLQ_CapDoiAdd QLQ_CapDoi_ById(long id, out ResponseModel restStatus);
        QLQ_CapDoiAdd QLQ_CapDoi_ByHoSoId(long id, out ResponseModel restStatus);
        QLQ_CapDoiAdd QLQ_CapDoi_ByQuyenLQId(long id, out ResponseModel restStatus);
        long QLQ_CapDoi_InsUpd(QLQ_CapDoiAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        #endregion QLQ_CapDoi

        #region QLQ_ThuHoi
        QLQ_ThuHoiAdd QLQ_ThuHoi_ById(long id, out ResponseModel restStatus);
        QLQ_ThuHoiAdd QLQ_ThuHoi_ByHoSoId(long id, out ResponseModel restStatus);
        QLQ_ThuHoiAdd QLQ_ThuHoi_ByQuyenLQId(long id, out ResponseModel restStatus);
        long QLQ_ThuHoi_InsUpd(QLQ_ThuHoiAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        #endregion QLQ_ThuHoi
    }
}
