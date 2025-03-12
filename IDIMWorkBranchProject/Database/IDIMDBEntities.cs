using BGB.Data.Entities.Admin;
using BGB.Data.Entities.Budget;
using BGB.Data.Entities.Irms;
using BGB.Data.Entities.Wbpm;
using BGB.Data.SqlViews.Pm;
using BGB.Data.SqlViews.Wbpm;

using System.Data.Entity;

namespace IDIMWorkBranchProject.Data.Database
{
    public partial class IDIMDBEntities : DbContext
    {
        public IDIMDBEntities() : base("name=IDIMDBEntities")
        {
        }

        //partial void OnModelCreatingPartial(DbModelBuilder modelBuilder);

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Wbpm
            modelBuilder.Entity<ADPReceivePayment>()
                .HasRequired(a => a.ProjectWork)
                .WithMany(x => x.ADPReceivePayments)
                .HasForeignKey(a => a.ProjectWorkId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ADPReceivePayment>()
                .HasRequired(a => a.BranchClerk)
                .WithMany(x => x.BranchClerks)
                .HasForeignKey(a => a.BranchClerkId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ADPReceivePayment>()
                .HasRequired(a => a.ConcernedEngineer)
                .WithMany(x => x.ConcernedEngineers)
                .HasForeignKey(a => a.ConcernedEngineerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ADPReceivePayment>()
                .HasRequired(a => a.SectionIC)
                .WithMany(x => x.SectionICs)
                .HasForeignKey(a => a.SectionICId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ADPReceivePayment>()
                .HasRequired(a => a.HeadAssistant)
                .WithMany(x => x.HeadAssistants)
                .HasForeignKey(a => a.HeadAssistantId)
                .WillCascadeOnDelete(false);



            modelBuilder.Entity<BGBMiscellaneousFund>()
                .HasRequired(b => b.ProjectWork)
                .WithMany(x => x.BGBMiscellaneousFunds)
                .HasForeignKey(b => b.ProjectWorkId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BGBMiscellaneousFund>()
                .HasRequired(b => b.ADPReceivePayment)
                .WithMany(x => x.BGBMiscellaneousFunds)
                .HasForeignKey(b => b.ADPReceivePaymentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ContractAgreement>()
                .HasRequired(b => b.ProjectWork)
                .WithMany(x => x.ContractAgreements)
                .HasForeignKey(b => b.ProjectWorkId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ContractAgreement>()
                .HasRequired(b => b.ConstructionCompany)
                .WithMany(x => x.ContractAgreements)
                .HasForeignKey(b => b.ConstructionCompanyId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ContractAgreement>()
                .HasRequired(b => b.AddDG)
                .WithMany(x => x.AddDGs)
                .HasForeignKey(b => b.AddDGId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ContractAgreement>()
                .HasRequired(b => b.DDG)
                .WithMany(x => x.DDGs)
                .HasForeignKey(b => b.DDGId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ContractAgreement>()
                .HasRequired(b => b.ProjectDirector)
                .WithMany(x => x.ProjectDirectors)
                .HasForeignKey(b => b.ProjectDirectorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ContractAgreement>()
                .HasRequired(b => b.Director)
                .WithMany(x => x.Directors)
                .HasForeignKey(b => b.DirectorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ContractorCompanyPayment>()
                .HasRequired(c => c.ProjectWork)
                .WithMany(x => x.ContractorCompanyPayments)
                .HasForeignKey(c => c.ProjectWorkId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FinancialYearAllocation>()
                .HasRequired(f => f.ADPProject)
                .WithMany(x => x.FinancialYearAllocations)
                .HasForeignKey(f => f.ADPProjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FinancialYearAllocation>()
                .HasRequired(f => f.FiscalYear)
                .WithMany(x => x.FinancialYearAllocations)
                .HasForeignKey(f => f.FiscalYearId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FiscalYearExpense>()
                .HasRequired(f => f.ADPProject)
                .WithMany(x => x.FiscalYearExpenses)
                .HasForeignKey(f => f.ADPProjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FiscalYearExpense>()
                .HasRequired(f => f.FiscalYear)
                .WithMany(x => x.FiscalYearExpenses)
                .HasForeignKey(f => f.FiscalYearId)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<FormalMeeting>()
                .HasRequired(f => f.ADPProject)
                .WithMany(x => x.FormalMeetings)
                .HasForeignKey(f => f.ADPProjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Masterplan>()
                .HasRequired(m => m.ADPProject)
                .WithMany(x => x.Masterplans)
                .HasForeignKey(m => m.ADPProjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Noha>()
                .HasRequired(m => m.ProjectWork)
                .WithMany(x => x.Nohas)
                .HasForeignKey(m => m.ProjectWorkId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PerformanceSecurity>()
                .HasRequired(m => m.ProjectWork)
                .WithMany(x => x.PerformanceSecurities)
                .HasForeignKey(m => m.ProjectWorkId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProjectDirector>()
               .HasRequired(p => p.ADPProjects)
               .WithMany(x => x.ProjectDirectors)
               .HasForeignKey(p => p.ADPProjectId)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProjectWork>()
                .HasRequired(p => p.ADPProject)
                .WithMany(x => x.ProjectWorks)
                .HasForeignKey(p => p.ADPProjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProjectWorkStatus>()
               .HasRequired(p => p.ProjectWork)
               .WithMany(x => x.ProjectWorkStatuses)
               .HasForeignKey(p => p.ProjectWorkId)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<VatTaxCollateral>()
              .HasRequired(p => p.ADPReceivePayment)
              .WithMany(x => x.VatTaxCollaterals)
              .HasForeignKey(p => p.ADPReceivePaymentId)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<WorkOrder>()
              .HasRequired(p => p.ProjectWork)
              .WithMany(x => x.WorkOrders)
              .HasForeignKey(p => p.ProjectWorkId)
              .WillCascadeOnDelete(false);

            #endregion

            #region Old Entity

            modelBuilder.Entity<BGB.Data.Entities.Admin.Application>()
                .HasMany(x => x.ActivityLogs)
                .WithRequired(e => e.Application)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BGB.Data.Entities.Admin.Application>()
                .HasMany(x => x.Roles)
                .WithRequired(e => e.Application)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BGB.Data.Entities.Admin.Application>()
                .HasMany(x => x.Menus)
                .WithRequired(e => e.Application)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralInformation>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.GeneralInformation)
                .WillCascadeOnDelete(false);
            #endregion
        }


        #region Admin
        public virtual DbSet<ActivityLog> ActivityLogs { get; set; }

        public virtual DbSet<Application> Applications { get; set; }

        public virtual DbSet<Menu> Menus { get; set; }

        public virtual DbSet<UserPrivilege> UserPrivileges { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<RolePrivilege> RolePrivileges { get; set; }

        public virtual DbSet<Role> Roles { get; set; }
        #endregion

        #region Wbpm
        public DbSet<ProjectDirector> ProjectDirectors { get; set; }
        public DbSet<ConstructionCompany> ConstructionCompanies { get; set; }
        public DbSet<ADPProject> ADPProjects { get; set; }
        public DbSet<Masterplan> Masterplans { get; set; }
        public DbSet<FinancialYearAllocation> FinancialYearAllocations { get; set; }
        public DbSet<FiscalYearExpense> FiscalYearExpenses { get; set; }
        public DbSet<FormalMeeting> FormalMeetings { get; set; }
        public DbSet<ProjectWork> ProjectWorks { get; set; }
        public DbSet<ADPReceivePayment> ADPReceivePayments { get; set; }
        public DbSet<BGBMiscellaneousFund> BGBMiscellaneousFunds { get; set; }
        public DbSet<ContractorCompanyPayment> ContractorCompanyPayments { get; set; }
        public DbSet<ContractAgreement> ContractAgreements { get; set; }
        public virtual DbSet<SignatoryAuthority> SignatoryAuthorities { get; set; }
        public virtual DbSet<TenderEvaluationCommittee> TenderEvaluationCommittees { get; set; }
        public virtual DbSet<VatTaxCollateral> VatTaxCollaterals { get; set; }
        public virtual DbSet<ProjectSignatoryAuthority> ProjectSignatoryAuthorities { get; set; }
        public virtual DbSet<RecruitmentCommittee> RecruitmentCommittees { get; set; }
        public virtual DbSet<Noha> Nohas { get; set; }
        public virtual DbSet<WorkOrder> WorkOrders { get; set; }
        public virtual DbSet<PerformanceSecurity> PerformanceSecurities { get; set; }
        public virtual DbSet<ProjectWorkStatus> ProjectWorkStatuses { get; set; }
        #endregion

        public virtual DbSet<IrmsSetupLocation> SetupLocations { get; set; }
        public virtual DbSet<FiscalYear> FiscalYears { get; set; }
        public virtual DbSet<IrmsSetupUnit> SetupUnits { get; set; }
        public virtual DbSet<GeneralInformation> GeneralInformations { get; set; }
        public virtual DbSet<ViewProjectInformationRpt> ViewProjectInformationRpts { get; set; }
        public virtual DbSet<ViewProjectPaymentReceiptRpt> ViewProjectPaymentReceiptRpts { get; set; }
        public virtual DbSet<ViewProjectProblemRpt> ViewProjectProblemRpts { get; set; }
        public virtual DbSet<ViewProjectSubProjectDetailsRpt> ViewProjectSubProjectDetailsRpts { get; set; }
        public virtual DbSet<ViewProjectWiseSubProjectRpt> ViewProjectWiseSubProjectRpts { get; set; }
        public virtual DbSet<ViewProjectList> ViewProjectLists { get; set; }
        public virtual DbSet<ViewProjectBillPayment> ViewProjectBillPayments { get; set; }
        public virtual DbSet<ViewExtended> ViewExtendeds { get; set; }
        public virtual DbSet<ViewProjectProblem> ViewProjectProblems { get; set; }
        public virtual DbSet<ViewADPReceivePayment> ViewADPReceivePayments { get; set; }
        public virtual DbSet<ViewContactAgreement> ViewContactAgreements { get; set; }
        public virtual DbSet<ViewVatTaxReport> ViewVatTaxReports { get; set; }
        public virtual DbSet<ViewContractorCompanyPayment> ViewContractorCompanyPayments { get; set; }
        public virtual DbSet<ViewBGBMiscellaneousFund> ViewBGBMiscellaneousFunds { get; set; }

    }
}

