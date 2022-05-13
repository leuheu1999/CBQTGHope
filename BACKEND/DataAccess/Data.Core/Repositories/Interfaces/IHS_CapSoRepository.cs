using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data;

namespace Data.Core.Repositories.Interfaces
{
    public interface IHS_CapSoRepository : IRepository<HS_CapSoMap>
    {
        HS_CapSoMapAdd HS_CapSo_ById(long capSoID, out ResponseModel restStatus);
        List<HS_CapSoMap> HS_CapSo_ByHoSoId(long hoSoID, out ResponseModel restStatus);
        int HS_CapSo_CheckLoaiSo(long capSoID, int loaiSoID, out ResponseModel restStatus);
        int HS_CapSo_CheckSo(long capSoID, int loaiSoID, string so, out ResponseModel restStatus);
        long HS_CapSo_InsUpd(HS_CapSoMapAdd param, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        long HS_CapSo_UpdChuyenNganh(HS_CapSoMapAdd param, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        int HS_CapSo_Del(long capSoID, Guid createdUserID, out ResponseModel restStatus);
        HS_GenSo HS_CapSo_GenSo(int loaiNghiepVuID, out ResponseModel restStatus);
        HS_GenSo DVC_HS_CapSo_GenSo(int loaiNghiepVuID, out ResponseModel restStatus);

        int HS_HoSo_CheckCapSo(long hoSoID, out ResponseModel restStatus);
    }
}
