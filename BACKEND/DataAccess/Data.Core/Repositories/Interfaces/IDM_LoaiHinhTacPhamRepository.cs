using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using System;
using System.Collections.Generic;

namespace Data.Core.Repositories.Interfaces
{
    public interface IDM_LoaiHinhTacPhamRepository : IRepository<DM_LoaiHinhTacPhamMap>
    {
        List<DM_LoaiHinhTacPhamMap> DM_LoaiHinhTacPham_List(DM_LoaiHinhTacPhamMapParam model, out ResponseModel restStatus);
        long DM_LoaiHinhTacPham_InsUpd(DM_LoaiHinhTacPhamMapAdd model, out ResponseModel restStatus);
        DM_LoaiHinhTacPhamMapAdd DM_LoaiHinhTacPham_GetById(long id, out ResponseModel restStatus);
        DM_LoaiHinhTacPhamMapAdd DM_LoaiHinhTacPham_GetByMa(string ma, out ResponseModel restStatus);

        bool DM_LoaiHinhTacPham_Delete(long id, Guid userId, out ResponseModel restStatus);
    }
}
