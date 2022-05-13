using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using System;
using System.Collections.Generic;

namespace Data.Core.Repositories.Interfaces
{
    public interface IDM_QuyenChucNangRepository : IRepository<DM_QuyenChucNangMap>
    {
        List<DM_QuyenChucNangMap> DM_QuyenChucNang_List(DM_QuyenChucNangMapParam model, out ResponseModel restStatus);
        DM_QuyenChucNangAdd DM_QuyenChucNang_GetById(long id, out ResponseModel restStatus);
        DM_QuyenChucNangAdd DM_QuyenChucNang_GetByMa(string ma, out ResponseModel restStatus);
        long DM_QuyenChuNang_InsUpd(DM_QuyenChucNangAdd model, out ResponseModel restStatus);
        bool DM_QuyenChucNang_Delete(long id, Guid userId, out ResponseModel restStatus);
    }
}
