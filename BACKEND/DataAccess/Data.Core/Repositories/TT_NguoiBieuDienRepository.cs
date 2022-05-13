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
using System.Text;
using System.Threading.Tasks;

namespace Data.Core.Repositories
{
    public class TT_NguoiBieuDienRepository : Repository<TT_NguoiBieuDienMap>, ITT_NguoiBieuDienRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(TT_NguoiBieuDienRepository));
        private const string TableName = "";
        private readonly ILogger _log;
        public TT_NguoiBieuDienRepository(ILog logger, ILogger log) : base(TableName)
        {
            _logger = logger;
            _log = log;
        }
        public List<TT_NguoiBieuDienMap> TT_NguoiBieuDien_List(TT_NguoiBieuDienParam model, out ResponseModel restStatus)
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
                    paramters.Add("DiaChi", model.DiaChi, DbType.String, ParameterDirection.Input);
                    paramters.Add("ButDanh", model.ButDanh, DbType.String, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<TT_NguoiBieuDienMap>("TT_NguoiBieuDien_List", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<TT_NguoiBieuDienMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("TT_NguoiBieuDien_List Error: " + ex.StackTrace);
                //log db
                _log.Error("TT_NguoiBieuDien_List Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public TT_NguoiBieuDienAdd TT_NguoiBieuDien_ById(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new TT_NguoiBieuDienAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    result = conns.QueryFirstOrDefault<TT_NguoiBieuDienAdd>("TT_NguoiBieuDien_ById", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return result as TT_NguoiBieuDienAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("TT_NguoiBieuDien_ById Error: " + ex.StackTrace);
                //log db
                _log.Error("TT_NguoiBieuDien_ById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public int TT_NguoiBieuDien_CheckCCCD(string cccd, long nguoiBieuDienID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("CCCD", cccd, DbType.String, ParameterDirection.Input);
                    paramters.Add("NguoiBieuDienID", nguoiBieuDienID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("TT_NguoiBieuDien_CheckCCCD", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("TT_NguoiBieuDien_CheckCCCD Error: " + ex.StackTrace);
                //log db
                _log.Error("TT_NguoiBieuDien_CheckCCCD Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public long TT_NguoiBieuDien_InsUpd(TT_NguoiBieuDienAdd model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("NguoiBieuDienID", model.NguoiBieuDienID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("HoVaTen", model.HoVaTen, DbType.String, ParameterDirection.Input);
                    paramters.Add("QuocTichID", model.QuocTichID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("QuocTich", model.QuocTich, DbType.String, ParameterDirection.Input);
                    paramters.Add("SoCMND", model.SoCMND, DbType.String, ParameterDirection.Input);
                    paramters.Add("NgayCap", model.NgayCapCMND, DbType.String, ParameterDirection.Input);
                    paramters.Add("NoiCapID", model.NoiCapID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("NoiCap", model.NoiCap, DbType.String, ParameterDirection.Input);
                    paramters.Add("DiaChi", model.DiaChi, DbType.String, ParameterDirection.Input);
                    paramters.Add("ButDanh", model.ButDanh, DbType.String, ParameterDirection.Input);
                    paramters.Add("UserID", model.UserID, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<long>("TT_NguoiBieuDien_InsUpd", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("TT_NguoiBieuDien_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("TT_NguoiBieuDien_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
    }
}