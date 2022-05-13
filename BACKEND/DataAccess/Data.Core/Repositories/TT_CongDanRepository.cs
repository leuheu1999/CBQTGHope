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
    public class TT_CongDanRepository : Repository<TT_CongDanMap>, ITT_CongDanRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(TT_CongDanRepository));
        private const string TableName = "";
        private readonly ILogger _log;
        public TT_CongDanRepository(ILog logger, ILogger log) : base(TableName)
        {
            _logger = logger;
            _log = log;
        }
        public List<TT_CongDanMap> TT_CongDan_List(TT_CongDanParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("HoVaTen", model.HoVaTen, DbType.String, ParameterDirection.Input);
                    paramters.Add("SoCMND", model.SoCMND, DbType.String, ParameterDirection.Input);
                    paramters.Add("NgayCap", model.NgayCap, DbType.String, ParameterDirection.Input);
                    paramters.Add("DiaChi", model.DiaChi, DbType.String, ParameterDirection.Input);
                    paramters.Add("SoDKKD", model.SoDKKD, DbType.String, ParameterDirection.Input);
                    paramters.Add("NgayCapDKKD", model.NgayCapDKKD, DbType.String, ParameterDirection.Input);
                    paramters.Add("ButDanh", model.ButDanh, DbType.String, ParameterDirection.Input);
                    paramters.Add("Key", model.Key, DbType.String, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<TT_CongDanMap>("TT_CongDan_List", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<TT_CongDanMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("TT_CongDan_List Error: " + ex.StackTrace);
                //log db
                _log.Error("TT_CongDan_List Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public TT_CongDanAdd TT_CongDan_ById(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new TT_CongDanAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    result = conns.QueryFirstOrDefault<TT_CongDanAdd>("TT_CongDan_ById", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return result as TT_CongDanAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("TT_CongDan_ById Error: " + ex.StackTrace);
                //log db
                _log.Error("TT_CongDan_ById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public int TT_CongDan_CheckCCCD(string cccd, long congDanID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("CCCD", cccd, DbType.String, ParameterDirection.Input);
                    paramters.Add("CongDanID", congDanID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("TT_CongDan_CheckCCCD", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("TT_CongDan_CheckCCCD Error: " + ex.StackTrace);
                //log db
                _log.Error("TT_CongDan_CheckCCCD Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int TT_CongDan_CheckDKKD(string dkkd, long congDanID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("DKKD", dkkd, DbType.String, ParameterDirection.Input);
                    paramters.Add("CongDanID", congDanID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("TT_CongDan_CheckDKKD", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("TT_CongDan_CheckDKKD Error: " + ex.StackTrace);
                //log db
                _log.Error("TT_CongDan_CheckDKKD Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public long TT_CongDan_InsUpd(TT_CongDanAdd model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("CongDanID", model.CongDanID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("HoVaTen", model.HoVaTen, DbType.String, ParameterDirection.Input);
                    paramters.Add("QuocTichID", model.QuocTichID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("QuocTich", model.QuocTich, DbType.String, ParameterDirection.Input);
                    paramters.Add("SoCMND", model.SoCMND, DbType.String, ParameterDirection.Input);
                    paramters.Add("NgayCap", model.NgayCapCMND, DbType.String, ParameterDirection.Input);
                    paramters.Add("NoiCapID", model.NoiCapID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("NoiCap", model.NoiCap, DbType.String, ParameterDirection.Input);
                    paramters.Add("SoDKKD", model.SoDKKD, DbType.String, ParameterDirection.Input);
                    paramters.Add("NgayCapDKKD", model.NgayCapDKKD, DbType.String, ParameterDirection.Input);
                    paramters.Add("NoiCapDKKDID", model.NoiCapDKKDID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("NoiCapDKKD", model.NoiCapDKKD, DbType.String, ParameterDirection.Input);
                    paramters.Add("ButDanh", model.ButDanh, DbType.String, ParameterDirection.Input);
                    paramters.Add("DiaChi", model.DiaChi, DbType.String, ParameterDirection.Input);
                    paramters.Add("UserID", model.UserID, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<long>("TT_CongDan_InsUpd", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("TT_CongDan_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("TT_CongDan_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
    }
}

