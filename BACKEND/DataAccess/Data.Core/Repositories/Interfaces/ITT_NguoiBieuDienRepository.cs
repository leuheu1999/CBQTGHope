using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using System.Collections.Generic;

namespace Data.Core.Repositories.Interfaces
{
    public interface ITT_NguoiBieuDienRepository : IRepository<TT_NguoiBieuDienMap>
    {
        List<TT_NguoiBieuDienMap> TT_NguoiBieuDien_List(TT_NguoiBieuDienParam model, out ResponseModel restStatus);
        TT_NguoiBieuDienAdd TT_NguoiBieuDien_ById(long id, out ResponseModel restStatus);
        long TT_NguoiBieuDien_InsUpd(TT_NguoiBieuDienAdd model, out ResponseModel restStatus);
        int TT_NguoiBieuDien_CheckCCCD(string cccd, long nguoiBieuDienID, out ResponseModel restStatus);
    }
}
