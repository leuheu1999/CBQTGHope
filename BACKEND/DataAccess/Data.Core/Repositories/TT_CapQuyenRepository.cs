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
    public class TT_CapQuyenRepository : Repository<TT_CapQuyenMap>, ITT_CapQuyenRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(TT_CapQuyenRepository));
        private const string TableName = "";
        private readonly ILogger _log;
        public TT_CapQuyenRepository(ILog logger, ILogger log) : base(TableName)
        {
            _logger = logger;
            _log = log;
        }

        public int TT_CapQuyen_CheckCapSo(long hoSoID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("HoSoID", hoSoID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("TT_CapQuyen_CheckCapSo", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("TT_CapQuyen_CheckCapSo Error: " + ex.StackTrace);
                //log db
                _log.Error("TT_CapQuyen_CheckCapSo Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public TT_CapQuyenAdd TT_CapQuyen_GetByHoSoId(long hoSoID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("HoSoID", hoSoID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<TT_CapQuyenAdd>("TT_CapQuyen_GetByHoSoId", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as TT_CapQuyenAdd ?? datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("TT_CapQuyen_GetByHoSoId Error: " + ex.StackTrace);
                //log db
                _log.Error("TT_CapQuyen_GetByHoSoId Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public long TT_CapQuyen_InsUpd(TT_CapQuyenAdd model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("CapQuyenID", model.CapQuyenID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("HoSoID", model.HoSoID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("ThuTucID", model.ThuTucID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("TenThuTuc", model.TenThuTuc, DbType.String, ParameterDirection.Input);
                paramters.Add("MaBoHoSo", model.MaBoHoSo, DbType.String, ParameterDirection.Input);
                paramters.Add("SoBienNhan", model.SoBienNhan, DbType.String, ParameterDirection.Input);
                paramters.Add("LoaiNghiepVuID", model.LoaiNghiepVuID, DbType.Int32, ParameterDirection.Input);
                paramters.Add("TenLoaiNghiepVu", model.TenLoaiNghiepVu, DbType.String, ParameterDirection.Input);
                paramters.Add("KeyMapID", model.KeyMapID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("LinhVucID", model.LinhVucID, DbType.Int32, ParameterDirection.Input);
                paramters.Add("TenLinhVuc", model.TenLinhVuc, DbType.String, ParameterDirection.Input);
                paramters.Add("NguoiKyID", model.NguoiKyID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("HoVaTen", model.HoVaTen, DbType.String, ParameterDirection.Input);
                paramters.Add("TinhThanhID", model.TinhThanhID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("TenTinhThanh", model.TenTinhThanh, DbType.String, ParameterDirection.Input);
                paramters.Add("QuanHuyenID", model.QuanHuyenID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("TenQuanHuyen", model.TenQuanHuyen, DbType.String, ParameterDirection.Input);
                paramters.Add("PhuongXaID", model.PhuongXaID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("TenPhuongXa", model.TenPhuongXa, DbType.String, ParameterDirection.Input);
                paramters.Add("DiaChi", model.DiaChi, DbType.String, ParameterDirection.Input);
                paramters.Add("NgayNhan", model.NgayNhan, DbType.String, ParameterDirection.Input);
                paramters.Add("NgayHenTra", model.NgayHenTra, DbType.String, ParameterDirection.Input);
                paramters.Add("SoNgayGiaiQuyet", model.SoNgayGiaiQuyet, DbType.Int32, ParameterDirection.Input);
                paramters.Add("UserID", model.UserID, DbType.Guid, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<long>("TT_CapQuyen_InsUpd", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("TT_CapQuyen_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("TT_CapQuyen_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public List<TT_CapQuyenMap> TT_CapQuyen_List(TT_CapQuyenParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("SoBienNhan", model.SoBienNhan, DbType.String, ParameterDirection.Input);
                    paramters.Add("MaBoHoSo", model.MaBoHoSo, DbType.String, ParameterDirection.Input);
                    paramters.Add("CongDanToChuc", model.CongDanToChuc, DbType.String, ParameterDirection.Input);
                    paramters.Add("TuNgay", model.TuNgay, DbType.String, ParameterDirection.Input);
                    paramters.Add("DenNgay", model.DenNgay, DbType.String, ParameterDirection.Input);
                    paramters.Add("LinhVucID", model.LinhVucID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("ThuTucID", model.ThuTucID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("TinhThanhID", model.TinhThanhID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("QuanHuyenID", model.QuanHuyenID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("PhuongXaID", model.PhuongXaID, DbType.Int64, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<TT_CapQuyenMap>("TT_CapQuyen_List", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas.ToList() ?? new List<TT_CapQuyenMap>();
                }

            }
            catch (Exception ex)
            {
                _logger.Error("TT_CapQuyen_InsUpd Error: " + ex.StackTrace);
                //log db
                _log.Error("TT_CapQuyen_InsUpd Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return new List<TT_CapQuyenMap>() ;
            }
        }
        public TT_CapQuyenAddDuyet TT_CapQuyen_UpdTinhTrang(TT_CapQuyenAddTrangThai model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("CapQuyenID", model.CapQuyenID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("TrangThaiDuyet", model.TrangThaiDuyet, DbType.Int32, ParameterDirection.Input);
                paramters.Add("CreatedUserID", model.CreatedUserID, DbType.Guid, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<TT_CapQuyenAddDuyet>("TT_CapQuyen_UpdTinhTrang", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("TT_CapQuyen_UpdTinhTrang Error: " + ex.StackTrace);
                //log db
                _log.Error("TT_CapQuyen_UpdTinhTrang Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public int TT_CapQuyen_UpdDuyet(TT_CapQuyenAddDuyet model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("LoaiNghiepVuID", model.LoaiNghiepVuID, DbType.Int32, ParameterDirection.Input);
                paramters.Add("KeyMapID", model.KeyMapID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("CreatedUserID", model.CreatedUserID, DbType.Guid, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<int>("TT_CapQuyen_UpdDuyet", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("TT_CapQuyen_UpdDuyet Error: " + ex.StackTrace);
                //log db
                _log.Error("TT_CapQuyen_UpdDuyet Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
        public int TT_CapQuyen_UpdKhongDuyet(TT_CapQuyenAddDuyet model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus)
        {
            try
            {
                if (conns.State == ConnectionState.Closed)
                    conns.Open();
                var paramters = new DynamicParameters();
                paramters.Add("LoaiNghiepVuID", model.LoaiNghiepVuID, DbType.Int32, ParameterDirection.Input);
                paramters.Add("KeyMapID", model.KeyMapID, DbType.Int64, ParameterDirection.Input);
                paramters.Add("CreatedUserID", model.CreatedUserID, DbType.Guid, ParameterDirection.Input);
                var datas = conns.QueryFirstOrDefault<int>("TT_CapQuyen_UpdKhongDuyet", paramters, trans, commandType: CommandType.StoredProcedure);
                restStatus = new ResponseModel();
                return datas;

            }
            catch (Exception ex)
            {
                _logger.Error("TT_CapQuyen_UpdKhongDuyet Error: " + ex.StackTrace);
                //log db
                _log.Error("TT_CapQuyen_UpdKhongDuyet Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1 ;
            }
        }
        public int TT_CapQuyen_GetLoaiNghiepVuId(long hoSoID, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("HoSoID", hoSoID, DbType.Int64, ParameterDirection.Input);
                    var datas = conns.QueryFirstOrDefault<int>("TT_CapQuyen_GetLoaiNghiepVuId", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("TT_CapQuyen_GetLoaiNghiepVuId Error: " + ex.StackTrace);
                //log db
                _log.Error("TT_CapQuyen_GetLoaiNghiepVuId Error: " + ex.Message, ex, new Guid());
                restStatus = new ResponseModel(ex);
                return -1;
            }
        }
    }
}
