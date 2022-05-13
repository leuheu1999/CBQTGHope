using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using System;
using System.Collections.Generic;

namespace Data.Core.Repositories.Interfaces
{
    public interface IDM_NguoiKyRepository : IRepository<DM_NguoiKyMap>
    {
        List<DM_NguoiKyMap> DM_NguoiKy_List(DM_NguoiKyMapParam model, out ResponseModel restStatus);
        long DM_NguoiKy_InsUpd(DM_NguoiKyMapAdd model, out ResponseModel restStatus);
        DM_NguoiKyMapAdd DM_NguoiKy_GetById(long id, out ResponseModel restStatus);
        DM_NguoiKyMapAdd DM_NguoiKy_GetByMa(string ma, out ResponseModel restStatus);
        bool DM_NguoiKy_Delete(long id, Guid userId, out ResponseModel restStatus);
    }
}
