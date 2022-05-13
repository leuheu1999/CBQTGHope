using Business.Entities.Domain;
using Business.Services.Interfaces;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using Data.Core.Repositories.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Business.Services
{
    public class BC_ThongKeService : IBC_ThongKeService
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(BC_ThongKeService));

        #region Private Repository & contractor       
        private static IBC_ThongKeRepository _bC_ThongKeRepository;
        private static string Conn = null;
        private string _key = "";
        public BC_ThongKeService(IBC_ThongKeRepository bC_ThongKeRepository)
        {
            _bC_ThongKeRepository = bC_ThongKeRepository;
            var settings = new DataSettingsManager();
            Conn = settings.GetStringConnectChuyenNganh();
        }
        #endregion

        public ResultResponse<List<BC_ThongKeGiayChungNhanMap>> BC_ThongKeGiayChungNhan_List(BC_ThongKeGiayChungNhanParam model)
        {
            var response = new ResponseModel();
            var data = _bC_ThongKeRepository.BC_ThongKeGiayChungNhan_List(model, out response);
            return new ResultResponse<List<BC_ThongKeGiayChungNhanMap>>(response, data);
        }

        public ResultResponse<List<BC_CongBaoMap>> BC_CongBao_List(BC_CongBaoParam model)
        {
            var response = new ResponseModel();
            var data = _bC_ThongKeRepository.BC_CongBao_List(model, out response);
            return new ResultResponse<List<BC_CongBaoMap>>(response, data);
        }
        public ResultResponse<List<BC_BaoCaoTacPhamMap>> BC_BaoCaoTacPham_List(BC_BaoCaoTacPhamParam model)
        {
            var response = new ResponseModel();
            var data = _bC_ThongKeRepository.BC_BaoCaoTacPham_List(model, out response);
            return new ResultResponse<List<BC_BaoCaoTacPhamMap>>(response, data);
        }

        public ResultResponse<List<BC_BaoCaoTongHopHoatDongMap>> BC_BaoCaoTongHopHoatDong_List(BC_BaoCaoTongHopHoatDongParam model)
        {
            var response = new ResponseModel();
            var data = _bC_ThongKeRepository.BC_BaoCaoTongHopHoatDong_List(model, out response);
            return new ResultResponse<List<BC_BaoCaoTongHopHoatDongMap>>(response, data);
        }
        public ResultResponse<List<BC_SoGiayChungNhanBanQuyenMap>> BC_SoGiayChungNhanBanQuyen_List(BC_SoGiayChungNhanBanQuyenParam model)
        {
            var response = new ResponseModel();
            var data = _bC_ThongKeRepository.BC_SoGiayChungNhanBanQuyen_List(model, out response);
            return new ResultResponse<List<BC_SoGiayChungNhanBanQuyenMap>>(response, data);
        }

        public ResultResponse<List<BC_ThongKeHoSoQuyenTacGiaMap>> BC_ThongKeHoSoQuyenTacGia_Dashboard(BC_ThongKeHoSoQuyenTacGiaParam model)
        {
            var response = new ResponseModel();
            var data = _bC_ThongKeRepository.BC_ThongKeHoSoQuyenTacGia_Dashboard(model, out response);
            return new ResultResponse<List<BC_ThongKeHoSoQuyenTacGiaMap>>(response, data);
        }

        public ResultResponse<List<BC_ThongKeHoSoQuyenLienQuanMap>> BC_ThongKeHoSoQuyenLienQuan_Dashboard(BC_ThongKeHoSoQuyenLienQuanParam model)
        {
            var response = new ResponseModel();
            var data = _bC_ThongKeRepository.BC_ThongKeHoSoQuyenLienQuan_Dashboard(model, out response);
            return new ResultResponse<List<BC_ThongKeHoSoQuyenLienQuanMap>>(response, data);
        }
    }
}
