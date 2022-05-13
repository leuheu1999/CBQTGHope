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
    public class DM_LoaiHinhDangKyRepository : Repository<DM_LoaiHinhDangKyMap>, IDM_LoaiHinhDangKyRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(DM_LoaiHinhDangKyRepository));
        private const string TableName = "";
        private readonly ILogger _log;
        public DM_LoaiHinhDangKyRepository(ILog logger, ILogger log) : base(TableName)
        {
            _logger = logger;
            _log = log;
        }

        public long DM_LoaiHinhDangKy_InsUpd(DM_LoaiHinhDangKyMapAdd model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("LoaiHinhID", model.LoaiHinhId, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("TenLoaiHinh", model.TenLoaiHinh, DbType.String, ParameterDirection.Input);
                    paramters.Add("Ma", model.Ma, DbType.String, ParameterDirection.Input);
                    paramters.Add("MoTa", model.MoTa, DbType.String, ParameterDirection.Input);
                    paramters.Add("IsActive", model.IsActive, DbType.Boolean, ParameterDirection.Input);
                    paramters.Add("CreatedUserID", model.CreatedUserID, DbType.Guid, ParameterDirection.Input);
                    paramters.Add("LastUpdUserID", model.LastUpdUserID, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<long>("DM_LoaiHinhDangKy_InsUpd", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DM_LoaiHinhDangKy_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("DM_LoaiHinhDangKy_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }

        public List<DM_LoaiHinhDangKyMap> DM_LoaiHinhDangKy_List(DM_LoaiHinhDangKyMapParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("TuKhoa", model.TuKhoa, DbType.String, ParameterDirection.Input);
                    paramters.Add("IsActive", model.IsActive, DbType.Boolean, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<DM_LoaiHinhDangKyMap>("DM_LoaiHinhDangKy_GetByCodition", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<DM_LoaiHinhDangKyMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_LoaiHinhDangKy_List Error: " + ex.StackTrace);
                //log db
                _log.Error("DM_LoaiHinhDangKy_List Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }

        public DM_LoaiHinhDangKyMapAdd DM_LoaiHinhDangKy_GetById(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<DM_LoaiHinhDangKyMapAdd>("DM_LoaiHinhDangKy_GetById", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as DM_LoaiHinhDangKyMapAdd ?? datas;
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_LoaiHinhDangKy_GetById Error: " + ex.StackTrace);
                //log db
                _log.Error("DM_LoaiHinhDangKy_GetById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public DM_LoaiHinhDangKyMapAdd DM_LoaiHinhDangKy_GetByMa(string ma, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Ma", ma, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<DM_LoaiHinhDangKyMapAdd>("DM_LoaiHinhDangKy_GetByMa", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as DM_LoaiHinhDangKyMapAdd ?? datas;
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_LoaiHinhDangKy_GetByMa Error: " + ex.StackTrace);
                //log db
                _log.Error("DM_LoaiHinhDangKy_GetByMa Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }

        public bool DM_LoaiHinhDangKy_Delete(long id, Guid userId, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("DeletedUserID", userId, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("DM_LoaiHinhDangKy_Del", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas == 1 ? true : false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DM_LoaiHinhDangKy_Delete Error: " + ex.StackTrace);
                //log db
                _log.Error("DM_LoaiHinhDangKy_Delete Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return false;
            }
        }
    }
}
