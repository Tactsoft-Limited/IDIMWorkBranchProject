using BGB.Data.Entities.Admin;
using BGB.Data.Entities.Budget;
using BGB.Data.Entities.Irms;
using BGB.Data.Entities.Pm;
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
            modelBuilder.Entity<ProjectDirector>()
               .HasRequired(p => p.ADPProjects)
               .WithMany(x => x.ProjectDirectors)
               .HasForeignKey(p => p.ADPProjectId)
               .WillCascadeOnDelete(false);


            modelBuilder.Entity<Masterplan>()
                .HasRequired(m => m.ADPProject)
                .WithMany(x => x.Masterplans)
                .HasForeignKey(m => m.ADPProjectId)
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

            modelBuilder.Entity<ProjectWork>()
                .HasRequired(p => p.ADPProject)
                .WithMany(x => x.ProjectWorks)
                .HasForeignKey(p => p.ADPProjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProjectWork>()
                .HasRequired(p => p.ConstructionCompany)
                .WithMany(x => x.ProjectWorks)
                .HasForeignKey(p => p.ConstructionCompanyId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ADPReceivePayment>()
                .HasRequired(a => a.ProjectWork)
                .WithMany(x => x.ADPReceivePayments)
                .HasForeignKey(a => a.ProjectWorkId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BGBMiscellaneousFund>()
                .HasRequired(b => b.ProjectWork)
                .WithMany(x => x.BGBMiscellaneousFunds)
                .HasForeignKey(b => b.ProjectWorkId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BGBMiscellaneousFund>()
                .HasOptional(b => b.ADPReceivePayment)
                .WithMany(x => x.BGBMiscellaneousFunds)
                .HasForeignKey(b => b.ADPReceivePaymentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ContractorCompanyPayment>()
                .HasRequired(c => c.ProjectWork)
                .WithMany(x => x.ContractorCompanyPayments)
                .HasForeignKey(c => c.ProjectWorkId)
                .WillCascadeOnDelete(false);
            #endregion

            #region Old Entity
            modelBuilder.Entity<Project>()
                    .HasMany(e => e.ReceivePayments)
                    .WithRequired(e => e.Project)
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.SubProjects)
                .WithRequired(e => e.Project)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.VatTaxes)
                .WithRequired(e => e.Project)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.BGBFunds)
                .WithRequired(e => e.Project)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.SignatoryAuthorities)
                .WithRequired(e => e.Project)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<SubProject>()
                .HasMany(e => e.BillPayments)
                .WithRequired(e => e.SubProject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SubProject>()
              .HasMany(e => e.ProjectStatus)
              .WithRequired(e => e.SubProject)
              .WillCascadeOnDelete(false);

            modelBuilder.Entity<SubProject>()
              .HasMany(e => e.ContractorPayments)
              .WithRequired(e => e.SubProject)
              .WillCascadeOnDelete(false);


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
        #endregion
        public virtual DbSet<ProjectType> ProjectTypes { get; set; }
        public virtual DbSet<SignatoryAuthority> SignatoryAuthorities { get; set; }
        public virtual DbSet<VatTax> VatTaxes { get; set; }
        public virtual DbSet<BGBFund> BGBFunds { get; set; }
        public virtual DbSet<ContractorPayment> ContractorPayments { get; set; }
        public virtual DbSet<ContractorBillPayment> ContractorBillPayments { get; set; }
        public virtual DbSet<IrmsSetupLocation> SetupLocations { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<BillPayment> BillPayments { get; set; }
        public virtual DbSet<BillType> BillTypes { get; set; }
        public virtual DbSet<ConstructionFirm> ConstructionFirms { get; set; }
        public virtual DbSet<ProjectExtend> ProjectExtends { get; set; }
        public virtual DbSet<ProjectProblem> ProjectProblems { get; set; }
        public virtual DbSet<ProjectStatus> ProjectStatus { get; set; }
        public virtual DbSet<Quarter> Quarters { get; set; }
        public virtual DbSet<ReceivePayment> ReceivePayments { get; set; }
        public virtual DbSet<ViewProjectInformationRpt> ViewProjectInformationRpts { get; set; }
        public virtual DbSet<ViewProjectPaymentReceiptRpt> ViewProjectPaymentReceiptRpts { get; set; }
        public virtual DbSet<ViewProjectProblemRpt> ViewProjectProblemRpts { get; set; }
        public virtual DbSet<ViewProjectSubProjectDetailsRpt> ViewProjectSubProjectDetailsRpts { get; set; }
        public virtual DbSet<ViewProjectWiseSubProjectRpt> ViewProjectWiseSubProjectRpts { get; set; }
        public virtual DbSet<FiscalYear> FiscalYears { get; set; }
        public virtual DbSet<PicMeeting> PicMeetings { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<IrmsSetupUnit> SetupUnits { get; set; }
        public virtual DbSet<SubProject> SubProjects { get; set; }
        public virtual DbSet<GeneralInformation> GeneralInformations { get; set; }
        public virtual DbSet<ConsultancyActivity> ConsultancyActivities { get; set; }
        public virtual DbSet<ConsultancyFee> ConsultancyFees { get; set; }
        public virtual DbSet<Consultant> Consultants { get; set; }
        public virtual DbSet<SubProjectDetail> SubProjectDetails { get; set; }
        public virtual DbSet<ViewProjectList> ViewProjectLists { get; set; }
        public virtual DbSet<ViewProjectBillPayment> ViewProjectBillPayments { get; set; }
        public virtual DbSet<FundRelease> FundReleases { get; set; }
        public virtual DbSet<ViewExtended> ViewExtendeds { get; set; }
        public virtual DbSet<ViewProjectProblem> ViewProjectProblems { get; set; }
        public virtual DbSet<SecurityDeposit> SecurityDeposits { get; set; }
        public virtual DbSet<ViewADPReceivePayment> ViewADPReceivePayments { get; set; }
    }
}

