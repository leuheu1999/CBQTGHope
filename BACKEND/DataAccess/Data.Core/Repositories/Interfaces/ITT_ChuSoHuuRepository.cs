using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using System.Collections.Generic;

namespace Data.Core.Repositories.Interfaces
{
    public interface ITT_ChuSoHuuRepository : IRepository<TT_ChuSoHuuMap>
    {
        List<TT_ChuSoHuuMap> TT_ChuSoHuu_List(TT_ChuSoHuuParam model, out ResponseModel restStatus);
        TT_ChuSoHuuAdd TT_ChuSoHuu_ById(long id, out ResponseModel restStatus);
        int TT_ChuSoHuu_CheckCCCD(string cccd, long chuSoHuuID, out ResponseModel restStatus);
        long TT_ChuSoHuu_InsUpd(TT_ChuSoHuuAdd model, out ResponseModel restStatus);
    }
}
