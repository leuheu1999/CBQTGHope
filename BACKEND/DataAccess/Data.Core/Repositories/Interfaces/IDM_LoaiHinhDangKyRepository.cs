using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using System;
using System.Collections.Generic;

namespace Data.Core.Repositories.Interfaces
{
    public interface IDM_LoaiHinhDangKyRepository : IRepository<DM_LoaiHinhDangKyMap>
    {
        List<DM_LoaiHinhDangKyMap> DM_LoaiHinhDangKy_List(DM_LoaiHinhDangKyMapParam model, out ResponseModel restStatus);
        long DM_LoaiHinhDangKy_InsUpd(DM_LoaiHinhDangKyMapAdd model, out ResponseModel restStatus);
        DM_LoaiHinhDangKyMapAdd DM_LoaiHinhDangKy_GetById(long id, out ResponseModel restStatus);
        DM_LoaiHinhDangKyMapAdd DM_LoaiHinhDangKy_GetByMa(string ma, out ResponseModel restStatus);

        bool DM_LoaiHinhDangKy_Delete(long id, Guid userId, out ResponseModel restStatus);
    }
}
