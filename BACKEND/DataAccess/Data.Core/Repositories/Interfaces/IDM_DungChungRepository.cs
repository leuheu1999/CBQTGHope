using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using System;
using System.Collections.Generic;

namespace Data.Core.Repositories.Interfaces
{
    public interface IDM_DungChungRepository : IRepository<DM_DungChungMap>
    {
        List<DM_DungChungMap> DM_DungChung_List(DM_DungChungMapParam model, out ResponseModel restStatus);
        DM_DungChungMapAdd DM_DungChung_GetByID(long ID, string Table, out ResponseModel restStatus);
        long DM_DungChung_InsUpd(DM_DungChungMapAdd model, out ResponseModel restStatus);
        int DM_DungChung_Del(long ID, string Table, Guid lastupduserid, out ResponseModel restStatus);
        DM_DungChungMapAdd DM_DungChung_GetByMa(string ma, string Table, out ResponseModel restStatus);
    }
}
