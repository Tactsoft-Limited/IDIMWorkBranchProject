using AutoMapper;
using BGB.Data.Entities.Admin;
using BGB.Data.Entities.Budget;
using BGB.Data.Entities.Irms;
using BGB.Data.Entities.Pm;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Models.Setup;
using IDIMWorkBranchProject.Models.User;
using IDIMWorkBranchProject.Models.WBP;
using IDIMWorkBranchProject.Models.Wbpm;

namespace IDIMWorkBranchProject
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            #region wbp 
            CreateMap<ProjectVm, Project>();
            CreateMap<Project, ProjectVm>()
                .ForMember(d => d.FiscalYearDropdown, opts => opts.Ignore())
                .ForMember(d => d.FiscalYearDescription, opt => opt.MapFrom(x => x.FiscalYear.FiscalYearDescription))
                .ForMember(d => d.ProjectTypeName, opt => opt.MapFrom(x => x.ProjectType.ProjectTypeName));


            CreateMap<SubProjectVm, SubProject>();
            CreateMap<SubProject, SubProjectVm>()
                .ForMember(d => d.ConstructionFirmDropdown, opts => opts.Ignore())
                .ForMember(d => d.ProjectName, opts => opts.MapFrom(src => src.Project.ProjectName))
                .ForMember(d => d.ConstructionFirmName, opts => opts.MapFrom(src => src.ConstructionFirm.ConstructionFirmName));

            CreateMap<SignatoryAuthorityVm, SignatoryAuthority>();
            CreateMap<SignatoryAuthority, SignatoryAuthorityVm>()
                .ForMember(d => d.ProjectName, opt => opt.MapFrom(src => src.Project.ProjectName))
                .ForMember(d => d.LetterNumber, opt => opt.MapFrom(src => src.ReceivePayment.LetterNo));

            CreateMap<VatTaxVm, VatTax>();
            CreateMap<VatTax, VatTaxVm>()
                .ForMember(d => d.ProjectName, opt => opt.MapFrom(src => src.Project.ProjectName))
                .ForMember(d => d.LetterNumber, opt => opt.MapFrom(src => src.ReceivePayment.LetterNo))
                .ForMember(d => d.ReceiveAmount, opt => opt.MapFrom(src => src.ReceivePayment.BillAmount));

            CreateMap<BGBFundVm, BGBFund>();
            CreateMap<BGBFund, BGBFundVm>();

            CreateMap<ContractorPaymentVm, ContractorPayment>();
            CreateMap<ContractorPayment, ContractorPaymentVm>()
                .ForMember(d => d.ConstructionFirmName, opts => opts.MapFrom(src => src.ConstructionFirm.ConstructionFirmName))
                .ForMember(d => d.SubProjectTitle, opt => opt.MapFrom(d => d.SubProject.SubProjectTitle));

            CreateMap<ContractorBillPaymentVm, ContractorBillPayment>();
            CreateMap<ContractorBillPayment, ContractorBillPaymentVm>();

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

            CreateMap<ReceivePaymentVm, ReceivePayment>();
            CreateMap<ReceivePayment, ReceivePaymentVm>()
                .ForMember(d => d.ProjectName, opts => opts.MapFrom(src => src.Project.ProjectName))
                .ForMember(d => d.ConstructionFirmName, opts => opts.MapFrom(src => src.ConstructionFirm.ConstructionFirmName));

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

            CreateMap<SecurityDepositVm, SecurityDeposit>();
            CreateMap<SecurityDeposit, SecurityDepositVm>()
                .ForMember(d => d.SubProjectDropdown, opts => opts.Ignore())
                .ForMember(d => d.SubProjectTitle, opts => opts.MapFrom(src => src.SubProject.SubProjectTitle));

            #endregion

            #region Wbpm

            CreateMap<ProjectDirectorVm, ProjectDirector>();
            CreateMap<ProjectDirector, ProjectDirectorVm>()
                .ForMember(x => x.ProjectTitle, opt => opt.MapFrom(x => x.ADPProjects.ProjectTitle));

            CreateMap<ConstructionCompany, ConstructionCompanyVm>();
            CreateMap<ConstructionCompanyVm, ConstructionCompany>();

            CreateMap<ADPProjectVm, ADPProject>();
            CreateMap<ADPProject, ADPProjectVm>();

            CreateMap<MasterplanVm, Masterplan>();
            CreateMap<Masterplan, MasterplanVm>();

            CreateMap<FinancialYearAllocationVm, FinancialYearAllocation>();
            CreateMap<FinancialYearAllocation, FinancialYearAllocationVm>();

            CreateMap<FiscalYearExpenseVm, FiscalYearExpense>();
            CreateMap<FiscalYearExpense, FiscalYearExpenseVm>();

            CreateMap<FormalMeetingVm, FormalMeeting>();
            CreateMap<FormalMeeting, FormalMeetingVm>();

            CreateMap<ProjectWorkVm, ProjectWork>();
            CreateMap<ProjectWork, ProjectWorkVm>();

            CreateMap<ADPReceivePaymentVm, ADPReceivePayment>();
            CreateMap<ADPReceivePayment, ADPReceivePaymentVm>();

            CreateMap<BGBMiscellaneousFundVm, BGBMiscellaneousFund>();
            CreateMap<BGBMiscellaneousFund, BGBMiscellaneousFundVm>();

            CreateMap<ContractorCompanyPaymentVm, ContractorCompanyPayment>();
            CreateMap<ContractorCompanyPayment, ContractorCompanyPaymentVm>();

            #endregion

            #region setup 
            CreateMap<FiscalYearVm, FiscalYear>();
            CreateMap<FiscalYear, FiscalYearVm>();

            CreateMap<BillTypeVm, BillType>();
            CreateMap<BillType, BillTypeVm>();

            CreateMap<QuarterVm, Quarter>();
            CreateMap<Quarter, QuarterVm>();

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