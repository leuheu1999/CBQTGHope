using Business.Entities.DataModel;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using System.Collections.Generic;

namespace Data.Core.Repositories.Interfaces
{
    public interface ITC_GiayChungNhanRepository : IRepository<TC_GiayChungNhanMap>
    {
        List<TC_GiayChungNhanMap> TC_GiayChungNhan_List(TC_GiayChungNhanMapParam model, out ResponseModel restStatus);
        TC_GiayChungNhanAdd TC_GiayChungNhan_GetDetail(TC_GiayChungNhanAddParam model, out ResponseModel restStatus);
        List<TC_GiayChungNhanCongBaoMap> TC_GiayChungNhanCongBao_List(TC_GiayChungNhanCongBaoParam model, out ResponseModel restStatus);
        List<TC_ThongTinSoHuu> QLQ_ThongTinSoHuu_ByQuyenId(long id, out ResponseModel restStatus);
        List<TC_ThongTinSoHuu> QTG_ThongTinSoHuu_ByQuyenId(long id, out ResponseModel restStatus);
    }
}
