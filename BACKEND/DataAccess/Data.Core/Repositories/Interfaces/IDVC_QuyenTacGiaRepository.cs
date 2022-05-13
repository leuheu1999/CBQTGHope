using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data;

namespace Data.Core.Repositories.Interfaces
{
    public interface IDVC_QuyenTacGiaRepository : IRepository<DVC_QTG_QuyenTacGiaMap>
    {
        #region DVC_QTG_QuyenTacGia
        List<DVC_QTG_QuyenTacGiaMap> DVC_QTG_QuyenTacGia_List(DVC_QTG_QuyenTacGiaParam model, out ResponseModel restStatus);
        DVC_QTG_QuyenTacGiaAdd DVC_QTG_QuyenTacGia_ById(long id, out ResponseModel restStatus);
        long DVC_QTG_QuyenTacGia_InsUpd(DVC_QTG_QuyenTacGiaAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        int DVC_QTG_TacGia_Ins(List<DVC_QTG_TacGiaAdd> list, long quyenTacGiaID, Guid userID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        int DVC_QTG_ChuSoHuu_Ins(List<DVC_QTG_ChuSoHuuAdd> list, long quyenTacGiaID, Guid userID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        int DVC_QTG_DinhKem_Ins(List<DVC_QTG_DinhKemAdd> list, long quyenTacGiaID, int loaiXuLyID, long keyMapID, Guid userID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        long DVC_QTG_QuyenTacGia_Change(DVC_QTG_QuyenTacGiaChange model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        List<DVC_QTG_LichSuMap> DVC_QTG_LichSu_ById(long id, long staticID, out ResponseModel restStatus);
        long DVC_QTG_QuyenTacGia_GetStt(out ResponseModel restStatus);
        List<DVC_QTG_TacGiaAdd> DVC_QTG_TacGia_ByQuyenTGId(long quyenTacGiaID, out ResponseModel restStatus);
        List<DVC_QTG_ChuSoHuuAdd> DVC_QTG_ChuSoHuu_ByQuyenTGId(long quyenTacGiaID, out ResponseModel restStatus);
        int DVC_QTG_QuyenTacGia_Del(long quyenTacGiaID, Guid userID, out ResponseModel restStatus);
        List<DVC_QTG_GiayChungNhanMap> DVC_QTG_QuyenTacGia_SearchGCN(DVC_QTG_GiayChungNhanParam model, out ResponseModel restStatus);
        long DVC_QTG_QuyenTacGia_OldToNew(long id, int loaiNghiepVuID, long keyMapID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        long DVC_QTG_QuyenTacGia_CapSo(DVC_QTG_QuyenTacGia_CapSo model, out ResponseModel restStatus);
        int DVC_QTG_QuyenTacGia_CheckSoTT(long id, string soTT, out ResponseModel restStatus);
        int DVC_QTG_QuyenTacGia_CheckSoGCN(long id, string soGCN, out ResponseModel restStatus);
        #endregion DVC_QTG_QuyenTacGia
    }
}
