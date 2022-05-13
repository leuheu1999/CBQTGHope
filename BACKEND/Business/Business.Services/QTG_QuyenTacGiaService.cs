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
    public class QTG_QuyenTacGiaService : IQTG_QuyenTacGiaService
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(QTG_QuyenTacGiaService));

        #region Private Repository & contractor       
        private static IQTG_QuyenTacGiaRepository _quyenTacGiaRepository;
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
        public QTG_QuyenTacGiaService(IQTG_QuyenTacGiaRepository quyenTacGiaRepository, ITT_CapQuyenRepository capQuyenRepository, ITT_HinhAnhRepository hinhAnhRepository, ITT_PhimRepository phimRepository)
        {
            _quyenTacGiaRepository = quyenTacGiaRepository;
            _capQuyenRepository = capQuyenRepository;
            _hinhAnhRepository = hinhAnhRepository;
            _phimRepository = phimRepository;
            var settings = new DataSettingsManager();
            Conn = settings.GetStringConnectChuyenNganh();
        }
        #endregion

        #region QTG_QuyenTacGia
        public ResultResponse<List<QTG_QuyenTacGiaMap>> QTG_QuyenTacGia_List(QTG_QuyenTacGiaParam model)
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.QTG_QuyenTacGia_List(model, out response);
            if (data != null && data.Count > 0)
            {
                foreach (var item in data)
                {
                    item.ListTacGia = _quyenTacGiaRepository.QTG_TacGia_ByQuyenTGId(item.QuyenTacGiaID, out response);
                    item.ListChuSoHuu = _quyenTacGiaRepository.QTG_ChuSoHuu_ByQuyenTGId(item.QuyenTacGiaID, out response);
                }
            }

            return new ResultResponse<List<QTG_QuyenTacGiaMap>>(response, data);
        }
        public ResultResponse<QTG_QuyenTacGiaAdd> QTG_QuyenTacGia_ById(long id)
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.QTG_QuyenTacGia_ById(id, out response);
            if (data != null)
            {
                data.ListLichSu = _quyenTacGiaRepository.QTG_LichSu_ById(id, data.StaticID, out response);
            }
            return new ResultResponse<QTG_QuyenTacGiaAdd>(response, data);
        }
        public ResultResponse<long> QTG_QuyenTacGia_GetStt()
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.QTG_QuyenTacGia_GetStt(out response);
            return new ResultResponse<long>(response, data);
        }
        public ResultResponse<long> QTG_QuyenTacGia_InsUpd(QTG_QuyenTacGiaAdd model)
        {
            var response = new ResponseModel();
            long quyenTacGiaID = 0;
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
                quyenTacGiaID = _quyenTacGiaRepository.QTG_QuyenTacGia_InsUpd(model, conns, trans, out response);
                if (quyenTacGiaID < 0)
                    return new ResultResponse<long>(response, -1);
                if (quyenTacGiaID > 0)
                {
                    if(model.ListHinhAnh != null && model.ListHinhAnh.Count > 0)
                    {
                        int delHinhAnh = _hinhAnhRepository.TT_HinhAnh_DelByQuyenID(quyenTacGiaID, _keyLinhVuc, conns, trans, out response);
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
                        int delPhim = _phimRepository.TT_Phim_DelByQuyenID(quyenTacGiaID, _keyLinhVuc, conns, trans, out response);
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
                            long phimID = _phimRepository.TT_Phim_InsUpd(item, conns, trans, out response);
                            if ((long)phimID < 0)
                            {
                                trans.Rollback();
                                conns.Close();
                                return new ResultResponse<long>(response, -1);
                            }
                        }
                    }
                    int tacGia = _quyenTacGiaRepository.QTG_TacGia_Ins(model.ListTacGia, quyenTacGiaID, model.UserID, conns, trans, out response);
                    if ((int)tacGia < 0)
                    {
                        trans.Rollback();
                        conns.Close();
                        return new ResultResponse<long>(response, -1);
                    }
                    int chuSoHuu = _quyenTacGiaRepository.QTG_ChuSoHuu_Ins(model.ListChuSoHuu, quyenTacGiaID, model.UserID, conns, trans, out response);
                    if ((int)chuSoHuu < 0)
                    {
                        trans.Rollback();
                        conns.Close();
                        return new ResultResponse<long>(response, -1);
                    }
                    int dinhKem = _quyenTacGiaRepository.QTG_DinhKem_Ins(model.ListDinhKem, quyenTacGiaID, 0, 0, model.UserID, conns, trans, out response);
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
                        modelCapQuyen.KeyMapID = quyenTacGiaID;
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
            return new ResultResponse<long>(response, quyenTacGiaID);
        }
        public ResultResponse<int> QTG_QuyenTacGia_Del(long quyenTacGiaID, Guid userID)
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.QTG_QuyenTacGia_Del(quyenTacGiaID, userID, out response);
            return new ResultResponse<int>(response, data);
        }
        public ResultResponse<List<QTG_GiayChungNhanMap>> QTG_QuyenTacGia_SearchGCN(QTG_GiayChungNhanParam model)
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.QTG_QuyenTacGia_SearchGCN(model, out response);
            return new ResultResponse<List<QTG_GiayChungNhanMap>>(response, data);
        }
        public ResultResponse<List<QTG_SoThuTuMap>> QTG_QuyenTacGia_SearchSTT(QTG_SoThuTuParam model)
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.QTG_QuyenTacGia_SearchSTT(model, out response);
            return new ResultResponse<List<QTG_SoThuTuMap>>(response, data);
        }
        public ResultResponse<long> QTG_QuyenTacGia_CapSo(QTG_QuyenTacGia_CapSo model)
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.QTG_QuyenTacGia_CapSo(model, out response);
            return new ResultResponse<long>(response, data);
        }
        public ResultResponse<int> QTG_QuyenTacGia_CheckSoTT(long id, string soTT)
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.QTG_QuyenTacGia_CheckSoTT(id, soTT, out response);
            return new ResultResponse<int>(response, data);
        }
        public ResultResponse<int> QTG_QuyenTacGia_CheckSoGCN(long id, string soGCN)
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.QTG_QuyenTacGia_CheckSoGCN(id, soGCN, out response);
            return new ResultResponse<int>(response, data);
        }
        public ResultResponse<int> QTG_QuyenTacGia_Duyet(long id, int loaiNghiepVuID, bool isDuyet)
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.QTG_QuyenTacGia_Duyet(id, loaiNghiepVuID, isDuyet, out response);
            return new ResultResponse<int>(response, data);
        }
        #endregion

        #region QTG_CapLai
        public ResultResponse<QTG_CapLaiAdd> QTG_CapLai_ById(long id)
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.QTG_CapLai_ById(id, out response);
            return new ResultResponse<QTG_CapLaiAdd>(response, data);
        }
        public ResultResponse<QTG_CapLaiAdd> QTG_CapLai_ByHoSoId(long id)
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.QTG_CapLai_ByHoSoId(id, out response);
            return new ResultResponse<QTG_CapLaiAdd>(response, data);
        }
        public ResultResponse<QTG_CapLaiAdd> QTG_CapLai_ByQuyenTGId(long id)
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.QTG_CapLai_ByQuyenTGId(id, out response);
            return new ResultResponse<QTG_CapLaiAdd>(response, data);
        }
        public ResultResponse<long> QTG_CapLai_InsUpd(QTG_CapLaiAdd model)
        {
            var response = new ResponseModel();
            long capLaiID = 0;
            using (var conns = new SqlConnection(Conn))
            {
                conns.Open();
                IDbTransaction trans = conns.BeginTransaction();
                var quyenID = _quyenTacGiaRepository.QTG_QuyenTacGia_OldToNew(model.QuyenTacGiaCuID, _keyCapLai, model.CapLaiID, conns, trans, out response);
                if (quyenID < 0)
                    return new ResultResponse<long>(response, -1);
                if (model.CapLaiID == 0)
                {
                    var hinhAnhParam = new TT_HinhAnhParam();
                    hinhAnhParam.QuyenID = model.QuyenTacGiaCuID;
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
                    phimParam.QuyenID = model.QuyenTacGiaCuID;
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
                model.QuyenTacGiaID = quyenID;
                capLaiID = _quyenTacGiaRepository.QTG_CapLai_InsUpd(model, conns, trans, out response);
                if (capLaiID < 0)
                {
                    trans.Rollback();
                    conns.Close();
                    return new ResultResponse<long>(response, -1);
                }
                if (capLaiID > 0)
                {
                    var modelChange = new QTG_QuyenTacGiaChange();
                    modelChange.QuyenTacGiaID = quyenID;
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
                    long change = _quyenTacGiaRepository.QTG_QuyenTacGia_Change(modelChange, conns, trans, out response);
                    if ((long)change < 0)
                    {
                        trans.Rollback();
                        conns.Close();
                        return new ResultResponse<long>(response, -1);
                    }
                    int dinhKem = _quyenTacGiaRepository.QTG_DinhKem_Ins(model.ListDinhKem, quyenID, _keyCapLai, capLaiID, model.UserID, conns, trans, out response);
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
        #endregion QTG_CapLai

        #region QTG_ThuHoi
        public ResultResponse<QTG_ThuHoiAdd> QTG_ThuHoi_ById(long id)
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.QTG_ThuHoi_ById(id, out response);
            return new ResultResponse<QTG_ThuHoiAdd>(response, data);
        }
        public ResultResponse<QTG_ThuHoiAdd> QTG_ThuHoi_ByHoSoId(long id)
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.QTG_ThuHoi_ByHoSoId(id, out response);
            return new ResultResponse<QTG_ThuHoiAdd>(response, data);
        }
        public ResultResponse<QTG_ThuHoiAdd> QTG_ThuHoi_ByQuyenTGId(long id)
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.QTG_ThuHoi_ByQuyenTGId(id, out response);
            return new ResultResponse<QTG_ThuHoiAdd>(response, data);
        }
        public ResultResponse<long> QTG_ThuHoi_InsUpd(QTG_ThuHoiAdd model)
        {
            var response = new ResponseModel();
            long thuHoiID = 0;
            using (var conns = new SqlConnection(Conn))
            {
                conns.Open();
                IDbTransaction trans = conns.BeginTransaction();
                thuHoiID = _quyenTacGiaRepository.QTG_ThuHoi_InsUpd(model, conns, trans, out response);
                if (thuHoiID < 0)
                    return new ResultResponse<long>(response, -1);
                if (thuHoiID > 0)
                {
                    var modelChange = new QTG_QuyenTacGiaChange();
                    modelChange.QuyenTacGiaID = model.QuyenTacGiaID;
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
                    long change = _quyenTacGiaRepository.QTG_QuyenTacGia_Change(modelChange, conns, trans, out response);
                    if ((long)change < 0)
                    {
                        trans.Rollback();
                        conns.Close();
                        return new ResultResponse<long>(response, -1);
                    }
                    int dinhKem = _quyenTacGiaRepository.QTG_DinhKem_Ins(model.ListDinhKem, model.QuyenTacGiaID, _keyThuHoi, thuHoiID, model.UserID, conns, trans, out response);
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
        #endregion QTG_ThuHoi

        #region QTG_ChuyenChuSoHuu
        public ResultResponse<QTG_ChuyenChuSoHuuAdd> QTG_ChuyenChuSoHuu_ById(long id)
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.QTG_ChuyenChuSoHuu_ById(id, out response);
            return new ResultResponse<QTG_ChuyenChuSoHuuAdd>(response, data);
        }
        public ResultResponse<QTG_ChuyenChuSoHuuAdd> QTG_ChuyenChuSoHuu_ByHoSoId(long id)
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.QTG_ChuyenChuSoHuu_ByHoSoId(id, out response);
            return new ResultResponse<QTG_ChuyenChuSoHuuAdd>(response, data);
        }
        public ResultResponse<QTG_ChuyenChuSoHuuAdd> QTG_ChuyenChuSoHuu_ByQuyenTGId(long id)
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.QTG_ChuyenChuSoHuu_ByQuyenTGId(id, out response);
            return new ResultResponse<QTG_ChuyenChuSoHuuAdd>(response, data);
        }
        public ResultResponse<long> QTG_ChuyenChuSoHuu_InsUpd(QTG_ChuyenChuSoHuuAdd model)
        {
            var response = new ResponseModel();
            long chuyenChuID = 0;
            using (var conns = new SqlConnection(Conn))
            {
                conns.Open();
                IDbTransaction trans = conns.BeginTransaction();
                var quyenID = _quyenTacGiaRepository.QTG_QuyenTacGia_OldToNew(model.QuyenTacGiaCuID, _keyDoiChu, model.ChuyenChuSoHuuID, conns, trans, out response);
                if (quyenID < 0)
                    return new ResultResponse<long>(response, -1);
                if (model.ChuyenChuSoHuuID == 0)
                {
                    var hinhAnhParam = new TT_HinhAnhParam();
                    hinhAnhParam.QuyenID = model.QuyenTacGiaCuID;
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
                    phimParam.QuyenID = model.QuyenTacGiaCuID;
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
                model.QuyenTacGiaID = quyenID;
                chuyenChuID = _quyenTacGiaRepository.QTG_ChuyenChuSoHuu_InsUpd(model, conns, trans, out response);
                if (chuyenChuID < 0)
                {
                    trans.Rollback();
                    conns.Close();
                    return new ResultResponse<long>(response, -1);
                }
                if (chuyenChuID > 0)
                {
                    var modelChange = new QTG_QuyenTacGiaChange();
                    modelChange.QuyenTacGiaID = quyenID;
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
                    long change = _quyenTacGiaRepository.QTG_QuyenTacGia_Change(modelChange, conns, trans, out response);
                    if ((long)change < 0)
                    {
                        trans.Rollback();
                        conns.Close();
                        return new ResultResponse<long>(response, -1);
                    }
                    int chuSoHuu = _quyenTacGiaRepository.QTG_ChuSoHuu_Ins(model.ListChuSoHuu, quyenID, model.UserID, conns, trans, out response);
                    if ((int)chuSoHuu < 0)
                    {
                        trans.Rollback();
                        conns.Close();
                        return new ResultResponse<long>(response, -1);
                    }
                    int dinhKem = _quyenTacGiaRepository.QTG_DinhKem_Ins(model.ListDinhKem, quyenID, _keyDoiChu, chuyenChuID, model.UserID, conns, trans, out response);
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
        #endregion QTG_ChuyenChuSoHuu

        #region QTG_CapDoi
        public ResultResponse<QTG_CapDoiAdd> QTG_CapDoi_ById(long id)
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.QTG_CapDoi_ById(id, out response);
            return new ResultResponse<QTG_CapDoiAdd>(response, data);
        }
        public ResultResponse<QTG_CapDoiAdd> QTG_CapDoi_ByHoSoId(long id)
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.QTG_CapDoi_ByHoSoId(id, out response);
            return new ResultResponse<QTG_CapDoiAdd>(response, data);
        }
        public ResultResponse<QTG_CapDoiAdd> QTG_CapDoi_ByQuyenTGId(long id)
        {
            var response = new ResponseModel();
            var data = _quyenTacGiaRepository.QTG_CapDoi_ByQuyenTGId(id, out response);
            return new ResultResponse<QTG_CapDoiAdd>(response, data);
        }
        public ResultResponse<long> QTG_CapDoi_InsUpd(QTG_CapDoiAdd model)
        {
            var response = new ResponseModel();
            long capDoiID = 0;
            using (var conns = new SqlConnection(Conn))
            {
                conns.Open();
                IDbTransaction trans = conns.BeginTransaction();
                var quyenID = _quyenTacGiaRepository.QTG_QuyenTacGia_OldToNew(model.QuyenTacGiaCuID, _keyCapDoi, model.CapDoiID, conns, trans, out response);
                if (quyenID < 0)
                    return new ResultResponse<long>(response, -1);
                if(model.CapDoiID == 0)
                {
                    var hinhAnhParam = new TT_HinhAnhParam();
                    hinhAnhParam.QuyenID = model.QuyenTacGiaCuID;
                    hinhAnhParam.LinhVucID = _keyLinhVuc;
                    var lstHinhAnh = _hinhAnhRepository.TT_HinhAnh_List(hinhAnhParam, out response);
                    if(lstHinhAnh != null && lstHinhAnh.Count > 0)
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
                    phimParam.QuyenID = model.QuyenTacGiaCuID;
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
                model.QuyenTacGiaID = quyenID;
                capDoiID = _quyenTacGiaRepository.QTG_CapDoi_InsUpd(model, conns, trans, out response);
                if (capDoiID < 0)
                {
                    trans.Rollback();
                    conns.Close();
                    return new ResultResponse<long>(response, -1);
                }
                if (capDoiID > 0)
                {
                    var modelChange = new QTG_QuyenTacGiaAdd();
                    modelChange.QuyenTacGiaID = quyenID;
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
                    modelChange.LoaiDangKyID = model.LoaiDangKyID;
                    modelChange.IsToChuc = model.IsToChuc;
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
                    long change = _quyenTacGiaRepository.QTG_QuyenTacGia_InsUpd(modelChange, conns, trans, out response);
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
                    int tacGia = _quyenTacGiaRepository.QTG_TacGia_Ins(model.ListTacGia, quyenID, model.UserID, conns, trans, out response);
                    if ((int)tacGia < 0)
                    {
                        trans.Rollback();
                        conns.Close();
                        return new ResultResponse<long>(response, -1);
                    }
                    int dinhKem = _quyenTacGiaRepository.QTG_DinhKem_Ins(model.ListDinhKem, quyenID, _keyCapDoi, capDoiID, model.UserID, conns, trans, out response);
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
        #endregion QTG_CapDoi
    }
}
