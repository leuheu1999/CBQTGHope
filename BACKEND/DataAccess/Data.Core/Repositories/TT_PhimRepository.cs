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
    public class TT_PhimRepository : Repository<TT_PhimMap>, ITT_PhimRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(TT_PhimRepository));
        private const string TableName = "";
        private readonly ILogger _log;
        public TT_PhimRepository(ILog logger, ILogger log) : base(TableName)
        {
            _logger = logger;
            _log = log;
        }
        public List<TT_PhimMap> TT_Phim_List(TT_PhimParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("QuyenID", model.QuyenID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("LinhVucID", model.LinhVucID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("Ten", model.Ten, DbType.String, ParameterDirection.Input);
                    paramters.Add("Tag", model.Tag, DbType.String, ParameterDirection.Input);
                    paramters.Add("GhiChu", model.GhiChu, DbType.String, ParameterDirection.Input);
                    paramters.Add("Checked", model.Checked, DbType.Int16, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<TT_PhimMap>("TT_Phim_List", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<TT_PhimMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("TT_Phim_List Error: " + ex.StackTrace);
                //log db
                _log.Error("TT_Phim_List Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public TT_PhimAdd TT_Phim_ById(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new TT_PhimAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    result = conns.QueryFirstOrDefault<TT_PhimAdd>("TT_Phim_ById", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return result as TT_PhimAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("TT_Phim_ById Error: " + ex.StackTrace);
                //log db
                _log.Error("TT_Phim_ById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long TT_Phim_InsUpd(TT_PhimAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("PhimID", model.PhimID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("QuyenID", model.QuyenID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("LinhVucID", model.LinhVucID, DbType.Int32, ParameterDirection.Input);
                paramters.Add("Ten", model.Ten, DbType.String, ParameterDirection.Input);
                paramters.Add("TenTepTin", model.TenTepTin, DbType.String, ParameterDirection.Input);
                paramters.Add("TenGoc", model.TenGoc, DbType.String, ParameterDirection.Input);
                paramters.Add("Tag", model.Tag, DbType.String, ParameterDirection.Input);
                paramters.Add("GhiChu", model.GhiChu, DbType.String, ParameterDirection.Input);
                paramters.Add("DuongDan", model.DuongDan, DbType.String, ParameterDirection.Input);
                paramters.Add("UserID", model.UserID, DbType.Guid, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<long>("TT_Phim_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;
            }
            catch (Exception ex)
            {
                _logger.Error("TT_Phim_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("TT_Phim_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int TT_Phim_Del(long id, Guid userId, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("UserID", userId, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("TT_Phim_Del", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("TT_Phim_Del Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("TT_Phim_Del Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int TT_Phim_DelByQuyenID(long quyenId, int linhVucId, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("QuyenID", quyenId, DbType.Int64, ParameterDirection.Input);
                paramters.Add("LinhVucID", linhVucId, DbType.Int16, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<int>("TT_Phim_DelByQuyenID", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;
            }
            catch (Exception ex)
            {
                _logger.Error("TT_Phim_DelByQuyenID Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("TT_Phim_DelByQuyenID Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int DVC_TT_Phim_DelByQuyenID(long quyenId, int linhVucId, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("QuyenID", quyenId, DbType.Int64, ParameterDirection.Input);
                paramters.Add("LinhVucID", linhVucId, DbType.Int16, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<int>("DVC_TT_Phim_DelByQuyenID", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_TT_Phim_DelByQuyenID Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("DVC_TT_Phim_DelByQuyenID Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public long DVC_TT_Phim_InsUpd(TT_PhimAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("PhimID", model.PhimID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("QuyenID", model.QuyenID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("LinhVucID", model.LinhVucID, DbType.Int32, ParameterDirection.Input);
                paramters.Add("Ten", model.Ten, DbType.String, ParameterDirection.Input);
                paramters.Add("TenTepTin", model.TenTepTin, DbType.String, ParameterDirection.Input);
                paramters.Add("TenGoc", model.TenGoc, DbType.String, ParameterDirection.Input);
                paramters.Add("Tag", model.Tag, DbType.String, ParameterDirection.Input);
                paramters.Add("GhiChu", model.GhiChu, DbType.String, ParameterDirection.Input);
                paramters.Add("DuongDan", model.DuongDan, DbType.String, ParameterDirection.Input);
                paramters.Add("UserID", model.UserID, DbType.Guid, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<long>("DVC_TT_Phim_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_TT_Phim_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_TT_Phim_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
    }
}
