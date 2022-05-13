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
    public class DM_TinhThanhRepository : Repository<DM_TinhThanhMap>, IDM_TinhThanhRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(DM_TinhThanhRepository));
        private const string TableName = "";
        private readonly ILogger _log;
        public DM_TinhThanhRepository(ILog logger, ILogger log) : base(TableName)
        {
            _logger = logger;
            _log = log;
        }
        public List<DM_TinhThanhMap> DM_TinhThanh_List(DM_TinhThanhMapParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("TuKhoa", model.TuKhoa, DbType.String, ParameterDirection.Input);
                    paramters.Add("QuocGiaID", model.QuocGiaID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<DM_TinhThanhMap>("DM_TinhThanh_GetByCodition", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<DM_TinhThanhMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_TinhThanh_List Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("DM_TinhThanh_List Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public DM_TinhThanhMapAdd DM_TinhThanh_GetById(long Id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", Id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<DM_TinhThanhMapAdd>("DM_TinhThanh_GetByID", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as DM_TinhThanhMapAdd ?? datas;
                }
            }
            catch (Exception ex)
            {
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_TinhThanh_GetById Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("DM_TinhThanh_GetById Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public DM_TinhThanhMapAdd DM_TinhThanh_GetByMa(string Ma, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Ma", Ma, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<DM_TinhThanhMapAdd>("DM_TinhThanh_GetByMa", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as DM_TinhThanhMapAdd ?? datas;
                }
            }
            catch (Exception ex)
            {
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_TinhThanh_GetByMa Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("DM_TinhThanh_GetByMa Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long DM_TinhThanh_InsUpd(DM_TinhThanhMapAdd model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", model.ID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("QuocGiaID", model.QuocGiaID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("Ma", model.Ma, DbType.String, ParameterDirection.Input);
                    paramters.Add("MaLienThong", model.MaLienThong, DbType.String, ParameterDirection.Input);
                    paramters.Add("Ten", model.Ten, DbType.String, ParameterDirection.Input);
                    paramters.Add("MoTa", model.MoTa, DbType.String, ParameterDirection.Input);
                    paramters.Add("CongKhai", model.CongKhai, DbType.Boolean, ParameterDirection.Input);
                    paramters.Add("ThuTuHienThi", model.ThuTuHienThi, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("CreatedUserID", model.CreatedUserID, DbType.Guid, ParameterDirection.Input);
                    paramters.Add("LastUpdUserID", model.LastUpdUserID, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<long>("DM_TinhThanh_InsUpd", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {            
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_TinhThanh_InsUpd Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("DM_TinhThanh_InsUpd Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int DM_TinhThanh_Del(long id, Guid createduserid, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("LastUpdUserID", createduserid, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("DM_TinhThanh_Del", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_TinhThanh_Del Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("DM_TinhThanh_Del Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
    }
}
