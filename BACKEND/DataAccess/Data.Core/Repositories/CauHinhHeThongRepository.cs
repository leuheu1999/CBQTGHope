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
    public class CauHinhHeThongRepository : Repository<CauHinhHeThongMap>, ICauHinhHeThongRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(DM_PhuongXaRepository));
        private const string TableName = "";
        private readonly ILogger _log;
        public CauHinhHeThongRepository(ILog logger, ILogger log) : base(TableName)
        {
            _logger = logger;
            _log = log;
        }
        public CauHinhHeThongMap CauHinhHeThong_Get(out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();                  
                    var datas = conns.QuerySingleOrDefault<CauHinhHeThongMap>("CauHinhHeThong_Get", commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("CauHinhHeThong_Get Error: " + ex.StackTrace);
                //log db
                _log.Error("CauHinhHeThong_Get Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public string CauHinhHeThong_GetByKey(string Key,out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Key", Key, DbType.String, ParameterDirection.Input);
                    var datas = conns.ExecuteScalar<string>("CauHinhHeThong_GetByKey", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("CauHinhHeThong_GetByKey Error: " + ex.StackTrace);
                //log db
                _log.Error("CauHinhHeThong_GetByKey Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public string DM_DonVi_GetByKey(string Key, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Key", Key, DbType.String, ParameterDirection.Input);
                    var datas = conns.ExecuteScalar<string>("DM_DonVi_GetByKey", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("CauHinhHeThong_GetByKey Error: " + ex.StackTrace);
                //log db
                _log.Error("CauHinhHeThong_GetByKey Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long CauHinhheThong_Ins(CauHinhHeThongMap model, IDbConnection conn, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                var paramters = new DynamicParameters();
                    paramters.Add("ImgUrl", model.ImgUrl, DbType.String, ParameterDirection.Input);
                paramters.Add("SoHangHienThi", model.SoHangHienThi, DbType.Int32, ParameterDirection.Input);
                paramters.Add("TieuDeTrang", model.TieuDeTrang, DbType.String, ParameterDirection.Input);
                paramters.Add("TuKhoa", model.TuKhoa, DbType.String, ParameterDirection.Input);
                paramters.Add("MoTaTuKhoa", model.MoTaTuKhoa, DbType.String, ParameterDirection.Input);
                paramters.Add("Logo", model.Logo, DbType.String, ParameterDirection.Input);
                paramters.Add("ThoiGianTruyCapTu", model.ThoiGianTruyCapTu, DbType.String, ParameterDirection.Input);
                paramters.Add("ThoiGianTruyCapDen", model.ThoiGianTruyCapDen, DbType.String, ParameterDirection.Input);
                paramters.Add("SoLanDuocDangNhapSai", model.SoLanDuocDangNhapSai, DbType.Int32, ParameterDirection.Input);
                var datas = conn.QueryFirstOrDefault<long>("CauHinhHeThong_InsUpd", paramters,trans, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                
            }
            catch (Exception ex)
            {
                _logger.Error("CauHinhheThong_Ins Error: " + ex.StackTrace);
                //log db
                _log.Error("CauHinhheThong_Ins Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public CauHinhMailMap CauHinhMail_Get(out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var datas = conns.QuerySingleOrDefault<CauHinhMailMap>("CauHinhMail_Get", commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("CauHinhMail_Get Error: " + ex.StackTrace);
                //log db
                _log.Error("CauHinhMail_Get Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long CauHinhmail_Ins(CauHinhMailMap model, IDbConnection conn, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                var paramters = new DynamicParameters();
                paramters.Add("Email", model.Email, DbType.String, ParameterDirection.Input);
                paramters.Add("TenHienThi", model.TenHienThi, DbType.String, ParameterDirection.Input);
                paramters.Add("Host", model.Host, DbType.String, ParameterDirection.Input);
                paramters.Add("Port", model.Port, DbType.String, ParameterDirection.Input);
                paramters.Add("NguoiDung", model.NguoiDung, DbType.String, ParameterDirection.Input);
                paramters.Add("MatKhau", model.MatKhau, DbType.String, ParameterDirection.Input);
                var datas = conn.QueryFirstOrDefault<long>("CauHinhMail_InsUpd", paramters,trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("CauHinhheThong_Ins Error: " + ex.StackTrace);
                //log db
                _log.Error("CauHinhheThong_Ins Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public DonViMap CauHinhDonVi_Get(out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var datas = conns.QueryFirstOrDefault<DonViMap>("CauHinhDonVi_Get", commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("CauHinhDonVi_Get Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("CauHinhDonVi_Get Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long CauHinhDonVi_Ins(DonViMap model, IDbConnection conn, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                var paramters = new DynamicParameters();
                paramters.Add("TenDonVi", model.TenDonVi, DbType.String, ParameterDirection.Input);
                paramters.Add("TenVietTat", model.TenVietTat, DbType.String, ParameterDirection.Input);
                paramters.Add("DiaChi", model.DiaChi, DbType.String, ParameterDirection.Input);
                paramters.Add("DienThoai", model.DienThoai, DbType.String, ParameterDirection.Input);
                paramters.Add("Fax", model.Fax, DbType.String, ParameterDirection.Input);
                paramters.Add("Email", model.Email, DbType.String, ParameterDirection.Input);
                paramters.Add("Website", model.Website, DbType.String, ParameterDirection.Input);
                paramters.Add("Copyright", model.Copyright, DbType.String, ParameterDirection.Input);
                paramters.Add("FaceBook", model.FaceBook, DbType.String, ParameterDirection.Input);
                paramters.Add("Zalo", model.Zalo, DbType.String, ParameterDirection.Input);
                paramters.Add("Twitter", model.Twitter, DbType.String, ParameterDirection.Input);
                paramters.Add("Youtube", model.Youtube, DbType.String, ParameterDirection.Input);
                paramters.Add("MoTaWebsite", model.MoTaWebsite, DbType.String, ParameterDirection.Input);

                var datas = conn.QueryFirstOrDefault<long>("CauHinhDonVi_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("CauHinhDonVi_Ins Error: " + ex.StackTrace);
                //log db
                _log.Error("CauHinhDonVi_Ins Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }

        public int CauHinhMail_DoiMatKhau(string pass, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Password", pass, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("CauHinhMail_DoiMatKhau", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("CauHinhMail_DoiMatKhau Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("CauHinhMail_DoiMatKhau Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public bool LuotTruyCapHeThong_Insert(out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    var datas = conns.QueryFirstOrDefault<bool>("LuotTruyCapHeThong_Insert", commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("LuotTruyCapHeThong_Insert Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("LuotTruyCapHeThong_Insert Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return false;
            }
        }
    }
}
