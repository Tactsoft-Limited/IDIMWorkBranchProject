using System;
using System.Web.Mvc;
using Autofac;
using AutoMapper;
using IDIMWorkBranchProject.Services.Setup;
using IDIMWorkBranchProject.Services.User;
using Autofac.Integration.Mvc;
using IDIMWorkBranchProject.Services.Dashboard;
using IDIMWorkBranchProject.Services.WBP;

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

            builder.Register(c => mapperConfig.CreateMapper())
                .Named<IMapper>("IDIM")
                .SingleInstance();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Register dependencies in filter attributes
            builder.RegisterFilterProvider();

            // Register dependencies in custom views
            builder.RegisterSource(new ViewRegistrationSource());

            #region wbp
            builder.Register(c => new DashboardService(c.ResolveNamed<IMapper>("IDIM"))).As<IDashboardService>().InstancePerLifetimeScope();
            builder.Register(c => new ProjectService(c.ResolveNamed<IMapper>("IDIM"))).As<IProjectService>().InstancePerLifetimeScope();
            builder.Register(c => new SubProjectService(c.ResolveNamed<IMapper>("IDIM"))).As<ISubProjectService>().InstancePerLifetimeScope();
            builder.Register(c => new BillPaymentService(c.ResolveNamed<IMapper>("IDIM"))).As<IBillPaymentService>().InstancePerLifetimeScope();
            builder.Register(c => new ProjectExtendService(c.ResolveNamed<IMapper>("IDIM"))).As<IProjectExtendService>().InstancePerLifetimeScope();
            builder.Register(c => new ProjectProblemService(c.ResolveNamed<IMapper>("IDIM"))).As<IProjectProblemService>().InstancePerLifetimeScope();
            builder.Register(c => new ProjectStatusService(c.ResolveNamed<IMapper>("IDIM"))).As<IProjectStatusService>().InstancePerLifetimeScope();
            builder.Register(c => new ReceiptPaymentService(c.ResolveNamed<IMapper>("IDIM"))).As<IReceiptPaymentService>().InstancePerLifetimeScope();
            builder.Register(c => new ConsultantService(c.ResolveNamed<IMapper>("IDIM"))).As<IConsultantService>().InstancePerLifetimeScope();
            builder.Register(c => new SubProjectDetailsService(c.ResolveNamed<IMapper>("IDIM"))).As<ISubProjectDetailsService>().InstancePerLifetimeScope();
            builder.Register(c => new ConsultancyFeesService(c.ResolveNamed<IMapper>("IDIM"))).As<IConsultancyFeesService>().InstancePerLifetimeScope();
            builder.Register(c => new ConsultancyActivitiesService(c.ResolveNamed<IMapper>("IDIM"))).As<IConsultancyActivitiesService>().InstancePerLifetimeScope();
            builder.Register(c => new SecurityDepositService(c.ResolveNamed<IMapper>("IDIM"))).As<ISecurityDepositService>().InstancePerLifetimeScope();
            #endregion

            #region setup
            builder.Register(c => new FiscalYearService(c.ResolveNamed<IMapper>("IDIM"))).As<IFiscalYearService>().InstancePerLifetimeScope();
            builder.Register(c => new BillTypeService(c.ResolveNamed<IMapper>("IDIM"))).As<IBillTypeService>().InstancePerLifetimeScope();
            builder.Register(c => new QuarterService(c.ResolveNamed<IMapper>("IDIM"))).As<IQuarterService>().InstancePerLifetimeScope();
            builder.Register(c => new ConstructionFirmService(c.ResolveNamed<IMapper>("IDIM"))).As<IConstructionFirmService>().InstancePerLifetimeScope();
            builder.Register(c => new UnitService(c.ResolveNamed<IMapper>("IDIM"))).As<IUnitService>().InstancePerLifetimeScope();
            #endregion

            #region security
            builder.Register(c => new ApplicationService(c.ResolveNamed<IMapper>("IDIM"))).As<IApplicationService>().InstancePerLifetimeScope();
            builder.Register(c => new DeviceService(c.ResolveNamed<IMapper>("IDIM"))).As<IDeviceService>().InstancePerLifetimeScope();
            builder.Register(c => new GeneralInformationService(c.ResolveNamed<IMapper>("IDIM"))).As<IGeneralInformationService>().InstancePerLifetimeScope();
            builder.Register(c => new MenuService(c.ResolveNamed<IMapper>("IDIM"))).As<IMenuService>().InstancePerLifetimeScope();
            builder.Register(c => new UserService(c.ResolveNamed<IMapper>("IDIM"))).As<IUserService>().InstancePerLifetimeScope();
            builder.Register(c => new UserPriviledgeService(c.ResolveNamed<IMapper>("IDIM"))).As<IUserPriviledgeService>().InstancePerLifetimeScope();
            builder.Register(c => new UserApplicationService(c.ResolveNamed<IMapper>("IDIM"))).As<IUserApplicationService>().InstancePerLifetimeScope();
            #endregion
        }
    }
}
