using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data;

namespace Data.Core.Repositories.Interfaces
{
    public interface IDM_LoaiSoRepository : IRepository<DM_LoaiSoMap>
    {
        DM_LoaiSoMapAdd DM_LoaiSo_ById(int ID, out ResponseModel restStatus);
        string DM_LoaiSo_GenCapSo(int ID, out ResponseModel restStatus);
        int DM_LoaiSo_UpdCapSo(int ID, int SoHienTai, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);

        List<DM_LoaiSoMap> DM_LoaiSo_List(DM_LoaiSoMapParam model, out ResponseModel restStatus);
        long DM_LoaiSo_InsUpd(DM_LoaiSoMapAdd model, out ResponseModel restStatus);
        DM_LoaiSoMapAdd DM_LoaiSo_GetById(long id, out ResponseModel restStatus);
        DM_LoaiSoMapAdd DM_LoaiSo_GetByLoaiNghieVuId(long loaiNghiepVuId, out ResponseModel restStatus);
        bool DM_LoaiSo_Delete(long id, out ResponseModel restStatus);
    }
}
