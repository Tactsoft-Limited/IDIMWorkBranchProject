using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using BGB.Data.Database;
using BGB.Data.Entities.Pm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Extentions.Session;
using IDIMWorkBranchProject.Models.WBP;

namespace IDIMWorkBranchProject.Services.WBP
{
    public class ProjectService : IProjectService
    {
        protected IDIMDBEntities Context { get; set; }
        protected IMapper Mapper { get; set; }

        public ProjectService(IMapper mapper)
        {
            Context = new IDIMDBEntities();
            Mapper = mapper;
        }

        private IQueryable<Project> GetAll()
        {
            return Context.Projects.OrderByDescending(e => e.ProjectId).AsQueryable();
        }

        public async Task<List<ProjectVm>> GetAllAsync()
        {
            var list = await GetAll().Take(DefaultData.Take).ToListAsync();

            return Mapper.Map<List<ProjectVm>>(list);
        }

        public async Task<ProjectVm> GetByIdAsync(int id)
        {
            var entity = await Context.Projects.FindAsync(id);

            return Mapper.Map<ProjectVm>(entity);
        }


        public async Task<ProjectVm> InsertAsync(ProjectVm model)
        {
            //var existing = await Context.Projects.FirstOrDefaultAsync(e => e.ProjectCode == model.ProjectCode);
            //if (existing != null)
            //    throw new ArgumentException($"Project No already exists.");

            var entity = Mapper.Map<Project>(model);
            entity.CreatedDateTime = DateTime.Now;
            entity.CreatedUser = UserExtention.GetUserId();

            var added = Context.Projects.Add(entity);

            await Context.SaveChangesAsync();

            return Mapper.Map<ProjectVm>(added);
        }

        public async Task<ProjectVm> UpdateAsync(ProjectVm model)
        {
            var existing = await Context.Projects
                .Where(e => e.ProjectId == model.ProjectId)
                .FirstOrDefaultAsync();
            if (existing == null)
                throw new ArgumentException($"Project does not exists.");

            //var duplicate = await Context.Projects
            //    .Where(e => e.ProjectId != model.ProjectId)
            //    .FirstOrDefaultAsync(e => e.ProjectCode == model.ProjectCode);
            //if (duplicate != null)
            //    throw new ArgumentException($"Project No already exists.");

            existing.DevelopmentTypeId = (int)model.DevelopmentTypeId;
            existing.AuthorizeUnitId = model.AuthorizeUnitId;
            existing.FiscalYearId = model.FiscalYearId;
            existing.ProjectCode = model.ProjectCode;
            existing.ProjectName = model.ProjectName;
            existing.ApprovalDate = model.ApprovalDate;
            existing.ProjectStartDate = model.ProjectStartDate;
            existing.ProjectEndDate = model.ProjectEndDate;
            existing.ProjectDirector = model.ProjectDirector;
            existing.BudgetCapital = model.BudgetCapital;
            existing.BudgetRevenue = model.BudgetRevenue;
            existing.Description = model.Description;
            existing.PicMeetingNo = model.PicMeetingNo;
            existing.Remark = model.Remark;
            existing.UpdatedDateTime = DateTime.Now;
            existing.UpdatedUser = UserExtention.GetUserId();
            existing.UpdateNo += 1;
            await Context.SaveChangesAsync();

            return Mapper.Map<ProjectVm>(existing);
        }

        public async Task<ProjectVm> DeleteAsync(int id)
        {
            var existing = await Context.Projects
                .Where(e => e.ProjectId == id)
                .FirstOrDefaultAsync();
            if (existing == null)
                throw new ArgumentException($"Project does not exists.");

            Context.Projects.Remove(existing);
            await Context.SaveChangesAsync();

            return Mapper.Map<ProjectVm>(existing);
        }

        public async Task<List<ProjectVm>> GetByAsync(ProjectSearchVm filter = null)
        {
            if (filter == null)
                filter = new ProjectSearchVm();

            var query = GetAll().Where(x =>
                    (string.IsNullOrEmpty(filter.ProjectCode) || x.ProjectCode.Contains(filter.ProjectCode)) &&
                    (string.IsNullOrEmpty(filter.ProjectName) || x.ProjectName.Contains(filter.ProjectName)) &&
                    (!filter.FiscalYearId.HasValue || x.FiscalYearId == filter.FiscalYearId) &&
                    (!filter.AuthorizeUnitId.HasValue || x.AuthorizeUnitId == filter.AuthorizeUnitId))
                .Take(DefaultData.Take);

            var list = await query.ToListAsync();

            return Mapper.Map<List<ProjectVm>>(list);
        }


        public async Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0)
        {
            var projects = await Context.Projects.ToListAsync();

            return projects.Select(e => new SelectListItem
            {
                Text = $"{e.ProjectCode} - {e.ProjectName}",
                Value = e.ProjectId.ToString(),
                Selected = e.ProjectId == selected
            });
        }
    }
}