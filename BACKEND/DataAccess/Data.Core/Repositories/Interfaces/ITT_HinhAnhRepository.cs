using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data;

namespace Data.Core.Repositories.Interfaces
{
    public interface ITT_HinhAnhRepository : IRepository<TT_HinhAnhMap>
    {
        List<TT_HinhAnhMap> TT_HinhAnh_List(TT_HinhAnhParam model, out ResponseModel restStatus);
        TT_HinhAnhAdd TT_HinhAnh_ById(long id, out ResponseModel restStatus);
        long TT_HinhAnh_InsUpd(TT_HinhAnhAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        int TT_HinhAnh_Del(long id, Guid userId, out ResponseModel restStatus);
        int TT_HinhAnh_DelByQuyenID(long quyenId, int linhVucId, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        int DVC_TT_HinhAnh_DelByQuyenID(long quyenId, int linhVucId, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        long DVC_TT_HinhAnh_InsUpd(TT_HinhAnhAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
    }
}
