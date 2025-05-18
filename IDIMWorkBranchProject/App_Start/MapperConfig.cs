using AutoMapper;
using BGB.Data.Entities.Admin;
using BGB.Data.Entities.Budget;
using BGB.Data.Entities.Irms;
using BGB.Data.Entities.Wbpm;
using IDIMWorkBranchProject.Models.Setup;
using IDIMWorkBranchProject.Models.User;
using IDIMWorkBranchProject.Models.Wbpm;

namespace IDIMWorkBranchProject
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            #region Wbpm

            CreateMap<RevenueContractAgreementVm, RevenueContractAgreement>();
            CreateMap<RevenueContractAgreement, RevenueContractAgreementVm>();

            CreateMap<RevenueVm, Revenue>();
            CreateMap<Revenue, RevenueVm>();

            CreateMap<RevenueWorkOrderVm, RevenueWorkOrder>();
            CreateMap<RevenueWorkOrder, RevenueWorkOrderVm>();

            CreateMap<RevenuePerformanceSecurityVm, RevenuePerformanceSecurity>();
            CreateMap<RevenuePerformanceSecurity, RevenuePerformanceSecurityVm>();

            CreateMap<RevenueNohaVm, RevenueNoha>();
            CreateMap<RevenueNoha, RevenueNohaVm>();

            CreateMap<HandoverApprovedVm, HandoverApproved>();
            CreateMap<HandoverApproved, HandoverApprovedVm>();
                
            CreateMap<CollateralReturnVm, CollateralReturn>();
            CreateMap<CollateralReturn, CollateralReturnVm>();

            CreateMap<FurnitureBillPaymentVm, FurnitureBillPayment>();
            CreateMap<FurnitureBillPayment, FurnitureBillPaymentVm>();

            CreateMap<FinalBillPaymentVm, FinalBillPayment>();
            CreateMap<FinalBillPayment, FinalBillPaymentVm>();

            CreateMap<BGBFundVm, BGBFund>();                
            CreateMap<BGBFund, BGBFundVm>();

            CreateMap<VatTaxCollateralVm, VatTaxCollateral>();
            CreateMap<VatTaxCollateral, VatTaxCollateralVm>();

            CreateMap<SignatoryAuthorityVm, SignatoryAuthority>();
            CreateMap<SignatoryAuthority, SignatoryAuthorityVm>();

            CreateMap<RecruitmentCommitteeVm, RecruitmentCommittee>();
            CreateMap<RecruitmentCommittee, RecruitmentCommitteeVm>();

            CreateMap<NohaVm, Noha>();
            CreateMap<Noha, NohaVm>();

            CreateMap<PerformanceSecurityVm, PerformanceSecurity>();
            CreateMap<PerformanceSecurity, PerformanceSecurityVm>();

            CreateMap<WorkOrderVm, WorkOrder>();
            CreateMap<WorkOrder, WorkOrderVm>();

            CreateMap<ProjectWorkStatusVm, ProjectWorkStatus>();
            CreateMap<ProjectWorkStatus, ProjectWorkStatusVm>();

            CreateMap<ContractAgreementVm, ContractAgreement>();
            CreateMap<ContractAgreement, ContractAgreementVm>()
                .ForMember(d => d.ProjectWorkTitle, opt => opt.MapFrom(x => x.ProjectWork.ProjectWorkTitle))
                .ForMember(d => d.ProjectWorkTitleB, opt => opt.MapFrom(x => x.ProjectWork.ProjectWorkTitleB))
                .ForMember(d => d.ConstructionFirm, opt => opt.MapFrom(x => x.ConstructionCompany.FirmNameB));

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
            CreateMap<FinancialYearAllocation, FinancialYearAllocationVm>()
                .ForMember(x => x.FiscalYearDescription, opt => opt.MapFrom(x => x.FiscalYear.FiscalYearDescription));

            CreateMap<FiscalYearExpenseVm, FiscalYearExpense>();
            CreateMap<FiscalYearExpense, FiscalYearExpenseVm>()
                .ForMember(x => x.FiscalYearDescription, opt => opt.MapFrom(x => x.FiscalYear.FiscalYearDescription));

            CreateMap<FormalMeetingVm, FormalMeeting>();
            CreateMap<FormalMeeting, FormalMeetingVm>();

            CreateMap<ProjectWorkVm, ProjectWork>();
            CreateMap<ProjectWork, ProjectWorkVm>()
                .ForMember(x => x.ProjectTitle, opt => opt.MapFrom(x => x.ADPProject.ProjectTitle));

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

            CreateMap<UnitVm, IrmsSetupUnit>();
            CreateMap<IrmsSetupUnit, UnitVm>();

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

            #endregion
        }
    }
}