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
    public class TT_ChuSoHuuRepository : Repository<TT_ChuSoHuuMap>, ITT_ChuSoHuuRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(TT_ChuSoHuuRepository));
        private const string TableName = "";
        private readonly ILogger _log;
        public TT_ChuSoHuuRepository(ILog logger, ILogger log) : base(TableName)
        {
            _logger = logger;
            _log = log;
        }
        public List<TT_ChuSoHuuMap> TT_ChuSoHuu_List(TT_ChuSoHuuParam model, out ResponseModel restStatus)
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
                    paramters.Add("SoDKKD", model.SoDKKD, DbType.String, ParameterDirection.Input);
                    paramters.Add("NgayCapDKKD", model.NgayCapDKKD, DbType.String, ParameterDirection.Input);
                    paramters.Add("DiaChi", model.DiaChi, DbType.String, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<TT_ChuSoHuuMap>("TT_ChuSoHuu_List", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<TT_ChuSoHuuMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("TT_ChuSoHuu_List Error: " + ex.StackTrace);
                //log db
                _log.Error("TT_ChuSoHuu_List Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public TT_ChuSoHuuAdd TT_ChuSoHuu_ById(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new TT_ChuSoHuuAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    result = conns.QueryFirstOrDefault<TT_ChuSoHuuAdd>("TT_ChuSoHuu_ById", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return result as TT_ChuSoHuuAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("TT_ChuSoHuu_ById Error: " + ex.StackTrace);
                //log db
                _log.Error("TT_ChuSoHuu_ById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public int TT_ChuSoHuu_CheckCCCD(string cccd, long chuSoHuuID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("CCCD", cccd, DbType.String, ParameterDirection.Input);
                    paramters.Add("ChuSoHuuID", chuSoHuuID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("TT_ChuSoHuu_CheckCCCD", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("TT_ChuSoHuu_CheckCCCD Error: " + ex.StackTrace);
                //log db
                _log.Error("TT_ChuSoHuu_CheckCCCD Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public long TT_ChuSoHuu_InsUpd(TT_ChuSoHuuAdd model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ChuSoHuuID", model.ChuSoHuuID, DbType.Int64, ParameterDirection.Input);
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
                    paramters.Add("DiaChi", model.DiaChi, DbType.String, ParameterDirection.Input);
                    paramters.Add("UserID", model.UserID, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<long>("TT_ChuSoHuu_InsUpd", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("TT_ChuSoHuu_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("TT_ChuSoHuu_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
    }
}
