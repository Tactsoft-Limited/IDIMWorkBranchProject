using BGB.Data.Entities.Admin;
using BGB.Data.Entities.Budget;
using BGB.Data.Entities.Central;
using BGB.Data.Entities.Irms;
using BGB.Data.Entities.Pm;
using BGB.Data.SqlViews.Pm;
using System.Data.Entity;

namespace BGB.Data.Database
{
    public partial class IDIMDBEntities : DbContext
    {
        public IDIMDBEntities() : base("name=IDIMDBEntities")
        {
        }

        //partial void OnModelCreatingPartial(DbModelBuilder modelBuilder);

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Entities.Admin.Application>()
                .HasMany(x => x.ActivityLogs)
                .WithRequired(e => e.Application)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Entities.Admin.Application>()
                .HasMany(x => x.Roles)
                .WithRequired(e => e.Application)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Entities.Admin.Application>()
                .HasMany(x => x.Menus)
                .WithRequired(e => e.Application)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GeneralInformation>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.GeneralInformation)
                .WillCascadeOnDelete(false);
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

        public virtual DbSet<IrmsSetupLocation> SetupLocations { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<BillPayment> BillPayments { get; set; }
        public virtual DbSet<BillType> BillTypes { get; set; }
        public virtual DbSet<ConstructionFirm> ConstructionFirms { get; set; }
        public virtual DbSet<ProjectExtend> ProjectExtends { get; set; }
        public virtual DbSet<ProjectProblem> ProjectProblems { get; set; }
        public virtual DbSet<ProjectStatus> ProjectStatus { get; set; }
        public virtual DbSet<PmQuarter> Quarters { get; set; }
        public virtual DbSet<ReceiptPayment> ReceiptPayments { get; set; }
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
        public virtual DbSet<PmSecurityDeposit> SecurityDeposits { get; set; }
    }
}

