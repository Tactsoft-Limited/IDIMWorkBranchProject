using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BGB.Data.Database;
using BGB.Data.Entities.Pm;
using IDIMWorkBranchProject.Extentions;
using IDIMWorkBranchProject.Extentions.Session;
using IDIMWorkBranchProject.Models.WBP;

namespace IDIMWorkBranchProject.Services.WBP
{
    public class ProjectStatusService : IProjectStatusService
    {
        protected IDIMDBEntities Context { get; set; }
        protected IMapper Mapper { get; set; }

        public ProjectStatusService(IMapper mapper)
        {
            Context = new IDIMDBEntities();
            Mapper = mapper;
        }

        private IQueryable<ProjectStatus> GetAll()
        {
            return Context.ProjectStatus.OrderByDescending(e => e.ProjectStatusId).AsQueryable();
        }

        public async Task<List<ProjectStatusVm>> GetAllAsync()
        {
            var list = await GetAll().Take(DefaultData.Take).ToListAsync();

            return Mapper.Map<List<ProjectStatusVm>>(list);
        }

        public async Task<ProjectStatusVm> GetByIdAsync(int id)
        {
            var entity = await Context.ProjectStatus.FindAsync(id);

            return Mapper.Map<ProjectStatusVm>(entity);
        }


        public async Task<ProjectStatusVm> InsertAsync(ProjectStatusVm model)
        {

            var entity = Mapper.Map<ProjectStatus>(model);
            entity.CreatedDateTime = DateTime.Now;
            entity.CreatedUser = UserExtention.GetUserId();

            var added = Context.ProjectStatus.Add(entity);

            await Context.SaveChangesAsync();

            return Mapper.Map<ProjectStatusVm>(added);
        }

        public async Task<ProjectStatusVm> UpdateAsync(ProjectStatusVm model)
        {
            var existing = await Context.ProjectStatus.FirstOrDefaultAsync((System.Linq.Expressions.Expression<Func<ProjectStatus, bool>>)(e => e.SubProjectId == model.SubProjectId));
            if (existing == null)
                throw new ArgumentException($"Project Status  does not exists.");

            existing.Progress = model.Progress;
            existing.PictureId = model.PictureId;
            existing.StatusDate = model.StatusDate;
            existing.Description = model.Description;
            existing.Remark = model.Remark;
            existing.UpdatedDateTime = DateTime.Now;
            existing.UpdatedUser = UserExtention.GetUserId();
            existing.UpdateNo += 1;
            await Context.SaveChangesAsync();

            return Mapper.Map<ProjectStatusVm>(existing);
        }

        public async Task<List<ProjectStatusVm>> GetByAsync(ProjectStatusSearchVm filter = null)
        {
            if (filter == null)
                filter = new ProjectStatusSearchVm();

            var query = GetAll().Where(x =>
                    (string.IsNullOrEmpty(filter.SubProjectTitle) || x.SubProject.SubProjectTitle.Contains(filter.SubProjectTitle)))
                .Take(DefaultData.Take);

            var list = await query.ToListAsync();

            return Mapper.Map<List<ProjectStatusVm>>(list);
        }

        public async Task<ProjectStatusVm> DeleteAsync(int id)
        {
            var existing = await Context.ProjectStatus.FirstOrDefaultAsync((System.Linq.Expressions.Expression<Func<ProjectStatus, bool>>)(e => e.SubProjectId == id));
            if (existing == null)
                throw new ArgumentException($"Project Status does not exists.");

            Context.ProjectStatus.Remove((ProjectStatus)existing);
            await Context.SaveChangesAsync();

            return Mapper.Map<ProjectStatusVm>(existing);
        }
    }
}