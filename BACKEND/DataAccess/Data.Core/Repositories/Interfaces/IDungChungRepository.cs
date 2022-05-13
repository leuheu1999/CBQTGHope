using Business.Entities;
using Business.Entities.Domain;
using Business.Entities.Domain.Print;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using System.Collections.Generic;
using System.Data;

namespace Data.Core.Repositories.Interfaces
{
    public interface IDungChungRepository : IRepository<DM_DungChungViewModel>
    {
        List<CBO_DungChungViewModel> CBO_DungChung_GetAll(CBO_DungChungParam model, out ResponseModel restStatus);
        string Print(Print_DataMap param, out ResponseModel restStatus);
        E_ParametersMap GetInfoParametersByKeyCode(string model, out ResponseModel restStatus);
        List<CBO_DungChungViewModel> CBO_DungChung_GetAllMater(CBO_DungChungParam model, out ResponseModel restStatus);
        MAP_ThuTuc_ManHinhAdd MAP_ThuTuc_ManHinh_GetById(long id, out ResponseModel restStatus);
    }
}
