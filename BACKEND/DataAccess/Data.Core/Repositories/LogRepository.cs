using Business.Entities;
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
    public class LogRepository : Repository<LogMap>, ILogRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(LogRepository));
        private const string TableName = "";
        public LogRepository(ILog logger) : base(TableName)
        {
            _logger = logger;
        }       
        public long InsertLog(LogAdd model, out ResponseModel restStatus)
        {
            try
            {               
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();                  
                    paramters.Add("LogLevelId", model.LogLevelId, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("ShortMessage", model.ShortMessage, DbType.String, ParameterDirection.Input);
                    paramters.Add("FullMessage", model.FullMessage, DbType.String, ParameterDirection.Input);
                    paramters.Add("CustomerId", model.CustomerId, DbType.Guid, ParameterDirection.Input);
                    paramters.Add("IpAddress", model.IpAddress, DbType.String, ParameterDirection.Input);
                    paramters.Add("PageUrl", model.PageUrl, DbType.String, ParameterDirection.Input);
                    paramters.Add("ReferrerUrl", model.ReferrerUrl, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<long>("LogSystem_Ins", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("InsertLog Error: " + ex.StackTrace);
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }

        public List<LogMap> LogSystem_List(LogParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("TuNgay", model.CreatedFrom, DbType.String, ParameterDirection.Input);
                    paramters.Add("DenNgay", model.CreatedTo, DbType.String, ParameterDirection.Input);
                    paramters.Add("TuKhoa", model.Message, DbType.String, ParameterDirection.Input);
                    paramters.Add("LoglevelID", model.LoglevelID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("TypeSort", model.TypeSort, DbType.String, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<LogMap>("LogSystem_List", paramters, commandType: CommandType.StoredProcedure)
                                    .ToList() ?? new List<LogMap>();
                    restStatus = new ResponseModel();
                    if (datas != null && datas.Count > 0)
                    {
                        return new List<LogMap>(datas);
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
                _logger.Error("LogSystem_List Error: " + ex.StackTrace);                
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public LogAdd GetLogSystemById(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("id", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<LogAdd>("LogSystem_ById", paramters, commandType: CommandType.StoredProcedure);
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
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("GetLogSystemById Error: " + ex.StackTrace);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public int LogSystem_DelByID(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("id", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("LogSystem_DelByID", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("LogSystem_DelByID Error: " + ex.StackTrace);               
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int LogSystem_DelLstID(List<long> lstid, out ResponseModel restStatus)
        {
            try
            {
                string ids = "";
                if(lstid!=null && lstid.Count>0)
                {
                    foreach(var i in lstid)
                    {
                        ids += i + ",";
                    }
                }
                ids = ids.Substring(0, ids.Length - 1);
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ids", ids, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("LogSystem_DelLstID", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("LogSystem_DelLstID Error: " + ex.StackTrace);
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int LogSystem_Trancate(out ResponseModel restStatus)
        {
            try
            {                
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();                 
                    var datas = conns.QueryFirstOrDefault<int>("LogSystem_Trancate", commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("LogSystem_Trancate Error: " + ex.StackTrace);
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
    }
}
