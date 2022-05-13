using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using PagedList;

namespace Data.Core.Repositories.Interfaces
{
   public interface IDM_TinhThanhRepository : IRepository<DM_TinhThanhMap>
    {
        List<DM_TinhThanhMap> DM_TinhThanh_List(DM_TinhThanhMapParam model, out ResponseModel restStatus);
        DM_TinhThanhMapAdd DM_TinhThanh_GetById(long Id, out ResponseModel restStatus);
        DM_TinhThanhMapAdd DM_TinhThanh_GetByMa(string Ma, out ResponseModel restStatus);
        long DM_TinhThanh_InsUpd(DM_TinhThanhMapAdd model, out ResponseModel restStatus);
        int DM_TinhThanh_Del(long id, Guid createduserid, out ResponseModel restStatus);
    }
}
