using Autofac;
using Business.Services;
using Business.Services.Interfaces;
using Data.Core.Repositories;
using Data.Core.Repositories.Interfaces;
using log4net;

namespace Host.WcfService
{
    public class MainModule
    {
        public static Autofac.IContainer BuildContainer()
        {
            log4net.Config.XmlConfigurator.Configure();
            var builder = new ContainerBuilder();

            // register services
            builder.RegisterType<TraCuuService>().As<ITraCuuService>();

            // register repositories & log4net
            builder.Register(log => LogManager.GetLogger(typeof(MainModule))).SingleInstance();

            // register Repository
            builder.RegisterType<TC_GiayChungNhanRepository>().As<ITC_GiayChungNhanRepository>();

            return builder.Build();
        }
    }
}