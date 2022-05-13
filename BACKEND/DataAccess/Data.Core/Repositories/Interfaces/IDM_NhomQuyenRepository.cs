using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using System;
using System.Collections.Generic;

namespace Data.Core.Repositories.Interfaces
{
    public interface IDM_NhomQuyenRepository : IRepository<DM_NhomQuyenMap>
    {
        List<DM_NhomQuyenMap> DM_NhomQuyen_List(DM_NhomQuyenParam model, out ResponseModel restStatus);
        DM_NhomQuyenAdd DM_NhomQuyen_GetById(long id, out ResponseModel restStatus);
        DM_NhomQuyenAdd DM_NhomQuyen_GetByMa(string ma, out ResponseModel restStatus);
        long DM_NhomQuyen_InsUpd(DM_NhomQuyenAdd model, out ResponseModel restStatus);
        bool DM_NhomQuyen_Delete(long id, Guid userId, out ResponseModel restStatus);
    }
}
