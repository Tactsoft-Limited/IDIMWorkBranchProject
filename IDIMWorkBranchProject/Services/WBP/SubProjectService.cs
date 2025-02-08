using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq.Dynamic.Core;
using AutoMapper;
using BGB.Data.Entities.Pm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Extentions.Session;
using IDIMWorkBranchProject.Models.WBP;
using IDIMWorkBranchProject.Data.Database;

namespace IDIMWorkBranchProject.Services.WBP
{
    public class SubProjectService : ISubProjectService
    {
        protected IDIMDBEntities Context { get; set; }
        protected IMapper Mapper { get; set; }

        public SubProjectService(IMapper mapper)
        {
            Context = new IDIMDBEntities();
            Mapper = mapper;
        }

        private IQueryable<SubProject> GetAll()
        {
            return Context.SubProjects.OrderByDescending(e => e.SubProjectId).AsQueryable();
        }

        public async Task<List<SubProjectVm>> GetAllAsync()
        {
            var list = await GetAll().Take(DefaultData.Take).ToListAsync();

            return Mapper.Map<List<SubProjectVm>>(list);
        }

        public async Task<object> GetAllAsync(SubProjectSearchVm filter)
        {
            if (filter == null)
                filter = new SubProjectSearchVm();

            // Querying SubProjects and including related Project
            var data = Context.SubProjects
                .Include(x => x.Project)
                .Include(x => x.ConstructionFirm)
                .AsQueryable();

            // Apply filters to the SubProject data
            var query = data.Where(x =>
                (string.IsNullOrEmpty(filter.SubProjectTitle) || x.SubProjectTitle.Contains(filter.SubProjectTitle)) &&
                (string.IsNullOrEmpty(filter.ProjectName) || x.Project.ProjectName.Contains(filter.ProjectName)) &&
                (!filter.ConstructionFirmId.HasValue || x.ConstructionFirmId == filter.ConstructionFirmId) &&
                ((!filter.StartDate.HasValue && !filter.EndDate.HasValue) ||
                (filter.StartDate.HasValue && !filter.EndDate.HasValue &&
                    DbFunctions.TruncateTime(x.StartDate) >= DbFunctions.TruncateTime(filter.StartDate)) ||
                (!filter.StartDate.HasValue &&
                    filter.EndDate.HasValue &&
                    DbFunctions.TruncateTime(x.EndDate) <= DbFunctions.TruncateTime(filter.EndDate)) ||
                (filter.StartDate.HasValue &&
                    filter.EndDate.HasValue &&
                    DbFunctions.TruncateTime(x.StartDate) >= DbFunctions.TruncateTime(filter.StartDate) &&
                    DbFunctions.TruncateTime(x.EndDate) <= DbFunctions.TruncateTime(filter.EndDate))));

            // Sorting: Dynamically apply sorting based on DataTable's request
            query = !string.IsNullOrEmpty(filter.SortColumn) && !string.IsNullOrEmpty(filter.SortDirection)
                ? query.OrderBy($"{filter.SortColumn} {filter.SortDirection}")
                : query.OrderBy(x => x.SubProjectId);  // Default ordering by SubProjectId


            // Get the total count of records before pagination (for recordsTotal)
            var totalRecords = await data.CountAsync(); // All records in the database without filter
                                                        // Get the total count of records after applying filters (for recordsFiltered)
            var filteredRecords = await query.CountAsync(); // After filters applied

            // Pagination: Apply pagination based on DataTable's filter
            var pagedRecords = await query
                .Skip(filter.PageIndex * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();


            // Return the response in DataTables format
            var result = new
            {
                draw = filter.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = filteredRecords,
                data = pagedRecords.Select(x => new SubProjectVm
                {
                    SubProjectId = x.SubProjectId,
                    SubProjectTitle = x.SubProjectTitle,
                    ProjectId = x.ProjectId,
                    ProjectName = x.Project.ProjectName,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    HandOverDate = x.HandOverDate,
                    ConstructionFirmId = x.ConstructionFirmId,
                    ConstructionFirmName = x.ConstructionFirm.ConstructionFirmName,
                })
            };

            return result;
        }


        public async Task<SubProjectVm> GetByIdAsync(int id)
        {
            var entity = await Context.SubProjects.FindAsync(id);

            return Mapper.Map<SubProjectVm>(entity);
        }


        public async Task<SubProjectVm> InsertAsync(SubProjectVm model)
        {
            var existing = await Context.SubProjects.FirstOrDefaultAsync(e => e.SubProjectTitle == model.SubProjectTitle);
            if (existing != null)
                throw new ArgumentException($"Sub Project Title already exists.");

            var entity = Mapper.Map<SubProject>(model);
            entity.CreatedDateTime = DateTime.Now;
            entity.CreatedUser = UserExtention.GetUserId();

            var added = Context.SubProjects.Add(entity);

            await Context.SaveChangesAsync();

            return Mapper.Map<SubProjectVm>(added);
        }

        public async Task<SubProjectVm> UpdateAsync(SubProjectVm model)
        {
            var existing = await Context.SubProjects
                .Where(e => e.SubProjectId == model.SubProjectId)
                .FirstOrDefaultAsync();
            if (existing == null)
                throw new ArgumentException($"Sub Project does not exists.");

            var duplicate = await Context.SubProjects
                .Where(e => e.SubProjectId != model.SubProjectId)
                .FirstOrDefaultAsync(e => e.SubProjectTitle == model.SubProjectTitle);
            if (duplicate != null)
                throw new ArgumentException($"Sub Project Title  already exists.");

            existing.SubProjectTitle = model.SubProjectTitle;
            existing.Description = model.Description;
            existing.ConstructionFirmId = model.ConstructionFirmId;
            existing.AgreementCost = model.AgreementCost;
            existing.StartDate = model.StartDate;
            existing.EndDate = model.EndDate;
            existing.HandOverDate = model.HandOverDate;
            existing.BankGuarantee = model.BankGuarantee;
            existing.BankGuaranteeEndDate = model.BankGuaranteeEndDate;
            existing.NOA = model.NOA;
            existing.NOADate = model.NOADate;
            existing.Agreement = model.Agreement;
            existing.AgreementDate = model.AgreementDate;
            existing.WorkOrder = model.WorkOrder;
            existing.WorkOrderDate = model.WorkOrderDate;
            existing.Remark = model.Remark;

            existing.UpdatedDateTime = DateTime.Now;
            existing.UpdatedUser = UserExtention.GetUserId();
            existing.UpdateNo += 1;
            await Context.SaveChangesAsync();

            return Mapper.Map<SubProjectVm>(existing);
        }

        public async Task<SubProjectVm> DeleteAsync(int id)
        {
            var existing = await Context.SubProjects
                .Where(e => e.SubProjectId == id)
                .FirstOrDefaultAsync();
            if (existing == null)
                throw new ArgumentException($"Sub Project  does not exists.");

            Context.SubProjects.Remove(existing);
            await Context.SaveChangesAsync();

            return Mapper.Map<SubProjectVm>(existing);
        }

        public async Task<List<SubProjectVm>> GetByAsync(SubProjectSearchVm filter = null)
        {
            if (filter == null)
                filter = new SubProjectSearchVm();
            var data = Context.SubProjects.Include(x => x.Project).Include(x => x.ConstructionFirm).AsQueryable();
            // Start query with GetAll() (presumably getting all SubProject entities)
            var query = data.Where(x =>
                    (string.IsNullOrEmpty(filter.SubProjectTitle) || x.SubProjectTitle.Contains(filter.SubProjectTitle)) &&
                    (string.IsNullOrEmpty(filter.ProjectName) || x.Project.ProjectName.Contains(filter.ProjectName)) &&
                    (!filter.ConstructionFirmId.HasValue || x.ConstructionFirmId == filter.ConstructionFirmId));

            // Group by ProjectName (you can include other properties in the grouping if needed)
            var groupedQuery = query
                .GroupBy(x => x.ProjectId)  // Group by ProjectId
                .Select(g => new
                {
                    ProjectId = g.Key,  // The ProjectId (group key)
                    ProjectName = g.Select(x => x.Project.ProjectName).FirstOrDefault(),  // Get the ProjectName (assumes one Project per ProjectId)
                    TotalProjects = g.Count(),  // Total number of SubProjects in this group
                    SubProjects = g.ToList()  // List of SubProjects in this group
                });

            // Take the desired number of records from the grouped query
            var groupedList = await groupedQuery.ToListAsync();

            // Flatten the groups into a list of SubProjectVm
            var result = groupedList.SelectMany(g => g.SubProjects)  // Flatten the grouped SubProjects
                .ToList();

            // Map the result to SubProjectVm and return
            return Mapper.Map<List<SubProjectVm>>(result);
        }


        public async Task<IEnumerable<SelectListItem>> GetDropdownAsync(int? selected = 0)
        {
            var subProjects = await Context.SubProjects.ToListAsync();

            return subProjects.Select(e => new SelectListItem
            {
                Text = e.SubProjectTitle,
                Value = e.SubProjectId.ToString(),
                Selected = e.SubProjectId == selected
            });
        }

        public async Task<IEnumerable<SelectListItem>> GetAllFirmByProject(int projectId)
        {
            return await (from cf in Context.ConstructionFirms
                          join sp in Context.SubProjects on cf.ConstructionFirmId equals sp.ConstructionFirmId
                          join p in Context.Projects on sp.ProjectId equals p.ProjectId
                          where p.ProjectId == projectId
                          select new SelectListItem
                          {
                              Text = cf.ConstructionFirmName,  // Text for the dropdown
                              Value = cf.ConstructionFirmId.ToString()  // Value for the dropdown item
                          }).Distinct().ToListAsync();
        }

        public async Task<SubProjectVm> GetByProjectIdAsync(int projectId)
        {
            var result = await Context.SubProjects.Where(x => x.ProjectId == projectId).FirstOrDefaultAsync();
            return Mapper.Map<SubProjectVm>(result);
        }

    }
}