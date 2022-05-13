using Business.Entities;
using Business.Entities.Domain;
using Business.Entities.Domain.Print;
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
    public class DungChungService : IDungChungService
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(DungChungService));

        #region Private Repository & contractor
        //IDbConnection cons;
        //IDbTransaction trans;
        private static string Conn = null;
        private static IDungChungRepository _dungChungRepository;
        private readonly IDM_NhomQuyenRepository _dm_NhomQuyenRepository;
        private readonly IDM_QuocGiaRepository _dm_QuocGiaRepository;
        private readonly IDM_TinhThanhRepository _dm_TinhThanhRepository;
        private readonly IDM_QuyenRepository _dm_QuyenRepository;
        private readonly IDM_QuyenChucNangRepository _dm_QuyenChucNangRepository;
        private readonly IDM_PhuongXaRepository _dm_PhuongXaRepository;
        private readonly IDM_QuanHuyenRepository _dm_QuanHuyenRepository;
        private readonly ICauHinhHeThongRepository _cauHinhHeThongRepository;
        private readonly ITT_ChuSoHuuRepository _chuSoHuuRepository;
        private readonly ITT_CongDanRepository _congDanRepository;
        private readonly ITT_HinhAnhRepository _hinhAnhRepository;
        private readonly ITT_PhimRepository _phimRepository;
        private readonly ITT_TacGiaRepository _tacGiaRepository;
        private readonly ITT_DinhKemRepository _dinhKemRepository;
        private readonly ITT_NguoiBieuDienRepository _nguoiBieuDienRepository;
        private readonly IDM_VungMienRepository _dm_VungMienRepository;
        private readonly IDM_LoaiHinhDangKyRepository _dm_LoaiHinhDangKyRepository;
        private readonly IDM_LoaiHinhTacPhamRepository _dm_LoaiHinhTacPhamRepository;
        private readonly IDM_CoQuanCapRepository _dm_CoQuanCapRepository;
        private readonly IDM_NguoiKyRepository _dm_NguoiKyRepository;
        private readonly IDM_LoaiSoRepository _dm_LoaiSoRepository;
        public DungChungService(IDungChungRepository dungChungRepository,
                                 IDM_NhomQuyenRepository dm_NhomQuyenRepository,
                                 IDM_QuocGiaRepository dm_QuocGiaRepository,
                                 IDM_TinhThanhRepository dm_TinhThanhRepository,
                                 IDM_QuyenRepository dm_QuyenRepository,
                                 IDM_PhuongXaRepository dm_PhuongXaRepository,
                                 IDM_QuyenChucNangRepository dm_QuyenChucNangRepository,
                                 IDM_QuanHuyenRepository dm_QuanHuyenRepository,
                                 ICauHinhHeThongRepository cauHinhHeThongRepository,
                                 ITT_ChuSoHuuRepository chuSoHuuRepository,
                                 ITT_CongDanRepository congDanRepository,
                                 ITT_HinhAnhRepository hinhAnhRepository,
                                 ITT_PhimRepository phimRepository,
                                 ITT_TacGiaRepository tacGiaRepository,
                                 ITT_DinhKemRepository dinhKemRepository,
                                 ITT_NguoiBieuDienRepository nguoiBieuDienRepository,
                                 IDM_VungMienRepository dm_VungMienRepository,
                                 IDM_LoaiHinhDangKyRepository dm_LoaiHinhDangKyRepository,
                                 IDM_LoaiHinhTacPhamRepository dm_LoaiHinhTacPhamRepository,
                                 IDM_CoQuanCapRepository dm_CoQuanCapRepository,
                                 IDM_NguoiKyRepository dm_NguoiKyRepository,
                                 IDM_LoaiSoRepository dm_LoaiSoRepository
        )
        {
            _dungChungRepository = dungChungRepository;
            _dm_NhomQuyenRepository = dm_NhomQuyenRepository;
            _dm_QuocGiaRepository = dm_QuocGiaRepository;
            _dm_TinhThanhRepository = dm_TinhThanhRepository;
            _dm_QuyenRepository = dm_QuyenRepository;
            _dm_QuyenChucNangRepository = dm_QuyenChucNangRepository;
            _dm_PhuongXaRepository = dm_PhuongXaRepository;
            _dm_QuanHuyenRepository = dm_QuanHuyenRepository;
            _cauHinhHeThongRepository = cauHinhHeThongRepository;
            _chuSoHuuRepository = chuSoHuuRepository;
            _congDanRepository = congDanRepository;
            _hinhAnhRepository = hinhAnhRepository;
            _phimRepository = phimRepository;
            _tacGiaRepository = tacGiaRepository;
            _dinhKemRepository = dinhKemRepository;
            _nguoiBieuDienRepository = nguoiBieuDienRepository;
            _dm_VungMienRepository = dm_VungMienRepository;
            _dm_LoaiHinhDangKyRepository = dm_LoaiHinhDangKyRepository;
            _dm_LoaiHinhTacPhamRepository = dm_LoaiHinhTacPhamRepository;
            _dm_CoQuanCapRepository = dm_CoQuanCapRepository;
            _dm_NguoiKyRepository = dm_NguoiKyRepository;
            _dm_LoaiSoRepository = dm_LoaiSoRepository;
            //get connection
            var settings = new DataSettingsManager();
            Conn = settings.GetStringConnectMaster();
        }
        #endregion
        public ResultResponse<List<CBO_DungChungViewModel>> CBO_DungChung_GetAll(CBO_DungChungParam model)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey(model.TableName, "DungChung" + (string.IsNullOrEmpty(model.ParentID1) ? "" : model.ParentID1));
            var data = CacheHelper.GetData<List<CBO_DungChungViewModel>>(cacheKey);
            if (data == null || data.Count == 0)
            {
                data = _dungChungRepository.CBO_DungChung_GetAll(model, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<List<CBO_DungChungViewModel>>(response, data);
        }
        public ResultResponse<List<CBO_DungChungViewModel>> CBO_DungChung_GetAllMater(CBO_DungChungParam model)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey(model.TableName, "DungChungMaster" + (string.IsNullOrEmpty(model.ParentID1) ? "" : model.ParentID1));
            var data = CacheHelper.GetData<List<CBO_DungChungViewModel>>(cacheKey);
            if (data == null || data.Count == 0)
            {
                data = _dungChungRepository.CBO_DungChung_GetAllMater(model, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<List<CBO_DungChungViewModel>>(response, data);
        }
        public ResultResponse<string> GetKeyCauHinhHeThong(string Key)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("CauHinhHeThong", Key);
            var data = CacheHelper.GetData<string>(cacheKey);
            if (data == null)
            {
                data = _cauHinhHeThongRepository.CauHinhHeThong_GetByKey(Key, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<string>(response, data);
        }
        public ResultResponse<string> GetKeyDMDonVi(string Key)
        {
            var response = new ResponseModel();
            var data = _cauHinhHeThongRepository.DM_DonVi_GetByKey(Key, out response);
            return new ResultResponse<string>(response, data);
        }
        public ResultResponse<bool> DisposeCache(string Key)
        {
            var response = new ResponseModel();
            if (!string.IsNullOrEmpty(Key))
            {
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey(Key, "");
                CacheHelper.Remove(cacheKey);
            }
            else
            {
                CacheHelper.RemoveAll();
            }
            return new ResultResponse<bool>(response, true);
        }

        #region DM_QuocGia
        public ResultResponse<List<DM_QuocGiaMap>> DM_QuocGia_List(DM_QuocGiaMapParam model)
        {
            var response = new ResponseModel();
            var data = _dm_QuocGiaRepository.DM_QuocGia_List(model, out response);
            var result = new ResultResponse<List<DM_QuocGiaMap>>(response, data);
            return result;
        }
        public ResultResponse<DM_QuocGiaMapAdd> GetQuocGiaById(long id)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("DM_QuocGia", id.ToString());
            var data = CacheHelper.GetData<DM_QuocGiaMapAdd>(cacheKey);
            if (data == null)
            {
                data = _dm_QuocGiaRepository.DM_QuocGia_GetById(id, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            var result = new ResultResponse<DM_QuocGiaMapAdd>(response, data);
            return result;
        }
        public ResultResponse<long> DM_QuocGia_InsUpdate(DM_QuocGiaMapAdd model)
        {
            var response = new ResponseModel();
            var data = _dm_QuocGiaRepository.DM_QuocGia_InsUpd(model, out response);
            if (data > 0)
            {
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey("DM_QuocGia", "");
                CacheHelper.Remove(cacheKey);
            }
            var result = new ResultResponse<long>(response, data);
            return result;
        }
        public ResultResponse<int> DM_QuocGia_Del(long id, Guid lastupduserid)
        {
            var response = new ResponseModel();
            var data = _dm_QuocGiaRepository.DM_QuocGia_Del(id, lastupduserid, out response);
            if (data > 0)
            {
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey("DM_QuocGia", "");
                CacheHelper.Remove(cacheKey);
            }
            var result = new ResultResponse<int>(response, data);
            return result;
        }
        public ResultResponse<DM_QuocGiaMapAdd> GetQuocGiaByMa(string ma)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("DM_QuocGia", ma.ToString());
            var data = CacheHelper.GetData<DM_QuocGiaMapAdd>(cacheKey);
            if (data == null)
            {
                data = _dm_QuocGiaRepository.DM_QuocGia_GetByMa(ma, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            var result = new ResultResponse<DM_QuocGiaMapAdd>(response, data);
            return result;
        }
        #endregion

        #region DM_TinhThanh
        public ResultResponse<List<DM_TinhThanhMap>> DM_TinhThanh_List(DM_TinhThanhMapParam model)
        {
            var response = new ResponseModel();
            var data = _dm_TinhThanhRepository.DM_TinhThanh_List(model, out response);
            return new ResultResponse<List<DM_TinhThanhMap>>(response, data);
        }
        public ResultResponse<DM_TinhThanhMapAdd> DM_TinhThanh_GetById(long tinhThanhID)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("DM_TinhThanh", tinhThanhID.ToString());
            var data = CacheHelper.GetData<DM_TinhThanhMapAdd>(cacheKey);
            if (data == null)
            {
                data = _dm_TinhThanhRepository.DM_TinhThanh_GetById(tinhThanhID, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<DM_TinhThanhMapAdd>(response, data);
        }
        public ResultResponse<DM_TinhThanhMapAdd> DM_TinhThanh_GetByMa(string maTinhThanh)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("DM_TinhThanh", maTinhThanh.ToString());
            var data = CacheHelper.GetData<DM_TinhThanhMapAdd>(cacheKey);
            if (data == null)
            {
                data = _dm_TinhThanhRepository.DM_TinhThanh_GetByMa(maTinhThanh, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<DM_TinhThanhMapAdd>(response, data);
        }
        public ResultResponse<long> DM_TinhThanh_InsUpd(DM_TinhThanhMapAdd model)
        {
            var response = new ResponseModel();
            var data = _dm_TinhThanhRepository.DM_TinhThanh_InsUpd(model, out response);
            if (data > 0)
            {
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey("DM_TinhThanh", "");
                CacheHelper.Remove(cacheKey);
            }
            return new ResultResponse<long>(response, data);
        }
        public ResultResponse<int> DM_TinhThanh_Del(long id, Guid lastupduserid)
        {
            var response = new ResponseModel();
            var data = _dm_TinhThanhRepository.DM_TinhThanh_Del(id, lastupduserid, out response);
            if (data > 0)
            {
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey("DM_TinhThanh", "");
                CacheHelper.Remove(cacheKey);
            }
            var result = new ResultResponse<int>(response, data);
            return result;
        }
        #endregion

        #region DM_NhomQuyen
        public ResultResponse<List<DM_NhomQuyenMap>> DM_NhomQuyen_List(DM_NhomQuyenParam model)
        {
            var response = new ResponseModel();
            var data = _dm_NhomQuyenRepository.DM_NhomQuyen_List(model, out response);
            var result = new ResultResponse<List<DM_NhomQuyenMap>>(response, data);
            return result;
        }
        public ResultResponse<DM_NhomQuyenAdd> DM_NhomQuyen_GetById(long id)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("NhomQuyen", id.ToString());
            var data = CacheHelper.GetData<DM_NhomQuyenAdd>(cacheKey);
            if (data == null)
            {
                data = _dm_NhomQuyenRepository.DM_NhomQuyen_GetById(id, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<DM_NhomQuyenAdd>(response, data);
        }
        public ResultResponse<DM_NhomQuyenAdd> DM_NhomQuyen_GetByMa(string ma)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("NhomQuyen", ma.ToString());
            var data = CacheHelper.GetData<DM_NhomQuyenAdd>(cacheKey);
            if (data == null)
            {
                data = _dm_NhomQuyenRepository.DM_NhomQuyen_GetByMa(ma, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<DM_NhomQuyenAdd>(response, data);
        }
        public ResultResponse<long> DM_NhomQuyen_InsUpd(DM_NhomQuyenAdd model)
        {
            var response = new ResponseModel();
            var data = _dm_NhomQuyenRepository.DM_NhomQuyen_InsUpd(model, out response);
            if (data > 0)
            {
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey("NhomQuyen", "");
                CacheHelper.Remove(cacheKey);
            }
            return new ResultResponse<long>(response, data);
        }
        public ResultResponse<bool> DM_NhomQuyen_Delete(long id, Guid userId)
        {
            var response = new ResponseModel();
            var data = _dm_NhomQuyenRepository.DM_NhomQuyen_Delete(id, userId, out response);
            if (data)
            {
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey("NhomQuyen", "");
                CacheHelper.Remove(cacheKey);
            }
            return new ResultResponse<bool>(response, data);
        }
        #endregion

        #region DM_Quyen
        public ResultResponse<List<DM_QuyenMap>> DM_Quyen_List(DM_QuyenMapParam model)
        {
            var response = new ResponseModel();
            var data = _dm_QuyenRepository.DM_Quyen_List(model, out response);
            var result = new ResultResponse<List<DM_QuyenMap>>(response, data);
            return result;
        }
        public ResultResponse<DM_QuyenMapAdd> DM_Quyen_GetById(long id)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("DM_Quyen", id.ToString());
            var data = CacheHelper.GetData<DM_QuyenMapAdd>(cacheKey);
            if (data == null)
            {
                data = _dm_QuyenRepository.DM_Quyen_GetById(id, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<DM_QuyenMapAdd>(response, data);
        }
        public ResultResponse<DM_QuyenMapAdd> DM_Quyen_GetByMa(string ma)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("DM_Quyen", ma.ToString());
            var data = CacheHelper.GetData<DM_QuyenMapAdd>(cacheKey);
            if (data == null)
            {
                data = _dm_QuyenRepository.DM_Quyen_GetByMa(ma, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<DM_QuyenMapAdd>(response, data);
        }
        public ResultResponse<long> DM_Quyen_InsUpd(DM_QuyenMapAdd model)
        {
            var response = new ResponseModel();
            var data = _dm_QuyenRepository.DM_Quyen_InsUpd(model, out response);
            if (data > 0)
            {
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey("DM_Quyen", "");
                CacheHelper.Remove(cacheKey);
            }
            return new ResultResponse<long>(response, data);
        }
        public ResultResponse<bool> DM_Quyen_Delete(long id, Guid userId)
        {
            var response = new ResponseModel();
            var data = _dm_QuyenRepository.DM_Quyen_Delete(id, userId, out response);
            if (data)
            {
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey("DM_Quyen", "");
                CacheHelper.Remove(cacheKey);
            }
            return new ResultResponse<bool>(response, data);
        }
        #endregion

        #region DM_QuyenChucNang
        public ResultResponse<List<DM_QuyenChucNangMap>> DM_QuyenChucNang_List(DM_QuyenChucNangMapParam model)
        {
            var response = new ResponseModel();
            var data = _dm_QuyenChucNangRepository.DM_QuyenChucNang_List(model, out response);
            var result = new ResultResponse<List<DM_QuyenChucNangMap>>(response, data);
            return result;
        }
        public ResultResponse<DM_QuyenChucNangAdd> DM_QuyenChucNang_GetById(long id)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("DM_QuyenChucNang", id.ToString());
            var data = CacheHelper.GetData<DM_QuyenChucNangAdd>(cacheKey);
            if (data == null)
            {
                data = _dm_QuyenChucNangRepository.DM_QuyenChucNang_GetById(id, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<DM_QuyenChucNangAdd>(response, data);
        }
        public ResultResponse<DM_QuyenChucNangAdd> DM_QuyenChucNang_GetByMa(string ma)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("DM_QuyenChucNang", ma.ToString());
            var data = CacheHelper.GetData<DM_QuyenChucNangAdd>(cacheKey);
            if (data == null)
            {
                data = _dm_QuyenChucNangRepository.DM_QuyenChucNang_GetByMa(ma, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<DM_QuyenChucNangAdd>(response, data);
        }
        public ResultResponse<long> DM_QuyenChuNang_InsUpd(DM_QuyenChucNangAdd model)
        {
            var response = new ResponseModel();
            var data = _dm_QuyenChucNangRepository.DM_QuyenChuNang_InsUpd(model, out response);
            if (data > 0)
            {
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey("DM_QuyenChucNang", "");
                CacheHelper.Remove(cacheKey);
            }
            return new ResultResponse<long>(response, data);
        }
        public ResultResponse<bool> DM_QuyenChucNang_Delete(long id, Guid userId)
        {
            var response = new ResponseModel();
            var data = _dm_QuyenChucNangRepository.DM_QuyenChucNang_Delete(id, userId, out response);
            if (data)
            {
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey("DM_QuyenChucNang", "");
                CacheHelper.Remove(cacheKey);
            }
            return new ResultResponse<bool>(response, data);
        }
        #endregion

        #region DM_PhuongXa
        public ResultResponse<List<DM_PhuongXaMap>> DM_PhuongXa_List(DM_PhuongXaMapParam model)
        {
            var response = new ResponseModel();
            var data = _dm_PhuongXaRepository.DM_PhuongXa_List(model, out response);
            return new ResultResponse<List<DM_PhuongXaMap>>(response, data);
        }
        public ResultResponse<DM_PhuongXaMapAdd> DM_PhuongXa_GetById(long id)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("DM_PhuongXa", id.ToString());
            var data = CacheHelper.GetData<DM_PhuongXaMapAdd>(cacheKey);
            if (data == null)
            {
                data = _dm_PhuongXaRepository.DM_PhuongXa_GetById(id, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<DM_PhuongXaMapAdd>(response, data);
        }
        public ResultResponse<DM_PhuongXaMapAdd> DM_PhuongXa_GetByMa(string ma)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("DM_PhuongXa", ma.ToString());
            var data = CacheHelper.GetData<DM_PhuongXaMapAdd>(cacheKey);
            if (data == null)
            {
                data = _dm_PhuongXaRepository.DM_PhuongXa_GetByMa(ma, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<DM_PhuongXaMapAdd>(response, data);
        }
        public ResultResponse<long> DM_PhuongXa_InsUpd(DM_PhuongXaMapAdd model)
        {
            var response = new ResponseModel();
            var data = _dm_PhuongXaRepository.DM_PhuongXa_InsUpd(model, out response);
            if (data > 0)
            {
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey("DM_PhuongXa", "");
                CacheHelper.Remove(cacheKey);
            }
            return new ResultResponse<long>(response, data);
        }
        public ResultResponse<int> DM_PhuongXa_Del(long id, Guid lastupduserid)
        {
            var response = new ResponseModel();
            var data = _dm_PhuongXaRepository.DM_PhuongXa_Del(id, lastupduserid, out response);
            if (data > 0)
            {
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey("DM_PhuongXa", "");
                CacheHelper.Remove(cacheKey);
            }
            var result = new ResultResponse<int>(response, data);
            return result;
        }
        #endregion

        #region DM_QuanHuyen
        public ResultResponse<List<DM_QuanHuyenMap>> DM_QuanHuyen_List(DM_QuanHuyenMapParam model)
        {
            var response = new ResponseModel();
            var data = _dm_QuanHuyenRepository.DM_QuanHuyen_List(model, out response);
            var result = new ResultResponse<List<DM_QuanHuyenMap>>(response, data);
            return result;
        }
        public ResultResponse<DM_QuanHuyenMapAdd> DM_QuanHuyen_GetById(long id)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("DM_QuanHuyen", id.ToString());
            var data = CacheHelper.GetData<DM_QuanHuyenMapAdd>(cacheKey);
            if (data == null)
            {
                data = _dm_QuanHuyenRepository.DM_QuanHuyen_GetById(id, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<DM_QuanHuyenMapAdd>(response, data);
        }
        public ResultResponse<DM_QuanHuyenMapAdd> DM_QuanHuyen_GetByMa(string ma)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("DM_QuanHuyen", ma.ToString());
            var data = CacheHelper.GetData<DM_QuanHuyenMapAdd>(cacheKey);
            if (data == null)
            {
                data = _dm_QuanHuyenRepository.DM_QuanHuyen_GetByMa(ma, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<DM_QuanHuyenMapAdd>(response, data);
        }
        public ResultResponse<long> DM_QuanHuyen_InsUpd(DM_QuanHuyenMapAdd model)
        {
            var response = new ResponseModel();
            var data = _dm_QuanHuyenRepository.DM_QuanHuyen_InsUpd(model, out response);
            if (data > 0)
            {
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey("DM_QuanHuyen", "");
                CacheHelper.Remove(cacheKey);
            }
            return new ResultResponse<long>(response, data);
        }
        public ResultResponse<bool> DM_QuanHuyen_Delete(long id, Guid userId)
        {
            var response = new ResponseModel();
            var data = _dm_QuanHuyenRepository.DM_QuanHuyen_Delete(id, userId, out response);
            if (data)
            {
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey("DM_QuanHuyen", "");
                CacheHelper.Remove(cacheKey);
            }
            return new ResultResponse<bool>(response, data);
        }
        #endregion

        #region CauHinhHeThong
        public ResultResponse<CauHinhHeThongAll> GetAllCauHinh()
        {
            var data = new CauHinhHeThongAll();
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("CauHinhHeThong", "");
            data.cauhinhhethong = CacheHelper.GetData<CauHinhHeThongMap>(cacheKey);
            if (data.cauhinhhethong == null)
            {
                data.cauhinhhethong = _cauHinhHeThongRepository.CauHinhHeThong_Get(out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            cacheKey = CacheHelper.BuildKey("CauHinhMail", "");
            data.cauhinhmail = CacheHelper.GetData<CauHinhMailMap>(cacheKey);
            if (data.cauhinhmail == null)
            {
                data.cauhinhmail = _cauHinhHeThongRepository.CauHinhMail_Get(out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            cacheKey = CacheHelper.BuildKey("CauHinhDonVi", "");
            data.donvi = CacheHelper.GetData<DonViMap>(cacheKey);
            if (data.donvi == null)
            {
                data.donvi = _cauHinhHeThongRepository.CauHinhDonVi_Get(out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<CauHinhHeThongAll>(response, data);
        }
        public ResultResponse<long> CauHinhheThong_Insert(CauHinhHeThongAll model)
        {
            IDbConnection conns = new SqlConnection(Conn);
            conns.Open();
            IDbTransaction transCC = conns.BeginTransaction();
            var response = new ResponseModel();
            try
            {
                var rshethong = _cauHinhHeThongRepository.CauHinhheThong_Ins(model.cauhinhhethong, conns, transCC, out response);
                if (rshethong <= 0)
                {
                    if (transCC != null) transCC.Rollback();
                    return new ResultResponse<long>(response, -1);
                }
                var rsmail = _cauHinhHeThongRepository.CauHinhmail_Ins(model.cauhinhmail, conns, transCC, out response);
                if (rsmail <= 0)
                {
                    if (transCC != null) transCC.Rollback();
                    return new ResultResponse<long>(response, -1);
                }
                var rsdonvi = _cauHinhHeThongRepository.CauHinhDonVi_Ins(model.donvi, conns, transCC, out response);
                if (rsdonvi <= 0)
                {
                    if (transCC != null) transCC.Rollback();
                    return new ResultResponse<long>(response, -1);
                }
                transCC.Commit();
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey("CauHinhHeThong", "");
                CacheHelper.Remove(cacheKey);
                cacheKey = CacheHelper.BuildKey("CauHinhMail", "");
                CacheHelper.Remove(cacheKey);
                cacheKey = CacheHelper.BuildKey("CauHinhDonVi", "");
                CacheHelper.Remove(cacheKey);
                return new ResultResponse<long>(response, rsdonvi);
            }
            catch (Exception ex)
            {
                if (transCC != null) transCC.Rollback();
                return new ResultResponse<long>(response, -1);
            }
            finally
            {
                if (transCC != null) transCC.Dispose();
                if (conns != null) conns.Dispose();
            }


        }
        public ResultResponse<int> CauHinhMail_DoiMatKhau(string pass)
        {
            var response = new ResponseModel();
            var cacheKey = CacheHelper.BuildKey("CauHinhHeThong", "");
            var data = CacheHelper.GetDataInt(cacheKey);
            if (data == 0)
            {
                data = _cauHinhHeThongRepository.CauHinhMail_DoiMatKhau(pass, out response);
                if (data != 0)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<int>(response, data);
        }
        public ResultResponse<bool> LuotTruyCapHeThong_Insert()
        {
            var response = new ResponseModel();
            var rshethong = _cauHinhHeThongRepository.LuotTruyCapHeThong_Insert(out response);
            if (rshethong == true)
            {
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey("CauHinhChung", "");
                CacheHelper.Remove(cacheKey);
            }
            return new ResultResponse<bool>(response, rshethong);
        }
        #endregion

        #region Export
        public ResultResponse<string> Print(Print_DataMap param)
        {
            ResponseModel resStatus;
            var _dt = _dungChungRepository.Print(param, out resStatus);
            return new ResultResponse<string>
            {
                resultObject = _dt,
                description = resStatus.description,
                resultType = resStatus.resultType,
                status = resStatus.status,
                StatusCode = resStatus.StatusCode,
                throwException = resStatus.throwException
            };

        }
        public ResultResponse<E_ParametersMap> GetInfoParametersByKeyCode(string model)
        {
            ResponseModel resStatus;
            var _dt = _dungChungRepository.GetInfoParametersByKeyCode(model, out resStatus);
            return new ResultResponse<E_ParametersMap>
            {
                resultObject = _dt,
                description = resStatus.description,
                resultType = resStatus.resultType,
                status = resStatus.status,
                StatusCode = resStatus.StatusCode,
                throwException = resStatus.throwException
            };
        }
        #endregion Export

        #region TT_ChuSoHuu
        public ResultResponse<List<TT_ChuSoHuuMap>> TT_ChuSoHuu_List(TT_ChuSoHuuParam model)
        {
            var response = new ResponseModel();
            var data = _chuSoHuuRepository.TT_ChuSoHuu_List(model, out response);
            var result = new ResultResponse<List<TT_ChuSoHuuMap>>(response, data);
            return result;
        }
        public ResultResponse<TT_ChuSoHuuAdd> TT_ChuSoHuu_ById(long id)
        {
            var response = new ResponseModel();
            var data = _chuSoHuuRepository.TT_ChuSoHuu_ById(id, out response);
            var result = new ResultResponse<TT_ChuSoHuuAdd>(response, data);
            return result;
        }
        public ResultResponse<int> TT_ChuSoHuu_CheckCCCD(string cccd, long chuSoHuuID)
        {
            var response = new ResponseModel();
            var data = _chuSoHuuRepository.TT_ChuSoHuu_CheckCCCD(cccd, chuSoHuuID, out response);
            var result = new ResultResponse<int>(response, data);
            return result;
        }
        public ResultResponse<long> TT_ChuSoHuu_InsUpd(TT_ChuSoHuuAdd model)
        {
            var response = new ResponseModel();
            var data = _chuSoHuuRepository.TT_ChuSoHuu_InsUpd(model, out response);
            var result = new ResultResponse<long>(response, data);
            return result;
        }
        #endregion TT_ChuSoHuu

        #region TT_TacGia
        public ResultResponse<List<TT_TacGiaMap>> TT_TacGia_List(TT_TacGiaParam model)
        {
            var response = new ResponseModel();
            var data = _tacGiaRepository.TT_TacGia_List(model, out response);
            var result = new ResultResponse<List<TT_TacGiaMap>>(response, data);
            return result;
        }
        public ResultResponse<TT_TacGiaAdd> TT_TacGia_ById(long id)
        {
            var response = new ResponseModel();
            var data = _tacGiaRepository.TT_TacGia_ById(id, out response);
            var result = new ResultResponse<TT_TacGiaAdd>(response, data);
            return result;
        }
        public ResultResponse<int> TT_TacGia_CheckCCCD(string cccd, long tacGiaID)
        {
            var response = new ResponseModel();
            var data = _tacGiaRepository.TT_TacGia_CheckCCCD(cccd, tacGiaID, out response);
            var result = new ResultResponse<int>(response, data);
            return result;
        }
        public ResultResponse<long> TT_TacGia_InsUpd(TT_TacGiaAdd model)
        {
            var response = new ResponseModel();
            var data = _tacGiaRepository.TT_TacGia_InsUpd(model, out response);
            var result = new ResultResponse<long>(response, data);
            return result;
        }
        #endregion TT_TacGia

        #region TT_CongDan
        public ResultResponse<List<TT_CongDanMap>> TT_CongDan_List(TT_CongDanParam model)
        {
            var response = new ResponseModel();
            var data = _congDanRepository.TT_CongDan_List(model, out response);
            var result = new ResultResponse<List<TT_CongDanMap>>(response, data);
            return result;
        }
        public ResultResponse<TT_CongDanAdd> TT_CongDan_ById(long id)
        {
            var response = new ResponseModel();
            var data = _congDanRepository.TT_CongDan_ById(id, out response);
            var result = new ResultResponse<TT_CongDanAdd>(response, data);
            return result;
        }
        public ResultResponse<int> TT_CongDan_CheckCCCD(string cccd, long congDanID)
        {
            var response = new ResponseModel();
            var data = _congDanRepository.TT_CongDan_CheckCCCD(cccd, congDanID, out response);
            var result = new ResultResponse<int>(response, data);
            return result;
        }
        public ResultResponse<int> TT_CongDan_CheckDKKD(string dkkd, long congDanID)
        {
            var response = new ResponseModel();
            var data = _congDanRepository.TT_CongDan_CheckDKKD(dkkd, congDanID, out response);
            var result = new ResultResponse<int>(response, data);
            return result;
        }
        public ResultResponse<long> TT_CongDan_InsUpd(TT_CongDanAdd model)
        {
            var response = new ResponseModel();
            var data = _congDanRepository.TT_CongDan_InsUpd(model, out response);
            var result = new ResultResponse<long>(response, data);
            return result;
        }
        #endregion TT_CongDan

        #region TT_HinhAnh
        public ResultResponse<List<TT_HinhAnhMap>> TT_HinhAnh_List(TT_HinhAnhParam model)
        {
            var response = new ResponseModel();
            var data = _hinhAnhRepository.TT_HinhAnh_List(model, out response);
            var result = new ResultResponse<List<TT_HinhAnhMap>>(response, data);
            return result;
        }
        public ResultResponse<TT_HinhAnhAdd> TT_HinhAnh_ById(long id)
        {
            var response = new ResponseModel();
            var data = _hinhAnhRepository.TT_HinhAnh_ById(id, out response);
            var result = new ResultResponse<TT_HinhAnhAdd>(response, data);
            return result;
        }
        public ResultResponse<long> TT_HinhAnh_InsUpd(TT_HinhAnhAdd model)
        {
            using (var conns = new SqlConnection(Conn))
            {
                conns.Open();
                IDbTransaction trans = conns.BeginTransaction();
                var response = new ResponseModel();
                var data = _hinhAnhRepository.TT_HinhAnh_InsUpd(model, conns, trans, out response);
                if ((long)data < 0)
                {
                    trans.Rollback();
                    conns.Close();
                    return new ResultResponse<long>(response, -1);
                }
                trans.Commit();
                conns.Close();
                var result = new ResultResponse<long>(response, data);
                return result;
            }
        }
        public ResultResponse<int> TT_HinhAnh_Del(long id, Guid userId)
        {
            var response = new ResponseModel();
            var data = _hinhAnhRepository.TT_HinhAnh_Del(id, userId, out response);
            var result = new ResultResponse<int>(response, data);
            return result;
        }
        #endregion TT_HinhAnh

        #region TT_Phim
        public ResultResponse<List<TT_PhimMap>> TT_Phim_List(TT_PhimParam model)
        {
            var response = new ResponseModel();
            var data = _phimRepository.TT_Phim_List(model, out response);
            var result = new ResultResponse<List<TT_PhimMap>>(response, data);
            return result;
        }
        public ResultResponse<TT_PhimAdd> TT_Phim_ById(long id)
        {
            var response = new ResponseModel();
            var data = _phimRepository.TT_Phim_ById(id, out response);
            var result = new ResultResponse<TT_PhimAdd>(response, data);
            return result;
        }
        public ResultResponse<long> TT_Phim_InsUpd(TT_PhimAdd model)
        {
            using (var conns = new SqlConnection(Conn))
            {
                conns.Open();
                IDbTransaction trans = conns.BeginTransaction();
                var response = new ResponseModel();
                var data = _phimRepository.TT_Phim_InsUpd(model, conns, trans, out response);
                if ((long)data < 0)
                {
                    trans.Rollback();
                    conns.Close();
                    return new ResultResponse<long>(response, -1);
                }
                trans.Commit();
                conns.Close();
                var result = new ResultResponse<long>(response, data);
                return result;
            }
        }
        public ResultResponse<int> TT_Phim_Del(long id, Guid userId)
        {
            var response = new ResponseModel();
            var data = _phimRepository.TT_Phim_Del(id, userId, out response);
            var result = new ResultResponse<int>(response, data);
            return result;
        }
        #endregion TT_Phim

        #region TT_DinhKem
        public ResultResponse<List<TT_DinhKemMap>> TT_DinhKem_List(TT_DinhKemParam model)
        {
            var response = new ResponseModel();
            var data = _dinhKemRepository.TT_DinhKem_List(model, out response);
            var result = new ResultResponse<List<TT_DinhKemMap>>(response, data);
            return result;
        }
        public ResultResponse<TT_DinhKemAdd> TT_DinhKem_ById(long id)
        {
            var response = new ResponseModel();
            var data = _dinhKemRepository.TT_DinhKem_ById(id, out response);
            var result = new ResultResponse<TT_DinhKemAdd>(response, data);
            return result;
        }
        public ResultResponse<long> TT_DinhKem_InsUpd(TT_DinhKemAdd model)
        {
            var response = new ResponseModel();
            var data = _dinhKemRepository.TT_DinhKem_InsUpd(model, out response);
            var result = new ResultResponse<long>(response, data);
            return result;
        }
        public ResultResponse<int> TT_DinhKem_Del(long id, Guid userId)
        {
            var response = new ResponseModel();
            var data = _dinhKemRepository.TT_DinhKem_Del(id, userId, out response);
            var result = new ResultResponse<int>(response, data);
            return result;
        }
        #endregion TT_DinhKem

        #region TT_NguoiBieuDien
        public ResultResponse<List<TT_NguoiBieuDienMap>> TT_NguoiBieuDien_List(TT_NguoiBieuDienParam model)
        {
            var response = new ResponseModel();
            var data = _nguoiBieuDienRepository.TT_NguoiBieuDien_List(model, out response);
            var result = new ResultResponse<List<TT_NguoiBieuDienMap>>(response, data);
            return result;
        }
        public ResultResponse<TT_NguoiBieuDienAdd> TT_NguoiBieuDien_ById(long id)
        {
            var response = new ResponseModel();
            var data = _nguoiBieuDienRepository.TT_NguoiBieuDien_ById(id, out response);
            var result = new ResultResponse<TT_NguoiBieuDienAdd>(response, data);
            return result;
        }
        public ResultResponse<int> TT_NguoiBieuDien_CheckCCCD(string cccd, long nguoiBieuDienID)
        {
            var response = new ResponseModel();
            var data = _nguoiBieuDienRepository.TT_NguoiBieuDien_CheckCCCD(cccd, nguoiBieuDienID, out response);
            var result = new ResultResponse<int>(response, data);
            return result;
        }
        public ResultResponse<long> TT_NguoiBieuDien_InsUpd(TT_NguoiBieuDienAdd model)
        {
            var response = new ResponseModel();
            var data = _nguoiBieuDienRepository.TT_NguoiBieuDien_InsUpd(model, out response);
            var result = new ResultResponse<long>(response, data);
            return result;
        }
        #endregion TT_NguoiBieuDien

        #region MAP_ThuTuc_ManHinh
        public ResultResponse<MAP_ThuTuc_ManHinhAdd> MAP_ThuTuc_ManHinh_GetById(long id)
        {
            var response = new ResponseModel();
            var data = _dungChungRepository.MAP_ThuTuc_ManHinh_GetById(id, out response);
            var result = new ResultResponse<MAP_ThuTuc_ManHinhAdd>(response, data);
            return result;
        }
        #endregion

        #region DM_LoaiHinhDangKy
        public ResultResponse<long> DM_LoaiHinhDangKy_InsUpd(DM_LoaiHinhDangKyMapAdd model)
        {
            var response = new ResponseModel();
            var data = _dm_LoaiHinhDangKyRepository.DM_LoaiHinhDangKy_InsUpd(model, out response);
            if (data > 0)
            {
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey("DM_LoaiHinhDangKy", "");
                CacheHelper.Remove(cacheKey);
            }
            return new ResultResponse<long>(response, data);
        }
        public ResultResponse<List<DM_LoaiHinhDangKyMap>> DM_LoaiHinhDangKy_List(DM_LoaiHinhDangKyMapParam model)
        {
            var response = new ResponseModel();
            var data = _dm_LoaiHinhDangKyRepository.DM_LoaiHinhDangKy_List(model, out response);
            var result = new ResultResponse<List<DM_LoaiHinhDangKyMap>>(response, data);
            return result;
        }

        public ResultResponse<DM_LoaiHinhDangKyMapAdd> DM_LoaiHinhDangKy_GetById(long id)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("DM_LoaiHinhDangKy", id.ToString());
            var data = CacheHelper.GetData<DM_LoaiHinhDangKyMapAdd>(cacheKey);
            if (data == null)
            {
                data = _dm_LoaiHinhDangKyRepository.DM_LoaiHinhDangKy_GetById(id, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<DM_LoaiHinhDangKyMapAdd>(response, data);
        }
        public ResultResponse<DM_LoaiHinhDangKyMapAdd> DM_LoaiHinhDangKy_GetByMa(string ma)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("DM_LoaiHinhDangKy", ma.ToString());
            var data = CacheHelper.GetData<DM_LoaiHinhDangKyMapAdd>(cacheKey);
            if (data == null)
            {
                data = _dm_LoaiHinhDangKyRepository.DM_LoaiHinhDangKy_GetByMa(ma, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<DM_LoaiHinhDangKyMapAdd>(response, data);
        }

        public ResultResponse<bool> DM_LoaiHinhDangKy_Delete(long id, Guid userId)
        {
            var response = new ResponseModel();
            var data = _dm_LoaiHinhDangKyRepository.DM_LoaiHinhDangKy_Delete(id, userId, out response);
            if (data)
            {
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey("DM_LoaiHinhDangKy", "");
                CacheHelper.Remove(cacheKey);
            }
            return new ResultResponse<bool>(response, data);
        }
        #endregion DM_LoaiHinhDangKy

        #region DM_LoaiHinhTacPham
        public ResultResponse<long> DM_LoaiHinhTacPham_InsUpd(DM_LoaiHinhTacPhamMapAdd model)
        {
            var response = new ResponseModel();
            var data = _dm_LoaiHinhTacPhamRepository.DM_LoaiHinhTacPham_InsUpd(model, out response);
            if (data > 0)
            {
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey("DM_LoaiHinhTacPham", "");
                CacheHelper.Remove(cacheKey);
            }
            return new ResultResponse<long>(response, data);
        }
        public ResultResponse<List<DM_LoaiHinhTacPhamMap>> DM_LoaiHinhTacPham_List(DM_LoaiHinhTacPhamMapParam model)
        {
            var response = new ResponseModel();
            var data = _dm_LoaiHinhTacPhamRepository.DM_LoaiHinhTacPham_List(model, out response);
            var result = new ResultResponse<List<DM_LoaiHinhTacPhamMap>>(response, data);
            return result;
        }

        public ResultResponse<DM_LoaiHinhTacPhamMapAdd> DM_LoaiHinhTacPham_GetById(long id)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("DM_LoaiHinhTacPham", id.ToString());
            var data = CacheHelper.GetData<DM_LoaiHinhTacPhamMapAdd>(cacheKey);
            if (data == null)
            {
                data = _dm_LoaiHinhTacPhamRepository.DM_LoaiHinhTacPham_GetById(id, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<DM_LoaiHinhTacPhamMapAdd>(response, data);
        }
        public ResultResponse<DM_LoaiHinhTacPhamMapAdd> DM_LoaiHinhTacPham_GetByMa(string ma)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("DM_LoaiHinhTacPham", ma.ToString());
            var data = CacheHelper.GetData<DM_LoaiHinhTacPhamMapAdd>(cacheKey);
            if (data == null)
            {
                data = _dm_LoaiHinhTacPhamRepository.DM_LoaiHinhTacPham_GetByMa(ma, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<DM_LoaiHinhTacPhamMapAdd>(response, data);
        }

        public ResultResponse<bool> DM_LoaiHinhTacPham_Delete(long id, Guid userId)
        {
            var response = new ResponseModel();
            var data = _dm_LoaiHinhTacPhamRepository.DM_LoaiHinhTacPham_Delete(id, userId, out response);
            if (data)
            {
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey("DM_LoaiHinhTacPham", "");
                CacheHelper.Remove(cacheKey);
            }
            return new ResultResponse<bool>(response, data);
        }
        #endregion DM_LoaiHinhTacPham

        #region DM_VungMien
        public ResultResponse<long> DM_VungMien_InsUpd(DM_VungMienMapAdd model)
        {
            var response = new ResponseModel();
            var data = _dm_VungMienRepository.DM_VungMien_InsUpd(model, out response);
            if (data > 0)
            {
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey("DM_VungMien", "");
                CacheHelper.Remove(cacheKey);
            }
            return new ResultResponse<long>(response, data);
        }
        public ResultResponse<List<DM_VungMienMap>> DM_VungMien_List(DM_VungMienMapParam model)
        {
            var response = new ResponseModel();
            var data = _dm_VungMienRepository.DM_VungMien_List(model, out response);
            var result = new ResultResponse<List<DM_VungMienMap>>(response, data);
            return result;
        }

        public ResultResponse<DM_VungMienMapAdd> DM_VungMien_GetById(long id)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("DM_VungMien", id.ToString());
            var data = CacheHelper.GetData<DM_VungMienMapAdd>(cacheKey);
            if (data == null)
            {
                data = _dm_VungMienRepository.DM_VungMien_GetById(id, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<DM_VungMienMapAdd>(response, data);
        }
        public ResultResponse<DM_VungMienMapAdd> DM_VungMien_GetByMa(string ma)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("DM_VungMien", ma.ToString());
            var data = CacheHelper.GetData<DM_VungMienMapAdd>(cacheKey);
            if (data == null)
            {
                data = _dm_VungMienRepository.DM_VungMien_GetByMa(ma, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<DM_VungMienMapAdd>(response, data);
        }

        public ResultResponse<bool> DM_VungMien_Delete(long id, Guid userId)
        {
            var response = new ResponseModel();
            var data = _dm_VungMienRepository.DM_VungMien_Delete(id, userId, out response);
            if (data)
            {
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey("DM_VungMien", "");
                CacheHelper.Remove(cacheKey);
            }
            return new ResultResponse<bool>(response, data);
        }
        #endregion DM_VungMien

        #region DM_CoQuanCap
        public ResultResponse<long> DM_CoQuanCap_InsUpd(DM_CoQuanCapMapAdd model)
        {
            var response = new ResponseModel();
            var data = _dm_CoQuanCapRepository.DM_CoQuanCap_InsUpd(model, out response);
            if (data > 0)
            {
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey("DM_CoQuanCap", "");
                CacheHelper.Remove(cacheKey);
            }
            return new ResultResponse<long>(response, data);
        }
        public ResultResponse<List<DM_CoQuanCapMap>> DM_CoQuanCap_List(DM_CoQuanCapMapParam model)
        {
            var response = new ResponseModel();
            var data = _dm_CoQuanCapRepository.DM_CoQuanCap_List(model, out response);
            var result = new ResultResponse<List<DM_CoQuanCapMap>>(response, data);
            return result;
        }

        public ResultResponse<DM_CoQuanCapMapAdd> DM_CoQuanCap_GetById(long id)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("DM_CoQuanCap", id.ToString());
            var data = CacheHelper.GetData<DM_CoQuanCapMapAdd>(cacheKey);
            if (data == null)
            {
                data = _dm_CoQuanCapRepository.DM_CoQuanCap_GetById(id, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<DM_CoQuanCapMapAdd>(response, data);
        }
        public ResultResponse<DM_CoQuanCapMapAdd> DM_CoQuanCap_GetByMa(string ma)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("DM_CoQuanCap", ma.ToString());
            var data = CacheHelper.GetData<DM_CoQuanCapMapAdd>(cacheKey);
            if (data == null)
            {
                data = _dm_CoQuanCapRepository.DM_CoQuanCap_GetByMa(ma, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<DM_CoQuanCapMapAdd>(response, data);
        }

        public ResultResponse<bool> DM_CoQuanCap_Delete(long id, Guid userId)
        {
            var response = new ResponseModel();
            var data = _dm_CoQuanCapRepository.DM_CoQuanCap_Delete(id, userId, out response);
            if (data)
            {
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey("DM_CoQuanCap", "");
                CacheHelper.Remove(cacheKey);
            }
            return new ResultResponse<bool>(response, data);
        }
        #endregion DM_CoQuanCap

        #region DM_NguoiKy
        public ResultResponse<long> DM_NguoiKy_InsUpd(DM_NguoiKyMapAdd model)
        {
            var response = new ResponseModel();
            var data = _dm_NguoiKyRepository.DM_NguoiKy_InsUpd(model, out response);
            if (data > 0)
            {
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey("DM_NguoiKy", "");
                CacheHelper.Remove(cacheKey);
            }
            return new ResultResponse<long>(response, data);
        }
        public ResultResponse<List<DM_NguoiKyMap>> DM_NguoiKy_List(DM_NguoiKyMapParam model)
        {
            var response = new ResponseModel();
            var data = _dm_NguoiKyRepository.DM_NguoiKy_List(model, out response);
            var result = new ResultResponse<List<DM_NguoiKyMap>>(response, data);
            return result;
        }

        public ResultResponse<DM_NguoiKyMapAdd> DM_NguoiKy_GetById(long id)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("DM_NguoiKy", id.ToString());
            var data = CacheHelper.GetData<DM_NguoiKyMapAdd>(cacheKey);
            if (data == null)
            {
                data = _dm_NguoiKyRepository.DM_NguoiKy_GetById(id, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<DM_NguoiKyMapAdd>(response, data);
        }
        public ResultResponse<DM_NguoiKyMapAdd> DM_NguoiKy_GetByMa(string ma)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("DM_NguoiKy", ma.ToString());
            var data = CacheHelper.GetData<DM_NguoiKyMapAdd>(cacheKey);
            if (data == null)
            {
                data = _dm_NguoiKyRepository.DM_NguoiKy_GetByMa(ma, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<DM_NguoiKyMapAdd>(response, data);
        }

        public ResultResponse<bool> DM_NguoiKy_Delete(long id, Guid userId)
        {
            var response = new ResponseModel();
            var data = _dm_NguoiKyRepository.DM_NguoiKy_Delete(id, userId, out response);
            if (data)
            {
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey("DM_NguoiKy", "");
                CacheHelper.Remove(cacheKey);
            }
            return new ResultResponse<bool>(response, data);
        }
        #endregion DM_NguoiKy

        #region DM_LoaiSo
        public ResultResponse<long> DM_LoaiSo_InsUpd(DM_LoaiSoMapAdd model)
        {
            var response = new ResponseModel();
            var data = _dm_LoaiSoRepository.DM_LoaiSo_InsUpd(model, out response);
            if (data > 0)
            {
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey("DM_LoaiSo", "");
                CacheHelper.Remove(cacheKey);
            }
            return new ResultResponse<long>(response, data);
        }
        public ResultResponse<List<DM_LoaiSoMap>> DM_LoaiSo_List(DM_LoaiSoMapParam model)
        {
            var response = new ResponseModel();
            var data = _dm_LoaiSoRepository.DM_LoaiSo_List(model, out response);
            var result = new ResultResponse<List<DM_LoaiSoMap>>(response, data);
            return result;
        }

        public ResultResponse<DM_LoaiSoMapAdd> DM_LoaiSo_GetById(long id)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("DM_LoaiSo", id.ToString());
            var data = CacheHelper.GetData<DM_LoaiSoMapAdd>(cacheKey);
            if (data == null)
            {
                data = _dm_LoaiSoRepository.DM_LoaiSo_GetById(id, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<DM_LoaiSoMapAdd>(response, data);
        }
        public ResultResponse<DM_LoaiSoMapAdd> DM_LoaiSo_GetByLoaiNghieVuId(long loaiNghiepVuId)
        {
            var response = new ResponseModel();
            string cacheKey = string.Empty;
            cacheKey = CacheHelper.BuildKey("DM_LoaiSo", loaiNghiepVuId.ToString());
            var data = CacheHelper.GetData<DM_LoaiSoMapAdd>(cacheKey);
            if (data == null)
            {
                data = _dm_LoaiSoRepository.DM_LoaiSo_GetByLoaiNghieVuId(loaiNghiepVuId, out response);
                if (data != null)
                    CacheHelper.SetData(cacheKey, data);
            }
            return new ResultResponse<DM_LoaiSoMapAdd>(response, data);
        }

        public ResultResponse<bool> DM_LoaiSo_Delete(long id)
        {
            var response = new ResponseModel();
            var data = _dm_LoaiSoRepository.DM_LoaiSo_Delete(id, out response);
            if (data)
            {
                string cacheKey = string.Empty;
                cacheKey = CacheHelper.BuildKey("DM_LoaiSo", "");
                CacheHelper.Remove(cacheKey);
            }
            return new ResultResponse<bool>(response, data);
        }
        #endregion DM_LoaiSo
    }
}
