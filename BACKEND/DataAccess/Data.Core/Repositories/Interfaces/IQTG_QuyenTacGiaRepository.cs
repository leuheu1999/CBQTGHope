using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data;

namespace Data.Core.Repositories.Interfaces
{
    public interface IQTG_QuyenTacGiaRepository : IRepository<QTG_QuyenTacGiaMap>
    {
        #region QTG_QuyenTacGia
        List<QTG_QuyenTacGiaMap> QTG_QuyenTacGia_List(QTG_QuyenTacGiaParam model, out ResponseModel restStatus);
        QTG_QuyenTacGiaAdd QTG_QuyenTacGia_ById(long id, out ResponseModel restStatus);
        long QTG_QuyenTacGia_InsUpd(QTG_QuyenTacGiaAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        int QTG_TacGia_Ins(List<QTG_TacGiaAdd> list, long quyenTacGiaID, Guid userID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        int QTG_ChuSoHuu_Ins(List<QTG_ChuSoHuuAdd> list, long quyenTacGiaID, Guid userID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        int QTG_DinhKem_Ins(List<QTG_DinhKemAdd> list, long quyenTacGiaID, int loaiXuLyID, long keyMapID, Guid userID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        long QTG_QuyenTacGia_Change(QTG_QuyenTacGiaChange model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        List<QTG_LichSuMap> QTG_LichSu_ById(long id, long staticID, out ResponseModel restStatus);
        long QTG_QuyenTacGia_GetStt(out ResponseModel restStatus);
        List<QTG_TacGiaAdd> QTG_TacGia_ByQuyenTGId(long quyenTacGiaID, out ResponseModel restStatus);
        List<QTG_ChuSoHuuAdd> QTG_ChuSoHuu_ByQuyenTGId(long quyenTacGiaID, out ResponseModel restStatus);
        int QTG_QuyenTacGia_Del(long quyenTacGiaID, Guid userID, out ResponseModel restStatus);
        List<QTG_GiayChungNhanMap> QTG_QuyenTacGia_SearchGCN(QTG_GiayChungNhanParam model, out ResponseModel restStatus);
        List<QTG_SoThuTuMap> QTG_QuyenTacGia_SearchSTT(QTG_SoThuTuParam model, out ResponseModel restStatus);
        long QTG_QuyenTacGia_OldToNew(long id, int loaiNghiepVuID, long keyMapID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        long QTG_QuyenTacGia_CapSo(QTG_QuyenTacGia_CapSo model, out ResponseModel restStatus);
        int QTG_QuyenTacGia_CheckSoTT(long id, string soTT, out ResponseModel restStatus);
        int QTG_QuyenTacGia_CheckSoGCN(long id, string soGCN, out ResponseModel restStatus);
        int QTG_QuyenTacGia_Duyet(long id, int loaiNghiepVuID, bool isDuyet, out ResponseModel restStatus);
        #endregion QTG_QuyenTacGia

        #region QTG_CapLai
        QTG_CapLaiAdd QTG_CapLai_ById(long id, out ResponseModel restStatus);
        QTG_CapLaiAdd QTG_CapLai_ByHoSoId(long id, out ResponseModel restStatus);
        QTG_CapLaiAdd QTG_CapLai_ByQuyenTGId(long id, out ResponseModel restStatus);
        long QTG_CapLai_InsUpd(QTG_CapLaiAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        #endregion QTG_CapLai

        #region QTG_ChuyenChuSoHuu
        QTG_ChuyenChuSoHuuAdd QTG_ChuyenChuSoHuu_ById(long id, out ResponseModel restStatus);
        QTG_ChuyenChuSoHuuAdd QTG_ChuyenChuSoHuu_ByHoSoId(long id, out ResponseModel restStatus);
        QTG_ChuyenChuSoHuuAdd QTG_ChuyenChuSoHuu_ByQuyenTGId(long id, out ResponseModel restStatus);
        long QTG_ChuyenChuSoHuu_InsUpd(QTG_ChuyenChuSoHuuAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        #endregion

        #region QTG_CapDoi
        QTG_CapDoiAdd QTG_CapDoi_ById(long id, out ResponseModel restStatus);
        QTG_CapDoiAdd QTG_CapDoi_ByHoSoId(long id, out ResponseModel restStatus);
        QTG_CapDoiAdd QTG_CapDoi_ByQuyenTGId(long id, out ResponseModel restStatus);
        long QTG_CapDoi_InsUpd(QTG_CapDoiAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        #endregion QTG_CapDoi

        #region QTG_ThuHoi
        QTG_ThuHoiAdd QTG_ThuHoi_ById(long id, out ResponseModel restStatus);
        QTG_ThuHoiAdd QTG_ThuHoi_ByHoSoId(long id, out ResponseModel restStatus);
        QTG_ThuHoiAdd QTG_ThuHoi_ByQuyenTGId(long id, out ResponseModel restStatus);
        long QTG_ThuHoi_InsUpd(QTG_ThuHoiAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        #endregion QTG_ThuHoi
    }
}
