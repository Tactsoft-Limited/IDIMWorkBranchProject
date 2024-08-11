using AutoMapper;
using IDIMWorkBranchProject.Entity;
using IDIMWorkBranchProject.Models.Dashboard;
using IDIMWorkBranchProject.Models.WBP;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
namespace IDIMWorkBranchProject.Services.Dashboard
{
    public class DashboardService : IDashboardService
    {
        protected IMapper Mapper { get; set; }
        protected IDIMDBEntities Context { get; set; }

        public DashboardService(IMapper mapper)
        {
            Context = new IDIMDBEntities();
            Mapper = mapper;
        }

        public async Task<DashboardVm> GetAll()
        {
            var model = new DashboardVm
            {
                
                Project = await Context.Projects.CountAsync(),
                Subproject = await Context.SubProjects.CountAsync(),
                TotalBillPayment = await Context.BillPayments.SumAsync(x => (double?)x.PaymentAmount) ?? 0,
                TotalBillReceived = await Context.ReceiptPayments.SumAsync(x => (double?)x.ReceiptAmount) ?? 0,
               
                ProjectExtended=await Context.ProjectExtends.CountAsync(),
                ProjectProblem=await Context.ProjectProblems.CountAsync()

            };

            return model;
        }


        public async Task<List<ProjectVm>> ProjectList()
        {
            var query = Context.Projects.AsQueryable();

            var projectDis = await query.ToListAsync();

            var model = projectDis.Select(e => new ProjectVm()
            {
                ProjectCode = e.ProjectCode,
                ProjectName = e.ProjectName,
                ProjectStartDate=e.ProjectStartDate
            }).ToList();

            return model;
        }








    }
}