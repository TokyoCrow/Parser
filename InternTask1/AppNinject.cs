using InternTask1.Services.Abstract;
using InternTask1.Services.Concrete;
using Ninject.Modules;

namespace InternTask1
{
    public class AppNinject : NinjectModule
    {
        public override void Load()
        {
            Bind<IParser>().To<Parser>();
            Bind<IRepository>().To<CsvFile>();
            Bind<ISendEmail>().To<InboxMailRU>();
            Bind<IConfiguration>().To<ConfigFile>();
        }
    }
}
