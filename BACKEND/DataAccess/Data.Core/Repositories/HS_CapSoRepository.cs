using Business.Entities.Domain;
using Core.Common.Utilities;
using Dapper;
using Data.Core.Repositories.Base;
using Data.Core.Repositories.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace Data.Core.Repositories
{
    public class HS_CapSoRepository : Repository<HS_CapSoMap>, IHS_CapSoRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(HS_CapSoRepository));
        private const string TableName = "";
        private readonly ILogger _log;
        public HS_CapSoRepository(ILog logger, ILogger log) : base(TableName)
        {
            _logger = logger;
            _log = log;
        }
      
        public HS_CapSoMapAdd HS_CapSo_ById(long capSoID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("CapSoID", capSoID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<HS_CapSoMapAdd>("HS_CapSo_ById", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("HS_CapSo_ById Error: " + ex.StackTrace);
                //log db
                _log.Error("HS_CapSo_ById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }

        public List<HS_CapSoMap> HS_CapSo_ByHoSoId(long hoSoID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("HoSoID", hoSoID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.Query<HS_CapSoMap>("HS_CapSo_ByHoSoId", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas.ToList() ?? new List<HS_CapSoMap>();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("HS_CapSo_ByHoSoId Error: " + ex.StackTrace);
                //log db
                _log.Error("HS_CapSo_ByHoSoId Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return new List<HS_CapSoMap>();
            }
        }

        public int HS_CapSo_CheckLoaiSo(long capSoID, int loaiSoID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("CapSoID", capSoID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("LoaiSoID", loaiSoID, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("HS_CapSo_CheckLoaiSo", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("HS_CapSo_ById Error: " + ex.StackTrace);
                //log db
                _log.Error("HS_CapSo_ById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int HS_CapSo_CheckSo(long capSoID, int loaiSoID, string so, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("CapSoID", capSoID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("LoaiSoID", loaiSoID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("So", so, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("HS_CapSo_CheckSo", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("HS_CapSo_CheckSo Error: " + ex.StackTrace);
                //log db
                _log.Error("HS_CapSo_CheckSo Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }

        public long HS_CapSo_InsUpd(HS_CapSoMapAdd param,IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("CapSoID", param.CapSoID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("LoaiSoID", param.LoaiSoID, DbType.Int32, ParameterDirection.Input);
                paramters.Add("So", param.So, DbType.String, ParameterDirection.Input);
                paramters.Add("Prefix", param.Prefix, DbType.String, ParameterDirection.Input);
                paramters.Add("NgayCap", param.NgayCap, DbType.String, ParameterDirection.Input);
                paramters.Add("CoQuanCapID", param.CoQuanCapID, DbType.Int32, ParameterDirection.Input);
                paramters.Add("NguoiKyID", param.NguoiKyID, DbType.Int32, ParameterDirection.Input);
                paramters.Add("GhiChu", param.GhiChu, DbType.String, ParameterDirection.Input);
                paramters.Add("HoSoID", param.HoSoID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("CreatedUserID", param.CreatedUserID, DbType.Guid, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<long>("HS_CapSo_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("HS_CapSo_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("HS_CapSo_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }

        public int HS_CapSo_Del(long capSoID, Guid createdUserID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("CapSoID", capSoID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("CreatedUserID", createdUserID, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("HS_CapSo_Del", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("HS_CapSo_Del Error: " + ex.StackTrace);
                //log db
                _log.Error("HS_CapSo_Del Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public long HS_CapSo_UpdChuyenNganh(HS_CapSoMapAdd param, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("LoaiSoID", param.LoaiSoID, DbType.Int32, ParameterDirection.Input);
                paramters.Add("So", param.So, DbType.String, ParameterDirection.Input);
                paramters.Add("Prefix", param.Prefix, DbType.String, ParameterDirection.Input);
                paramters.Add("NgayCap", param.NgayCap, DbType.String, ParameterDirection.Input);
                paramters.Add("HoSoID", param.HoSoID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("CreatedUserID", param.CreatedUserID, DbType.Guid, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<long>("HS_CapSo_UpdChuyenNganh", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("HS_CapSo_UpdChuyenNganh Error: " + ex.StackTrace);
                //log db
                _log.Error("HS_CapSo_UpdChuyenNganh Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }

        public HS_GenSo HS_CapSo_GenSo(int loaiNghiepVuID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("LoaiNghiepVuID", loaiNghiepVuID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<HS_GenSo>("HS_CapSo_GenSo", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("HS_CapSo_GenSo Error: " + ex.StackTrace);
                //log db
                _log.Error("HS_CapSo_GenSo Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }

        public HS_GenSo DVC_HS_CapSo_GenSo(int loaiNghiepVuID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("LoaiNghiepVuID", loaiNghiepVuID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<HS_GenSo>("DVC_HS_CapSo_GenSo", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_HS_CapSo_GenSo Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_HS_CapSo_GenSo Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }

        #region HS_HoSo
        public int HS_HoSo_CheckCapSo(long hoSoID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("HoSoID", hoSoID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("HS_HoSo_CheckCapSo", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("HS_HoSo_CheckCapSo Error: " + ex.StackTrace);
                //log db
                _log.Error("HS_HoSo_CheckCapSo Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        #endregion HS_HoSo
    }
}
