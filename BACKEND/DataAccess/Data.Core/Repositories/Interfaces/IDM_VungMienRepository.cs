using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using System;
using System.Collections.Generic;

namespace Data.Core.Repositories.Interfaces
{
    public interface IDM_VungMienRepository : IRepository<DM_VungMienMap>
    {
        List<DM_VungMienMap> DM_VungMien_List(DM_VungMienMapParam model, out ResponseModel restStatus);
        long DM_VungMien_InsUpd(DM_VungMienMapAdd model, out ResponseModel restStatus);
        DM_VungMienMapAdd DM_VungMien_GetById(long id, out ResponseModel restStatus);
        DM_VungMienMapAdd DM_VungMien_GetByMa(string ma, out ResponseModel restStatus);

        bool DM_VungMien_Delete(long id, Guid userId, out ResponseModel restStatus);
    }
}
