using Business.Entities.DataModel;
using Core.Common.Utilities;
using Dapper;
using Data.Core.Repositories.Base;
using Data.Core.Repositories.Interfaces;
using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace Data.Core.Repositories
{
    public class TC_GiayChungNhanRepository : Repository<TC_GiayChungNhanMap>, ITC_GiayChungNhanRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(TC_GiayChungNhanRepository));
        private const string TableName = "TC_GiayChungNhan";
        private static readonly string ChuyenNganhConn = ConfigurationManager.ConnectionStrings["ChuyenNganh.ConnString"].ConnectionString;


        public TC_GiayChungNhanRepository(ILog logger) : base(TableName, ChuyenNganhConn)
        {
            _logger = logger;
        }

        public List<TC_GiayChungNhanMap> TC_GiayChungNhan_List(TC_GiayChungNhanMapParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = Connection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("LoaiDangKyID", model.LoaiDangKyID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("TuNgay", model.TuNgay, DbType.String, ParameterDirection.Input);
                    paramters.Add("DenNgay", model.DenNgay, DbType.String, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<TC_GiayChungNhanMap>("TC_GiayChungNhan_List", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<TC_GiayChungNhanMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("TC_GiayChungNhan_List Error: " + ex.StackTrace);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public TC_GiayChungNhanAdd TC_GiayChungNhan_GetDetail(TC_GiayChungNhanAddParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = Connection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("SoGiayChungNhan", model.SoGiayChungNhan, DbType.String, ParameterDirection.Input);
                    paramters.Add("NgayCap", model.NgayCap, DbType.String, ParameterDirection.Input);
                    paramters.Add("TacPham", model.TacPham, DbType.String, ParameterDirection.Input);
                    paramters.Add("TacGia", model.TacGia, DbType.String, ParameterDirection.Input);
                    paramters.Add("NguoiBieuDien", model.NguoiBieuDien, DbType.String, ParameterDirection.Input);
                    paramters.Add("ChuSoHuu", model.ChuSoHuu, DbType.String, ParameterDirection.Input);
                    var result = conns.QuerySingleOrDefault<TC_GiayChungNhanAdd>("TC_GiayChungNhan_GetDetail", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("TC_GiayChungNhan_ById Error: " + ex.StackTrace);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public List<TC_GiayChungNhanCongBaoMap> TC_GiayChungNhanCongBao_List(TC_GiayChungNhanCongBaoParam model, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = Connection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("LoaiDangKyID", model.LoaiDangKyID, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("TuNgay", model.TuNgay, DbType.String, ParameterDirection.Input);
                    paramters.Add("DenNgay", model.DenNgay, DbType.String, ParameterDirection.Input);
                    paramters.Add("PageIndex", model.PageIndex, DbType.Int32, ParameterDirection.Input);
                    paramters.Add("PageSize", model.PageSize, DbType.Int32, ParameterDirection.Input);
                    var datas = conns.Query<TC_GiayChungNhanCongBaoMap>("TC_GiayChungNhanCongBao_List", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return datas as List<TC_GiayChungNhanCongBaoMap> ?? datas.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("TC_GiayChungNhanCongBao_List Error: " + ex.StackTrace);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public List<TC_ThongTinSoHuu> QTG_ThongTinSoHuu_ByQuyenId(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = Connection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("QuyenID", id, DbType.String, ParameterDirection.Input);
                    var result = conns.Query<TC_ThongTinSoHuu>("QTG_ThongTinSoHuu_ByQuyenId", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return result as List<TC_ThongTinSoHuu> ?? result.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QTG_ThongTinSoHuu_ByQuyenId Error: " + ex.StackTrace);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
        public List<TC_ThongTinSoHuu> QLQ_ThongTinSoHuu_ByQuyenId(long id, out ResponseModel restStatus)
        {
            try
            {
                using (IDbConnection conns = Connection)
                {
                    conns.Open();
                    var paramters = new DynamicParameters();
                    paramters.Add("QuyenID", id, DbType.String, ParameterDirection.Input);
                    var result = conns.Query<TC_ThongTinSoHuu>("QLQ_ThongTinSoHuu_ByQuyenId", paramters, commandType: CommandType.StoredProcedure);
                    restStatus = new ResponseModel();
                    return result as List<TC_ThongTinSoHuu> ?? result.ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error("QLQ_ThongTinSoHuu_ByQuyenId Error: " + ex.StackTrace);
                restStatus = new ResponseModel(ex);
                return null;
            }
        }
    }
}
