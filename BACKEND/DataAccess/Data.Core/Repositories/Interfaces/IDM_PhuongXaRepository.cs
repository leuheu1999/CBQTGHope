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
   public interface IDM_PhuongXaRepository : IRepository<DM_PhuongXaMap>
    {
        List<DM_PhuongXaMap> DM_PhuongXa_List(DM_PhuongXaMapParam model, out ResponseModel restStatus);
        DM_PhuongXaMapAdd DM_PhuongXa_GetById(long phuongXaId, out ResponseModel restStatus);
        DM_PhuongXaMapAdd DM_PhuongXa_GetByMa(string maPhuongXa, out ResponseModel restStatus);
        long DM_PhuongXa_InsUpd(DM_PhuongXaMapAdd model, out ResponseModel restStatus);
        int DM_PhuongXa_Del(long id, Guid createduserid, out ResponseModel restStatus);
    }
}
