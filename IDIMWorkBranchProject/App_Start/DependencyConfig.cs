using System;
using System.Web.Mvc;
using Autofac;
using AutoMapper;
using Autofac.Integration.Mvc;
using System.Reflection;
using IDIMWorkBranchProject.Data.Database;

namespace IDIMWorkBranchProject
{
    public static class DependencyConfig
    {
        private static readonly Lazy<IContainer> Container = new Lazy<IContainer>(() =>
        {
            var builder = new ContainerBuilder();

            Configure(builder);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            return container;
        });

        public static IContainer GetConfiguredContainer()
        {
            return Container.Value;
        }

        private static void Configure(ContainerBuilder builder)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;

                // Auto-mapper profiles
                cfg.AddProfile<MapperConfig>();
            });

            builder.Register(c => mapperConfig.CreateMapper()).SingleInstance();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Register dependencies in filter attributes
            builder.RegisterFilterProvider();
            builder.RegisterType<IDIMDBEntities>().AsSelf();

            // Register dependencies in custom views
            builder.RegisterSource(new ViewRegistrationSource());
            var dataAccess = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(dataAccess).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();

        }
    }
}
