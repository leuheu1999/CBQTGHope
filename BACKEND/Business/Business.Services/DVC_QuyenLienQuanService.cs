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
    public class DVC_QuyenLienQuanService : IDVC_QuyenLienQuanService
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(DVC_QuyenLienQuanService));

        #region Private Repository & contractor       
        private static IDVC_QuyenLienQuanRepository _quyenLienQuanRepository;
        private static ITT_CapQuyenRepository _capQuyenRepository;
        private static ITT_HinhAnhRepository _hinhAnhRepository;
        private static ITT_PhimRepository _phimRepository;
        private static string Conn = null;
        private int _keyLinhVuc = 2;
        private string _nameLinhVuc = "Quyền liên quan";
        private int _keyTTDacap = 1;
        private string _nameTTDacap = "Đã cấp";
        private int _keyTTChuaCap = 2;
        private string _nameTTChuaCap = "Chưa cấp";
        private int _keyTTThuHoi = 3;
        private string _nameTTThuHoi = "Thu hồi";
        private int _keyCapMoi = 6;//NV cấp mới
        private int _keyCapLai = 7;//NV cấp lại
        private int _keyThuHoi = 8;//NV thu hồi
        private int _keyDoiChu = 9;//NV đổi chủ
        private int _keyCapDoi = 10;//NV cấp đổi
        public DVC_QuyenLienQuanService(IDVC_QuyenLienQuanRepository quyenLienQuanRepository, ITT_CapQuyenRepository capQuyenRepository, ITT_HinhAnhRepository hinhAnhRepository, ITT_PhimRepository phimRepository)
        {
            _quyenLienQuanRepository = quyenLienQuanRepository;
            _capQuyenRepository = capQuyenRepository;
            _hinhAnhRepository = hinhAnhRepository;
            _phimRepository = phimRepository;
            var settings = new DataSettingsManager();
            Conn = settings.GetStringConnectChuyenNganh();
        }
        #endregion

        #region DVC_QLQ_QuyenLienQuan
        public ResultResponse<List<DVC_QLQ_QuyenLienQuanMap>> DVC_QLQ_QuyenLienQuan_List(DVC_QLQ_QuyenLienQuanParam model)
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.DVC_QLQ_QuyenLienQuan_List(model, out response);
            if (data != null && data.Count > 0)
            {
                foreach (var item in data)
                {
                    item.ListNguoiBieuDien = _quyenLienQuanRepository.DVC_QLQ_NguoiBieuDien_ByQuyenLQId(item.QuyenLienQuanID, out response);
                    item.ListChuSoHuu = _quyenLienQuanRepository.DVC_QLQ_ChuSoHuu_ByQuyenLQId(item.QuyenLienQuanID, out response);
                }
            }

            return new ResultResponse<List<DVC_QLQ_QuyenLienQuanMap>>(response, data);
        }
        public ResultResponse<DVC_QLQ_QuyenLienQuanAdd> DVC_QLQ_QuyenLienQuan_ById(long id)
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.DVC_QLQ_QuyenLienQuan_ById(id, out response);
            if (data != null)
            {
                data.ListLichSu = _quyenLienQuanRepository.DVC_QLQ_LichSu_ById(id, data.StaticID, out response);
            }
            return new ResultResponse<DVC_QLQ_QuyenLienQuanAdd>(response, data);
        }
        public ResultResponse<long> DVC_QLQ_QuyenLienQuan_GetStt()
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.DVC_QLQ_QuyenLienQuan_GetStt(out response);
            return new ResultResponse<long>(response, data);
        }
        public ResultResponse<long> DVC_QLQ_QuyenLienQuan_InsUpd(DVC_QLQ_QuyenLienQuanAdd model)
        {
            var response = new ResponseModel();
            long quyenLienQuanID = 0;
            using (var conns = new SqlConnection(Conn))
            {
                conns.Open();
                IDbTransaction trans = conns.BeginTransaction();
                model.TrangThaiID = _keyTTChuaCap;
                model.TenTrangThai = _nameTTChuaCap;
                model.IsDauKy = false;
                model.LoaiNghiepVuID = _keyCapMoi;
                quyenLienQuanID = _quyenLienQuanRepository.DVC_QLQ_QuyenLienQuan_InsUpd(model, conns, trans, out response);
                if (quyenLienQuanID < 0)
                    return new ResultResponse<long>(response, -1);
                if (quyenLienQuanID > 0)
                {
                    if (model.ListHinhAnh != null && model.ListHinhAnh.Count > 0)
                    {
                        int delHinhAnh = _hinhAnhRepository.DVC_TT_HinhAnh_DelByQuyenID(quyenLienQuanID, _keyLinhVuc, conns, trans, out response);
                        if ((int)delHinhAnh < 0)
                        {
                            trans.Rollback();
                            conns.Close();
                            return new ResultResponse<long>(response, -1);
                        }
                        foreach (var item in model.ListHinhAnh)
                        {
                            item.QuyenID = quyenLienQuanID;
                            item.LinhVucID = _keyLinhVuc;
                            item.UserID = model.UserID;
                            long hinhAnhID = _hinhAnhRepository.DVC_TT_HinhAnh_InsUpd(item, conns, trans, out response);
                            if ((long)hinhAnhID < 0)
                            {
                                trans.Rollback();
                                conns.Close();
                                return new ResultResponse<long>(response, -1);
                            }
                        }
                    }
                    if (model.ListPhim != null && model.ListPhim.Count > 0)
                    {
                        int delPhim = _phimRepository.DVC_TT_Phim_DelByQuyenID(quyenLienQuanID, _keyLinhVuc, conns, trans, out response);
                        if ((int)delPhim < 0)
                        {
                            trans.Rollback();
                            conns.Close();
                            return new ResultResponse<long>(response, -1);
                        }
                        foreach (var item in model.ListPhim)
                        {
                            item.QuyenID = quyenLienQuanID;
                            item.LinhVucID = _keyLinhVuc;
                            item.UserID = model.UserID;
                            long phimID = _phimRepository.DVC_TT_Phim_InsUpd(item, conns, trans, out response);
                            if ((long)phimID < 0)
                            {
                                trans.Rollback();
                                conns.Close();
                                return new ResultResponse<long>(response, -1);
                            }
                        }
                    }
                    int nguoiBieuDien = _quyenLienQuanRepository.DVC_QLQ_NguoiBieuDien_Ins(model.ListNguoiBieuDien, quyenLienQuanID, model.UserID, conns, trans, out response);
                    if ((int)nguoiBieuDien < 0)
                    {
                        trans.Rollback();
                        conns.Close();
                        return new ResultResponse<long>(response, -1);
                    }
                    int chuSoHuu = _quyenLienQuanRepository.DVC_QLQ_ChuSoHuu_Ins(model.ListChuSoHuu, quyenLienQuanID, model.UserID, conns, trans, out response);
                    if ((int)chuSoHuu < 0)
                    {
                        trans.Rollback();
                        conns.Close();
                        return new ResultResponse<long>(response, -1);
                    }
                    int dinhKem = _quyenLienQuanRepository.DVC_QLQ_DinhKem_Ins(model.ListDinhKem, quyenLienQuanID, 0, 0, model.UserID, conns, trans, out response);
                    if ((int)dinhKem < 0)
                    {
                        trans.Rollback();
                        conns.Close();
                        return new ResultResponse<long>(response, -1);
                    }
                }
                trans.Commit();
                conns.Close();
            }
            return new ResultResponse<long>(response, quyenLienQuanID);
        }
        public ResultResponse<int> DVC_QLQ_QuyenLienQuan_Del(long quyenLienQuanID, Guid userID)
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.DVC_QLQ_QuyenLienQuan_Del(quyenLienQuanID, userID, out response);
            return new ResultResponse<int>(response, data);
        }
        public ResultResponse<List<DVC_QLQ_GiayChungNhanMap>> DVC_QLQ_QuyenLienQuan_SearchGCN(DVC_QLQ_GiayChungNhanParam model)
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.DVC_QLQ_QuyenLienQuan_SearchGCN(model, out response);
            return new ResultResponse<List<DVC_QLQ_GiayChungNhanMap>>(response, data);
        }
        public ResultResponse<long> DVC_QLQ_QuyenLienQuan_CapSo(DVC_QLQ_QuyenLienQuan_CapSo model)
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.DVC_QLQ_QuyenLienQuan_CapSo(model, out response);
            return new ResultResponse<long>(response, data);
        }
        public ResultResponse<int> DVC_QLQ_QuyenLienQuan_CheckSoTT(long id, string soTT)
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.DVC_QLQ_QuyenLienQuan_CheckSoTT(id, soTT, out response);
            return new ResultResponse<int>(response, data);
        }
        public ResultResponse<int> DVC_QLQ_QuyenLienQuan_CheckSoGCN(long id, string soGCN)
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.DVC_QLQ_QuyenLienQuan_CheckSoGCN(id, soGCN, out response);
            return new ResultResponse<int>(response, data);
        }
        #endregion
    }
}