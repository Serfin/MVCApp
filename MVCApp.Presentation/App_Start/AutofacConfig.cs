using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using MVCApp.Common.Mappings;
using MVCApp.Infrastructure.Interfaces;
using MVCApp.Data.EntityFramework;
using MVCApp.Infrastructure.CommandHandlers;
using MVCApp.Infrastructure.Services;
using MVCApp.Presentation.Controllers;

namespace MVCApp.Presentation
{
    public static class AutofacConfig
    {
        public static void Initialize()
        {
            var builder = new ContainerBuilder();

            // Register controllers

            builder.RegisterControllers(typeof(HomeController).Assembly);

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

            builder.RegisterAssemblyTypes(infrastructureAssembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();

            // Register single instances

            builder.RegisterType<Encrypter>()
                .As<IEncrypter>()
                .SingleInstance();

            builder.RegisterInstance(AutoMapperConfiguration.Initialize())
                .SingleInstance();

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