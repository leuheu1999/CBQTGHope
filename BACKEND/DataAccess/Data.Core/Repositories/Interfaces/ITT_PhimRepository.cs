using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data;

namespace Data.Core.Repositories.Interfaces
{
    public interface ITT_PhimRepository : IRepository<TT_PhimMap>
    {
        List<TT_PhimMap> TT_Phim_List(TT_PhimParam model, out ResponseModel restStatus);
        TT_PhimAdd TT_Phim_ById(long id, out ResponseModel restStatus);
        long TT_Phim_InsUpd(TT_PhimAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        int TT_Phim_Del(long id, Guid userId, out ResponseModel restStatus);
        int TT_Phim_DelByQuyenID(long quyenId, int linhVucId, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        int DVC_TT_Phim_DelByQuyenID(long quyenId, int linhVucId, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        long DVC_TT_Phim_InsUpd(TT_PhimAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
    }
}
