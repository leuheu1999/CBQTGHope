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
    public class TT_DinhKemRepository : Repository<TT_DinhKemMap>, ITT_DinhKemRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(TT_DinhKemRepository));
        private const string TableName = "";
        private readonly ILogger _log;
        public TT_DinhKemRepository(ILog logger, ILogger log) : base(TableName)
        {
            _logger = logger;
            _log = log;
        }
        public List<TT_DinhKemMap> TT_DinhKem_List(TT_DinhKemParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Ten", model.Ten, DbType.String, ParameterDirection.Input);
                    paramters.Add("Tag", model.Tag, DbType.String, ParameterDirection.Input);
                    paramters.Add("GhiChu", model.GhiChu, DbType.String, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<TT_DinhKemMap>("TT_DinhKem_List", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<TT_DinhKemMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("TT_DinhKem_List Error: " + ex.StackTrace);
                //log db
                _log.Error("TT_DinhKem_List Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public TT_DinhKemAdd TT_DinhKem_ById(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new TT_DinhKemAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    result = conns.QueryFirstOrDefault<TT_DinhKemAdd>("TT_DinhKem_ById", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return result as TT_DinhKemAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("TT_DinhKem_ById Error: " + ex.StackTrace);
                //log db
                _log.Error("TT_DinhKem_ById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long TT_DinhKem_InsUpd(TT_DinhKemAdd model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("DinhKemID", model.DinhKemID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("Ten", model.Ten, DbType.String, ParameterDirection.Input);
                    paramters.Add("TenTepTin", model.TenTepTin, DbType.String, ParameterDirection.Input);
                    paramters.Add("TenGoc", model.TenGoc, DbType.String, ParameterDirection.Input);
                    paramters.Add("Tag", model.Tag, DbType.String, ParameterDirection.Input);
                    paramters.Add("GhiChu", model.GhiChu, DbType.String, ParameterDirection.Input);
                    paramters.Add("DuongDan", model.DuongDan, DbType.String, ParameterDirection.Input);
                    paramters.Add("UserID", model.UserID, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<long>("TT_DinhKem_InsUpd", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("TT_DinhKem_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("TT_DinhKem_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int TT_DinhKem_Del(long id, Guid userId, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("UserID", userId, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("TT_DinhKem_Del", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("TT_DinhKem_Del Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("TT_DinhKem_Del Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
    }
}