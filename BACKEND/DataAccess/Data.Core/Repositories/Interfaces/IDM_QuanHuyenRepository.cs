using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using System;
using System.Collections.Generic;

namespace Data.Core.Repositories.Interfaces
{
    public interface IDM_QuanHuyenRepository : IRepository<DM_QuanHuyenMap>
    {
        List<DM_QuanHuyenMap> DM_QuanHuyen_List(DM_QuanHuyenMapParam model, out ResponseModel restStatus);
        DM_QuanHuyenMapAdd DM_QuanHuyen_GetById(long id, out ResponseModel restStatus);
        DM_QuanHuyenMapAdd DM_QuanHuyen_GetByMa(string ma, out ResponseModel restStatus);
        long DM_QuanHuyen_InsUpd(DM_QuanHuyenMapAdd model, out ResponseModel restStatus);
        bool DM_QuanHuyen_Delete(long id, Guid userId, out ResponseModel restStatus);
    }
}
