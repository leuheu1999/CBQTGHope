using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using System;
using System.Collections.Generic;

namespace Data.Core.Repositories.Interfaces
{
    public interface IDM_QuyenRepository : IRepository<DM_QuyenMap>
    {
        List<DM_QuyenMap> DM_Quyen_List(DM_QuyenMapParam model, out ResponseModel restStatus);
        DM_QuyenMapAdd DM_Quyen_GetById(long id, out ResponseModel restStatus);
        DM_QuyenMapAdd DM_Quyen_GetByMa(string ma, out ResponseModel restStatus);
        long DM_Quyen_InsUpd(DM_QuyenMapAdd model, out ResponseModel restStatus);
        bool DM_Quyen_Delete(long id, Guid userId, out ResponseModel restStatus);
    }
}
