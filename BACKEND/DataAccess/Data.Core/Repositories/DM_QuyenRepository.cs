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
    public class DM_QuyenRepository : Repository<DM_QuyenMap>, IDM_QuyenRepository
    {

        private readonly ILog _logger = LogManager.GetLogger(typeof(DM_QuyenRepository));
        private const string TableName = "";
        private readonly ILogger _log;

        public DM_QuyenRepository(ILog logger, ILogger log) : base(TableName)
        {
            _logger = logger;
            _log = log;
        }
        public List<DM_QuyenMap> DM_Quyen_List(DM_QuyenMapParam model,out ResponseModel restStatus)
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
                    var datas = conns.Query<DM_QuyenMap>("DM_Quyen_List", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<DM_QuyenMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_NhomQuyen_List Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("DM_NhomQuyen_List Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public DM_QuyenMapAdd DM_Quyen_GetById(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<DM_QuyenMapAdd>("DM_Quyen_ById", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as DM_QuyenMapAdd ?? datas;
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_Quyen_GetById Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("DM_Quyen_GetById Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public DM_QuyenMapAdd DM_Quyen_GetByMa(string ma, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Ma", ma, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<DM_QuyenMapAdd>("DM_Quyen_ByMa", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as DM_QuyenMapAdd ?? datas;
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_NhomQuyen_GetByMa Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("DM_NhomQuyen_GetByMa Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long DM_Quyen_InsUpd(DM_QuyenMapAdd model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Id", model.Id, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("Ma", model.Ma, DbType.String, ParameterDirection.Input);
                    paramters.Add("Ten", model.Ten, DbType.String, ParameterDirection.Input);
                    paramters.Add("MoTa", model.MoTa, DbType.String, ParameterDirection.Input);
                    paramters.Add("DefaultController", model.DefaultController, DbType.String, ParameterDirection.Input);
                    paramters.Add("ThuTuHienThi", model.ThuTuHienThi, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("CongKhai", model.CongKhai, DbType.Boolean, ParameterDirection.Input);
                    paramters.Add("CreatedUserID", model.CreatedUserID, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<long>("DM_Quyen_InsUpd", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DM_Quyen_InsUpd Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("DM_Quyen_InsUpd Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public bool DM_Quyen_Delete(long id, Guid userId, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("CreatedUserID", userId, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("DM_Quyen_Del", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas == 1 ? true : false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DM_Quyen_Delete Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("DM_Quyen_Delete Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return false;
            }
        }
    }
}
