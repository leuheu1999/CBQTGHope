using System;
using System.Collections.Generic;
using System.Data;
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
   public interface INhatKyNguoiDungRepository : IRepository<ND_NhatKyNguoiDungMap>
    {
        long NhatKyNguoiDung_Insert(ND_NhatKyNguoiDungAdd model, out ResponseModel restStatus);
        LogTypeNDmap LogTypeND_ByKeyWord(string keyword, out ResponseModel restStatus);
        List<ND_NhatKyNguoiDungMap> NhatKyNguoiDung_List(ND_NhatKyNguoiDungParam model, out ResponseModel restStatus);
        int NhatKyNguoiDung_Del(long id, out ResponseModel restStatus);
        int NhatKyNguoiDung_DelLstID(List<long> lstid, out ResponseModel restStatus);
        List<DSLogTypeNDmap> LogTypeND_List(LogTypeNDParam model, out ResponseModel restStatus);
        int LogTypeND_Update(string lstid, string lstidUn, out ResponseModel restStatus);
    }
}
