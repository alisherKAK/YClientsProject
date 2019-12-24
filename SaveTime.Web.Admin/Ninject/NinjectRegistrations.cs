using AutoMapper;
using Ninject.Modules;
using SaveTime.AbstractModels;
using SaveTime.DataAccess;
using SaveTime.Services;
using SaveTime.Web.Admin.AutoMapper;

namespace SaveTime.Web.Admin.Ninject
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            this.Bind(typeof(IRepository<>)).To(typeof(DbRepository<>)).WithConstructorArgument("context", new YClientsContext());
            this.Bind<IMapper>().ToMethod(context => AutoMapperConfiguration.Config());
            this.Bind<IEncrypter>().To<Hashing>();
        }
    }
}