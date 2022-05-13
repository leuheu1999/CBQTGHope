using Business.Entities.Domain;
using Core.Common.Utilities;
using Dapper;
using Data.Core.Repositories.Base;
using Data.Core.Repositories.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Data.Core.Repositories
{
    public class QT_ChuyenMucRepository : Repository<DM_LoaiChuyenMucMap>, IQT_ChuyenMucRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(QT_ChuyenMucRepository));
        private const string TableName = "";
        private readonly ILogger _log;
        public QT_ChuyenMucRepository(ILog logger, ILogger log) : base(TableName)
        {
            _logger = logger;
            _log = log;
        }

        #region QT LoaiChuyenMuc
        public List<DM_LoaiChuyenMucMap> QT_LoaiChuyenMuc_GetList(DM_LoaiChuyenMucParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = KhuyenMaiConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("TuKhoa", model.TuKhoa, DbType.String, ParameterDirection.Input);
                    paramters.Add("CongKhai", model.CongKhai, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<DM_LoaiChuyenMucMap>("TT_LoaiChuyenMuc_GetList", paramters, commandType: CommandType.StoredProcedure)
                                    .ToList() ?? new List<DM_LoaiChuyenMucMap>();
                    restStatus = new ResponseModel();
                    if (datas != null && datas.Count > 0)
                    {
                        return new List<DM_LoaiChuyenMucMap>(datas);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("TT_LoaiChuyenMuc_GetList Error: " + ex.StackTrace);
                //log db
                _log.Error("TT_LoaiChuyenMuc_GetList Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public DM_LoaiChuyenMucAdd QT_LoaiChuyenMuc_GetById(long Id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = KhuyenMaiConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", Id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<DM_LoaiChuyenMucAdd>("TT_LoaiChuyenMuc_ById", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    if (datas != null)
                    {
                        return datas;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_TinTuc_GetById Error: " + ex.StackTrace);
                //log db
                _log.Error("DM_TinTuc_GetById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long QT_LoaiChuyenMuc_Del(long Id, Guid lastupduserid, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = KhuyenMaiConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Id", Id, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("LastUpdUserID", lastupduserid, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("TT_LoaiChuyenMuc_Del", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("QT_LoaiChuyenMuc_Del Error: " + ex.StackTrace);
                //log db
                _log.Error("QT_LoaiChuyenMuc_Del Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public long QT_LoaiChuyenMuc_InsUpdate(DM_LoaiChuyenMucAdd model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = KhuyenMaiConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", model.ID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("ParentID", model.ParentID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("MucChuyenMuc", model.MucChuyenMuc, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("TieuDe", model.TieuDe, DbType.String, ParameterDirection.Input);
                    paramters.Add("TomTat", model.TomTat, DbType.String, ParameterDirection.Input);
                    paramters.Add("NoiDung", model.NoiDung, DbType.String, ParameterDirection.Input);
                    paramters.Add("ImageUrl", model.ImageUrl, DbType.String, ParameterDirection.Input);
                    paramters.Add("ThuTuHienThi", model.ThuTuHienThi, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("SeName", model.SeName, DbType.String, ParameterDirection.Input);
                    paramters.Add("MetaKeywords", model.MetaKeywords, DbType.String, ParameterDirection.Input);
                    paramters.Add("MetaDescription", model.MetaDescription, DbType.String, ParameterDirection.Input);
                    paramters.Add("MetaTitle", model.MetaTitle, DbType.String, ParameterDirection.Input);
                    paramters.Add("IsShowHome", model.IsShowHome, DbType.Boolean, ParameterDirection.Input);
                    paramters.Add("CongKhai", model.CongKhai, DbType.Boolean, ParameterDirection.Input);
                    paramters.Add("CreatedUserID", model.CreatedUserID, DbType.Guid, ParameterDirection.Input);
                    paramters.Add("LastUpdUserID", model.LastUpdUserID, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<long>("TT_LoaiChuyenMuc_InsUpd", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_TinTuc_InsUpdate Error: " + ex.StackTrace);
                //log db
                _log.Error("DM_TinTuc_InsUpdate Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        #endregion

    }
}
