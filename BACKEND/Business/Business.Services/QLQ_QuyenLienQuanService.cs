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
    public class QLQ_QuyenLienQuanService : IQLQ_QuyenLienQuanService
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(QLQ_QuyenLienQuanService));

        #region Private Repository & contractor       
        private static IQLQ_QuyenLienQuanRepository _quyenLienQuanRepository;
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
        public QLQ_QuyenLienQuanService(IQLQ_QuyenLienQuanRepository quyenLienQuanRepository, ITT_CapQuyenRepository capQuyenRepository, ITT_HinhAnhRepository hinhAnhRepository, ITT_PhimRepository phimRepository)
        {
            _quyenLienQuanRepository = quyenLienQuanRepository;
            _capQuyenRepository = capQuyenRepository;
            _hinhAnhRepository = hinhAnhRepository;
            _phimRepository = phimRepository;
            var settings = new DataSettingsManager();
            Conn = settings.GetStringConnectChuyenNganh();
        }
        #endregion

        #region QLQ_QuyenLienQuan
        public ResultResponse<List<QLQ_QuyenLienQuanMap>> QLQ_QuyenLienQuan_List(QLQ_QuyenLienQuanParam model)
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.QLQ_QuyenLienQuan_List(model, out response);
            if (data != null && data.Count > 0)
            {
                foreach (var item in data)
                {
                    item.ListNguoiBieuDien = _quyenLienQuanRepository.QLQ_NguoiBieuDien_ByQuyenLQId(item.QuyenLienQuanID, out response);
                    item.ListChuSoHuu = _quyenLienQuanRepository.QLQ_ChuSoHuu_ByQuyenLQId(item.QuyenLienQuanID, out response);
                }
            }

            return new ResultResponse<List<QLQ_QuyenLienQuanMap>>(response, data);
        }
        public ResultResponse<QLQ_QuyenLienQuanAdd> QLQ_QuyenLienQuan_ById(long id)
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.QLQ_QuyenLienQuan_ById(id, out response);
            if (data != null)
            {
                data.ListLichSu = _quyenLienQuanRepository.QLQ_LichSu_ById(id, data.StaticID, out response);
            }
            return new ResultResponse<QLQ_QuyenLienQuanAdd>(response, data);
        }
        public ResultResponse<long> QLQ_QuyenLienQuan_GetStt()
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.QLQ_QuyenLienQuan_GetStt(out response);
            return new ResultResponse<long>(response, data);
        }
        public ResultResponse<long> QLQ_QuyenLienQuan_InsUpd(QLQ_QuyenLienQuanAdd model)
        {
            var response = new ResponseModel();
            long quyenLienQuanID = 0;
            using (var conns = new SqlConnection(Conn))
            {
                conns.Open();
                IDbTransaction trans = conns.BeginTransaction();
                if (model.HoSoID > 0)
                {
                    model.TrangThaiID = _keyTTChuaCap;
                    model.TenTrangThai = _nameTTChuaCap;
                    model.IsDauKy = false;
                }
                else
                {
                    model.TrangThaiID = _keyTTDacap;
                    model.TenTrangThai = _nameTTDacap;
                    model.IsDauKy = true;
                }
                model.LoaiNghiepVuID = _keyCapMoi;
                quyenLienQuanID = _quyenLienQuanRepository.QLQ_QuyenLienQuan_InsUpd(model, conns, trans, out response);
                if (quyenLienQuanID < 0)
                    return new ResultResponse<long>(response, -1);
                if (quyenLienQuanID > 0)
                {
                    if (model.ListHinhAnh != null && model.ListHinhAnh.Count > 0)
                    {
                        int delHinhAnh = _hinhAnhRepository.TT_HinhAnh_DelByQuyenID(quyenLienQuanID, _keyLinhVuc, conns, trans, out response);
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
                            long hinhAnhID = _hinhAnhRepository.TT_HinhAnh_InsUpd(item, conns, trans, out response);
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
                        int delPhim = _phimRepository.TT_Phim_DelByQuyenID(quyenLienQuanID, _keyLinhVuc, conns, trans, out response);
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
                            long phimID = _phimRepository.TT_Phim_InsUpd(item, conns, trans, out response);
                            if ((long)phimID < 0)
                            {
                                trans.Rollback();
                                conns.Close();
                                return new ResultResponse<long>(response, -1);
                            }
                        }
                    }
                    int nguoiBieuDien = _quyenLienQuanRepository.QLQ_NguoiBieuDien_Ins(model.ListNguoiBieuDien, quyenLienQuanID, model.UserID, conns, trans, out response);
                    if ((int)nguoiBieuDien < 0)
                    {
                        trans.Rollback();
                        conns.Close();
                        return new ResultResponse<long>(response, -1);
                    }
                    int chuSoHuu = _quyenLienQuanRepository.QLQ_ChuSoHuu_Ins(model.ListChuSoHuu, quyenLienQuanID, model.UserID, conns, trans, out response);
                    if ((int)chuSoHuu < 0)
                    {
                        trans.Rollback();
                        conns.Close();
                        return new ResultResponse<long>(response, -1);
                    }
                    int dinhKem = _quyenLienQuanRepository.QLQ_DinhKem_Ins(model.ListDinhKem, quyenLienQuanID, 0, 0, model.UserID, conns, trans, out response);
                    if ((int)dinhKem < 0)
                    {
                        trans.Rollback();
                        conns.Close();
                        return new ResultResponse<long>(response, -1);
                    }
                    if (model.HoSoID > 0)
                    {
                        TT_CapQuyenAdd modelCapQuyen = new TT_CapQuyenAdd();
                        modelCapQuyen.HoSoID = model.HoSoID;
                        modelCapQuyen.ThuTucID = model.ThuTucID;
                        modelCapQuyen.TenThuTuc = model.TenThuTuc;
                        modelCapQuyen.MaBoHoSo = model.MaBoHoSo;
                        modelCapQuyen.SoBienNhan = model.SoBienNhan;
                        modelCapQuyen.LoaiNghiepVuID = _keyCapMoi;
                        modelCapQuyen.KeyMapID = quyenLienQuanID;
                        modelCapQuyen.LinhVucID = _keyLinhVuc;
                        modelCapQuyen.TenLinhVuc = _nameLinhVuc;
                        modelCapQuyen.UserID = model.UserID;
                        modelCapQuyen.NguoiKyID = model.NguoiKyID;
                        long capQuyen = _capQuyenRepository.TT_CapQuyen_InsUpd(modelCapQuyen, conns, trans, out response);
                        if ((long)capQuyen < 0)
                        {
                            trans.Rollback();
                            conns.Close();
                            return new ResultResponse<long>(response, -1);
                        }
                    }
                }
                trans.Commit();
                conns.Close();
            }
            return new ResultResponse<long>(response, quyenLienQuanID);
        }
        public ResultResponse<int> QLQ_QuyenLienQuan_Del(long quyenLienQuanID, Guid userID)
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.QLQ_QuyenLienQuan_Del(quyenLienQuanID, userID, out response);
            return new ResultResponse<int>(response, data);
        }
        public ResultResponse<List<QLQ_GiayChungNhanMap>> QLQ_QuyenLienQuan_SearchGCN(QLQ_GiayChungNhanParam model)
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.QLQ_QuyenLienQuan_SearchGCN(model, out response);
            return new ResultResponse<List<QLQ_GiayChungNhanMap>>(response, data);
        }
        public ResultResponse<List<QLQ_SoThuTuMap>> QLQ_QuyenLienQuan_SearchSTT(QLQ_SoThuTuParam model)
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.QLQ_QuyenLienQuan_SearchSTT(model, out response);
            return new ResultResponse<List<QLQ_SoThuTuMap>>(response, data);
        }       
        public ResultResponse<long> QLQ_QuyenLienQuan_CapSo(QLQ_QuyenLienQuan_CapSo model)
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.QLQ_QuyenLienQuan_CapSo(model, out response);
            return new ResultResponse<long>(response, data);
        }
        public ResultResponse<int> QLQ_QuyenLienQuan_CheckSoTT(long id, string soTT)
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.QLQ_QuyenLienQuan_CheckSoTT(id, soTT, out response);
            return new ResultResponse<int>(response, data);
        }
        public ResultResponse<int> QLQ_QuyenLienQuan_CheckSoGCN(long id, string soGCN)
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.QLQ_QuyenLienQuan_CheckSoGCN(id, soGCN, out response);
            return new ResultResponse<int>(response, data);
        }
        public ResultResponse<int> QLQ_QuyenLienQuan_Duyet(long id, int loaiNghiepVuID, bool isDuyet)
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.QLQ_QuyenLienQuan_Duyet(id, loaiNghiepVuID, isDuyet, out response);
            return new ResultResponse<int>(response, data);
        }
        #endregion

        #region QLQ_CapLai
        public ResultResponse<QLQ_CapLaiAdd> QLQ_CapLai_ById(long id)
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.QLQ_CapLai_ById(id, out response);
            return new ResultResponse<QLQ_CapLaiAdd>(response, data);
        }
        public ResultResponse<QLQ_CapLaiAdd> QLQ_CapLai_ByHoSoId(long id)
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.QLQ_CapLai_ByHoSoId(id, out response);
            return new ResultResponse<QLQ_CapLaiAdd>(response, data);
        }
        public ResultResponse<QLQ_CapLaiAdd> QLQ_CapLai_ByQuyenLQId(long id)
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.QLQ_CapLai_ByQuyenLQId(id, out response);
            return new ResultResponse<QLQ_CapLaiAdd>(response, data);
        }
        public ResultResponse<long> QLQ_CapLai_InsUpd(QLQ_CapLaiAdd model)
        {
            var response = new ResponseModel();
            long capLaiID = 0;
            using (var conns = new SqlConnection(Conn))
            {
                conns.Open();
                IDbTransaction trans = conns.BeginTransaction();
                var quyenID = _quyenLienQuanRepository.QLQ_QuyenLienQuan_OldToNew(model.QuyenLienQuanCuID, _keyCapLai, model.CapLaiID, conns, trans, out response);
                if (quyenID < 0)
                    return new ResultResponse<long>(response, -1);
                if (model.CapLaiID == 0)
                {
                    var hinhAnhParam = new TT_HinhAnhParam();
                    hinhAnhParam.QuyenID = model.QuyenLienQuanCuID;
                    hinhAnhParam.LinhVucID = _keyLinhVuc;
                    var lstHinhAnh = _hinhAnhRepository.TT_HinhAnh_List(hinhAnhParam, out response);
                    if (lstHinhAnh != null && lstHinhAnh.Count > 0)
                    {
                        foreach (var item in lstHinhAnh)
                        {
                            var hinhAnhAdd = new TT_HinhAnhAdd();
                            hinhAnhAdd.QuyenID = quyenID;
                            hinhAnhAdd.LinhVucID = _keyLinhVuc;
                            hinhAnhAdd.UserID = model.UserID;
                            hinhAnhAdd.Ten = item.Ten;
                            hinhAnhAdd.TenTepTin = item.TenTepTin;
                            hinhAnhAdd.TenGoc = item.TenGoc;
                            hinhAnhAdd.Tag = item.Tag;
                            hinhAnhAdd.DuongDan = item.DuongDan;
                            hinhAnhAdd.GhiChu = item.GhiChu;
                            hinhAnhAdd.CreatedUser = item.CreatedUser;
                            hinhAnhAdd.CreatedDate = item.CreatedDate;
                            long hinhAnhID = _hinhAnhRepository.TT_HinhAnh_InsUpd(hinhAnhAdd, conns, trans, out response);
                            if ((long)hinhAnhID < 0)
                            {
                                trans.Rollback();
                                conns.Close();
                                return new ResultResponse<long>(response, -1);
                            }
                        }
                    }
                    var phimParam = new TT_PhimParam();
                    phimParam.QuyenID = model.QuyenLienQuanCuID;
                    phimParam.LinhVucID = _keyLinhVuc;
                    var lstPhim = _phimRepository.TT_Phim_List(phimParam, out response);
                    if (lstPhim != null && lstPhim.Count > 0)
                    {
                        foreach (var item in lstPhim)
                        {
                            var phimAdd = new TT_PhimAdd();
                            phimAdd.QuyenID = quyenID;
                            phimAdd.LinhVucID = _keyLinhVuc;
                            phimAdd.UserID = model.UserID;
                            phimAdd.Ten = item.Ten;
                            phimAdd.TenTepTin = item.TenTepTin;
                            phimAdd.TenGoc = item.TenGoc;
                            phimAdd.Tag = item.Tag;
                            phimAdd.DuongDan = item.DuongDan;
                            phimAdd.GhiChu = item.GhiChu;
                            phimAdd.CreatedUser = item.CreatedUser;
                            phimAdd.CreatedDate = item.CreatedDate;
                            long phimID = _phimRepository.TT_Phim_InsUpd(phimAdd, conns, trans, out response);
                            if ((long)phimID < 0)
                            {
                                trans.Rollback();
                                conns.Close();
                                return new ResultResponse<long>(response, -1);
                            }
                        }
                    }
                }
                model.QuyenLienQuanID = quyenID;
                capLaiID = _quyenLienQuanRepository.QLQ_CapLai_InsUpd(model, conns, trans, out response);
                if (capLaiID < 0)
                {
                    trans.Rollback();
                    conns.Close();
                    return new ResultResponse<long>(response, -1);
                }
                if (capLaiID > 0)
                {
                    var modelChange = new QLQ_QuyenLienQuanChange();
                    modelChange.QuyenLienQuanID = quyenID;
                    modelChange.STT = model.STT;
                    modelChange.SoGCN = model.SoGCN;
                    modelChange.NgayCap = model.NgayCapGCN;
                    modelChange.LoaiNghiepVuID = _keyCapLai;
                    modelChange.KeyMapID = capLaiID;
                    if (model.HoSoID > 0)
                    {
                        modelChange.TrangThaiID = _keyTTChuaCap;
                        modelChange.TenTrangThai = _nameTTChuaCap;
                    }
                    else
                    {
                        modelChange.TrangThaiID = _keyTTDacap;
                        modelChange.TenTrangThai = _nameTTDacap;
                    }
                    long change = _quyenLienQuanRepository.QLQ_QuyenLienQuan_Change(modelChange, conns, trans, out response);
                    if ((long)change < 0)
                    {
                        trans.Rollback();
                        conns.Close();
                        return new ResultResponse<long>(response, -1);
                    }
                    int dinhKem = _quyenLienQuanRepository.QLQ_DinhKem_Ins(model.ListDinhKem, quyenID, _keyCapLai, capLaiID, model.UserID, conns, trans, out response);
                    if ((int)dinhKem < 0)
                    {
                        trans.Rollback();
                        conns.Close();
                        return new ResultResponse<long>(response, -1);
                    }
                    if (model.HoSoID > 0)
                    {
                        TT_CapQuyenAdd modelCapQuyen = new TT_CapQuyenAdd();
                        modelCapQuyen.HoSoID = model.HoSoID;
                        modelCapQuyen.ThuTucID = model.ThuTucID;
                        modelCapQuyen.TenThuTuc = model.TenThuTuc;
                        modelCapQuyen.MaBoHoSo = model.MaBoHoSo;
                        modelCapQuyen.SoBienNhan = model.SoBienNhan;
                        modelCapQuyen.LoaiNghiepVuID = _keyCapLai;
                        modelCapQuyen.KeyMapID = capLaiID;
                        modelCapQuyen.LinhVucID = _keyLinhVuc;
                        modelCapQuyen.TenLinhVuc = _nameLinhVuc;
                        modelCapQuyen.NguoiKyID = model.NguoiKyID;
                        modelCapQuyen.UserID = model.UserID;
                        long capQuyen = _capQuyenRepository.TT_CapQuyen_InsUpd(modelCapQuyen, conns, trans, out response);
                        if ((long)capQuyen < 0)
                        {
                            trans.Rollback();
                            conns.Close();
                            return new ResultResponse<long>(response, -1);
                        }
                    }
                }
                trans.Commit();
                conns.Close();
            }
            return new ResultResponse<long>(response, capLaiID);
        }
        #endregion QLQ_CapLai

        #region QLQ_ThuHoi
        public ResultResponse<QLQ_ThuHoiAdd> QLQ_ThuHoi_ById(long id)
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.QLQ_ThuHoi_ById(id, out response);
            return new ResultResponse<QLQ_ThuHoiAdd>(response, data);
        }
        public ResultResponse<QLQ_ThuHoiAdd> QLQ_ThuHoi_ByHoSoId(long id)
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.QLQ_ThuHoi_ByHoSoId(id, out response);
            return new ResultResponse<QLQ_ThuHoiAdd>(response, data);
        }
        public ResultResponse<QLQ_ThuHoiAdd> QLQ_ThuHoi_ByQuyenLQId(long id)
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.QLQ_ThuHoi_ByQuyenLQId(id, out response);
            return new ResultResponse<QLQ_ThuHoiAdd>(response, data);
        }
        public ResultResponse<long> QLQ_ThuHoi_InsUpd(QLQ_ThuHoiAdd model)
        {
            var response = new ResponseModel();
            long thuHoiID = 0;
            using (var conns = new SqlConnection(Conn))
            {
                conns.Open();
                IDbTransaction trans = conns.BeginTransaction();
                thuHoiID = _quyenLienQuanRepository.QLQ_ThuHoi_InsUpd(model, conns, trans, out response);
                if (thuHoiID < 0)
                    return new ResultResponse<long>(response, -1);
                if (thuHoiID > 0)
                {
                    var modelChange = new QLQ_QuyenLienQuanChange();
                    modelChange.QuyenLienQuanID = model.QuyenLienQuanID;
                    modelChange.LoaiNghiepVuID = _keyThuHoi;
                    modelChange.KeyMapID = thuHoiID;
                    if (model.HoSoID > 0)
                    {
                        modelChange.TrangThaiID = _keyTTChuaCap;
                        modelChange.TenTrangThai = _nameTTChuaCap;
                    }
                    else
                    {
                        modelChange.TrangThaiID = _keyTTThuHoi;
                        modelChange.TenTrangThai = _nameTTThuHoi;
                    }
                    long change = _quyenLienQuanRepository.QLQ_QuyenLienQuan_Change(modelChange, conns, trans, out response);
                    if ((long)change < 0)
                    {
                        trans.Rollback();
                        conns.Close();
                        return new ResultResponse<long>(response, -1);
                    }
                    int dinhKem = _quyenLienQuanRepository.QLQ_DinhKem_Ins(model.ListDinhKem, model.QuyenLienQuanID, _keyThuHoi, thuHoiID, model.UserID, conns, trans, out response);
                    if ((int)dinhKem < 0)
                    {
                        trans.Rollback();
                        conns.Close();
                        return new ResultResponse<long>(response, -1);
                    }
                    if (model.HoSoID > 0)
                    {
                        TT_CapQuyenAdd modelCapQuyen = new TT_CapQuyenAdd();
                        modelCapQuyen.HoSoID = model.HoSoID;
                        modelCapQuyen.ThuTucID = model.ThuTucID;
                        modelCapQuyen.TenThuTuc = model.TenThuTuc;
                        modelCapQuyen.MaBoHoSo = model.MaBoHoSo;
                        modelCapQuyen.SoBienNhan = model.SoBienNhan;
                        modelCapQuyen.LoaiNghiepVuID = _keyThuHoi;
                        modelCapQuyen.KeyMapID = thuHoiID;
                        modelCapQuyen.LinhVucID = _keyLinhVuc;
                        modelCapQuyen.TenLinhVuc = _nameLinhVuc;
                        modelCapQuyen.NguoiKyID = model.NguoiKyID;
                        modelCapQuyen.UserID = model.UserID;
                        long capQuyen = _capQuyenRepository.TT_CapQuyen_InsUpd(modelCapQuyen, conns, trans, out response);
                        if ((long)capQuyen < 0)
                        {
                            trans.Rollback();
                            conns.Close();
                            return new ResultResponse<long>(response, -1);
                        }
                    }
                }
                trans.Commit();
                conns.Close();
            }
            return new ResultResponse<long>(response, thuHoiID);
        }
        #endregion QLQ_ThuHoi

        #region QLQ_ChuyenChuSoHuu
        public ResultResponse<QLQ_ChuyenChuSoHuuAdd> QLQ_ChuyenChuSoHuu_ById(long id)
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.QLQ_ChuyenChuSoHuu_ById(id, out response);
            return new ResultResponse<QLQ_ChuyenChuSoHuuAdd>(response, data);
        }
        public ResultResponse<QLQ_ChuyenChuSoHuuAdd> QLQ_ChuyenChuSoHuu_ByHoSoId(long id)
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.QLQ_ChuyenChuSoHuu_ByHoSoId(id, out response);
            return new ResultResponse<QLQ_ChuyenChuSoHuuAdd>(response, data);
        }
        public ResultResponse<QLQ_ChuyenChuSoHuuAdd> QLQ_ChuyenChuSoHuu_ByQuyenLQId(long id)
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.QLQ_ChuyenChuSoHuu_ByQuyenLQId(id, out response);
            return new ResultResponse<QLQ_ChuyenChuSoHuuAdd>(response, data);
        }
        public ResultResponse<long> QLQ_ChuyenChuSoHuu_InsUpd(QLQ_ChuyenChuSoHuuAdd model)
        {
            var response = new ResponseModel();
            long chuyenChuID = 0;
            using (var conns = new SqlConnection(Conn))
            {
                conns.Open();
                IDbTransaction trans = conns.BeginTransaction();
                var quyenID = _quyenLienQuanRepository.QLQ_QuyenLienQuan_OldToNew(model.QuyenLienQuanCuID, _keyDoiChu, model.ChuyenChuSoHuuID, conns, trans, out response);
                if (quyenID < 0)
                    return new ResultResponse<long>(response, -1);
                if (model.ChuyenChuSoHuuID == 0)
                {
                    var hinhAnhParam = new TT_HinhAnhParam();
                    hinhAnhParam.QuyenID = model.QuyenLienQuanCuID;
                    hinhAnhParam.LinhVucID = _keyLinhVuc;
                    var lstHinhAnh = _hinhAnhRepository.TT_HinhAnh_List(hinhAnhParam, out response);
                    if (lstHinhAnh != null && lstHinhAnh.Count > 0)
                    {
                        foreach (var item in lstHinhAnh)
                        {
                            var hinhAnhAdd = new TT_HinhAnhAdd();
                            hinhAnhAdd.QuyenID = quyenID;
                            hinhAnhAdd.LinhVucID = _keyLinhVuc;
                            hinhAnhAdd.UserID = model.UserID;
                            hinhAnhAdd.Ten = item.Ten;
                            hinhAnhAdd.TenTepTin = item.TenTepTin;
                            hinhAnhAdd.TenGoc = item.TenGoc;
                            hinhAnhAdd.Tag = item.Tag;
                            hinhAnhAdd.DuongDan = item.DuongDan;
                            hinhAnhAdd.GhiChu = item.GhiChu;
                            hinhAnhAdd.CreatedUser = item.CreatedUser;
                            hinhAnhAdd.CreatedDate = item.CreatedDate;
                            long hinhAnhID = _hinhAnhRepository.TT_HinhAnh_InsUpd(hinhAnhAdd, conns, trans, out response);
                            if ((long)hinhAnhID < 0)
                            {
                                trans.Rollback();
                                conns.Close();
                                return new ResultResponse<long>(response, -1);
                            }
                        }
                    }
                    var phimParam = new TT_PhimParam();
                    phimParam.QuyenID = model.QuyenLienQuanCuID;
                    phimParam.LinhVucID = _keyLinhVuc;
                    var lstPhim = _phimRepository.TT_Phim_List(phimParam, out response);
                    if (lstPhim != null && lstPhim.Count > 0)
                    {
                        foreach (var item in lstPhim)
                        {
                            var phimAdd = new TT_PhimAdd();
                            phimAdd.QuyenID = quyenID;
                            phimAdd.LinhVucID = _keyLinhVuc;
                            phimAdd.UserID = model.UserID;
                            phimAdd.Ten = item.Ten;
                            phimAdd.TenTepTin = item.TenTepTin;
                            phimAdd.TenGoc = item.TenGoc;
                            phimAdd.Tag = item.Tag;
                            phimAdd.DuongDan = item.DuongDan;
                            phimAdd.GhiChu = item.GhiChu;
                            phimAdd.CreatedUser = item.CreatedUser;
                            phimAdd.CreatedDate = item.CreatedDate;
                            long phimID = _phimRepository.TT_Phim_InsUpd(phimAdd, conns, trans, out response);
                            if ((long)phimID < 0)
                            {
                                trans.Rollback();
                                conns.Close();
                                return new ResultResponse<long>(response, -1);
                            }
                        }
                    }
                }
                model.QuyenLienQuanID = quyenID;
                chuyenChuID = _quyenLienQuanRepository.QLQ_ChuyenChuSoHuu_InsUpd(model, conns, trans, out response);
                if (chuyenChuID < 0)
                {
                    trans.Rollback();
                    conns.Close();
                    return new ResultResponse<long>(response, -1);
                }
                if (chuyenChuID > 0)
                {
                    var modelChange = new QLQ_QuyenLienQuanChange();
                    modelChange.QuyenLienQuanID = quyenID;
                    modelChange.STT = model.STT;
                    modelChange.SoGCN = model.SoGCN;
                    modelChange.NgayCap = model.NgayCapGCN;
                    modelChange.LoaiNghiepVuID = _keyDoiChu;
                    modelChange.KeyMapID = chuyenChuID;
                    if (model.HoSoID > 0)
                    {
                        modelChange.TrangThaiID = _keyTTChuaCap;
                        modelChange.TenTrangThai = _nameTTChuaCap;
                    }
                    else
                    {
                        modelChange.TrangThaiID = _keyTTDacap;
                        modelChange.TenTrangThai = _nameTTDacap;
                    }
                    long change = _quyenLienQuanRepository.QLQ_QuyenLienQuan_Change(modelChange, conns, trans, out response);
                    if ((long)change < 0)
                    {
                        trans.Rollback();
                        conns.Close();
                        return new ResultResponse<long>(response, -1);
                    }
                    int chuSoHuu = _quyenLienQuanRepository.QLQ_ChuSoHuu_Ins(model.ListChuSoHuu, quyenID, model.UserID, conns, trans, out response);
                    if ((int)chuSoHuu < 0)
                    {
                        trans.Rollback();
                        conns.Close();
                        return new ResultResponse<long>(response, -1);
                    }
                    int dinhKem = _quyenLienQuanRepository.QLQ_DinhKem_Ins(model.ListDinhKem, quyenID, _keyDoiChu, chuyenChuID, model.UserID, conns, trans, out response);
                    if ((int)dinhKem < 0)
                    {
                        trans.Rollback();
                        conns.Close();
                        return new ResultResponse<long>(response, -1);
                    }
                    if (model.HoSoID > 0)
                    {
                        TT_CapQuyenAdd modelCapQuyen = new TT_CapQuyenAdd();
                        modelCapQuyen.HoSoID = model.HoSoID;
                        modelCapQuyen.ThuTucID = model.ThuTucID;
                        modelCapQuyen.TenThuTuc = model.TenThuTuc;
                        modelCapQuyen.MaBoHoSo = model.MaBoHoSo;
                        modelCapQuyen.SoBienNhan = model.SoBienNhan;
                        modelCapQuyen.LoaiNghiepVuID = _keyDoiChu;
                        modelCapQuyen.KeyMapID = chuyenChuID;
                        modelCapQuyen.LinhVucID = _keyLinhVuc;
                        modelCapQuyen.TenLinhVuc = _nameLinhVuc;
                        modelCapQuyen.NguoiKyID = model.NguoiKyID;
                        modelCapQuyen.UserID = model.UserID;
                        long capQuyen = _capQuyenRepository.TT_CapQuyen_InsUpd(modelCapQuyen, conns, trans, out response);
                        if ((long)capQuyen < 0)
                        {
                            trans.Rollback();
                            conns.Close();
                            return new ResultResponse<long>(response, -1);
                        }
                    }
                }
                trans.Commit();
                conns.Close();
            }
            return new ResultResponse<long>(response, chuyenChuID);
        }
        #endregion QLQ_ChuyenChuSoHuu

        #region QLQ_CapDoi
        public ResultResponse<QLQ_CapDoiAdd> QLQ_CapDoi_ById(long id)
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.QLQ_CapDoi_ById(id, out response);
            return new ResultResponse<QLQ_CapDoiAdd>(response, data);
        }
        public ResultResponse<QLQ_CapDoiAdd> QLQ_CapDoi_ByHoSoId(long id)
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.QLQ_CapDoi_ByHoSoId(id, out response);
            return new ResultResponse<QLQ_CapDoiAdd>(response, data);
        }
        public ResultResponse<QLQ_CapDoiAdd> QLQ_CapDoi_ByQuyenLQId(long id)
        {
            var response = new ResponseModel();
            var data = _quyenLienQuanRepository.QLQ_CapDoi_ByQuyenLQId(id, out response);
            return new ResultResponse<QLQ_CapDoiAdd>(response, data);
        }
        public ResultResponse<long> QLQ_CapDoi_InsUpd(QLQ_CapDoiAdd model)
        {
            var response = new ResponseModel();
            long capDoiID = 0;
            using (var conns = new SqlConnection(Conn))
            {
                conns.Open();
                IDbTransaction trans = conns.BeginTransaction();
                var quyenID = _quyenLienQuanRepository.QLQ_QuyenLienQuan_OldToNew(model.QuyenLienQuanCuID, _keyCapDoi, model.CapDoiID, conns, trans, out response);
                if (quyenID < 0)
                    return new ResultResponse<long>(response, -1);
                if (model.CapDoiID == 0)
                {
                    var hinhAnhParam = new TT_HinhAnhParam();
                    hinhAnhParam.QuyenID = model.QuyenLienQuanCuID;
                    hinhAnhParam.LinhVucID = _keyLinhVuc;
                    var lstHinhAnh = _hinhAnhRepository.TT_HinhAnh_List(hinhAnhParam, out response);
                    if (lstHinhAnh != null && lstHinhAnh.Count > 0)
                    {
                        foreach (var item in lstHinhAnh)
                        {
                            var hinhAnhAdd = new TT_HinhAnhAdd();
                            hinhAnhAdd.QuyenID = quyenID;
                            hinhAnhAdd.LinhVucID = _keyLinhVuc;
                            hinhAnhAdd.UserID = model.UserID;
                            hinhAnhAdd.Ten = item.Ten;
                            hinhAnhAdd.TenTepTin = item.TenTepTin;
                            hinhAnhAdd.TenGoc = item.TenGoc;
                            hinhAnhAdd.Tag = item.Tag;
                            hinhAnhAdd.DuongDan = item.DuongDan;
                            hinhAnhAdd.GhiChu = item.GhiChu;
                            hinhAnhAdd.CreatedUser = item.CreatedUser;
                            hinhAnhAdd.CreatedDate = item.CreatedDate;
                            long hinhAnhID = _hinhAnhRepository.TT_HinhAnh_InsUpd(hinhAnhAdd, conns, trans, out response);
                            if ((long)hinhAnhID < 0)
                            {
                                trans.Rollback();
                                conns.Close();
                                return new ResultResponse<long>(response, -1);
                            }
                        }
                    }
                    var phimParam = new TT_PhimParam();
                    phimParam.QuyenID = model.QuyenLienQuanCuID;
                    phimParam.LinhVucID = _keyLinhVuc;
                    var lstPhim = _phimRepository.TT_Phim_List(phimParam, out response);
                    if (lstPhim != null && lstPhim.Count > 0)
                    {
                        foreach (var item in lstPhim)
                        {
                            var phimAdd = new TT_PhimAdd();
                            phimAdd.QuyenID = quyenID;
                            phimAdd.LinhVucID = _keyLinhVuc;
                            phimAdd.UserID = model.UserID;
                            phimAdd.Ten = item.Ten;
                            phimAdd.TenTepTin = item.TenTepTin;
                            phimAdd.TenGoc = item.TenGoc;
                            phimAdd.Tag = item.Tag;
                            phimAdd.DuongDan = item.DuongDan;
                            phimAdd.GhiChu = item.GhiChu;
                            phimAdd.CreatedUser = item.CreatedUser;
                            phimAdd.CreatedDate = item.CreatedDate;
                            long phimID = _phimRepository.TT_Phim_InsUpd(phimAdd, conns, trans, out response);
                            if ((long)phimID < 0)
                            {
                                trans.Rollback();
                                conns.Close();
                                return new ResultResponse<long>(response, -1);
                            }
                        }
                    }
                }
                model.QuyenLienQuanID = quyenID;
                capDoiID = _quyenLienQuanRepository.QLQ_CapDoi_InsUpd(model, conns, trans, out response);
                if (capDoiID < 0)
                {
                    trans.Rollback();
                    conns.Close();
                    return new ResultResponse<long>(response, -1);
                }
                if (capDoiID > 0)
                {
                    var modelChange = new QLQ_QuyenLienQuanAdd();
                    modelChange.QuyenLienQuanID = quyenID;
                    modelChange.STT = model.STT;
                    modelChange.SoGCN = model.SoGCN;
                    modelChange.NgayCap = model.NgayCapGCN;
                    modelChange.LoaiNghiepVuID = _keyCapDoi;
                    modelChange.KeyMapID = capDoiID;
                    if (model.HoSoID > 0)
                    {
                        modelChange.TrangThaiID = _keyTTChuaCap;
                        modelChange.TenTrangThai = _nameTTChuaCap;
                    }
                    else
                    {
                        modelChange.TrangThaiID = _keyTTDacap;
                        modelChange.TenTrangThai = _nameTTDacap;
                    }
                    modelChange.VungMienID = model.VungMienID;
                    modelChange.CoNguoiDaiDien = model.CoNguoiDaiDien;
                    modelChange.NDDHoVaTen = model.NDDHoVaTen;
                    modelChange.NDDSoCMND = model.NDDSoCMND;
                    modelChange.NDDSoDienThoai = model.NDDSoDienThoai;
                    modelChange.NDDDiaChi = model.NDDDiaChi;
                    modelChange.IsToChuc = model.IsToChuc;
                    modelChange.LoaiDangKyID = model.LoaiDangKyID;
                    modelChange.TenTacPham = model.TenTacPham;
                    modelChange.PhimID = model.PhimID;
                    modelChange.TenPhim = model.TenPhim;
                    modelChange.TrangThaiCongBoID = model.TrangThaiCongBoID;
                    modelChange.NgayCongBo = model.NgayCongBo;
                    modelChange.LoaiHinhID = model.LoaiHinhID;
                    modelChange.NoiCongBo = model.NoiCongBo;
                    modelChange.HinhDaiDienID = model.HinhDaiDienID;
                    modelChange.HinhDaiDienUrl = model.HinhDaiDienUrl;
                    modelChange.UserID = model.UserID;
                    long change = _quyenLienQuanRepository.QLQ_QuyenLienQuan_InsUpd(modelChange, conns, trans, out response);
                    if ((long)change < 0)
                    {
                        trans.Rollback();
                        conns.Close();
                        return new ResultResponse<long>(response, -1);
                    }
                    if (model.ListHinhAnh != null && model.ListHinhAnh.Count > 0)
                    {
                        int delHinhAnh = _hinhAnhRepository.TT_HinhAnh_DelByQuyenID(quyenID, _keyLinhVuc, conns, trans, out response);
                        if ((int)delHinhAnh < 0)
                        {
                            trans.Rollback();
                            conns.Close();
                            return new ResultResponse<long>(response, -1);
                        }
                        foreach (var item in model.ListHinhAnh)
                        {
                            item.QuyenID = quyenID;
                            item.LinhVucID = _keyLinhVuc;
                            item.UserID = model.UserID;
                            long hinhAnhID = _hinhAnhRepository.TT_HinhAnh_InsUpd(item, conns, trans, out response);
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
                        int delPhim = _phimRepository.TT_Phim_DelByQuyenID(quyenID, _keyLinhVuc, conns, trans, out response);
                        if ((int)delPhim < 0)
                        {
                            trans.Rollback();
                            conns.Close();
                            return new ResultResponse<long>(response, -1);
                        }
                        foreach (var item in model.ListPhim)
                        {
                            item.QuyenID = quyenID;
                            item.LinhVucID = _keyLinhVuc;
                            item.UserID = model.UserID;
                            long phimID = _phimRepository.TT_Phim_InsUpd(item, conns, trans, out response);
                            if ((long)phimID < 0)
                            {
                                trans.Rollback();
                                conns.Close();
                                return new ResultResponse<long>(response, -1);
                            }
                        }
                    }
                    int nguoiBieuDien = _quyenLienQuanRepository.QLQ_NguoiBieuDien_Ins(model.ListNguoiBieuDien, quyenID, model.UserID, conns, trans, out response);
                    if ((int)nguoiBieuDien < 0)
                    {
                        trans.Rollback();
                        conns.Close();
                        return new ResultResponse<long>(response, -1);
                    }
                    int dinhKem = _quyenLienQuanRepository.QLQ_DinhKem_Ins(model.ListDinhKem, quyenID, _keyCapDoi, capDoiID, model.UserID, conns, trans, out response);
                    if ((int)dinhKem < 0)
                    {
                        trans.Rollback();
                        conns.Close();
                        return new ResultResponse<long>(response, -1);
                    }
                    if (model.HoSoID > 0)
                    {
                        TT_CapQuyenAdd modelCapQuyen = new TT_CapQuyenAdd();
                        modelCapQuyen.HoSoID = model.HoSoID;
                        modelCapQuyen.ThuTucID = model.ThuTucID;
                        modelCapQuyen.TenThuTuc = model.TenThuTuc;
                        modelCapQuyen.MaBoHoSo = model.MaBoHoSo;
                        modelCapQuyen.SoBienNhan = model.SoBienNhan;
                        modelCapQuyen.LoaiNghiepVuID = _keyCapDoi;
                        modelCapQuyen.KeyMapID = capDoiID;
                        modelCapQuyen.LinhVucID = _keyLinhVuc;
                        modelCapQuyen.TenLinhVuc = _nameLinhVuc;
                        modelCapQuyen.NguoiKyID = model.NguoiKyID;
                        modelCapQuyen.UserID = model.UserID;
                        long capQuyen = _capQuyenRepository.TT_CapQuyen_InsUpd(modelCapQuyen, conns, trans, out response);
                        if ((long)capQuyen < 0)
                        {
                            trans.Rollback();
                            conns.Close();
                            return new ResultResponse<long>(response, -1);
                        }
                    }
                }
                trans.Commit();
                conns.Close();
            }
            return new ResultResponse<long>(response, capDoiID);
        }
        #endregion QLQ_CapDoi
    }
}