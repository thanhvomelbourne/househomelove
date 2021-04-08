using Autofac;
using Autofac.Integration.Mvc;
using BusinessService.Service;
using CoreLibrary.Data.Entity;
using CoreLibrary.Data.Interface;
using Database.Model;
using System;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using UserService.RoleService;
using UserService.UserService;

namespace OnlineStore.App_Start
{
    public class IocConfig
    {
        public static void Config()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.Register<Func<IPrincipal>>(c => () => HttpContext.Current.User);

            builder.RegisterType(typeof(UnitOfWorkFactory<UnitOfWork, Entities>)).As(
                typeof(IUnitOfWorkFactory<UnitOfWork>))
                .SingleInstance().WithParameter("connectionStringName", "Entities");

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Business"))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(IService).Assembly).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(IRoleService).Assembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces();

            builder.RegisterType(typeof(SimpleMembershipUserService<UserProfile>))
                   .As(typeof(IUserService<UserProfile, int>)).SingleInstance().PreserveExistingDefaults();

            //builder.RegisterAssemblyTypes(typeof(IEmailService).Assembly).AsImplementedInterfaces();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}