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
    public class DVC_QuyenTacGiaRepository : Repository<DVC_QTG_QuyenTacGiaMap>, IDVC_QuyenTacGiaRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(DVC_QuyenTacGiaRepository));
        private const string TableName = "";
        private readonly ILogger _log;
        public DVC_QuyenTacGiaRepository(ILog logger, ILogger log) : base(TableName)
        {
            _logger = logger;
            _log = log;
        }

        #region DVC_QTG_QuyenTacGia
        public List<DVC_QTG_QuyenTacGiaMap> DVC_QTG_QuyenTacGia_List(DVC_QTG_QuyenTacGiaParam model, out ResponseModel restStatus)
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
                    var datas = conns.Query<DVC_QTG_QuyenTacGiaMap>("DVC_QTG_QuyenTacGia_List", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<DVC_QTG_QuyenTacGiaMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QTG_QuyenTacGia_List Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QTG_QuyenTacGia_List Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public DVC_QTG_QuyenTacGiaAdd DVC_QTG_QuyenTacGia_ById(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var result = new DVC_QTG_QuyenTacGiaAdd();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryMultiple("DVC_QTG_QuyenTacGia_ById", paramters, commandType: CommandType.StoredProcedure);
                    var quyenTacGia = datas.Read<DVC_QTG_QuyenTacGiaAdd>().FirstOrDefault();
                    var lstTacGia = datas.Read<DVC_QTG_TacGiaAdd>();
                    var lstChuSoHuu = datas.Read<DVC_QTG_ChuSoHuuAdd>();
                    var lstDinhKem = datas.Read<DVC_QTG_DinhKemAdd>();
                    if (quyenTacGia != null)
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
                    return result as DVC_QTG_QuyenTacGiaAdd ?? result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QTG_QuyenTacGia_ById Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QTG_QuyenTacGia_ById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long DVC_QTG_QuyenTacGia_GetStt(out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    var datas = conns.QueryFirstOrDefault<long>("DVC_QTG_QuyenTacGia_GetStt", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QTG_QuyenTacGia_GetStt Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QTG_QuyenTacGia_GetStt Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public long DVC_QTG_QuyenTacGia_InsUpd(DVC_QTG_QuyenTacGiaAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
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
                paramters.Add("NgayKy", model.NgayKy, DbType.String, ParameterDirection.Input);
                paramters.Add("UserID", model.UserID, DbType.Guid, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<long>("DVC_QTG_QuyenTacGia_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QTG_QuyenTacGia_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QTG_QuyenTacGia_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int DVC_QTG_TacGia_Ins(List<DVC_QTG_TacGiaAdd> list, long quyenTacGiaID, Guid userID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("QuyenTacGiaID", quyenTacGiaID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("UserID", userID, DbType.Guid, ParameterDirection.Input);
                paramters.Add("table", list.ToDataTable().AsTableValuedParameter("dbo.TableTacGiaMap"));
                var data = conns.ExecuteScalar<int>("DVC_QTG_TacGia_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return data;
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QTG_TacGia_Ins Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QTG_TacGia_Ins Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int DVC_QTG_ChuSoHuu_Ins(List<DVC_QTG_ChuSoHuuAdd> list, long quyenTacGiaID, Guid userID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("QuyenTacGiaID", quyenTacGiaID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("UserID", userID, DbType.Guid, ParameterDirection.Input);
                paramters.Add("table", list.ToDataTable().AsTableValuedParameter("dbo.TableChuSoHuuMap"));
                var data = conns.ExecuteScalar<int>("DVC_QTG_ChuSoHuu_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return data;
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QTG_ChuSoHuu_Ins Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QTG_ChuSoHuu_Ins Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public long DVC_QTG_QuyenTacGia_Change(DVC_QTG_QuyenTacGiaChange model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
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
                var datas = conns.QueryFirstOrDefault<long>("DVC_QTG_QuyenTacGia_Change", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QTG_QuyenTacGia_ChangeStatus Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QTG_QuyenTacGia_ChangeStatus Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public List<DVC_QTG_TacGiaAdd> DVC_QTG_TacGia_ByQuyenTGId(long quyenTacGiaID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("QuyenTacGiaID", quyenTacGiaID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.Query<DVC_QTG_TacGiaAdd>("DVC_QTG_TacGia_ByQuyenTGId", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<DVC_QTG_TacGiaAdd> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QTG_TacGia_ByQuyenTGId Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QTG_TacGia_ByQuyenTGId Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public List<DVC_QTG_ChuSoHuuAdd> DVC_QTG_ChuSoHuu_ByQuyenTGId(long quyenTacGiaID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("QuyenTacGiaID", quyenTacGiaID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.Query<DVC_QTG_ChuSoHuuAdd>("DVC_QTG_ChuSoHuu_ByQuyenTGId", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<DVC_QTG_ChuSoHuuAdd> ?? datas.ToList(); ;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QTG_ChuSoHuu_ByQuyenTGId Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QTG_ChuSoHuu_ByQuyenTGId Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public int DVC_QTG_QuyenTacGia_Del(long quyenTacGiaID, Guid userID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("QuyenTacGiaID", quyenTacGiaID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("UserID", userID, DbType.Guid, ParameterDirection.Input);
                    var datas = conns.ExecuteScalar<int>("DVC_QTG_QuyenTacGia_Del", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QTG_QuyenTacGia_Del Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QTG_QuyenTacGia_Del Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public List<DVC_QTG_GiayChungNhanMap> DVC_QTG_QuyenTacGia_SearchGCN(DVC_QTG_GiayChungNhanParam model, out ResponseModel restStatus)
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
                    var datas = conns.Query<DVC_QTG_GiayChungNhanMap>("DVC_QTG_QuyenTacGia_SearchGCN", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<DVC_QTG_GiayChungNhanMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QTG_QuyenTacGia_SearchGCN Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QTG_QuyenTacGia_SearchGCN Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long DVC_QTG_QuyenTacGia_OldToNew(long id, int loaiNghiepVuID, long keyMapID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("QuyenTacGiaID", id, DbType.Int64, ParameterDirection.Input);
                paramters.Add("LoaiNghiepVuID", loaiNghiepVuID, DbType.Int16, ParameterDirection.Input);
                paramters.Add("KeyMapID", keyMapID, DbType.Int64, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<long>("DVC_QTG_QuyenTacGia_OldToNew", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QTG_QuyenTacGia_OldToNew Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QTG_QuyenTacGia_OldToNew Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public long DVC_QTG_QuyenTacGia_CapSo(DVC_QTG_QuyenTacGia_CapSo model, out ResponseModel restStatus)
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
                    var datas = conns.ExecuteScalar<long>("DVC_QTG_QuyenTacGia_CapSo", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QTG_QuyenTacGia_CapSo Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QTG_QuyenTacGia_CapSo Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int DVC_QTG_QuyenTacGia_CheckSoTT(long id, string soTT, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("STT", soTT, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("DVC_QTG_QuyenTacGia_CheckSoTT", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QTG_QuyenTacGia_CheckSoTT Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QTG_QuyenTacGia_CheckSoTT Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int DVC_QTG_QuyenTacGia_CheckSoGCN(long id, string soGCN, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("SoGCN", soGCN, DbType.String, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("DVC_QTG_QuyenTacGia_CheckSoGCN", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QTG_QuyenTacGia_CheckSoGCN Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QTG_QuyenTacGia_CheckSoGCN Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        #endregion DVC_QTG_QuyenTacGia

        #region DVC_QTG_DinhKem
        public int DVC_QTG_DinhKem_Ins(List<DVC_QTG_DinhKemAdd> list, long quyenTacGiaID, int loaiXuLyID, long keyMapID, Guid userID, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
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
                var data = conns.ExecuteScalar<int>("DVC_QTG_DinhKem_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return data;
            }
            catch (Exception ex)
            {
                _logger.Error("DVC_QTG_DinhKem_Ins Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QTG_DinhKem_Ins Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        #endregion DVC_QTG_DinhKem

        #region DVC_QTG_LichSu
        public List<DVC_QTG_LichSuMap> DVC_QTG_LichSu_ById(long id, long staticID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("ID", id, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("StaticID", staticID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.Query<DVC_QTG_LichSuMap>("DVC_QTG_LichSu_ById", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<DVC_QTG_LichSuMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("DVC_QTG_LichSu_ById Error: " + ex.StackTrace);
                //log db
                _log.Error("DVC_QTG_LichSu_ById Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        #endregion DVC_QTG_LichSu
    }
}
