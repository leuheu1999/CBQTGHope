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
    public class HS_CapSoService : IHS_CapSoService
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(HS_CapSoService));

        #region Private Repository & contractor       
        private static IHS_CapSoRepository _hs_CapSoRepository;
        private static IDM_LoaiSoRepository _dM_LoaiSoRepository;
        private static ITT_CapQuyenRepository _tT_CapQuyenRepository;
        private static string Conn = null;
        private string _key = "";
        public HS_CapSoService(IHS_CapSoRepository hs_CapSoRepository
            , IDM_LoaiSoRepository dM_LoaiSoRepository
            , ITT_CapQuyenRepository tT_CapQuyenRepository)
        {
            _hs_CapSoRepository = hs_CapSoRepository;
            _dM_LoaiSoRepository = dM_LoaiSoRepository;
            _tT_CapQuyenRepository = tT_CapQuyenRepository;
            var settings = new DataSettingsManager();
            Conn = settings.GetStringConnectChuyenNganh();
        }
        #endregion

        #region TT_CapQuyen
        public ResultResponse<int> TT_CapQuyen_CheckCapSo(long hoSoID)
        {
            var response = new ResponseModel();
            var data = _tT_CapQuyenRepository.TT_CapQuyen_CheckCapSo(hoSoID, out response);
            return new ResultResponse<int>(response, data);
        }
        public ResultResponse<List<TT_CapQuyenMap>> TT_CapQuyen_List(TT_CapQuyenParam model)
        {
            var response = new ResponseModel();
            var data = _tT_CapQuyenRepository.TT_CapQuyen_List(model, out response);
            return new ResultResponse<List<TT_CapQuyenMap>>(response, data);
        }
        public ResultResponse<TT_CapQuyenAdd> TT_CapQuyen_GetByHoSoId(long hoSoID)
        {
            var response = new ResponseModel();
            var data = _tT_CapQuyenRepository.TT_CapQuyen_GetByHoSoId(hoSoID, out response);
            return new ResultResponse<TT_CapQuyenAdd>(response, data);
        }
        public ResultResponse<long> TT_CapQuyen_UpdTinhTrang(TT_CapQuyenAddTrangThai model)
        {
            var response = new ResponseModel();
            using (var conns = new SqlConnection(Conn))
            {
                conns.Open();
                IDbTransaction trans = conns.BeginTransaction();
                var data = _tT_CapQuyenRepository.TT_CapQuyen_UpdTinhTrang(model,conns,trans, out response);
                if (data == null)
                {
                    trans.Rollback();
                    conns.Close();
                    return new ResultResponse<long>(response, -1);
                }
                data.CreatedUserID = model.CreatedUserID;
                if(model.TrangThaiDuyet == 1)
                {
                    var data2 = _tT_CapQuyenRepository.TT_CapQuyen_UpdDuyet(data, conns, trans, out response);
                    if(data2 < 0)
                    {
                        trans.Rollback();
                        conns.Close();
                        return new ResultResponse<long>(response, -1);
                    }
                }
                else
                {
                    var data2 = _tT_CapQuyenRepository.TT_CapQuyen_UpdKhongDuyet(data, conns, trans, out response);
                    if (data2 < 0)
                    {
                        trans.Rollback();
                        conns.Close();
                        return new ResultResponse<long>(response, -1);
                    }
                }
                trans.Commit();
            }
            return new ResultResponse<long>(response, 1);
        }
        public ResultResponse<int> TT_CapQuyen_GetLoaiNghiepVuId(long hoSoID)
        {
            var response = new ResponseModel();
            var data = _tT_CapQuyenRepository.TT_CapQuyen_GetLoaiNghiepVuId(hoSoID, out response);
            return new ResultResponse<int>(response, data);
        }
        #endregion

        #region HS_CapSo
        public ResultResponse<HS_GenSo> HS_CapSo_GenSo(int loaiNghiepVuID)
        {
            var response = new ResponseModel();
            var data = _hs_CapSoRepository.HS_CapSo_GenSo(loaiNghiepVuID, out response);
            return new ResultResponse<HS_GenSo>(response, data);
        }
        public ResultResponse<HS_GenSo> DVC_HS_CapSo_GenSo(int loaiNghiepVuID)
        {
            var response = new ResponseModel();
            var data = _hs_CapSoRepository.DVC_HS_CapSo_GenSo(loaiNghiepVuID, out response);
            return new ResultResponse<HS_GenSo>(response, data);
        }
        #endregion HS_CapSo

        #region HS_HoSo
        public ResultResponse<int> HS_HoSo_CheckCapSo(long hoSoID)
        {
            var response = new ResponseModel();
            var data = _hs_CapSoRepository.HS_HoSo_CheckCapSo(hoSoID, out response);
            return new ResultResponse<int>(response, data);
        }
        #endregion HS_HoSo
    }
}
