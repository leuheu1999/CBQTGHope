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
    public class DM_QuocGiaRepository : Repository<DM_QuocGiaMap>, IDM_QuocGiaRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(DM_QuocGiaRepository));
        private const string TableName = "";
        private readonly ILogger _log;
        public DM_QuocGiaRepository(ILog logger, ILogger log) : base(TableName)
        {
            _logger = logger;
            _log = log;
        }
        public List<DM_QuocGiaMap> DM_QuocGia_List(DM_QuocGiaMapParam model, out ResponseModel restStatus)
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
                    var datas = conns.Query<DM_QuocGiaMap>("DM_QuocGia_Lst", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<DM_QuocGiaMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_QuocGia_List Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("DM_QuocGia_List Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public DM_QuocGiaMapAdd DM_QuocGia_GetById(long quocGiaId, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("QuocGiaID", quocGiaId, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<DM_QuocGiaMapAdd>("DM_QuocGia_GetByID", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as DM_QuocGiaMapAdd ?? datas;
                }
            }
            catch (Exception ex)
            {
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_QuocGia_GetById Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("DM_QuocGia_GetById Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public DM_QuocGiaMapAdd DM_QuocGia_GetByMa(string maQuocGia, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("MaQuocGia", maQuocGia, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<DM_QuocGiaMapAdd>("DM_QuocGia_GetByMa", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as DM_QuocGiaMapAdd ?? datas;
                }
            }
            catch (Exception ex)
            {
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_QuocGia_GetByMa Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("DM_QuocGia_GetByMa Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long DM_QuocGia_InsUpd(DM_QuocGiaMapAdd model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("QuocGiaID", model.QuocGiaID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("MaQuocGia", model.MaQuocGia, DbType.String, ParameterDirection.Input);
                    paramters.Add("TenQuocGia", model.TenQuocGia, DbType.String, ParameterDirection.Input);
                    paramters.Add("MoTa", model.MoTa, DbType.String, ParameterDirection.Input);
                    paramters.Add("Used", model.Used, DbType.Boolean, ParameterDirection.Input);
                    paramters.Add("ThuTuHienThi", model.ThuTuHienThi, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("CreatedUserID", model.CreatedUserID, DbType.Guid, ParameterDirection.Input);
                    paramters.Add("LastUpdUserID", model.LastUpdUserID, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<long>("DM_QuocGia_InsUpd", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {            
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_QuocGia_InsUpd Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("DM_QuocGia_InsUpd Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int DM_QuocGia_Del(long id, Guid createduserid, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("id", id, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("LastUpdUserID", createduserid, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("DM_QuocGia_Del", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_QuocGia_Del Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("DM_QuocGia_Del Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
    }
}
