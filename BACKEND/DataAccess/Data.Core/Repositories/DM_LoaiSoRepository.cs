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
    public class DM_LoaiSoRepository : Repository<DM_LoaiSoMap>, IDM_LoaiSoRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(DM_LoaiSoRepository));
        private const string TableName = "";
        private readonly ILogger _log;
        public DM_LoaiSoRepository(ILog logger, ILogger log) : base(TableName)
        {
            _logger = logger;
            _log = log;
        }
      
        public DM_LoaiSoMapAdd DM_LoaiSo_ById(int ID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", ID, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<DM_LoaiSoMapAdd>("DM_LoaiSo_ById", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DM_LoaiSo_ById Error: " + ex.StackTrace);
                //log db
                _log.Error("DM_LoaiSo_ById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public string DM_LoaiSo_GenCapSo(int ID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", ID, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<string>("DM_LoaiSo_GenCapSo", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DM_LoaiSo_GenCapSo Error: " + ex.StackTrace);
                //log db
                _log.Error("DM_LoaiSo_GenCapSo Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return "";
            }
        }
        public int DM_LoaiSo_UpdCapSo(int ID, int SoHienTai, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("ID", ID, DbType.Int32, ParameterDirection.Input);
                paramters.Add("SoHienTai", SoHienTai, DbType.Int32, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<int>("DM_LoaiSo_UpdCapSo", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;
            }
            catch (Exception ex)
            {
                _logger.Error("DM_LoaiSo_UpdCapSo Error: " + ex.StackTrace);
                //log db
                _log.Error("DM_LoaiSo_UpdCapSo Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }

        public long DM_LoaiSo_InsUpd(DM_LoaiSoMapAdd model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Id", model.ID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("Ten", model.Ten, DbType.String, ParameterDirection.Input);
                    paramters.Add("SoHienTai", model.SoHienTai, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("Nam", model.Nam, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("Prefix", model.Prefix, DbType.String, ParameterDirection.Input);
                    paramters.Add("ResetTheoNam", model.ResetTheoNam, DbType.Boolean, ParameterDirection.Input);
                    paramters.Add("TuTang", model.TuTang, DbType.Boolean, ParameterDirection.Input);
                    paramters.Add("LoaiNghiepVuID", model.LoaiNghiepVuID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<long>("DM_LoaiSo_InsUpd", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DM_LoaiSo_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("DM_LoaiSo_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }

        public List<DM_LoaiSoMap> DM_LoaiSo_List(DM_LoaiSoMapParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("TuKhoa", model.TuKhoa, DbType.String, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<DM_LoaiSoMap>("DM_LoaiSo_GetByCodition", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<DM_LoaiSoMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_LoaiSo_List Error: " + ex.StackTrace);
                //log db
                _log.Error("DM_LoaiSo_List Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }

        public DM_LoaiSoMapAdd DM_LoaiSo_GetById(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<DM_LoaiSoMapAdd>("DM_LoaiSo_GetById", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as DM_LoaiSoMapAdd ?? datas;
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_LoaiSo_GetById Error: " + ex.StackTrace);
                //log db
                _log.Error("DM_LoaiSo_GetById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public DM_LoaiSoMapAdd DM_LoaiSo_GetByLoaiNghieVuId(long loaiNghiepVuId, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("loaiNghiepVuId", loaiNghiepVuId, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<DM_LoaiSoMapAdd>("DM_LoaiSo_GetByLoaiNghieVuId", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as DM_LoaiSoMapAdd ?? datas;
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_LoaiSo_GetByLoaiNghieVuId Error: " + ex.StackTrace);
                //log db
                _log.Error("DM_LoaiSo_GetByLoaiNghieVuId Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }

        public bool DM_LoaiSo_Delete(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("DM_LoaiSo_Del", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas == 1 ? true : false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DM_LoaiSo_Delete Error: " + ex.StackTrace);
                //log db
                _log.Error("DM_LoaiSo_Delete Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return false;
            }
        }
    }
}
