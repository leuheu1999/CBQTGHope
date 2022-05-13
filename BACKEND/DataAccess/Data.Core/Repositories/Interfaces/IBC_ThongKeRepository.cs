using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using PagedList;

namespace Data.Core.Repositories.Interfaces
{
   public interface IBC_ThongKeRepository : IRepository<DM_DungChungMap>
    {
        List<BC_ThongKeGiayChungNhanMap> BC_ThongKeGiayChungNhan_List(BC_ThongKeGiayChungNhanParam model, out ResponseModel restStatus);
        List<BC_CongBaoMap> BC_CongBao_List(BC_CongBaoParam model, out ResponseModel restStatus);
        List<BC_BaoCaoTacPhamMap> BC_BaoCaoTacPham_List(BC_BaoCaoTacPhamParam model, out ResponseModel restStatus);
        List<BC_BaoCaoTongHopHoatDongMap> BC_BaoCaoTongHopHoatDong_List(BC_BaoCaoTongHopHoatDongParam model, out ResponseModel restStatus);
        List<BC_SoGiayChungNhanBanQuyenMap> BC_SoGiayChungNhanBanQuyen_List(BC_SoGiayChungNhanBanQuyenParam model, out ResponseModel restStatus);
        List<BC_ThongKeHoSoQuyenTacGiaMap> BC_ThongKeHoSoQuyenTacGia_Dashboard(BC_ThongKeHoSoQuyenTacGiaParam model, out ResponseModel restStatus);
        List<BC_ThongKeHoSoQuyenLienQuanMap> BC_ThongKeHoSoQuyenLienQuan_Dashboard(BC_ThongKeHoSoQuyenLienQuanParam model, out ResponseModel restStatus);
    }
}
