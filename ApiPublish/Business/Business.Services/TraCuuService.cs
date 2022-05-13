using Business.Entities.DataModel;
using Business.Services.Interfaces;
using Core.Common.Utilities;
using Data.Core.Repositories.Interfaces;
using log4net;
using System.Collections.Generic;

namespace Business.Services
{
    public class TraCuuService : ITraCuuService
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(TraCuuService));
        private readonly ITC_GiayChungNhanRepository _giayChungNhanRepository;

        public TraCuuService(ITC_GiayChungNhanRepository giayChungNhanRepository)
        {
            _giayChungNhanRepository = giayChungNhanRepository;
        }

        public ResultResponse<List<TC_GiayChungNhanMap>> TC_GiayChungNhan_List(TC_GiayChungNhanMapParam model)
        {
            ResponseModel resStatus;
            var data = _giayChungNhanRepository.TC_GiayChungNhan_List(model, out resStatus);
            if(data != null && data.Count > 0)
            {
                foreach(var item in data)
                {
                    if(item.LoaiDangKyID == 1)
                    {
                        var _dataSoHuu = _giayChungNhanRepository.QTG_ThongTinSoHuu_ByQuyenId(item.QuyenID, out resStatus);
                        if(_dataSoHuu != null && _dataSoHuu.Count > 0)
                        {
                            foreach(var soHuu in _dataSoHuu)
                            {
                                item.StrTacGia += soHuu.StrTacGia;
                                item.StrChuSoHuu += soHuu.StrChuSoHuu;
                            }
                        }
                    }
                    else if (item.LoaiDangKyID == 2)
                    {
                        var _dataSoHuu = _giayChungNhanRepository.QLQ_ThongTinSoHuu_ByQuyenId(item.QuyenID, out resStatus);
                        if (_dataSoHuu != null && _dataSoHuu.Count > 0)
                        {
                            foreach (var soHuu in _dataSoHuu)
                            {
                                item.StrNguoiBieuDien += soHuu.StrNguoiBieuDien;
                                item.StrChuSoHuu += soHuu.StrChuSoHuu;
                            }
                        }
                    }
                }
            }
            return new ResultResponse<List<TC_GiayChungNhanMap>>
            {
                resultObject = data,
                description = resStatus.description,
                resultType = resStatus.resultType,
                status = resStatus.status,
                StatusCode = resStatus.StatusCode,
                throwException = resStatus.throwException
            };
        }

        public ResultResponse<TC_GiayChungNhanAdd> TC_GiayChungNhan_GetDetail(TC_GiayChungNhanAddParam model)
        {
            ResponseModel resStatus;
            var data = _giayChungNhanRepository.TC_GiayChungNhan_GetDetail(model, out resStatus);
            if (data != null)
            {
                if (data.LoaiDangKyID == 1)
                {
                    var _dataSoHuu = _giayChungNhanRepository.QTG_ThongTinSoHuu_ByQuyenId(data.QuyenID, out resStatus);
                    if (_dataSoHuu != null && _dataSoHuu.Count > 0)
                    {
                        foreach (var soHuu in _dataSoHuu)
                        {
                            data.StrTacGia += soHuu.StrTacGia;
                            data.StrChuSoHuu += soHuu.StrChuSoHuu;
                        }
                    }
                }
                else if(data.LoaiDangKyID == 2)
                {
                    var _dataSoHuu = _giayChungNhanRepository.QLQ_ThongTinSoHuu_ByQuyenId(data.QuyenID, out resStatus);
                    if (_dataSoHuu != null && _dataSoHuu.Count > 0)
                    {
                        foreach (var soHuu in _dataSoHuu)
                        {
                            data.StrNguoiBieuDien += soHuu.StrNguoiBieuDien;
                            data.StrChuSoHuu += soHuu.StrChuSoHuu;
                        }
                    }
                }
            }
            return new ResultResponse<TC_GiayChungNhanAdd>
            {
                resultObject = data,
                description = resStatus.description,
                resultType = resStatus.resultType,
                status = resStatus.status,
                StatusCode = resStatus.StatusCode,
                throwException = resStatus.throwException
            };
        }

        public ResultResponse<List<TC_GiayChungNhanCongBaoMap>> TC_GiayChungNhanCongBao_List(TC_GiayChungNhanCongBaoParam model)
        {
            ResponseModel resStatus;
            var data = _giayChungNhanRepository.TC_GiayChungNhanCongBao_List(model, out resStatus);
            if (data != null && data.Count > 0)
            {
                foreach (var item in data)
                {
                    if (item.LoaiDangKyID == 1)
                    {
                        var _dataSoHuu = _giayChungNhanRepository.QTG_ThongTinSoHuu_ByQuyenId(item.QuyenID, out resStatus);
                        if (_dataSoHuu != null && _dataSoHuu.Count > 0)
                        {
                            foreach (var soHuu in _dataSoHuu)
                            {
                                item.StrTacGia += soHuu.StrTacGia;
                                item.StrChuSoHuu += soHuu.StrChuSoHuu;
                            }
                        }
                    }
                    else if (item.LoaiDangKyID == 2)
                    {
                        var _dataSoHuu = _giayChungNhanRepository.QLQ_ThongTinSoHuu_ByQuyenId(item.QuyenID, out resStatus);
                        if (_dataSoHuu != null && _dataSoHuu.Count > 0)
                        {
                            foreach (var soHuu in _dataSoHuu)
                            {
                                item.StrNguoiBieuDien += soHuu.StrNguoiBieuDien;
                                item.StrChuSoHuu += soHuu.StrChuSoHuu;
                            }
                        }
                    }
                }
            }
            return new ResultResponse<List<TC_GiayChungNhanCongBaoMap>>
            {
                resultObject = data,
                description = resStatus.description,
                resultType = resStatus.resultType,
                status = resStatus.status,
                StatusCode = resStatus.StatusCode,
                throwException = resStatus.throwException
            };
        }
    }
}
