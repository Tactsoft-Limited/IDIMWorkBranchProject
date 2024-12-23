using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq.Dynamic.Core;
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

            existing.ProjectTypeId = model.ProjectTypeId;
            existing.FiscalYearId = model.FiscalYearId;
            existing.ProjectName = model.ProjectName;
            existing.MinistryDepartment = model.MinistryDepartment;
            existing.EstimatedExpenses = model.EstimatedExpenses;
            existing.StartingDate = model.StartingDate;
            existing.EndingDate = model.EndingDate;
            existing.NoOfWork = model.NoOfWork;
            existing.EconomicProgress = model.EconomicProgress;
            existing.ConstructionProgress = model.ConstructionProgress;
            existing.PD = model.PD;
            existing.DPD = model.DPD;
            existing.Remarks = model.Remarks;

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

        public async Task<object> GetByAsync(ProjectSearchVm filter = null)
        {
            if (filter == null)
                filter = new ProjectSearchVm();

            // Build the query with the additional filters
            var query = Context.Projects.Include(x => x.ProjectType).Include(x => x.FiscalYear).Where(x =>
                    (string.IsNullOrEmpty(filter.ProjectName) || x.ProjectName.Contains(filter.ProjectName)) &&
                    (!filter.FiscalYearId.HasValue || x.FiscalYearId == filter.FiscalYearId) &&
                    (!filter.ProjectTypeId.HasValue || x.ProjectTypeId == filter.ProjectTypeId) &&
                    (!filter.StartingDate.HasValue || x.StartingDate >= filter.StartingDate.Value) &&
                    (!filter.EndingDate.HasValue || x.EndingDate <= filter.EndingDate.Value));

            query = !string.IsNullOrEmpty(filter.SortColumn) && !string.IsNullOrEmpty(filter.SortDirection)
                ? query.OrderBy($"{filter.SortColumn} {filter.SortDirection}")
                : query.OrderBy(x => x.ProjectId);  // Default ordering by SubProjectId

            var totalRecords = await query.CountAsync();
            var filteredRecords = await query.CountAsync();
            var pagedData = await query.Skip(filter.PageIndex * filter.PageSize).Take(filter.PageSize)                                   .ToListAsync();

            // Return the response in DataTables format
            var result = new
            {
                draw = filter.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = filteredRecords,
                data = pagedData.Select(x => new ProjectVm
                {
                    ProjectId = x.ProjectId,
                    ProjectTypeId = x.ProjectTypeId,
                    ProjectTypeName = x.ProjectType.ProjectTypeName,
                    FiscalYearId = x.FiscalYearId,
                    FiscalYearDescription = x.FiscalYear != null ? x.FiscalYear.FiscalYearDescription : "-",
                    ProjectName = x.ProjectName,
                    MinistryDepartment = x.MinistryDepartment,
                    EstimatedExpenses = x.EstimatedExpenses,
                    StartingDate = x.StartingDate,
                    EndingDate = x.EndingDate,
                    NoOfWork = x.NoOfWork,
                    EconomicProgress = x.EconomicProgress,
                    ConstructionProgress = x.ConstructionProgress,
                    PD = x.PD,
                    DPD = x.DPD,
                })
            };

            return result;
        }


        public async Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0)
        {
            var projects = await Context.Projects.ToListAsync();

            return projects.Select(e => new SelectListItem
            {
                Text = e.ProjectName,
                Value = e.ProjectId.ToString(),
                Selected = e.ProjectId == selected
            });
        }

        public async Task<IEnumerable<SelectListItem>> GetProductTypeDropdown(int? selected = 0)
        {
            var result = Context.ProjectTypes.Select(e => new SelectListItem
            {
                Value = e.ProjectTypeId.ToString(),
                Text = e.ProjectTypeName,
                Selected = e.ProjectTypeId == selected
            }).ToList();

            return await Task.FromResult(result); // Wrap the result in a completed Task
        }

        public async Task<decimal> GetEstimatedExpenses(int id)
        {
            return await Context.Projects.Where(x => x.ProjectId == id).Select(x => x.EstimatedExpenses).FirstOrDefaultAsync();
        }
    }
}