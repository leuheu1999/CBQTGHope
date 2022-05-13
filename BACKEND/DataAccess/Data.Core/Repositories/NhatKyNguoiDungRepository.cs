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
    public class NhatKyNguoiDungRepository : Repository<ND_NhatKyNguoiDungMap>, INhatKyNguoiDungRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(NhatKyNguoiDungRepository));
        private const string TableName = "";
        private readonly ILogger _log;
        public NhatKyNguoiDungRepository(ILog logger, ILogger log) : base(TableName)
        {
            _logger = logger;
            _log = log;
        }
        public long NhatKyNguoiDung_Insert(ND_NhatKyNguoiDungAdd model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("NguoiDungID", model.NguoiDungID, DbType.Guid, ParameterDirection.Input);
                    paramters.Add("TenTaiKhoan", model.TenTaiKhoan, DbType.String, ParameterDirection.Input);
                    paramters.Add("NoiDung", model.NoiDung, DbType.String, ParameterDirection.Input);
                    paramters.Add("Controller", model.Controller, DbType.String, ParameterDirection.Input);
                    paramters.Add("Action", model.Action, DbType.String, ParameterDirection.Input);
                    paramters.Add("LogTypeID", model.LogTypeID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("KeyWord", model.KeyWord, DbType.String, ParameterDirection.Input);
                    paramters.Add("LogTypeName", model.LogTypeName, DbType.String, ParameterDirection.Input);
                    paramters.Add("DiaChiIP", model.DiaChiIP, DbType.String, ParameterDirection.Input);

                    var datas = conns.QueryFirstOrDefault<long>("ND_NhatKyNguoiDung_InsUpd", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("NhatKyNguoiDung_Insert Error: " + ex.StackTrace);
                //log db
                _log.Error("NhatKyNguoiDung_Insert Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public LogTypeNDmap LogTypeND_ByKeyWord(string keyword, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("KeyWord", keyword, DbType.String, ParameterDirection.Input);
                    var datas = conns.QuerySingleOrDefault<LogTypeNDmap>("ND_LogTypeND_ByKeyWord", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("LogTypeND_ByKeyWord Error: " + ex.StackTrace);
                //log db
                _log.Error("LogTypeND_ByKeyWord Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }

        public List<ND_NhatKyNguoiDungMap> NhatKyNguoiDung_List(ND_NhatKyNguoiDungParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("TuKhoa", model.TuKhoa, DbType.String, ParameterDirection.Input);
                    paramters.Add("TuNgay", model.TuNgay, DbType.String, ParameterDirection.Input);
                    paramters.Add("DenNgay", model.DenNgay, DbType.String, ParameterDirection.Input);
                    paramters.Add("LogTypeID", model.LogTypeID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<ND_NhatKyNguoiDungMap>("ND_NhatKyNguoiDung_GetByCodition", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<ND_NhatKyNguoiDungMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("NhatKyNguoiDung_List Error: " + ex.StackTrace);
                //log db
                _log.Error("NhatKyNguoiDung_List Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public int NhatKyNguoiDung_Del(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QuerySingleOrDefault<int>("ND_NhatKyNguoiDung_Del", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("NhatKyNguoiDung_Del Error: " + ex.StackTrace);
                //log db
                _log.Error("NhatKyNguoiDung_Del Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int NhatKyNguoiDung_DelLstID(List<long> lstid, out ResponseModel restStatus)
        {
            try
            {
                string ids = "";
                if (lstid != null && lstid.Count > 0)
                {
                    foreach (var i in lstid)
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
                    var datas = conns.QueryFirstOrDefault<int>("NhatKyNguoiDung_DelLstID", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("NhatKyNguoiDung_DelLstID Error: " + ex.StackTrace);
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public List<DSLogTypeNDmap> LogTypeND_List(LogTypeNDParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("TuKhoa", model.TuKhoa, DbType.String, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<DSLogTypeNDmap>("E_LogTypeND_GetByCodition", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<DSLogTypeNDmap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("LogTypeND_List Error: " + ex.StackTrace);
                //log db
                _log.Error("LogTypeND_List Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public int LogTypeND_Update(string lstid, string lstidUn, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("IDs", lstid, DbType.String, ParameterDirection.Input);
                    paramters.Add("IDsUn", lstidUn, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("E_LogTypeND_Update", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("LogTypeND_Update Error: " + ex.StackTrace);
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
    }
}
