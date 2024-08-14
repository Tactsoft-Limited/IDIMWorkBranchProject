using AutoMapper;
using BGB.Data.Entities.Admin;
using BGB.Data.Entities.Budget;
using BGB.Data.Entities.Irms;
using BGB.Data.Entities.Pm;
using IDIMWorkBranchProject.Models.Setup;
using IDIMWorkBranchProject.Models.User;
using IDIMWorkBranchProject.Models.WBP;

namespace IDIMWorkBranchProject
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            #region wbp 
            CreateMap<ProjectVm, Project>();
            // .ForMember(d => d.SetupUnit, opts => opts.Ignore());

            CreateMap<Project, ProjectVm>()
                .ForMember(d => d.AuthorizeUnitDropdown, opts => opts.Ignore())
                .ForMember(d => d.FiscalYearDropdown, opts => opts.Ignore());
            // .ForMember(d => d.FiscalYearName, opts => opts.MapFrom(d=>d.FiscalYear.FiscalYearDescription));

            CreateMap<SubProjectVm, SubProject>()
                .ForMember(d => d.IrmsSetupUnit, opts => opts.Ignore());
            CreateMap<SubProject, SubProjectVm>()
                .ForMember(d => d.UnitDropdown, opts => opts.Ignore())
                .ForMember(d => d.ConstructionFirmDropdown, opts => opts.Ignore())
                .ForMember(d => d.ProjectName, opts => opts.MapFrom(src => src.Project.ProjectName))
                .ForMember(d => d.UnitName, opts => opts.MapFrom(src => src.IrmsSetupUnit.UnitName))
                .ForMember(d => d.ConstructionFirmName, opts => opts.MapFrom(src => src.ConstructionFirm.ConstructionFirmName));

            CreateMap<BillPaymentVm, BillPayment>()
                .ForMember(d => d.SubProject, opts => opts.Ignore());
            CreateMap<BillPayment, BillPaymentVm>()
                .ForMember(d => d.BillTypeDropdown, opts => opts.Ignore())
                .ForMember(d => d.FiscalYearDropdown, opts => opts.Ignore())
                .ForMember(d => d.SubProjectTitle, opts => opts.MapFrom(src => src.SubProject.SubProjectTitle));

            CreateMap<ProjectExtendVm, ProjectExtend>()
                .ForMember(d => d.SubProject, opts => opts.Ignore());

            CreateMap<ProjectExtend, ProjectExtendVm>()
                .ForMember(d => d.SubProjectTitle, opts => opts.MapFrom(src => src.SubProject.SubProjectTitle));

            CreateMap<ProjectProblemVm, ProjectProblem>();
            CreateMap<ProjectProblem, ProjectProblemVm>()
                .ForMember(d => d.SubProjectTitle, opts => opts.MapFrom(src => src.SubProject.SubProjectTitle));

            CreateMap<ProjectStatusVm, ProjectStatus>();
            CreateMap<ProjectStatus, ProjectStatusVm>()
                .ForMember(d => d.SubProjectTitle, opts => opts.MapFrom(src => src.SubProject.SubProjectTitle));

            CreateMap<ReceiptPaymentVm, ReceiptPayment>();
            CreateMap<ReceiptPayment, ReceiptPaymentVm>()
                .ForMember(d => d.FiscalYearDropdown, opts => opts.Ignore())
                .ForMember(d => d.QuarterDropdown, opts => opts.Ignore())
                .ForMember(d => d.ProjectName, opts => opts.MapFrom(src => src.Project.ProjectName))
                .ForMember(d => d.FiscalYearName, opts => opts.MapFrom(src => src.FiscalYear.FiscalYearDescription))
                .ForMember(d => d.QuarterName, opts => opts.MapFrom(src => src.Quarter.QuarterName));

            CreateMap<ConsultantVm, Consultant>();

            CreateMap<SubProjectDetailsVm, SubProjectDetail>();
            CreateMap<SubProjectDetail, SubProjectDetailsVm>()
                .ForMember(d => d.SubProjectDropdown, opts => opts.Ignore())
                .ForMember(d => d.SubProjectTitle, opts => opts.MapFrom(src => src.SubProject.SubProjectTitle));

            CreateMap<ConsultancyFeesVm, ConsultancyFee>();
            CreateMap<ConsultancyFee, ConsultancyFeesVm>()
                .ForMember(d => d.SubProjectDropdown, opts => opts.Ignore())
                .ForMember(d => d.ConsultantDropdown, opts => opts.Ignore())
                .ForMember(d => d.SubProjectTitle, opts => opts.MapFrom(src => src.SubProject.SubProjectTitle))
                .ForMember(d => d.ConsultantName, opts => opts.MapFrom(src => src.Consultant.Name))
                .ForMember(d => d.ConsultancyFee, opts => opts.MapFrom(src => src.ConsultancyFee1));

            CreateMap<ConsultancyActivitiesVm, ConsultancyActivity>();
            CreateMap<ConsultancyActivity, ConsultancyActivitiesVm>()
                .ForMember(d => d.SubProjectDropdown, opts => opts.Ignore())
                .ForMember(d => d.ConsultantDropdown, opts => opts.Ignore())
                .ForMember(d => d.SubProjectTitle, opts => opts.MapFrom(src => src.SubProject.SubProjectTitle))
                .ForMember(d => d.ConsultantName, opts => opts.MapFrom(src => src.Consultant.Name));

            CreateMap<SecurityDepositVm, PmSecurityDeposit>();
            CreateMap<PmSecurityDeposit, SecurityDepositVm>()
                .ForMember(d => d.SubProjectDropdown, opts => opts.Ignore())
                .ForMember(d => d.SubProjectTitle, opts => opts.MapFrom(src => src.SubProject.SubProjectTitle));

            #endregion

            #region setup 
            CreateMap<FiscalYearVm, FiscalYear>();
            CreateMap<FiscalYear, FiscalYearVm>();

            CreateMap<BillTypeVm, BillType>();
            CreateMap<BillType, BillTypeVm>();

            CreateMap<QuarterVm, PmQuarter>();
            CreateMap<PmQuarter, QuarterVm>();

            CreateMap<UnitVm, IrmsSetupUnit>();
            CreateMap<IrmsSetupUnit, UnitVm>();

            CreateMap<ConstructionFirmVm, ConstructionFirm>();
            CreateMap<ConstructionFirm, ConstructionFirmVm>();
            #endregion

            #region security
            CreateMap<RegisterVm, User>();
            CreateMap<User, RegisterVm>()
                .ForMember(d => d.RegimentNo, opts => opts.MapFrom(src => src.GeneralInformation.RegimentNo));

            CreateMap<UserVm, User>();
            CreateMap<User, UserVm>()
                .ForMember(d => d.RegimentNo, opts => opts.MapFrom(src => src.GeneralInformation.RegimentNo));

            CreateMap<MenuVm, Menu>();
            CreateMap<Menu, MenuVm>();

            CreateMap<Application, ApplicationVm>();
            CreateMap<ApplicationVm, Application>();

            CreateMap<ActivityLogVm, ActivityLog>();
            CreateMap<ActivityLog, ActivityLogVm>();

            //CreateMap<Device, DeviceVm>();
            //CreateMap<DeviceVm, Device>();

            //CreateMap<UserApplication, UserApplicationVm>();
            //CreateMap<UserApplicationVm, UserApplication>();

            //CreateMap<UserDevice, UserDeviceVm>();
            //CreateMap<UserDeviceVm, UserDevice>();

            //CreateMap<UserLoginDevice, UserLoginDeviceVm>();
            //CreateMap<UserLoginDeviceVm, UserLoginDevice>();

            //CreateMap<UserPriviledge, UserPriviledgeVm>();
            //CreateMap<UserPriviledgeVm, UserPriviledge>();
            #endregion
        }
    }
}