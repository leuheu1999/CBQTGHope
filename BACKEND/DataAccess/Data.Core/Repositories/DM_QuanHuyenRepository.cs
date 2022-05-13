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
    public class DM_QuanHuyenRepository : Repository<DM_QuanHuyenMap>, IDM_QuanHuyenRepository
    {

        private readonly ILog _logger = LogManager.GetLogger(typeof(DM_QuanHuyenRepository));
        private const string TableName = "";
        private readonly ILogger _log;
        public DM_QuanHuyenRepository(ILog logger, ILogger log) : base(TableName)
        {
            _logger = logger;
            _log = log;
        }
        public List<DM_QuanHuyenMap> DM_QuanHuyen_List(DM_QuanHuyenMapParam model,out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("TuKhoa", model.TuKhoa, DbType.String, ParameterDirection.Input);
                    paramters.Add("TinhThanhID", model.TinhThanhID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<DM_QuanHuyenMap>("DM_QuanHuyen_GetByCodition", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<DM_QuanHuyenMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_QuanHuyen_List Error: " + ex.StackTrace);
                //log db
                _log.Error("DM_QuanHuyen_List Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public DM_QuanHuyenMapAdd DM_QuanHuyen_GetById(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<DM_QuanHuyenMapAdd>("DM_QuanHuyen_GetByID", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as DM_QuanHuyenMapAdd ?? datas;
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_QuanHuyen_GetById Error: " + ex.StackTrace);
                //log db
                _log.Error("DM_QuanHuyen_GetById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public DM_QuanHuyenMapAdd DM_QuanHuyen_GetByMa(string ma, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Ma", ma, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<DM_QuanHuyenMapAdd>("DM_QuanHuyen_GetByMa", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as DM_QuanHuyenMapAdd ?? datas;
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_QuanHuyen_GetByMa Error: " + ex.StackTrace);
                //log db
                _log.Error("DM_QuanHuyen_GetByMa Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long DM_QuanHuyen_InsUpd(DM_QuanHuyenMapAdd model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("TinhThanhID", model.TinhThanhID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("Id", model.Id, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("Ma", model.Ma, DbType.String, ParameterDirection.Input);
                    paramters.Add("MaLienThong", model.MaLienThong, DbType.String, ParameterDirection.Input);
                    paramters.Add("Ten", model.Ten, DbType.String, ParameterDirection.Input);
                    paramters.Add("MoTa", model.MoTa, DbType.String, ParameterDirection.Input);
                    paramters.Add("CongKhai", model.CongKhai, DbType.Boolean, ParameterDirection.Input);
                    paramters.Add("ThuTuHienThi", model.ThuTuHienThi, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("CreatedUserID", model.CreatedUserID, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<long>("DM_QuanHuyen_InsUpd", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DM_QuanHuyen_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("DM_QuanHuyen_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public bool DM_QuanHuyen_Delete(long id, Guid userId, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("LastUpdUserID", userId, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("DM_QuanHuyen_Del", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas == 1 ? true : false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DM_QuanHuyen_Delete Error: " + ex.StackTrace);
                //log db
                _log.Error("DM_QuanHuyen_Delete Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return false;
            }
        }
    }
}
