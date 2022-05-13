using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using System.Collections.Generic;
using System.Data;

namespace Data.Core.Repositories.Interfaces
{
    public interface ITT_CapQuyenRepository : IRepository<TT_CapQuyenMap>
    {
        int TT_CapQuyen_CheckCapSo(long hoSoID, out ResponseModel restStatus);
        TT_CapQuyenAdd TT_CapQuyen_GetByHoSoId(long hoSoID, out ResponseModel restStatus);
        long TT_CapQuyen_InsUpd(TT_CapQuyenAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        List<TT_CapQuyenMap> TT_CapQuyen_List(TT_CapQuyenParam model, out ResponseModel restStatus);
        TT_CapQuyenAddDuyet TT_CapQuyen_UpdTinhTrang(TT_CapQuyenAddTrangThai model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        int TT_CapQuyen_UpdDuyet(TT_CapQuyenAddDuyet model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        int TT_CapQuyen_UpdKhongDuyet(TT_CapQuyenAddDuyet model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        int TT_CapQuyen_GetLoaiNghiepVuId(long hoSoID, out ResponseModel restStatus);
    }
}
