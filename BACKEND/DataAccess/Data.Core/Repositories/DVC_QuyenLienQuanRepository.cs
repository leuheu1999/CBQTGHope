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
    public class DVC_QuyenLienQuanRepository : Repository<DVC_QLQ_QuyenLienQuanMap>, IDVC_QuyenLienQuanRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(DVC_QuyenLienQuanRepository));
        private const string TableName = "";
        private readonly ILogger _log;
        public DVC_QuyenLienQuanRepository(ILog logger, ILogger log) : base(TableName)
        {
            _logger = logger;
            _log = log;
        }
        #region DVC_QLQ_QuyenLienQuan
        public List<DVC_QLQ_QuyenLienQuanMap> DVC_QLQ_QuyenLienQuan_List(DVC_QLQ_QuyenLienQuanParam model, out ResponseModel restStatus)
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
                    var datas = conns.Query<DVC_QLQ_QuyenLienQuanMap>("DVC_QLQ_QuyenLienQuan_List", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<DVC_QLQ_QuyenLienQuanMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QLQ_QuyenLienQuan_List Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QLQ_QuyenLienQuan_List Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public DVC_QLQ_QuyenLienQuanAdd DVC_QLQ_QuyenLienQuan_ById(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new DVC_QLQ_QuyenLienQuanAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryMultiple("DVC_QLQ_QuyenLienQuan_ById", paramters, commandType: CommandType.StoredProcedure);
                    var quyenLienQuan = datas.Read<DVC_QLQ_QuyenLienQuanAdd>().FirstOrDefault();
                    var lstNguoiBieuDien = datas.Read<DVC_QLQ_NguoiBieuDienAdd>();
                    var lstChuSoHuu = datas.Read<DVC_QLQ_ChuSoHuuAdd>();
                    var lstDinhKem = datas.Read<DVC_QLQ_DinhKemAdd>();
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
                    return result as DVC_QLQ_QuyenLienQuanAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QLQ_QuyenLienQuan_ById Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QLQ_QuyenLienQuan_ById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long DVC_QLQ_QuyenLienQuan_GetStt(out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    var datas = conns.QueryFirstOrDefault<long>("DVC_QLQ_QuyenLienQuan_GetStt", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QLQ_QuyenLienQuan_GetStt Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QLQ_QuyenLienQuan_GetStt Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public long DVC_QLQ_QuyenLienQuan_InsUpd(DVC_QLQ_QuyenLienQuanAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
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
                paramters.Add("NgayKy", model.NgayKy, DbType.String, ParameterDirection.Input);
                paramters.Add("UserID", model.UserID, DbType.Guid, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<long>("DVC_QLQ_QuyenLienQuan_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QLQ_QuyenLienQuan_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QLQ_QuyenLienQuan_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int DVC_QLQ_NguoiBieuDien_Ins(List<DVC_QLQ_NguoiBieuDienAdd> list, long quyenLienQuanID, Guid userID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("QuyenLienQuanID", quyenLienQuanID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("UserID", userID, DbType.Guid, ParameterDirection.Input);
                paramters.Add("table", list.ToDataTable().AsTableValuedParameter("dbo.TableNguoiBieuDienMap"));
                var data = conns.ExecuteScalar<int>("DVC_QLQ_NguoiBieuDien_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return data;
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QLQ_NguoiBieuDien_Ins Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QLQ_NguoiBieuDien_Ins Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int DVC_QLQ_ChuSoHuu_Ins(List<DVC_QLQ_ChuSoHuuAdd> list, long quyenLienQuanID, Guid userID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("QuyenLienQuanID", quyenLienQuanID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("UserID", userID, DbType.Guid, ParameterDirection.Input);
                paramters.Add("table", list.ToDataTable().AsTableValuedParameter("dbo.TableChuSoHuuMap"));
                var data = conns.ExecuteScalar<int>("DVC_QLQ_ChuSoHuu_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return data;
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QLQ_ChuSoHuu_Ins Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QLQ_ChuSoHuu_Ins Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public long DVC_QLQ_QuyenLienQuan_Change(DVC_QLQ_QuyenLienQuanChange model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
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
                var datas = conns.QueryFirstOrDefault<long>("DVC_QLQ_QuyenLienQuan_Change", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QLQ_QuyenLienQuan_ChangeStatus Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QLQ_QuyenLienQuan_ChangeStatus Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public List<DVC_QLQ_NguoiBieuDienAdd> DVC_QLQ_NguoiBieuDien_ByQuyenLQId(long quyenLienQuanID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("QuyenLienQuanID", quyenLienQuanID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.Query<DVC_QLQ_NguoiBieuDienAdd>("DVC_QLQ_NguoiBieuDien_ByQuyenLQId", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<DVC_QLQ_NguoiBieuDienAdd> ?? datas.ToList(); ;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QLQ_NguoiBieuDien_ByQuyenLQId Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QLQ_NguoiBieuDien_ByQuyenLQId Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public List<DVC_QLQ_ChuSoHuuAdd> DVC_QLQ_ChuSoHuu_ByQuyenLQId(long quyenLienQuanID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("QuyenLienQuanID", quyenLienQuanID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.Query<DVC_QLQ_ChuSoHuuAdd>("DVC_QLQ_ChuSoHuu_ByQuyenLQId", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<DVC_QLQ_ChuSoHuuAdd> ?? datas.ToList(); ;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QLQ_ChuSoHuu_ByQuyenLQId Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QLQ_ChuSoHuu_ByQuyenLQId Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public int DVC_QLQ_QuyenLienQuan_Del(long quyenLienQuanID, Guid userID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("QuyenLienQuanID", quyenLienQuanID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("UserID", userID, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.ExecuteScalar<int>("DVC_QLQ_QuyenLienQuan_Del", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QLQ_QuyenLienQuan_Del Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QLQ_QuyenLienQuan_Del Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public List<DVC_QLQ_GiayChungNhanMap> DVC_QLQ_QuyenLienQuan_SearchGCN(DVC_QLQ_GiayChungNhanParam model, out ResponseModel restStatus)
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
                    var datas = conns.Query<DVC_QLQ_GiayChungNhanMap>("DVC_QLQ_QuyenLienQuan_SearchGCN", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<DVC_QLQ_GiayChungNhanMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QLQ_QuyenLienQuan_SearchGCN Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QLQ_QuyenLienQuan_SearchGCN Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long DVC_QLQ_QuyenLienQuan_OldToNew(long id, int loaiNghiepVuID, long keyMapID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("QuyenLienQuanID", id, DbType.Int64, ParameterDirection.Input);
                paramters.Add("LoaiNghiepVuID", loaiNghiepVuID, DbType.Int16, ParameterDirection.Input);
                paramters.Add("KeyMapID", keyMapID, DbType.Int64, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<long>("DVC_QLQ_QuyenLienQuan_OldToNew", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QLQ_QuyenLienQuan_OldToNew Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QLQ_QuyenLienQuan_OldToNew Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public long DVC_QLQ_QuyenLienQuan_CapSo(DVC_QLQ_QuyenLienQuan_CapSo model, out ResponseModel restStatus)
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
                    var datas = conns.ExecuteScalar<long>("DVC_QLQ_QuyenLienQuan_CapSo", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QLQ_QuyenLienQuan_CapSo Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QLQ_QuyenLienQuan_CapSo Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int DVC_QLQ_QuyenLienQuan_CheckSoTT(long id, string soTT, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("STT", soTT, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("DVC_QLQ_QuyenLienQuan_CheckSoTT", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QLQ_QuyenLienQuan_CheckSoTT Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QLQ_QuyenLienQuan_CheckSoTT Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int DVC_QLQ_QuyenLienQuan_CheckSoGCN(long id, string soGCN, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("SoGCN", soGCN, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("DVC_QLQ_QuyenLienQuan_CheckSoGCN", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QLQ_QuyenLienQuan_CheckSoGCN Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QLQ_QuyenLienQuan_CheckSoGCN Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        #endregion DVC_QLQ_QuyenLienQuan

        #region DVC_QLQ_DinhKem
        public int DVC_QLQ_DinhKem_Ins(List<DVC_QLQ_DinhKemAdd> list, long quyenLienQuanID, int loaiXuLyID, long keyMapID, Guid userID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
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
                var data = conns.ExecuteScalar<int>("DVC_QLQ_DinhKem_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return data;
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QLQ_DinhKem_Ins Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QLQ_DinhKem_Ins Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        #endregion DVC_QLQ_DinhKem

        #region DVC_QLQ_LichSu
        public List<DVC_QLQ_LichSuMap> DVC_QLQ_LichSu_ById(long id, long staticID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("StaticID", staticID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.Query<DVC_QLQ_LichSuMap>("DVC_QLQ_LichSu_ById", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<DVC_QLQ_LichSuMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DVC_QLQ_LichSu_ById Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QLQ_LichSu_ById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        #endregion DVC_QLQ_LichSu
    }
}
