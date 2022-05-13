using Ninject;
using Ninject.Modules;
using Ninject.Parameters;

namespace Core.Common.UI
{
    public class NinjectFactory
    {
        private static readonly IKernel Kernel = new StandardKernel(new DataModuleLoader());

        public static T Get<T>(params IParameter[] param)
        {
            return Kernel.Get<T>(param);
        }

        public class DataModuleLoader : NinjectModule
        {
            public override void Load()
            {
                Bind<IPageHeadBuilder>().To<PageHeadBuilder>();
            }
        }
    }
}
