
using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using System.Data;

namespace Data.Core.Repositories.Interfaces
{
   public interface ICauHinhHeThongRepository : IRepository<CauHinhHeThongMap>
    {
        CauHinhHeThongMap CauHinhHeThong_Get(out ResponseModel restStatus);
        string CauHinhHeThong_GetByKey(string Key,out ResponseModel restStatus);
        string DM_DonVi_GetByKey(string Key, out ResponseModel restStatus);
        long CauHinhheThong_Ins(CauHinhHeThongMap model,IDbConnection conn, IDbTransaction trans, out ResponseModel restStatus);
        CauHinhMailMap CauHinhMail_Get(out ResponseModel restStatus);
        long CauHinhmail_Ins(CauHinhMailMap model, IDbConnection conn, IDbTransaction trans, out ResponseModel restStatus);
        DonViMap CauHinhDonVi_Get(out ResponseModel restStatus);
        long CauHinhDonVi_Ins(DonViMap model, IDbConnection conn, IDbTransaction trans, out ResponseModel restStatus);
        int CauHinhMail_DoiMatKhau(string pass, out ResponseModel restStatus);
        bool LuotTruyCapHeThong_Insert(out ResponseModel restStatus);
    }
}
