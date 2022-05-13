using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using System;
using System.Collections.Generic;

namespace Data.Core.Repositories.Interfaces
{
    public interface IQT_ChuyenMucRepository :IRepository<QT_LoaiChuyenMuc>
    {
        List<QT_LoaiChuyenMuc> QT_LoaiChuyenMuc_GetList(QT_LoaiChuyenMucParam model, out ResponseModel restStatus);
        QT_LoaiChuyenMucAdd QT_LoaiChuyenMuc_GetById(long Id, out ResponseModel restStatus);
        long QT_LoaiChuyenMuc_Del(long Id, Guid lastupduserid, out ResponseModel restStatus);
        long QT_LoaiChuyenMuc_InsUpdate(QT_LoaiChuyenMucAdd model, out ResponseModel restStatus);
    }
}
