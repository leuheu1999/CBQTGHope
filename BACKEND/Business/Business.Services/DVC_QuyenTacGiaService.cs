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
    public class DVC_QuyenTacGiaService : IDVC_QuyenTacGiaService
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(DVC_QuyenTacGiaService));

        #region Private Repository & contractor       
        private static IDVC_QuyenTacGiaRepository _quyenTacGiaRepository;
        private static ITT_CapQuyenRepository _capQuyenRepository;
        private static ITT_HinhAnhRepository _hinhAnhRepository;
        private static ITT_PhimRepository _phimRepository;
        private static string Conn = null;
        private int _keyLinhVuc = 1;
        private string _nameLinhVuc = "Quyền tác giả";
        private int _keyTTDacap = 1;
        private string _nameTTDacap = "Đã cấp";
        private int _keyTTChuaCap = 2;
        private string _nameTTChuaCap = "Chưa cấp";
        private int _keyTTThuHoi = 3;
        private string _nameTTThuHoi = "Thu hồi";
        private int _keyCapMoi = 1;//NV cấp mới
        private int _keyCapLai = 2;//NV cấp lại
        private int _keyThuHoi = 3;//NV thu hồi
        private int _keyDoiChu = 4;//NV đổi chủ
        private int _keyCapDoi = 5;//NV cấp đổi
        public DVC_QuyenTacGiaService(IDVC_QuyenTacGiaRepository quyenTacGiaRepository, ITT_CapQuyenRepository capQuyenRepository, ITT_HinhAnhRepository hinhAnhRepository, ITT_PhimRepository phimRepository)
        {
            _quyenTacGiaRepository = quyenTacGiaRepository;
            _capQuyenRepository = capQuyenRepository;
            _hinhAnhRepository = hinhAnhRepository;
            _phimRepository = phimRepository;
            var settings = new DataSettingsManager();
            Conn = settings.GetStringConnectChuyenNganh();
        }
        #endregion

        #region DVC_QTG_QuyenTacGia
        public ResultResponse<List<DVC_QTG_QuyenTacGiaMap>> DVC_QTG_QuyenTacGia_List(DVC_QTG_QuyenTacGiaParam model)
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.DVC_QTG_QuyenTacGia_List(model, out response);
            if (data != null && data.Count > 0)
            {
                foreach (var item in data)
                {
                    item.ListTacGia = _quyenTacGiaRepository.DVC_QTG_TacGia_ByQuyenTGId(item.QuyenTacGiaID, out response);
                    item.ListChuSoHuu = _quyenTacGiaRepository.DVC_QTG_ChuSoHuu_ByQuyenTGId(item.QuyenTacGiaID, out response);
                }
            }

            return new ResultResponse<List<DVC_QTG_QuyenTacGiaMap>>(response, data);
        }
        public ResultResponse<DVC_QTG_QuyenTacGiaAdd> DVC_QTG_QuyenTacGia_ById(long id)
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.DVC_QTG_QuyenTacGia_ById(id, out response);
            if (data != null)
            {
                data.ListLichSu = _quyenTacGiaRepository.DVC_QTG_LichSu_ById(id, data.StaticID, out response);
            }
            return new ResultResponse<DVC_QTG_QuyenTacGiaAdd>(response, data);
        }
        public ResultResponse<long> DVC_QTG_QuyenTacGia_GetStt()
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.DVC_QTG_QuyenTacGia_GetStt(out response);
            return new ResultResponse<long>(response, data);
        }
        public ResultResponse<long> DVC_QTG_QuyenTacGia_InsUpd(DVC_QTG_QuyenTacGiaAdd model)
        {
            var response = new ResponseModel();
            long quyenTacGiaID = 0;
            using (var conns = new SqlConnection(Conn))
            {
                conns.Open();
                IDbTransaction trans = conns.BeginTransaction();
                model.TrangThaiID = _keyTTChuaCap;
                model.TenTrangThai = _nameTTChuaCap;
                model.IsDauKy = false;
                model.LoaiNghiepVuID = _keyCapMoi;
                quyenTacGiaID = _quyenTacGiaRepository.DVC_QTG_QuyenTacGia_InsUpd(model, conns, trans, out response);
                if (quyenTacGiaID < 0)
                    return new ResultResponse<long>(response, -1);
                if (quyenTacGiaID > 0)
                {
                    if (model.ListHinhAnh != null && model.ListHinhAnh.Count > 0)
                    {
                        int delHinhAnh = _hinhAnhRepository.DVC_TT_HinhAnh_DelByQuyenID(quyenTacGiaID, _keyLinhVuc, conns, trans, out response);
                        if ((int)delHinhAnh < 0)
                        {
                            trans.Rollback();
                            conns.Close();
                            return new ResultResponse<long>(response, -1);
                        }
                        foreach (var item in model.ListHinhAnh)
                        {
                            item.QuyenID = quyenTacGiaID;
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
                        int delPhim = _phimRepository.DVC_TT_Phim_DelByQuyenID(quyenTacGiaID, _keyLinhVuc, conns, trans, out response);
                        if ((int)delPhim < 0)
                        {
                            trans.Rollback();
                            conns.Close();
                            return new ResultResponse<long>(response, -1);
                        }
                        foreach (var item in model.ListPhim)
                        {
                            item.QuyenID = quyenTacGiaID;
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
                    int tacGia = _quyenTacGiaRepository.DVC_QTG_TacGia_Ins(model.ListTacGia, quyenTacGiaID, model.UserID, conns, trans, out response);
                    if ((int)tacGia < 0)
                    {
                        trans.Rollback();
                        conns.Close();
                        return new ResultResponse<long>(response, -1);
                    }
                    int chuSoHuu = _quyenTacGiaRepository.DVC_QTG_ChuSoHuu_Ins(model.ListChuSoHuu, quyenTacGiaID, model.UserID, conns, trans, out response);
                    if ((int)chuSoHuu < 0)
                    {
                        trans.Rollback();
                        conns.Close();
                        return new ResultResponse<long>(response, -1);
                    }
                    int dinhKem = _quyenTacGiaRepository.DVC_QTG_DinhKem_Ins(model.ListDinhKem, quyenTacGiaID, 0, 0, model.UserID, conns, trans, out response);
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
            return new ResultResponse<long>(response, quyenTacGiaID);
        }
        public ResultResponse<int> DVC_QTG_QuyenTacGia_Del(long quyenTacGiaID, Guid userID)
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.DVC_QTG_QuyenTacGia_Del(quyenTacGiaID, userID, out response);
            return new ResultResponse<int>(response, data);
        }
        public ResultResponse<List<DVC_QTG_GiayChungNhanMap>> DVC_QTG_QuyenTacGia_SearchGCN(DVC_QTG_GiayChungNhanParam model)
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.DVC_QTG_QuyenTacGia_SearchGCN(model, out response);
            return new ResultResponse<List<DVC_QTG_GiayChungNhanMap>>(response, data);
        }
        public ResultResponse<long> DVC_QTG_QuyenTacGia_CapSo(DVC_QTG_QuyenTacGia_CapSo model)
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.DVC_QTG_QuyenTacGia_CapSo(model, out response);
            return new ResultResponse<long>(response, data);
        }
        public ResultResponse<int> DVC_QTG_QuyenTacGia_CheckSoTT(long id, string soTT)
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.DVC_QTG_QuyenTacGia_CheckSoTT(id, soTT, out response);
            return new ResultResponse<int>(response, data);
        }
        public ResultResponse<int> DVC_QTG_QuyenTacGia_CheckSoGCN(long id, string soGCN)
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.DVC_QTG_QuyenTacGia_CheckSoGCN(id, soGCN, out response);
            return new ResultResponse<int>(response, data);
        }
        #endregion
    }
}
