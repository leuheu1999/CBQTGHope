using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using System;
using System.Collections.Generic;

namespace Data.Core.Repositories.Interfaces
{
    public interface ITT_DinhKemRepository : IRepository<TT_DinhKemMap>
    {
        List<TT_DinhKemMap> TT_DinhKem_List(TT_DinhKemParam model, out ResponseModel restStatus);
        TT_DinhKemAdd TT_DinhKem_ById(long id, out ResponseModel restStatus);
        long TT_DinhKem_InsUpd(TT_DinhKemAdd model, out ResponseModel restStatus);
        int TT_DinhKem_Del(long id, Guid userId, out ResponseModel restStatus);
    }
}
