using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data;

namespace Data.Core.Repositories.Interfaces
{
    public interface IDVC_QuyenLienQuanRepository : IRepository<DVC_QLQ_QuyenLienQuanMap>
    {
        #region DVC_QLQ_QuyenLienQuan
        List<DVC_QLQ_QuyenLienQuanMap> DVC_QLQ_QuyenLienQuan_List(DVC_QLQ_QuyenLienQuanParam model, out ResponseModel restStatus);
        DVC_QLQ_QuyenLienQuanAdd DVC_QLQ_QuyenLienQuan_ById(long id, out ResponseModel restStatus);
        long DVC_QLQ_QuyenLienQuan_InsUpd(DVC_QLQ_QuyenLienQuanAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        int DVC_QLQ_NguoiBieuDien_Ins(List<DVC_QLQ_NguoiBieuDienAdd> list, long quyenNguoiBieuDienID, Guid userID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        int DVC_QLQ_ChuSoHuu_Ins(List<DVC_QLQ_ChuSoHuuAdd> list, long quyenNguoiBieuDienID, Guid userID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        int DVC_QLQ_DinhKem_Ins(List<DVC_QLQ_DinhKemAdd> list, long quyenNguoiBieuDienID, int loaiXuLyID, long keyMapID, Guid userID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        long DVC_QLQ_QuyenLienQuan_Change(DVC_QLQ_QuyenLienQuanChange model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        List<DVC_QLQ_LichSuMap> DVC_QLQ_LichSu_ById(long id, long staticID, out ResponseModel restStatus);
        long DVC_QLQ_QuyenLienQuan_GetStt(out ResponseModel restStatus);
        List<DVC_QLQ_NguoiBieuDienAdd> DVC_QLQ_NguoiBieuDien_ByQuyenLQId(long quyenNguoiBieuDienID, out ResponseModel restStatus);
        List<DVC_QLQ_ChuSoHuuAdd> DVC_QLQ_ChuSoHuu_ByQuyenLQId(long quyenNguoiBieuDienID, out ResponseModel restStatus);
        int DVC_QLQ_QuyenLienQuan_Del(long quyenLienQuanID, Guid userID, out ResponseModel restStatus);
        List<DVC_QLQ_GiayChungNhanMap> DVC_QLQ_QuyenLienQuan_SearchGCN(DVC_QLQ_GiayChungNhanParam model, out ResponseModel restStatus);
        long DVC_QLQ_QuyenLienQuan_OldToNew(long id, int loaiNghiepVuID, long keyMapID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        long DVC_QLQ_QuyenLienQuan_CapSo(DVC_QLQ_QuyenLienQuan_CapSo model, out ResponseModel restStatus);
        int DVC_QLQ_QuyenLienQuan_CheckSoTT(long id, string soTT, out ResponseModel restStatus);
        int DVC_QLQ_QuyenLienQuan_CheckSoGCN(long id, string soGCN, out ResponseModel restStatus);
        #endregion DVC_QLQ_QuyenLienQuan
    }
}
