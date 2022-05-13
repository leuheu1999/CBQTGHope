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
    public class BC_ThongKeRepository : Repository<DM_DungChungMap>, IBC_ThongKeRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(DM_TinhThanhRepository));
        private const string TableName = "";
        private readonly ILogger _log;
        public BC_ThongKeRepository(ILog logger, ILogger log) : base(TableName)
        {
            _logger = logger;
            _log = log;
        }
        public List<BC_ThongKeGiayChungNhanMap> BC_ThongKeGiayChungNhan_List(BC_ThongKeGiayChungNhanParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("KyBaoCao", model.KyBaoCao, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("Nam", model.Nam, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("Quy", model.Quy, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("Thang", model.Thang, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("TuNgay", model.TuNgay, DbType.String, ParameterDirection.Input);
                    paramters.Add("DenNgay", model.DenNgay, DbType.String, ParameterDirection.Input);
                    paramters.Add("LoaiHinhID", model.LoaiHinhID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("VungMienID", model.VungMienID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("LinhVucID", model.LinhVucID, DbType.Int32, ParameterDirection.Input);
                    //paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    //paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<BC_ThongKeGiayChungNhanMap>("BC_ThongKeGiayChungNhan_List", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<BC_ThongKeGiayChungNhanMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("BC_ThongKeGiayChungNhan_List Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("BC_ThongKeGiayChungNhan_List Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }

        public List<BC_CongBaoMap> BC_CongBao_List(BC_CongBaoParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("KyBaoCao", model.KyBaoCao, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("Nam", model.Nam, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("Quy", model.Quy, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("Thang", model.Thang, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("TuNgay", model.TuNgay, DbType.String, ParameterDirection.Input);
                    paramters.Add("DenNgay", model.DenNgay, DbType.String, ParameterDirection.Input);
                    paramters.Add("LoaiHinhID", model.LoaiHinhID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("VungMienID", model.VungMienID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("QuyenID", model.QuyenID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("LoaiThuTucID", model.LoaiThuTucID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("TacGia", model.TacGia, DbType.String, ParameterDirection.Input);
                    paramters.Add("ChuSoHuu", model.ChuSoHuu, DbType.String, ParameterDirection.Input);
                    paramters.Add("NguoiDaiDien", model.NguoiDaiDien, DbType.String, ParameterDirection.Input);
                    paramters.Add("IsToChuc", model.IsToChuc, DbType.Boolean, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<BC_CongBaoMap>("BC_CongBao_List", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<BC_CongBaoMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("BC_CongBao_List Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("BC_CongBao_List Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }

        public List<BC_BaoCaoTacPhamMap> BC_BaoCaoTacPham_List(BC_BaoCaoTacPhamParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("KyBaoCao", model.KyBaoCao, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("Nam", model.Nam, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("Quy", model.Quy, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("Thang", model.Thang, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("TuNgay", model.TuNgay, DbType.String, ParameterDirection.Input);
                    paramters.Add("DenNgay", model.DenNgay, DbType.String, ParameterDirection.Input);
                    paramters.Add("LoaiHinhID", model.LoaiHinhID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("VungMienID", model.VungMienID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("QuyenID", model.QuyenID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("LoaiThuTucID", model.LoaiThuTucID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("TacGia", model.TacGia, DbType.String, ParameterDirection.Input);
                    paramters.Add("ChuSoHuu", model.ChuSoHuu, DbType.String, ParameterDirection.Input);
                    paramters.Add("NguoiDaiDien", model.NguoiDaiDien, DbType.String, ParameterDirection.Input);
                    paramters.Add("IsToChuc", model.IsToChuc, DbType.Boolean, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<BC_BaoCaoTacPhamMap>("BC_BaoCaoTacPham_List", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<BC_BaoCaoTacPhamMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("BC_BaoCaoTacPham_List Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("BC_BaoCaoTacPham_List Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }

        public List<BC_BaoCaoTongHopHoatDongMap> BC_BaoCaoTongHopHoatDong_List(BC_BaoCaoTongHopHoatDongParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("KyBaoCao", model.KyBaoCao, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("Nam", model.Nam, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("Quy", model.Quy, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("Thang", model.Thang, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("TuNgay", model.TuNgay, DbType.String, ParameterDirection.Input);
                    paramters.Add("DenNgay", model.DenNgay, DbType.String, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<BC_BaoCaoTongHopHoatDongMap>("BC_BaoCaoTongHopHoatDong_List", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<BC_BaoCaoTongHopHoatDongMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("BC_BaoCaoTongHopHoatDong_List Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("BC_BaoCaoTongHopHoatDong_List Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }

        public List<BC_SoGiayChungNhanBanQuyenMap> BC_SoGiayChungNhanBanQuyen_List(BC_SoGiayChungNhanBanQuyenParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("KyBaoCao", model.KyBaoCao, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("Nam", model.Nam, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("Quy", model.Quy, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("Thang", model.Thang, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("TuNgay", model.TuNgay, DbType.String, ParameterDirection.Input);
                    paramters.Add("DenNgay", model.DenNgay, DbType.String, ParameterDirection.Input);
                    paramters.Add("LoaiHinhID", model.LoaiHinhID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("VungMienID", model.VungMienID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("QuyenID", model.QuyenID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("LoaiThuTucID", model.LoaiThuTucID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("TacGia", model.TacGia, DbType.String, ParameterDirection.Input);
                    paramters.Add("ChuSoHuu", model.ChuSoHuu, DbType.String, ParameterDirection.Input);
                    paramters.Add("NguoiDaiDien", model.NguoiDaiDien, DbType.String, ParameterDirection.Input);
                    paramters.Add("IsToChuc", model.IsToChuc, DbType.Boolean, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<BC_SoGiayChungNhanBanQuyenMap>("BC_SoGiayChungNhanBanQuyen_List", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<BC_SoGiayChungNhanBanQuyenMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("BC_SoGiayChungNhanBanQuyen_List Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("BC_SoGiayChungNhanBanQuyen_List Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }

        public List<BC_ThongKeHoSoQuyenTacGiaMap> BC_ThongKeHoSoQuyenTacGia_Dashboard(BC_ThongKeHoSoQuyenTacGiaParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Nam", model.Nam, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<BC_ThongKeHoSoQuyenTacGiaMap>("BC_ThongKeHoSoQuyenTacGia_Dashboard", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<BC_ThongKeHoSoQuyenTacGiaMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("BC_ThongKeHoSoQuyenTacGia_Dashboard Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("BC_ThongKeHoSoQuyenTacGia_Dashboard Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }

        public List<BC_ThongKeHoSoQuyenLienQuanMap> BC_ThongKeHoSoQuyenLienQuan_Dashboard(BC_ThongKeHoSoQuyenLienQuanParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = ChuyenNganhConnection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("Nam", model.Nam, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<BC_ThongKeHoSoQuyenLienQuanMap>("BC_ThongKeHoSoQuyenLienQuan_Dashboard", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<BC_ThongKeHoSoQuyenLienQuanMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                // ghi log file
                log4net.Config.XmlConfigurator.Configure();
                _logger.Error("BC_ThongKeHoSoQuyenLienQuan_Dashboard Error: " + ex.StackTrace);
                // ghi log db
                _log.Error("BC_ThongKeHoSoQuyenLienQuan_Dashboard Error", ex, new Guid());
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
    }
}
