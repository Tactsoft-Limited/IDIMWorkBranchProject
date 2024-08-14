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
            // entity.StatusTypeId = (int)StatusType.Running;
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
            existing.UnitId = model.UnitId;
            existing.Description = model.Description;
            existing.ConstructionFirmId = model.ConstructionFirmId;
            existing.AgreementCost = model.AgreementCost;
            existing.StartDate = model.StartDate;
            existing.EndDate = model.EndDate;
            // existing.StatusTypeId = (int)model.StatusTypeId;
            existing.Status = model.Status;
            existing.HandOverDate = model.HandOverDate;
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

            var query = GetAll().Where(x =>
                    (string.IsNullOrEmpty(filter.SubProjectTitle) || x.SubProjectTitle.Contains(filter.SubProjectTitle)) &&
                    (string.IsNullOrEmpty(filter.ProjectName) || x.Project.ProjectName.Contains(filter.ProjectName)) &&
                    (!filter.UnitId.HasValue || x.UnitId == filter.UnitId) &&
                    (!filter.ConstructionFirmId.HasValue || x.ConstructionFirmId == filter.ConstructionFirmId))
                .Take(DefaultData.Take);

            var list = await query.ToListAsync();

            return Mapper.Map<List<SubProjectVm>>(list);
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
    }
}