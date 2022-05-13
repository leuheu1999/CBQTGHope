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
    public class QLQ_QuyenLienQuanRepository : Repository<QLQ_QuyenLienQuanMap>, IQLQ_QuyenLienQuanRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(QLQ_QuyenLienQuanRepository));
        private const string TableName = "";
        private readonly ILogger _log;
        public QLQ_QuyenLienQuanRepository(ILog logger, ILogger log) : base(TableName)
        {
            _logger = logger;
            _log = log;
        }
        #region QLQ_QuyenLienQuan
        public List<QLQ_QuyenLienQuanMap> QLQ_QuyenLienQuan_List(QLQ_QuyenLienQuanParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("TuKhoa", model.TuKhoa, DbType.String, ParameterDirection.Input);
                    paramters.Add("SoGCN", model.SoGCN, DbType.String, ParameterDirection.Input);
                    paramters.Add("NgayCapTu", model.NgayCapTu, DbType.String, ParameterDirection.Input);
                    paramters.Add("NgayCapDen", model.NgayCapDen, DbType.String, ParameterDirection.Input);
                    paramters.Add("TenNguoiBieuDien", model.TenNguoiBieuDien, DbType.String, ParameterDirection.Input);
                    paramters.Add("TenChuSoHuu", model.TenChuSoHuu, DbType.String, ParameterDirection.Input);
                    paramters.Add("CreatedUser", model.CreatedUser, DbType.String, ParameterDirection.Input);
                    paramters.Add("VungMienID", model.VungMienID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("TrangThaiID", model.TrangThaiID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<QLQ_QuyenLienQuanMap>("QLQ_QuyenLienQuan_List", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<QLQ_QuyenLienQuanMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_QuyenLienQuan_List Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_QuyenLienQuan_List Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public QLQ_QuyenLienQuanAdd QLQ_QuyenLienQuan_ById(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new QLQ_QuyenLienQuanAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryMultiple("QLQ_QuyenLienQuan_ById", paramters, commandType: CommandType.StoredProcedure);
                    var quyenLienQuan = datas.Read<QLQ_QuyenLienQuanAdd>().FirstOrDefault();
                    var lstNguoiBieuDien = datas.Read<QLQ_NguoiBieuDienAdd>();
                    var lstChuSoHuu = datas.Read<QLQ_ChuSoHuuAdd>();
                    var lstDinhKem = datas.Read<QLQ_DinhKemAdd>();
                    if (quyenLienQuan != null)
                    {
                        result = quyenLienQuan;
                        if (lstNguoiBieuDien != null && lstNguoiBieuDien.Count() > 0)
                            result.ListNguoiBieuDien = lstNguoiBieuDien.ToList();
                        if (lstChuSoHuu != null && lstChuSoHuu.Count() > 0)
                            result.ListChuSoHuu = lstChuSoHuu.ToList();
                        if (lstDinhKem != null && lstDinhKem.Count() > 0)
                            result.ListDinhKem = lstDinhKem.ToList();
                    }
                    restStatus = new ResponseModel();
                    return result as QLQ_QuyenLienQuanAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_QuyenLienQuan_ById Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_QuyenLienQuan_ById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long QLQ_QuyenLienQuan_GetStt(out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    var datas = conns.QueryFirstOrDefault<long>("QLQ_QuyenLienQuan_GetStt", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_QuyenLienQuan_GetStt Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_QuyenLienQuan_GetStt Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public long QLQ_QuyenLienQuan_InsUpd(QLQ_QuyenLienQuanAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("QuyenLienQuanID", model.QuyenLienQuanID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("STT", model.STT, DbType.String, ParameterDirection.Input);
                paramters.Add("SoGCN", model.SoGCN, DbType.String, ParameterDirection.Input);
                paramters.Add("VungMienID", model.VungMienID, DbType.Int32, ParameterDirection.Input);
                paramters.Add("NgayCap", model.NgayCap, DbType.String, ParameterDirection.Input);
                paramters.Add("CoNguoiDaiDien", model.CoNguoiDaiDien, DbType.Boolean, ParameterDirection.Input);
                paramters.Add("NDDHoVaTen", model.NDDHoVaTen, DbType.String, ParameterDirection.Input);
                paramters.Add("NDDSoCMND", model.NDDSoCMND, DbType.String, ParameterDirection.Input);
                paramters.Add("NDDSoDienThoai", model.NDDSoDienThoai, DbType.String, ParameterDirection.Input);
                paramters.Add("NDDDiaChi", model.NDDDiaChi, DbType.String, ParameterDirection.Input);
                paramters.Add("IsToChuc", model.IsToChuc, DbType.Boolean, ParameterDirection.Input);
                paramters.Add("LoaiDangKyID", model.LoaiDangKyID, DbType.Int32, ParameterDirection.Input);
                paramters.Add("NgayHT", model.NgayHT, DbType.String, ParameterDirection.Input);
                paramters.Add("TenTacPham", model.TenTacPham, DbType.String, ParameterDirection.Input);
                paramters.Add("PhimID", model.PhimID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("PhimStt", model.PhimStt, DbType.Int32, ParameterDirection.Input);
                paramters.Add("TenPhim", model.TenPhim, DbType.String, ParameterDirection.Input);
                paramters.Add("TrangThaiCongBoID", model.TrangThaiCongBoID, DbType.Int32, ParameterDirection.Input);
                paramters.Add("NgayCongBo", model.NgayCongBo, DbType.String, ParameterDirection.Input);
                paramters.Add("LoaiHinhID", model.LoaiHinhID, DbType.Int32, ParameterDirection.Input);
                paramters.Add("NoiCongBo", model.NoiCongBo, DbType.String, ParameterDirection.Input);
                paramters.Add("HinhDaiDienID", model.HinhDaiDienID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("HinhDaiDienStt", model.HinhDaiDienStt, DbType.Int32, ParameterDirection.Input);
                paramters.Add("HinhDaiDienUrl", model.HinhDaiDienUrl, DbType.String, ParameterDirection.Input);
                paramters.Add("TrangThaiID", model.TrangThaiID, DbType.Int32, ParameterDirection.Input);
                paramters.Add("TenTrangThai", model.TenTrangThai, DbType.String, ParameterDirection.Input);
                paramters.Add("LoaiNghiepVuID", model.LoaiNghiepVuID, DbType.Int32, ParameterDirection.Input);
                paramters.Add("KeyMapID", model.KeyMapID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("IsDauKy", model.IsDauKy, DbType.Boolean, ParameterDirection.Input);
                paramters.Add("NguoiKyID", model.NguoiKyID, DbType.Int32, ParameterDirection.Input);
                paramters.Add("DVCID", model.DVCID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("NgayKy", model.NgayKy, DbType.String, ParameterDirection.Input);
                paramters.Add("UserID", model.UserID, DbType.Guid, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<long>("QLQ_QuyenLienQuan_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_QuyenLienQuan_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_QuyenLienQuan_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int QLQ_NguoiBieuDien_Ins(List<QLQ_NguoiBieuDienAdd> list, long quyenLienQuanID, Guid userID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("QuyenLienQuanID", quyenLienQuanID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("UserID", userID, DbType.Guid, ParameterDirection.Input);
                paramters.Add("table", list.ToDataTable().AsTableValuedParameter("dbo.TableNguoiBieuDienMap"));
                var data = conns.ExecuteScalar<int>("QLQ_NguoiBieuDien_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return data;
            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_NguoiBieuDien_Ins Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_NguoiBieuDien_Ins Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int QLQ_ChuSoHuu_Ins(List<QLQ_ChuSoHuuAdd> list, long quyenLienQuanID, Guid userID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("QuyenLienQuanID", quyenLienQuanID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("UserID", userID, DbType.Guid, ParameterDirection.Input);
                paramters.Add("table", list.ToDataTable().AsTableValuedParameter("dbo.TableChuSoHuuMap"));
                var data = conns.ExecuteScalar<int>("QLQ_ChuSoHuu_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return data;
            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_ChuSoHuu_Ins Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_ChuSoHuu_Ins Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public long QLQ_QuyenLienQuan_Change(QLQ_QuyenLienQuanChange model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("QuyenLienQuanID", model.QuyenLienQuanID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("STT", model.STT, DbType.String, ParameterDirection.Input);
                paramters.Add("SoGCN", model.SoGCN, DbType.String, ParameterDirection.Input);
                paramters.Add("NgayCap", model.NgayCap, DbType.String, ParameterDirection.Input);
                paramters.Add("TrangThaiID", model.TrangThaiID, DbType.Int32, ParameterDirection.Input);
                paramters.Add("TenTrangThai", model.TenTrangThai, DbType.String, ParameterDirection.Input);
                paramters.Add("LoaiNghiepVuID", model.LoaiNghiepVuID, DbType.Int32, ParameterDirection.Input);
                paramters.Add("KeyMapID", model.KeyMapID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("SoLanCapLai", model.SoLanCapLai, DbType.Int32, ParameterDirection.Input);
                paramters.Add("SoLanThuHoi", model.SoLanThuHoi, DbType.Int32, ParameterDirection.Input);
                paramters.Add("SoLanCapDoi", model.SoLanCapDoi, DbType.Int32, ParameterDirection.Input);
                paramters.Add("SoLanDoiChu", model.SoLanDoiChu, DbType.Int32, ParameterDirection.Input);
                paramters.Add("UserID", model.UserID, DbType.Guid, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<long>("QLQ_QuyenLienQuan_Change", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_QuyenLienQuan_ChangeStatus Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_QuyenLienQuan_ChangeStatus Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public List<QLQ_NguoiBieuDienAdd> QLQ_NguoiBieuDien_ByQuyenLQId(long quyenLienQuanID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("QuyenLienQuanID", quyenLienQuanID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.Query<QLQ_NguoiBieuDienAdd>("QLQ_NguoiBieuDien_ByQuyenLQId", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<QLQ_NguoiBieuDienAdd> ?? datas.ToList(); ;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_NguoiBieuDien_ByQuyenLQId Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_NguoiBieuDien_ByQuyenLQId Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public List<QLQ_ChuSoHuuAdd> QLQ_ChuSoHuu_ByQuyenLQId(long quyenLienQuanID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("QuyenLienQuanID", quyenLienQuanID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.Query<QLQ_ChuSoHuuAdd>("QLQ_ChuSoHuu_ByQuyenLQId", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<QLQ_ChuSoHuuAdd> ?? datas.ToList(); ;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_ChuSoHuu_ByQuyenLQId Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_ChuSoHuu_ByQuyenLQId Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public int QLQ_QuyenLienQuan_Del(long quyenLienQuanID, Guid userID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("QuyenLienQuanID", quyenLienQuanID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("UserID", userID, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.ExecuteScalar<int>("QLQ_QuyenLienQuan_Del", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_QuyenLienQuan_Del Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_QuyenLienQuan_Del Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public List<QLQ_GiayChungNhanMap> QLQ_QuyenLienQuan_SearchGCN(QLQ_GiayChungNhanParam model, out ResponseModel restStatus)
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
                    var datas = conns.Query<QLQ_GiayChungNhanMap>("QLQ_QuyenLienQuan_SearchGCN", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<QLQ_GiayChungNhanMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_QuyenLienQuan_SearchGCN Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_QuyenLienQuan_SearchGCN Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public List<QLQ_SoThuTuMap> QLQ_QuyenLienQuan_SearchSTT(QLQ_SoThuTuParam model, out ResponseModel restStatus)
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
                    var datas = conns.Query<QLQ_SoThuTuMap>("QLQ_QuyenLienQuan_SearchSTT", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<QLQ_SoThuTuMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_QuyenLienQuan_SearchSTT Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_QuyenLienQuan_SearchSTT Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long QLQ_QuyenLienQuan_OldToNew(long id, int loaiNghiepVuID, long keyMapID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("QuyenLienQuanID", id, DbType.Int64, ParameterDirection.Input);
                paramters.Add("LoaiNghiepVuID", loaiNghiepVuID, DbType.Int16, ParameterDirection.Input);
                paramters.Add("KeyMapID", keyMapID, DbType.Int64, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<long>("QLQ_QuyenLienQuan_OldToNew", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;
            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_QuyenLienQuan_OldToNew Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_QuyenLienQuan_OldToNew Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public long QLQ_QuyenLienQuan_CapSo(QLQ_QuyenLienQuan_CapSo model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", model.ID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("LoaiNghiepVuID", model.LoaiNghiepVuID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("STT", model.STT, DbType.String, ParameterDirection.Input);
                    paramters.Add("SoGCN", model.SoGCN, DbType.String, ParameterDirection.Input);
                    paramters.Add("UserID", model.UserID, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.ExecuteScalar<long>("QLQ_QuyenLienQuan_CapSo", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_QuyenLienQuan_CapSo Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_QuyenLienQuan_CapSo Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int QLQ_QuyenLienQuan_CheckSoTT(long id, string soTT, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("STT", soTT, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("QLQ_QuyenLienQuan_CheckSoTT", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_QuyenLienQuan_CheckSoTT Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_QuyenLienQuan_CheckSoTT Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int QLQ_QuyenLienQuan_CheckSoGCN(long id, string soGCN, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("SoGCN", soGCN, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("QLQ_QuyenLienQuan_CheckSoGCN", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_QuyenLienQuan_CheckSoGCN Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_QuyenLienQuan_CheckSoGCN Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int QLQ_QuyenLienQuan_Duyet(long id, int loaiNghiepVuID, bool isDuyet, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("LoaiNghiepVuID", loaiNghiepVuID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("IsDuyet", isDuyet, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("QLQ_QuyenLienQuan_Duyet", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_QuyenLienQuan_Duyet Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_QuyenLienQuan_Duyet Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        #endregion QLQ_QuyenLienQuan

        #region QLQ_CapLai
        public QLQ_CapLaiAdd QLQ_CapLai_ById(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new QLQ_CapLaiAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryMultiple("QLQ_CapLai_ById", paramters, commandType: CommandType.StoredProcedure);
                    var capLai = datas.Read<QLQ_CapLaiAdd>().FirstOrDefault();
                    var lstDinhKem = datas.Read<QLQ_DinhKemAdd>();
                    if (capLai != null)
                    {
                        result = capLai;
                        if (lstDinhKem != null && lstDinhKem.Count() > 0)
                            result.ListDinhKem = lstDinhKem.ToList();
                    }
                    restStatus = new ResponseModel();
                    return result as QLQ_CapLaiAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_CapLai_ById Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_CapLai_ById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public QLQ_CapLaiAdd QLQ_CapLai_ByHoSoId(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new QLQ_CapLaiAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryMultiple("QLQ_CapLai_ByHoSoId", paramters, commandType: CommandType.StoredProcedure);
                    var capLai = datas.Read<QLQ_CapLaiAdd>().FirstOrDefault();
                    var lstDinhKem = datas.Read<QLQ_DinhKemAdd>();
                    if (capLai != null)
                    {
                        result = capLai;
                        if (lstDinhKem != null && lstDinhKem.Count() > 0)
                            result.ListDinhKem = lstDinhKem.ToList();
                    }
                    restStatus = new ResponseModel();
                    return result as QLQ_CapLaiAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_CapLai_ByHoSoId Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_CapLai_ByHoSoId Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public QLQ_CapLaiAdd QLQ_CapLai_ByQuyenLQId(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new QLQ_CapLaiAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryMultiple("QLQ_CapLai_ByQuyenLQId", paramters, commandType: CommandType.StoredProcedure);
                    var capLai = datas.Read<QLQ_CapLaiAdd>().FirstOrDefault();
                    var lstDinhKem = datas.Read<QLQ_DinhKemAdd>();
                    if (capLai != null)
                    {
                        result = capLai;
                        if (lstDinhKem != null && lstDinhKem.Count() > 0)
                            result.ListDinhKem = lstDinhKem.ToList();
                    }
                    restStatus = new ResponseModel();
                    return result as QLQ_CapLaiAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_CapLai_ByQuyenLQId Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_CapLai_ByQuyenLQId Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long QLQ_CapLai_InsUpd(QLQ_CapLaiAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("CapLaiID", model.CapLaiID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("STT", model.STT, DbType.String, ParameterDirection.Input);
                paramters.Add("SoGCN", model.SoGCN, DbType.String, ParameterDirection.Input);
                paramters.Add("NgayCapGCN", model.NgayCapGCN, DbType.String, ParameterDirection.Input);
                paramters.Add("ThongTinCapLai", model.ThongTinCapLai, DbType.String, ParameterDirection.Input);
                paramters.Add("SoQD", model.SoQD, DbType.String, ParameterDirection.Input);
                paramters.Add("NgayQD", model.NgayQD, DbType.String, ParameterDirection.Input);
                paramters.Add("LyDoCapLai", model.LyDoCapLai, DbType.String, ParameterDirection.Input);
                paramters.Add("ThongTinTacPham", model.ThongTinTacPham, DbType.String, ParameterDirection.Input);
                paramters.Add("NguoiKyID", model.NguoiKyID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("NgayKy", model.NgayKy, DbType.String, ParameterDirection.Input);
                paramters.Add("StaticID", model.StaticID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("QuyenLienQuanID", model.QuyenLienQuanID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("QuyenLienQuanCuID", model.QuyenLienQuanCuID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("UserID", model.UserID, DbType.Guid, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<long>("QLQ_CapLai_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_CapLai_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_CapLai_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        #endregion QLQ_CapLai

        #region QLQ_ChuyenChuSoHuu
        public QLQ_ChuyenChuSoHuuAdd QLQ_ChuyenChuSoHuu_ById(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new QLQ_ChuyenChuSoHuuAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryMultiple("QLQ_ChuyenChuSoHuu_ById", paramters, commandType: CommandType.StoredProcedure);
                    var chuyenChu = datas.Read<QLQ_ChuyenChuSoHuuAdd>().FirstOrDefault();
                    var lstDinhKem = datas.Read<QLQ_DinhKemAdd>();
                    if (chuyenChu != null)
                    {
                        result = chuyenChu;
                        if (lstDinhKem != null && lstDinhKem.Count() > 0)
                            result.ListDinhKem = lstDinhKem.ToList();
                    }
                    restStatus = new ResponseModel();
                    return result as QLQ_ChuyenChuSoHuuAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_ChuyenChuSoHuu_ById Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_ChuyenChuSoHuu_ById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public QLQ_ChuyenChuSoHuuAdd QLQ_ChuyenChuSoHuu_ByHoSoId(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new QLQ_ChuyenChuSoHuuAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryMultiple("QLQ_ChuyenChuSoHuu_ByHoSoId", paramters, commandType: CommandType.StoredProcedure);
                    var chuyenChu = datas.Read<QLQ_ChuyenChuSoHuuAdd>().FirstOrDefault();
                    var lstDinhKem = datas.Read<QLQ_DinhKemAdd>();
                    if (chuyenChu != null)
                    {
                        result = chuyenChu;
                        if (lstDinhKem != null && lstDinhKem.Count() > 0)
                            result.ListDinhKem = lstDinhKem.ToList();
                    }
                    restStatus = new ResponseModel();
                    return result as QLQ_ChuyenChuSoHuuAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_ChuyenChuSoHuu_ByHoSoId Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_ChuyenChuSoHuu_ByHoSoId Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public QLQ_ChuyenChuSoHuuAdd QLQ_ChuyenChuSoHuu_ByQuyenLQId(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new QLQ_ChuyenChuSoHuuAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryMultiple("QLQ_ChuyenChuSoHuu_ByQuyenLQId", paramters, commandType: CommandType.StoredProcedure);
                    var chuyenChu = datas.Read<QLQ_ChuyenChuSoHuuAdd>().FirstOrDefault();
                    var lstDinhKem = datas.Read<QLQ_DinhKemAdd>();
                    if (chuyenChu != null)
                    {
                        result = chuyenChu;
                        if (lstDinhKem != null && lstDinhKem.Count() > 0)
                            result.ListDinhKem = lstDinhKem.ToList();
                    }
                    restStatus = new ResponseModel();
                    return result as QLQ_ChuyenChuSoHuuAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_ChuyenChuSoHuu_ByQuyenLQId Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_ChuyenChuSoHuu_ByQuyenLQId Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long QLQ_ChuyenChuSoHuu_InsUpd(QLQ_ChuyenChuSoHuuAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("ChuyenChuSoHuuID", model.ChuyenChuSoHuuID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("STT", model.STT, DbType.String, ParameterDirection.Input);
                paramters.Add("SoGCN", model.SoGCN, DbType.String, ParameterDirection.Input);
                paramters.Add("NgayCapGCN", model.NgayCapGCN, DbType.String, ParameterDirection.Input);
                paramters.Add("ThongTinChuyenChuSoHuu", model.ThongTinChuyenChuSoHuu, DbType.String, ParameterDirection.Input);
                paramters.Add("LyDoChuyenChuSoHuu", model.LyDoChuyenChuSoHuu, DbType.String, ParameterDirection.Input);
                paramters.Add("NguoiKyID", model.NguoiKyID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("NgayKy", model.NgayKy, DbType.String, ParameterDirection.Input);
                paramters.Add("StaticID", model.StaticID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("QuyenLienQuanID", model.QuyenLienQuanID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("QuyenLienQuanCuID", model.QuyenLienQuanCuID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("UserID", model.UserID, DbType.Guid, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<long>("QLQ_ChuyenChuSoHuu_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_ChuyenChuSoHuu_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_ChuyenChuSoHuu_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        #endregion QLQ_ChuyenChuSoHuu

        #region QLQ_CapDoi
        public QLQ_CapDoiAdd QLQ_CapDoi_ById(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new QLQ_CapDoiAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryMultiple("QLQ_CapDoi_ById", paramters, commandType: CommandType.StoredProcedure);
                    var capLai = datas.Read<QLQ_CapDoiAdd>().FirstOrDefault();
                    var lstDinhKem = datas.Read<QLQ_DinhKemAdd>();
                    if (capLai != null)
                    {
                        result = capLai;
                        if (lstDinhKem != null && lstDinhKem.Count() > 0)
                            result.ListDinhKem = lstDinhKem.ToList();
                    }
                    restStatus = new ResponseModel();
                    return result as QLQ_CapDoiAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_CapDoi_ById Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_CapDoi_ById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public QLQ_CapDoiAdd QLQ_CapDoi_ByHoSoId(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new QLQ_CapDoiAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryMultiple("QLQ_CapDoi_ByHoSoId", paramters, commandType: CommandType.StoredProcedure);
                    var capLai = datas.Read<QLQ_CapDoiAdd>().FirstOrDefault();
                    var lstDinhKem = datas.Read<QLQ_DinhKemAdd>();
                    if (capLai != null)
                    {
                        result = capLai;
                        if (lstDinhKem != null && lstDinhKem.Count() > 0)
                            result.ListDinhKem = lstDinhKem.ToList();
                    }
                    restStatus = new ResponseModel();
                    return result as QLQ_CapDoiAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_CapDoi_ByHoSoId Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_CapDoi_ByHoSoId Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public QLQ_CapDoiAdd QLQ_CapDoi_ByQuyenLQId(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new QLQ_CapDoiAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryMultiple("QLQ_CapDoi_ByQuyenLQId", paramters, commandType: CommandType.StoredProcedure);
                    var capLai = datas.Read<QLQ_CapDoiAdd>().FirstOrDefault();
                    var lstDinhKem = datas.Read<QLQ_DinhKemAdd>();
                    if (capLai != null)
                    {
                        result = capLai;
                        if (lstDinhKem != null && lstDinhKem.Count() > 0)
                            result.ListDinhKem = lstDinhKem.ToList();
                    }
                    restStatus = new ResponseModel();
                    return result as QLQ_CapDoiAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_CapDoi_ByQuyenLQId Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_CapDoi_ByQuyenLQId Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long QLQ_CapDoi_InsUpd(QLQ_CapDoiAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("CapDoiID", model.CapDoiID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("STT", model.STT, DbType.String, ParameterDirection.Input);
                paramters.Add("SoGCN", model.SoGCN, DbType.String, ParameterDirection.Input);
                paramters.Add("NgayCapGCN", model.NgayCapGCN, DbType.String, ParameterDirection.Input);
                paramters.Add("ThongTinCapDoi", model.ThongTinCapDoi, DbType.String, ParameterDirection.Input);
                paramters.Add("SoQD", model.SoQD, DbType.String, ParameterDirection.Input);
                paramters.Add("NgayQD", model.NgayQD, DbType.String, ParameterDirection.Input);
                paramters.Add("LyDoCapDoi", model.LyDoCapDoi, DbType.String, ParameterDirection.Input);
                paramters.Add("ThongTinTacPham", model.ThongTinTacPham, DbType.String, ParameterDirection.Input);
                paramters.Add("NguoiKyID", model.NguoiKyID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("NgayKy", model.NgayKy, DbType.String, ParameterDirection.Input);
                paramters.Add("StaticID", model.StaticID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("QuyenLienQuanID", model.QuyenLienQuanID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("QuyenLienQuanCuID", model.QuyenLienQuanCuID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("UserID", model.UserID, DbType.Guid, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<long>("QLQ_CapDoi_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_CapDoi_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_CapDoi_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        #endregion QLQ_CapDoi

        #region QLQ_ThuHoi
        public QLQ_ThuHoiAdd QLQ_ThuHoi_ById(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new QLQ_ThuHoiAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryMultiple("QLQ_ThuHoi_ById", paramters, commandType: CommandType.StoredProcedure);
                    var thuHoi = datas.Read<QLQ_ThuHoiAdd>().FirstOrDefault();
                    var lstDinhKem = datas.Read<QLQ_DinhKemAdd>();
                    if (thuHoi != null)
                    {
                        result = thuHoi;
                        if (lstDinhKem != null && lstDinhKem.Count() > 0)
                            result.ListDinhKem = lstDinhKem.ToList();
                    }
                    restStatus = new ResponseModel();
                    return result as QLQ_ThuHoiAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_ThuHoi_ById Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_ThuHoi_ById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public QLQ_ThuHoiAdd QLQ_ThuHoi_ByHoSoId(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new QLQ_ThuHoiAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryMultiple("QLQ_ThuHoi_ByHoSoId", paramters, commandType: CommandType.StoredProcedure);
                    var thuHoi = datas.Read<QLQ_ThuHoiAdd>().FirstOrDefault();
                    var lstDinhKem = datas.Read<QLQ_DinhKemAdd>();
                    if (thuHoi != null)
                    {
                        result = thuHoi;
                        if (lstDinhKem != null && lstDinhKem.Count() > 0)
                            result.ListDinhKem = lstDinhKem.ToList();
                    }
                    restStatus = new ResponseModel();
                    return result as QLQ_ThuHoiAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_ThuHoi_ByHoSoId Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_ThuHoi_ByHoSoId Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public QLQ_ThuHoiAdd QLQ_ThuHoi_ByQuyenLQId(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new QLQ_ThuHoiAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryMultiple("QLQ_ThuHoi_ByQuyenLQId", paramters, commandType: CommandType.StoredProcedure);
                    var thuHoi = datas.Read<QLQ_ThuHoiAdd>().FirstOrDefault();
                    var lstDinhKem = datas.Read<QLQ_DinhKemAdd>();
                    if (thuHoi != null)
                    {
                        result = thuHoi;
                        if (lstDinhKem != null && lstDinhKem.Count() > 0)
                            result.ListDinhKem = lstDinhKem.ToList();
                    }
                    restStatus = new ResponseModel();
                    return result as QLQ_ThuHoiAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_ThuHoi_ByQuyenLQId Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_ThuHoi_ByQuyenLQId Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long QLQ_ThuHoi_InsUpd(QLQ_ThuHoiAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("ThuHoiID", model.ThuHoiID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("STT", model.STT, DbType.String, ParameterDirection.Input);
                paramters.Add("SoGCN", model.SoGCN, DbType.String, ParameterDirection.Input);
                paramters.Add("NgayCapGCN", model.NgayCapGCN, DbType.String, ParameterDirection.Input);
                paramters.Add("ThongTinThuHoi", model.ThongTinThuHoi, DbType.String, ParameterDirection.Input);
                paramters.Add("SoQD", model.SoQD, DbType.String, ParameterDirection.Input);
                paramters.Add("NgayQD", model.NgayQD, DbType.String, ParameterDirection.Input);
                paramters.Add("LyDoThuHoi", model.LyDoThuHoi, DbType.String, ParameterDirection.Input);
                paramters.Add("ThongTinTacPham", model.ThongTinTacPham, DbType.String, ParameterDirection.Input);
                paramters.Add("NguoiKyID", model.NguoiKyID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("NgayKy", model.NgayKy, DbType.String, ParameterDirection.Input);
                paramters.Add("StaticID", model.StaticID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("QuyenLienQuanID", model.QuyenLienQuanID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("UserID", model.UserID, DbType.Guid, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<long>("QLQ_ThuHoi_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_ThuHoi_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_ThuHoi_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        #endregion QLQ_ThuHoi

        #region QLQ_DinhKem
        public int QLQ_DinhKem_Ins(List<QLQ_DinhKemAdd> list, long quyenLienQuanID, int loaiXuLyID, long keyMapID, Guid userID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                list = list.Where(x => x.IsMotCua == false).ToList();
                var paramters = new DynamicParameters();
                paramters.Add("QuyenLienQuanID", quyenLienQuanID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("LoaiXuLyID", loaiXuLyID, DbType.Int32, ParameterDirection.Input);
                paramters.Add("KeyMapID", keyMapID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("UserID", userID, DbType.Guid, ParameterDirection.Input);
                paramters.Add("table", list.ToDataTable().AsTableValuedParameter("dbo.TableDinhKemMap"));
                var data = conns.ExecuteScalar<int>("QLQ_DinhKem_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return data;
            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_DinhKem_Ins Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_DinhKem_Ins Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        #endregion QLQ_DinhKem

        #region QLQ_LichSu
        public List<QLQ_LichSuMap> QLQ_LichSu_ById(long id, long staticID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("StaticID", staticID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.Query<QLQ_LichSuMap>("QLQ_LichSu_ById", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<QLQ_LichSuMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("QLQ_LichSu_ById Error: " + ex.StackTrace);
                //log db
                _log.Error("QLQ_LichSu_ById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        #endregion QLQ_LichSu
    }
}
