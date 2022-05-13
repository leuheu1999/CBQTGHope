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
    public class DM_PhuongXaRepository : Repository<DM_PhuongXaMap>, IDM_PhuongXaRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(DM_PhuongXaRepository));
        private const string TableName = "";
        private readonly ILogger _log;
        public DM_PhuongXaRepository(ILog logger, ILogger log) : base(TableName)
        {
            _logger = logger;
            _log = log;
        }
        public List<DM_PhuongXaMap> DM_PhuongXa_List(DM_PhuongXaMapParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("TuKhoa", model.TuKhoa, DbType.String, ParameterDirection.Input);
                    paramters.Add("QuanHuyenID", model.QuanHuyenID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("TinhThanhID", model.TinhThanhID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<DM_PhuongXaMap>("DM_PhuongXa_GetByCodition", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<DM_PhuongXaMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DM_PhuongXa_List Error: " + ex.StackTrace);
                //log db
                _log.Error("DM_PhuongXa_List Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public DM_PhuongXaMapAdd DM_PhuongXa_GetById(long phuongXaId, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("PhuongXaID", phuongXaId, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<DM_PhuongXaMapAdd>("DM_PhuongXa_GetByID", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as DM_PhuongXaMapAdd ?? datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DM_PhuongXa_GetById Error: " + ex.StackTrace);
                //log db
                _log.Error("DM_PhuongXa_GetById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public DM_PhuongXaMapAdd DM_PhuongXa_GetByMa(string maPhuongXa, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("MaPhuongXa", maPhuongXa, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<DM_PhuongXaMapAdd>("DM_PhuongXa_GetByMa", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as DM_PhuongXaMapAdd ?? datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DM_PhuongXa_GetByMa Error: " + ex.StackTrace);
                //log db
                _log.Error("DM_PhuongXa_GetByMa Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long DM_PhuongXa_InsUpd(DM_PhuongXaMapAdd model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", model.ID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("QuanHuyenID", model.QuanHuyenID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("Ma", model.Ma, DbType.String, ParameterDirection.Input);
                    paramters.Add("MaLienThong", model.MaLienThong, DbType.String, ParameterDirection.Input);
                    paramters.Add("Ten", model.Ten, DbType.String, ParameterDirection.Input);
                    paramters.Add("MoTa", model.MoTa, DbType.String, ParameterDirection.Input);
                    paramters.Add("ThuTuHienThi", model.ThuTuHienThi, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("CongKhai", model.CongKhai, DbType.Boolean, ParameterDirection.Input);
                    paramters.Add("CreatedUserID", model.CreatedUserID, DbType.Guid, ParameterDirection.Input);
                    paramters.Add("LastUpdUserID", model.LastUpdUserID, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<long>("DM_PhuongXa_InsUpd", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DM_PhuongXa_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("DM_PhuongXa_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int DM_PhuongXa_Del(long id, Guid createduserid, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("LastUpdUserID", createduserid, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("DM_PhuongXa_Del", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_PhuongXa_Del Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("DM_PhuongXa_Del Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
    }
}
