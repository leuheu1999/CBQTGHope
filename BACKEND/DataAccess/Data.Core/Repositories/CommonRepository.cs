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
    public class CommonRepository : Repository<PageMap>, IPageRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(PageRepository));
        private const string TableName = "";
        private readonly ILogger _log;
        public CommonRepository(ILog logger, ILogger log) : base(TableName)
        {
            _logger = logger;
            _log = log;
        }
        public List<PageMap> Pages_List(pageMapParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = BaoDienTuConn)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("TuKhoa", model.TuKhoa, DbType.String, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<PageMap>("Page_List", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<PageMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Pages_List Error: " + ex.StackTrace);
                //log db
                _log.Error("Pages_List Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public PageMapAdd Page_GetById(long Id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = BaoDienTuConn)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", Id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<PageMapAdd>("Page_ByID", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as PageMapAdd ?? datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Page_GetById Error: " + ex.StackTrace);
                //log db
                _log.Error("Page_GetById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public PageMapAdd Page_ByPageName(string pagename, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = BaoDienTuConn)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("PageName", pagename, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<PageMapAdd>("Page_ByPageName", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as PageMapAdd ?? datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Page_ByPageName Error: " + ex.StackTrace);
                //log db
                _log.Error("Page_ByPageName Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long Page_InsUpd(PageMapAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("ID", model.ID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("PageName", model.PageName, DbType.String, ParameterDirection.Input);
                paramters.Add("Title", model.Title, DbType.String, ParameterDirection.Input);
                paramters.Add("Body", model.Body, DbType.String, ParameterDirection.Input);
                paramters.Add("ThuTuHienThi", model.ThuTuHienThi, DbType.Int32, ParameterDirection.Input);
                paramters.Add("Used", model.Used, DbType.Boolean, ParameterDirection.Input);

                var datas = conns.QueryFirstOrDefault<long>("Page_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("Page_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("Page_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int Page_Del(long id, Guid lastupduserid, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<int>("Page_Del", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("Page_Del Error: " + ex.StackTrace);
                //log db
                _log.Error("Page_Del Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public long UrlRecord_InsUpd(UrlRecordMapAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("EntityId", model.EntityId, DbType.Int64, ParameterDirection.Input);
                paramters.Add("EntityName", model.EntityName, DbType.String, ParameterDirection.Input);
                paramters.Add("Slug", model.Slug, DbType.String, ParameterDirection.Input);
                paramters.Add("IsActive", model.IsActive, DbType.Boolean, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<long>("UrlRecord_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("UrlRecord_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("UrlRecord_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public long UrlRecord_DelByPageID(UrlRecordMapAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("EntityId", model.EntityId, DbType.Int64, ParameterDirection.Input);
                paramters.Add("EntityName", model.EntityName, DbType.String, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<long>("UrlRecord_DelByPageID", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("UrlRecord_DelByPageID Error: " + ex.StackTrace);
                //log db
                _log.Error("UrlRecord_DelByPageID Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
    }
}
