using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using System;
using System.Collections.Generic;

namespace Data.Core.Repositories.Interfaces
{
    public interface IDM_CoQuanCapRepository : IRepository<DM_CoQuanCapMap>
    {
        List<DM_CoQuanCapMap> DM_CoQuanCap_List(DM_CoQuanCapMapParam model, out ResponseModel restStatus);
        long DM_CoQuanCap_InsUpd(DM_CoQuanCapMapAdd model, out ResponseModel restStatus);
        DM_CoQuanCapMapAdd DM_CoQuanCap_GetById(long id, out ResponseModel restStatus);
        DM_CoQuanCapMapAdd DM_CoQuanCap_GetByMa(string ma, out ResponseModel restStatus);
        bool DM_CoQuanCap_Delete(long id, Guid userId, out ResponseModel restStatus);
    }
}
