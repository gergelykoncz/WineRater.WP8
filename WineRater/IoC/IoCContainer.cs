using Ninject;
using WineRater.Data;
using WineRater.Facade;
using WineRater.ViewModels;

namespace WineRater.IoC
{
    public static class IoCContainer
    {
        private static IKernel kernel;

        public static void Initialize()
        {
            if (kernel == null)
            {
                kernel = new StandardKernel();
                //Application architecture
                kernel.Bind<WineDataContext>().To<WineDataContext>().InSingletonScope();
                kernel.Bind<IWineRepository>().To<WineRepository>().InSingletonScope();
                kernel.Bind<IWineFacade>().To<WineFacade>().InSingletonScope();

                //View models
                kernel.Bind<WineListViewModel>().To<WineListViewModel>();
                kernel.Bind<WineDetailsViewModel>().To<WineDetailsViewModel>();
            }
        }

        public static T Get<T>()
        {
            return kernel.Get<T>();
        }
    }
}
