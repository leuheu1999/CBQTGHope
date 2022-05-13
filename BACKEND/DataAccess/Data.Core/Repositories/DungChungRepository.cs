using Business.Entities;
using Dapper;
using Data.Core.Repositories.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Core.Common.Utilities;
using Newtonsoft.Json.Linq;
using Business.Entities.Domain;
using Newtonsoft.Json;
using Business.Entities.Domain.Print;
using Data.Core.Repositories.Base;
using System.Data.SqlClient;

namespace Data.Core.Repositories
{
    public class DungChungRepository : Repository<DM_DungChungViewModel>, IDungChungRepository
    {

        private readonly ILog _logger = LogManager.GetLogger(typeof(DungChungRepository));
        private const string TableName = "DM_DungChungViewModel";
        private readonly ILogger _log;
        public DungChungRepository(ILog logger, ILogger log) : base(TableName)
        {
            _logger = logger;
            _log = log;
        }
        public List<CBO_DungChungViewModel> CBO_DungChung_GetAll(CBO_DungChungParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("TableName", model.TableName, DbType.String, ParameterDirection.Input);
                    paramters.Add("ParentID1", model.ParentID1, DbType.String, ParameterDirection.Input);
                    paramters.Add("ParentID2", model.ParentID2, DbType.String, ParameterDirection.Input);
                    var datas = conns.Query<CBO_DungChungViewModel>("CBO_DungChung_GetAll", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<CBO_DungChungViewModel> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("CBO_DungChung_GetAll Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("CBO_DungChung_GetAll Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public List<CBO_DungChungViewModel> CBO_DungChung_GetAllMater(CBO_DungChungParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("TableName", model.TableName, DbType.String, ParameterDirection.Input);
                    paramters.Add("ParentID1", model.ParentID1, DbType.String, ParameterDirection.Input);
                    paramters.Add("ParentID2", model.ParentID2, DbType.String, ParameterDirection.Input);
                    var datas = conns.Query<CBO_DungChungViewModel>("CBO_DungChung_GetAll", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<CBO_DungChungViewModel> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("CBO_DungChung_GetAll Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("CBO_DungChung_GetAll Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }

        #region "Get Info Pargram"
        public string Print(Print_DataMap param, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var parameters = new DynamicParameters();
                    JObject jObject = JObject.Parse("{" + param.Parameters + "}");
                    var value = "";
                    int _index = 0;
                    string _val = "";
                    bool isNumerical = false;
                    int myInt = 0;
                    foreach (var item in jObject)
                    {
                        _val = item.Value.ToString();
                        isNumerical = int.TryParse(_val, out myInt);
                        if (_index == 0)
                        {
                            value = (_val.Length > 0) ? ((isNumerical == true) ? _val : "N\'" + _val + "\'") : "null";
                        }
                        else
                        {
                            value += ",";
                            value += (_val.Length > 0) ? ((isNumerical == true) ? _val : "N\'" + _val + "\'") : "null";
                        }
                        _index++;
                    }
                    string vSQL = param.StoreProcedure + " " + value;
                    DataSet dsTable = clsCommon.GetdataSet(vSQL, ChuyenNganhConnection.ConnectionString);
                    restStatus = new ResponseModel();
                    return JsonConvert.SerializeObject(dsTable);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Print Error: " + ex.StackTrace);
                _log.Error("Print Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public E_ParametersMap GetInfoParametersByKeyCode(string model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conn = ChuyenNganhConnection)
                {
                    conn.Open();
                    var parameters = new DynamicParameters();
                    parameters.Add("KeyCode", model, DbType.String, ParameterDirection.Input);
                    var retval = conn.Query<E_ParametersMap>("E_GetInfoParametersByKeyCode", parameters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return retval.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("GetInfoParametersByKeyCode Error: " + ex.StackTrace);
                _log.Error("GetInfoParametersByKeyCode Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        #endregion

        #region MAP_ThuTuc_ManHinh
        public MAP_ThuTuc_ManHinhAdd MAP_ThuTuc_ManHinh_GetById(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<MAP_ThuTuc_ManHinhAdd>("MAP_ThuTuc_ManHinh_GetById", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as MAP_ThuTuc_ManHinhAdd ?? datas;
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("MAP_ThuTuc_ManHinh_GetById Error: " + ex.StackTrace);
                //log db
                _log.Error("MAP_ThuTuc_ManHinh_GetById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        #endregion MAP_ThuTuc_ManHinh
    }
}
