using Autofac;
using Business.Services;
using Business.Services.Interfaces;
using Data.Core.Repositories;
using Data.Core.Repositories.Base;
using Data.Core.Repositories.Interfaces;
using log4net;

namespace Host.WcfService
{
    public class MainModule
    {
        public static IContainer BuildContainer()
        {
            log4net.Config.XmlConfigurator.Configure();
            var builder = new ContainerBuilder();

            // register services
            builder.RegisterType<AuthService>().As<IAuthService>();
            builder.RegisterType<DungChungService>().As<IDungChungService>();
            builder.RegisterType<LoggingService>().As<ILoggingService>();
            builder.RegisterType<UsersService>().As<IUsersService>();
            builder.RegisterType<QTG_QuyenTacGiaService>().As<IQTG_QuyenTacGiaService>();
            builder.RegisterType<HS_CapSoService>().As<IHS_CapSoService>();
            builder.RegisterType<BC_ThongKeService>().As<IBC_ThongKeService>();
            builder.RegisterType<QLQ_QuyenLienQuanService>().As<IQLQ_QuyenLienQuanService>();
            builder.RegisterType<DVC_QuyenTacGiaService>().As<IDVC_QuyenTacGiaService>();
            builder.RegisterType<DVC_QuyenLienQuanService>().As<IDVC_QuyenLienQuanService>();

            // register repositories & log4net
            builder.Register(log => LogManager.GetLogger(typeof(MainModule))).SingleInstance();

            // builder.RegisterInstance(LogManager.GetLogger(typeof(MainModule))).As<ILog>();         
            builder.RegisterType<DM_NhomQuyenRepository>().As<IDM_NhomQuyenRepository>();
            builder.RegisterType<DM_QuocGiaRepository>().As<IDM_QuocGiaRepository>();
            builder.RegisterType<DM_TinhThanhRepository>().As<IDM_TinhThanhRepository>();
            builder.RegisterType<DM_QuyenRepository>().As<IDM_QuyenRepository>();
            builder.RegisterType<DM_QuyenChucNangRepository>().As<IDM_QuyenChucNangRepository>();
            builder.RegisterType<DM_QuanHuyenRepository>().As<IDM_QuanHuyenRepository>();
            builder.RegisterType<DM_PhuongXaRepository>().As<IDM_PhuongXaRepository>();
            builder.RegisterType<DM_DungChungRepository>().As<IDM_DungChungRepository>();
            builder.RegisterType<DefaultLogger>().As<ILogger>();
            builder.RegisterType<DungChungRepository>().As<IDungChungRepository>();
            builder.RegisterType<CauHinhHeThongRepository>().As<ICauHinhHeThongRepository>();          
            builder.RegisterType<LogRepository>().As<ILogRepository>();          
            builder.RegisterType<NhatKyNguoiDungRepository>().As<INhatKyNguoiDungRepository>();
            builder.RegisterType<NguoiDungHeThongRepository>().As<INguoiDungHeThongRepository>();
            builder.RegisterType<WebHelper>().As<IWebHelper>();
            builder.RegisterType<TT_ChuSoHuuRepository>().As<ITT_ChuSoHuuRepository>();
            builder.RegisterType<TT_TacGiaRepository>().As<ITT_TacGiaRepository>();
            builder.RegisterType<TT_PhimRepository>().As<ITT_PhimRepository>();
            builder.RegisterType<TT_HinhAnhRepository>().As<ITT_HinhAnhRepository>();
            builder.RegisterType<QTG_QuyenTacGiaRepository>().As<IQTG_QuyenTacGiaRepository>();
            builder.RegisterType<TT_CapQuyenRepository>().As<ITT_CapQuyenRepository>();
            builder.RegisterType<DM_LoaiSoRepository>().As<IDM_LoaiSoRepository>();
            builder.RegisterType<HS_CapSoRepository>().As<IHS_CapSoRepository>();
            builder.RegisterType<TT_DinhKemRepository>().As<ITT_DinhKemRepository>();
            builder.RegisterType<BC_ThongKeRepository>().As<IBC_ThongKeRepository>();
            builder.RegisterType<QLQ_QuyenLienQuanRepository>().As<IQLQ_QuyenLienQuanRepository>();
            builder.RegisterType<TT_NguoiBieuDienRepository>().As<ITT_NguoiBieuDienRepository>();
            builder.RegisterType<DM_LoaiHinhDangKyRepository>().As<IDM_LoaiHinhDangKyRepository>();
            builder.RegisterType<DM_VungMienRepository>().As<IDM_VungMienRepository>();
            builder.RegisterType<DM_LoaiHinhTacPhamRepository>().As<IDM_LoaiHinhTacPhamRepository>();
            builder.RegisterType<DM_CoQuanCapRepository>().As<IDM_CoQuanCapRepository>();
            builder.RegisterType<DM_NguoiKyRepository>().As<IDM_NguoiKyRepository>();
            builder.RegisterType<DVC_QuyenLienQuanRepository>().As<IDVC_QuyenLienQuanRepository>();
            builder.RegisterType<DVC_QuyenTacGiaRepository>().As<IDVC_QuyenTacGiaRepository>();
            builder.RegisterType<TT_CongDanRepository>().As<ITT_CongDanRepository>();
            return builder.Build();
        }
    }
}