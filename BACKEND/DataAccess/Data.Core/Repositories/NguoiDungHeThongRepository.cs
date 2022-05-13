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
    public class NguoiDungHeThongRepository : Repository<NguoiDungHeThongMap>, INguoiDungHeThongRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(NguoiDungHeThongRepository));
        private const string TableName = "";
        private readonly ILogger _log;
        public NguoiDungHeThongRepository(ILog logger, ILogger log) : base(TableName)
        {
            _logger = logger;
            _log = log;
        }
        public List<NguoiDungHeThongMap> NguoiDungHeThong_List(NguoiDungHeThongMapParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("HoTen", model.HoVaTen, DbType.String, ParameterDirection.Input);
                    paramters.Add("Email", model.Email, DbType.String, ParameterDirection.Input);
                    paramters.Add("DienThoai", model.DienThoai, DbType.String, ParameterDirection.Input);
                    paramters.Add("NhomQuyenID", model.NhomQuyen, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<NguoiDungHeThongMap>("ND_NguoiDungHeThong_GetByCodition", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<NguoiDungHeThongMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("NguoiDungHeThong_List Error: " + ex.StackTrace);
                //log db
                _log.Error("NguoiDungHeThong_List Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public NguoiDungHeThongMapAdd NguoiDungHeThong_GetById(long Id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", Id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<NguoiDungHeThongMapAdd>("ND_NguoiDungHeThong_ByID", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as NguoiDungHeThongMapAdd ?? datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("NguoiDungHeThong_GetById Error: " + ex.StackTrace);
                //log db
                _log.Error("NguoiDungHeThong_GetById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public NguoiDungHeThongMapAdd NguoiDungHeThong_GetByGuid(Guid Id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("TaiKhoanGuid", Id, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<NguoiDungHeThongMapAdd>("ND_NguoiDungHeThong_ByGuid", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as NguoiDungHeThongMapAdd ?? datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("NguoiDungHeThong_GetByGuid Error: " + ex.StackTrace);
                //log db
                _log.Error("NguoiDungHeThong_GetByGuid Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public NguoiDungHeThongMapAdd NguoiDungHeThong_GetByTenTaiKhoan(string TenTaiKhoan, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("TenTaiKhoan", TenTaiKhoan, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<NguoiDungHeThongMapAdd>("ND_NguoiDungHeThong_ByTenTaiKhoan", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as NguoiDungHeThongMapAdd ?? datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("NguoiDungHeThong_GetByTenTaiKhoan Error: " + ex.StackTrace);
                //log db
                _log.Error("NguoiDungHeThong_GetByTenTaiKhoan Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public NguoiDungHeThongMapAdd NguoiDungHeThong_GetBySDT(string SDT, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("SoDienThoai", SDT, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<NguoiDungHeThongMapAdd>("ND_NguoiDungHeThong_BySDT", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as NguoiDungHeThongMapAdd ?? datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("NguoiDungHeThong_GetBySDT Error: " + ex.StackTrace);
                //log db
                _log.Error("NguoiDungHeThong_GetBySDT Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public NguoiDungHeThongMapAdd NguoiDungHeThong_GetByEmail(string Email, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Email", Email, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<NguoiDungHeThongMapAdd>("ND_NguoiDungHeThong_ByEmail", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as NguoiDungHeThongMapAdd ?? datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("NguoiDungHeThong_GetByEmail Error: " + ex.StackTrace);
                //log db
                _log.Error("NguoiDungHeThong_GetByEmail Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public int NguoiDungHeThong_CheckMCUserId(long userId, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("UserID", userId, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("ND_NguoiDungHeThong_CheckMCUserId", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("NguoiDungHeThong_CheckMCUserId Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("NguoiDungHeThong_CheckMCUserId Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public Guid NguoiDungHeThong_InsUpd(NguoiDungHeThongMapAdd model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", model.ID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("TaiKhoanGuid", model.TaiKhoanGuid, DbType.Guid, ParameterDirection.Input);
                    paramters.Add("TenTaiKhoan", model.TenTaiKhoan, DbType.String, ParameterDirection.Input);
                    paramters.Add("NgaySinh", model.NgaySinh, DbType.String, ParameterDirection.Input);
                    paramters.Add("HoVaTen", model.HoVaTen, DbType.String, ParameterDirection.Input);
                    paramters.Add("GioiTinh", model.GioiTinh, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("CMND", model.CMND, DbType.String, ParameterDirection.Input);
                    paramters.Add("NgayCapCMND", model.NgayCapCMND, DbType.String, ParameterDirection.Input);
                    paramters.Add("NoiCapCMND", model.NoiCapCMND, DbType.String, ParameterDirection.Input);
                    paramters.Add("Avatar", model.Avatar, DbType.String, ParameterDirection.Input);
                    paramters.Add("DiaChi", model.DiaChi, DbType.String, ParameterDirection.Input);
                    paramters.Add("MatKhau", model.MatKhau, DbType.String, ParameterDirection.Input);
                    paramters.Add("Salt", model.Salt, DbType.String, ParameterDirection.Input);
                    paramters.Add("Email", model.Email, DbType.String, ParameterDirection.Input);
                    paramters.Add("DienThoai", model.DienThoai, DbType.String, ParameterDirection.Input);
                    paramters.Add("Khoa", model.Khoa, DbType.Boolean, ParameterDirection.Input);
                    paramters.Add("TenHienThi", model.TenHienThi, DbType.String, ParameterDirection.Input);
                    paramters.Add("DSNhomQuyen", model.DSNhomQuyen, DbType.String, ParameterDirection.Input);
                    paramters.Add("MCUserID", model.MCUserID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("MCPhongBanID", model.MCPhongBanID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("MCTenPhongBan", model.MCTenPhongBan, DbType.String, ParameterDirection.Input);
                    paramters.Add("MCDonViID", model.MCDonViID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("MCTenDonVi", model.MCTenDonVi, DbType.String, ParameterDirection.Input);
                    paramters.Add("MCChucVuID", model.MCChucVuID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("MCTenChucVu", model.MCTenChucVu, DbType.String, ParameterDirection.Input);
                    paramters.Add("CreatedUserID", model.CreatedUserID, DbType.Guid, ParameterDirection.Input);
                    paramters.Add("LastUpdUserID", model.LastUpdUserID, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<Guid>("ND_NguoiDungHeThong_InsUpd", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("NguoiDungHeThong_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("NguoiDungHeThong_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return Guid.Empty;
            }
        }
        public int NguoiDungHeThong_Del(Guid id, Guid lastupduserid, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("TaiKhoanGuid", id, DbType.Guid, ParameterDirection.Input);
                    paramters.Add("LastUpdUserID", lastupduserid, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("ND_NguoiDungHeThong_Del", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("NguoiDungHeThong_Del Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("NguoiDungHeThong_Del Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int NguoiDungHeThong_ChangePass(NguoiDungHeThongChagePass model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("TaiKhoanGuid", model.TaiKhoanGuid, DbType.Guid, ParameterDirection.Input);
                    paramters.Add("MatKhau", model.MatKhau, DbType.String, ParameterDirection.Input);
                    paramters.Add("Salt", model.Salt, DbType.String, ParameterDirection.Input);
                    paramters.Add("LastUpdUserID", model.LastUpdUserID, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("ND_NguoiDungHeThong_ChangePass", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("NguoiDungHeThong_ChangePass Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("NguoiDungHeThong_ChangePass Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }

        #region nguoi dung he thong nhom quyen
        public List<DM_NhomQuyenAdd> NguoiDungHeThong_NhomQuyen(Guid TaiKhoanGuid, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("NguoiDungID", TaiKhoanGuid, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.Query<DM_NhomQuyenAdd>("ND_NguoiDungHeThong_NhomQuyen_List", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<DM_NhomQuyenAdd> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("NguoiDungHeThong_NhomQuyen Error: " + ex.StackTrace);
                //log db
                _log.Error("NguoiDungHeThong_NhomQuyen Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long NguoiDungHeThong_NhomQuyen_Insert(NguoiDungHeThong_NhomQuyenMapAdd model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("NhomQuyenIDs", model.NhomQuyenIDs, DbType.String, ParameterDirection.Input);
                    paramters.Add("NguoiDungID", model.NguoiDungID, DbType.Guid, ParameterDirection.Input);

                    var datas = conns.QueryFirstOrDefault<long>("ND_NguoiDungHeThong_NhomQuyen_Insert", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("NguoiDungHeThong_NhomQuyen_Insert Error: " + ex.StackTrace);
                //log db
                _log.Error("NguoiDungHeThong_NhomQuyen_Insert Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        #endregion

        #region Phan Quyen chuc nang
        public List<PQ_DanhSachNhomQuyen> PQ_DanhSachNhomQuyen(out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var datas = conns.Query<PQ_DanhSachNhomQuyen>("PQ_DanhSachNhomQuyen", commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<PQ_DanhSachNhomQuyen> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("PQ_DanhSachNhomQuyen Error: " + ex.StackTrace);
                //log db
                _log.Error("PQ_DanhSachNhomQuyen Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public List<PQ_PhanQuyenChucNang> PQ_PhanQuyenChucNang_ByNhomQuyenID(long NhomQuyenID, out ResponseModel restStatus)
        {
            try
            {

                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("NhomQuyenID", NhomQuyenID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.Query<PQ_PhanQuyenChucNang>("PQ_PhanQuyenChucNang_ByNhomQuyenID", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<PQ_PhanQuyenChucNang> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("NhomQuyen_List Error: " + ex.StackTrace);
                //log db
                _log.Error("NhomQuyen_List Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public List<DM_QuyenChucNangMap> PQ_QuyenChucNang_List(out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var datas = conns.Query<DM_QuyenChucNangMap>("PQ_QuyenChucNang_List", commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<DM_QuyenChucNangMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("PQ_QuyenChucNang_List Error: " + ex.StackTrace);
                //log db
                _log.Error("PQ_QuyenChucNang_List Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }

        public long PhanQuyenChucNang_InsUpd(PQ_PhanQuyenChucNang model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("NhomQuyenID", model.NhomQuyenID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("QuyenChucNangID", model.QuyenChucNangID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("CreatedUserID", model.CreatedUserID, DbType.Guid, ParameterDirection.Input);
                paramters.Add("LastUpdUserID", model.LastUpdUserID, DbType.Guid, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<long>("PQ_PhanQuyenChucNang_Ins", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("PhanQuyenChucNang_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("PhanQuyenChucNang_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int PQ_QuyenChucNang_Del(IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var datas = conns.QueryFirstOrDefault<int>("PQ_QuyenChucNang_Del", null, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("PQ_QuyenChucNang_Del Error: " + ex.StackTrace);
                //log db
                _log.Error("PQ_QuyenChucNang_Del Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }

        #endregion

        #region login
        public string GetSalt(string username)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("TenTaiKhoan", username, DbType.String, ParameterDirection.Input);
                    var datas = conns.QuerySingleOrDefault<string>("ND_GetSalt", paramters, commandType: CommandType.StoredProcedure);
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("GetSalt Error: " + ex.StackTrace);
                //log db
                _log.Error("GetSalt Error: " + ex.Message, ex, new Guid());
                return "";
            }
        }
        public NguoiDungHeThongMapAdd GetUserLogin(string userName, string password, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("TenTaiKhoan", userName, DbType.String, ParameterDirection.Input);
                    paramters.Add("MatKhau", password, DbType.String, ParameterDirection.Input);
                    var datas = conns.QuerySingleOrDefault<NguoiDungHeThongMapAdd>("ND_GetUserLogin", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("GetUserLogin Error: " + ex.StackTrace);
                //log db
                _log.Error("GetUserLogin Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public bool IsActive(string userName, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("TenTaiKhoan", userName, DbType.String, ParameterDirection.Input);
                    var datas = conns.QuerySingleOrDefault<bool>("ND_IsActive", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("IsActive Error: " + ex.StackTrace);
                //log db
                _log.Error("IsActive Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return false;
            }
        }
        public List<BanIPMap> BanIP_List(out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var datas = conns.Query<BanIPMap>("BanIP_List", commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<BanIPMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("BanIP_List Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("BanIP_List Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public BanIPMap BanIP_byIp(string ip, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Ipadress", ip, DbType.String, ParameterDirection.Input);
                    var datas = conns.QuerySingleOrDefault<BanIPMap>("BanIP_byIp", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("BanIP_byIp Error: " + ex.StackTrace);
                //log db
                _log.Error("BanIP_byIp Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public int BanIP_deletebyIp(string ip, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Ipadress", ip, DbType.String, ParameterDirection.Input);
                    var datas = conns.QuerySingleOrDefault<int>("BanIP_deletebyIp", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("BanIP_deletebyIp Error: " + ex.StackTrace);
                //log db
                _log.Error("BanIP_deletebyIp Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public long BanIP_InsUpd(BanIPMap model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Id", model.Id, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("IPAdress", model.IPAdress, DbType.String, ParameterDirection.Input);
                    paramters.Add("Status", model.Status, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("DateEnd", model.DateEnd, DbType.DateTime, ParameterDirection.Input);
                    paramters.Add("WrongNum", model.WrongNum, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("Created_Date", model.Created_Date, DbType.DateTime, ParameterDirection.Input);
                    paramters.Add("Modified_Date", model.Modified_Date, DbType.DateTime, ParameterDirection.Input);
                    var datas = conns.QuerySingleOrDefault<long>("BanIP_InsUpd", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("BanIP_deletebyIp Error: " + ex.StackTrace);
                //log db
                _log.Error("BanIP_deletebyIp Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int ND_NguoiDungHeThong_UpdateToken(Guid UserID, string Token, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("TaiKhoanGuid", UserID, DbType.Guid, ParameterDirection.Input);
                    paramters.Add("Token", Token, DbType.String, ParameterDirection.Input);
                    var datas = conns.QuerySingleOrDefault<int>("ND_NguoiDungHeThong_UpdateToken", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("ND_NguoiDungHeThong_UpdateToken Error: " + ex.StackTrace);
                //log db
                _log.Error("ND_NguoiDungHeThong_UpdateToken Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }

        #endregion

        #region phan quyen
        public List<Roles> GetRolesByUser(string username, out ResponseModel restStatus)// lấy danh sách nhóm quyền của user
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("username", username, DbType.String, ParameterDirection.Input);
                    var datas = conns.Query<Roles>("PQ_GetRolesByUser", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<Roles> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("GetRolesByUser Error: " + ex.StackTrace);
                //log db
                _log.Error("GetRolesByUser Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public List<Right> GetRightsByUser(string username, out ResponseModel restStatus)// lấy danh sách quyền chức năng của user
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("username", username, DbType.String, ParameterDirection.Input);
                    var datas = conns.Query<Right>("PQ_GetRightsByUser", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<Right> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("GetRightsByUser Error: " + ex.StackTrace);
                //log db
                _log.Error("GetRightsByUser Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public UrlViewModel GetUrlViewModel(string DSQuyenChucNang, out ResponseModel restStatus)// lấy đương dẫn mực định thuộc ds quyền chức năng
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("DSQuyenChucNang", DSQuyenChucNang, DbType.String, ParameterDirection.Input);
                    var datas = conns.QuerySingleOrDefault<UrlViewModel>("PQ_GetUrlViewModel", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("GetUrlViewModel Error: " + ex.StackTrace);
                //log db
                _log.Error("GetUrlViewModel Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }

        #endregion

        #region Khoa/ Mo khoa
        public long NguoiDungHeThong_Khoa(Guid UserID, bool Khoa, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("UserID", UserID, DbType.Guid, ParameterDirection.Input);
                    paramters.Add("Khoa", Khoa, DbType.Boolean, ParameterDirection.Input);
                    var datas = conns.ExecuteScalar<long>("NguoiDungHeThong_Khoa", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("NguoiDungHeThong_Khoa Error: " + ex.StackTrace);
                //log db
                _log.Error("NguoiDungHeThong_Khoa Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        #endregion End khoa/ Mo khoa

        public string ND_NguoiDungHeThong_UpdateConnectionSignalR(Guid UserID, string ConnectionId, bool IsOnline, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("UserID", UserID, DbType.Guid, ParameterDirection.Input);
                    paramters.Add("ConnectionId", ConnectionId, DbType.String, ParameterDirection.Input);
                    paramters.Add("IsOnline", IsOnline, DbType.Boolean, ParameterDirection.Input);
                    var datas = conns.ExecuteScalar<string>("ND_NguoiDungHeThong_UpdateConnectionSignalR", paramters, commandType: CommandType.StoredProcedure);//NguoiDungHeThong_Khoa
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("ND_NguoiDungHeThong_UpdateConnectionSignalR Error: " + ex.StackTrace);
                //log db
                _log.Error("ND_NguoiDungHeThong_UpdateConnectionSignalR Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return string.Empty;
            }
        }
        public int ND_NguoiDungHeThong_CountOnline(out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = MasterConnection)
                {
                    conns.Open();
                    var datas = conns.ExecuteScalar<int>("ND_NguoiDungHeThong_CountOnline", null, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("ND_NguoiDungHeThong_CountOnline Error: " + ex.StackTrace);
                //log db
                _log.Error("ND_NguoiDungHeThong_CountOnline Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
    }
}
