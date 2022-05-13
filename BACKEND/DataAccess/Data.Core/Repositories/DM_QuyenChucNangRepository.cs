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
    public class DM_QuyenChucNangRepository : Repository<DM_QuyenChucNangMap>, IDM_QuyenChucNangRepository
    {

        private readonly ILog _logger = LogManager.GetLogger(typeof(DM_QuyenChucNangRepository));
        private const string TableName = "";
        private readonly ILogger _log;

        public DM_QuyenChucNangRepository(ILog logger, ILogger log) : base(TableName)
        {
            _logger = logger;
            _log = log;
        }
        public List<DM_QuyenChucNangMap> DM_QuyenChucNang_List(DM_QuyenChucNangMapParam model,out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("TuKhoa", model.TuKhoa, DbType.String, ParameterDirection.Input);
                    paramters.Add("QuyenID", model.QuyenID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<DM_QuyenChucNangMap>("DM_QuyenChucNang_List", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<DM_QuyenChucNangMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_QuyenChucNang_List Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("DM_QuyenChucNang_List Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public DM_QuyenChucNangAdd DM_QuyenChucNang_GetById(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<DM_QuyenChucNangAdd>("DM_QuyenChucNang_ById", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as DM_QuyenChucNangAdd ?? datas;
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_QuyenChucNang_GetById Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("DM_QuyenChucNang_GetById Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public DM_QuyenChucNangAdd DM_QuyenChucNang_GetByMa(string ma, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Ma", ma, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<DM_QuyenChucNangAdd>("DM_QuyenChucNang_ByMa", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as DM_QuyenChucNangAdd ?? datas;
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DM_QuyenChucNang_GetByMa Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("DM_QuyenChucNang_GetByMa Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long DM_QuyenChuNang_InsUpd(DM_QuyenChucNangAdd model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    string cookie = GetCookie(conns);
                    string strCookieIn = "RCK";
                    int currentTT = 1;
                    if (!string.IsNullOrEmpty(cookie))
                    {
                        string[] arrSTT = cookie.Split(new string[] { "RCK" }, StringSplitOptions.None);
                        currentTT += int.Parse(arrSTT[1]);
                        strCookieIn += currentTT.ToString("D2");
                    }
                    else
                    {
                        strCookieIn += "01";
                    }
                    var paramters = new DynamicParameters();
                    paramters.Add("Id", model.Id, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("QuyenID", model.QuyenID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("CodeCookie", strCookieIn, DbType.String, ParameterDirection.Input);
                    paramters.Add("Ma", model.Ma, DbType.String, ParameterDirection.Input);
                    paramters.Add("Ten", model.Ten, DbType.String, ParameterDirection.Input);
                    paramters.Add("TenHienThi", model.TenHienThi, DbType.String, ParameterDirection.Input);
                    paramters.Add("MoTa", model.MoTa, DbType.String, ParameterDirection.Input);
                    paramters.Add("Action", model.Action, DbType.String, ParameterDirection.Input);
                    paramters.Add("Controller", model.Controller, DbType.String, ParameterDirection.Input);
                    paramters.Add("IsDefault", model.IsDefault, DbType.Boolean, ParameterDirection.Input);
                    paramters.Add("ThuTuHienThi", model.ThuTuHienThi, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("CongKhai", model.CongKhai, DbType.Boolean, ParameterDirection.Input);
                    paramters.Add("CreatedUserID", model.CreatedUserID, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<long>("DM_QuyenChucNang_InsUpd", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DM_QuyenChuNang_InsUpd Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("DM_QuyenChuNang_InsUpd Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public string GetCookie(IDbConnection conns)
        {
            try
            {
                var datas = conns.QueryFirstOrDefault<string>("DM_QuyenChucNang_CodeCookie", commandType: CommandType.StoredProcedure);
                return datas;
            }
            catch (Exception ex)
            {
                _logger.Error("GetCookie Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("GetCookie Error", ex, new Guid());
                return "";
            }
        }
        public bool DM_QuyenChucNang_Delete(long id, Guid userId, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Id", id, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("CreatedUserID", userId, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("DM_QuyenChucNang_Del", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas == 1 ? true : false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DM_QuyenChucNang_Delete Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("DM_QuyenChucNang_Delete Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return false;
            }
        }

    }
}
