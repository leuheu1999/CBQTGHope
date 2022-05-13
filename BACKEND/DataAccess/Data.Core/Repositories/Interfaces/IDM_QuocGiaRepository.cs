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
   public interface IDM_QuocGiaRepository : IRepository<DM_QuocGiaMap>
    {
        List<DM_QuocGiaMap> DM_QuocGia_List(DM_QuocGiaMapParam model, out ResponseModel restStatus);
        DM_QuocGiaMapAdd DM_QuocGia_GetById(long quocGiaId, out ResponseModel restStatus);
        DM_QuocGiaMapAdd DM_QuocGia_GetByMa(string maQuocGia, out ResponseModel restStatus);
        long DM_QuocGia_InsUpd(DM_QuocGiaMapAdd model, out ResponseModel restStatus);
        int DM_QuocGia_Del(long id, Guid createduserid, out ResponseModel restStatus);
    }
}
