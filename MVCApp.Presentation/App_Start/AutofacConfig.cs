using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using MVCApp.Core.Repositories;
using MVCApp.Data.EntityFramework;
using MVCApp.Infrastructure.Services;

namespace MVCApp.Presentation
{
    public static class AutofacConfig
    {
        public static void Initialize()
        {
            var builder = new ContainerBuilder();

            // Register controllers

            builder.RegisterControllers(typeof(BundleConfig).Assembly);

            // Register types

            var infrastructureAssembly = typeof(UserService)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(infrastructureAssembly)
                .Where(x => x.IsAssignableTo<IRepository>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(infrastructureAssembly)
                .Where(x => x.IsAssignableTo<IService>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            // Register Entity Framework

            builder.RegisterType<MVCAppContext>()
                .AsSelf()
                .InstancePerLifetimeScope();

            // Resolve dependency

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}