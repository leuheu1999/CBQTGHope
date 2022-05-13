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
    public class QTG_QuyenTacGiaRepository : Repository<QTG_QuyenTacGiaMap>, IQTG_QuyenTacGiaRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(QTG_QuyenTacGiaRepository));
        private const string TableName = "";
        private readonly ILogger _log;
        public QTG_QuyenTacGiaRepository(ILog logger, ILogger log) : base(TableName)
        {
            _logger = logger;
            _log = log;
        }

        #region QTG_QuyenTacGia
        public List<QTG_QuyenTacGiaMap> QTG_QuyenTacGia_List(QTG_QuyenTacGiaParam model, out ResponseModel restStatus)
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
                    paramters.Add("TenTacGia", model.TenTacGia, DbType.String, ParameterDirection.Input);
                    paramters.Add("TenChuSoHuu", model.TenChuSoHuu, DbType.String, ParameterDirection.Input);
                    paramters.Add("CreatedUser", model.CreatedUser, DbType.String, ParameterDirection.Input);
                    paramters.Add("VungMienID", model.VungMienID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("TrangThaiID", model.TrangThaiID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<QTG_QuyenTacGiaMap>("QTG_QuyenTacGia_List", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<QTG_QuyenTacGiaMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QTG_QuyenTacGia_List Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_QuyenTacGia_List Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public QTG_QuyenTacGiaAdd QTG_QuyenTacGia_ById(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new QTG_QuyenTacGiaAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryMultiple("QTG_QuyenTacGia_ById", paramters, commandType: CommandType.StoredProcedure);
                    var quyenTacGia = datas.Read<QTG_QuyenTacGiaAdd>().FirstOrDefault();
                    var lstTacGia = datas.Read<QTG_TacGiaAdd>();
                    var lstChuSoHuu = datas.Read<QTG_ChuSoHuuAdd>();
                    var lstDinhKem = datas.Read<QTG_DinhKemAdd>();
                    if(quyenTacGia != null)
                    {
                        result = quyenTacGia;
                        if (lstTacGia != null && lstTacGia.Count() > 0)
                            result.ListTacGia = lstTacGia.ToList();
                        if (lstChuSoHuu != null && lstChuSoHuu.Count() > 0)
                            result.ListChuSoHuu = lstChuSoHuu.ToList();
                        if (lstDinhKem != null && lstDinhKem.Count() > 0)
                            result.ListDinhKem = lstDinhKem.ToList();
                    }
                    restStatus = new ResponseModel();
                    return result as QTG_QuyenTacGiaAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QTG_QuyenTacGia_ById Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_QuyenTacGia_ById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long QTG_QuyenTacGia_GetStt(out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    var datas = conns.QueryFirstOrDefault<long>("QTG_QuyenTacGia_GetStt", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QTG_QuyenTacGia_GetStt Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_QuyenTacGia_GetStt Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public long QTG_QuyenTacGia_InsUpd(QTG_QuyenTacGiaAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("QuyenTacGiaID", model.QuyenTacGiaID, DbType.Int64, ParameterDirection.Input);
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
                var datas = conns.QueryFirstOrDefault<long>("QTG_QuyenTacGia_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("QTG_QuyenTacGia_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_QuyenTacGia_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int QTG_TacGia_Ins(List<QTG_TacGiaAdd> list, long quyenTacGiaID, Guid userID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("QuyenTacGiaID", quyenTacGiaID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("UserID", userID, DbType.Guid, ParameterDirection.Input);
                paramters.Add("table", list.ToDataTable().AsTableValuedParameter("dbo.TableTacGiaMap"));
                var data = conns.ExecuteScalar<int>("QTG_TacGia_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return data;
            }
            catch (Exception ex)
            {
                _logger.Error("QTG_TacGia_Ins Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_TacGia_Ins Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int QTG_ChuSoHuu_Ins(List<QTG_ChuSoHuuAdd> list, long quyenTacGiaID, Guid userID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("QuyenTacGiaID", quyenTacGiaID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("UserID", userID, DbType.Guid, ParameterDirection.Input);
                paramters.Add("table", list.ToDataTable().AsTableValuedParameter("dbo.TableChuSoHuuMap"));
                var data = conns.ExecuteScalar<int>("QTG_ChuSoHuu_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return data;
            }
            catch (Exception ex)
            {
                _logger.Error("QTG_ChuSoHuu_Ins Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_ChuSoHuu_Ins Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public long QTG_QuyenTacGia_Change(QTG_QuyenTacGiaChange model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("QuyenTacGiaID", model.QuyenTacGiaID, DbType.Int64, ParameterDirection.Input);
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
                var datas = conns.QueryFirstOrDefault<long>("QTG_QuyenTacGia_Change", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("QTG_QuyenTacGia_ChangeStatus Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_QuyenTacGia_ChangeStatus Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public List<QTG_TacGiaAdd> QTG_TacGia_ByQuyenTGId(long quyenTacGiaID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("QuyenTacGiaID", quyenTacGiaID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.Query<QTG_TacGiaAdd>("QTG_TacGia_ByQuyenTGId", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<QTG_TacGiaAdd> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QTG_TacGia_ByQuyenTGId Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_TacGia_ByQuyenTGId Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public List<QTG_ChuSoHuuAdd> QTG_ChuSoHuu_ByQuyenTGId(long quyenTacGiaID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("QuyenTacGiaID", quyenTacGiaID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.Query<QTG_ChuSoHuuAdd>("QTG_ChuSoHuu_ByQuyenTGId", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<QTG_ChuSoHuuAdd> ?? datas.ToList(); ;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QTG_ChuSoHuu_ByQuyenTGId Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_ChuSoHuu_ByQuyenTGId Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public int QTG_QuyenTacGia_Del(long quyenTacGiaID, Guid userID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("QuyenTacGiaID", quyenTacGiaID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("UserID", userID, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.ExecuteScalar<int>("QTG_QuyenTacGia_Del", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QTG_QuyenTacGia_Del Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_QuyenTacGia_Del Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public List<QTG_GiayChungNhanMap> QTG_QuyenTacGia_SearchGCN(QTG_GiayChungNhanParam model, out ResponseModel restStatus)
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
                    var datas = conns.Query<QTG_GiayChungNhanMap>("QTG_QuyenTacGia_SearchGCN", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<QTG_GiayChungNhanMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QTG_QuyenTacGia_SearchGCN Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_QuyenTacGia_SearchGCN Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public List<QTG_SoThuTuMap> QTG_QuyenTacGia_SearchSTT(QTG_SoThuTuParam model, out ResponseModel restStatus)
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
                    var datas = conns.Query<QTG_SoThuTuMap>("QTG_QuyenTacGia_SearchSTT", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<QTG_SoThuTuMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QTG_QuyenTacGia_SearchSTT Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_QuyenTacGia_SearchSTT Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long QTG_QuyenTacGia_OldToNew(long id, int loaiNghiepVuID, long keyMapID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("QuyenTacGiaID", id, DbType.Int64, ParameterDirection.Input);
                paramters.Add("LoaiNghiepVuID", loaiNghiepVuID, DbType.Int16, ParameterDirection.Input);
                paramters.Add("KeyMapID", keyMapID, DbType.Int64, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<long>("QTG_QuyenTacGia_OldToNew", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;
            }
            catch (Exception ex)
            {
                _logger.Error("QTG_QuyenTacGia_OldToNew Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_QuyenTacGia_OldToNew Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public long QTG_QuyenTacGia_CapSo(QTG_QuyenTacGia_CapSo model, out ResponseModel restStatus)
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
                    var datas = conns.ExecuteScalar<long>("QTG_QuyenTacGia_CapSo", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QTG_QuyenTacGia_CapSo Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_QuyenTacGia_CapSo Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int QTG_QuyenTacGia_CheckSoTT(long id, string soTT, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("STT", soTT, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("QTG_QuyenTacGia_CheckSoTT", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QTG_QuyenTacGia_CheckSoTT Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_QuyenTacGia_CheckSoTT Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int QTG_QuyenTacGia_CheckSoGCN(long id, string soGCN, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("SoGCN", soGCN, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("QTG_QuyenTacGia_CheckSoGCN", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QTG_QuyenTacGia_CheckSoGCN Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_QuyenTacGia_CheckSoGCN Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int QTG_QuyenTacGia_Duyet(long id, int loaiNghiepVuID, bool isDuyet, out ResponseModel restStatus)
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
                    var datas = conns.QueryFirstOrDefault<int>("QTG_QuyenTacGia_Duyet", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QTG_QuyenTacGia_Duyet Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_QuyenTacGia_Duyet Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        #endregion QTG_QuyenTacGia

        #region QTG_CapLai
        public QTG_CapLaiAdd QTG_CapLai_ById(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new QTG_CapLaiAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryMultiple("QTG_CapLai_ById", paramters, commandType: CommandType.StoredProcedure);
                    var capLai = datas.Read<QTG_CapLaiAdd>().FirstOrDefault();
                    var lstDinhKem = datas.Read<QTG_DinhKemAdd>();
                    if (capLai != null)
                    {
                        result = capLai;
                        if (lstDinhKem != null && lstDinhKem.Count() > 0)
                            result.ListDinhKem = lstDinhKem.ToList();
                    }
                    restStatus = new ResponseModel();
                    return result as QTG_CapLaiAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QTG_CapLai_ById Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_CapLai_ById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public QTG_CapLaiAdd QTG_CapLai_ByHoSoId(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new QTG_CapLaiAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryMultiple("QTG_CapLai_ByHoSoId", paramters, commandType: CommandType.StoredProcedure);
                    var capLai = datas.Read<QTG_CapLaiAdd>().FirstOrDefault();
                    var lstDinhKem = datas.Read<QTG_DinhKemAdd>();
                    if (capLai != null)
                    {
                        result = capLai;
                        if (lstDinhKem != null && lstDinhKem.Count() > 0)
                            result.ListDinhKem = lstDinhKem.ToList();
                    }
                    restStatus = new ResponseModel();
                    return result as QTG_CapLaiAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QTG_CapLai_ByHoSoId Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_CapLai_ByHoSoId Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public QTG_CapLaiAdd QTG_CapLai_ByQuyenTGId(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new QTG_CapLaiAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryMultiple("QTG_CapLai_ByQuyenTGId", paramters, commandType: CommandType.StoredProcedure);
                    var capLai = datas.Read<QTG_CapLaiAdd>().FirstOrDefault();
                    var lstDinhKem = datas.Read<QTG_DinhKemAdd>();
                    if (capLai != null)
                    {
                        result = capLai;
                        if (lstDinhKem != null && lstDinhKem.Count() > 0)
                            result.ListDinhKem = lstDinhKem.ToList();
                    }
                    restStatus = new ResponseModel();
                    return result as QTG_CapLaiAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QTG_CapLai_ByQuyenTGId Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_CapLai_ByQuyenTGId Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long QTG_CapLai_InsUpd(QTG_CapLaiAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
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
                paramters.Add("QuyenTacGiaID", model.QuyenTacGiaID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("QuyenTacGiaCuID", model.QuyenTacGiaCuID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("UserID", model.UserID, DbType.Guid, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<long>("QTG_CapLai_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("QTG_CapLai_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_CapLai_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        #endregion QTG_CapLai

        #region QTG_ChuyenChuSoHuu
        public QTG_ChuyenChuSoHuuAdd QTG_ChuyenChuSoHuu_ById(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new QTG_ChuyenChuSoHuuAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryMultiple("QTG_ChuyenChuSoHuu_ById", paramters, commandType: CommandType.StoredProcedure);
                    var chuyenChu = datas.Read<QTG_ChuyenChuSoHuuAdd>().FirstOrDefault();
                    var lstDinhKem = datas.Read<QTG_DinhKemAdd>();
                    if (chuyenChu != null)
                    {
                        result = chuyenChu;
                        if (lstDinhKem != null && lstDinhKem.Count() > 0)
                            result.ListDinhKem = lstDinhKem.ToList();
                    }
                    restStatus = new ResponseModel();
                    return result as QTG_ChuyenChuSoHuuAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QTG_ChuyenChuSoHuu_ById Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_ChuyenChuSoHuu_ById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public QTG_ChuyenChuSoHuuAdd QTG_ChuyenChuSoHuu_ByHoSoId(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new QTG_ChuyenChuSoHuuAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryMultiple("QTG_ChuyenChuSoHuu_ByHoSoId", paramters, commandType: CommandType.StoredProcedure);
                    var chuyenChu = datas.Read<QTG_ChuyenChuSoHuuAdd>().FirstOrDefault();
                    var lstDinhKem = datas.Read<QTG_DinhKemAdd>();
                    if (chuyenChu != null)
                    {
                        result = chuyenChu;
                        if (lstDinhKem != null && lstDinhKem.Count() > 0)
                            result.ListDinhKem = lstDinhKem.ToList();
                    }
                    restStatus = new ResponseModel();
                    return result as QTG_ChuyenChuSoHuuAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QTG_ChuyenChuSoHuu_ByHoSoId Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_ChuyenChuSoHuu_ByHoSoId Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public QTG_ChuyenChuSoHuuAdd QTG_ChuyenChuSoHuu_ByQuyenTGId(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new QTG_ChuyenChuSoHuuAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryMultiple("QTG_ChuyenChuSoHuu_ByQuyenTGId", paramters, commandType: CommandType.StoredProcedure);
                    var chuyenChu = datas.Read<QTG_ChuyenChuSoHuuAdd>().FirstOrDefault();
                    var lstDinhKem = datas.Read<QTG_DinhKemAdd>();
                    if (chuyenChu != null)
                    {
                        result = chuyenChu;
                        if (lstDinhKem != null && lstDinhKem.Count() > 0)
                            result.ListDinhKem = lstDinhKem.ToList();
                    }
                    restStatus = new ResponseModel();
                    return result as QTG_ChuyenChuSoHuuAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QTG_ChuyenChuSoHuu_ByQuyenTGId Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_ChuyenChuSoHuu_ByQuyenTGId Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long QTG_ChuyenChuSoHuu_InsUpd(QTG_ChuyenChuSoHuuAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
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
                paramters.Add("QuyenTacGiaID", model.QuyenTacGiaID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("QuyenTacGiaCuID", model.QuyenTacGiaCuID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("UserID", model.UserID, DbType.Guid, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<long>("QTG_ChuyenChuSoHuu_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("QTG_ChuyenChuSoHuu_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_ChuyenChuSoHuu_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        #endregion QTG_ChuyenChuSoHuu

        #region QTG_CapDoi
        public QTG_CapDoiAdd QTG_CapDoi_ById(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new QTG_CapDoiAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryMultiple("QTG_CapDoi_ById", paramters, commandType: CommandType.StoredProcedure);
                    var capLai = datas.Read<QTG_CapDoiAdd>().FirstOrDefault();
                    var lstDinhKem = datas.Read<QTG_DinhKemAdd>();
                    if (capLai != null)
                    {
                        result = capLai;
                        if (lstDinhKem != null && lstDinhKem.Count() > 0)
                            result.ListDinhKem = lstDinhKem.ToList();
                    }
                    restStatus = new ResponseModel();
                    return result as QTG_CapDoiAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QTG_CapDoi_ById Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_CapDoi_ById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public QTG_CapDoiAdd QTG_CapDoi_ByHoSoId(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new QTG_CapDoiAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryMultiple("QTG_CapDoi_ByHoSoId", paramters, commandType: CommandType.StoredProcedure);
                    var capLai = datas.Read<QTG_CapDoiAdd>().FirstOrDefault();
                    var lstDinhKem = datas.Read<QTG_DinhKemAdd>();
                    if (capLai != null)
                    {
                        result = capLai;
                        if (lstDinhKem != null && lstDinhKem.Count() > 0)
                            result.ListDinhKem = lstDinhKem.ToList();
                    }
                    restStatus = new ResponseModel();
                    return result as QTG_CapDoiAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QTG_CapDoi_ByHoSoId Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_CapDoi_ByHoSoId Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public QTG_CapDoiAdd QTG_CapDoi_ByQuyenTGId(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new QTG_CapDoiAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryMultiple("QTG_CapDoi_ByQuyenTGId", paramters, commandType: CommandType.StoredProcedure);
                    var capLai = datas.Read<QTG_CapDoiAdd>().FirstOrDefault();
                    var lstDinhKem = datas.Read<QTG_DinhKemAdd>();
                    if (capLai != null)
                    {
                        result = capLai;
                        if (lstDinhKem != null && lstDinhKem.Count() > 0)
                            result.ListDinhKem = lstDinhKem.ToList();
                    }
                    restStatus = new ResponseModel();
                    return result as QTG_CapDoiAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QTG_CapDoi_ByQuyenTGId Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_CapDoi_ByQuyenTGId Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long QTG_CapDoi_InsUpd(QTG_CapDoiAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
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
                paramters.Add("QuyenTacGiaID", model.QuyenTacGiaID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("QuyenTacGiaCuID", model.QuyenTacGiaCuID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("UserID", model.UserID, DbType.Guid, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<long>("QTG_CapDoi_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("QTG_CapDoi_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_CapDoi_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        #endregion QTG_CapDoi

        #region QTG_ThuHoi
        public QTG_ThuHoiAdd QTG_ThuHoi_ById(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new QTG_ThuHoiAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryMultiple("QTG_ThuHoi_ById", paramters, commandType: CommandType.StoredProcedure);
                    var thuHoi = datas.Read<QTG_ThuHoiAdd>().FirstOrDefault();
                    var lstDinhKem = datas.Read<QTG_DinhKemAdd>();
                    if (thuHoi != null)
                    {
                        result = thuHoi;
                        if (lstDinhKem != null && lstDinhKem.Count() > 0)
                            result.ListDinhKem = lstDinhKem.ToList();
                    }
                    restStatus = new ResponseModel();
                    return result as QTG_ThuHoiAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QTG_ThuHoi_ById Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_ThuHoi_ById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public QTG_ThuHoiAdd QTG_ThuHoi_ByHoSoId(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new QTG_ThuHoiAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryMultiple("QTG_ThuHoi_ByHoSoId", paramters, commandType: CommandType.StoredProcedure);
                    var thuHoi = datas.Read<QTG_ThuHoiAdd>().FirstOrDefault();
                    var lstDinhKem = datas.Read<QTG_DinhKemAdd>();
                    if (thuHoi != null)
                    {
                        result = thuHoi;
                        if (lstDinhKem != null && lstDinhKem.Count() > 0)
                            result.ListDinhKem = lstDinhKem.ToList();
                    }
                    restStatus = new ResponseModel();
                    return result as QTG_ThuHoiAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QTG_ThuHoi_ByHoSoId Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_ThuHoi_ByHoSoId Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public QTG_ThuHoiAdd QTG_ThuHoi_ByQuyenTGId(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new QTG_ThuHoiAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryMultiple("QTG_ThuHoi_ByQuyenTGId", paramters, commandType: CommandType.StoredProcedure);
                    var thuHoi = datas.Read<QTG_ThuHoiAdd>().FirstOrDefault();
                    var lstDinhKem = datas.Read<QTG_DinhKemAdd>();
                    if (thuHoi != null)
                    {
                        result = thuHoi;
                        if (lstDinhKem != null && lstDinhKem.Count() > 0)
                            result.ListDinhKem = lstDinhKem.ToList();
                    }
                    restStatus = new ResponseModel();
                    return result as QTG_ThuHoiAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QTG_ThuHoi_ByQuyenTGId Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_ThuHoi_ByQuyenTGId Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long QTG_ThuHoi_InsUpd(QTG_ThuHoiAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
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
                paramters.Add("QuyenTacGiaID", model.QuyenTacGiaID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("UserID", model.UserID, DbType.Guid, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<long>("QTG_ThuHoi_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("QTG_ThuHoi_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_ThuHoi_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        #endregion QTG_ThuHoi

        #region QTG_DinhKem
        public int QTG_DinhKem_Ins(List<QTG_DinhKemAdd> list, long quyenTacGiaID, int loaiXuLyID, long keyMapID, Guid userID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                list = list.Where(x => x.IsMotCua == false).ToList();
                var paramters = new DynamicParameters();
                paramters.Add("QuyenTacGiaID", quyenTacGiaID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("LoaiXuLyID", loaiXuLyID, DbType.Int32, ParameterDirection.Input);
                paramters.Add("KeyMapID", keyMapID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("UserID", userID, DbType.Guid, ParameterDirection.Input);
                paramters.Add("table", list.ToDataTable().AsTableValuedParameter("dbo.TableDinhKemMap"));
                var data = conns.ExecuteScalar<int>("QTG_DinhKem_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return data;
            }
            catch (Exception ex)
            {
                _logger.Error("QTG_DinhKem_Ins Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_DinhKem_Ins Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        #endregion QTG_DinhKem

        #region QTG_LichSu
        public List<QTG_LichSuMap> QTG_LichSu_ById(long id, long staticID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("StaticID", staticID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.Query<QTG_LichSuMap>("QTG_LichSu_ById", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<QTG_LichSuMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("QTG_LichSu_ById Error: " + ex.StackTrace);
                //log db
                _log.Error("QTG_LichSu_ById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        #endregion QTG_LichSu
    }
}
