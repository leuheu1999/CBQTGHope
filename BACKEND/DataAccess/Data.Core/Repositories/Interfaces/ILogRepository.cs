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
   public interface ILogRepository : IRepository<LogMap>
    {
       
        long InsertLog(LogAdd model, out ResponseModel restStatus);
        List<LogMap> LogSystem_List(LogParam model, out ResponseModel restStatus);
        LogAdd GetLogSystemById(long id, out ResponseModel restStatus);
        int LogSystem_DelByID(long id, out ResponseModel restStatus);
        int LogSystem_DelLstID(List<long> lstid, out ResponseModel restStatus);
        int LogSystem_Trancate(out ResponseModel restStatus);

    }
}
