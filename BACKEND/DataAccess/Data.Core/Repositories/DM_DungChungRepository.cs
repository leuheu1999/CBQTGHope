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
    public class DM_DungChungRepository : Repository<DM_DungChungMap>, IDM_DungChungRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(DM_DungChungRepository));
        private const string TableName = "";
        private readonly ILogger _log;
        public DM_DungChungRepository(ILog logger, ILogger log) : base(TableName)
        {
            _logger = logger;
            _log = log;
        }
        #region DanhMuc
        public List<DM_DungChungMap> DM_DungChung_List(DM_DungChungMapParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("TuKhoa", model.TuKhoa, DbType.String, ParameterDirection.Input);
                    paramters.Add("Table", model.Table, DbType.String, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<DM_DungChungMap>("DM_DungChung_List", paramters, commandType: CommandType.StoredProcedure)
                                    .ToList() ?? new List<DM_DungChungMap>();
                    restStatus = new ResponseModel();
                    if (datas != null && datas.Count > 0)
                    {
                        return new List<DM_DungChungMap>(datas);
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
                _logger.Error("DM_DungChung_List Error: " + ex.StackTrace);
                //log db
                _log.Error("DM_DungChung_List Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public DM_DungChungMapAdd DM_DungChung_GetByID(long ID, string Table, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", ID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("Table", Table, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<DM_DungChungMapAdd>("DM_DungChung_GetByID", paramters, commandType: CommandType.StoredProcedure);
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
                _logger.Error("DM_DungChung_GetByID Error: " + ex.StackTrace);
                //log db
                _log.Error("DM_DungChung_GetByID Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long DM_DungChung_InsUpd(DM_DungChungMapAdd model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", model.ID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("Table", model.Table, DbType.String, ParameterDirection.Input);
                    paramters.Add("Ma", model.Ma, DbType.String, ParameterDirection.Input);
                    paramters.Add("Ten", model.Ten, DbType.String, ParameterDirection.Input);
                    paramters.Add("MoTa", model.MoTa, DbType.String, ParameterDirection.Input);
                    paramters.Add("CreatedUserID", model.CreatedUserID, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<long>("DM_DungChung_InsUpd", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_DungChung_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("DM_DungChung_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int DM_DungChung_Del(long ID, string Table, Guid lastupduserid, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", ID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("Table", Table, DbType.String, ParameterDirection.Input);
                    paramters.Add("CreatedUserID", lastupduserid, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("DM_DungChung_Del", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_DungChung_Del Error: " + ex.StackTrace);
                //log db
                _log.Error("DM_DungChung_Del Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public DM_DungChungMapAdd DM_DungChung_GetByMa(string ma, string Table, out ResponseModel restStatus)//string
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Ma", ma, DbType.String, ParameterDirection.Input);
                    paramters.Add("Table", Table, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<DM_DungChungMapAdd>("DM_DungChung_GetByMa", paramters, commandType: CommandType.StoredProcedure);
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
                _logger.Error("DM_DungChung_GetByMa Error: " + ex.StackTrace);
                //log db
                _log.Error("DM_DungChung_GetByMa Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        #endregion DanhMuc
    }
}
