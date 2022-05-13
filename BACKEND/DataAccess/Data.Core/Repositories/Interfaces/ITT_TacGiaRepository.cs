using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using System.Collections.Generic;

namespace Data.Core.Repositories.Interfaces
{
    public interface ITT_TacGiaRepository : IRepository<TT_TacGiaMap>
    {
        List<TT_TacGiaMap> TT_TacGia_List(TT_TacGiaParam model, out ResponseModel restStatus);
        TT_TacGiaAdd TT_TacGia_ById(long id, out ResponseModel restStatus);
        long TT_TacGia_InsUpd(TT_TacGiaAdd model, out ResponseModel restStatus);
        int TT_TacGia_CheckCCCD(string cccd, long tacGiaID, out ResponseModel restStatus);
    }
}
