using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using System.Collections.Generic;

namespace Data.Core.Repositories.Interfaces
{
    public interface ITT_CongDanRepository : IRepository<TT_CongDanMap>
    {
        List<TT_CongDanMap> TT_CongDan_List(TT_CongDanParam model, out ResponseModel restStatus);
        TT_CongDanAdd TT_CongDan_ById(long id, out ResponseModel restStatus);
        int TT_CongDan_CheckCCCD(string cccd, long congDanID, out ResponseModel restStatus);
        int TT_CongDan_CheckDKKD(string dkkd, long congDanID, out ResponseModel restStatus);
        long TT_CongDan_InsUpd(TT_CongDanAdd model, out ResponseModel restStatus);
    }
}
