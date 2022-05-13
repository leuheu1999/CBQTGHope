using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Business.Entities.Domain;
using Core.Common.Utilities;
using Data.Core.Repositories.Base;
using PagedList;

namespace Data.Core.Repositories.Interfaces
{
   public interface INguoiDungHeThongRepository : IRepository<NguoiDungHeThongMap>
    {
        List<NguoiDungHeThongMap> NguoiDungHeThong_List(NguoiDungHeThongMapParam model, out ResponseModel restStatus);
        NguoiDungHeThongMapAdd NguoiDungHeThong_GetById(long Id, out ResponseModel restStatus);
        NguoiDungHeThongMapAdd NguoiDungHeThong_GetByGuid(Guid Id, out ResponseModel restStatus);
        NguoiDungHeThongMapAdd NguoiDungHeThong_GetByTenTaiKhoan(string TenTaiKhoan, out ResponseModel restStatus);
        NguoiDungHeThongMapAdd NguoiDungHeThong_GetBySDT(string SDT, out ResponseModel restStatus);
        NguoiDungHeThongMapAdd NguoiDungHeThong_GetByEmail(string Email, out ResponseModel restStatus);
        int NguoiDungHeThong_CheckMCUserId(long userId, out ResponseModel restStatus);
        Guid NguoiDungHeThong_InsUpd(NguoiDungHeThongMapAdd model, out ResponseModel restStatus);
        int NguoiDungHeThong_Del(Guid id, Guid lastupduserid, out ResponseModel restStatus);
        int NguoiDungHeThong_ChangePass(NguoiDungHeThongChagePass model, out ResponseModel restStatus);
        long NguoiDungHeThong_Khoa(Guid UserID, bool Khoa, out ResponseModel restStatus);
        List<DM_NhomQuyenAdd> NguoiDungHeThong_NhomQuyen(Guid TaiKhoanGuid, out ResponseModel restStatus);
        long NguoiDungHeThong_NhomQuyen_Insert(NguoiDungHeThong_NhomQuyenMapAdd model, out ResponseModel restStatus);
        List<PQ_PhanQuyenChucNang> PQ_PhanQuyenChucNang_ByNhomQuyenID(long NhomQuyenID, out ResponseModel restStatus);
        List<PQ_DanhSachNhomQuyen> PQ_DanhSachNhomQuyen(out ResponseModel restStatus);
        List<DM_QuyenChucNangMap> PQ_QuyenChucNang_List(out ResponseModel restStatus);

        long PhanQuyenChucNang_InsUpd(PQ_PhanQuyenChucNang model, IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        int PQ_QuyenChucNang_Del(IDbConnection conns, IDbTransaction trans, out ResponseModel restStatus);
        string GetSalt(string username);
        NguoiDungHeThongMapAdd GetUserLogin(string userName, string password, out ResponseModel restStatus);
        bool IsActive(string userName, out ResponseModel restStatus);
        List<BanIPMap> BanIP_List(out ResponseModel restStatus);
        BanIPMap BanIP_byIp(string ip, out ResponseModel restStatus);
        int BanIP_deletebyIp(string ip, out ResponseModel restStatus);
        long BanIP_InsUpd(BanIPMap model, out ResponseModel restStatus);
        List<Roles> GetRolesByUser(string username, out ResponseModel restStatus);
        List<Right> GetRightsByUser(string username, out ResponseModel restStatus);// lấy danh sách quyền chức năng của user
        UrlViewModel GetUrlViewModel(string DSQuyenChucNang, out ResponseModel restStatus);// lấy đương dẫn mực định thuộc ds quyền chức năng
        int ND_NguoiDungHeThong_UpdateToken(Guid UserID, string Token, out ResponseModel restStatus);
        string ND_NguoiDungHeThong_UpdateConnectionSignalR(Guid UserID, string ConnectionId, bool IsOnline, out ResponseModel restStatus);
        int ND_NguoiDungHeThong_CountOnline(out ResponseModel restStatus);
    }
}
